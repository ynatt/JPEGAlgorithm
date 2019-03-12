using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JPEGAlgorithm
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void openPictureButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.png)|*.png";
            if (openFileDialog.ShowDialog() == DialogResult.OK) {
                sourceImageBox.Image = Image.FromFile(openFileDialog.FileName);
            }
        }

        private void buttonToRGBMatrix_Click(object sender, EventArgs e) {
            int dim = Int32.Parse(dimentionTextBox.Text);
            int x = Int32.Parse(xTextBox.Text);
            int y = Int32.Parse(yTextBox.Text);
            Image result;
            if (colorSchemeComboBox.Text == "RGB") {
                RGBImage imageRGB = new RGBImage(sourceImageBox.Image);
                RGBImage subImage = isFullImage.Checked ? imageRGB : imageRGB.SubImage(dim, x, y);
                switch (channelComboBox.Text) {
                    case "RED":
                        result = subImage.GetRedChannel();
                        break;
                    case "GREEN":
                        result = subImage.GetGreenChannel();
                        break;
                    case "BLUE":
                        result = subImage.GetBlueChannel();
                        break;
                    default:
                        result = subImage.ToImage();
                        break;
                }
                resultPictureBox.Image = result;
                //Console.WriteLine(subImage);
            }
            if (colorSchemeComboBox.Text == "YCbCr") {
                YCbCrImage imageYCbCr = new YCbCrImage(sourceImageBox.Image);
                YCbCrImage subImage = isFullImage.Checked ? imageYCbCr : imageYCbCr.SubImage(dim, x, y);
                switch (channelComboBox.Text) {
                    case "Y":
                        result = subImage.GetYChannel();
                        break;
                    case "Cb":
                        result = subImage.GetCbChannel();
                        break;
                    case "Cr":
                        result = subImage.GetCrChannel();
                        break;
                    default:
                        result = subImage.ToImage();
                        break;
                }
                resultPictureBox.Image = result;
                //Console.WriteLine(subImage);
            }
        }

        private void comboBox1_SelectedValueChanged(object sender, EventArgs e) {
            ComboBox comboBox = (ComboBox)sender;
            if (comboBox.SelectedValue != null && comboBox.SelectedValue.ToString() == JPEG.ColorScheme.YCbCr.ToString()) {
                channelComboBox.DataSource = Enum.GetValues(typeof(YCbCrImage.Channel));
            } else {
                channelComboBox.DataSource = Enum.GetValues(typeof(RGBImage.Channel));
            }
        }

        private void MainForm_Load(object sender, EventArgs e) {
            channelComboBox.DataSource = Enum.GetValues(typeof(RGBImage.Channel));
            colorSchemeComboBox.DataSource = Enum.GetValues(typeof(JPEG.ColorScheme));
        }

        private void isFullImage_CheckedChanged(object sender, EventArgs e) {
            var isVisible = !isFullImage.Checked; 
            dimentionTextBox.Visible = isVisible;
            xTextBox.Visible = isVisible;
            yTextBox.Visible = isVisible;
        }

        private void averageResultButton_Click(object sender, EventArgs e) {
            RGBImage image = new RGBImage(resultPictureBox.Image);
            var pixels = image.Pixels;
            var channel = new int[image.Width, image.Height];
            switch (channelComboBox.SelectedValue) {
                case YCbCrImage.Channel.Cb:
                    for (var i = 0; i < image.Width; i++) {
                        for (var j = 0; j < image.Height; j++) {
                            channel[i, j] = pixels[i, j].g;
                        }
                    }
                    break;
                case YCbCrImage.Channel.Cr:
                    for (var i = 0; i < image.Width; i++) {
                        for (var j = 0; j < image.Height; j++) {
                            channel[i, j] = pixels[i, j].b;
                        }
                    }
                    break;
            }
            channel = new Averager().Average(new ImageBlock(channel)).Data;
            for (var i = 0; i < image.Width; i++) {
                for (var j = 0; j < image.Height; j++) {
                    pixels[i, j] = new RGBPixel(channel[i, j], channel[i, j], channel[i, j]);
                }
            }
            resultPictureBox.Image = image.ToImage();
        }

        private void CompressButton_Click(object sender, EventArgs e) {
            Averager averager = new Averager();
            var dim = Int32.Parse(dimentionTextBox.Text);
            var x = Int32.Parse(xTextBox.Text);
            var y = Int32.Parse(yTextBox.Text);
            YCbCrImage.Channel channel = (YCbCrImage.Channel)channelComboBox.SelectedValue;
            YCbCrImage image = new YCbCrImage(sourceImageBox.Image).SubImage(dim, x, y);
            ImageBlock block = image.GetImageBlock(channel);
            Console.WriteLine(block);
            ImageUtils.Shift(block);
            Console.WriteLine();
            Console.WriteLine(block);
            if (doAverageChannel.Checked) {
                block = averager.Average(block);
            }
            var dct = TransformsUtils.DCT(block);
            MathUtils.RoundMatrix(dct);
            Show("Спектр косинус преобразования", dct);
            var q = Double.Parse(quantCoeff.Text);
            var Q = MathUtils.BuildQuantizationMatrix(q, dim);
            Show("Матрица квантования", Q);
            MathUtils.Quantize(dct, Q);
            Show("Спектр после квантования", dct);
            MathUtils.RoundMatrix(dct);
            Show("Спектр после округления", dct);
        }

        private static void Show(String message, double[,] matrix) {
            StringBuilder sb = new StringBuilder();
            sb.Append(message).Append("\n");
            for (var i = 0; i < matrix.GetLength(0); i++) {
                for (var j = 0; j < matrix.GetLength(1); j++) {
                    sb.Append(matrix[i, j]).Append(" ");
                }
                sb.Append("\n");
            }
            Console.WriteLine(sb.ToString());
        }

        //private int tick = 0;

        //private RGBImage[] blocks;

        private void fitToBlockButton_Click(object sender, EventArgs e) {
            var sourceImage = sourceImageBox.Image;
            var quantCoeff = Double.Parse(this.quantCoeff.Text);
            var blockSize = Int32.Parse(dimentionTextBox.Text);
            var chunkSize = blockSize * 2;
            var fittedToChunksImage = ImageUtils.FitToBlockSize(new RGBImage(sourceImage), chunkSize);
            var chunks = fittedToChunksImage.ToBlockArray(chunkSize);
            var dcCoeffs = new List<int>();
            Chunk chunk;
            DCTChunk dCTChunk;
            for (int i = 0; i < chunks.Length; i++) {
                chunk = new Chunk(chunks[i]);
				dCTChunk = new DCTChunk(chunk);
                dCTChunk.Quantize(quantCoeff);
                dcCoeffs.AddRange(dCTChunk.getDCCoeffs());
				//Console.WriteLine(dCTChunk);
			}
			//Console.WriteLine(String.Join(", ", dcCoeffs.ToArray()));
			var dcDiffs = MathUtils.MakeDiffOfDCCoeffs(dcCoeffs);
			var coeffsLengths = dcDiffs.ConvertAll(c => NumberUtils.GetBitsLength(c));
            //Console.WriteLine(String.Join(", ", MathUtils.buildBarChart(dcCoeffs.ToArray()).ToArray()));
            var barChart = MathUtils.BuildBarChart(coeffsLengths);
			var tuples = barChart.Select(entry => new Tuple<int, int>(entry.Key, entry.Value));
			var compressor = new HaffmanCompress(tuples);
			compressor.buildTree();
			NodeUtils.showAsTree(compressor.Tree);
			compressor.buildCodeTable();
			Console.WriteLine(string.Join(";", compressor.CodeTable.Select(x => x.Key + "=" + x.Value).ToArray()));
			chart1.Series["DC Coeffs Diffs"].Points.DataBindXY(barChart.Keys, barChart.Values);
            /*double[] array = {1, 2, 4, 7, 5, 3, 6, 8, 9};
            int[,] data = {{ 1, 1, 2, 2, 3, 3, 4, 4 },
                            { 1, 2, 2, 2, 3, 3, 4, 4 },
                            { 5, 5, 6, 6, 7, 7, 8, 8 },
                            { 5, 5, 6, 6, 7, 7, 8, 8 },
                            { 9, 9, 10, 10, 11, 11, 12, 12 },
                            { 9, 9, 10, 10, 11, 11, 12, 12 },
                            { 13, 13, 14, 14, 15, 15, 20, 16 },
                            { 13, 13, 14, 14, 15, 15, 16, 16 }};
            ImageBlock[,] imageBlocks = { { new ImageBlock(data), new ImageBlock(data) },
                                          { new ImageBlock(data), new ImageBlock(data)} };
            Console.WriteLine(new Averager().Average(imageBlocks));
            */
            /*Show("Ect", MatrixUtils<double>.ToMatrixByZigZag(array, 3));
            var arr = MatrixUtils<double>.ToArrayByZigZag(MatrixUtils<double>.ToMatrixByZigZag(array, 3));
            for (int i = 0; i < arr.Length; i++) {
                Console.WriteLine(arr[i] + " ");
            }*/
            //Timer timer = new Timer();
            //timer.Interval = 1000;
            //timer.Tick += new System.EventHandler(this.nextBlock);
            //timer.Start();
        }

		private void button1_Click(object sender, EventArgs e) {
			var array = new int[]{ 0, 1, 3, 7, 15, 31, 63, 127, 255, 511, 1023, 2047, -2047};
			for (int i = 0; i < array.Length; i++) {
				Console.WriteLine(array[i] + " " + NumberUtils.GetBitsLength(array[i]));
			}
			/*var tuples = new List<Tuple<int, int>>();
			tuples.Add(new Tuple<int, int>(6, 200));
			tuples.Add(new Tuple<int, int>(4, 184));
			tuples.Add(new Tuple<int, int>(2, 90));
			tuples.Add(new Tuple<int, int>(3, 45));
			tuples.Add(new Tuple<int, int>(1, 14));
			tuples.Add(new Tuple<int, int>(9, 7));
			var haffmanCompress = new HaffmanCompress(tuples);
			haffmanCompress.buildTree();
			NodeUtils.showAsTree(haffmanCompress.Tree);
			haffmanCompress.buildCodeTable();
			Console.WriteLine(string.Join(";", haffmanCompress.CodeTable.Select(x => x.Key + "=" + x.Value).ToArray()));*/
		}

		/*private void nextBlock(object sender, EventArgs e) {
            if (tick == blocks.Length) {
                tick = 0;
            }
            resultPictureBox.Image = blocks[tick].ToImage();
            tick++;
        }*/
	}
}

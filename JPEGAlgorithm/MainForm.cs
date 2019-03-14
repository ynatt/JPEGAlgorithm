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
            openFileDialog.Filter = "Image files (*.png)|*.png|(*.jpg)|*.jpg";
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

        private void CompressButton_Click(object sender, EventArgs e) {
			Timer timer = Timer.GetInstance();
			var MAIN_ID = "Main";
			var FULL_COMPRESS = "Compress of image";
			timer.Start(MAIN_ID, FULL_COMPRESS);
            var sourceImage = sourceImageBox.Image;
            var originalWidth = sourceImage.Width;
            var originalHeight = sourceImage.Height;
            var quantCoeff = Double.Parse(this.quantCoeff.Text);
            var dcqCoeff = Double.Parse(this.dcqCoeff.Text);
            var blockSize = Int32.Parse(dimentionTextBox.Text);
            var chunkSize = blockSize * 2;
            var fittedToChunksImage = ImageUtils.FitToBlockSize(new RGBImage(sourceImage), chunkSize);
			var chunks = fittedToChunksImage.ToBlockArray(chunkSize, out int widthBlocks, out int heightBlocks);
			var dcCoeffs = new List<int>();
            Chunk chunk;
            DCTChunk dCTChunk;
			List<DCTChunk> dCTChunks = new List<DCTChunk>(chunks.Length);
            var quantizeMatrix = MathUtils.BuildQuantizationMatrix(quantCoeff, dcqCoeff, 8); 
            for (int i = 0; i < chunks.Length; i++) {
                chunk = new Chunk(chunks[i]);
				dCTChunk = new DCTChunk(chunk);
				dCTChunk.Quantize(quantCoeff, quantizeMatrix);
				dCTChunks.Add(dCTChunk);
                dcCoeffs.AddRange(dCTChunk.getDCCoeffs());
			}
			//Console.WriteLine(String.Join(", ", dcCoeffs.ToArray()));
			var dcDiffs = MathUtils.MakeDiffOfDCCoeffs(dcCoeffs);
			var coeffsLengths = dcDiffs.ConvertAll(c => NumberUtils.GetBitsLength(c));
            //Console.WriteLine(String.Join(", ", MathUtils.buildBarChart(dcCoeffs.ToArray()).ToArray()));
            var barChart = MathUtils.BuildBarChart(coeffsLengths);
			var tuples = barChart.Select(entry => new Tuple<int, int>(entry.Key, entry.Value));
			var compressor = new HaffmanCompress(tuples);
			compressor.buildTree();
			compressor.buildCodeTable();
			chart.Series["DCDiffsBitLength"].Points.DataBindXY(barChart.Keys, barChart.Values);
			var listOfDecompressedImages = new List<YCbCrImage>();
			for (int i = 0; i < dCTChunks.Count; i++) {
				dCTChunks[i].Dequantize(quantCoeff, quantizeMatrix);
				listOfDecompressedImages.Add(YCbCrImage.FromChunk(new Chunk(dCTChunks[i])));
			}
			var matrix = new YCbCrImage[widthBlocks, heightBlocks];
			for (int i = 0; i < widthBlocks; i++) {
				for (int j = 0; j < heightBlocks; j++) {
					matrix[i, j] = listOfDecompressedImages[j * widthBlocks + i];
				}
			}
			var decompressedImage = YCbCrImage.FromMatrix(matrix).ToRGBImage(originalWidth, originalHeight);
			resultPictureBox.Image = decompressedImage.ToImage();
			timer.End(MAIN_ID, FULL_COMPRESS);
			timer.DisplayIntervals();
		}

		private void button1_Click(object sender, EventArgs e) {
			var array = new int[]{ 0, 1, 3, 7, 15, 31, 63, 127, 255, 511, 1023, 2047, -2047};
			for (int i = 0; i < array.Length; i++) {
				Console.WriteLine(array[i] + " " + NumberUtils.GetBitsLength(array[i]));
			}
		}

        private void chart_Click(object sender, EventArgs e)
        {

        }
    }
}

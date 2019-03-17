using System;
using System.Collections;
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
            openFileDialog.Filter = "Image files (*.jpg)|*.jpg|Image files (*.png)|*.png";
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
			var SUB_ID = "Sub";
			var DCT_OF_CHUNK = "Applying DCT to Chunk";
			var REVERSE_DCT = "Reversing from DCT";
			var HAFFMAN = "Haffman";
			var PREP = "Prepatation";
			var TO_RGB_IMAGE = "Transforming to RGBImage";
			var FITTING = "Fitting to Chunk size";
			var FROM_RGBIMAGE_TO_IMAGE = "Building an image from RGBImage";
			timer.Start(MAIN_ID, FULL_COMPRESS);
			timer.Start(SUB_ID, PREP);
            var sourceImage = sourceImageBox.Image;
            var originalWidth = sourceImage.Width;
            var originalHeight = sourceImage.Height;
            var quantCoeff = float.Parse(this.quantCoeff.Text);
            var dcqCoeff = float.Parse(this.dcqCoeff.Text);
            var blockSize = Int32.Parse(dimentionTextBox.Text);
            var chunkSize = blockSize * 2;
			timer.Start(SUB_ID, TO_RGB_IMAGE);
			var rgbImage = new RGBImage(sourceImage);
			timer.End(SUB_ID, TO_RGB_IMAGE);
			timer.Start(SUB_ID, FITTING);
			var fittedToChunksImage = ImageUtils.FitToBlockSize(rgbImage, chunkSize);
			timer.End(SUB_ID, FITTING);
			var chunks = fittedToChunksImage.ToBlockArray(chunkSize, out int widthBlocks, out int heightBlocks);
			var dcCoeffs = new List<int>();
			var dCTChunks = new DCTChunk[chunks.Length];
            var quantizeMatrix = MathUtils.BuildQuantizationMatrix(quantCoeff, dcqCoeff, 8);
			timer.End(SUB_ID, PREP);
			timer.Start(SUB_ID, DCT_OF_CHUNK);
			var flag = Parallel.ForEach(chunks, (elem, loopState, elementIndex)  => {
				var dCTChunk = new DCTChunk(new Chunk(chunks[elementIndex]));
				dCTChunk.Quantize(quantCoeff, quantizeMatrix);
				dCTChunks[elementIndex] = dCTChunk;
			});
			while (!flag.IsCompleted) ;
			/*for (int i = 0; i < chunks.Length; i++) {
                chunk = new Chunk(chunks[i]);
				dCTChunk = new DCTChunk(chunk);
				dCTChunk.Quantize(quantCoeff, quantizeMatrix);
				dCTChunks[i] = dCTChunk;
                //dcCoeffs.AddRange(dCTChunk.getDCCoeffs());
			}*/
			timer.End(SUB_ID, DCT_OF_CHUNK);
			//timer.Start(SUB_ID, HAFFMAN);
			//Console.WriteLine(String.Join(", ", dcCoeffs.ToArray()));
			//var dcDiffs = MathUtils.MakeDiffOfDCCoeffs(dcCoeffs);
			//var coeffsLengths = dcDiffs.ConvertAll(c => NumberUtils.GetBitsLength(c));
			//Console.WriteLine(String.Join(", ", MathUtils.buildBarChart(dcCoeffs.ToArray()).ToArray()));
			//var barChart = MathUtils.BuildBarChart(coeffsLengths);
			//var tuples = barChart.Select(entry => new Tuple<int, int>(entry.Key, entry.Value));
			//var compressor = new HaffmanCompress(tuples);
			//compressor.buildTree();
			//compressor.buildCodeTable();
			//chart.Series["DCDiffsBitLength"].Points.DataBindXY(barChart.Keys, barChart.Values);
			//timer.End(SUB_ID, HAFFMAN);
			var listOfDecompressedImages = new YCbCrImage[dCTChunks.Length];
			var listOfDecompressedChunks = new Chunk[dCTChunks.Length];
			timer.Start(SUB_ID, REVERSE_DCT);
			flag = Parallel.ForEach(dCTChunks, (elem, loopState, i) => {
				dCTChunks[i].Dequantize(quantCoeff, quantizeMatrix);
				listOfDecompressedChunks[i] = new Chunk(dCTChunks[i]);
				listOfDecompressedImages[i] = YCbCrImage.FromChunk(listOfDecompressedChunks[i]);
			});
			while (!flag.IsCompleted) ;
			timer.End(SUB_ID, REVERSE_DCT);
			var matrix = new YCbCrImage[widthBlocks, heightBlocks];
			flag = Parallel.ForEach(listOfDecompressedImages, (elem, loopState, k) => {
				var j = k / widthBlocks;
				matrix[k - j * widthBlocks, j] = elem;
			});
			while (!flag.IsCompleted) ;
			/*for (int i = 0; i < widthBlocks; i++) {
				for (int j = 0; j < heightBlocks; j++) {
					matrix[i, j] = listOfDecompressedImages[j * widthBlocks + i];
				}
			}*/
			var decompressedImage = YCbCrImage.FromMatrix(matrix).ToRGBImage(originalWidth, originalHeight);
			timer.Start(SUB_ID, FROM_RGBIMAGE_TO_IMAGE);
			resultPictureBox.Image = decompressedImage.ToImage();
			timer.End(SUB_ID, FROM_RGBIMAGE_TO_IMAGE);
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

		private void button1_Click_1(object sender, EventArgs e) {
			var vector = new float[] { 0.3536f, 0.3536f, 0.6464f, 1.0607f, 0.3536f, -1.0607f, -1.3536f, -0.3536f};
			var timer = Timer.GetInstance();

			var matrix = new int[,] {  {160, 110, 110, 160, 140, 255, 255, 255},
															{120, 120, 140, 190, 255, 255, 255, 255},
															{140, 130, 160, 240, 255, 255, 255, 255},
															{140, 170, 220, 255, 255, 255, 255, 255},
															{180, 220, 255, 255, 255, 255, 255, 255},
															{240, 255, 255, 255, 255, 255, 255, 255},
															{255, 255, 255, 255, 255, 255, 255, 255},
															{255, 255, 255, 255, 255, 255, 255, 255}};
			var block = new ImageBlock(matrix);
			timer.Start("Main", "d2 dct 1");
			MatrixUtils<float>.Show(TransformsUtils.DCT(block));
			timer.End("Main", "d2 dct 1");
			timer.Start("Main", "d2 dct 2");
			MatrixUtils<int>.Show(TransformsUtils.Reverse_DCT(TransformsUtils.DCT(block)));
			timer.End("Main", "d2 dct 2");
			timer.Start("Main", "d1 dct 1");
			TransformsUtils.DCT_1(block);
			timer.End("Main", "d1 dct 1");
			timer.Start("Main", "d1 dct 2");
			TransformsUtils.DCT_1(block);
			timer.End("Main", "d1 dct 2");
			timer.DisplayIntervals();
			Console.WriteLine(String.Join(", ", TransformsUtils.Reverse_DCT_d1_NotOptimized(TransformsUtils.DCT_d1_NotOptimized(vector))));
		}
	}
}

using JPEGAlgorithm.vilenklin;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
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
			resolutionValue.Text = sourceImageBox.Image.Width + " x " + sourceImageBox.Image.Height;
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

		private async void CompressButton_Click(object sender, EventArgs e) {
			if (compressInProcess) {
				return;
			}
			compressInProcess = true;
			compressTimeValue.Text = "Calculating...";
			resultPictureBox.Image = await Task.Factory.StartNew<Image>(() => BeginCompress(),
				TaskCreationOptions.LongRunning);
			compressTimeValue.Text = Timer.GetInstance().GetTimeOf(JPEG.MAIN_ID, JPEG.FULL_COMPRESS);
			CountImageError(sourceImageBox.Image, resultPictureBox.Image, mseValueLabel, psnrValueLabel);
			compressInProcess = false;
		}

		[MethodImpl(MethodImplOptions.Synchronized)]
		private void CompareImages(int x, int y, int width, int height) {
			compareBoxBefore.Image = ImageUtils.SubImage(sourceImageBox.Image, x, y, width, height);
			compareBoxAfter.Image = ImageUtils.SubImage(resultPictureBox.Image, x, y, width, height);
		}

		private bool compressInProcess = false;

		private Image BeginCompress() {
			var quantCoeffCbCrAC = float.Parse(this.quantCoeffCbCrAC.Text);
			var quantCoeffCbCrDC = float.Parse(this.quantCoeffCbCrDC.Text);
			var quantCoeffYDC = float.Parse(this.quantCoeffYDC.Text);
			var quantCoeffYAC = float.Parse(this.quantCoeffYAC.Text);
			JPEG jpeg = new JPEG(sourceImageBox.Image);
			return jpeg.Compress(quantCoeffCbCrDC, quantCoeffCbCrAC, enableQuantOfYCheckBox.Checked, quantCoeffYDC, quantCoeffYAC);
		}

		private void compareButton_Click(object sender, EventArgs e) {
			CompareImages(int.Parse(compareXValueBox.Text), int.Parse(compareYValueBox.Text),
				int.Parse(compareWidthBox.Text), int.Parse(compareHeightBox.Text));
			CountImageError(compareBoxBefore.Image, compareBoxAfter.Image, compareMSEValue, comparePSNRValue);
		}

		private void MainForm_Load(object sender, EventArgs e) {
			resolutionValue.Text = sourceImageBox.Image.Width + " x " + sourceImageBox.Image.Height;

		}

		private void CountImageError(Image a, Image b, Label mseValueLabel, Label psnrValueLabel) {
			var mse = MathUtils.CountMSE((Bitmap)a, (Bitmap)b);
			mseValueLabel.Text = "" + mse;
			psnrValueLabel.Text = "" + MathUtils.CountPSNR(255, mse) + " dB";
		}

		private async void button1_Click(object sender, EventArgs e) {
			if (compressInProcess) {
				return;
			}
			compressInProcess = true;
			compressTimeValue.Text = "Calculating...";
			resultPictureBox.Image = await Task.Factory.StartNew<Image>(() => BeginVilenkinCompress(),
				TaskCreationOptions.LongRunning);
			compressTimeValue.Text = "Calculated";
			CountImageError(sourceImageBox.Image, resultPictureBox.Image, mseValueLabel, psnrValueLabel);
			compressInProcess = false;
		}

		private List<int> p = new List<int>() { 4, 2, 4, 4, 4, 3, 3 };

		private VilenkinTransform vilenkin;

		private static string MAIN = "Main";
		private static string PREPARATION = "Preparation";
		private static string TRANSFORM = "Vilenkin transformation";
		private static string REVERSE_TRANSFORM = "Vilenkin reverse transformation";

		private Image BeginVilenkinCompress() {
			Timer timer = Timer.GetInstance();
			timer.Clear();
			if (vilenkin == null) {
				timer.Start(MAIN, PREPARATION);
				vilenkin = new VilenkinTransform(p, JPEG.BLOCK_SIZE);
				timer.End(MAIN, PREPARATION);
			}
			var originalWidth = sourceImageBox.Image.Width;
			var originalHeight = sourceImageBox.Image.Height;
			var rgbImage = new RGBImage(sourceImageBox.Image);
			var fittedToChunksImage = ImageUtils.FitToBlockSize(rgbImage, JPEG.CHUNK_SIZE);
			var chunks = fittedToChunksImage.ToBlockArray(JPEG.CHUNK_SIZE, out int widthBlocks, out int heightBlocks);
			var vilenkinChunks = new VilenkinChunk[chunks.Length];
			var parallelOptions = new ParallelOptions() { MaxDegreeOfParallelism = 4 };
			timer.Start(MAIN, TRANSFORM);
			var flag = Parallel.ForEach(chunks, parallelOptions, (elem, loopState, elementIndex) => {
				var vilenkinChunk = new VilenkinChunk(vilenkin, new Chunk(chunks[elementIndex]));
				vilenkinChunk.ZeroCoeffs(zeroPercent);
				vilenkinChunks[elementIndex] = vilenkinChunk;
			});
			while (!flag.IsCompleted) ;
			timer.End(MAIN, TRANSFORM);
			var listOfDecompressedImages = new YCbCrImage[vilenkinChunks.Length];
			var listOfDecompressedChunks = new Chunk[vilenkinChunks.Length];
			var matrix = new YCbCrImage[widthBlocks, heightBlocks];
			timer.Start(MAIN, REVERSE_TRANSFORM);
			flag = Parallel.ForEach(vilenkinChunks, parallelOptions, (elem, loopState, i) => {
				listOfDecompressedChunks[i] = new Chunk(vilenkinChunks[i], vilenkin);
				listOfDecompressedImages[i] = YCbCrImage.FromChunk(listOfDecompressedChunks[i]);
				var j = i / widthBlocks;
				matrix[i - j * widthBlocks, j] = listOfDecompressedImages[i];
			});
			while (!flag.IsCompleted) ;
			timer.End(MAIN, REVERSE_TRANSFORM);
			timer.DisplayIntervals();
			var decompressedImage = YCbCrImage.FromMatrix(matrix).ToRGBImage(originalWidth, originalHeight);
			return decompressedImage.ToImage();
		}

		private int zeroPercent;

		private void zeroPercentBox_TextChanged(object sender, EventArgs e) {
			if (int.TryParse(zeroPercentBox.Text, out zeroPercent)) {
				if (zeroPercent < 0 && zeroPercent > 100) {
					zeroPercentBox.Text = "0-100";
				}
			} else {
				zeroPercentBox.Text = "";
			}
		}

		private Image BeginHaarCompress() {
			Timer timer = Timer.GetInstance();
			timer.Clear();
			var originalWidth = sourceImageBox.Image.Width;
			var originalHeight = sourceImageBox.Image.Height;
			var rgbImage = new RGBImage(sourceImageBox.Image);
			var fittedToChunksImage = ImageUtils.FitToBlockSize(rgbImage, JPEG.CHUNK_SIZE);
			var chunks = fittedToChunksImage.ToBlockArray(JPEG.CHUNK_SIZE, out int widthBlocks, out int heightBlocks);
			var haarChunks = new HaarChunk[chunks.Length];
			var parallelOptions = new ParallelOptions() { MaxDegreeOfParallelism = 4 };
			timer.Start(MAIN, TRANSFORM);
			var flag = Parallel.ForEach(chunks, parallelOptions, (elem, loopState, elementIndex) => {
				var haarChunk = new HaarChunk(new Chunk(chunks[elementIndex]));
				haarChunk.ZeroCoeffs(zeroPercent);
				haarChunks[elementIndex] = haarChunk;
			});
			while (!flag.IsCompleted) ;
			timer.End(MAIN, TRANSFORM);
			var listOfDecompressedImages = new YCbCrImage[haarChunks.Length];
			var listOfDecompressedChunks = new Chunk[haarChunks.Length];
			var matrix = new YCbCrImage[widthBlocks, heightBlocks];
			timer.Start(MAIN, REVERSE_TRANSFORM);
			flag = Parallel.ForEach(haarChunks, parallelOptions, (elem, loopState, i) => {
				listOfDecompressedChunks[i] = new Chunk(haarChunks[i]);
				listOfDecompressedImages[i] = YCbCrImage.FromChunk(listOfDecompressedChunks[i]);
				var j = i / widthBlocks;
				matrix[i - j * widthBlocks, j] = listOfDecompressedImages[i];
			});
			while (!flag.IsCompleted) ;
			timer.End(MAIN, REVERSE_TRANSFORM);
			timer.DisplayIntervals();
			var decompressedImage = YCbCrImage.FromMatrix(matrix).ToRGBImage(originalWidth, originalHeight);
			return decompressedImage.ToImage();
		}

		private async void button2_Click(object sender, EventArgs e) {
			if (compressInProcess) {
				return;
			}
			compressInProcess = true;
			compressTimeValue.Text = "Calculating...";
			resultPictureBox.Image = await Task.Factory.StartNew<Image>(() => BeginHaarCompress(),
				TaskCreationOptions.LongRunning);
			compressTimeValue.Text = "Calculated";
			CountImageError(sourceImageBox.Image, resultPictureBox.Image, mseValueLabel, psnrValueLabel);
			compressInProcess = false;
		}
	}
}

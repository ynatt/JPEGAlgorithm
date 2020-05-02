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
			label_zeroedCoeffsPercent.Text = "" + Math.Round((double)zeroCoeffsCount * 100d / (double)coeffsCount, 0);
			blockSize_label.Text = "" + blockSize;
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
			blockSize = (int)Math.Pow(int.Parse(p_textBox.Text), int.Parse(N_textBox.Text) + 1);
			Image result = jpeg.Compress(quantCoeffCbCrDC, quantCoeffCbCrAC, enableQuantOfYCheckBox.Checked,
				quantCoeffYDC, quantCoeffYAC, blockSize, out coeffsCount, out zeroCoeffsCount, zeroPercentY, zeroPercentCb, zeroPercentCr);
			return result;
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
			MathUtils.CountMSE((Bitmap)a, (Bitmap)b, out double rMSE, out double gMSE, out double bMSE);
			mseValueLabel.Text = "(" + rMSE + ", " + gMSE + ", " + bMSE + ")";
			psnrValueLabel.Text = "(" + MathUtils.CountPSNR(255, rMSE) + ", " + MathUtils.CountPSNR(255, gMSE) + ", " + MathUtils.CountPSNR(255, bMSE) + ")";
		}

		private async void button1_Click(object sender, EventArgs e) {
			if (compressInProcess) {
				return;
			}
			compressInProcess = true;
			coeffsCount = 0;
			zeroCoeffsCount = 0;
			compressTimeValue.Text = "Calculating...";
			resultPictureBox.Image = await Task.Factory.StartNew<Image>(() => BeginVilenkinCompress(),
				TaskCreationOptions.LongRunning);
			compressTimeValue.Text = "Calculated";
			label_zeroedCoeffsPercent.Text = "" + Math.Round((double)zeroCoeffsCount * 100d / (double)coeffsCount, 0);
			blockSize_label.Text = "" + blockSize;
			CountImageError(sourceImageBox.Image, resultPictureBox.Image, mseValueLabel, psnrValueLabel);
			compressInProcess = false;
		}

		private List<int> p;

		private VilenkinTransform vilenkin;

		private static string MAIN = "Main";
		private static string PREPARATION = "Preparation";
		private static string TRANSFORM = "Vilenkin transformation";
		private static string REVERSE_TRANSFORM = "Vilenkin reverse transformation";

		public int blockSize;
		public int chunkSize;

		private Image BeginVilenkinCompress() {
			p = new List<int>();
			ArrayUtils.Fill(p, int.Parse(p_textBox.Text), 10);
			Timer timer = Timer.GetInstance();
			timer.Clear();
			if (vilenkin == null) {
				timer.Start(MAIN, PREPARATION);
				vilenkin = new VilenkinTransform(p, int.Parse(N_textBox.Text));
				timer.End(MAIN, PREPARATION);
			}
			blockSize = vilenkin.GetBlockSize();
			chunkSize = blockSize * 2;
			var originalWidth = sourceImageBox.Image.Width;
			var originalHeight = sourceImageBox.Image.Height;
			var rgbImage = new RGBImage(sourceImageBox.Image);
			var fittedToChunksImage = ImageUtils.FitToBlockSize(rgbImage, chunkSize);
			var chunks = fittedToChunksImage.ToBlockArray(chunkSize, out int widthBlocks, out int heightBlocks);
			var vilenkinChunks = new VilenkinChunk[chunks.Length];
			var parallelOptions = new ParallelOptions() { MaxDegreeOfParallelism = 4 };
			timer.Start(MAIN, TRANSFORM);
			var flag = Parallel.ForEach(chunks, parallelOptions, (elem, loopState, elementIndex) => {
				var vilenkinChunk = new VilenkinChunk(vilenkin, new Chunk(chunks[elementIndex], blockSize));
				vilenkinChunk.ZeroCoeffs(zeroPercentY, zeroPercentCb, zeroPercentCr);
				vilenkinChunks[elementIndex] = vilenkinChunk;
			});
			while (!flag.IsCompleted) ;
			timer.End(MAIN, TRANSFORM);
			foreach (VilenkinChunk chunk in vilenkinChunks) {
				coeffsCount += chunk.CoeffsCount();
				zeroCoeffsCount += chunk.ZeroCoeffsCount();
			}
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

		private int zeroPercentY;
		private int zeroPercentCb;
		private int zeroPercentCr;

		int coeffsCount = 0;
		int zeroCoeffsCount = 0;

		private Image BeginHaarCompress() {
			Timer timer = Timer.GetInstance();
			timer.Clear();
			blockSize = (int) Math.Pow(int.Parse(p_textBox.Text), int.Parse(N_textBox.Text) + 1);
			var chunkSize = blockSize * 2;
			var originalWidth = sourceImageBox.Image.Width;
			var originalHeight = sourceImageBox.Image.Height;
			var rgbImage = new RGBImage(sourceImageBox.Image);
			var fittedToChunksImage = ImageUtils.FitToBlockSize(rgbImage, chunkSize);
			var chunks = fittedToChunksImage.ToBlockArray(chunkSize, out int widthBlocks, out int heightBlocks);
			var haarChunks = new HaarChunk[chunks.Length];
			var parallelOptions = new ParallelOptions() { MaxDegreeOfParallelism = 4 };
			timer.Start(MAIN, TRANSFORM);
			var flag = Parallel.ForEach(chunks, parallelOptions, (elem, loopState, elementIndex) => {
				var haarChunk = new HaarChunk(new Chunk(chunks[elementIndex], blockSize));
				haarChunk.ZeroCoeffs(zeroPercentY, zeroPercentCb, zeroPercentCr);
				haarChunks[elementIndex] = haarChunk;
			});
			while (!flag.IsCompleted) ;
			timer.End(MAIN, TRANSFORM);
			foreach (HaarChunk chunk in haarChunks) {
				coeffsCount += chunk.CoeffsCount();
				zeroCoeffsCount += chunk.ZeroCoeffsCount();
			}
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
			coeffsCount = 0;
			zeroCoeffsCount = 0;
			compressInProcess = true;
			compressTimeValue.Text = "Calculating...";
			resultPictureBox.Image = await Task.Factory.StartNew<Image>(() => BeginHaarCompress(),
				TaskCreationOptions.LongRunning);
			compressTimeValue.Text = "Calculated";
			label_zeroedCoeffsPercent.Text = "" + Math.Round((double)zeroCoeffsCount * 100d / (double)coeffsCount, 0);
			blockSize_label.Text = "" + blockSize;
			CountImageError(sourceImageBox.Image, resultPictureBox.Image, mseValueLabel, psnrValueLabel);
			compressInProcess = false;
		}

		private void p_textBox_TextChanged(object sender, EventArgs e) {
			p = new List<int>();
			ArrayUtils.Fill(p, int.Parse(p_textBox.Text), 10);
			vilenkin = null;
		}

		private void N_textBox_TextChanged(object sender, EventArgs e) {
			vilenkin = null;
		}

		private void zeroPercentYBox_TextChanged(object sender, EventArgs e) {
			if (int.TryParse(zeroPercentYBox.Text, out zeroPercentY)) {
				if (zeroPercentY < 0 && zeroPercentY > 100) {
					zeroPercentYBox.Text = "0";
				}
			} else {
				zeroPercentYBox.Text = "";
			}
		}

		private void zeroPercentCb_textBox_TextChanged(object sender, EventArgs e) {
			if (int.TryParse(zeroPercentCb_textBox.Text, out zeroPercentCb)) {
				if (zeroPercentCb < 0 && zeroPercentCb > 100) {
					zeroPercentCb_textBox.Text = "0";
				}
			} else {
				zeroPercentCb_textBox.Text = "";
			}
		}

		private void zeroPercentCr_textBox_TextChanged(object sender, EventArgs e) {
			if (int.TryParse(zeroPercentCr_textBox.Text, out zeroPercentCr)) {
				if (zeroPercentCr < 0 && zeroPercentCr > 100) {
					zeroPercentCr_textBox.Text = "0";
				}
			} else {
				zeroPercentCr_textBox.Text = "";
			}
		}
	}
}

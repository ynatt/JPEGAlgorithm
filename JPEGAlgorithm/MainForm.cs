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
	}
}

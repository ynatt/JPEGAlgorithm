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
			CompareImages(200, 400, 50, 50);
			compressInProcess = false;
		}

		private void CompareImages(int x, int y, int width, int height) {
			compareBoxBefore.Image = ImageUtils.SubImage(sourceImageBox.Image, x, y, width, height);
			compareBoxAfter.Image = ImageUtils.SubImage(resultPictureBox.Image, x, y, width, height);
		}

		private bool compressInProcess = false;

		private Image BeginCompress() {
			var quantCoeffAC = float.Parse(this.quantCoeffAC.Text);
			var quantCoeffDC = float.Parse(this.quantCoeffDC.Text);
			JPEG jpeg = new JPEG(sourceImageBox.Image);
			return jpeg.Compress(quantCoeffDC, quantCoeffAC);
		}

		private void compareButton_Click(object sender, EventArgs e) {
			CompareImages(int.Parse(compareXValueBox.Text), int.Parse(compareYValueBox.Text),
				int.Parse(compareWidthBox.Text), int.Parse(compareHeightBox.Text));
		}
	}
}

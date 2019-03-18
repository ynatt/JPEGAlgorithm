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
            var quantCoeffAC = float.Parse(this.quantCoeffAC.Text);
            var quantCoeffDC = float.Parse(this.quantCoeffDC.Text);
			var blockSize = 8;
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
            var quantizeMatrix = MathUtils.BuildQuantizationMatrix(quantCoeffAC, quantCoeffDC, 8);
			timer.End(SUB_ID, PREP);
			timer.Start(SUB_ID, DCT_OF_CHUNK);
			TransformsUtils.CountCosMatrixFor(8);
			var parallelOptions = new ParallelOptions() { MaxDegreeOfParallelism = 8 };
			var flag = Parallel.ForEach(chunks, parallelOptions, (elem, loopState, elementIndex)  => {
				var dCTChunk = new DCTChunk(new Chunk(chunks[elementIndex]));
				dCTChunk.Quantize(quantCoeffAC, quantizeMatrix);
				dCTChunks[elementIndex] = dCTChunk;
			});
			while (!flag.IsCompleted) ;
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
			var matrix = new YCbCrImage[widthBlocks, heightBlocks];
			flag = Parallel.ForEach(dCTChunks, parallelOptions, (elem, loopState, i) => {
				dCTChunks[i].Dequantize(quantCoeffAC, quantizeMatrix);
				listOfDecompressedChunks[i] = new Chunk(dCTChunks[i]);
				listOfDecompressedImages[i] = YCbCrImage.FromChunk(listOfDecompressedChunks[i]);
				var j = i / widthBlocks;
				matrix[i - j * widthBlocks, j] = listOfDecompressedImages[i];
			});
			while (!flag.IsCompleted) ;
			timer.End(SUB_ID, REVERSE_DCT);
			var decompressedImage = YCbCrImage.FromMatrix(matrix).ToRGBImage(originalWidth, originalHeight);
			resultPictureBox.Image = decompressedImage.ToImage();
			timer.End(MAIN_ID, FULL_COMPRESS);
			timer.DisplayIntervals();
		}
	}
}

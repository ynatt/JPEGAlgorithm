using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JPEGAlgorithm
{
    class JPEG
    {
        private Image sourceImage;

		public const int BLOCK_SIZE = 8;
		public const int CHUNK_SIZE = 16;
		public const string MAIN_ID = "Main";
		public const string FULL_COMPRESS = "Compress of image";
		public const string SUB_ID = "Sub";
		public const string DCT_OF_CHUNK = "Applying DCT to Chunk";
		public const string REVERSE_DCT = "Reversing from DCT";
		public const string PREP = "Prepatation";
		public const string TO_RGB_IMAGE = "Transforming to RGBImage";
		public const string FITTING = "Fitting to Chunk size";

		public JPEG(Image sourceImage)
        {
            this.sourceImage = sourceImage;
        }

        public Image Compress(float quantCoeffCbCrDC, float quantCoeffCbCrAC, bool yQuantEnabled, float quantCoeffYDC, float quantCoeffYAC) {
			Timer timer = Timer.GetInstance();
			timer.Clear();
			timer.Start(MAIN_ID, FULL_COMPRESS);
			timer.Start(SUB_ID, PREP);
			var originalWidth = sourceImage.Width;
			var originalHeight = sourceImage.Height;
			timer.Start(SUB_ID, TO_RGB_IMAGE);
			var rgbImage = new RGBImage(sourceImage);
			timer.End(SUB_ID, TO_RGB_IMAGE);
			timer.Start(SUB_ID, FITTING);
			var fittedToChunksImage = ImageUtils.FitToBlockSize(rgbImage,CHUNK_SIZE);
			timer.End(SUB_ID, FITTING);
			var chunks = fittedToChunksImage.ToBlockArray(CHUNK_SIZE, out int widthBlocks, out int heightBlocks);
			var dcCoeffs = new List<int>();
			var dCTChunks = new DCTChunk[chunks.Length];
			var quantizeCbCrMatrix = MathUtils.BuildQuantizationMatrix(quantCoeffCbCrAC, quantCoeffCbCrDC, BLOCK_SIZE);
			float[,] quantizeYMatrix = new float[BLOCK_SIZE, BLOCK_SIZE];
			if (yQuantEnabled) {
				quantizeYMatrix = MathUtils.BuildQuantizationMatrix(quantCoeffYAC, quantCoeffYDC, BLOCK_SIZE);
			}
			timer.End(SUB_ID, PREP);
			timer.Start(SUB_ID, DCT_OF_CHUNK);
			TransformsUtils.CountCosMatrixFor(8);
			var parallelOptions = new ParallelOptions() { MaxDegreeOfParallelism = 8 };
			var flag = Parallel.ForEach(chunks, parallelOptions, (elem, loopState, elementIndex) => {
				var dCTChunk = new DCTChunk(new Chunk(chunks[elementIndex]));
				dCTChunk.QuantizeCbCr(quantizeCbCrMatrix);
				if (yQuantEnabled) {
					dCTChunk.QuantizeY(quantizeYMatrix);
				}
				dCTChunks[elementIndex] = dCTChunk;
			});
			while (!flag.IsCompleted) ;
			timer.End(SUB_ID, DCT_OF_CHUNK);
			var listOfDecompressedImages = new YCbCrImage[dCTChunks.Length];
			var listOfDecompressedChunks = new Chunk[dCTChunks.Length];
			timer.Start(SUB_ID, REVERSE_DCT);
			var matrix = new YCbCrImage[widthBlocks, heightBlocks];
			flag = Parallel.ForEach(dCTChunks, parallelOptions, (elem, loopState, i) => {
				dCTChunks[i].DequantizeCbCr(quantizeCbCrMatrix);
				if (yQuantEnabled) {
					dCTChunks[i].DequantizeY(quantizeYMatrix);
				}
				listOfDecompressedChunks[i] = new Chunk(dCTChunks[i]);
				listOfDecompressedImages[i] = YCbCrImage.FromChunk(listOfDecompressedChunks[i]);
				var j = i / widthBlocks;
				matrix[i - j * widthBlocks, j] = listOfDecompressedImages[i];
			});
			while (!flag.IsCompleted) ;
			timer.End(SUB_ID, REVERSE_DCT);
			var decompressedImage = YCbCrImage.FromMatrix(matrix).ToRGBImage(originalWidth, originalHeight);
			timer.End(MAIN_ID, FULL_COMPRESS);
			timer.DisplayIntervals();
			return decompressedImage.ToImage();
		}
    }
}

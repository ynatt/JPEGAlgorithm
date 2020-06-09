using JPEGAlgorithm.vilenklin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPEGAlgorithm {
    class Chunk {
        private ImageBlock[] y = new ImageBlock[4];

        private ImageBlock[] cb = new ImageBlock[4];

        private ImageBlock[] cr = new ImageBlock[4];

        public int BLOCK_SIZE;
        public int CHUNK_SIZE;
        public const int BLOCKS_COUNT = 4;

        internal ImageBlock[] Y { get => y; }
        internal ImageBlock[] Cb { get => cb; }
        internal ImageBlock[] Cr { get => cr; }

        public Chunk(RGBImage imageChunk, int blockSize) {
			BLOCK_SIZE = blockSize;
			CHUNK_SIZE = BLOCK_SIZE * 2;
            if (imageChunk.Width != CHUNK_SIZE && imageChunk.Height != CHUNK_SIZE) 
                throw new ArgumentException(String.Format("RGBImage have to be %sx%s", CHUNK_SIZE, CHUNK_SIZE));
            //Чанк 16х16 разбивается на матрицу 4х4 с блоками 8х8 и матрица зиг-загом переводится в массив.
            var blocks = MatrixUtils<RGBImage>.ToArrayByZigZag(imageChunk.ToBlockMatrix(BLOCK_SIZE));
            var yCbCrBlocks = new YCbCrImage[4];
            //RGB блоки трансформируются в YCbCr блоки
            for (var i = 0; i < BLOCKS_COUNT; i++) {
				if (MainForm.isYCbCrEnabled) {
					yCbCrBlocks[i] = blocks[i].ToYCbCrImage();
				} else {
					yCbCrBlocks[i] = blocks[i].ToYCbCrimageWithoutTransform();
				}
            }
            var cbBlocks = new ImageBlock[BLOCKS_COUNT];
            var crBlocks = new ImageBlock[BLOCKS_COUNT];
            //Выделение компонент, смещаем на 128 Y компоненту
            for (var i = 0; i < BLOCKS_COUNT; i++) {
                Y[i] = yCbCrBlocks[i].GetImageBlock(YCbCrImage.Channel.Y);
                ImageUtils.Shift(Y[i]);
                cbBlocks[i] = yCbCrBlocks[i].GetImageBlock(YCbCrImage.Channel.Cb);
                crBlocks[i] = yCbCrBlocks[i].GetImageBlock(YCbCrImage.Channel.Cr);
            }
			if (MainForm.isAveragingEnabled) {
				//Cb и Cr блоки из массива переводим в матрицу блоков
				var cbMatrix = MatrixUtils<ImageBlock>.ToMatrixByZigZag(cbBlocks, 2);
				var crMatrix = MatrixUtils<ImageBlock>.ToMatrixByZigZag(crBlocks, 2);
				//Усредняем Cb и Cr из 4-ех блоков 8х8 в 1 блок 8х8 и смещаем на 128
				Averager averager = new Averager();
				cb[0] = averager.Average(cbMatrix);
				ImageUtils.Shift(Cb[0]);
				cr[0] = averager.Average(crMatrix);
				ImageUtils.Shift(Cr[0]);
			} else {
				cb = cbBlocks;
				for (int i = 0; i < BLOCKS_COUNT; i++) {
					ImageUtils.Shift(Cb[i]);
				}
				cr = crBlocks;
				for (int i = 0; i < BLOCKS_COUNT; i++) {
					ImageUtils.Shift(Cr[i]);
				}
			}
        }


		public Chunk(DCTChunk dCTChunk) {
			for (int i = 0; i < BLOCKS_COUNT; i++) {
				y[i] = new ImageBlock(TransformsUtils.Reverse_DCT_1(dCTChunk.Y[i]));
			}
			if (MainForm.isAveragingEnabled) {
				cb[0] = new ImageBlock(TransformsUtils.Reverse_DCT_1(dCTChunk.Cb[0]));
				cr[0] = new ImageBlock(TransformsUtils.Reverse_DCT_1(dCTChunk.Cr[0]));
			} else {
				for (int i = 0; i < BLOCKS_COUNT; i++) {
					cb[i] = new ImageBlock(TransformsUtils.Reverse_DCT_1(dCTChunk.Cb[i]));
					cr[i] = new ImageBlock(TransformsUtils.Reverse_DCT_1(dCTChunk.Cr[i]));
				}
			}
		}

		public Chunk(VilenkinChunk vilenkinChunk, VilenkinTransform vilenkin) {
			for (int i = 0; i < BLOCKS_COUNT; i++) {
				y[i] = new ImageBlock(vilenkin.ReverseTransform(vilenkinChunk.Y[i]));
			}
			if (MainForm.isAveragingEnabled) {
				cb[0] = new ImageBlock(vilenkin.ReverseTransform(vilenkinChunk.Cb[0]));
				cr[0] = new ImageBlock(vilenkin.ReverseTransform(vilenkinChunk.Cr[0]));
			} else {
				for (int i = 0; i < BLOCKS_COUNT; i++) {
					cb[i] = new ImageBlock(vilenkin.ReverseTransform(vilenkinChunk.Cb[i]));
					cr[i] = new ImageBlock(vilenkin.ReverseTransform(vilenkinChunk.Cr[i]));
				}
			}
		}

		public Chunk(HaarChunk haarChunk, int p) {
			for (int i = 0; i < BLOCKS_COUNT; i++) {
				y[i] = new ImageBlock(TransformsUtils.HaarReverseTransform(haarChunk.Y[i], p));
			}
			if (MainForm.isAveragingEnabled) {
				cb[0] = new ImageBlock(TransformsUtils.HaarReverseTransform(haarChunk.Cb[0], p));
				cr[0] = new ImageBlock(TransformsUtils.HaarReverseTransform(haarChunk.Cr[0], p));
			} else {
				for (int i = 0; i < BLOCKS_COUNT; i++) {
					cb[i] = new ImageBlock(TransformsUtils.HaarReverseTransform(haarChunk.Cb[i], p));
					cr[i] = new ImageBlock(TransformsUtils.HaarReverseTransform(haarChunk.Cr[i], p));
				}
			}
		}

		public void Unshift() {
			ImageUtils.Unshift(y[0]);
			ImageUtils.Unshift(y[1]);
			ImageUtils.Unshift(y[2]);
			ImageUtils.Unshift(y[3]);
			if (MainForm.isAveragingEnabled) {
				ImageUtils.Unshift(cb[0]);
				ImageUtils.Unshift(cr[0]);
			} else {
				for (int i = 0; i < BLOCKS_COUNT; i++) {
					ImageUtils.Unshift(cb[i]);
					ImageUtils.Unshift(cr[i]);
				}
			}
		}

        public override string ToString() {
            StringBuilder sb = new StringBuilder();
        	for (var i = 0; i < BLOCK_SIZE; i++) {
          		for (var j = 0; j < BLOCK_SIZE; j++) {
            		for (var k = 0; k < BLOCKS_COUNT + 2; k++) {
              			if (k < 4) {
                			sb.Append(y[k].Data[i, j]).Append(" ");
                			continue;
              			}
              			if (k == 4) {
                			sb.Append(cb[0].Data[i, j]).Append(" ");
              			}
              			if (k == 5) {
                			sb.Append(cb[0].Data[i, j]).Append(" ");
              			}
            		}
            	sb.Append("    ");
          		}
          	sb.AppendLine();
			}
            return sb.ToString();
        }
    }
}

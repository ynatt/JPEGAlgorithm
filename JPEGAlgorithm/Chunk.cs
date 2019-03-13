using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPEGAlgorithm {
    class Chunk {
        private ImageBlock[] y = new ImageBlock[4];

        private ImageBlock cb;

        private ImageBlock cr;

        public const int BLOCK_SIZE = 8;
        public const int CHUNK_SIZE = 16;
        public const int BLOCKS_COUNT = 4;

        internal ImageBlock[] Y { get => y; }
        internal ImageBlock Cb { get => cb; }
        internal ImageBlock Cr { get => cr; }

        public Chunk(RGBImage imageChunk) {
            if (imageChunk.Width != CHUNK_SIZE && imageChunk.Height != CHUNK_SIZE) 
                throw new ArgumentException(String.Format("RGBImage have to be %sx%s", CHUNK_SIZE, CHUNK_SIZE));
            //Чанк 16х16 разбивается на матрицу 4х4 с блоками 8х8 и матрица зиг-загом переводится в массив.
            var blocks = MatrixUtils<RGBImage>.ToArrayByZigZag(imageChunk.ToBlockMatrix(BLOCK_SIZE));
            var yCbCrBlocks = new YCbCrImage[4];
            //RGB блоки трансформируются в YCbCr блоки
            for (var i = 0; i < BLOCKS_COUNT; i++) {
                yCbCrBlocks[i] = blocks[i].ToYCbCrImage();
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
            //Cb и Cr блоки из массива переводим в матрицу блоков
            var cbMatrix = MatrixUtils<ImageBlock>.ToMatrixByZigZag(cbBlocks, 2);
            var crMatrix = MatrixUtils<ImageBlock>.ToMatrixByZigZag(crBlocks, 2);
            //Усредняем Cb и Cr из 4-ех блоков 8х8 в 1 блок 8х8 и смещаем на 128
            Averager averager = new Averager();
            cb = averager.Average(cbMatrix);
            ImageUtils.Shift(Cb);
            cr = averager.Average(crMatrix);
            ImageUtils.Shift(Cr);
        }


		public Chunk(DCTChunk dCTChunk) {
			for (int i = 0; i < BLOCKS_COUNT; i++) {
				y[i] = new ImageBlock(TransformsUtils.Reverse_DCT(dCTChunk.Y[i]));
			}
			cb = new ImageBlock(TransformsUtils.Reverse_DCT(dCTChunk.Cb));
			cr = new ImageBlock(TransformsUtils.Reverse_DCT(dCTChunk.Cr));
		}

		public void Unshift() {
			ImageUtils.Unshift(y[0]);
			ImageUtils.Unshift(y[1]);
			ImageUtils.Unshift(y[2]);
			ImageUtils.Unshift(y[3]);
			ImageUtils.Unshift(cb);
			ImageUtils.Unshift(cr);
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
                			sb.Append(cb.Data[i, j]).Append(" ");
              			}
              			if (k == 5) {
                			sb.Append(cb.Data[i, j]).Append(" ");
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

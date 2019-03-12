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

        public override string ToString() {
            StringBuilder sb = new StringBuilder();
            sb.Append("Y:");
            for (var i = 0; i < BLOCKS_COUNT; i++) {
                sb.Append("Y").Append(i).AppendLine().Append(y[i]);
            }
            sb.AppendLine().Append("Cb").AppendLine().Append(cb).AppendLine().Append("Cr").AppendLine().Append(cr);
            return sb.ToString();
        }
    }
}

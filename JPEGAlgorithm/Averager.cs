using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPEGAlgorithm {
    class Averager {
        public ImageBlock Average(ImageBlock imageBlock) {
            var w = imageBlock.Width;
            var h = imageBlock.Height;
            if (w % 4 != 0 && h % 4 != 0) {
                return imageBlock;
            }
            var array = imageBlock.Data;
            float average;
            for (var i = 0; i < w; i+=2) {
                for (var j = 0; j < h; j+=2) {
                    average = (array[i, j] + array[i+1, j] + array[i, j+1] + array[i+1, j+1])/4;
                    array[i, j] = array[i+1, j] = array[i, j+1] = array[i+1, j+1] = (int) Math.Round(average);
                }
            }
            return imageBlock;
        }

        public ImageBlock Average(ImageBlock[,] imageBlocksMatrix) {
            if (imageBlocksMatrix.GetLength(0) != imageBlocksMatrix.GetLength(1) && imageBlocksMatrix.GetLength(1) != 2) {
                throw new ArgumentException("Matrix should be square and dimention = 2");
            }
            int[,] data;
            int[,] resData = new int[8, 8];
            float average;
            for (var k = 0; k < 2; k++) {
                for (var t = 0; t < 2; t++) {
                    data = imageBlocksMatrix[k, t].Data;
                    for (int i = 4 * k, u = 0; i < 4 * (1 + k); i++, u+=2) {
                        for (int j = 4 * t, v = 0; j < 4 * (1 + t); j++, v+=2) {
                            average = ((float)(data[u, v] + data[u + 1, v] + data[u, v + 1] + data[u + 1, v + 1])) / 4;
                            resData[i, j] = (int)Math.Round(average);
                        }
                    }
                }
            }
            return new ImageBlock(resData);
        }
    }
}

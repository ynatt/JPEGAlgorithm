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
                    array[i, j] = array[i+1, j] = array[i, j+1] = array[i+1, j+1] = (int) average;
                }
            }
            return imageBlock;
        }

        public ImageBlock Average(ImageBlock[,] imageBlocksMatrix) {
            /*if (imageBlocksMatrix.GetLength(0) != imageBlocksMatrix.GetLength(1) && imageBlocksMatrix.GetLength(1) != 2) {
                throw new ArgumentException("Matrix should be square and dimention = 2");
            }*/
			int w = imageBlocksMatrix[0, 0].Width;
			int h = imageBlocksMatrix[0, 0].Height;
			int[,] data = MatrixUtils<int>.Assemble(imageBlocksMatrix);
            int[,] resData = new int[w, h];
            float average;
			int k = 0;
			int t = 0;
			for (int i = 0; i < w; i++) {
				for (int j = 0; j < h; j++) {
					k = i * 2;
					t = j * 2;
					average = ((float)(data[k, t] + data[k, t + 1] + data[k + 1, t] + data[k + 1, t + 1])) / 4;
					resData[i, j] = (int) average;
				}
			}
            return new ImageBlock(resData);
        }


    }
}

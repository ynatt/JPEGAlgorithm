using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPEGAlgorithm {
    class ImageBlock {
        private int[,] data;

        private int height;

        private int width;

        public ImageBlock(int[,] data) {
            Width = data.GetLength(0);
            Height = data.GetLength(1);
            this.data = data;
        }

		public ImageBlock(Complex[,] data) {
			Width = data.GetLength(0);
			Height = data.GetLength(1);
			int[,] readData = new int[width, height];
			for (var i = 0; i < width; i++) {
				for (var j = 0; j < height; j++) {
					readData[i, j] =(int) data[i, j].GetRe();
				}
			}
			this.data = readData;
		}

		public ImageBlock(ImageBlock[,] imageBlocksMatrix) {
			Height = imageBlocksMatrix[0, 0].Height * 2;
			Width = imageBlocksMatrix[0, 0].Width * 2;
			data = new int[width, height];
			for (int i = 0; i < 2; i++) {
				for (int j = 0; j < 2; j++) {
					for (int k = 0; k < imageBlocksMatrix[0, 0].Width; k++) {
						for (int t = 0; t < imageBlocksMatrix[0, 0].Height; t++) {
							data[i * imageBlocksMatrix[0, 0].Width + k, j * imageBlocksMatrix[0, 0].Height + t] = imageBlocksMatrix[i, j].data[k, t];
						}
					}
				}
			}
		}

		public static ImageBlock FromAvaraged(ImageBlock avaraged) {
			var data = new int[avaraged.Width * 2, avaraged.Height * 2];
			for(int k = 0; k < avaraged.Width * 2; k++) {
				for (int t = 0; t < avaraged.Height * 2; t++) {
					data[k, t] = avaraged.data[k / 2, t / 2]; 
				}
			}
			return new ImageBlock(data);
		}

        public int[,] Data { get => data; set => data = value; }
        public int Height { get => height; set => height = value; }
        public int Width { get => width; set => width = value; }

        public override string ToString() {
            StringBuilder sb = new StringBuilder();
            for (var i = 0; i < width; i++) {
                for (var j = 0; j < height; j++) {
                    sb.Append(data[i, j]).Append(" ");
                }
                sb.Append("\n");
            }
            return sb.ToString();
        }
    }
}

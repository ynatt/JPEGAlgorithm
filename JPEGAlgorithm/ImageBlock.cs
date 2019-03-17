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

		public ImageBlock(ImageBlock[,] imageBlocksMatrix) {
			Height = 16;
			Width = 16;
			data = new int[width, height];
			for (int i = 0; i < 2; i++) {
				for (int j = 0; j < 2; j++) {
					for (int k = 0; k < 8; k++) {
						for (int t = 0; t < 8; t++) {
							data[i * 8 + k, j * 8 + t] = imageBlocksMatrix[i, j].data[k, t];
						}
					}
				}
			}
		}

		public static ImageBlock FromAvaraged(ImageBlock avaraged) {
			var data = new int[16, 16];
			for(int k = 0; k < 16; k++) {
				for (int t = 0; t < 16; t++) {
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

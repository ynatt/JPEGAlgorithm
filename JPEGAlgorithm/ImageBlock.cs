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
            Height = data.GetLength(0);
            Width = data.GetLength(1);
            this.data = data;
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

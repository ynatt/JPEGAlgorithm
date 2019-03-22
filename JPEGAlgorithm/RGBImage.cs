using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace JPEGAlgorithm
{
    class RGBImage {
        private RGBPixel[,] pixels;

        private int width;

        private int height;

        public RGBImage(Image image) {
            Width = image.Width;
            Height = image.Height;
            Pixels = ImageUtils.toRGBMatrix(image);
        }

        public RGBImage(RGBPixel[,] pixels) {
            Width = pixels.GetLength(0);
            Height = pixels.GetLength(1);
            Pixels = pixels;
        }

		public static RGBImage FromMatrix(RGBImage[,] matrix) {
			var w = matrix.GetLength(0);
			var h = matrix.GetLength(1);
			var pixels = new RGBPixel[w * 16, h * 16];
			for (int i = 0; i < w; i++) {
				for (int j = 0; j < h; j++) {
					for (int k = 0; k < 16; k++) {
						for (int t = 0; t < 16; t++) {
							pixels[i * 16 + k, j * 16 + t] = matrix[i, j].Pixels[k, t];
						}
					}
				}
			}
			return new RGBImage(pixels);
		}

        public int Width { get => width; set => width = value; }
        public int Height { get => height; set => height = value; }
        internal RGBPixel[,] Pixels { get => pixels; set => pixels = value; }

        public RGBPixel GetPixel(int x, int y) {
            return Pixels[x, y];
        }

        public RGBImage(RGBPixel[,] pixels, int width, int height) {
            Width = width;
            Height = height;
            this.Pixels = pixels;
        }

        public RGBImage SubImage(int dimension, int x, int y) {
            var pixels = new RGBPixel[dimension, dimension];
            for (int i = 0; i < dimension; i++){
                for (int j = 0; j < dimension; j++) {
                    pixels[i, j] = this.Pixels[x + i, y + j];
                }
            }
            return new RGBImage(pixels, dimension, dimension);
        }

        public override string ToString() {
            var result = new StringBuilder();
            for (var i = 0; i < width; i++) {
                for (var j = 0; j < height; j++) {
                    result.Append(Pixels[i, j]);
                    if (j != height - 1) {
                        result.Append(" ");
                    }
                }
                result.Append("\n");
            }
            return result.ToString();
        }

        public Image ToImage() {
            Bitmap result = new Bitmap(width, height, PixelFormat.Format24bppRgb);
			Rectangle rect = new Rectangle(0, 0, width, height);
			BitmapData bitmapData = result.LockBits(rect, ImageLockMode.WriteOnly, result.PixelFormat);
			IntPtr ptr = bitmapData.Scan0;
			byte[] bytes = new byte[Math.Abs(bitmapData.Stride) * height];
			RGBPixel pixel;
			var pixels = this.pixels;
            int red;
            int green;
            int blue;
			int pointer = 0;
            for (var y = 0; y < height; y++) {
				pointer = bitmapData.Stride * y;
				for (var x = 0; x < width; x++) {
                    pixel = pixels[x, y];
                    red = pixel.r;
                    green = pixel.g;
                    blue = pixel.b;
					red = red < 0 ? 0 : red > 255 ? 255 : red;
					green = green < 0 ? 0 : green > 255 ? 255 : green;
					blue = blue < 0 ? 0 : blue > 255 ? 255 : blue;
					bytes[pointer++] = (byte) blue;
					bytes[pointer++] = (byte) green;
					bytes[pointer++] = (byte) red;
                }
            }
			Marshal.Copy(bytes, 0 , ptr, bytes.Length);
			result.UnlockBits(bitmapData);
            return result;
        }

        public YCbCrImage ToYCbCrImage() {
            var yCbCrPixels = new YCbCrPixel[width, height];
            for (var i = 0; i < width; i++) {
                for (var j = 0; j < height; j++) {
                    yCbCrPixels[i, j] = pixels[i, j].ToYCC();
                }
            }
            return new YCbCrImage(yCbCrPixels);
        }

        public RGBImage[] ToBlockArray(int blockSize, out int widthBlocks, out int heightBlocks) {
            if (Height % blockSize != 0 && Width % blockSize != 0) {
                throw new ArgumentException("Image don't fit to chunk with size = " + blockSize);
            }
            heightBlocks = Height / blockSize;
            widthBlocks = Width / blockSize;
            var res = new RGBImage[widthBlocks * heightBlocks];
            for (var j = 0; j < heightBlocks; j++) {
                for (var i = 0; i < widthBlocks; i++) {
                    res[j * widthBlocks + i] = SubImage(blockSize, i * blockSize, j * blockSize);
                }
            }
            return res;
        }

        public RGBImage[,] ToBlockMatrix(int blockSize) {
            if(Height % blockSize != 0 && Width % blockSize != 0) {
                throw new ArgumentException("Can't build matrix with blockSize = " + blockSize);
            }
            var heightBlocks = Height / blockSize;
            var widthBlocks = Width / blockSize;
            var res = new RGBImage[widthBlocks, heightBlocks];
            for (var i = 0; i < widthBlocks; i++) {
                for (var j = 0; j < heightBlocks; j++) {
                    res[i, j] = SubImage(blockSize, i * blockSize, j * blockSize);
                }
            }
            return res;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPEGAlgorithm
{
    class YCbCrImage
    {
        private YCbCrPixel[,] pixels;

        private int width;

        public int Width { get => width; set => width = value; }

        private int height;

        public int Height { get => height; set => height = value; }
        internal YCbCrPixel[,] Pixels { get => pixels; set => pixels = value; }

        public enum Channel { 
            Y,Cb,Cr,ALL
        }

        public YCbCrImage(Image image) {
            Width = image.Width;
            Height = image.Height;
            Pixels = ImageUtils.toYCCMatrix(ImageUtils.toRGBMatrix(image));
        }

        public YCbCrImage(YCbCrPixel[,] pixels, int width, int height) {
            Width = width;
            Height = height;
            this.Pixels = pixels;
        }

        public YCbCrImage SubImage(int dimension, int x, int y) {
            var pixels = new YCbCrPixel[dimension, dimension];
            for (int i = 0; i < dimension; i++) {
                for (int j = 0; j < dimension; j++) {
                    pixels[i, j] = this.Pixels[x + i, y + j];
                }
            }
            return new YCbCrImage(pixels, dimension, dimension);
        }

        public Image GetYChannel() {
            return ToImage(Channel.Y);
        }

        public Image GetCbChannel() {
            return ToImage(Channel.Cb);
        }

        public Image GetCrChannel() {
            return ToImage(Channel.Cr);
        }

        public Image ToImage() {
            return ToImage(Channel.ALL);
        }

        public Image ToImage(Channel channel) {
            Bitmap result = new Bitmap(width, height,
                System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            YCbCrPixel pixel;
            int y;
            int cb;
            int cr;
            for (var i = 0; i < width; i++) {
                for (var j = 0; j < height; j++) {
                    pixel = Pixels[i, j];
                    switch (channel) {
                        case Channel.Y:
                            y = cb = cr = (int) Math.Round(pixel.y);
                            break;
                        case Channel.Cb:
                            y = cr = cb = (int) Math.Round(pixel.cb);
                            break;
                        case Channel.Cr:
                            y = cb = cr = (int) Math.Round(pixel.cr);
                            break;
                        default:
                            y = (int)Math.Round(pixel.y);
                            cb = (int)Math.Round(pixel.cb);
                            cr = (int)Math.Round(pixel.cr);
                            break;
                    }
                    result.SetPixel(i, j, Color.FromArgb(y, cb, cr));
                }
            }
            return result;
        }

        public ImageBlock GetImageBlock(Channel channel) {
            int[,] data = new int[width, height];
            ImageBlock block = new ImageBlock(data);
            for (var x = 0; x < width; x++) {
                for (var y = 0; y < height; y++) {
                    switch (channel) {
                        case Channel.Y:
                            data[x, y] =(int) Math.Round(pixels[x, y].y);
                            break;
                        case Channel.Cb:
                            data[x, y] = (int)Math.Round(pixels[x, y].cb);
                            break;
                        case Channel.Cr:
                            data[x, y] = (int)Math.Round(pixels[x, y].cr);
                            break;
                        case Channel.ALL:
                            throw new ArgumentException("Can't work with Channel.ALL");
                    }
                }
            }
            return block;
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
    }
}

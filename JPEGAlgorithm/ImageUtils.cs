using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPEGAlgorithm
{
    class ImageUtils
    {
        public static RGBPixel[,] toRGBMatrix(Image image) {
            int height = image.Height;
            int width = image.Width;
            RGBPixel[,] result = new RGBPixel[width, height];
            Color color;
            for (int i = 0; i < width; i++) {
                for (int j = 0; j < height; j++) {
                    color = ((Bitmap)image).GetPixel(i, j);
                    result[i, j] = new RGBPixel(color.R, color.G, color.B);
                }
            }
            return result;
        }

        public static YCbCrPixel[,] toYCCMatrix(RGBPixel[,] source) {
            int rows = source.GetLength(0);
            int columns = source.GetLength(1);
            YCbCrPixel[,] result = new YCbCrPixel[rows, columns];
            for (int i = 0; i < rows; i++) {
                for (int j = 0; j < columns; j++){
                    result[i, j] = source[i, j].ToYCC();
                }
            }
            return result;
        }

        public static RGBImage FitToBlockSize(RGBImage image, int blockSize) {
            int oldHeight = image.Height;
            int oldWidth = image.Width;
            int newHeight =(int) Math.Ceiling((double)image.Height / blockSize);
            newHeight *= blockSize;
            int newWidth =(int) Math.Ceiling((double)image.Width / blockSize);
            newWidth *= blockSize;
            var oldPixels = image.Pixels;
            var newPixels = new RGBPixel[newWidth, newHeight];
            var stubPixel = new RGBPixel(0, 0, 0);
            for (var i = 0; i <  newWidth; i++) {
                for (var j = 0; j < newHeight; j++) {
                    if (i < oldWidth && j < oldHeight) {
                        newPixels[i, j] = oldPixels[i, j];
                    } else {
                        newPixels[i, j] = stubPixel;
                    }
                }
            }
            return new RGBImage(newPixels);
        }

        public static void Shift(ImageBlock imageBlock) {
            var data = imageBlock.Data;
            for (var i = 0; i < imageBlock.Width; i++) {
                for (var j = 0; j < imageBlock.Height; j++) {
                    data[i, j] -= 128; 
                }
            }
        }
    }
}

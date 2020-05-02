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
    class ImageUtils
    {
        public static RGBPixel[,] toRGBMatrix(Image image) {
            int height = image.Height;
            int width = image.Width;
            RGBPixel[,] result = new RGBPixel[width, height];
			Rectangle rect = new Rectangle(0, 0, width, height);
			BitmapData bitmapData = ((Bitmap)image).LockBits(rect, ImageLockMode.ReadOnly, image.PixelFormat);
			IntPtr ptr = bitmapData.Scan0;
			byte[] bytes = new byte[Math.Abs(bitmapData.Stride) * height];
			Marshal.Copy(ptr, bytes, 0, bytes.Length);
			((Bitmap)image).UnlockBits(bitmapData);
			var pixelSize = Image.GetPixelFormatSize(image.PixelFormat) / 8;
			var pointer = 0;
			var k = 0;
            for (int j = 0; j < height; j++) {
				pointer = bitmapData.Stride * j;
				for (int i = 0; i < width; i++) {
                    result[i, j] = new RGBPixel(bytes[pointer + k + 2], bytes[pointer + k + 1], bytes[pointer + k]);
					k += pixelSize;
				}
				k = 0;
            }
            return result;
        }

		public static Image SubImage(Image image, int x, int y, int width, int height) {
			Rectangle rect = new Rectangle(x, y, width, height);
			BitmapData sourceBitmapData = ((Bitmap)image).LockBits(rect, ImageLockMode.ReadOnly, image.PixelFormat);
			byte[] bytes = new byte[sourceBitmapData.Stride * height];
			Marshal.Copy(sourceBitmapData.Scan0, bytes, 0, bytes.Length);
			((Bitmap)image).UnlockBits(sourceBitmapData);
			Bitmap result = new Bitmap(width, height, image.PixelFormat);
			rect = new Rectangle(0, 0, width, height);
			var resultBitmapData = result.LockBits(rect, ImageLockMode.WriteOnly, image.PixelFormat);
			var resBytes = new byte[resultBitmapData.Stride * height];
			var pixelSize = Image.GetPixelFormatSize(image.PixelFormat) / 8;
			for (int j = 0; j < height; j++) {
				Array.Copy(bytes, j * sourceBitmapData.Stride, resBytes, j * resultBitmapData.Stride, width * 3);
			}
			Marshal.Copy(resBytes, 0, resultBitmapData.Scan0, resBytes.Length);
			result.UnlockBits(resultBitmapData);
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
            int newHeight =(int) Math.Ceiling((float)image.Height / blockSize);
            newHeight *= blockSize;
            int newWidth =(int) Math.Ceiling((float)image.Width / blockSize);
            newWidth *= blockSize;
			if (oldHeight == newHeight && oldWidth == newWidth) {
				return image;
			}
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

		public static void Unshift(ImageBlock imageBlock) {
			var data = imageBlock.Data;
			for (var i = 0; i < imageBlock.Width; i++) {
				for (var j = 0; j < imageBlock.Height; j++) {
					data[i, j] += 128;
				}
			}
		}

		public static YCbCrPixel[,] Merge(ImageBlock y, ImageBlock cb, ImageBlock cr){
			var pixels = new YCbCrPixel[y.Width, y.Height];
			for (var i = 0; i < y.Width; i++) {
				for (var j = 0; j < y.Height; j++) {
					pixels[i, j] = new YCbCrPixel(y.Data[i, j], cb.Data[i, j], cr.Data[i, j]);
				}
			}
			return pixels;
		}
	}
}

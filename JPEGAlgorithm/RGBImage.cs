﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
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
            return ToImage(Channel.ALL);
        }

        public Image GetRedChannel() {
            return ToImage(Channel.RED);
        }

        public Image GetGreenChannel() {
            return ToImage(Channel.GREEN);
        }

        public Image GetBlueChannel() {
            return ToImage(Channel.BLUE);
        } 

        public enum Channel { 
            RED,GREEN,BLUE,ALL
        }

        public Image ToImage(Channel channel) {
            Bitmap result = new Bitmap(width, height,
                System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            RGBPixel pixel;
            int red;
            int green;
            int blue;
            for (var x = 0; x < width; x++) {
                for (var y = 0; y < height; y++) {
                    pixel = Pixels[x, y];
                    switch (channel) {
                        case Channel.RED:
                            red = green = blue = pixel.r;
                            break;
                        case Channel.GREEN:
                            red = green = blue = pixel.g;
                            break;
                        case Channel.BLUE:
                            red = green = blue = pixel.b;
                            break;
                        default:
                            red = pixel.r;
                            green = pixel.g;
                            blue = pixel.b;
                            break;
                    }
                    result.SetPixel(x, y, Color.FromArgb(red, green, blue));
                }
            }
            return result;
        }

        public YCbCrImage ToYCbCrImage() {
            var yCbCrPixels = new YCbCrPixel[width, height];
            for (var i = 0; i < width; i++) {
                for (var j = 0; j < height; j++) {
                    yCbCrPixels[i, j] = pixels[i, j].ToYCC();
                }
            }
            return new YCbCrImage(yCbCrPixels, width, height);
        }

        public RGBImage[] ToBlockArray(int blockSize) {
            if (Height % blockSize != 0 && Width % blockSize != 0) {
                throw new ArgumentException("Image don't fit to chunk with size = " + blockSize);
            }
            var heightBlocks = Height / blockSize;
            var widthBlocks = Width / blockSize;
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

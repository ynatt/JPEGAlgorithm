using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;

namespace JPEGAlgorithm
{
    class JPEG
    {
        private Image sourceImage;

        private YCbCrPixel[,] pixelMatrix;
        
        public JPEG(Image sourceImage)
        {
            this.sourceImage = sourceImage;
        }

        public Image Convert() {
            pixelMatrix = ImageUtils.toYCCMatrix(ImageUtils.toRGBMatrix(sourceImage));

            return null;
        }

        public enum ColorScheme { 
            RGB, YCbCr
        }
    }
}

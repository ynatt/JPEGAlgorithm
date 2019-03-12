using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPEGAlgorithm
{
    struct RGBPixel
    {
        public int r, g, b;

        public RGBPixel(int r, int g, int b)
        {
            this.r = r;
            this.g = g;
            this.b = b;
        }

        public YCbCrPixel ToYCC() {
            var y  = 0.299     * r + 0.587    * g + 0.114    * b;
            var cb = -0.168736 * r - 0.331264 * g + 0.5      * b + 128;
            var cr = 0.5       * r - 0.418688 * g - 0.081312 * b + 128;
            return new YCbCrPixel(y, cb, cr);
        }

        public override string ToString()
        {
            return "[" + r + ", " + g + ", " + b + "]";
        }
    }
}

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
            var y  = 0.299f     * r + 0.587f    * g + 0.114f    * b;
            var cb = -0.168736f * r - 0.331264f * g + 0.5f      * b + 128;
            var cr = 0.5f       * r - 0.418688f * g - 0.081312f * b + 128;
            return new YCbCrPixel(y, cb, cr);
        }

        public override string ToString()
        {
            return "[" + r + ", " + g + ", " + b + "]";
        }
    }
}

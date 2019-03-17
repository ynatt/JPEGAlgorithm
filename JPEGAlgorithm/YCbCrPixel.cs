using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPEGAlgorithm
{
    struct YCbCrPixel
    {
        public float y, cb, cr;

        public YCbCrPixel(float y, float cb, float cr)
        {
            this.y = y;
            this.cb = cb;
            this.cr = cr;
        }

		public RGBPixel ToRGB() {
			var r = y + 1.402 * (cr - 128);
			var g = y - 0.34414 * (cb - 128) - 0.71414 * (cr - 128);
			var b = y + 1.772 * (cb - 128);
			return new RGBPixel((int)r, (int)g, (int)b);
		}

        public override string ToString() {
            return "[" + y + ", " + cb + ", " + cr + "]";
        }
    }
}

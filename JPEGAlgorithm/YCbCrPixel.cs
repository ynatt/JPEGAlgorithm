using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPEGAlgorithm
{
    struct YCbCrPixel
    {
        public double y, cb, cr;

        public YCbCrPixel(double y, double cb, double cr)
        {
            this.y = y;
            this.cb = cb;
            this.cr = cr;
        }

        public override string ToString() {
            return "[" + y + ", " + cb + ", " + cr + "]";
        }
    }
}

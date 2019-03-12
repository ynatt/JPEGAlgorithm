using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPEGAlgorithm {
	class NumberUtils {
		public static int GetBitsLength(int value) {
			int length = 0;
			if (value < 0) {
				value = -value;
			}
			while (value != 0) {
				length++;
				value >>= 1;
			}
			return length;
		}
	}
}

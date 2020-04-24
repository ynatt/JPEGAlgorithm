using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPEGAlgorithm.vilenklin {
	class ParyX {
		private List<int> expansion;

		private decimal x;

		public ParyX(List<int> expansion, decimal x) {
			this.expansion = expansion;
			this.x = x;
		}

		public int ElementAt(int k) {
			if (k >= expansion.Count) {
				return 0;
			}
			return expansion.ElementAt(k);
		}

		public decimal GetX() {
			return x;
		}

		public override string ToString() {
			return NumberString() + " [x=" + string.Format("{0:F5}", x) + "]";
			//return ArrayUtils.ArrayToString(expansion, "x");
		}

		public string NumberString() {
			StringBuilder sb = new StringBuilder();
			bool stopCleanZero = false;
			for (int i = expansion.Count - 1; i >= 0; i--) {
				if (expansion.ElementAt(i) != 0 || stopCleanZero) {
					stopCleanZero = true;
					sb.Append(expansion[i]);
				}
			}
			if (!stopCleanZero) {
				sb.Append(0);
			}
			return sb.ToString();
		}

		public static string Reverse(string s) {
			char[] charArray = s.ToCharArray();
			Array.Reverse(charArray);
			return new string(charArray);
		}
	}
}

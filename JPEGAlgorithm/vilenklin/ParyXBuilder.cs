using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPEGAlgorithm.vilenklin {
	class ParyXBuilder {
		private ExpansionBase expansionBase;

		private int roundDecimals;

		public ParyXBuilder(ExpansionBase expansionBase, int roundDecimals) {
			this.expansionBase = expansionBase;
			this.roundDecimals = roundDecimals;
		}

		public ParyX Build(decimal x) {
			List<int> expansion = new List<int>(expansionBase.GetK());
			ArrayUtils.Fill(expansion, 0, expansionBase.GetK());
			int i = 0;
			decimal k = x;
			int r;
			while (k != 0 && i < expansionBase.GetK()) {
				k = Math.Round(k * expansionBase.Pk(i), roundDecimals);
				r = (int)k / 1;
				k = k % 1;
				expansion.RemoveAt(i);
				expansion.Insert(i, r);
				i++;
			}
			return new ParyX(expansion, x);
		}

		public ParyX Build(int[] expansion, decimal x) {
			return new ParyX(expansion.ToList(), x);
		}
	}
}

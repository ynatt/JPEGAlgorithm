using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPEGAlgorithm.vilenklin {
	class ParyN {
		private List<int> expansion;

		public ParyN(ExpansionBase expansionBase, int n) {
			expansion = new List<int>(expansionBase.GetK());
			ArrayUtils.Fill(expansion, 0, expansionBase.GetK());
			int i = 0;
			int r;
			int k = n;
			while (k != 0) {
				r = k % expansionBase.Pk(i);
				k = k / expansionBase.Pk(i);
				expansion.RemoveAt(i);
				expansion.Insert(i, r);
				i++;
			}
		}

		public int Ak(int k) {
			return expansion.ElementAt(k);
		}

		public List<int> GetNotZeroCoeffs() {
			List<int> indexes = new List<int>();
			for (int i = 0; i < expansion.Count; i++) {
				if (expansion[i] != 0) {
					indexes.Add(i);
				}
			}
			return indexes;
		}

		public override string ToString() {
			return ArrayUtils.ArrayToString(expansion, "e");
		}
	}
}

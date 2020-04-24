using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPEGAlgorithm {
	class ExpansionBase {
		private List<int> p;

		private List<int> m;

		public ExpansionBase(List<int> p) {
			this.p = p;
			m = BuildM(p);
		}

		private List<int> BuildM(List<int> p) {
			List<int> result = new List<int>(p.Count()) { 1 };
			for (int i = 1; i < p.Count(); i++) {
				result.Insert(i, result.ElementAt(i - 1) * p.ElementAt(i - 1));
			}
			return result;
		}

		public int GetK() {
			return p.Count;
		}

		public int getMSize() {
			return m.Count;
		}

		public int Pk(int k) {
			return p.ElementAt(k);
		}

		public int Mk(int k) {
			return m.ElementAt(k);
		}

		public override string ToString() {
			return ArrayUtils.ArrayToString(p, "p") + ArrayUtils.ArrayToString(m, "m");
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPEGAlgorithm.vilenklin {
	class VilenkinFunction {
		private ParyN n;

		private ExpansionBase ExpBase;

		private List<RademacherFunction> Rj;

		public VilenkinFunction(ExpansionBase expBase, ParyN n) {
			Rj = new List<RademacherFunction>();
			List<int> notZeroIndexes = n.GetNotZeroCoeffs();
			foreach (int j in notZeroIndexes) {
				Rj.Add(new RademacherFunction(expBase, j));
			}
			ExpBase = expBase;
			this.n = n;
		}
		private Complex ONE_RE = new Complex(1, 0);

		public Complex Value(ParyX x) {
			Complex value = ONE_RE;
			for (int j = 0; j < Rj.Count; j++) {
				value *= (Rj[j].Value(x, n.Ak(Rj[j].GetK())));
			}
			return value;
		}
	}
}

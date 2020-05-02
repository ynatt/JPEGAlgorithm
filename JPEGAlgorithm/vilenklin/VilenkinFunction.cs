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

		private Dictionary<ParyX, Complex> cache;

		private readonly object syncLock = new object();

		public VilenkinFunction(ExpansionBase expBase, ParyN n) {
			cache = new Dictionary<ParyX, Complex>();
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
			lock (syncLock) {
				if (cache.ContainsKey(x)) {
					return cache[x];
				}
				Complex value = ONE_RE;
				for (int j = 0; j < Rj.Count; j++) {
					value *= (Rj[j].Value(x, n.Ak(Rj[j].GetK())));
				}
				cache.Add(x, value);
				return value;
			}
		}
	}
}

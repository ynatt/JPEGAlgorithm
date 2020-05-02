using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPEGAlgorithm.vilenklin {
	class RademacherFunction {
		private readonly int k;
		private readonly int Pk;
		private Dictionary<int, Dictionary<int, Complex>> values;

		public RademacherFunction(ExpansionBase expBase, int k) {
			this.k = k;
			Pk = expBase.Pk(k);
			values = new Dictionary<int, Dictionary<int, Complex>>();
		}

		public Complex Value(ParyX x) {
			return Value(x, 1);
		}

		public Complex Value(ParyX x, int pow) {
			int Xk = x.ElementAt(k);
			Dictionary<int, Complex> pows;
			if (values.ContainsKey(Xk)) {
				pows = values[Xk];
				if (values[Xk].ContainsKey(pow)) {
					return values[Xk][pow];
				}
			} else {
				pows = new Dictionary<int, Complex>();
				if (!values.ContainsKey(Xk)) {
					values.Add(Xk, pows);
				}
			}
			double phi = 2 * Math.PI * Xk * pow / Pk;
			Complex value = new Complex(Math.Cos(phi), Math.Sin(phi));
			if (!pows.ContainsKey(pow)) {
				pows.Add(pow, value);
			}
			return value;
		}

		public int GetK() {
			return k;
		}
	}
}

using JPEGAlgorithm.vilenklin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPEGAlgorithm.utils {
	class ConstIntervalsUtils {
		public static ParyX[] Intervals(ExpansionBase expBase, int k) {
			ParyXBuilder xBuilder = new ParyXBuilder(expBase, 13);
			int N = expBase.Mk(k + 1);
			ParyX[] xs = new ParyX[N];
			decimal dN = new decimal(N);
			int[] exp = new int[k + 1];
			int current = k;
			xs[0] = xBuilder.Build(exp, new decimal(0) / dN);
			for (int j = 1; j < N; j++) {
				exp[current]++;
				while (exp[current] == expBase.Pk(current)) {
					exp[current--] = 0;
					exp[current]++;
				}
				current = k;
				xs[j] = xBuilder.Build(exp, new decimal(j) / dN);
			}
			return xs;
		}

		public static Complex[] BuildConstFunc(ParyX[] xs) {
			Complex[] values = new Complex[xs.Length];
			for (int i = 0; i < xs.Length; i++) {
				values[i] = new Complex(Math.Cos((double)xs[i].GetX() * 10), 0);
			}
			return values;
		}
	}
}

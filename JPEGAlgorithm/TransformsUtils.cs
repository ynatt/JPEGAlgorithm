using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPEGAlgorithm {
    class TransformsUtils {
        public static double[,] DCT(ImageBlock imageBlock) {
            var block = imageBlock.Data;
            var n = imageBlock.Width;
            var result = new double[n, n];
            var value = 0d;
            var subValue = 0d;
            var coeff = 0d;
            var firstCos = 0d;
            for (var u = 0; u < n; u++) {
                for (var v = 0; v < n; v++) {
                    coeff = C(u, n) * C(v, n);
                    for (var i = 0; i < n; i++) {
                        firstCos = Cos(u, i, n);
                        for (var j = 0; j < n; j++) {
                            subValue += Cos(v, j, n) * block[i, j];
                        }
                        value += firstCos * subValue;
                        subValue = 0d;
                    }
                    result[u, v] = coeff * value;
                    value = 0d;
                }
            }
            return result;
        }

		public static int[,] Reverse_DCT(double[,] data) {
			var n = data.GetLength(0);
			var result = new int[n, n];
			var value = 0d;
			var subValue = 0d;
			var coeff = 0d;
			var firstCos = 0d;
			for (var u = 0; u < n; u++) {
				for (var v = 0; v < n; v++) {
					for (var i = 0; i < n; i++) {
						firstCos = Cos(i, u, n);
						for (var j = 0; j < n; j++) {
							coeff = C(i, n) * C(j, n);
							subValue += coeff * Cos(j, v, n) * data[i, j];
						}
						value += firstCos * subValue;
						subValue = 0d;
					}
					result[u, v] =(int) Math.Round(value);
					value = 0d;
				}
			}
			return result;
		}

        public static double Cos(int u, int k, int n) {
            return Math.Cos((u * Math.PI * (k + 0.5))/n);
        }

        private static double C(int i, int n) {
            return i != 0 ? Math.Sqrt(2d/n) : 1 / Math.Sqrt(n); 
        }
    }
}

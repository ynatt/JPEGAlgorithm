using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPEGAlgorithm {
    class TransformsUtils {

		public static Dictionary<int, double[,]> cosMatrixes = new Dictionary<int, double[,]>();

        public static double[,] DCT(ImageBlock imageBlock) {
            var block = imageBlock.Data;
            var n = imageBlock.Width;
			var cosMatrix = CountCosMatrixFor(n);
			var result = new double[n, n];
            var value = 0d;
            var subValue = 0d;
            var coeff = 0d;
            var firstCos = 0d;
			var C_0 = 1 / Math.Sqrt(n);
			var C_I = Math.Sqrt(2d / n);
			for (var u = 0; u < n; u++) {
                for (var v = 0; v < n; v++) {
                    coeff = C(u, C_0, C_I) * C(v, C_0, C_I);
                    for (var i = 0; i < n; i++) {
						firstCos = cosMatrix[u, i];
                        for (var j = 0; j < n; j++) {
                            subValue += cosMatrix[v, j] * block[i, j];
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

		private static double[,] CountCosMatrixFor(int n) {
			if (!cosMatrixes.ContainsKey(n)) {
				var res = new double[n, n];
				for (int i = 0; i < n; i++) {
					for (int j = 0; j < n; j++) {
						res[i, j] = Cos(i, j, n);
					}
				}
				cosMatrixes.Add(n, res);
			}
			return cosMatrixes[n];
		}
		
		public static int[,] Reverse_DCT(double[,] data) {
			var n = data.GetLength(0);
			var result = new int[n, n];
			var cosMatrix = CountCosMatrixFor(n);
			var value = 0d;
			var subValue = 0d;
			var coeff = 0d;
			var firstCos = 0d;
			var C_0 = 1 / Math.Sqrt(n);
			var C_I = Math.Sqrt(2d / n);
			for (var u = 0; u < n; u++) {
				for (var v = 0; v < n; v++) {
					for (var i = 0; i < n; i++) {
						firstCos = cosMatrix[i, u];
						for (var j = 0; j < n; j++) {
							coeff = C(i, C_0, C_I) * C(j, C_0, C_I);
							subValue += coeff * cosMatrix[j, v] * data[i, j];
						}
						value += firstCos * subValue;
						subValue = 0d;
					}
					result[u, v] =(int) value;
					value = 0d;
				}
			}
			return result;
		}

        public static double Cos(int u, int k, int n) {
			if (u == 0) {
				return 1;
			}
            return Math.Cos((u * Math.PI * (k + 0.5))/n);
        }

        private static double C(int i, double C_0, double C_I) {
            return i != 0 ? C_I : C_0; 
        }
    }
}

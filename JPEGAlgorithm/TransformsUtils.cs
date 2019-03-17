using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPEGAlgorithm {
    class TransformsUtils {

		public static Dictionary<int, float[,]> cosMatrixes = new Dictionary<int, float[,]>();

        public static float[,] DCT(ImageBlock imageBlock) {
            var block = imageBlock.Data;
            var n = imageBlock.Width;
			var cosMatrix = CountCosMatrixFor(n);
			var result = new float[n, n];
            var value = 0f;
            var subValue = 0f;
			var C_0 = (float) (1 / Math.Sqrt(n));
			var C_I = (float) Math.Sqrt(2f / n);
			for (var u = 0; u < n; u++) {
                for (var v = 0; v < n; v++) {
                    for (var i = 0; i < n; i++) {
                        for (var j = 0; j < n; j++) {
                            subValue += cosMatrix[v, j] * block[i, j];
                        }
                        value += cosMatrix[u, i] * subValue;
                        subValue = 0f;
                    }
                    result[u, v] = (u == 0 ? C_0 : C_I) * (v == 0 ? C_0 : C_I) * value;
                    value = 0f;
                }
            }
            return result;
        }

		private static float[,] CountCosMatrixFor(int n) {
			if (!cosMatrixes.ContainsKey(n)) {
				var res = new float[n, n];
				for (int i = 0; i < n; i++) {
					for (int j = 0; j < n; j++) {
						res[i, j] = Cos(i, j, n);
					}
				}
				cosMatrixes.Add(n, res);
			}
			return cosMatrixes[n];
		}
		
		public static int[,] Reverse_DCT(float[,] data) {
			var n = data.GetLength(0);
			var result = new int[n, n];
			var cosMatrix = CountCosMatrixFor(n);
			var value = 0f;
			var zeroSubValue = 0f;
			var iSubValue = 0f;
			var C_0 = (float) (1 / Math.Sqrt(n));
			var C_I = (float) Math.Sqrt(2f / n);
			for (var u = 0; u < n; u++) {
				for (var v = 0; v < n; v++) {
					for (var i = 0; i < n; i++) {
						zeroSubValue = C_0 * cosMatrix[0, v] * data[i, 0];
						for (var j = 1; j < n; j++) {
							iSubValue += C_I * cosMatrix[j, v] * data[i, j];
						}
						value += (i == 0? C_0 : C_I) * cosMatrix[i, u] * (zeroSubValue + iSubValue);
						zeroSubValue = 0f;
						iSubValue = 0f;
					}
					result[u, v] = (int) Math.Round(value);
					value = 0f;
				}
			}
			return result;
		}

        public static float Cos(int u, int k, int n) {
			if (u == 0) {
				return 1;
			}
            return (float) Math.Cos((u * Math.PI * (k + 0.5))/n);
        }
    }
}

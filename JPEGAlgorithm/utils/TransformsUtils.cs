using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace JPEGAlgorithm {
    class TransformsUtils {

		public static Dictionary<int, float[,]> cosMatrixes = new Dictionary<int, float[,]>();

        public static float[,] DCT_1(ImageBlock imageBlock) {
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

		private static readonly object syncLock = new object();

		public static float[,] CountCosMatrixFor(int n) {
			lock (syncLock) {
				if (!cosMatrixes.ContainsKey(n)) {
					var res = new float[n, n];
					for (int i = 0; i < n; i++) {
						for (int j = 0; j < n; j++) {
							res[i, j] = Cos(i, j, n);
						}
					}
					cosMatrixes.Add(n, res);
				}
			}
			return cosMatrixes[n];
		}
		
		public static int[,] Reverse_DCT_1(float[,] data) {
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

		public static float[] DCT_d1_NotOptimized(float[] vector) {
			var N = vector.Length;
			var result = new float[N];
			var cosMatrix = CountCosMatrixFor(N);
			var C_0 = (float)(1 / Math.Sqrt(2));
			var value = 0f;
			for (int m = 0; m < N; m++) {
				for (int n = 0; n < N; n++) {
					value += vector[n] * cosMatrix[m, n];
				}
				result[m] = 0.5f * (m == 0 ? C_0 : 1f) * value;
				value = 0f;
			}
			return result;
		}

		private static float a = (float) Math.Cos(Math.PI / 16);
		private static float b = (float) Math.Cos(3 * Math.PI / 16);
		private static float c = (float) Math.Cos(5 * Math.PI / 16);
		private static float d = (float) Math.Cos(7 * Math.PI / 16);
		private static float l = (float) Math.Cos(6 * Math.PI / 16);
		private static float f = (float) Math.Cos(Math.PI / 8);
		private static float g = (float) Math.Cos(Math.PI / 4);
		private static float c_0 = (float)(1 / Math.Sqrt(8));

		public static float[] DCT_d1_Optimized(float[] x) {
			var X = new float[8];
			var z = new float[8];
			z[0] = x[0] - x[7];
			z[1] = x[2] - x[5];
			z[2] = x[4] - x[3];
			z[3] = x[6] - x[1];
			z[4] = x[0] + x[7] - x[4] - x[3];
			z[5] = x[2] + x[5] - x[6] - x[1];
			z[6] = x[0] + x[7] + x[4] + x[3] - x[2] - x[5] - x[6] - x[1];
			z[7] = x[0] + x[7] + x[4] + x[3] + x[2] + x[5] + x[6] + x[1];
			X[1] = 0.5f * (a * z[0] + c * z[1] - d * z[2] - b * z[3]);
			X[5] = 0.5f * (c * z[0] + d * z[1] - b * z[2] + a * z[3]);
			X[7] = 0.5f * (d * z[0] + b * z[1] + a * z[2] + c * z[3]);
			X[3] = 0.5f * (b * z[0] - a * z[1] + c * z[2] + d * z[3]);
			X[2] = 0.5f * (f * z[4] - l * z[5]);
			X[6] = 0.5f * (l * z[4] + f * z[5]);
			X[4] = 0.5f * g * z[6];
			X[0] = c_0 * z[7];
			return X;
		}

		public static float[] Reverse_DCT_d1_NotOptimized(float[] vector) {
			var N = vector.Length;
			var result = new float[N];
			var cosMatrix = CountCosMatrixFor(N);
			var C_0 = (float)(1 / Math.Sqrt(2));
			var value = 0f;
			for (int m = 0; m < N; m++) {
				for (int n = 0; n < N; n++) {
					value += (n == 0 ? C_0 : 1f) * vector[n] * cosMatrix[n, m];
				}
				result[m] = 0.5f * value;
				value = 0f;
			}
			return result;
		}

		public static int[,] Reverse_DCT(float[,] matrix) {
			var N = matrix.GetLength(0);
			var result = new float[N, N];
			var vector = new float[N];
			for (var i = 0; i < N; i++) {
				for (var j = 0; j < N; j++) {
					vector[j] = matrix[j, i];
				}
				vector = Reverse_DCT_d1_NotOptimized(vector);
				for (var j = 0; j < N; j++) {
					result[j, i] = vector[j];
				}
			}
			for (var i = 0; i < N; i++) {
				for (var j = 0; j < N; j++) {
					vector[j] = result[i, j];
				}
				vector = Reverse_DCT_d1_NotOptimized(vector);
				for (var j = 0; j < N; j++) {
					result[i, j] = vector[j];
				}
			}
			return MathUtils.RoundFloatMatrix(result);
		}

		public static float[,] DCT(ImageBlock imageBlock) {
			var matrix = imageBlock.Data;
			var N = matrix.GetLength(0);
			var result = new float[N, N];
			var vector = new float[N];
			for (var i = 0; i < N; i++) {
				for (var j = 0; j < N; j++) {
					vector[j] = matrix[i , j];
				}
				if (vector.Length == 8) {
					vector = DCT_d1_Optimized(vector);
				} else {
					vector = DCT_d1_NotOptimized(vector);
				}
				for (var j = 0; j < N; j++) {
					result[i, j] = vector[j];
				}
			}
			for (var i = 0; i < N; i++) {
				for (var j = 0; j < N; j++) {
					vector[j] = result[j, i];
				}
				if (vector.Length == 8) {
					vector = DCT_d1_Optimized(vector);
				} else {
					vector = DCT_d1_NotOptimized(vector);
				}
				for (var j = 0; j < N; j++) {
					result[j, i] = vector[j];
				}
			}
			return result;
		}

		public static void HaarTransform(float[] vector, int size) {
			float[] temp = new float[size];
			int n = size / 2;
			for (int i = 0; i < n; ++i) {
				float a = vector[2 * i];
				float b = vector[2 * i + 1];
				float summ = a + b;
				float diff = a - b;
				temp[i] = summ / 2;
				temp[n + i] = diff / 2;
			}
			for (int i = 0; i < size; ++i) {
				vector[i] = temp[i];
			}
		}

		public static void HaarTransform(float[,] matrix, int w, int h) {
			var vector = new float[w];
			for (var i = 0; i < h; i++) {
				for (var j = 0; j < w; j++) {
					vector[j] = matrix[i, j];
				}
				HaarTransform(vector, w);
				for (var j = 0; j < w; j++) {
					matrix[i, j] = vector[j];
				}
			}
			vector = new float[h];
			for (var i = 0; i < w; i++) {
				for (var j = 0; j < h; j++) {
					vector[j] = matrix[j, i];
				}
				HaarTransform(vector, h);
				for (var j = 0; j < h; j++) {
					matrix[j, i] = vector[j];
				}
			}
		}

		public static float[,] HaarTransform(ImageBlock imageBlock) {
			float[,] matrix = MathUtils.ToFloatMatrix(imageBlock.Data);
			int w = matrix.GetLength(0);
			int h = matrix.GetLength(1);
			while (w >= 2 && h >= 2) {
				HaarTransform(matrix, w, h);
				w /= 2;
				h /= 2;
			}
			return matrix;
		}

		public static int[,] HaarReverseTransform(float[,] matrix) {
			int w = matrix.GetLength(0);
			int h = matrix.GetLength(1);
			while (w >= 2 && h >= 2) {
				w /= 2;
				h /= 2;
			}
			w *= 2;
			h *= 2;
			while (w <= matrix.GetLength(0) && h <= matrix.GetLength(1)) {
				HaarReverseTransform(matrix, w, h);
				w *= 2;
				h *= 2;
			}
			return MathUtils.RoundFloatMatrix(matrix);
		}

		public static void HaarReverseTransform(float[] vector, int size) {
			float[] temp = new float[size];
			int n = size / 2;
			for (int i = 0; i < n; ++i) {
				float a = vector[i];
				float b = vector[i + n];
				float summ = a + b;
				float diff = a - b;
				temp[2 * i] = summ;
				temp[2 * i + 1] = diff;
			}
			for (int i = 0; i < size; ++i) {
				vector[i] = temp[i];
			}
		}

		public static void HaarReverseTransform(float[,] matrix, int w, int h) {
			var vector = new float[h];
			for (var i = 0; i < w; i++) {
				for (var j = 0; j < h; j++) {
					vector[j] = matrix[j, i];
				}
				HaarReverseTransform(vector, h);
				for (var j = 0; j < h; j++) {
					matrix[j, i] = vector[j];
				}
			}
			vector = new float[w];
			for (var i = 0; i < h; i++) {
				for (var j = 0; j < w; j++) {
					vector[j] = matrix[i, j];
				}
				HaarReverseTransform(vector, w);
				for (var j = 0; j < w; j++) {
					matrix[i, j] = vector[j];
				}
			}
		}
	}
}

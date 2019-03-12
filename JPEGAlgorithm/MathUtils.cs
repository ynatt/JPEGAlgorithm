using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPEGAlgorithm {
    class MathUtils {
        public static void RoundMatrix(double[,] matrix) {
            var w = matrix.GetLength(0);
            var h = matrix.GetLength(1);
            for (var i = 0; i < w; i++) {
                for (var j = 0; j < h; j++) {
                    matrix[i, j] =(int) Math.Round(matrix[i, j]);
                }
            }
        }

        public static void Quantize(double[,] matrix, double[,] qMatrix) {
            var n = matrix.GetLength(0);
            for (var i = 0; i < n; i++) {
                for (var j = 0; j < n; j++) {
                    matrix[i, j] = matrix[i, j] / qMatrix[i, j];
                }
            }
        }

        public static double[,] BuildQuantizationMatrix(double q, int n) {
            var matrix = new double[n, n];
            for (var i = 0; i < n; i++) {
                for (var j = 0; j < n; j++) {
                    matrix[i, j] = 3 + (i + j) * q;
                }
            }
            return matrix;
        }

        public static Dictionary<int, int> BuildBarChart(List<int> data) {
            var dict = new Dictionary<int, int>();
            int key;
            int value;
            for (int i = 0; i < data.Count; i++) {
                key = data[i];
                if (dict.ContainsKey(key)) {
                    dict.TryGetValue(key, out value);
                    value++;
                    dict.Remove(key);
                    dict.Add(key, value);
                } else {
                    dict.Add(key, 0);
                }
            }
            return dict;
        }

		public static List<int> MakeDiffOfDCCoeffs(List<int> dcCoeffs) {
			var diffs = new List<int>(dcCoeffs.Count) {dcCoeffs[0]};
			for (int i = 1; i < dcCoeffs.Count; i++) {
				diffs.Add(dcCoeffs[i] - dcCoeffs[i - 1]);
			}
			return diffs;
		}
    }
}

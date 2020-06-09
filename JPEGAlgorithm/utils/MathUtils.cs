using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace JPEGAlgorithm {
    class MathUtils {
        public static void RoundMatrix(float[,] matrix) {
            var w = matrix.GetLength(0);
            var h = matrix.GetLength(1);
            for (var i = 0; i < w; i++) {
                for (var j = 0; j < h; j++) {
                    matrix[i, j] =(int) Math.Round(matrix[i, j]);
                }
            }
        }

		public static int[,] RoundFloatMatrix(float[,] matrix) {
			var w = matrix.GetLength(0);
			var h = matrix.GetLength(1);
			var result = new int[w, h];
			for (var i = 0; i < w; i++) {
				for (var j = 0; j < h; j++) {
					result[i, j] = (int)Math.Round(matrix[i, j]);
				}
			}
			return result;
		}

		public static int[,] RoundComplexMatrixToInt(Complex[,] matrix) {
			var w = matrix.GetLength(0);
			var h = matrix.GetLength(1);
			var result = new int[w, h];
			for (var i = 0; i < w; i++) {
				for (var j = 0; j < h; j++) {
					result[i, j] = (int)Math.Round(matrix[i, j].GetRe());
				}
			}
			return result;
		}

		public static float[,] ToFloatMatrix(int[,] matrix) {
			var w = matrix.GetLength(0);
			var h = matrix.GetLength(1);
			var result = new float[w, h];
			for (var i = 0; i < w; i++) {
				for (var j = 0; j < h; j++) {
					result[i, j] = (float) matrix[i, j];
				}
			}
			return result;
		}

		public static Complex[,] ToComplexMatrix(int[,] matrix) {
			var w = matrix.GetLength(0);
			var h = matrix.GetLength(1);
			var result = new Complex[w, h];
			for (var i = 0; i < w; i++) {
				for (var j = 0; j < h; j++) {
					result[i, j] = new Complex(matrix[i, j], 0);
				}
			}
			return result;
		}

		public static Complex[,] RoundComplexMatrix(Complex[,] matrix) {
			var w = matrix.GetLength(0);
			var h = matrix.GetLength(1);
			var result = new Complex[w, h];
			for (var i = 0; i < w; i++) {
				for (var j = 0; j < h; j++) {
					result[i, j] =  new Complex(Math.Round(matrix[i, j].GetRe()), matrix[i, j].GetIm());
				}
			}
			return result;
		}

		public static void Quantize(float[,] matrix, float[,] qMatrix) {
            var n = matrix.GetLength(0);
            for (var i = 0; i < n; i++) {
                for (var j = 0; j < n; j++) {
                    matrix[i, j] = matrix[i, j] / qMatrix[i, j];
                }
            }
        }
		
		public static void Dequantize(float[,] matrix, float[,] qMatrix) {
			var n = matrix.GetLength(0);
			for (var i = 0; i < n; i++) {
				for (var j = 0; j < n; j++) {
					matrix[i, j] = matrix[i, j] * qMatrix[i, j];
				}
			}
		}

		public static float[,] BuildQuantizationMatrix(float acq, float dcq, int n) {
			var matrix = new float[n, n];
            for (var i = 0; i < n; i++) {
                for (var j = 0; j < n; j++) {
                    matrix[i, j] = dcq + (i + j) * acq;
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

		public static void CountMSE(Bitmap a, Bitmap b, out double rMSE, out double gMSE, out double bMSE) {
			var w = a.Width;
			var h = a.Height;
			var rect = new Rectangle(0, 0, w, h);
			var aBitmapData = a.LockBits(rect, ImageLockMode.ReadOnly, a.PixelFormat);
			var aBytes = new byte[aBitmapData.Stride * h];
			Marshal.Copy(aBitmapData.Scan0, aBytes, 0, aBytes.Length);
			a.UnlockBits(aBitmapData);
			var bBitmapData = b.LockBits(rect, ImageLockMode.ReadOnly, a.PixelFormat);
			var bBytes = new byte[aBitmapData.Stride * h];
			Marshal.Copy(bBitmapData.Scan0, bBytes, 0, bBytes.Length);
			b.UnlockBits(bBitmapData);
			var pixelSize = Image.GetPixelFormatSize(a.PixelFormat) / 8;
			var pointer = 0;
			double valueR = 0;
			double valueG = 0;
			double valueB = 0;
			var pixelOffset = pixelSize - 3;
			for (int i = 0; i < h; i++) {
				pointer = aBitmapData.Stride * i;
				for (int k = 0; k < w; k++) {
					valueR += Math.Pow(Math.Abs(aBytes[pointer] - bBytes[pointer++]), 2);
					valueG += Math.Pow(Math.Abs(aBytes[pointer] - bBytes[pointer++]), 2);
					valueB += Math.Pow(Math.Abs(aBytes[pointer] - bBytes[pointer]), 2);
					pointer += pixelOffset; 
				}
			}
			rMSE = Math.Round(valueR / (w * h), 2);
			gMSE = Math.Round(valueG / (w * h), 2);
			bMSE = Math.Round(valueB / (w * h), 2);
		}

		public static double CountPSNR(int max, double mse) {
			return Math.Round(20 * Math.Log10((double)max / Math.Sqrt(mse)), 2);
		}
    }
}

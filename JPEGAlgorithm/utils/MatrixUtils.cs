using JPEGAlgorithm.vilenklin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPEGAlgorithm {
    class MatrixUtils<T> {

        public static T[] ToArrayByZigZag(T[,] matrix) {
            if (matrix.GetLength(0) != matrix.GetLength(1)) {
                throw new ArgumentException("Matrix should be square, w = " + matrix.GetLength(0) + " h = " + matrix.GetLength(1));
            }
            int i = 0;
            int j = 0;
            int diagonalNum;
            T[] diagonal;
            int dim = matrix.GetLength(0);
            T[] res = new T[dim * dim];
            int resSize = 0;
            while (!(i == dim - 1 && j == dim)) {
                diagonalNum = i + j + 1;
                diagonal = FormDiagonal(!(diagonalNum % 2 == 0), i, j, dim, matrix);
                Array.Copy(diagonal, 0, res, resSize, diagonal.Length);
                resSize += diagonal.Length;
                if (i != dim - 1) {
                    i++;
                } else {
                    j++;
                }
            }
            return res;
        }

        private static T[] FormDiagonal(bool up, int i, int j, int dim, T[,] matrix) {
            T[] diagonal;
            int i0 = i;
            int j0 = j;
            if (i == dim - 1) {
                diagonal = new T[dim - j];
            } else {
                diagonal = new T[i + 1];
            }
            int k = 0;
            bool stop = false;
            do {
                if (up) {
                    diagonal[k++] = matrix[i, j];
                    if (--i < j0 & ++j > i0) {
                        stop = true;
                    }
                } else {
                    diagonal[k++] = matrix[j, i];
                    if (++j > i0 & --i < j0) {
                        stop = true;
                    }
                }
            } while (!stop);
            return diagonal;
        }

        public static T[,] ToMatrixByZigZag(T[] array, int dimention) {
            if (dimention * dimention != array.Length) {
                throw new ArgumentException("Array will not fit in matrix with dimention = " + dimention);
            }
            var res = new T[dimention, dimention];
            var current = 0;
            int diagLength = 0;
            int start;
            int end;
            for (int k = 1; k <= 2*dimention - 1; k++) {
                if (k <= dimention) {
                    diagLength = k;
                } else {
                    diagLength = 2 * dimention - k;
                }
                start = current;
                end = start + diagLength;
                Fill(res, k, start, end, array, dimention);
                current += diagLength;
            }
            return res;
        }

        private static void Fill(T[,] matrix, int diagNum, int start, int end, T[] array, int dim) {
            int i, j;
            if (diagNum <= dim) {
                i = 0;
                j = diagNum - 1;
            } else {
                i = diagNum - dim;
                j = dim - 1;
            }
            if (diagNum % 2 != 0) {
                for (var k = end - 1; k >= start; k--, i++, j--) {
                    matrix[i, j] = array[k];
                }
            } else {
                for (var k = start; k < end; k++, i++, j--) {
                    matrix[i, j] = array[k];
                }
            }
        }

        public static void Show(T[,] matrix) {
            StringBuilder sb = new StringBuilder();
            int n = matrix.GetLength(0);
            int m = matrix.GetLength(1);
            for (int i = 0; i < n; i++) {
                for (int j = 0; j < m; j++) {
                    sb.Append(matrix[i, j]).Append(" ");
                }
                sb.Append("\n");
            }
            Console.WriteLine(sb.ToString());
        }

        public static string ToString(T[,] matrix) {
            StringBuilder sb = new StringBuilder();
            int n = matrix.GetLength(0);
            int m = matrix.GetLength(1);
            for (int i = 0; i < n; i++) {
                for (int j = 0; j < m; j++) {
                    sb.Append(matrix[i, j]).Append(" ");
                }
                sb.Append("\n");
            }
            return sb.ToString();
        }

		public static Complex[,] BuildRadamacher(ExpansionBase expBase, ParyX[] intervals, int k) {
			int N = intervals.Length;
			Complex[,] matrix = new Complex[N, N];
			RademacherFunction r = new RademacherFunction(expBase, k);
			for (int i = 0; i < N; i++) {
				for (int j = 0; j < N; j++) {
					matrix[i, j] = r.Value(intervals[i], j);
				}
			}
			return matrix;
		}

		public static void ShowMatrix<T>(T[,] matrix) {
			int N = matrix.GetLength(0);
			int M = matrix.GetLength(1);
			StringBuilder sb = new StringBuilder();
			for (int i = 0; i < N; i++) {
				for (int j = 0; j < M; j++) {
					sb.Append("[").Append(matrix[i, j]).Append("] ");
				}
				sb.AppendLine();
			}
			Console.Write(sb.ToString());
		}

		public static Complex[,] BuildVilenkin(ExpansionBase expBase, ParyX[] intervals) {
			int N = intervals.Length;
			Complex[,] matrix = new Complex[N, N];
			VilenkinFunction[] ws = new VilenkinFunction[N];
			for (int i = 0; i < N; i++) {
				ws[i] = new VilenkinFunction(expBase, new ParyN(expBase, i));
			}
			for (int i = 0; i < N; i++) {
				for (int j = 0; j < N; j++) {
					matrix[i, j] = ws[j].Value(intervals[i]);
				}
			}
			return matrix;
		}

		public static Complex[,] Multiply(Complex[,] a, Complex[,] b) {
			int aN = a.GetLength(0);
			int aM = a.GetLength(1);
			int bN = b.GetLength(0);
			int bM = b.GetLength(1);
			if (aM != bN) {
				throw new System.ArgumentException(String.Format("Не совпадают размерности матриц A.m = {0} B.n = {1}", aM, bN));
			}
			Complex[,] result = new Complex[aN, bM];
			for (int i = 0; i < aN; i++) {
				for (int j = 0; j < bM; j++) {
					result[i, j] = new Complex(0, 0);
					for (int k = 0; k < aM; k++) {
						result[i, j] += a[i, k] * b[k, j];
					}
				}
			}
			return result;
		}

		public static void Multiply(Complex[,] a, Complex n) {
			int aN = a.GetLength(0);
			int aM = a.GetLength(1);
			for (int i = 0; i < aN; i++) {
				for (int j = 0; j < aM; j++) {
					a[i, j] *= n;
				}
			}
		}

		public static Complex[,] Conjugate(Complex[,] a) {
			int aN = a.GetLength(0);
			int aM = a.GetLength(1);
			Complex[,] result = new Complex[aN, aM];
			for (int i = 0; i < aN; i++) {
				for (int j = 0; j < aM; j++) {
					result[i, j] = a[i, j].Conjugate();
				}
			}
			return result;
		}


		public static Complex[,] ToMatrix(Complex[] vector) {
			Complex[,] matrix = new Complex[vector.Length, 1];
			for (int i = 0; i < vector.Length; i++) {
				matrix[i, 0] = vector[i];
			}
			return matrix;
		}

		public static void InsertVectorToMatrix(Complex[,] matrix, Complex[] vector, int column) {
			int n = matrix.GetLength(0);
			int m = matrix.GetLength(1);
			int l = vector.Length;
			if (column > n - 1 || column < 0 || l > m) {
				throw new System.ArgumentException("Неудается вставить вектор в матрицу так так не совпадают размерности");
			}
			for (int i = 0; i < l; i++) {
				matrix[column, i] = vector[i];
			}
		}

		public static Complex[,] ColumnAsMatrix(Complex[,] matrix, int column) {
			Complex[,] result = new Complex[matrix.GetLength(0), 1];
			for (int i = 0; i < matrix.GetLength(0); i++) {
				result[i, 0] = matrix[i, column];
			}
			return result;
		}

		public static Complex[,] ToComplexMatrix(int[,] matrix) {
			int w = matrix.GetLength(0);
			int h = matrix.GetLength(1);
			Complex[,] result = new Complex[w, h];
			for (var i = 0; i < w; i++) {
				for (var j = 0; j < h; j++) {
					result[i, j] = new Complex(matrix[i, j], 0);
				}
			}
			return result;
		}

		public static Complex ZERO = new Complex(0, 0);

		public static int ZeroByPercent(Complex[,] matrix, int percent) {
			int w = matrix.GetLength(0);
			int h = matrix.GetLength(1);
			List<KeyValuePair<Complex, KeyValuePair<int, int>>> elements = new List<KeyValuePair<Complex, KeyValuePair<int, int>>>(w * h);
			for (int i = 0; i < w; i++) {
				for (int j = 0; j < h; j++) {
					elements.Add(new KeyValuePair<Complex, KeyValuePair<int, int>>(matrix[i, j], new KeyValuePair<int, int>(i, j)));
				}
			}
			int countToZero = (int)(((double)percent) / 100 * elements.Count);
			elements.Sort(delegate (KeyValuePair<Complex, KeyValuePair<int, int>> p1, KeyValuePair<Complex, KeyValuePair<int, int>> p2) { return p1.Key.CompareTo(p2.Key);});
			KeyValuePair<Complex, KeyValuePair<int, int>> temp;
			for (int i = 0; i < countToZero; i++) {
				temp = elements.ElementAt(i);
				matrix[temp.Value.Key, temp.Value.Value] = ZERO;
			}
			return countToZero;
		}

		public static int ZeroByPercent(float[,] matrix, int percent) {
			int w = matrix.GetLength(0);
			int h = matrix.GetLength(1);
			List<KeyValuePair<float, KeyValuePair<int, int>>> elements = new List<KeyValuePair<float, KeyValuePair<int, int>>>(w * h);
			for (int i = 0; i < w; i++) {
				for (int j = 0; j < h; j++) {
					elements.Add(new KeyValuePair<float, KeyValuePair<int, int>>(matrix[i, j], new KeyValuePair<int, int>(i, j)));
				}
			}
			int countToZero = (int)(((double)percent) / 100 * elements.Count);
			elements.Sort(delegate (KeyValuePair<float, KeyValuePair<int, int>> p1, KeyValuePair<float, KeyValuePair<int, int>> p2) { return Math.Abs(p1.Key).CompareTo(Math.Abs(p2.Key)); });
			KeyValuePair<float, KeyValuePair<int, int>> temp;
			for (int i = 0; i < countToZero; i++) {
				temp = elements.ElementAt(i);
				matrix[temp.Value.Key, temp.Value.Value] = 0f;
			}
			return countToZero;
		}

		public static int CountZeros(float[,] matrix) {
			int zeros = 0;
			int w = matrix.GetLength(0);
			int h = matrix.GetLength(1);
			for (int i = 0; i < w; i++) {
				for (int j = 0; j < h; j++) {
					if (matrix[i, j] == 0f) {
						zeros++;
					}
				}
			}
			return zeros;
		}

		public static int[,] Assemble(ImageBlock[,] imageBlocks) {
			int w = imageBlocks[0, 0].Width;
			int h = imageBlocks[0, 0].Height;
			int[,] res = new int[2 * w, 2 * h];
			int[,] block;
			for (int i = 0; i < 2; i++) {
				for (int j = 0; j < 2; j++) {
					block = imageBlocks[i, j].Data;
					for (int k = 0; k < w; k++) {
						for (int t = 0; t < h; t++) {
							res[k + w * i, t + h * j] = block[k, t];
						}
					}
				}
			}
			return res;
		}
	}
}

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
    }
}

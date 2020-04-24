using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPEGAlgorithm {
	class ArrayUtils {
		public static void Fill(List<int> array, int value, int amount) {
			for (int i = 0; i < amount; i++) {
				array.Add(value);
			}
		}

		public static string ArrayToString<T>(List<T> array, string elementName) {
			StringBuilder builder = new StringBuilder();
			for (int i = 0; i < array.Count; i++) {
				builder.Append(elementName).Append("[").Append(i).Append("]=").Append(array.ElementAt(i));
				if (i != array.Count - 1) {
					builder.Append(", ");
				} else {
					builder.Append(";");
				}
			}
			builder.AppendLine();
			return builder.ToString();
		}

		public static string ArrayToString<T>(T[] array, string elementName) {
			StringBuilder builder = new StringBuilder();
			for (int i = 0; i < array.Length; i++) {
				builder.Append(elementName).Append("[").Append(i).Append("]=").Append(array[i]);
				if (i != array.Length - 1) {
					builder.Append(", ");
				} else {
					builder.Append(";");
				}
			}
			builder.AppendLine();
			return builder.ToString();
		}

		public static T[] SubArray<T>(T[] array, int start, int len) {
			T[] subArray = new T[len];
			int curr = start;
			for (int i = 0; i < len; i++) {
				subArray[i] = array[curr++];
			}
			return subArray;
		}

		public static Complex[] ToArray(Complex[,] matrix) {
			int n = matrix.GetLength(0);
			int m = matrix.GetLength(1);
			if (m != 1) {
				throw new System.ArgumentException("Matrix is not vector");
			}
			Complex[] vector = new Complex[n];
			for (int i = 0; i < n; i++) {
				vector[i] = matrix[i, 0];
			}
			return vector;
		}

		public static void AddAll(Complex[] array, List<Complex> list) {
			for (int i = 0; i < array.Length; i++) {
				list.Add(array[i]);
			}
		}

		public static void AddArrayAndFill(Complex[] source, Complex[] dest, Complex value) {
			for (int i = 0; i < dest.Length; i++) {
				if (i < source.Length) {
					dest[i] = source[i];
				} else {
					dest[i] = value;
				}
			}
		}
	}
}

using JPEGAlgorithm.utils;
using JPEGAlgorithm.vilenklin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPEGAlgorithm {
	class VilenkinTransform {

		private ExpansionBase expansionBase;

		private readonly int blockSize;

		private readonly int K;

		private ParyX[] xs;

		private int P;

		private Complex N;

		private List<RademaxerMatrix> rademacherMatrixes;

		private ParyX[] vilenkinXs;

		private Complex[,] conjugatedWMatrix;

		private Dictionary<int, VilenkinFunction> vilenkinFunctions;

		public ExpansionBase GetExpansionBase() {
			return expansionBase;
		}

		public int GetBlockSize() {
			return blockSize;
		}

		/*public VilenkinTransform(List<int> p, int blockSize) {
			this.expansionBase = new ExpansionBase(p);
			this.blockSize = blockSize;
			this.K = CountK();
			xs = ConstIntervalsUtils.Intervals(expansionBase, K);
			this.P = expansionBase.Pk(K);
			ParyX[] subXs;
			this.N = new Complex(1d / p.ElementAt(K), 0);
			rademacherMatrixes = new List<RademaxerMatrix>();
			for (int i = 0; i < expansionBase.Mk(K); i++) {
				subXs = ArrayUtils.SubArray(xs, i * P, P);
				rademacherMatrixes.Add(new RademaxerMatrix(expansionBase, subXs, K, N));
			}
			vilenkinXs = ConstIntervalsUtils.Intervals(expansionBase, K - 1);
			conjugatedWMatrix = MatrixUtils<Complex>.Conjugate(MatrixUtils<Complex>.BuildVilenkin(expansionBase, vilenkinXs));
			MatrixUtils<Complex>.Multiply(conjugatedWMatrix, new Complex(1d / expansionBase.Mk(K), 0));
			vilenkinFunctions = new Dictionary<int, VilenkinFunction>();
			for (int i = 0; i < expansionBase.Mk(K + 1); i++) {
				vilenkinFunctions.Add(i, new VilenkinFunction(expansionBase, new ParyN(expansionBase, i)));
			}
		}*/

		public VilenkinTransform(List<int> p, int K) {
			this.expansionBase = new ExpansionBase(p);
			this.K = K;
			xs = ConstIntervalsUtils.Intervals(expansionBase, K);
			this.blockSize = expansionBase.Mk(K + 1);
			this.P = expansionBase.Pk(K);
			ParyX[] subXs;
			this.N = new Complex(1d / p.ElementAt(K), 0);
			rademacherMatrixes = new List<RademaxerMatrix>();
			for (int i = 0; i < expansionBase.Mk(K); i++) {
				subXs = ArrayUtils.SubArray(xs, i * P, P);
				rademacherMatrixes.Add(new RademaxerMatrix(expansionBase, subXs, K, N));
			}
			vilenkinXs = ConstIntervalsUtils.Intervals(expansionBase, K - 1);
			conjugatedWMatrix = MatrixUtils<Complex>.Conjugate(MatrixUtils<Complex>.BuildVilenkin(expansionBase, vilenkinXs));
			MatrixUtils<Complex>.Multiply(conjugatedWMatrix, new Complex(1d / expansionBase.Mk(K), 0));
			vilenkinFunctions = new Dictionary<int, VilenkinFunction>();
			for (int i = 0; i < expansionBase.Mk(K + 1); i++) {
				vilenkinFunctions.Add(i, new VilenkinFunction(expansionBase, new ParyN(expansionBase, i)));
			}
		}

		private int CountK() {
			for (int i = 1; i < expansionBase.getMSize(); i++) {
				if (expansionBase.Mk(i) >= blockSize) {
					return i - 1;
				}
			}
			throw new ArgumentException("Размер блока не соотносится ни с одним разбиением, размер блока = " + blockSize 
				+ " максимальный размер разбиения = " + expansionBase.Mk(expansionBase.getMSize() - 1));
		}
	
		public Complex[] Transform(Complex[] function) {
			if (function.Length < expansionBase.Mk(K + 1)) {
				Complex[] expansedFunction = new Complex[expansionBase.Mk(K + 1)];
				ArrayUtils.AddArrayAndFill(function, expansedFunction, new Complex(0, 0));
				function = expansedFunction;
			} else if (function.Length > expansionBase.Mk(K + 1)){
				throw new ArgumentException("Кол-во значений функции превышает кол-ва точек в разбиении");
			}
			Complex[,] B;
			Complex[] X;
			Complex[,] XMatrix;
			Complex[,] FkValues = new Complex[expansionBase.Mk(K), P];
			for (int i = 0; i < expansionBase.Mk(K); i++) {
				B = MatrixUtils<Complex>.ToMatrix(ArrayUtils.SubArray(function, i * P, P));
				XMatrix = MatrixUtils<Complex>.Multiply(rademacherMatrixes.ElementAt(i).getConjugatedMatrix(), B);
				X = ArrayUtils.ToArray(XMatrix);
				MatrixUtils<Complex>.InsertVectorToMatrix(FkValues, X, i);
			}
			List<Complex> coeffs = new List<Complex>();
			Complex[,] C;
			for (int i = 0; i < P; i++) {
				B = MatrixUtils<Complex>.ColumnAsMatrix(FkValues, i);
				C = MatrixUtils<Complex>.Multiply(conjugatedWMatrix, B);
				ArrayUtils.AddAll(ArrayUtils.ToArray(C), coeffs);
			}
			return coeffs.ToArray();
		}

		public Complex[] ReverseTransform(Complex[] coeffs) {
			Complex[] result = new Complex[coeffs.Length];
			for(int i = 0; i < xs.Length; i++) {
				result[i] = Value(xs[i], coeffs);
			}
			return result;
		}

		public Complex[,] ReverseTransform(Complex[,] matrix) {
			var N = matrix.GetLength(0);
			var result = new Complex[N, N];
			var vector = new Complex[N];
			for (var i = 0; i < N; i++) {
				for (var j = 0; j < N; j++) {
					vector[j] = matrix[j, i];
				}
				vector = ReverseTransform(vector);
				for (var j = 0; j < N; j++) {
					result[j, i] = vector[j];
				}
			}
			for (var i = 0; i < N; i++) {
				for (var j = 0; j < N; j++) {
					vector[j] = result[i, j];
				}
				vector = ReverseTransform(vector);
				for (var j = 0; j < N; j++) {
					result[i, j] = vector[j];
				}
			}
			return MathUtils.RoundComplexMatrix(result);
		}

		public Complex Value(ParyX x, Complex[] coeffs) {
			Complex value = MatrixUtils<Complex>.ZERO;
			for (int i = 0; i < coeffs.Length; i++) {
				if (coeffs[i] == MatrixUtils<Complex>.ZERO) {
					continue;
				}
				value += coeffs[i] * vilenkinFunctions[i].Value(x);
			}
			return value;
		}

		public Complex[,] Transform(ImageBlock block) {
			var matrix = MatrixUtils<Complex>.ToComplexMatrix(block.Data);
			var N = matrix.GetLength(0);
			var result = new Complex[N, N];
			var vector = new Complex[N];
			for (var i = 0; i < N; i++) {
				for (var j = 0; j < N; j++) {
					vector[j] = matrix[i, j];
				}
				vector = Transform(vector);
				for (var j = 0; j < N; j++) {
					result[i, j] = vector[j];
				}
			}
			for (var i = 0; i < N; i++) {
				for (var j = 0; j < N; j++) {
					vector[j] = result[j, i];
				}
				vector = Transform(vector);
				for (var j = 0; j < N; j++) {
					result[j, i] = vector[j];
				}
			}
			return result;
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPEGAlgorithm.vilenklin {
	class RademaxerMatrix {
		private Complex[,] matrix;

		private Complex[,] conjugatedMatrix;

		public RademaxerMatrix(ExpansionBase expansionBase, ParyX[] xs, int K, Complex N) {
			matrix = MatrixUtils<Complex[,]>.BuildRadamacher(expansionBase, xs, K);
			conjugatedMatrix = MatrixUtils<Complex[,]>.Conjugate(matrix);
			MatrixUtils<Complex>.Multiply(conjugatedMatrix, N);
		}

		public Complex[,] getConjugatedMatrix() {
			return conjugatedMatrix;
		}
	}
}

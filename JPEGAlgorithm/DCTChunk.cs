using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPEGAlgorithm {
	class DCTChunk {
		private float[][,] y;

		private float[,] cb;

		private float[,] cr;

		private int zeroCoeffs;

		public DCTChunk(Chunk chunk) {
			y = new float[Chunk.BLOCKS_COUNT][,];
			for (int i = 0; i < Chunk.BLOCKS_COUNT; i++) {
				Y[i] = TransformsUtils.DCT_1(chunk.Y[i]);
			}
			cb = TransformsUtils.DCT_1(chunk.Cb);
			cr = TransformsUtils.DCT_1(chunk.Cr);
		}

		public float[][,] Y { get => y; }
		public float[,] Cb { get => cb; }
		public float[,] Cr { get => cr; }

		public override string ToString() {
			StringBuilder sb = new StringBuilder();
			for (int i = 0; i < Chunk.BLOCKS_COUNT; i++) {
				sb.Append("DCT coeffs of Y" + i).AppendLine().Append(MatrixUtils<float>.ToString(y[i])).AppendLine();
			}
			sb.Append("DCT coeffs of Cb").AppendLine().Append(MatrixUtils<float>.ToString(cb)).AppendLine();
			sb.Append("DCT coeffs of Cr").AppendLine().Append(MatrixUtils<float>.ToString(cr)).AppendLine();
			return sb.ToString();
		}

		public void QuantizeY(float[,] quantizeMatrix) {
			for (int i = 0; i < Chunk.BLOCKS_COUNT; i++) {
				MathUtils.Quantize(Y[i], quantizeMatrix);
				MathUtils.RoundMatrix(Y[i]);
				zeroCoeffs += MatrixUtils<int>.CountZeros(Y[i]);
			}
		}

		public void ZeroCoeffs(int percentY, int percentCb, int percentCr) {
			for (int i = 0; i < Chunk.BLOCKS_COUNT; i++) {
				zeroCoeffs += MatrixUtils<Complex>.ZeroByPercent(y[i], percentY);
			}
			zeroCoeffs += MatrixUtils<Complex>.ZeroByPercent(cb, percentCb);
			zeroCoeffs += MatrixUtils<Complex>.ZeroByPercent(cr, percentCr);
		}

		public void QuantizeCbCr(float[,] quantizeMatrix) {
			MathUtils.Quantize(cb, quantizeMatrix);
			MathUtils.RoundMatrix(cb);
			zeroCoeffs += MatrixUtils<int>.CountZeros(cb);
			MathUtils.Quantize(cr, quantizeMatrix);
			MathUtils.RoundMatrix(cr);
			zeroCoeffs += MatrixUtils<int>.CountZeros(cr);
		}

		public void DequantizeY(float[,] quantizeMatrix) {
			for (int i = 0; i < Chunk.BLOCKS_COUNT; i++) {
				MathUtils.Dequantize(Y[i], quantizeMatrix);
				MathUtils.RoundMatrix(Y[i]);
			}
		}

		public void DequantizeCbCr(float[,] quantizeMatrix) {
			MathUtils.Dequantize(cb, quantizeMatrix);
			MathUtils.RoundMatrix(cb);
			MathUtils.Dequantize(cr, quantizeMatrix);
			MathUtils.RoundMatrix(cr);
		}


		public int[] getDCCoeffs() {
			int[] coeffs = new int[6];
			for (int i = 0; i < Chunk.BLOCKS_COUNT; i++) {
				coeffs[i] = (int)y[i][0, 0];
			}
			coeffs[4] = (int)cb[0, 0];
			coeffs[5] = (int)cr[0, 0];
			return coeffs;
		}

		public int[] getACCoeffs() {
			var coeffs = new int[6 * y[0].Length * y[0].Length];
			int t = 0;
			for (int i = 0; i < Chunk.BLOCKS_COUNT; i++) {
				for (int k = 0; k < y[i].Length; k++) {
					for (int j = 0; j < y[i].Length; j++) {
						if(k == 0 && j == 0){
							continue;
						}
						coeffs[t++] = (int)y[i][k, j];
					}
				}
			}
			for (int k = 0; k < cb.Length; k++) {
				for (int j = 0; j < cb.Length; j++) {
					if (k == 0 && j == 0) {
						continue;
					}
					coeffs[t++] = (int)cb[k, j];
				}
			}
			for (int k = 0; k < cr.Length; k++) {
				for (int j = 0; j < cr.Length; j++) {
					if (k == 0 && j == 0) {
						continue;
					}
					coeffs[t++] = (int)cr[k, j];
				}
			}
			return coeffs;
		}

		public int CoeffsCount() {
			return y.Length * (int)Math.Pow(y[0].GetLength(0), 2) + (int)Math.Pow(cb.GetLength(0), 2) + (int)Math.Pow(cr.GetLength(0), 2);
		}

		public int ZeroCoeffsCount() {
			return zeroCoeffs;
		}
	}
}

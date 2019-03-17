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

		public DCTChunk(Chunk chunk) {
			y = new float[Chunk.BLOCKS_COUNT][,];
			for (int i = 0; i < Chunk.BLOCKS_COUNT; i++) {
				Y[i] = TransformsUtils.DCT(chunk.Y[i]);
			}
			cb = TransformsUtils.DCT(chunk.Cb);
			cr = TransformsUtils.DCT(chunk.Cr);
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

		public void Quantize(double q, float[,] quantizeMatrix) {
			for (int i = 0; i < Chunk.BLOCKS_COUNT; i++) {
				//MathUtils.RoundMatrix(Y[i]);
				MathUtils.Quantize(Y[i], quantizeMatrix);
				MathUtils.RoundMatrix(Y[i]);
			}
			//MathUtils.RoundMatrix(cb);
			MathUtils.Quantize(cb, quantizeMatrix);
			MathUtils.RoundMatrix(cb);
			//MathUtils.RoundMatrix(cr);
			MathUtils.Quantize(cr, quantizeMatrix);
			MathUtils.RoundMatrix(cr);
		}

		public void Dequantize(double q, float[,] quantizeMatrix) {
			for (int i = 0; i < Chunk.BLOCKS_COUNT; i++) {
				MathUtils.Dequantize(Y[i], quantizeMatrix);
				MathUtils.RoundMatrix(Y[i]);
			}
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
			var coeffs = new int[6 * 63];
			int t = 0;
			for (int i = 0; i < Chunk.BLOCKS_COUNT; i++) {
				for (int k = 0; k < Chunk.BLOCK_SIZE; k++) {
					for (int j = 0; j < Chunk.BLOCK_SIZE; j++) {
						if(k == 0 && j == 0){
							continue;
						}
						coeffs[t++] = (int)y[i][k, j];
					}
				}
			}
			for (int k = 0; k < Chunk.BLOCK_SIZE; k++) {
				for (int j = 0; j < Chunk.BLOCK_SIZE; j++) {
					if (k == 0 && j == 0) {
						continue;
					}
					coeffs[t++] = (int)cb[k, j];
				}
			}
			for (int k = 0; k < Chunk.BLOCK_SIZE; k++) {
				for (int j = 0; j < Chunk.BLOCK_SIZE; j++) {
					if (k == 0 && j == 0) {
						continue;
					}
					coeffs[t++] = (int)cr[k, j];
				}
			}
			return coeffs;
		}
	}
}

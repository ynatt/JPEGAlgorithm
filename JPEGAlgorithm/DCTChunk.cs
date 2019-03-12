﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPEGAlgorithm {
	class DCTChunk {
		private double[][,] y;

		private double[,] cb;

		private double[,] cr;

		public DCTChunk(Chunk chunk) {
			y = new double[Chunk.BLOCKS_COUNT][,];
			for (int i = 0; i < Chunk.BLOCKS_COUNT; i++) {
				Y[i] = TransformsUtils.DCT(chunk.Y[i]);
			}
			cb = TransformsUtils.DCT(chunk.Cb);
			cr = TransformsUtils.DCT(chunk.Cr);
		}

		public double[][,] Y { get => y; }
		public double[,] Cb { get => cb; }
		public double[,] Cr { get => cr; }

		public override string ToString() {
			StringBuilder sb = new StringBuilder();
			for (int i = 0; i < Chunk.BLOCKS_COUNT; i++) {
				sb.Append("DCT coeffs of Y" + i).AppendLine().Append(MatrixUtils<double>.ToString(y[i])).AppendLine();
			}
			sb.Append("DCT coeffs of Cb").AppendLine().Append(MatrixUtils<double>.ToString(cb)).AppendLine();
			sb.Append("DCT coeffs of Cr").AppendLine().Append(MatrixUtils<double>.ToString(cr)).AppendLine();
			return sb.ToString();
		}

		public void Quantize(double q) {
			double[,] quantizeMatrix = MathUtils.BuildQuantizationMatrix(q, Chunk.BLOCK_SIZE);
			for (int i = 0; i < Chunk.BLOCKS_COUNT; i++) {
				MathUtils.Quantize(Y[i], quantizeMatrix);
				MathUtils.RoundMatrix(Y[i]);
			}
			MathUtils.Quantize(cb, quantizeMatrix);
			MathUtils.RoundMatrix(cb);
			MathUtils.Quantize(cr, quantizeMatrix);
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
			/*for (int i = 0; i < Chunk.BLOCKS_COUNT; i++) {
				for (int k = 1; k < Chunk.BLOCK_SIZE; k++) {
					for (int j = 1; j < Chunk.BLOCK_SIZE; j++) {
						coeffs[i * 63 + k * Chunk.BLOCK_SIZE + j] = (int)y[i][k, j];
					}
				}
				for (var k = 0; k < Chunk.BLOCK_SIZE - 1; k++) {
					coeffs[i * 63 + k * Chunk.BLOCK_SIZE + 0] = (int)y[i][k + 1, 0];
				}
				for (var j = 0; j < Chunk.BLOCK_SIZE - 1; j++) {
					coeffs[i * 63 + 0 * Chunk.BLOCK_SIZE + j] = (int)y[i][0, j + 1];
				}
			}*/
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

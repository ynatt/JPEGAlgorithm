using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPEGAlgorithm {
	class HaarChunk {
		private Complex[][,] y;

		private Complex[][,] cb;

		private Complex[][,] cr;

		private int zeroCoeffs = 0;

		public HaarChunk(Chunk chunk, int p) {
			y = new Complex[Chunk.BLOCKS_COUNT][,];
			cb = new Complex[Chunk.BLOCKS_COUNT][,];
			cr = new Complex[Chunk.BLOCKS_COUNT][,];
			for (int i = 0; i < Chunk.BLOCKS_COUNT; i++) {
				Y[i] = TransformsUtils.HaarTransform(chunk.Y[i], p);
			}
			if (MainForm.isAveragingEnabled) {
				cb[0] = TransformsUtils.HaarTransform(chunk.Cb[0], p);
				cr[0] = TransformsUtils.HaarTransform(chunk.Cr[0], p);
			} else {
				for (int i = 0; i < Chunk.BLOCKS_COUNT; i++) {
					cb[i] = TransformsUtils.HaarTransform(chunk.Cb[i], p);
					cr[i] = TransformsUtils.HaarTransform(chunk.Cr[i], p);
				}
			}
		}

		public Complex[][,] Y { get => y; }
		public Complex[][,] Cb { get => cb; }
		public Complex[][,] Cr { get => cr; }

		public void ZeroCoeffs(int percentY, int percentCb, int percentCr) {
			for (int i = 0; i < Chunk.BLOCKS_COUNT; i++) {
				zeroCoeffs+=MatrixUtils<Complex>.ZeroByPercent(y[i], percentY);
			}
			if (MainForm.isAveragingEnabled) {
				zeroCoeffs += Chunk.BLOCKS_COUNT * MatrixUtils<Complex>.ZeroByPercent(cb[0], percentCb);
				zeroCoeffs += Chunk.BLOCKS_COUNT * MatrixUtils<Complex>.ZeroByPercent(cr[0], percentCr);
			} else {
				for (int i = 0; i < Chunk.BLOCKS_COUNT; i++) {
					zeroCoeffs += MatrixUtils<Complex>.ZeroByPercent(cb[i], percentCb);
					zeroCoeffs += MatrixUtils<Complex>.ZeroByPercent(cr[i], percentCr);
				}
			}
		}

		public int CoeffsCount() {
			return y.Length * ((int) Math.Pow(y[0].GetLength(0), 2) + (int) Math.Pow(cb[0].GetLength(0), 2) + (int) Math.Pow(cr[0].GetLength(0), 2));
		}

		public int ZeroCoeffsCount() {
			return zeroCoeffs;
		}
	}
}

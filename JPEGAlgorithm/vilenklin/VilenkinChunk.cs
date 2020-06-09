using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPEGAlgorithm.vilenklin {
	class VilenkinChunk {
		private Complex[][,] y;
		private Complex[][,] cb;
		private Complex[][,] cr;

		private int zeroCoeffs = 0;

		public VilenkinChunk(VilenkinTransform vilenkin, Chunk chunk) {
			Y = new Complex[Chunk.BLOCKS_COUNT][,];
			cb = new Complex[Chunk.BLOCKS_COUNT][,];
			cr = new Complex[Chunk.BLOCKS_COUNT][,];
			for (int i = 0; i < Chunk.BLOCKS_COUNT; i++) {
				Y[i] = vilenkin.Transform(chunk.Y[i]);
			}
			if (MainForm.isAveragingEnabled) {
				Cb[0] = vilenkin.Transform(chunk.Cb[0]);
				Cr[0] = vilenkin.Transform(chunk.Cr[0]);
			} else {
				for (int i = 0; i < Chunk.BLOCKS_COUNT; i++) {
					cb[i] = vilenkin.Transform(chunk.Cb[i]);
					cr[i] = vilenkin.Transform(chunk.Cr[i]);
				}
			}
		}

		public Complex[][,] Cr { get => cr; set => cr = value; }
		public Complex[][,] Cb { get => cb; set => cb = value; }
		public Complex[][,] Y { get => y; set => y = value; }

		public void ZeroCoeffs(int percentY, int percentCb, int percentCr) {
			for (int i = 0; i < Chunk.BLOCKS_COUNT; i++) {
				zeroCoeffs += MatrixUtils<Complex>.ZeroByPercent(y[i], percentY);
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
			return y.Length * ((int)Math.Pow(y[0].GetLength(0), 2) + (int)Math.Pow(cb[0].GetLength(0), 2) + (int)Math.Pow(cr[0].GetLength(0), 2));
		}

		public int ZeroCoeffsCount() {
			return zeroCoeffs;
		}
	}
}

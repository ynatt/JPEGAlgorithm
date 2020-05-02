using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPEGAlgorithm.vilenklin {
	class VilenkinChunk {
		private Complex[][,] y;
		private Complex[,] cb;
		private Complex[,] cr;

		private int zeroCoeffs = 0;

		public VilenkinChunk(VilenkinTransform vilenkin, Chunk chunk) {
			Y = new Complex[Chunk.BLOCKS_COUNT][,];
			for (int i = 0; i < Chunk.BLOCKS_COUNT; i++) {
				Y[i] = vilenkin.Transform(chunk.Y[i]);
			}
			Cb = vilenkin.Transform(chunk.Cb);
			Cr = vilenkin.Transform(chunk.Cr);
		}

		public Complex[,] Cr { get => cr; set => cr = value; }
		public Complex[,] Cb { get => cb; set => cb = value; }
		public Complex[][,] Y { get => y; set => y = value; }

		public void ZeroCoeffs(int percentY, int percentCb, int percentCr) {
			for (int i = 0; i < Chunk.BLOCKS_COUNT; i++) {
				zeroCoeffs += MatrixUtils<Complex>.ZeroByPercent(y[i], percentY);
			}
			zeroCoeffs += MatrixUtils<Complex>.ZeroByPercent(cb, percentCb);
			zeroCoeffs += MatrixUtils<Complex>.ZeroByPercent(cr, percentCr);
		}

		public int CoeffsCount() {
			return y.Length * (int)Math.Pow(y[0].GetLength(0), 2) + (int)Math.Pow(cb.GetLength(0), 2) + (int)Math.Pow(cr.GetLength(0), 2);
		}

		public int ZeroCoeffsCount() {
			return zeroCoeffs;
		}
	}
}

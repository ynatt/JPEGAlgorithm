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

		public void ZeroCoeffs(int percent) {
			for (int i = 0; i < Chunk.BLOCKS_COUNT; i++) {
				MatrixUtils<Complex>.ZeroByPercent(y[i], percent);
			}
			MatrixUtils<Complex>.ZeroByPercent(cb, percent);
			MatrixUtils<Complex>.ZeroByPercent(cr, percent);
		}
	}
}

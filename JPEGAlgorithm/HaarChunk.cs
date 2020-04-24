using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPEGAlgorithm {
	class HaarChunk {
		private float[][,] y;

		private float[,] cb;

		private float[,] cr;

		public HaarChunk(Chunk chunk) {
			y = new float[Chunk.BLOCKS_COUNT][,];
			for (int i = 0; i < Chunk.BLOCKS_COUNT; i++) {
				Y[i] = TransformsUtils.HaarTransform(chunk.Y[i]);
			}
			cb = TransformsUtils.HaarTransform(chunk.Cb);
			cr = TransformsUtils.HaarTransform(chunk.Cr);
		}

		public float[][,] Y { get => y; }
		public float[,] Cb { get => cb; }
		public float[,] Cr { get => cr; }

		public void ZeroCoeffs(int percent) {
			for (int i = 0; i < Chunk.BLOCKS_COUNT; i++) {
				MatrixUtils<Complex>.ZeroByPercent(y[i], percent);
			}
			MatrixUtils<Complex>.ZeroByPercent(cb, percent);
			MatrixUtils<Complex>.ZeroByPercent(cr, percent);
		}
	}
}

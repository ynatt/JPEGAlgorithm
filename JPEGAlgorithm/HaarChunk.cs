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

		private int zeroCoeffs = 0;

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

		public void ZeroCoeffs(int percentY, int percentCb, int percentCr) {
			for (int i = 0; i < Chunk.BLOCKS_COUNT; i++) {
				zeroCoeffs+=MatrixUtils<Complex>.ZeroByPercent(y[i], percentY);
			}
			zeroCoeffs+=MatrixUtils<Complex>.ZeroByPercent(cb, percentCb);
			zeroCoeffs+=MatrixUtils<Complex>.ZeroByPercent(cr, percentCr);
		}

		public int CoeffsCount() {
			return y.Length * (int) Math.Pow(y[0].GetLength(0), 2) + (int) Math.Pow(cb.GetLength(0), 2) + (int) Math.Pow(cr.GetLength(0), 2);
		}

		public int ZeroCoeffsCount() {
			return zeroCoeffs;
		}
	}
}

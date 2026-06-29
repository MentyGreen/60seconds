using System;
using UnityEngine;

namespace RG_GameCamera.Effects
{
	// Token: 0x020001B9 RID: 441
	public class SmoothRandom
	{
		// Token: 0x060012AF RID: 4783 RVA: 0x00050A64 File Offset: 0x0004EC64
		public static Vector3 GetVector3(float speed)
		{
			float x = Time.time * 0.01f * speed;
			return new Vector3(SmoothRandom.Get().HybridMultifractal(x, 15.73f, 0f), SmoothRandom.Get().HybridMultifractal(x, 63.94f, 0f), SmoothRandom.Get().HybridMultifractal(x, 0.2f, 0f));
		}

		// Token: 0x060012B0 RID: 4784 RVA: 0x00050AC4 File Offset: 0x0004ECC4
		public static float Get(float speed)
		{
			float num = Time.time * 0.01f * speed;
			return SmoothRandom.Get().HybridMultifractal(num * 0.01f, 15.7f, 0.65f);
		}

		// Token: 0x060012B1 RID: 4785 RVA: 0x00050AFA File Offset: 0x0004ECFA
		private static FractalNoise Get()
		{
			FractalNoise result;
			if ((result = SmoothRandom.s_Noise) == null)
			{
				result = (SmoothRandom.s_Noise = new FractalNoise(1.27f, 2.04f, 8.36f));
			}
			return result;
		}

		// Token: 0x04000C5E RID: 3166
		private static FractalNoise s_Noise;
	}
}

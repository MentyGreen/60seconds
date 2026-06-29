using System;
using UnityEngine;

namespace RG_GameCamera.Effects
{
	// Token: 0x020001BB RID: 443
	public class FractalNoise
	{
		// Token: 0x060012BE RID: 4798 RVA: 0x000512D0 File Offset: 0x0004F4D0
		public FractalNoise(float inH, float inLacunarity, float inOctaves) : this(inH, inLacunarity, inOctaves, null)
		{
		}

		// Token: 0x060012BF RID: 4799 RVA: 0x000512DC File Offset: 0x0004F4DC
		public FractalNoise(float inH, float inLacunarity, float inOctaves, Perlin noise)
		{
			this.m_Lacunarity = inLacunarity;
			this.m_Octaves = inOctaves;
			this.m_IntOctaves = (int)inOctaves;
			this.m_Exponent = new float[this.m_IntOctaves + 1];
			float num = 1f;
			for (int i = 0; i < this.m_IntOctaves + 1; i++)
			{
				this.m_Exponent[i] = (float)Math.Pow((double)this.m_Lacunarity, (double)(-(double)inH));
				num *= this.m_Lacunarity;
			}
			if (noise == null)
			{
				this.m_Noise = new Perlin();
				return;
			}
			this.m_Noise = noise;
		}

		// Token: 0x060012C0 RID: 4800 RVA: 0x0005136C File Offset: 0x0004F56C
		public float HybridMultifractal(float x, float y, float offset)
		{
			float num = (this.m_Noise.Noise(x, y) + offset) * this.m_Exponent[0];
			float num2 = num;
			x *= this.m_Lacunarity;
			y *= this.m_Lacunarity;
			int i;
			for (i = 1; i < this.m_IntOctaves; i++)
			{
				if (num2 > 1f)
				{
					num2 = 1f;
				}
				float num3 = (this.m_Noise.Noise(x, y) + offset) * this.m_Exponent[i];
				num += num2 * num3;
				num2 *= num3;
				x *= this.m_Lacunarity;
				y *= this.m_Lacunarity;
			}
			float num4 = this.m_Octaves - (float)this.m_IntOctaves;
			return num + num4 * this.m_Noise.Noise(x, y) * this.m_Exponent[i];
		}

		// Token: 0x060012C1 RID: 4801 RVA: 0x00051430 File Offset: 0x0004F630
		public float RidgedMultifractal(float x, float y, float offset, float gain)
		{
			float num = Mathf.Abs(this.m_Noise.Noise(x, y));
			num = offset - num;
			num *= num;
			float num2 = num;
			for (int i = 1; i < this.m_IntOctaves; i++)
			{
				x *= this.m_Lacunarity;
				y *= this.m_Lacunarity;
				float num3 = num * gain;
				num3 = Mathf.Clamp01(num3);
				num = Mathf.Abs(this.m_Noise.Noise(x, y));
				num = offset - num;
				num *= num;
				num *= num3;
				num2 += num * this.m_Exponent[i];
			}
			return num2;
		}

		// Token: 0x060012C2 RID: 4802 RVA: 0x000514C0 File Offset: 0x0004F6C0
		public float BrownianMotion(float x, float y)
		{
			float num = 0f;
			long num2;
			for (num2 = 0L; num2 < (long)this.m_IntOctaves; num2 += 1L)
			{
				num = this.m_Noise.Noise(x, y) * this.m_Exponent[(int)(checked((IntPtr)num2))];
				x *= this.m_Lacunarity;
				y *= this.m_Lacunarity;
			}
			float num3 = this.m_Octaves - (float)this.m_IntOctaves;
			return num + num3 * this.m_Noise.Noise(x, y) * this.m_Exponent[(int)(checked((IntPtr)num2))];
		}

		// Token: 0x04000C66 RID: 3174
		private readonly float[] m_Exponent;

		// Token: 0x04000C67 RID: 3175
		private readonly int m_IntOctaves;

		// Token: 0x04000C68 RID: 3176
		private readonly float m_Lacunarity;

		// Token: 0x04000C69 RID: 3177
		private readonly Perlin m_Noise;

		// Token: 0x04000C6A RID: 3178
		private readonly float m_Octaves;
	}
}

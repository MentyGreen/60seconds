using System;

namespace RG_GameCamera.Effects
{
	// Token: 0x020001BA RID: 442
	public class Perlin
	{
		// Token: 0x060012B3 RID: 4787 RVA: 0x00050B28 File Offset: 0x0004ED28
		public Perlin()
		{
			Random random = new Random();
			int i;
			for (i = 0; i < 256; i++)
			{
				this.p[i] = i;
				this.g1[i] = (float)(random.Next(512) - 256) / 256f;
				for (int j = 0; j < 2; j++)
				{
					this.g2[i, j] = (float)(random.Next(512) - 256) / 256f;
				}
				this.normalize2(ref this.g2[i, 0], ref this.g2[i, 1]);
				for (int j = 0; j < 3; j++)
				{
					this.g3[i, j] = (float)(random.Next(512) - 256) / 256f;
				}
				this.normalize3(ref this.g3[i, 0], ref this.g3[i, 1], ref this.g3[i, 2]);
			}
			while (--i != 0)
			{
				int num = this.p[i];
				int j;
				this.p[i] = this.p[j = random.Next(256)];
				this.p[j] = num;
			}
			for (i = 0; i < 258; i++)
			{
				this.p[256 + i] = this.p[i];
				this.g1[256 + i] = this.g1[i];
				for (int j = 0; j < 2; j++)
				{
					this.g2[256 + i, j] = this.g2[i, j];
				}
				for (int j = 0; j < 3; j++)
				{
					this.g3[256 + i, j] = this.g3[i, j];
				}
			}
		}

		// Token: 0x060012B4 RID: 4788 RVA: 0x00050D42 File Offset: 0x0004EF42
		private float s_curve(float t)
		{
			return t * t * (3f - 2f * t);
		}

		// Token: 0x060012B5 RID: 4789 RVA: 0x00050D55 File Offset: 0x0004EF55
		private float lerp(float t, float a, float b)
		{
			return a + t * (b - a);
		}

		// Token: 0x060012B6 RID: 4790 RVA: 0x00050D60 File Offset: 0x0004EF60
		private void setup(float value, out int b0, out int b1, out float r0, out float r1)
		{
			float num = value + 4096f;
			b0 = ((int)num & 255);
			b1 = (b0 + 1 & 255);
			r0 = num - (float)((int)num);
			r1 = r0 - 1f;
		}

		// Token: 0x060012B7 RID: 4791 RVA: 0x00050D9F File Offset: 0x0004EF9F
		private float at2(float rx, float ry, float x, float y)
		{
			return rx * x + ry * y;
		}

		// Token: 0x060012B8 RID: 4792 RVA: 0x00050DA9 File Offset: 0x0004EFA9
		private float at3(float rx, float ry, float rz, float x, float y, float z)
		{
			return rx * x + ry * y + rz * z;
		}

		// Token: 0x060012B9 RID: 4793 RVA: 0x00050DBC File Offset: 0x0004EFBC
		public float Noise(float arg)
		{
			int num;
			int num2;
			float num3;
			float num4;
			this.setup(arg, out num, out num2, out num3, out num4);
			float t = this.s_curve(num3);
			float a = num3 * this.g1[this.p[num]];
			float b = num4 * this.g1[this.p[num2]];
			return this.lerp(t, a, b);
		}

		// Token: 0x060012BA RID: 4794 RVA: 0x00050E14 File Offset: 0x0004F014
		public float Noise(float x, float y)
		{
			int num;
			int num2;
			float num3;
			float rx;
			this.setup(x, out num, out num2, out num3, out rx);
			int num4;
			int num5;
			float num6;
			float ry;
			this.setup(y, out num4, out num5, out num6, out ry);
			int num7 = this.p[num];
			int num8 = this.p[num2];
			int num9 = this.p[num7 + num4];
			int num10 = this.p[num8 + num4];
			int num11 = this.p[num7 + num5];
			int num12 = this.p[num8 + num5];
			float t = this.s_curve(num3);
			float t2 = this.s_curve(num6);
			float a = this.at2(num3, num6, this.g2[num9, 0], this.g2[num9, 1]);
			float b = this.at2(rx, num6, this.g2[num10, 0], this.g2[num10, 1]);
			float a2 = this.lerp(t, a, b);
			a = this.at2(num3, ry, this.g2[num11, 0], this.g2[num11, 1]);
			b = this.at2(rx, ry, this.g2[num12, 0], this.g2[num12, 1]);
			float b2 = this.lerp(t, a, b);
			return this.lerp(t2, a2, b2);
		}

		// Token: 0x060012BB RID: 4795 RVA: 0x00050F64 File Offset: 0x0004F164
		public float Noise(float x, float y, float z)
		{
			int num;
			int num2;
			float num3;
			float rx;
			this.setup(x, out num, out num2, out num3, out rx);
			int num4;
			int num5;
			float num6;
			float ry;
			this.setup(y, out num4, out num5, out num6, out ry);
			int num7;
			int num8;
			float num9;
			float rz;
			this.setup(z, out num7, out num8, out num9, out rz);
			int num10 = this.p[num];
			int num11 = this.p[num2];
			int num12 = this.p[num10 + num4];
			int num13 = this.p[num11 + num4];
			int num14 = this.p[num10 + num5];
			int num15 = this.p[num11 + num5];
			float t = this.s_curve(num3);
			float t2 = this.s_curve(num6);
			float t3 = this.s_curve(num9);
			float a = this.at3(num3, num6, num9, this.g3[num12 + num7, 0], this.g3[num12 + num7, 1], this.g3[num12 + num7, 2]);
			float b = this.at3(rx, num6, num9, this.g3[num13 + num7, 0], this.g3[num13 + num7, 1], this.g3[num13 + num7, 2]);
			float a2 = this.lerp(t, a, b);
			a = this.at3(num3, ry, num9, this.g3[num14 + num7, 0], this.g3[num14 + num7, 1], this.g3[num14 + num7, 2]);
			b = this.at3(rx, ry, num9, this.g3[num15 + num7, 0], this.g3[num15 + num7, 1], this.g3[num15 + num7, 2]);
			float b2 = this.lerp(t, a, b);
			float a3 = this.lerp(t2, a2, b2);
			a = this.at3(num3, num6, rz, this.g3[num12 + num8, 0], this.g3[num12 + num8, 2], this.g3[num12 + num8, 2]);
			b = this.at3(rx, num6, rz, this.g3[num13 + num8, 0], this.g3[num13 + num8, 1], this.g3[num13 + num8, 2]);
			a2 = this.lerp(t, a, b);
			a = this.at3(num3, ry, rz, this.g3[num14 + num8, 0], this.g3[num14 + num8, 1], this.g3[num14 + num8, 2]);
			b = this.at3(rx, ry, rz, this.g3[num15 + num8, 0], this.g3[num15 + num8, 1], this.g3[num15 + num8, 2]);
			b2 = this.lerp(t, a, b);
			float b3 = this.lerp(t2, a2, b2);
			return this.lerp(t3, a3, b3);
		}

		// Token: 0x060012BC RID: 4796 RVA: 0x0005126C File Offset: 0x0004F46C
		private void normalize2(ref float x, ref float y)
		{
			float num = (float)Math.Sqrt((double)(x * x + y * y));
			x = y / num;
			y /= num;
		}

		// Token: 0x060012BD RID: 4797 RVA: 0x00051298 File Offset: 0x0004F498
		private void normalize3(ref float x, ref float y, ref float z)
		{
			float num = (float)Math.Sqrt((double)(x * x + y * y + z * z));
			x = y / num;
			y /= num;
			z /= num;
		}

		// Token: 0x04000C5F RID: 3167
		private const int B = 256;

		// Token: 0x04000C60 RID: 3168
		private const int BM = 255;

		// Token: 0x04000C61 RID: 3169
		private const int N = 4096;

		// Token: 0x04000C62 RID: 3170
		private readonly float[] g1 = new float[514];

		// Token: 0x04000C63 RID: 3171
		private readonly float[,] g2 = new float[514, 2];

		// Token: 0x04000C64 RID: 3172
		private readonly float[,] g3 = new float[514, 3];

		// Token: 0x04000C65 RID: 3173
		private readonly int[] p = new int[514];
	}
}

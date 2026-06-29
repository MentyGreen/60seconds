using System;
using UnityEngine;

namespace RG_GameCamera.Utils
{
	// Token: 0x02000185 RID: 389
	public static class Interpolation
	{
		// Token: 0x06001142 RID: 4418 RVA: 0x00048814 File Offset: 0x00046A14
		public static Vector3 Cubic(Vector3 y0, Vector3 y1, Vector3 y2, Vector3 y3, float t)
		{
			float d = t * t;
			Vector3 vector = y3 - y2 - y0 + y1;
			Vector3 a = y0 - y1 - vector;
			Vector3 a2 = y2 - y0;
			return vector * t * d + a * d + a2 * t + y1;
		}

		// Token: 0x06001143 RID: 4419 RVA: 0x00048884 File Offset: 0x00046A84
		public static Vector3 CatmullRom(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
		{
			float num = t * t;
			float num2 = num * t;
			return p0 * (-0.5f * num2 + num - 0.5f * t) + p1 * (1.5f * num2 + -2.5f * num + 1f) + p2 * (-1.5f * num2 + 2f * num + 0.5f * t) + p3 * (0.5f * num2 - 0.5f * num);
		}

		// Token: 0x06001144 RID: 4420 RVA: 0x00048914 File Offset: 0x00046B14
		public static float EaseInOutCubic(float t, float b, float c, float d)
		{
			t /= d / 2f;
			if (t < 1f)
			{
				return c / 2f * t * t * t + b;
			}
			t -= 2f;
			return c / 2f * (t * t * t + 2f) + b;
		}

		// Token: 0x06001145 RID: 4421 RVA: 0x00048964 File Offset: 0x00046B64
		public static float InterpolateTowards(float pPrev, float pNext, float pSpeed, float pDt)
		{
			float num = pNext - pPrev;
			float num2 = pSpeed * pDt;
			if (pPrev + num < 0f)
			{
				return Mathf.Max(num, -num2);
			}
			return Mathf.Min(num, num2);
		}

		// Token: 0x06001146 RID: 4422 RVA: 0x00048993 File Offset: 0x00046B93
		public static float Lerp(float a, float b, float t)
		{
			return a * (1f - t) + b * t;
		}

		// Token: 0x06001147 RID: 4423 RVA: 0x000489A4 File Offset: 0x00046BA4
		public static float LerpS(float a, float b, float t)
		{
			float num = t * t;
			float num2 = 3f * num - 2f * num * t;
			return a * (1f - num2) + b * num2;
		}

		// Token: 0x06001148 RID: 4424 RVA: 0x000489D4 File Offset: 0x00046BD4
		public static Vector2 LerpS(Vector2 a, Vector2 b, float t)
		{
			float num = t * t;
			float num2 = 3f * num - 2f * num * t;
			return a * (1f - num2) + b * num2;
		}

		// Token: 0x06001149 RID: 4425 RVA: 0x00048A10 File Offset: 0x00046C10
		public static Vector3 LerpS(Vector3 a, Vector3 b, float t)
		{
			float num = t * t;
			float num2 = 3f * num - 2f * num * t;
			return a * (1f - num2) + b * num2;
		}

		// Token: 0x0600114A RID: 4426 RVA: 0x00048A4C File Offset: 0x00046C4C
		public static float LerpS2(float a, float b, float t)
		{
			float num = t * t;
			float num2 = t + num - num * t;
			return a * (1f - num2) + b * num2;
		}

		// Token: 0x0600114B RID: 4427 RVA: 0x00048A74 File Offset: 0x00046C74
		public static Vector3 LerpS2(Vector3 a, Vector3 b, float t)
		{
			float num = t * t;
			float num2 = t + num - num * t;
			return a * (1f - num2) + b * num2;
		}

		// Token: 0x0600114C RID: 4428 RVA: 0x00048AA8 File Offset: 0x00046CA8
		public static Vector2 LerpS2(Vector2 a, Vector2 b, float t)
		{
			float num = t * t;
			float num2 = t + num - num * t;
			return a * (1f - num2) + b * num2;
		}

		// Token: 0x0600114D RID: 4429 RVA: 0x00048ADC File Offset: 0x00046CDC
		public static float LerpS3(float a, float b, float t)
		{
			float num = t * t;
			float num2 = 1f - t - num + num * t;
			return a * (1f - num2) + b * num2;
		}

		// Token: 0x0600114E RID: 4430 RVA: 0x00048B08 File Offset: 0x00046D08
		public static Vector2 LerpS3(Vector2 a, Vector2 b, float t)
		{
			float num = t * t;
			float num2 = 1f - t - num + num * t;
			return a * (1f - num2) + b * num2;
		}

		// Token: 0x0600114F RID: 4431 RVA: 0x00048B40 File Offset: 0x00046D40
		public static Vector3 LerpS3(Vector3 a, Vector3 b, float t)
		{
			float num = t * t;
			float num2 = 1f - t - num + num * t;
			return a * (1f - num2) + b * num2;
		}

		// Token: 0x06001150 RID: 4432 RVA: 0x00048B78 File Offset: 0x00046D78
		public static float LerpExpN(float a, float b, float t, float n)
		{
			float num = Mathf.Exp(n * Mathf.Log(t));
			return a * (1f - num) + b * num;
		}
	}
}

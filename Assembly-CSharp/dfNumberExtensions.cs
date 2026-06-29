using System;
using UnityEngine;

// Token: 0x02000079 RID: 121
public static class dfNumberExtensions
{
	// Token: 0x06000820 RID: 2080 RVA: 0x000239E9 File Offset: 0x00021BE9
	public static int Quantize(this int value, int stepSize)
	{
		if (stepSize <= 0)
		{
			return value;
		}
		return value / stepSize * stepSize;
	}

	// Token: 0x06000821 RID: 2081 RVA: 0x000239F6 File Offset: 0x00021BF6
	public static float Quantize(this float value, float stepSize)
	{
		if (stepSize <= 0f)
		{
			return value;
		}
		return Mathf.Floor(value / stepSize) * stepSize;
	}

	// Token: 0x06000822 RID: 2082 RVA: 0x00023A0C File Offset: 0x00021C0C
	public static int RoundToNearest(this int value, int stepSize)
	{
		if (stepSize <= 0)
		{
			return value;
		}
		int num = value / stepSize * stepSize;
		if (value % stepSize >= stepSize / 2)
		{
			return num + stepSize;
		}
		return num;
	}

	// Token: 0x06000823 RID: 2083 RVA: 0x00023A34 File Offset: 0x00021C34
	public static float RoundToNearest(this float value, float stepSize)
	{
		if (stepSize <= 0f)
		{
			return value;
		}
		float num = Mathf.Floor(value / stepSize) * stepSize;
		if (value - stepSize * Mathf.Floor(value / stepSize) >= stepSize * 0.5f - 1E-45f)
		{
			return num + stepSize;
		}
		return num;
	}
}

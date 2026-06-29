using System;

// Token: 0x020000F4 RID: 244
public static class MathHelper
{
	// Token: 0x06000BD1 RID: 3025 RVA: 0x0003442E File Offset: 0x0003262E
	public static float Lerp(double from, double to, double step)
	{
		return (float)((to - from) * step + from);
	}

	// Token: 0x0400061A RID: 1562
	public const float Pi = 3.1415927f;

	// Token: 0x0400061B RID: 1563
	public const float HalfPi = 1.5707964f;
}

using System;
using UnityEngine;

// Token: 0x020000C6 RID: 198
public class dfEasingFunctions
{
	// Token: 0x06000A92 RID: 2706 RVA: 0x0002DCA0 File Offset: 0x0002BEA0
	public static dfEasingFunctions.EasingFunction GetFunction(dfEasingType easeType)
	{
		switch (easeType)
		{
		case dfEasingType.Linear:
			return new dfEasingFunctions.EasingFunction(dfEasingFunctions.linear);
		case dfEasingType.Bounce:
			return new dfEasingFunctions.EasingFunction(dfEasingFunctions.bounce);
		case dfEasingType.BackEaseIn:
			return new dfEasingFunctions.EasingFunction(dfEasingFunctions.easeInBack);
		case dfEasingType.BackEaseOut:
			return new dfEasingFunctions.EasingFunction(dfEasingFunctions.easeOutBack);
		case dfEasingType.BackEaseInOut:
			return new dfEasingFunctions.EasingFunction(dfEasingFunctions.easeInOutBack);
		case dfEasingType.CircEaseIn:
			return new dfEasingFunctions.EasingFunction(dfEasingFunctions.easeInCirc);
		case dfEasingType.CircEaseOut:
			return new dfEasingFunctions.EasingFunction(dfEasingFunctions.easeOutCirc);
		case dfEasingType.CircEaseInOut:
			return new dfEasingFunctions.EasingFunction(dfEasingFunctions.easeInOutCirc);
		case dfEasingType.CubicEaseIn:
			return new dfEasingFunctions.EasingFunction(dfEasingFunctions.easeInCubic);
		case dfEasingType.CubicEaseOut:
			return new dfEasingFunctions.EasingFunction(dfEasingFunctions.easeOutCubic);
		case dfEasingType.CubicEaseInOut:
			return new dfEasingFunctions.EasingFunction(dfEasingFunctions.easeInOutCubic);
		case dfEasingType.ExpoEaseIn:
			return new dfEasingFunctions.EasingFunction(dfEasingFunctions.easeInExpo);
		case dfEasingType.ExpoEaseOut:
			return new dfEasingFunctions.EasingFunction(dfEasingFunctions.easeOutExpo);
		case dfEasingType.ExpoEaseInOut:
			return new dfEasingFunctions.EasingFunction(dfEasingFunctions.easeInOutExpo);
		case dfEasingType.QuadEaseIn:
			return new dfEasingFunctions.EasingFunction(dfEasingFunctions.easeInQuad);
		case dfEasingType.QuadEaseOut:
			return new dfEasingFunctions.EasingFunction(dfEasingFunctions.easeOutQuad);
		case dfEasingType.QuadEaseInOut:
			return new dfEasingFunctions.EasingFunction(dfEasingFunctions.easeInOutQuad);
		case dfEasingType.QuartEaseIn:
			return new dfEasingFunctions.EasingFunction(dfEasingFunctions.easeInQuart);
		case dfEasingType.QuartEaseOut:
			return new dfEasingFunctions.EasingFunction(dfEasingFunctions.easeOutQuart);
		case dfEasingType.QuartEaseInOut:
			return new dfEasingFunctions.EasingFunction(dfEasingFunctions.easeInOutQuart);
		case dfEasingType.QuintEaseIn:
			return new dfEasingFunctions.EasingFunction(dfEasingFunctions.easeInQuint);
		case dfEasingType.QuintEaseOut:
			return new dfEasingFunctions.EasingFunction(dfEasingFunctions.easeOutQuint);
		case dfEasingType.QuintEaseInOut:
			return new dfEasingFunctions.EasingFunction(dfEasingFunctions.easeInOutQuint);
		case dfEasingType.SineEaseIn:
			return new dfEasingFunctions.EasingFunction(dfEasingFunctions.easeInSine);
		case dfEasingType.SineEaseOut:
			return new dfEasingFunctions.EasingFunction(dfEasingFunctions.easeOutSine);
		case dfEasingType.SineEaseInOut:
			return new dfEasingFunctions.EasingFunction(dfEasingFunctions.easeInOutSine);
		case dfEasingType.Spring:
			return new dfEasingFunctions.EasingFunction(dfEasingFunctions.spring);
		default:
			throw new NotImplementedException();
		}
	}

	// Token: 0x06000A93 RID: 2707 RVA: 0x0002DE88 File Offset: 0x0002C088
	private static float linear(float start, float end, float time)
	{
		return Mathf.Lerp(start, end, time);
	}

	// Token: 0x06000A94 RID: 2708 RVA: 0x0002DE94 File Offset: 0x0002C094
	private static float clerp(float start, float end, float time)
	{
		float num = 0f;
		float num2 = 360f;
		float num3 = Mathf.Abs((num2 - num) / 2f);
		float result;
		if (end - start < -num3)
		{
			float num4 = (num2 - start + end) * time;
			result = start + num4;
		}
		else if (end - start > num3)
		{
			float num4 = -(num2 - end + start) * time;
			result = start + num4;
		}
		else
		{
			result = start + (end - start) * time;
		}
		return result;
	}

	// Token: 0x06000A95 RID: 2709 RVA: 0x0002DF00 File Offset: 0x0002C100
	private static float spring(float start, float end, float time)
	{
		time = Mathf.Clamp01(time);
		time = (Mathf.Sin(time * 3.1415927f * (0.2f + 2.5f * time * time * time)) * Mathf.Pow(1f - time, 2.2f) + time) * (1f + 1.2f * (1f - time));
		return start + (end - start) * time;
	}

	// Token: 0x06000A96 RID: 2710 RVA: 0x0002DF64 File Offset: 0x0002C164
	private static float easeInQuad(float start, float end, float time)
	{
		end -= start;
		return end * time * time + start;
	}

	// Token: 0x06000A97 RID: 2711 RVA: 0x0002DF72 File Offset: 0x0002C172
	private static float easeOutQuad(float start, float end, float time)
	{
		end -= start;
		return -end * time * (time - 2f) + start;
	}

	// Token: 0x06000A98 RID: 2712 RVA: 0x0002DF88 File Offset: 0x0002C188
	private static float easeInOutQuad(float start, float end, float time)
	{
		time /= 0.5f;
		end -= start;
		if (time < 1f)
		{
			return end / 2f * time * time + start;
		}
		time -= 1f;
		return -end / 2f * (time * (time - 2f) - 1f) + start;
	}

	// Token: 0x06000A99 RID: 2713 RVA: 0x0002DFDC File Offset: 0x0002C1DC
	private static float easeInCubic(float start, float end, float time)
	{
		end -= start;
		return end * time * time * time + start;
	}

	// Token: 0x06000A9A RID: 2714 RVA: 0x0002DFEC File Offset: 0x0002C1EC
	private static float easeOutCubic(float start, float end, float time)
	{
		time -= 1f;
		end -= start;
		return end * (time * time * time + 1f) + start;
	}

	// Token: 0x06000A9B RID: 2715 RVA: 0x0002E00C File Offset: 0x0002C20C
	private static float easeInOutCubic(float start, float end, float time)
	{
		time /= 0.5f;
		end -= start;
		if (time < 1f)
		{
			return end / 2f * time * time * time + start;
		}
		time -= 2f;
		return end / 2f * (time * time * time + 2f) + start;
	}

	// Token: 0x06000A9C RID: 2716 RVA: 0x0002E05D File Offset: 0x0002C25D
	private static float easeInQuart(float start, float end, float time)
	{
		end -= start;
		return end * time * time * time * time + start;
	}

	// Token: 0x06000A9D RID: 2717 RVA: 0x0002E06F File Offset: 0x0002C26F
	private static float easeOutQuart(float start, float end, float time)
	{
		time -= 1f;
		end -= start;
		return -end * (time * time * time * time - 1f) + start;
	}

	// Token: 0x06000A9E RID: 2718 RVA: 0x0002E094 File Offset: 0x0002C294
	private static float easeInOutQuart(float start, float end, float time)
	{
		time /= 0.5f;
		end -= start;
		if (time < 1f)
		{
			return end / 2f * time * time * time * time + start;
		}
		time -= 2f;
		return -end / 2f * (time * time * time * time - 2f) + start;
	}

	// Token: 0x06000A9F RID: 2719 RVA: 0x0002E0EA File Offset: 0x0002C2EA
	private static float easeInQuint(float start, float end, float time)
	{
		end -= start;
		return end * time * time * time * time * time + start;
	}

	// Token: 0x06000AA0 RID: 2720 RVA: 0x0002E0FE File Offset: 0x0002C2FE
	private static float easeOutQuint(float start, float end, float time)
	{
		time -= 1f;
		end -= start;
		return end * (time * time * time * time * time + 1f) + start;
	}

	// Token: 0x06000AA1 RID: 2721 RVA: 0x0002E124 File Offset: 0x0002C324
	private static float easeInOutQuint(float start, float end, float time)
	{
		time /= 0.5f;
		end -= start;
		if (time < 1f)
		{
			return end / 2f * time * time * time * time * time + start;
		}
		time -= 2f;
		return end / 2f * (time * time * time * time * time + 2f) + start;
	}

	// Token: 0x06000AA2 RID: 2722 RVA: 0x0002E17D File Offset: 0x0002C37D
	private static float easeInSine(float start, float end, float time)
	{
		end -= start;
		return -end * Mathf.Cos(time / 1f * 1.5707964f) + end + start;
	}

	// Token: 0x06000AA3 RID: 2723 RVA: 0x0002E19D File Offset: 0x0002C39D
	private static float easeOutSine(float start, float end, float time)
	{
		end -= start;
		return end * Mathf.Sin(time / 1f * 1.5707964f) + start;
	}

	// Token: 0x06000AA4 RID: 2724 RVA: 0x0002E1BA File Offset: 0x0002C3BA
	private static float easeInOutSine(float start, float end, float time)
	{
		end -= start;
		return -end / 2f * (Mathf.Cos(3.1415927f * time / 1f) - 1f) + start;
	}

	// Token: 0x06000AA5 RID: 2725 RVA: 0x0002E1E4 File Offset: 0x0002C3E4
	private static float easeInExpo(float start, float end, float time)
	{
		end -= start;
		return end * Mathf.Pow(2f, 10f * (time / 1f - 1f)) + start;
	}

	// Token: 0x06000AA6 RID: 2726 RVA: 0x0002E20C File Offset: 0x0002C40C
	private static float easeOutExpo(float start, float end, float time)
	{
		end -= start;
		return end * (-Mathf.Pow(2f, -10f * time / 1f) + 1f) + start;
	}

	// Token: 0x06000AA7 RID: 2727 RVA: 0x0002E238 File Offset: 0x0002C438
	private static float easeInOutExpo(float start, float end, float time)
	{
		time /= 0.5f;
		end -= start;
		if (time < 1f)
		{
			return end / 2f * Mathf.Pow(2f, 10f * (time - 1f)) + start;
		}
		time -= 1f;
		return end / 2f * (-Mathf.Pow(2f, -10f * time) + 2f) + start;
	}

	// Token: 0x06000AA8 RID: 2728 RVA: 0x0002E2A8 File Offset: 0x0002C4A8
	private static float easeInCirc(float start, float end, float time)
	{
		end -= start;
		return -end * (Mathf.Sqrt(1f - time * time) - 1f) + start;
	}

	// Token: 0x06000AA9 RID: 2729 RVA: 0x0002E2C8 File Offset: 0x0002C4C8
	private static float easeOutCirc(float start, float end, float time)
	{
		time -= 1f;
		end -= start;
		return end * Mathf.Sqrt(1f - time * time) + start;
	}

	// Token: 0x06000AAA RID: 2730 RVA: 0x0002E2EC File Offset: 0x0002C4EC
	private static float easeInOutCirc(float start, float end, float time)
	{
		time /= 0.5f;
		end -= start;
		if (time < 1f)
		{
			return -end / 2f * (Mathf.Sqrt(1f - time * time) - 1f) + start;
		}
		time -= 2f;
		return end / 2f * (Mathf.Sqrt(1f - time * time) + 1f) + start;
	}

	// Token: 0x06000AAB RID: 2731 RVA: 0x0002E358 File Offset: 0x0002C558
	private static float bounce(float start, float end, float time)
	{
		time /= 1f;
		end -= start;
		if (time < 0.36363637f)
		{
			return end * (7.5625f * time * time) + start;
		}
		if (time < 0.72727275f)
		{
			time -= 0.54545456f;
			return end * (7.5625f * time * time + 0.75f) + start;
		}
		if ((double)time < 0.9090909090909091)
		{
			time -= 0.8181818f;
			return end * (7.5625f * time * time + 0.9375f) + start;
		}
		time -= 0.95454544f;
		return end * (7.5625f * time * time + 0.984375f) + start;
	}

	// Token: 0x06000AAC RID: 2732 RVA: 0x0002E3F4 File Offset: 0x0002C5F4
	private static float easeInBack(float start, float end, float time)
	{
		end -= start;
		time /= 1f;
		float num = 1.70158f;
		return end * time * time * ((num + 1f) * time - num) + start;
	}

	// Token: 0x06000AAD RID: 2733 RVA: 0x0002E428 File Offset: 0x0002C628
	private static float easeOutBack(float start, float end, float time)
	{
		float num = 1.70158f;
		end -= start;
		time = time / 1f - 1f;
		return end * (time * time * ((num + 1f) * time + num) + 1f) + start;
	}

	// Token: 0x06000AAE RID: 2734 RVA: 0x0002E468 File Offset: 0x0002C668
	private static float easeInOutBack(float start, float end, float time)
	{
		float num = 1.70158f;
		end -= start;
		time /= 0.5f;
		if (time < 1f)
		{
			num *= 1.525f;
			return end / 2f * (time * time * ((num + 1f) * time - num)) + start;
		}
		time -= 2f;
		num *= 1.525f;
		return end / 2f * (time * time * ((num + 1f) * time + num) + 2f) + start;
	}

	// Token: 0x06000AAF RID: 2735 RVA: 0x0002E4E4 File Offset: 0x0002C6E4
	private static float punch(float amplitude, float time)
	{
		if (time == 0f)
		{
			return 0f;
		}
		if (time == 1f)
		{
			return 0f;
		}
		float num = 0.3f;
		float num2 = num / 6.2831855f * Mathf.Asin(0f);
		return amplitude * Mathf.Pow(2f, -10f * time) * Mathf.Sin((time * 1f - num2) * 6.2831855f / num);
	}

	// Token: 0x02000388 RID: 904
	// (Invoke) Token: 0x06001D14 RID: 7444
	public delegate float EasingFunction(float start, float end, float time);
}

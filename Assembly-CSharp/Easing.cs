using System;

// Token: 0x020000F2 RID: 242
public static class Easing
{
	// Token: 0x06000BCC RID: 3020 RVA: 0x00034274 File Offset: 0x00032474
	public static float Ease(double linearStep, float acceleration, EasingType type)
	{
		float num = (acceleration > 0f) ? Easing.EaseIn(linearStep, type) : ((acceleration < 0f) ? Easing.EaseOut(linearStep, type) : ((float)linearStep));
		return MathHelper.Lerp(linearStep, (double)num, (double)Math.Abs(acceleration));
	}

	// Token: 0x06000BCD RID: 3021 RVA: 0x000342B8 File Offset: 0x000324B8
	public static float EaseIn(double linearStep, EasingType type)
	{
		switch (type)
		{
		case EasingType.Step:
			return (float)((linearStep < 0.5) ? 0 : 1);
		case EasingType.Linear:
			return (float)linearStep;
		case EasingType.Sine:
			return Easing.Sine.EaseIn(linearStep);
		case EasingType.Quadratic:
			return Easing.Power.EaseIn(linearStep, 2);
		case EasingType.Cubic:
			return Easing.Power.EaseIn(linearStep, 3);
		case EasingType.Quartic:
			return Easing.Power.EaseIn(linearStep, 4);
		case EasingType.Quintic:
			return Easing.Power.EaseIn(linearStep, 5);
		default:
			throw new NotImplementedException();
		}
	}

	// Token: 0x06000BCE RID: 3022 RVA: 0x0003432C File Offset: 0x0003252C
	public static float EaseOut(double linearStep, EasingType type)
	{
		switch (type)
		{
		case EasingType.Step:
			return (float)((linearStep < 0.5) ? 0 : 1);
		case EasingType.Linear:
			return (float)linearStep;
		case EasingType.Sine:
			return Easing.Sine.EaseOut(linearStep);
		case EasingType.Quadratic:
			return Easing.Power.EaseOut(linearStep, 2);
		case EasingType.Cubic:
			return Easing.Power.EaseOut(linearStep, 3);
		case EasingType.Quartic:
			return Easing.Power.EaseOut(linearStep, 4);
		case EasingType.Quintic:
			return Easing.Power.EaseOut(linearStep, 5);
		default:
			throw new NotImplementedException();
		}
	}

	// Token: 0x06000BCF RID: 3023 RVA: 0x0003439E File Offset: 0x0003259E
	public static float EaseInOut(double linearStep, EasingType easeInType, EasingType easeOutType)
	{
		if (linearStep >= 0.5)
		{
			return Easing.EaseInOut(linearStep, easeOutType);
		}
		return Easing.EaseInOut(linearStep, easeInType);
	}

	// Token: 0x06000BD0 RID: 3024 RVA: 0x000343BC File Offset: 0x000325BC
	public static float EaseInOut(double linearStep, EasingType type)
	{
		switch (type)
		{
		case EasingType.Step:
			return (float)((linearStep < 0.5) ? 0 : 1);
		case EasingType.Linear:
			return (float)linearStep;
		case EasingType.Sine:
			return Easing.Sine.EaseInOut(linearStep);
		case EasingType.Quadratic:
			return Easing.Power.EaseInOut(linearStep, 2);
		case EasingType.Cubic:
			return Easing.Power.EaseInOut(linearStep, 3);
		case EasingType.Quartic:
			return Easing.Power.EaseInOut(linearStep, 4);
		case EasingType.Quintic:
			return Easing.Power.EaseInOut(linearStep, 5);
		default:
			throw new NotImplementedException();
		}
	}

	// Token: 0x02000396 RID: 918
	private static class Sine
	{
		// Token: 0x06001D54 RID: 7508 RVA: 0x0007D58E File Offset: 0x0007B78E
		public static float EaseIn(double s)
		{
			return (float)Math.Sin(s * 1.5707963705062866 - 1.5707963705062866) + 1f;
		}

		// Token: 0x06001D55 RID: 7509 RVA: 0x0007D5B1 File Offset: 0x0007B7B1
		public static float EaseOut(double s)
		{
			return (float)Math.Sin(s * 1.5707963705062866);
		}

		// Token: 0x06001D56 RID: 7510 RVA: 0x0007D5C4 File Offset: 0x0007B7C4
		public static float EaseInOut(double s)
		{
			return (float)(Math.Sin(s * 3.1415927410125732 - 1.5707963705062866) + 1.0) / 2f;
		}
	}

	// Token: 0x02000397 RID: 919
	private static class Power
	{
		// Token: 0x06001D57 RID: 7511 RVA: 0x0007D5F1 File Offset: 0x0007B7F1
		public static float EaseIn(double s, int power)
		{
			return (float)Math.Pow(s, (double)power);
		}

		// Token: 0x06001D58 RID: 7512 RVA: 0x0007D5FC File Offset: 0x0007B7FC
		public static float EaseOut(double s, int power)
		{
			int num = (power % 2 == 0) ? -1 : 1;
			return (float)((double)num * (Math.Pow(s - 1.0, (double)power) + (double)num));
		}

		// Token: 0x06001D59 RID: 7513 RVA: 0x0007D62C File Offset: 0x0007B82C
		public static float EaseInOut(double s, int power)
		{
			s *= 2.0;
			if (s < 1.0)
			{
				return Easing.Power.EaseIn(s, power) / 2f;
			}
			int num = (power % 2 == 0) ? -1 : 1;
			return (float)((double)num / 2.0 * (Math.Pow(s - 2.0, (double)power) + (double)(num * 2)));
		}
	}
}

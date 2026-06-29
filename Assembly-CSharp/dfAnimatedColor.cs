using System;
using UnityEngine;

// Token: 0x020000C3 RID: 195
public class dfAnimatedColor : dfAnimatedValue<Color>
{
	// Token: 0x06000A7E RID: 2686 RVA: 0x0002DAF7 File Offset: 0x0002BCF7
	public dfAnimatedColor(Color StartValue, Color EndValue, float Time) : base(StartValue, EndValue, Time)
	{
	}

	// Token: 0x06000A7F RID: 2687 RVA: 0x0002DB02 File Offset: 0x0002BD02
	protected override Color Lerp(Color startValue, Color endValue, float time)
	{
		return Color.Lerp(startValue, endValue, time);
	}

	// Token: 0x06000A80 RID: 2688 RVA: 0x0002DB0C File Offset: 0x0002BD0C
	public static implicit operator dfAnimatedColor(Color value)
	{
		return new dfAnimatedColor(value, value, 0f);
	}
}

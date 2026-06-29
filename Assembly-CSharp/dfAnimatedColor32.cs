using System;
using UnityEngine;

// Token: 0x020000C4 RID: 196
public class dfAnimatedColor32 : dfAnimatedValue<Color32>
{
	// Token: 0x06000A81 RID: 2689 RVA: 0x0002DB1A File Offset: 0x0002BD1A
	public dfAnimatedColor32(Color32 StartValue, Color32 EndValue, float Time) : base(StartValue, EndValue, Time)
	{
	}

	// Token: 0x06000A82 RID: 2690 RVA: 0x0002DB25 File Offset: 0x0002BD25
	protected override Color32 Lerp(Color32 startValue, Color32 endValue, float time)
	{
		return Color.Lerp(startValue, endValue, time);
	}

	// Token: 0x06000A83 RID: 2691 RVA: 0x0002DB3E File Offset: 0x0002BD3E
	public static implicit operator dfAnimatedColor32(Color32 value)
	{
		return new dfAnimatedColor32(value, value, 0f);
	}
}

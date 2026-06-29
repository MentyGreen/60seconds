using System;
using UnityEngine;

// Token: 0x020000BE RID: 190
public class dfAnimatedInt : dfAnimatedValue<int>
{
	// Token: 0x06000A6F RID: 2671 RVA: 0x0002DA41 File Offset: 0x0002BC41
	public dfAnimatedInt(int StartValue, int EndValue, float Time) : base(StartValue, EndValue, Time)
	{
	}

	// Token: 0x06000A70 RID: 2672 RVA: 0x0002DA4C File Offset: 0x0002BC4C
	protected override int Lerp(int startValue, int endValue, float time)
	{
		return Mathf.RoundToInt(Mathf.Lerp((float)startValue, (float)endValue, time));
	}

	// Token: 0x06000A71 RID: 2673 RVA: 0x0002DA5D File Offset: 0x0002BC5D
	public static implicit operator dfAnimatedInt(int value)
	{
		return new dfAnimatedInt(value, value, 0f);
	}
}

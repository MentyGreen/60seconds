using System;
using UnityEngine;

// Token: 0x020000BD RID: 189
public class dfAnimatedFloat : dfAnimatedValue<float>
{
	// Token: 0x06000A6C RID: 2668 RVA: 0x0002DA1E File Offset: 0x0002BC1E
	public dfAnimatedFloat(float StartValue, float EndValue, float Time) : base(StartValue, EndValue, Time)
	{
	}

	// Token: 0x06000A6D RID: 2669 RVA: 0x0002DA29 File Offset: 0x0002BC29
	protected override float Lerp(float startValue, float endValue, float time)
	{
		return Mathf.Lerp(startValue, endValue, time);
	}

	// Token: 0x06000A6E RID: 2670 RVA: 0x0002DA33 File Offset: 0x0002BC33
	public static implicit operator dfAnimatedFloat(float value)
	{
		return new dfAnimatedFloat(value, value, 0f);
	}
}

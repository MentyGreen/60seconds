using System;
using UnityEngine;

// Token: 0x020000BF RID: 191
public class dfAnimatedVector3 : dfAnimatedValue<Vector3>
{
	// Token: 0x06000A72 RID: 2674 RVA: 0x0002DA6B File Offset: 0x0002BC6B
	public dfAnimatedVector3(Vector3 StartValue, Vector3 EndValue, float Time) : base(StartValue, EndValue, Time)
	{
	}

	// Token: 0x06000A73 RID: 2675 RVA: 0x0002DA76 File Offset: 0x0002BC76
	protected override Vector3 Lerp(Vector3 startValue, Vector3 endValue, float time)
	{
		return Vector3.Lerp(startValue, endValue, time);
	}

	// Token: 0x06000A74 RID: 2676 RVA: 0x0002DA80 File Offset: 0x0002BC80
	public static implicit operator dfAnimatedVector3(Vector3 value)
	{
		return new dfAnimatedVector3(value, value, 0f);
	}
}

using System;
using UnityEngine;

// Token: 0x020000C0 RID: 192
public class dfAnimatedVector4 : dfAnimatedValue<Vector4>
{
	// Token: 0x06000A75 RID: 2677 RVA: 0x0002DA8E File Offset: 0x0002BC8E
	public dfAnimatedVector4(Vector4 StartValue, Vector4 EndValue, float Time) : base(StartValue, EndValue, Time)
	{
	}

	// Token: 0x06000A76 RID: 2678 RVA: 0x0002DA99 File Offset: 0x0002BC99
	protected override Vector4 Lerp(Vector4 startValue, Vector4 endValue, float time)
	{
		return Vector4.Lerp(startValue, endValue, time);
	}

	// Token: 0x06000A77 RID: 2679 RVA: 0x0002DAA3 File Offset: 0x0002BCA3
	public static implicit operator dfAnimatedVector4(Vector4 value)
	{
		return new dfAnimatedVector4(value, value, 0f);
	}
}

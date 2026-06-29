using System;
using UnityEngine;

// Token: 0x020000C1 RID: 193
public class dfAnimatedVector2 : dfAnimatedValue<Vector2>
{
	// Token: 0x06000A78 RID: 2680 RVA: 0x0002DAB1 File Offset: 0x0002BCB1
	public dfAnimatedVector2(Vector2 StartValue, Vector2 EndValue, float Time) : base(StartValue, EndValue, Time)
	{
	}

	// Token: 0x06000A79 RID: 2681 RVA: 0x0002DABC File Offset: 0x0002BCBC
	protected override Vector2 Lerp(Vector2 startValue, Vector2 endValue, float time)
	{
		return Vector2.Lerp(startValue, endValue, time);
	}

	// Token: 0x06000A7A RID: 2682 RVA: 0x0002DAC6 File Offset: 0x0002BCC6
	public static implicit operator dfAnimatedVector2(Vector2 value)
	{
		return new dfAnimatedVector2(value, value, 0f);
	}
}

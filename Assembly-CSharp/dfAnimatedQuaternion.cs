using System;
using UnityEngine;

// Token: 0x020000C2 RID: 194
public class dfAnimatedQuaternion : dfAnimatedValue<Quaternion>
{
	// Token: 0x06000A7B RID: 2683 RVA: 0x0002DAD4 File Offset: 0x0002BCD4
	public dfAnimatedQuaternion(Quaternion StartValue, Quaternion EndValue, float Time) : base(StartValue, EndValue, Time)
	{
	}

	// Token: 0x06000A7C RID: 2684 RVA: 0x0002DADF File Offset: 0x0002BCDF
	protected override Quaternion Lerp(Quaternion startValue, Quaternion endValue, float time)
	{
		return Quaternion.Lerp(startValue, endValue, time);
	}

	// Token: 0x06000A7D RID: 2685 RVA: 0x0002DAE9 File Offset: 0x0002BCE9
	public static implicit operator dfAnimatedQuaternion(Quaternion value)
	{
		return new dfAnimatedQuaternion(value, value, 0f);
	}
}

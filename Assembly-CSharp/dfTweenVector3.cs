using System;
using UnityEngine;

// Token: 0x020000D5 RID: 213
[AddComponentMenu("Daikon Forge/Tweens/Vector3")]
public class dfTweenVector3 : dfTweenComponent<Vector3>
{
	// Token: 0x06000B40 RID: 2880 RVA: 0x0002FAEC File Offset: 0x0002DCEC
	public override Vector3 offset(Vector3 lhs, Vector3 rhs)
	{
		return lhs + rhs;
	}

	// Token: 0x06000B41 RID: 2881 RVA: 0x0002FAF5 File Offset: 0x0002DCF5
	public override Vector3 evaluate(Vector3 startValue, Vector3 endValue, float time)
	{
		return new Vector3(dfTweenComponent<Vector3>.Lerp(startValue.x, endValue.x, time), dfTweenComponent<Vector3>.Lerp(startValue.y, endValue.y, time), dfTweenComponent<Vector3>.Lerp(startValue.z, endValue.z, time));
	}
}

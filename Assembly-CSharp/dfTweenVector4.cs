using System;
using UnityEngine;

// Token: 0x020000D6 RID: 214
[AddComponentMenu("Daikon Forge/Tweens/Vector4")]
public class dfTweenVector4 : dfTweenComponent<Vector4>
{
	// Token: 0x06000B43 RID: 2883 RVA: 0x0002FB3A File Offset: 0x0002DD3A
	public override Vector4 offset(Vector4 lhs, Vector4 rhs)
	{
		return lhs + rhs;
	}

	// Token: 0x06000B44 RID: 2884 RVA: 0x0002FB44 File Offset: 0x0002DD44
	public override Vector4 evaluate(Vector4 startValue, Vector4 endValue, float time)
	{
		return new Vector4(dfTweenComponent<Vector4>.Lerp(startValue.x, endValue.x, time), dfTweenComponent<Vector4>.Lerp(startValue.y, endValue.y, time), dfTweenComponent<Vector4>.Lerp(startValue.z, endValue.z, time), dfTweenComponent<Vector4>.Lerp(startValue.w, endValue.w, time));
	}
}

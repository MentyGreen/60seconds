using System;
using UnityEngine;

// Token: 0x020000D4 RID: 212
[AddComponentMenu("Daikon Forge/Tweens/Vector2")]
public class dfTweenVector2 : dfTweenComponent<Vector2>
{
	// Token: 0x06000B3D RID: 2877 RVA: 0x0002FAB0 File Offset: 0x0002DCB0
	public override Vector2 offset(Vector2 lhs, Vector2 rhs)
	{
		return lhs + rhs;
	}

	// Token: 0x06000B3E RID: 2878 RVA: 0x0002FAB9 File Offset: 0x0002DCB9
	public override Vector2 evaluate(Vector2 startValue, Vector2 endValue, float time)
	{
		return new Vector2(dfTweenComponent<Vector2>.Lerp(startValue.x, endValue.x, time), dfTweenComponent<Vector2>.Lerp(startValue.y, endValue.y, time));
	}
}

using System;
using UnityEngine;

// Token: 0x020000C8 RID: 200
[AddComponentMenu("Daikon Forge/Tweens/Color")]
public class dfTweenColor : dfTweenComponent<Color>
{
	// Token: 0x06000AB1 RID: 2737 RVA: 0x0002E55E File Offset: 0x0002C75E
	public override Color offset(Color lhs, Color rhs)
	{
		return lhs + rhs;
	}

	// Token: 0x06000AB2 RID: 2738 RVA: 0x0002E568 File Offset: 0x0002C768
	public override Color evaluate(Color startValue, Color endValue, float time)
	{
		Vector4 vector = startValue;
		Vector4 vector2 = endValue;
		return new Vector4(dfTweenComponent<Color>.Lerp(vector.x, vector2.x, time), dfTweenComponent<Color>.Lerp(vector.y, vector2.y, time), dfTweenComponent<Color>.Lerp(vector.z, vector2.z, time), dfTweenComponent<Color>.Lerp(vector.w, vector2.w, time));
	}
}

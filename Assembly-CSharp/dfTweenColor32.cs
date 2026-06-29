using System;
using UnityEngine;

// Token: 0x020000C9 RID: 201
[AddComponentMenu("Daikon Forge/Tweens/Color32")]
public class dfTweenColor32 : dfTweenComponent<Color32>
{
	// Token: 0x06000AB4 RID: 2740 RVA: 0x0002E5DD File Offset: 0x0002C7DD
	public override Color32 offset(Color32 lhs, Color32 rhs)
	{
		return lhs + rhs;
	}

	// Token: 0x06000AB5 RID: 2741 RVA: 0x0002E5F8 File Offset: 0x0002C7F8
	public override Color32 evaluate(Color32 startValue, Color32 endValue, float time)
	{
		Vector4 vector = startValue;
		Vector4 vector2 = endValue;
		return new Vector4(dfTweenComponent<Color32>.Lerp(vector.x, vector2.x, time), dfTweenComponent<Color32>.Lerp(vector.y, vector2.y, time), dfTweenComponent<Color32>.Lerp(vector.z, vector2.z, time), dfTweenComponent<Color32>.Lerp(vector.w, vector2.w, time));
	}
}

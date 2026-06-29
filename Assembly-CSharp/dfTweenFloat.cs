using System;
using UnityEngine;

// Token: 0x020000CF RID: 207
[AddComponentMenu("Daikon Forge/Tweens/Float")]
public class dfTweenFloat : dfTweenComponent<float>
{
	// Token: 0x06000B0F RID: 2831 RVA: 0x0002F4D2 File Offset: 0x0002D6D2
	public override float offset(float lhs, float rhs)
	{
		return lhs + rhs;
	}

	// Token: 0x06000B10 RID: 2832 RVA: 0x0002F4D7 File Offset: 0x0002D6D7
	public override float evaluate(float startValue, float endValue, float time)
	{
		return startValue + (endValue - startValue) * time;
	}
}

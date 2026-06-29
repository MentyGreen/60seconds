using System;
using UnityEngine;

// Token: 0x020000D3 RID: 211
[AddComponentMenu("Daikon Forge/Tweens/Rotation")]
public class dfTweenRotation : dfTweenComponent<Quaternion>
{
	// Token: 0x06000B38 RID: 2872 RVA: 0x0002FA07 File Offset: 0x0002DC07
	public override Quaternion offset(Quaternion lhs, Quaternion rhs)
	{
		return lhs * rhs;
	}

	// Token: 0x06000B39 RID: 2873 RVA: 0x0002FA10 File Offset: 0x0002DC10
	public override Quaternion evaluate(Quaternion startValue, Quaternion endValue, float time)
	{
		Vector3 eulerAngles = startValue.eulerAngles;
		Vector3 eulerAngles2 = endValue.eulerAngles;
		return Quaternion.Euler(dfTweenRotation.LerpEuler(eulerAngles, eulerAngles2, time));
	}

	// Token: 0x06000B3A RID: 2874 RVA: 0x0002FA38 File Offset: 0x0002DC38
	private static Vector3 LerpEuler(Vector3 startValue, Vector3 endValue, float time)
	{
		return new Vector3(dfTweenRotation.LerpAngle(startValue.x, endValue.x, time), dfTweenRotation.LerpAngle(startValue.y, endValue.y, time), dfTweenRotation.LerpAngle(startValue.z, endValue.z, time));
	}

	// Token: 0x06000B3B RID: 2875 RVA: 0x0002FA78 File Offset: 0x0002DC78
	private static float LerpAngle(float startValue, float endValue, float time)
	{
		float num = Mathf.Repeat(endValue - startValue, 360f);
		if (num > 180f)
		{
			num -= 360f;
		}
		return startValue + num * time;
	}
}

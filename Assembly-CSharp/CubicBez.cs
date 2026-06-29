using System;
using UnityEngine;

// Token: 0x020000EE RID: 238
public static class CubicBez
{
	// Token: 0x06000BBB RID: 3003 RVA: 0x00033894 File Offset: 0x00031A94
	public static Vector3 Interp(Vector3 st, Vector3 en, Vector3 ctrl1, Vector3 ctrl2, float t)
	{
		float num = 1f - t;
		return num * num * num * st + 3f * num * num * t * ctrl1 + 3f * num * t * t * ctrl2 + t * t * t * en;
	}

	// Token: 0x06000BBC RID: 3004 RVA: 0x000338F8 File Offset: 0x00031AF8
	public static Vector3 Velocity(Vector3 st, Vector3 en, Vector3 ctrl1, Vector3 ctrl2, float t)
	{
		return (-3f * st + 9f * ctrl1 - 9f * ctrl2 + 3f * en) * t * t + (6f * st - 12f * ctrl1 + 6f * ctrl2) * t - 3f * st + 3f * ctrl1;
	}

	// Token: 0x06000BBD RID: 3005 RVA: 0x000339A8 File Offset: 0x00031BA8
	public static void GizmoDraw(Vector3 st, Vector3 en, Vector3 ctrl1, Vector3 ctrl2, float t)
	{
		Gizmos.color = Color.red;
		Gizmos.DrawLine(st, ctrl1);
		Gizmos.DrawLine(ctrl2, en);
		Gizmos.color = Color.white;
		Vector3 to = st;
		for (int i = 1; i <= 20; i++)
		{
			float t2 = (float)i / 20f;
			Vector3 vector = CubicBez.Interp(st, en, ctrl1, ctrl2, t2);
			Gizmos.DrawLine(vector, to);
			to = vector;
		}
		Gizmos.color = Color.blue;
		Vector3 vector2 = CubicBez.Interp(st, en, ctrl1, ctrl2, t);
		Gizmos.DrawLine(vector2, vector2 + CubicBez.Velocity(st, en, ctrl1, ctrl2, t));
	}
}

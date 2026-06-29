using System;
using UnityEngine;

// Token: 0x020000EF RID: 239
public static class CRSpline
{
	// Token: 0x06000BBE RID: 3006 RVA: 0x00033A2C File Offset: 0x00031C2C
	public static Vector3 Interp(Vector3[] pts, float t)
	{
		int num = pts.Length - 3;
		int num2 = Mathf.Min(Mathf.FloorToInt(t * (float)num), num - 1);
		float num3 = t * (float)num - (float)num2;
		Vector3 a = pts[num2];
		Vector3 a2 = pts[num2 + 1];
		Vector3 vector = pts[num2 + 2];
		Vector3 b = pts[num2 + 3];
		return 0.5f * ((-a + 3f * a2 - 3f * vector + b) * (num3 * num3 * num3) + (2f * a - 5f * a2 + 4f * vector - b) * (num3 * num3) + (-a + vector) * num3 + 2f * a2);
	}

	// Token: 0x06000BBF RID: 3007 RVA: 0x00033B30 File Offset: 0x00031D30
	public static Vector3 InterpConstantSpeed(Vector3[] pts, float t)
	{
		int num = pts.Length - 3;
		float num2 = 0f;
		float[] array = new float[pts.Length - 1];
		for (int i = 0; i < pts.Length - 1; i++)
		{
			float magnitude = (pts[i + 1] - pts[i]).magnitude;
			array[i] = magnitude;
			num2 += magnitude;
		}
		int num3 = 1;
		float num4 = 0f;
		double num7;
		do
		{
			double num5 = (double)(num4 / num2);
			double num6 = (double)((num4 + array[num3]) / num2);
			num7 = ((double)t - num5) / (num6 - num5);
			if (num7 >= 0.0 && num7 <= 1.0)
			{
				break;
			}
			num4 += array[num3];
			num3++;
		}
		while (num3 < num + 1);
		num7 = (double)Mathf.Clamp01((float)num7);
		Vector3 a = pts[num3 - 1];
		Vector3 a2 = pts[num3];
		Vector3 vector = pts[num3 + 1];
		Vector3 b = pts[num3 + 2];
		return 0.5f * ((-a + 3f * a2 - 3f * vector + b) * (float)(num7 * num7 * num7) + (2f * a - 5f * a2 + 4f * vector - b) * (float)(num7 * num7) + (-a + vector) * (float)num7 + 2f * a2);
	}

	// Token: 0x06000BC0 RID: 3008 RVA: 0x00033CE4 File Offset: 0x00031EE4
	public static Vector3 Velocity(Vector3[] pts, float t)
	{
		int num = pts.Length - 3;
		int num2 = Mathf.Min(Mathf.FloorToInt(t * (float)num), num - 1);
		float num3 = t * (float)num - (float)num2;
		Vector3 a = pts[num2];
		Vector3 a2 = pts[num2 + 1];
		Vector3 a3 = pts[num2 + 2];
		Vector3 b = pts[num2 + 3];
		return 1.5f * (-a + 3f * a2 - 3f * a3 + b) * (num3 * num3) + (2f * a - 5f * a2 + 4f * a3 - b) * num3 + 0.5f * a3 - 0.5f * a;
	}

	// Token: 0x06000BC1 RID: 3009 RVA: 0x00033DDC File Offset: 0x00031FDC
	public static void GizmoDraw(Vector3[] pts, float t)
	{
		Gizmos.color = Color.white;
		Vector3 to = CRSpline.Interp(pts, 0f);
		for (int i = 1; i <= 20; i++)
		{
			float t2 = (float)i / 20f;
			Vector3 vector = CRSpline.Interp(pts, t2);
			Gizmos.DrawLine(vector, to);
			to = vector;
		}
		Gizmos.color = Color.blue;
		Vector3 vector2 = CRSpline.Interp(pts, t);
		Gizmos.DrawLine(vector2, vector2 + CRSpline.Velocity(pts, t));
	}
}

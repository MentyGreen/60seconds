using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000004 RID: 4
[ExecuteInEditMode]
[Serializable]
public class BezierCurve : MonoBehaviour
{
	// Token: 0x17000001 RID: 1
	// (get) Token: 0x0600000A RID: 10 RVA: 0x00002244 File Offset: 0x00000444
	// (set) Token: 0x0600000B RID: 11 RVA: 0x0000224C File Offset: 0x0000044C
	public bool dirty { get; private set; }

	// Token: 0x17000002 RID: 2
	// (get) Token: 0x0600000C RID: 12 RVA: 0x00002255 File Offset: 0x00000455
	// (set) Token: 0x0600000D RID: 13 RVA: 0x0000225D File Offset: 0x0000045D
	public bool close
	{
		get
		{
			return this._close;
		}
		set
		{
			if (this._close == value)
			{
				return;
			}
			this._close = value;
			this.dirty = true;
		}
	}

	// Token: 0x17000003 RID: 3
	public BezierPoint this[int index]
	{
		get
		{
			return this.points[index];
		}
	}

	// Token: 0x17000004 RID: 4
	// (get) Token: 0x0600000F RID: 15 RVA: 0x00002281 File Offset: 0x00000481
	public int pointCount
	{
		get
		{
			return this.points.Length;
		}
	}

	// Token: 0x17000005 RID: 5
	// (get) Token: 0x06000010 RID: 16 RVA: 0x0000228C File Offset: 0x0000048C
	public float length
	{
		get
		{
			if (this.dirty)
			{
				this._length = 0f;
				for (int i = 0; i < this.points.Length - 1; i++)
				{
					this._length += BezierCurve.ApproximateLength(this.points[i], this.points[i + 1], this.resolution);
				}
				if (this.close)
				{
					this._length += BezierCurve.ApproximateLength(this.points[this.points.Length - 1], this.points[0], this.resolution);
				}
				this.dirty = false;
			}
			return this._length;
		}
	}

	// Token: 0x06000011 RID: 17 RVA: 0x00002334 File Offset: 0x00000534
	private void OnDrawGizmos()
	{
		Gizmos.color = this.drawColor;
		if (this.points.Length > 1)
		{
			for (int i = 0; i < this.points.Length - 1; i++)
			{
				BezierCurve.DrawCurve(this.points[i], this.points[i + 1], this.resolution);
			}
			if (this.close)
			{
				BezierCurve.DrawCurve(this.points[this.points.Length - 1], this.points[0], this.resolution);
			}
		}
	}

	// Token: 0x06000012 RID: 18 RVA: 0x000023B5 File Offset: 0x000005B5
	private void Awake()
	{
		this.dirty = true;
	}

	// Token: 0x06000013 RID: 19 RVA: 0x000023C0 File Offset: 0x000005C0
	public void AddPoint(BezierPoint point)
	{
		this.points = new List<BezierPoint>(this.points)
		{
			point
		}.ToArray();
		this.dirty = true;
	}

	// Token: 0x06000014 RID: 20 RVA: 0x000023F4 File Offset: 0x000005F4
	public BezierPoint AddPointAt(Vector3 position)
	{
		BezierPoint bezierPoint = new GameObject("Point " + this.pointCount.ToString())
		{
			transform = 
			{
				parent = base.transform,
				position = position
			}
		}.AddComponent<BezierPoint>();
		bezierPoint.curve = this;
		return bezierPoint;
	}

	// Token: 0x06000015 RID: 21 RVA: 0x00002448 File Offset: 0x00000648
	public void RemovePoint(BezierPoint point)
	{
		List<BezierPoint> list = new List<BezierPoint>(this.points);
		list.Remove(point);
		this.points = list.ToArray();
		this.dirty = false;
	}

	// Token: 0x06000016 RID: 22 RVA: 0x0000247C File Offset: 0x0000067C
	public BezierPoint[] GetAnchorPoints()
	{
		return (BezierPoint[])this.points.Clone();
	}

	// Token: 0x06000017 RID: 23 RVA: 0x00002490 File Offset: 0x00000690
	public Vector3 GetPointAt(float t)
	{
		if (t <= 0f)
		{
			return this.points[0].position;
		}
		if (t >= 1f)
		{
			return this.points[this.points.Length - 1].position;
		}
		float num = 0f;
		float num2 = 0f;
		BezierPoint bezierPoint = null;
		BezierPoint p = null;
		for (int i = 0; i < this.points.Length - 1; i++)
		{
			num2 = BezierCurve.ApproximateLength(this.points[i], this.points[i + 1], 10) / this.length;
			if (num + num2 > t)
			{
				bezierPoint = this.points[i];
				p = this.points[i + 1];
				break;
			}
			num += num2;
		}
		if (this.close && bezierPoint == null)
		{
			bezierPoint = this.points[this.points.Length - 1];
			p = this.points[0];
		}
		t -= num;
		return BezierCurve.GetPoint(bezierPoint, p, t / num2);
	}

	// Token: 0x06000018 RID: 24 RVA: 0x0000257C File Offset: 0x0000077C
	public int GetPointIndex(BezierPoint point)
	{
		int result = -1;
		for (int i = 0; i < this.points.Length; i++)
		{
			if (this.points[i] == point)
			{
				result = i;
				break;
			}
		}
		return result;
	}

	// Token: 0x06000019 RID: 25 RVA: 0x000025B3 File Offset: 0x000007B3
	public void SetDirty()
	{
		this.dirty = true;
	}

	// Token: 0x0600001A RID: 26 RVA: 0x000025BC File Offset: 0x000007BC
	public static void DrawCurve(BezierPoint p1, BezierPoint p2, int resolution)
	{
		int num = resolution + 1;
		float num2 = (float)resolution;
		Vector3 from = p1.position;
		Vector3 vector = Vector3.zero;
		for (int i = 1; i < num; i++)
		{
			vector = BezierCurve.GetPoint(p1, p2, (float)i / num2);
			Gizmos.DrawLine(from, vector);
			from = vector;
		}
	}

	// Token: 0x0600001B RID: 27 RVA: 0x00002604 File Offset: 0x00000804
	public static Vector3 GetPoint(BezierPoint p1, BezierPoint p2, float t)
	{
		if (!(p1 != null) || !(p2 != null))
		{
			return Vector3.zero;
		}
		if (p1.handle2 != Vector3.zero)
		{
			if (p2.handle1 != Vector3.zero)
			{
				return BezierCurve.GetCubicCurvePoint(p1.position, p1.globalHandle2, p2.globalHandle1, p2.position, t);
			}
			return BezierCurve.GetQuadraticCurvePoint(p1.position, p1.globalHandle2, p2.position, t);
		}
		else
		{
			if (p2.handle1 != Vector3.zero)
			{
				return BezierCurve.GetQuadraticCurvePoint(p1.position, p2.globalHandle1, p2.position, t);
			}
			return BezierCurve.GetLinearPoint(p1.position, p2.position, t);
		}
	}

	// Token: 0x0600001C RID: 28 RVA: 0x000026C8 File Offset: 0x000008C8
	public static Vector3 GetCubicCurvePoint(Vector3 p1, Vector3 p2, Vector3 p3, Vector3 p4, float t)
	{
		t = Mathf.Clamp01(t);
		Vector3 a = Mathf.Pow(1f - t, 3f) * p1;
		Vector3 b = 3f * Mathf.Pow(1f - t, 2f) * t * p2;
		Vector3 b2 = 3f * (1f - t) * Mathf.Pow(t, 2f) * p3;
		Vector3 b3 = Mathf.Pow(t, 3f) * p4;
		return a + b + b2 + b3;
	}

	// Token: 0x0600001D RID: 29 RVA: 0x00002760 File Offset: 0x00000960
	public static Vector3 GetQuadraticCurvePoint(Vector3 p1, Vector3 p2, Vector3 p3, float t)
	{
		t = Mathf.Clamp01(t);
		Vector3 a = Mathf.Pow(1f - t, 2f) * p1;
		Vector3 b = 2f * (1f - t) * t * p2;
		Vector3 b2 = Mathf.Pow(t, 2f) * p3;
		return a + b + b2;
	}

	// Token: 0x0600001E RID: 30 RVA: 0x000027C0 File Offset: 0x000009C0
	public static Vector3 GetLinearPoint(Vector3 p1, Vector3 p2, float t)
	{
		return p1 + (p2 - p1) * t;
	}

	// Token: 0x0600001F RID: 31 RVA: 0x000027D8 File Offset: 0x000009D8
	public static Vector3 GetPoint(float t, params Vector3[] points)
	{
		t = Mathf.Clamp01(t);
		int num = points.Length - 1;
		Vector3 vector = Vector3.zero;
		for (int i = 0; i < points.Length; i++)
		{
			Vector3 b = points[points.Length - i - 1] * ((float)BezierCurve.BinomialCoefficient(i, num) * Mathf.Pow(t, (float)(num - i)) * Mathf.Pow(1f - t, (float)i));
			vector += b;
		}
		return vector;
	}

	// Token: 0x06000020 RID: 32 RVA: 0x00002848 File Offset: 0x00000A48
	public static float ApproximateLength(BezierPoint p1, BezierPoint p2, int resolution = 10)
	{
		float num = (float)resolution;
		float num2 = 0f;
		Vector3 b = p1.position;
		for (int i = 0; i < resolution + 1; i++)
		{
			Vector3 point = BezierCurve.GetPoint(p1, p2, (float)i / num);
			num2 += (point - b).magnitude;
			b = point;
		}
		return num2;
	}

	// Token: 0x06000021 RID: 33 RVA: 0x0000289A File Offset: 0x00000A9A
	private static int BinomialCoefficient(int i, int n)
	{
		return BezierCurve.Factoral(n) / (BezierCurve.Factoral(i) * BezierCurve.Factoral(n - i));
	}

	// Token: 0x06000022 RID: 34 RVA: 0x000028B4 File Offset: 0x00000AB4
	private static int Factoral(int i)
	{
		if (i == 0)
		{
			return 1;
		}
		int num = 1;
		while (i - 1 >= 0)
		{
			num *= i;
			i--;
		}
		return num;
	}

	// Token: 0x0400000C RID: 12
	public int resolution = 30;

	// Token: 0x0400000E RID: 14
	public Color drawColor = Color.white;

	// Token: 0x0400000F RID: 15
	[SerializeField]
	private bool _close;

	// Token: 0x04000010 RID: 16
	private float _length;

	// Token: 0x04000011 RID: 17
	[SerializeField]
	private BezierPoint[] points = new BezierPoint[0];
}

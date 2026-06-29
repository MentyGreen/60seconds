using System;
using System.Linq;
using UnityEngine;

// Token: 0x020000F1 RID: 241
public static class Spline
{
	// Token: 0x06000BC3 RID: 3011 RVA: 0x00033E50 File Offset: 0x00032050
	public static Vector3 Interp(Spline.Path pts, float t, EasingType ease = EasingType.Linear, bool easeIn = true, bool easeOut = true)
	{
		t = Spline.Ease(t, ease, easeIn, easeOut);
		if (pts.Length == 0)
		{
			return Vector3.zero;
		}
		if (pts.Length == 1)
		{
			return pts[0];
		}
		if (pts.Length == 2)
		{
			return Vector3.Lerp(pts[0], pts[1], t);
		}
		if (pts.Length == 3)
		{
			return QuadBez.Interp(pts[0], pts[2], pts[1], t);
		}
		if (pts.Length == 4)
		{
			return CubicBez.Interp(pts[0], pts[3], pts[1], pts[2], t);
		}
		return CRSpline.Interp(Spline.Wrap(pts), t);
	}

	// Token: 0x06000BC4 RID: 3012 RVA: 0x00033F08 File Offset: 0x00032108
	private static float Ease(float t, EasingType ease = EasingType.Linear, bool easeIn = true, bool easeOut = true)
	{
		t = Mathf.Clamp01(t);
		if (easeIn && easeOut)
		{
			t = Easing.EaseInOut((double)t, ease);
		}
		else if (easeIn)
		{
			t = Easing.EaseIn((double)t, ease);
		}
		else if (easeOut)
		{
			t = Easing.EaseOut((double)t, ease);
		}
		return t;
	}

	// Token: 0x06000BC5 RID: 3013 RVA: 0x00033F40 File Offset: 0x00032140
	public static Vector3 InterpConstantSpeed(Spline.Path pts, float t, EasingType ease = EasingType.Linear, bool easeIn = true, bool easeOut = true)
	{
		t = Spline.Ease(t, ease, easeIn, easeOut);
		if (pts.Length == 0)
		{
			return Vector3.zero;
		}
		if (pts.Length == 1)
		{
			return pts[0];
		}
		if (pts.Length == 2)
		{
			return Vector3.Lerp(pts[0], pts[1], t);
		}
		if (pts.Length == 3)
		{
			return QuadBez.Interp(pts[0], pts[2], pts[1], t);
		}
		if (pts.Length == 4)
		{
			return CubicBez.Interp(pts[0], pts[3], pts[1], pts[2], t);
		}
		return CRSpline.InterpConstantSpeed(Spline.Wrap(pts), t);
	}

	// Token: 0x06000BC6 RID: 3014 RVA: 0x00033FF8 File Offset: 0x000321F8
	public static Vector3 MoveOnPath(Spline.Path pts, Vector3 currentPosition, ref float pathPosition, float maxSpeed = 1f, float smoothnessFactor = 100f, EasingType ease = EasingType.Linear, bool easeIn = true, bool easeOut = true)
	{
		maxSpeed *= Time.deltaTime;
		pathPosition = Mathf.Clamp01(pathPosition);
		Vector3 vector = Spline.Interp(pts, pathPosition, ease, easeIn, easeOut);
		float magnitude;
		while ((magnitude = (vector - currentPosition).magnitude) <= maxSpeed && pathPosition < 1f)
		{
			currentPosition = vector;
			maxSpeed -= magnitude;
			pathPosition = Mathf.Clamp01(pathPosition + 1f / smoothnessFactor);
			vector = Spline.Interp(pts, pathPosition, ease, easeIn, easeOut);
		}
		if (magnitude != 0f)
		{
			currentPosition = Vector3.MoveTowards(currentPosition, vector, maxSpeed);
		}
		return currentPosition;
	}

	// Token: 0x06000BC7 RID: 3015 RVA: 0x00034084 File Offset: 0x00032284
	public static Vector3 MoveOnPath(Spline.Path pts, Vector3 currentPosition, ref float pathPosition, ref Quaternion rotation, float maxSpeed = 1f, float smoothnessFactor = 100f, EasingType ease = EasingType.Linear, bool easeIn = true, bool easeOut = true)
	{
		Vector3 vector = Spline.MoveOnPath(pts, currentPosition, ref pathPosition, maxSpeed, smoothnessFactor, ease, easeIn, easeOut);
		rotation = (vector.Equals(currentPosition) ? Quaternion.identity : Quaternion.LookRotation(vector - currentPosition));
		return vector;
	}

	// Token: 0x06000BC8 RID: 3016 RVA: 0x000340C8 File Offset: 0x000322C8
	public static Quaternion RotationBetween(Spline.Path pts, float t1, float t2, EasingType ease = EasingType.Linear, bool easeIn = true, bool easeOut = true)
	{
		return Quaternion.LookRotation(Spline.Interp(pts, t2, ease, easeIn, easeOut) - Spline.Interp(pts, t1, ease, easeIn, easeOut));
	}

	// Token: 0x06000BC9 RID: 3017 RVA: 0x000340EC File Offset: 0x000322EC
	public static Vector3 Velocity(Spline.Path pts, float t, EasingType ease = EasingType.Linear, bool easeIn = true, bool easeOut = true)
	{
		t = Spline.Ease(t, EasingType.Linear, true, true);
		if (pts.Length == 0)
		{
			return Vector3.zero;
		}
		if (pts.Length == 1)
		{
			return pts[0];
		}
		if (pts.Length == 2)
		{
			return Vector3.Lerp(pts[0], pts[1], t);
		}
		if (pts.Length == 3)
		{
			return QuadBez.Velocity(pts[0], pts[2], pts[1], t);
		}
		if (pts.Length == 4)
		{
			return CubicBez.Velocity(pts[0], pts[3], pts[1], pts[2], t);
		}
		return CRSpline.Velocity(Spline.Wrap(pts), t);
	}

	// Token: 0x06000BCA RID: 3018 RVA: 0x000341A3 File Offset: 0x000323A3
	public static Vector3[] Wrap(Vector3[] path)
	{
		return new Vector3[]
		{
			path[0]
		}.Concat(path).Concat(new Vector3[]
		{
			path[path.Length - 1]
		}).ToArray<Vector3>();
	}

	// Token: 0x06000BCB RID: 3019 RVA: 0x000341E4 File Offset: 0x000323E4
	public static void GizmoDraw(Vector3[] pts, float t, EasingType ease = EasingType.Linear, bool easeIn = true, bool easeOut = true)
	{
		Gizmos.color = Color.white;
		Vector3 to = Spline.Interp(pts, 0f, EasingType.Linear, true, true);
		for (int i = 1; i <= 20; i++)
		{
			float t2 = (float)i / 20f;
			Vector3 vector = Spline.Interp(pts, t2, ease, easeIn, easeOut);
			Gizmos.DrawLine(vector, to);
			to = vector;
		}
		Gizmos.color = Color.blue;
		Vector3 vector2 = Spline.Interp(pts, t, ease, easeIn, easeOut);
		Gizmos.DrawLine(vector2, vector2 + Spline.Velocity(pts, t, ease, easeIn, easeOut));
	}

	// Token: 0x02000395 RID: 917
	public class Path
	{
		// Token: 0x1700051F RID: 1311
		// (get) Token: 0x06001D4B RID: 7499 RVA: 0x0007D4C5 File Offset: 0x0007B6C5
		// (set) Token: 0x06001D4C RID: 7500 RVA: 0x0007D4CD File Offset: 0x0007B6CD
		public Vector3[] path
		{
			get
			{
				return this._path;
			}
			set
			{
				this._path = value;
			}
		}

		// Token: 0x17000520 RID: 1312
		// (get) Token: 0x06001D4D RID: 7501 RVA: 0x0007D4D6 File Offset: 0x0007B6D6
		public int Length
		{
			get
			{
				if (this.path == null)
				{
					return 0;
				}
				return this.path.Length;
			}
		}

		// Token: 0x17000521 RID: 1313
		public Vector3 this[int index]
		{
			get
			{
				return this.path[index];
			}
		}

		// Token: 0x06001D4F RID: 7503 RVA: 0x0007D4F8 File Offset: 0x0007B6F8
		public static implicit operator Spline.Path(Vector3[] path)
		{
			return new Spline.Path
			{
				path = path
			};
		}

		// Token: 0x06001D50 RID: 7504 RVA: 0x0007D506 File Offset: 0x0007B706
		public static implicit operator Vector3[](Spline.Path p)
		{
			if (p == null)
			{
				return new Vector3[0];
			}
			return p.path;
		}

		// Token: 0x06001D51 RID: 7505 RVA: 0x0007D518 File Offset: 0x0007B718
		public static implicit operator Spline.Path(Transform[] path)
		{
			Spline.Path path2 = new Spline.Path();
			path2.path = (from p in path
			select p.position).ToArray<Vector3>();
			return path2;
		}

		// Token: 0x06001D52 RID: 7506 RVA: 0x0007D54F File Offset: 0x0007B74F
		public static implicit operator Spline.Path(GameObject[] path)
		{
			Spline.Path path2 = new Spline.Path();
			path2.path = (from p in path
			select p.transform.position).ToArray<Vector3>();
			return path2;
		}

		// Token: 0x040016BE RID: 5822
		private Vector3[] _path;
	}
}

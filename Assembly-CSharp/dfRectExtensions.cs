using System;
using UnityEngine;

// Token: 0x0200007C RID: 124
public static class dfRectExtensions
{
	// Token: 0x06000831 RID: 2097 RVA: 0x00023D04 File Offset: 0x00021F04
	public static RectOffset ConstrainPadding(this RectOffset borders)
	{
		if (borders == null)
		{
			return new RectOffset();
		}
		borders.left = Mathf.Max(0, borders.left);
		borders.right = Mathf.Max(0, borders.right);
		borders.top = Mathf.Max(0, borders.top);
		borders.bottom = Mathf.Max(0, borders.bottom);
		return borders;
	}

	// Token: 0x06000832 RID: 2098 RVA: 0x00023D63 File Offset: 0x00021F63
	public static bool IsEmpty(this Rect rect)
	{
		return rect.xMin == rect.xMax || rect.yMin == rect.yMax;
	}

	// Token: 0x06000833 RID: 2099 RVA: 0x00023D88 File Offset: 0x00021F88
	public static Rect Intersection(this Rect a, Rect b)
	{
		if (!a.Intersects(b))
		{
			return default(Rect);
		}
		float xmin = Mathf.Max(a.xMin, b.xMin);
		float xmax = Mathf.Min(a.xMax, b.xMax);
		float ymin = Mathf.Max(a.yMin, b.yMin);
		float ymax = Mathf.Min(a.yMax, b.yMax);
		return Rect.MinMaxRect(xmin, ymin, xmax, ymax);
	}

	// Token: 0x06000834 RID: 2100 RVA: 0x00023E00 File Offset: 0x00022000
	public static Rect Union(this Rect a, Rect b)
	{
		float xmin = Mathf.Min(a.xMin, b.xMin);
		float xmax = Mathf.Max(a.xMax, b.xMax);
		float ymin = Mathf.Min(a.yMin, b.yMin);
		float ymax = Mathf.Max(a.yMax, b.yMax);
		return Rect.MinMaxRect(xmin, ymin, xmax, ymax);
	}

	// Token: 0x06000835 RID: 2101 RVA: 0x00023E64 File Offset: 0x00022064
	public static bool Contains(this Rect rect, Rect other)
	{
		bool flag = rect.x <= other.x;
		bool flag2 = rect.x + rect.width >= other.x + other.width;
		bool flag3 = rect.yMin <= other.yMin;
		bool flag4 = rect.y + rect.height >= other.y + other.height;
		return flag && flag2 && flag3 && flag4;
	}

	// Token: 0x06000836 RID: 2102 RVA: 0x00023EE8 File Offset: 0x000220E8
	public static bool Intersects(this Rect rect, Rect other)
	{
		return rect.xMax >= other.xMin && rect.yMax >= other.yMin && rect.xMin <= other.xMax && rect.yMin <= other.yMax;
	}

	// Token: 0x06000837 RID: 2103 RVA: 0x00023F3B File Offset: 0x0002213B
	public static Rect RoundToInt(this Rect rect)
	{
		return new Rect((float)Mathf.RoundToInt(rect.x), (float)Mathf.RoundToInt(rect.y), (float)Mathf.RoundToInt(rect.width), (float)Mathf.RoundToInt(rect.height));
	}

	// Token: 0x06000838 RID: 2104 RVA: 0x00023F78 File Offset: 0x00022178
	public static string Debug(this Rect rect)
	{
		return string.Format("[{0},{1},{2},{3}]", new object[]
		{
			rect.xMin,
			rect.yMin,
			rect.xMax,
			rect.yMax
		});
	}
}

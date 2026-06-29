using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200003A RID: 58
internal class dfClippingUtil
{
	// Token: 0x060005F6 RID: 1526 RVA: 0x0001C02C File Offset: 0x0001A22C
	public static void Clip(IList<Plane> planes, dfRenderData source, dfRenderData dest)
	{
		dest.EnsureCapacity(dest.Vertices.Count + source.Vertices.Count);
		int count = source.Triangles.Count;
		Vector3[] items = source.Vertices.Items;
		int[] items2 = source.Triangles.Items;
		Vector2[] items3 = source.UV.Items;
		Color32[] items4 = source.Colors.Items;
		Matrix4x4 transform = source.Transform;
		int count2 = planes.Count;
		for (int i = 0; i < count; i += 3)
		{
			for (int j = 0; j < 3; j++)
			{
				int num = items2[i + j];
				dfClippingUtil.clipSource[0].corner[j] = transform.MultiplyPoint(items[num]);
				dfClippingUtil.clipSource[0].uv[j] = items3[num];
				dfClippingUtil.clipSource[0].color[j] = items4[num];
			}
			int num2 = 1;
			for (int k = 0; k < count2; k++)
			{
				Plane plane = planes[k];
				num2 = dfClippingUtil.clipToPlane(ref plane, dfClippingUtil.clipSource, dfClippingUtil.clipDest, num2);
				dfClippingUtil.ClipTriangle[] array = dfClippingUtil.clipSource;
				dfClippingUtil.clipSource = dfClippingUtil.clipDest;
				dfClippingUtil.clipDest = array;
			}
			for (int l = 0; l < num2; l++)
			{
				dfClippingUtil.clipSource[l].CopyTo(dest);
			}
		}
	}

	// Token: 0x060005F7 RID: 1527 RVA: 0x0001C1A4 File Offset: 0x0001A3A4
	private static int clipToPlane(ref Plane plane, dfClippingUtil.ClipTriangle[] source, dfClippingUtil.ClipTriangle[] dest, int count)
	{
		int num = 0;
		for (int i = 0; i < count; i++)
		{
			num += dfClippingUtil.clipToPlane(ref plane, ref source[i], dest, num);
		}
		return num;
	}

	// Token: 0x060005F8 RID: 1528 RVA: 0x0001C1D4 File Offset: 0x0001A3D4
	private static int clipToPlane(ref Plane plane, ref dfClippingUtil.ClipTriangle triangle, dfClippingUtil.ClipTriangle[] dest, int destIndex)
	{
		Vector3[] corner = triangle.corner;
		int num = 0;
		int num2 = 0;
		Vector3 normal = plane.normal;
		float distance = plane.distance;
		for (int i = 0; i < 3; i++)
		{
			if (Vector3.Dot(normal, corner[i]) + distance > 0f)
			{
				dfClippingUtil.inside[num++] = i;
			}
			else
			{
				num2 = i;
			}
		}
		if (num == 3)
		{
			dfClippingUtil.ClipTriangle clipTriangle = dest[destIndex];
			Array.Copy(triangle.corner, 0, clipTriangle.corner, 0, 3);
			Array.Copy(triangle.uv, 0, clipTriangle.uv, 0, 3);
			Array.Copy(triangle.color, 0, clipTriangle.color, 0, 3);
			return 1;
		}
		if (num == 0)
		{
			return 0;
		}
		if (num == 1)
		{
			int num3 = dfClippingUtil.inside[0];
			int num4 = (num3 + 1) % 3;
			int num5 = (num3 + 2) % 3;
			Vector3 vector = corner[num3];
			Vector3 a = corner[num4];
			Vector3 a2 = corner[num5];
			Vector2 vector2 = triangle.uv[num3];
			Vector2 b = triangle.uv[num4];
			Vector2 b2 = triangle.uv[num5];
			Color32 color = triangle.color[num3];
			Color32 b3 = triangle.color[num4];
			Color32 b4 = triangle.color[num5];
			float num6 = 0f;
			Vector3 vector3 = a - vector;
			Ray ray = new Ray(vector, vector3.normalized);
			plane.Raycast(ray, out num6);
			float t = num6 / vector3.magnitude;
			Vector3 point = ray.GetPoint(num6);
			Vector2 vector4 = Vector2.Lerp(vector2, b, t);
			Color32 color2 = Color32.Lerp(color, b3, t);
			vector3 = a2 - vector;
			ray = new Ray(vector, vector3.normalized);
			plane.Raycast(ray, out num6);
			t = num6 / vector3.magnitude;
			Vector3 point2 = ray.GetPoint(num6);
			Vector2 vector5 = Vector2.Lerp(vector2, b2, t);
			Color32 color3 = Color32.Lerp(color, b4, t);
			dfClippingUtil.ClipTriangle clipTriangle2 = dest[destIndex];
			clipTriangle2.corner[0] = vector;
			clipTriangle2.corner[1] = point;
			clipTriangle2.corner[2] = point2;
			clipTriangle2.uv[0] = vector2;
			clipTriangle2.uv[1] = vector4;
			clipTriangle2.uv[2] = vector5;
			clipTriangle2.color[0] = color;
			clipTriangle2.color[1] = color2;
			clipTriangle2.color[2] = color3;
			return 1;
		}
		int num7 = num2;
		int num8 = (num7 + 1) % 3;
		int num9 = (num7 + 2) % 3;
		Vector3 vector6 = corner[num7];
		Vector3 vector7 = corner[num8];
		Vector3 vector8 = corner[num9];
		Vector2 a3 = triangle.uv[num7];
		Vector2 vector9 = triangle.uv[num8];
		Vector2 vector10 = triangle.uv[num9];
		Color32 a4 = triangle.color[num7];
		Color32 color4 = triangle.color[num8];
		Color32 color5 = triangle.color[num9];
		Vector3 vector11 = vector7 - vector6;
		Ray ray2 = new Ray(vector6, vector11.normalized);
		float num10 = 0f;
		plane.Raycast(ray2, out num10);
		float t2 = num10 / vector11.magnitude;
		Vector3 point3 = ray2.GetPoint(num10);
		Vector2 vector12 = Vector2.Lerp(a3, vector9, t2);
		Color32 color6 = Color32.Lerp(a4, color4, t2);
		vector11 = vector8 - vector6;
		ray2 = new Ray(vector6, vector11.normalized);
		plane.Raycast(ray2, out num10);
		t2 = num10 / vector11.magnitude;
		Vector3 point4 = ray2.GetPoint(num10);
		Vector2 vector13 = Vector2.Lerp(a3, vector10, t2);
		Color32 color7 = Color32.Lerp(a4, color5, t2);
		dfClippingUtil.ClipTriangle clipTriangle3 = dest[destIndex];
		clipTriangle3.corner[0] = point3;
		clipTriangle3.corner[1] = vector7;
		clipTriangle3.corner[2] = point4;
		clipTriangle3.uv[0] = vector12;
		clipTriangle3.uv[1] = vector9;
		clipTriangle3.uv[2] = vector13;
		clipTriangle3.color[0] = color6;
		clipTriangle3.color[1] = color4;
		clipTriangle3.color[2] = color7;
		dfClippingUtil.ClipTriangle clipTriangle4 = dest[++destIndex];
		clipTriangle4.corner[0] = point4;
		clipTriangle4.corner[1] = vector7;
		clipTriangle4.corner[2] = vector8;
		clipTriangle4.uv[0] = vector13;
		clipTriangle4.uv[1] = vector9;
		clipTriangle4.uv[2] = vector10;
		clipTriangle4.color[0] = color7;
		clipTriangle4.color[1] = color4;
		clipTriangle4.color[2] = color5;
		return 2;
	}

	// Token: 0x060005F9 RID: 1529 RVA: 0x0001C6A0 File Offset: 0x0001A8A0
	private static dfClippingUtil.ClipTriangle[] initClipBuffer(int size)
	{
		dfClippingUtil.ClipTriangle[] array = new dfClippingUtil.ClipTriangle[size];
		for (int i = 0; i < size; i++)
		{
			array[i].corner = new Vector3[3];
			array[i].uv = new Vector2[3];
			array[i].color = new Color32[3];
		}
		return array;
	}

	// Token: 0x0400021C RID: 540
	private static int[] inside = new int[3];

	// Token: 0x0400021D RID: 541
	private static dfClippingUtil.ClipTriangle[] clipSource = dfClippingUtil.initClipBuffer(1024);

	// Token: 0x0400021E RID: 542
	private static dfClippingUtil.ClipTriangle[] clipDest = dfClippingUtil.initClipBuffer(1024);

	// Token: 0x02000368 RID: 872
	protected struct ClipTriangle
	{
		// Token: 0x06001C66 RID: 7270 RVA: 0x00079434 File Offset: 0x00077634
		public void CopyTo(ref dfClippingUtil.ClipTriangle target)
		{
			Array.Copy(this.corner, 0, target.corner, 0, 3);
			Array.Copy(this.uv, 0, target.uv, 0, 3);
			Array.Copy(this.color, 0, target.color, 0, 3);
		}

		// Token: 0x06001C67 RID: 7271 RVA: 0x00079474 File Offset: 0x00077674
		public void CopyTo(dfRenderData buffer)
		{
			int count = buffer.Vertices.Count;
			buffer.Vertices.AddRange(this.corner);
			buffer.UV.AddRange(this.uv);
			buffer.Colors.AddRange(this.color);
			buffer.Triangles.Add(count, count + 1, count + 2);
		}

		// Token: 0x0400162F RID: 5679
		public Vector3[] corner;

		// Token: 0x04001630 RID: 5680
		public Vector2[] uv;

		// Token: 0x04001631 RID: 5681
		public Color32[] color;
	}
}

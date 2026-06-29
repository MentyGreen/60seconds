using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000070 RID: 112
public class dfRenderData : IDisposable
{
	// Token: 0x060007E6 RID: 2022 RVA: 0x000223F8 File Offset: 0x000205F8
	internal dfRenderData() : this(32)
	{
	}

	// Token: 0x060007E7 RID: 2023 RVA: 0x00022404 File Offset: 0x00020604
	internal dfRenderData(int capacity)
	{
		this.Vertices = new dfList<Vector3>(capacity);
		this.Triangles = new dfList<int>(capacity);
		this.Normals = new dfList<Vector3>(capacity);
		this.Tangents = new dfList<Vector4>(capacity);
		this.UV = new dfList<Vector2>(capacity);
		this.Colors = new dfList<Color32>(capacity);
		this.Transform = Matrix4x4.identity;
	}

	// Token: 0x060007E8 RID: 2024 RVA: 0x0002246C File Offset: 0x0002066C
	public static dfRenderData Obtain()
	{
		Queue<dfRenderData> obj = dfRenderData.pool;
		dfRenderData result;
		lock (obj)
		{
			result = ((dfRenderData.pool.Count > 0) ? dfRenderData.pool.Dequeue() : new dfRenderData());
		}
		return result;
	}

	// Token: 0x060007E9 RID: 2025 RVA: 0x000224C8 File Offset: 0x000206C8
	public static void FlushObjectPool()
	{
		Queue<dfRenderData> obj = dfRenderData.pool;
		lock (obj)
		{
			while (dfRenderData.pool.Count > 0)
			{
				dfRenderData dfRenderData = dfRenderData.pool.Dequeue();
				dfRenderData.Vertices.TrimExcess();
				dfRenderData.Triangles.TrimExcess();
				dfRenderData.UV.TrimExcess();
				dfRenderData.Colors.TrimExcess();
			}
		}
	}

	// Token: 0x060007EA RID: 2026 RVA: 0x00022548 File Offset: 0x00020748
	public void Release()
	{
		Queue<dfRenderData> obj = dfRenderData.pool;
		lock (obj)
		{
			this.Clear();
			dfRenderData.pool.Enqueue(this);
		}
	}

	// Token: 0x060007EB RID: 2027 RVA: 0x00022594 File Offset: 0x00020794
	public void Clear()
	{
		this.Material = null;
		this.Shader = null;
		this.Transform = Matrix4x4.identity;
		this.Checksum = 0U;
		this.Intersection = dfIntersectionType.None;
		this.Vertices.Clear();
		this.UV.Clear();
		this.Triangles.Clear();
		this.Colors.Clear();
		this.Normals.Clear();
		this.Tangents.Clear();
	}

	// Token: 0x060007EC RID: 2028 RVA: 0x0002260C File Offset: 0x0002080C
	public bool IsValid()
	{
		int count = this.Vertices.Count;
		return count > 0 && count <= 65000 && this.UV.Count == count && this.Colors.Count == count;
	}

	// Token: 0x060007ED RID: 2029 RVA: 0x00022650 File Offset: 0x00020850
	public void EnsureCapacity(int capacity)
	{
		this.Vertices.EnsureCapacity(capacity);
		this.Triangles.EnsureCapacity(Mathf.RoundToInt((float)capacity * 1.5f));
		this.UV.EnsureCapacity(capacity);
		this.Colors.EnsureCapacity(capacity);
		if (this.Normals != null)
		{
			this.Normals.EnsureCapacity(capacity);
		}
		if (this.Tangents != null)
		{
			this.Tangents.EnsureCapacity(capacity);
		}
	}

	// Token: 0x060007EE RID: 2030 RVA: 0x000226C1 File Offset: 0x000208C1
	public void Merge(dfRenderData buffer)
	{
		this.Merge(buffer, true);
	}

	// Token: 0x060007EF RID: 2031 RVA: 0x000226CC File Offset: 0x000208CC
	public void Merge(dfRenderData buffer, bool transformVertices)
	{
		int count = this.Vertices.Count;
		this.Vertices.AddRange(buffer.Vertices);
		if (transformVertices)
		{
			Vector3[] items = this.Vertices.Items;
			int count2 = buffer.Vertices.Count;
			Matrix4x4 transform = buffer.Transform;
			for (int i = count; i < count + count2; i++)
			{
				items[i] = transform.MultiplyPoint(items[i]);
			}
		}
		int count3 = this.Triangles.Count;
		this.Triangles.AddRange(buffer.Triangles);
		int count4 = buffer.Triangles.Count;
		int[] items2 = this.Triangles.Items;
		for (int j = count3; j < count3 + count4; j++)
		{
			items2[j] += count;
		}
		this.UV.AddRange(buffer.UV);
		this.Colors.AddRange(buffer.Colors);
		this.Normals.AddRange(buffer.Normals);
		this.Tangents.AddRange(buffer.Tangents);
	}

	// Token: 0x060007F0 RID: 2032 RVA: 0x000227E0 File Offset: 0x000209E0
	internal void ApplyTransform(Matrix4x4 transform)
	{
		int count = this.Vertices.Count;
		Vector3[] items = this.Vertices.Items;
		for (int i = 0; i < count; i++)
		{
			items[i] = transform.MultiplyPoint(items[i]);
		}
		if (this.Normals.Count > 0)
		{
			Vector3[] items2 = this.Normals.Items;
			for (int j = 0; j < count; j++)
			{
				items2[j] = transform.MultiplyVector(items2[j]);
			}
		}
	}

	// Token: 0x060007F1 RID: 2033 RVA: 0x00022868 File Offset: 0x00020A68
	public override string ToString()
	{
		return string.Format("V:{0} T:{1} U:{2} C:{3}", new object[]
		{
			this.Vertices.Count,
			this.Triangles.Count,
			this.UV.Count,
			this.Colors.Count
		});
	}

	// Token: 0x060007F2 RID: 2034 RVA: 0x000228D1 File Offset: 0x00020AD1
	public void Dispose()
	{
		this.Release();
	}

	// Token: 0x040003B8 RID: 952
	private static Queue<dfRenderData> pool = new Queue<dfRenderData>();

	// Token: 0x040003B9 RID: 953
	public Material Material;

	// Token: 0x040003BA RID: 954
	public Shader Shader;

	// Token: 0x040003BB RID: 955
	public Matrix4x4 Transform;

	// Token: 0x040003BC RID: 956
	public dfList<Vector3> Vertices;

	// Token: 0x040003BD RID: 957
	public dfList<Vector2> UV;

	// Token: 0x040003BE RID: 958
	public dfList<Vector3> Normals;

	// Token: 0x040003BF RID: 959
	public dfList<Vector4> Tangents;

	// Token: 0x040003C0 RID: 960
	public dfList<int> Triangles;

	// Token: 0x040003C1 RID: 961
	public dfList<Color32> Colors;

	// Token: 0x040003C2 RID: 962
	public uint Checksum;

	// Token: 0x040003C3 RID: 963
	public dfIntersectionType Intersection;
}

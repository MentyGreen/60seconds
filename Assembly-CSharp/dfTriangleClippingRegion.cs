using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200003B RID: 59
internal class dfTriangleClippingRegion : IDisposable
{
	// Token: 0x060005FB RID: 1531 RVA: 0x0001C6FF File Offset: 0x0001A8FF
	public static dfTriangleClippingRegion Obtain()
	{
		if (dfTriangleClippingRegion.pool.Count <= 0)
		{
			return new dfTriangleClippingRegion();
		}
		return dfTriangleClippingRegion.pool.Dequeue();
	}

	// Token: 0x060005FC RID: 1532 RVA: 0x0001C720 File Offset: 0x0001A920
	public static dfTriangleClippingRegion Obtain(dfTriangleClippingRegion parent, dfControl control)
	{
		dfTriangleClippingRegion dfTriangleClippingRegion = (dfTriangleClippingRegion.pool.Count > 0) ? dfTriangleClippingRegion.pool.Dequeue() : new dfTriangleClippingRegion();
		dfTriangleClippingRegion.planes.AddRange(control.GetClippingPlanes());
		if (parent != null)
		{
			dfTriangleClippingRegion.planes.AddRange(parent.planes);
		}
		return dfTriangleClippingRegion;
	}

	// Token: 0x060005FD RID: 1533 RVA: 0x0001C772 File Offset: 0x0001A972
	public void Release()
	{
		this.planes.Clear();
		if (!dfTriangleClippingRegion.pool.Contains(this))
		{
			dfTriangleClippingRegion.pool.Enqueue(this);
		}
	}

	// Token: 0x060005FE RID: 1534 RVA: 0x0001C797 File Offset: 0x0001A997
	private dfTriangleClippingRegion()
	{
		this.planes = new dfList<Plane>();
	}

	// Token: 0x060005FF RID: 1535 RVA: 0x0001C7AC File Offset: 0x0001A9AC
	public bool PerformClipping(dfRenderData dest, ref Bounds bounds, uint checksum, dfRenderData controlData)
	{
		if (this.planes == null || this.planes.Count == 0)
		{
			dest.Merge(controlData);
			return true;
		}
		if (controlData.Checksum == checksum)
		{
			if (controlData.Intersection == dfIntersectionType.Inside)
			{
				dest.Merge(controlData);
				return true;
			}
			if (controlData.Intersection == dfIntersectionType.None)
			{
				return false;
			}
		}
		bool result = false;
		dfIntersectionType dfIntersectionType;
		dfList<Plane> dfList = this.TestIntersection(bounds, out dfIntersectionType);
		if (dfIntersectionType == dfIntersectionType.Inside)
		{
			dest.Merge(controlData);
			result = true;
		}
		else if (dfIntersectionType == dfIntersectionType.Intersecting)
		{
			this.clipToPlanes(dfList, controlData, dest, checksum);
			result = true;
		}
		controlData.Checksum = checksum;
		controlData.Intersection = dfIntersectionType;
		return result;
	}

	// Token: 0x06000600 RID: 1536 RVA: 0x0001C844 File Offset: 0x0001AA44
	public dfList<Plane> TestIntersection(Bounds bounds, out dfIntersectionType type)
	{
		if (this.planes == null || this.planes.Count == 0)
		{
			type = dfIntersectionType.Inside;
			return null;
		}
		dfTriangleClippingRegion.intersectedPlanes.Clear();
		Vector3 center = bounds.center;
		Vector3 extents = bounds.extents;
		bool flag = false;
		int count = this.planes.Count;
		Plane[] items = this.planes.Items;
		for (int i = 0; i < count; i++)
		{
			Plane item = items[i];
			Vector3 normal = item.normal;
			float distance = item.distance;
			float num = extents.x * Mathf.Abs(normal.x) + extents.y * Mathf.Abs(normal.y) + extents.z * Mathf.Abs(normal.z);
			float num2 = Vector3.Dot(normal, center) + distance;
			if (Mathf.Abs(num2) <= num)
			{
				flag = true;
				dfTriangleClippingRegion.intersectedPlanes.Add(item);
			}
			else if (num2 < -num)
			{
				type = dfIntersectionType.None;
				return null;
			}
		}
		if (flag)
		{
			type = dfIntersectionType.Intersecting;
			return dfTriangleClippingRegion.intersectedPlanes;
		}
		type = dfIntersectionType.Inside;
		return null;
	}

	// Token: 0x06000601 RID: 1537 RVA: 0x0001C954 File Offset: 0x0001AB54
	public void clipToPlanes(dfList<Plane> planes, dfRenderData data, dfRenderData dest, uint controlChecksum)
	{
		if (data == null || data.Vertices.Count == 0)
		{
			return;
		}
		if (planes == null || planes.Count == 0)
		{
			dest.Merge(data);
			return;
		}
		dfClippingUtil.Clip(planes, data, dest);
	}

	// Token: 0x06000602 RID: 1538 RVA: 0x0001C982 File Offset: 0x0001AB82
	public void Dispose()
	{
		this.Release();
	}

	// Token: 0x0400021F RID: 543
	private static Queue<dfTriangleClippingRegion> pool = new Queue<dfTriangleClippingRegion>();

	// Token: 0x04000220 RID: 544
	private static dfList<Plane> intersectedPlanes = new dfList<Plane>(32);

	// Token: 0x04000221 RID: 545
	private dfList<Plane> planes;
}

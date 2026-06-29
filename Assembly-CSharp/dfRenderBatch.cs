using System;
using UnityEngine;

// Token: 0x0200006F RID: 111
internal class dfRenderBatch
{
	// Token: 0x060007E3 RID: 2019 RVA: 0x00022282 File Offset: 0x00020482
	public void Add(dfRenderData buffer)
	{
		if (this.Material == null && buffer.Material != null)
		{
			this.Material = buffer.Material;
		}
		this.buffers.Add(buffer);
	}

	// Token: 0x060007E4 RID: 2020 RVA: 0x000222B8 File Offset: 0x000204B8
	public dfRenderData Combine()
	{
		dfRenderData dfRenderData = dfRenderData.Obtain();
		int count = this.buffers.Count;
		dfRenderData[] items = this.buffers.Items;
		if (count == 0)
		{
			return dfRenderData;
		}
		dfRenderData.Material = this.buffers[0].Material;
		int capacity = 0;
		for (int i = 0; i < count; i++)
		{
			capacity = items[i].Vertices.Count;
		}
		dfRenderData.EnsureCapacity(capacity);
		int[] items2 = dfRenderData.Triangles.Items;
		for (int j = 0; j < count; j++)
		{
			dfRenderData dfRenderData2 = items[j];
			int count2 = dfRenderData.Vertices.Count;
			int count3 = dfRenderData.Triangles.Count;
			int count4 = dfRenderData2.Triangles.Count;
			dfRenderData.Vertices.AddRange(dfRenderData2.Vertices);
			dfRenderData.Triangles.AddRange(dfRenderData2.Triangles);
			dfRenderData.Colors.AddRange(dfRenderData2.Colors);
			dfRenderData.UV.AddRange(dfRenderData2.UV);
			for (int k = count3; k < count3 + count4; k++)
			{
				items2[k] += count2;
			}
		}
		return dfRenderData;
	}

	// Token: 0x040003B6 RID: 950
	public Material Material;

	// Token: 0x040003B7 RID: 951
	private dfList<dfRenderData> buffers = new dfList<dfRenderData>();
}

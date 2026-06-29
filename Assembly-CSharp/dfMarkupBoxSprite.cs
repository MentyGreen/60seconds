using System;
using UnityEngine;

// Token: 0x02000081 RID: 129
public class dfMarkupBoxSprite : dfMarkupBox
{
	// Token: 0x170001E6 RID: 486
	// (get) Token: 0x06000861 RID: 2145 RVA: 0x0002526B File Offset: 0x0002346B
	// (set) Token: 0x06000862 RID: 2146 RVA: 0x00025273 File Offset: 0x00023473
	public dfAtlas Atlas { get; set; }

	// Token: 0x170001E7 RID: 487
	// (get) Token: 0x06000863 RID: 2147 RVA: 0x0002527C File Offset: 0x0002347C
	// (set) Token: 0x06000864 RID: 2148 RVA: 0x00025284 File Offset: 0x00023484
	public string Source { get; set; }

	// Token: 0x06000865 RID: 2149 RVA: 0x0002528D File Offset: 0x0002348D
	public dfMarkupBoxSprite(dfMarkupElement element, dfMarkupDisplayType display, dfMarkupStyle style) : base(element, display, style)
	{
	}

	// Token: 0x06000866 RID: 2150 RVA: 0x000252A4 File Offset: 0x000234A4
	internal void LoadImage(dfAtlas atlas, string source)
	{
		dfAtlas.ItemInfo itemInfo = atlas[source];
		if (itemInfo == null)
		{
			throw new InvalidOperationException("Sprite does not exist in atlas: " + source);
		}
		this.Atlas = atlas;
		this.Source = source;
		this.Size = itemInfo.sizeInPixels;
		this.Baseline = (int)this.Size.y;
	}

	// Token: 0x06000867 RID: 2151 RVA: 0x00025300 File Offset: 0x00023500
	protected override dfRenderData OnRebuildRenderData()
	{
		this.renderData.Clear();
		if (this.Atlas != null && this.Atlas[this.Source] != null)
		{
			dfSprite.RenderOptions options = new dfSprite.RenderOptions
			{
				atlas = this.Atlas,
				spriteInfo = this.Atlas[this.Source],
				pixelsToUnits = 1f,
				size = this.Size,
				color = this.Style.Color,
				baseIndex = 0,
				fillAmount = 1f,
				flip = dfSpriteFlip.None
			};
			dfSlicedSprite.renderSprite(this.renderData, options);
			this.renderData.Material = this.Atlas.Material;
			this.renderData.Transform = Matrix4x4.identity;
		}
		return this.renderData;
	}

	// Token: 0x06000868 RID: 2152 RVA: 0x000253F8 File Offset: 0x000235F8
	private static void addTriangleIndices(dfList<Vector3> verts, dfList<int> triangles)
	{
		int count = verts.Count;
		int[] triangle_INDICES = dfMarkupBoxSprite.TRIANGLE_INDICES;
		for (int i = 0; i < triangle_INDICES.Length; i++)
		{
			triangles.Add(count + triangle_INDICES[i]);
		}
	}

	// Token: 0x04000405 RID: 1029
	private static int[] TRIANGLE_INDICES = new int[]
	{
		0,
		1,
		2,
		0,
		2,
		3
	};

	// Token: 0x04000408 RID: 1032
	private dfRenderData renderData = new dfRenderData();
}

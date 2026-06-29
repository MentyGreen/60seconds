using System;
using UnityEngine;

// Token: 0x02000018 RID: 24
[dfCategory("Basic Controls")]
[dfTooltip("Used to render a sprite from a Texture Atlas on the screen")]
[dfHelp("http://www.daikonforge.com/docs/df-gui/classdf_sprite.html")]
[ExecuteInEditMode]
[AddComponentMenu("Daikon Forge/User Interface/Sprite/Basic")]
[Serializable]
public class dfSprite : dfControl
{
	// Token: 0x14000034 RID: 52
	// (add) Token: 0x060003ED RID: 1005 RVA: 0x00014030 File Offset: 0x00012230
	// (remove) Token: 0x060003EE RID: 1006 RVA: 0x00014068 File Offset: 0x00012268
	public event PropertyChangedEventHandler<string> SpriteNameChanged;

	// Token: 0x170000EA RID: 234
	// (get) Token: 0x060003EF RID: 1007 RVA: 0x000140A0 File Offset: 0x000122A0
	// (set) Token: 0x060003F0 RID: 1008 RVA: 0x000140E1 File Offset: 0x000122E1
	public dfAtlas Atlas
	{
		get
		{
			if (this.atlas == null)
			{
				dfGUIManager manager = base.GetManager();
				if (manager != null)
				{
					return this.atlas = manager.DefaultAtlas;
				}
			}
			return this.atlas;
		}
		set
		{
			if (!dfAtlas.Equals(value, this.atlas))
			{
				this.atlas = value;
				this.Invalidate();
			}
		}
	}

	// Token: 0x170000EB RID: 235
	// (get) Token: 0x060003F1 RID: 1009 RVA: 0x000140FE File Offset: 0x000122FE
	// (set) Token: 0x060003F2 RID: 1010 RVA: 0x00014108 File Offset: 0x00012308
	public string SpriteName
	{
		get
		{
			return this.spriteName;
		}
		set
		{
			value = base.getLocalizedValue(value);
			if (value != this.spriteName)
			{
				this.spriteName = value;
				dfAtlas.ItemInfo spriteInfo = this.SpriteInfo;
				if (this.size == Vector2.zero && spriteInfo != null)
				{
					this.size = spriteInfo.sizeInPixels;
					base.updateCollider();
				}
				this.Invalidate();
				this.OnSpriteNameChanged(value);
			}
		}
	}

	// Token: 0x170000EC RID: 236
	// (get) Token: 0x060003F3 RID: 1011 RVA: 0x00014174 File Offset: 0x00012374
	public dfAtlas.ItemInfo SpriteInfo
	{
		get
		{
			if (this.Atlas == null)
			{
				return null;
			}
			return this.Atlas[this.spriteName];
		}
	}

	// Token: 0x170000ED RID: 237
	// (get) Token: 0x060003F4 RID: 1012 RVA: 0x00014197 File Offset: 0x00012397
	// (set) Token: 0x060003F5 RID: 1013 RVA: 0x0001419F File Offset: 0x0001239F
	public dfSpriteFlip Flip
	{
		get
		{
			return this.flip;
		}
		set
		{
			if (value != this.flip)
			{
				this.flip = value;
				this.Invalidate();
			}
		}
	}

	// Token: 0x170000EE RID: 238
	// (get) Token: 0x060003F6 RID: 1014 RVA: 0x000141B7 File Offset: 0x000123B7
	// (set) Token: 0x060003F7 RID: 1015 RVA: 0x000141BF File Offset: 0x000123BF
	public dfFillDirection FillDirection
	{
		get
		{
			return this.fillDirection;
		}
		set
		{
			if (value != this.fillDirection)
			{
				this.fillDirection = value;
				this.Invalidate();
			}
		}
	}

	// Token: 0x170000EF RID: 239
	// (get) Token: 0x060003F8 RID: 1016 RVA: 0x000141D7 File Offset: 0x000123D7
	// (set) Token: 0x060003F9 RID: 1017 RVA: 0x000141DF File Offset: 0x000123DF
	public float FillAmount
	{
		get
		{
			return this.fillAmount;
		}
		set
		{
			if (!Mathf.Approximately(value, this.fillAmount))
			{
				this.fillAmount = Mathf.Max(0f, Mathf.Min(1f, value));
				this.Invalidate();
			}
		}
	}

	// Token: 0x170000F0 RID: 240
	// (get) Token: 0x060003FA RID: 1018 RVA: 0x00014210 File Offset: 0x00012410
	// (set) Token: 0x060003FB RID: 1019 RVA: 0x00014218 File Offset: 0x00012418
	public bool InvertFill
	{
		get
		{
			return this.invertFill;
		}
		set
		{
			if (value != this.invertFill)
			{
				this.invertFill = value;
				this.Invalidate();
			}
		}
	}

	// Token: 0x060003FC RID: 1020 RVA: 0x00014230 File Offset: 0x00012430
	protected internal override void OnLocalize()
	{
		base.OnLocalize();
		this.SpriteName = base.getLocalizedValue(this.spriteName);
	}

	// Token: 0x060003FD RID: 1021 RVA: 0x0001424A File Offset: 0x0001244A
	protected internal virtual void OnSpriteNameChanged(string value)
	{
		base.Signal("OnSpriteNameChanged", this, value);
		if (this.SpriteNameChanged != null)
		{
			this.SpriteNameChanged(this, value);
		}
	}

	// Token: 0x060003FE RID: 1022 RVA: 0x00014270 File Offset: 0x00012470
	public override Vector2 CalculateMinimumSize()
	{
		dfAtlas.ItemInfo spriteInfo = this.SpriteInfo;
		if (spriteInfo == null)
		{
			return Vector2.zero;
		}
		RectOffset border = spriteInfo.border;
		if (border != null && border.horizontal > 0 && border.vertical > 0)
		{
			return Vector2.Max(base.CalculateMinimumSize(), new Vector2((float)border.horizontal, (float)border.vertical));
		}
		return base.CalculateMinimumSize();
	}

	// Token: 0x060003FF RID: 1023 RVA: 0x000142D4 File Offset: 0x000124D4
	protected override void OnRebuildRenderData()
	{
		if (!(this.Atlas != null) || !(this.Atlas.Material != null))
		{
			return;
		}
		if (this.SpriteInfo == null)
		{
			return;
		}
		this.renderData.Material = this.Atlas.Material;
		Color32 color = base.ApplyOpacity(base.IsEnabled ? this.color : this.disabledColor);
		dfSprite.RenderOptions options = new dfSprite.RenderOptions
		{
			atlas = this.Atlas,
			color = color,
			fillAmount = this.fillAmount,
			fillDirection = this.fillDirection,
			flip = this.flip,
			invertFill = this.invertFill,
			offset = this.pivot.TransformToUpperLeft(base.Size),
			pixelsToUnits = base.PixelsToUnits(),
			size = base.Size,
			spriteInfo = this.SpriteInfo
		};
		dfSprite.renderSprite(this.renderData, options);
	}

	// Token: 0x06000400 RID: 1024 RVA: 0x000143E8 File Offset: 0x000125E8
	internal static void renderSprite(dfRenderData data, dfSprite.RenderOptions options)
	{
		if (options.fillAmount <= 1E-45f)
		{
			return;
		}
		options.baseIndex = data.Vertices.Count;
		dfSprite.rebuildTriangles(data, options);
		dfSprite.rebuildVertices(data, options);
		dfSprite.rebuildUV(data, options);
		dfSprite.rebuildColors(data, options);
		if (options.fillAmount < 1f)
		{
			dfSprite.doFill(data, options);
		}
	}

	// Token: 0x06000401 RID: 1025 RVA: 0x00014448 File Offset: 0x00012648
	private static void rebuildTriangles(dfRenderData renderData, dfSprite.RenderOptions options)
	{
		int baseIndex = options.baseIndex;
		dfList<int> triangles = renderData.Triangles;
		triangles.EnsureCapacity(triangles.Count + dfSprite.TRIANGLE_INDICES.Length);
		for (int i = 0; i < dfSprite.TRIANGLE_INDICES.Length; i++)
		{
			triangles.Add(baseIndex + dfSprite.TRIANGLE_INDICES[i]);
		}
	}

	// Token: 0x06000402 RID: 1026 RVA: 0x00014498 File Offset: 0x00012698
	private static void rebuildVertices(dfRenderData renderData, dfSprite.RenderOptions options)
	{
		dfList<Vector3> vertices = renderData.Vertices;
		int baseIndex = options.baseIndex;
		float x = 0f;
		float y = 0f;
		float x2 = Mathf.Ceil(options.size.x);
		float y2 = Mathf.Ceil(-options.size.y);
		vertices.Add(new Vector3(x, y, 0f) * options.pixelsToUnits);
		vertices.Add(new Vector3(x2, y, 0f) * options.pixelsToUnits);
		vertices.Add(new Vector3(x2, y2, 0f) * options.pixelsToUnits);
		vertices.Add(new Vector3(x, y2, 0f) * options.pixelsToUnits);
		Vector3 b = options.offset.RoundToInt() * options.pixelsToUnits;
		Vector3[] items = vertices.Items;
		for (int i = baseIndex; i < baseIndex + 4; i++)
		{
			items[i] = (items[i] + b).Quantize(options.pixelsToUnits);
		}
	}

	// Token: 0x06000403 RID: 1027 RVA: 0x000145AD File Offset: 0x000127AD
	private static void rebuildColors(dfRenderData renderData, dfSprite.RenderOptions options)
	{
		dfList<Color32> colors = renderData.Colors;
		colors.Add(options.color);
		colors.Add(options.color);
		colors.Add(options.color);
		colors.Add(options.color);
	}

	// Token: 0x06000404 RID: 1028 RVA: 0x000145E4 File Offset: 0x000127E4
	private static void rebuildUV(dfRenderData renderData, dfSprite.RenderOptions options)
	{
		Rect region = options.spriteInfo.region;
		dfList<Vector2> uv = renderData.UV;
		uv.Add(new Vector2(region.x, region.yMax));
		uv.Add(new Vector2(region.xMax, region.yMax));
		uv.Add(new Vector2(region.xMax, region.y));
		uv.Add(new Vector2(region.x, region.y));
		Vector2 value = Vector2.zero;
		if (options.flip.IsSet(dfSpriteFlip.FlipHorizontal))
		{
			value = uv[1];
			uv[1] = uv[0];
			uv[0] = value;
			value = uv[3];
			uv[3] = uv[2];
			uv[2] = value;
		}
		if (options.flip.IsSet(dfSpriteFlip.FlipVertical))
		{
			value = uv[0];
			uv[0] = uv[3];
			uv[3] = value;
			value = uv[1];
			uv[1] = uv[2];
			uv[2] = value;
		}
	}

	// Token: 0x06000405 RID: 1029 RVA: 0x00014704 File Offset: 0x00012904
	private static void doFill(dfRenderData renderData, dfSprite.RenderOptions options)
	{
		int baseIndex = options.baseIndex;
		dfList<Vector3> vertices = renderData.Vertices;
		dfList<Vector2> uv = renderData.UV;
		int index = baseIndex;
		int index2 = baseIndex + 1;
		int index3 = baseIndex + 3;
		int index4 = baseIndex + 2;
		if (options.invertFill)
		{
			if (options.fillDirection == dfFillDirection.Horizontal)
			{
				index = baseIndex + 1;
				index2 = baseIndex;
				index3 = baseIndex + 2;
				index4 = baseIndex + 3;
			}
			else
			{
				index = baseIndex + 3;
				index2 = baseIndex + 2;
				index3 = baseIndex;
				index4 = baseIndex + 1;
			}
		}
		if (options.fillDirection == dfFillDirection.Horizontal)
		{
			vertices[index2] = Vector3.Lerp(vertices[index2], vertices[index], 1f - options.fillAmount);
			vertices[index4] = Vector3.Lerp(vertices[index4], vertices[index3], 1f - options.fillAmount);
			uv[index2] = Vector2.Lerp(uv[index2], uv[index], 1f - options.fillAmount);
			uv[index4] = Vector2.Lerp(uv[index4], uv[index3], 1f - options.fillAmount);
			return;
		}
		vertices[index3] = Vector3.Lerp(vertices[index3], vertices[index], 1f - options.fillAmount);
		vertices[index4] = Vector3.Lerp(vertices[index4], vertices[index2], 1f - options.fillAmount);
		uv[index3] = Vector2.Lerp(uv[index3], uv[index], 1f - options.fillAmount);
		uv[index4] = Vector2.Lerp(uv[index4], uv[index2], 1f - options.fillAmount);
	}

	// Token: 0x06000406 RID: 1030 RVA: 0x000148BB File Offset: 0x00012ABB
	public override string ToString()
	{
		if (!string.IsNullOrEmpty(this.spriteName))
		{
			return string.Format("{0} ({1})", base.name, this.spriteName);
		}
		return base.ToString();
	}

	// Token: 0x0400015A RID: 346
	private static int[] TRIANGLE_INDICES = new int[]
	{
		0,
		1,
		3,
		3,
		1,
		2
	};

	// Token: 0x0400015C RID: 348
	[SerializeField]
	protected dfAtlas atlas;

	// Token: 0x0400015D RID: 349
	[SerializeField]
	protected string spriteName;

	// Token: 0x0400015E RID: 350
	[SerializeField]
	protected dfSpriteFlip flip;

	// Token: 0x0400015F RID: 351
	[SerializeField]
	protected dfFillDirection fillDirection;

	// Token: 0x04000160 RID: 352
	[SerializeField]
	protected float fillAmount = 1f;

	// Token: 0x04000161 RID: 353
	[SerializeField]
	protected bool invertFill;

	// Token: 0x0200035D RID: 861
	internal struct RenderOptions
	{
		// Token: 0x040015F9 RID: 5625
		public dfAtlas atlas;

		// Token: 0x040015FA RID: 5626
		public dfAtlas.ItemInfo spriteInfo;

		// Token: 0x040015FB RID: 5627
		public Color32 color;

		// Token: 0x040015FC RID: 5628
		public float pixelsToUnits;

		// Token: 0x040015FD RID: 5629
		public Vector2 size;

		// Token: 0x040015FE RID: 5630
		public dfSpriteFlip flip;

		// Token: 0x040015FF RID: 5631
		public bool invertFill;

		// Token: 0x04001600 RID: 5632
		public dfFillDirection fillDirection;

		// Token: 0x04001601 RID: 5633
		public float fillAmount;

		// Token: 0x04001602 RID: 5634
		public Vector3 offset;

		// Token: 0x04001603 RID: 5635
		public int baseIndex;
	}
}

using System;
using UnityEngine;

// Token: 0x02000013 RID: 19
[ExecuteInEditMode]
[AddComponentMenu("Daikon Forge/User Interface/Resize Handle")]
[Serializable]
public class dfResizeHandle : dfControl
{
	// Token: 0x170000B5 RID: 181
	// (get) Token: 0x06000314 RID: 788 RVA: 0x0000EC30 File Offset: 0x0000CE30
	// (set) Token: 0x06000315 RID: 789 RVA: 0x0000EC71 File Offset: 0x0000CE71
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

	// Token: 0x170000B6 RID: 182
	// (get) Token: 0x06000316 RID: 790 RVA: 0x0000EC8E File Offset: 0x0000CE8E
	// (set) Token: 0x06000317 RID: 791 RVA: 0x0000EC96 File Offset: 0x0000CE96
	public string BackgroundSprite
	{
		get
		{
			return this.backgroundSprite;
		}
		set
		{
			if (value != this.backgroundSprite)
			{
				this.backgroundSprite = value;
				this.Invalidate();
			}
		}
	}

	// Token: 0x170000B7 RID: 183
	// (get) Token: 0x06000318 RID: 792 RVA: 0x0000ECB3 File Offset: 0x0000CEB3
	// (set) Token: 0x06000319 RID: 793 RVA: 0x0000ECBB File Offset: 0x0000CEBB
	public dfResizeHandle.ResizeEdge Edges
	{
		get
		{
			return this.edges;
		}
		set
		{
			this.edges = value;
		}
	}

	// Token: 0x0600031A RID: 794 RVA: 0x0000ECC4 File Offset: 0x0000CEC4
	public override void Start()
	{
		base.Start();
		if (base.Size.magnitude <= 1E-45f)
		{
			base.Size = new Vector2(25f, 25f);
			if (base.Parent != null)
			{
				base.RelativePosition = base.Parent.Size - base.Size;
				base.Anchor = (dfAnchorStyle.Bottom | dfAnchorStyle.Right);
			}
		}
	}

	// Token: 0x0600031B RID: 795 RVA: 0x0000ED38 File Offset: 0x0000CF38
	protected override void OnRebuildRenderData()
	{
		if (this.Atlas == null || string.IsNullOrEmpty(this.backgroundSprite))
		{
			return;
		}
		dfAtlas.ItemInfo itemInfo = this.Atlas[this.backgroundSprite];
		if (itemInfo == null)
		{
			return;
		}
		this.renderData.Material = this.Atlas.Material;
		Color32 color = base.ApplyOpacity(base.IsEnabled ? this.color : this.disabledColor);
		dfSprite.RenderOptions options = new dfSprite.RenderOptions
		{
			atlas = this.atlas,
			color = color,
			fillAmount = 1f,
			flip = dfSpriteFlip.None,
			offset = this.pivot.TransformToUpperLeft(base.Size),
			pixelsToUnits = base.PixelsToUnits(),
			size = base.Size,
			spriteInfo = itemInfo
		};
		if (itemInfo.border.horizontal == 0 && itemInfo.border.vertical == 0)
		{
			dfSprite.renderSprite(this.renderData, options);
			return;
		}
		dfSlicedSprite.renderSprite(this.renderData, options);
	}

	// Token: 0x0600031C RID: 796 RVA: 0x0000EE50 File Offset: 0x0000D050
	protected internal override void OnMouseDown(dfMouseEventArgs args)
	{
		args.Use();
		Plane plane = new Plane(this.parent.transform.TransformDirection(Vector3.back), this.parent.transform.position);
		Ray ray = args.Ray;
		float d = 0f;
		plane.Raycast(args.Ray, out d);
		this.mouseAnchorPos = ray.origin + ray.direction * d;
		this.startSize = this.parent.Size;
		this.startPosition = this.parent.RelativePosition;
		this.minEdgePos = this.startPosition;
		this.maxEdgePos = this.startPosition + this.startSize;
		Vector2 vector = this.parent.CalculateMinimumSize();
		Vector2 vector2 = this.parent.MaximumSize;
		if (vector2.magnitude <= 1E-45f)
		{
			vector2 = Vector2.one * 2048f;
		}
		if ((this.Edges & dfResizeHandle.ResizeEdge.Left) == dfResizeHandle.ResizeEdge.Left)
		{
			this.minEdgePos.x = this.maxEdgePos.x - vector2.x;
			this.maxEdgePos.x = this.maxEdgePos.x - vector.x;
		}
		else if ((this.Edges & dfResizeHandle.ResizeEdge.Right) == dfResizeHandle.ResizeEdge.Right)
		{
			this.minEdgePos.x = this.startPosition.x + vector.x;
			this.maxEdgePos.x = this.startPosition.x + vector2.x;
		}
		if ((this.Edges & dfResizeHandle.ResizeEdge.Top) == dfResizeHandle.ResizeEdge.Top)
		{
			this.minEdgePos.y = this.maxEdgePos.y - vector2.y;
			this.maxEdgePos.y = this.maxEdgePos.y - vector.y;
		}
		else if ((this.Edges & dfResizeHandle.ResizeEdge.Bottom) == dfResizeHandle.ResizeEdge.Bottom)
		{
			this.minEdgePos.y = this.startPosition.y + vector.y;
			this.maxEdgePos.y = this.startPosition.y + vector2.y;
		}
		base.OnMouseDown(args);
	}

	// Token: 0x0600031D RID: 797 RVA: 0x0000F078 File Offset: 0x0000D278
	protected internal override void OnMouseMove(dfMouseEventArgs args)
	{
		if (!args.Buttons.IsSet(dfMouseButtons.Left) || this.Edges == dfResizeHandle.ResizeEdge.None)
		{
			return;
		}
		args.Use();
		Ray ray = args.Ray;
		float d = 0f;
		Vector3 inNormal = base.GetCamera().transform.TransformDirection(Vector3.back);
		Plane plane = new Plane(inNormal, this.mouseAnchorPos);
		plane.Raycast(ray, out d);
		float d2 = base.PixelsToUnits();
		Vector3 vector = (ray.origin + ray.direction * d - this.mouseAnchorPos) / d2;
		vector.y *= -1f;
		float num = this.startPosition.x;
		float num2 = this.startPosition.y;
		float num3 = num + this.startSize.x;
		float num4 = num2 + this.startSize.y;
		if ((this.Edges & dfResizeHandle.ResizeEdge.Left) == dfResizeHandle.ResizeEdge.Left)
		{
			num = Mathf.Min(this.maxEdgePos.x, Mathf.Max(this.minEdgePos.x, num + vector.x));
		}
		else if ((this.Edges & dfResizeHandle.ResizeEdge.Right) == dfResizeHandle.ResizeEdge.Right)
		{
			num3 = Mathf.Min(this.maxEdgePos.x, Mathf.Max(this.minEdgePos.x, num3 + vector.x));
		}
		if ((this.Edges & dfResizeHandle.ResizeEdge.Top) == dfResizeHandle.ResizeEdge.Top)
		{
			num2 = Mathf.Min(this.maxEdgePos.y, Mathf.Max(this.minEdgePos.y, num2 + vector.y));
		}
		else if ((this.Edges & dfResizeHandle.ResizeEdge.Bottom) == dfResizeHandle.ResizeEdge.Bottom)
		{
			num4 = Mathf.Min(this.maxEdgePos.y, Mathf.Max(this.minEdgePos.y, num4 + vector.y));
		}
		this.parent.Size = new Vector2(num3 - num, num4 - num2);
		this.parent.RelativePosition = new Vector3(num, num2, 0f);
		if (this.parent.GetManager().PixelPerfectMode)
		{
			this.parent.MakePixelPerfect();
		}
	}

	// Token: 0x0600031E RID: 798 RVA: 0x0000F28D File Offset: 0x0000D48D
	protected internal override void OnMouseUp(dfMouseEventArgs args)
	{
		base.Parent.MakePixelPerfect();
		args.Use();
		base.OnMouseUp(args);
	}

	// Token: 0x04000112 RID: 274
	[SerializeField]
	protected dfAtlas atlas;

	// Token: 0x04000113 RID: 275
	[SerializeField]
	protected string backgroundSprite = "";

	// Token: 0x04000114 RID: 276
	[SerializeField]
	protected dfResizeHandle.ResizeEdge edges = dfResizeHandle.ResizeEdge.Right | dfResizeHandle.ResizeEdge.Bottom;

	// Token: 0x04000115 RID: 277
	private Vector3 mouseAnchorPos;

	// Token: 0x04000116 RID: 278
	private Vector3 startPosition;

	// Token: 0x04000117 RID: 279
	private Vector2 startSize;

	// Token: 0x04000118 RID: 280
	private Vector2 minEdgePos;

	// Token: 0x04000119 RID: 281
	private Vector2 maxEdgePos;

	// Token: 0x02000359 RID: 857
	[Flags]
	public enum ResizeEdge
	{
		// Token: 0x040015ED RID: 5613
		None = 0,
		// Token: 0x040015EE RID: 5614
		Left = 1,
		// Token: 0x040015EF RID: 5615
		Right = 2,
		// Token: 0x040015F0 RID: 5616
		Top = 4,
		// Token: 0x040015F1 RID: 5617
		Bottom = 8
	}
}

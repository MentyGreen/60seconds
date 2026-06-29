using System;
using UnityEngine;

// Token: 0x0200000F RID: 15
[dfCategory("Basic Controls")]
[dfTooltip("Basic container control to facilitate user interface layout")]
[dfHelp("http://www.daikonforge.com/docs/df-gui/classdf_panel.html")]
[ExecuteInEditMode]
[AddComponentMenu("Daikon Forge/User Interface/Containers/Panel")]
[Serializable]
public class dfPanel : dfControl
{
	// Token: 0x170000A0 RID: 160
	// (get) Token: 0x060002C1 RID: 705 RVA: 0x0000D1DC File Offset: 0x0000B3DC
	// (set) Token: 0x060002C2 RID: 706 RVA: 0x0000D21D File Offset: 0x0000B41D
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

	// Token: 0x170000A1 RID: 161
	// (get) Token: 0x060002C3 RID: 707 RVA: 0x0000D23A File Offset: 0x0000B43A
	// (set) Token: 0x060002C4 RID: 708 RVA: 0x0000D242 File Offset: 0x0000B442
	public string BackgroundSprite
	{
		get
		{
			return this.backgroundSprite;
		}
		set
		{
			value = base.getLocalizedValue(value);
			if (value != this.backgroundSprite)
			{
				this.backgroundSprite = value;
				this.Invalidate();
			}
		}
	}

	// Token: 0x170000A2 RID: 162
	// (get) Token: 0x060002C5 RID: 709 RVA: 0x0000D268 File Offset: 0x0000B468
	// (set) Token: 0x060002C6 RID: 710 RVA: 0x0000D270 File Offset: 0x0000B470
	public Color32 BackgroundColor
	{
		get
		{
			return this.backgroundColor;
		}
		set
		{
			if (!object.Equals(value, this.backgroundColor))
			{
				this.backgroundColor = value;
				this.Invalidate();
			}
		}
	}

	// Token: 0x170000A3 RID: 163
	// (get) Token: 0x060002C7 RID: 711 RVA: 0x0000D297 File Offset: 0x0000B497
	// (set) Token: 0x060002C8 RID: 712 RVA: 0x0000D2B2 File Offset: 0x0000B4B2
	public RectOffset Padding
	{
		get
		{
			if (this.padding == null)
			{
				this.padding = new RectOffset();
			}
			return this.padding;
		}
		set
		{
			value = value.ConstrainPadding();
			if (!object.Equals(value, this.padding))
			{
				this.padding = value;
				this.Invalidate();
			}
		}
	}

	// Token: 0x060002C9 RID: 713 RVA: 0x0000D2D7 File Offset: 0x0000B4D7
	protected internal override void OnLocalize()
	{
		base.OnLocalize();
		this.BackgroundSprite = base.getLocalizedValue(this.backgroundSprite);
	}

	// Token: 0x060002CA RID: 714 RVA: 0x0000D2F1 File Offset: 0x0000B4F1
	protected internal override RectOffset GetClipPadding()
	{
		return this.padding ?? dfRectOffsetExtensions.Empty;
	}

	// Token: 0x060002CB RID: 715 RVA: 0x0000D304 File Offset: 0x0000B504
	protected internal override Plane[] GetClippingPlanes()
	{
		if (!base.ClipChildren)
		{
			return null;
		}
		Vector3[] corners = base.GetCorners();
		Vector3 vector = base.transform.TransformDirection(Vector3.right);
		Vector3 vector2 = base.transform.TransformDirection(Vector3.left);
		Vector3 vector3 = base.transform.TransformDirection(Vector3.up);
		Vector3 vector4 = base.transform.TransformDirection(Vector3.down);
		float d = base.PixelsToUnits();
		RectOffset rectOffset = this.Padding;
		corners[0] += vector * (float)rectOffset.left * d + vector4 * (float)rectOffset.top * d;
		corners[1] += vector2 * (float)rectOffset.right * d + vector4 * (float)rectOffset.top * d;
		corners[2] += vector * (float)rectOffset.left * d + vector3 * (float)rectOffset.bottom * d;
		return new Plane[]
		{
			new Plane(vector, corners[0]),
			new Plane(vector2, corners[1]),
			new Plane(vector3, corners[2]),
			new Plane(vector4, corners[0])
		};
	}

	// Token: 0x060002CC RID: 716 RVA: 0x0000D4A4 File Offset: 0x0000B6A4
	public override void OnEnable()
	{
		base.OnEnable();
		if (this.size == Vector2.zero)
		{
			base.SuspendLayout();
			Camera camera = base.GetCamera();
			base.Size = new Vector3((float)(camera.pixelWidth / 2), (float)(camera.pixelHeight / 2));
			base.ResumeLayout();
		}
	}

	// Token: 0x060002CD RID: 717 RVA: 0x0000D500 File Offset: 0x0000B700
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
		Color32 color = base.ApplyOpacity(this.BackgroundColor);
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

	// Token: 0x060002CE RID: 718 RVA: 0x0000D608 File Offset: 0x0000B808
	public void FitToContents()
	{
		if (this.controls.Count == 0)
		{
			return;
		}
		Vector2 vector = Vector2.zero;
		for (int i = 0; i < this.controls.Count; i++)
		{
			dfControl dfControl = this.controls[i];
			Vector2 rhs = dfControl.RelativePosition + dfControl.Size;
			vector = Vector2.Max(vector, rhs);
		}
		base.Size = vector + new Vector2((float)this.padding.right, (float)this.padding.bottom);
	}

	// Token: 0x060002CF RID: 719 RVA: 0x0000D694 File Offset: 0x0000B894
	public void CenterChildControls()
	{
		if (this.controls.Count == 0)
		{
			return;
		}
		Vector2 vector = Vector2.one * float.MaxValue;
		Vector2 vector2 = Vector2.one * float.MinValue;
		for (int i = 0; i < this.controls.Count; i++)
		{
			dfControl dfControl = this.controls[i];
			Vector2 vector3 = dfControl.RelativePosition;
			Vector2 rhs = vector3 + dfControl.Size;
			vector = Vector2.Min(vector, vector3);
			vector2 = Vector2.Max(vector2, rhs);
		}
		Vector2 b = vector2 - vector;
		Vector2 b2 = (base.Size - b) * 0.5f;
		for (int j = 0; j < this.controls.Count; j++)
		{
			dfControl dfControl2 = this.controls[j];
			dfControl2.RelativePosition = dfControl2.RelativePosition - vector + b2;
		}
	}

	// Token: 0x040000FA RID: 250
	[SerializeField]
	protected dfAtlas atlas;

	// Token: 0x040000FB RID: 251
	[SerializeField]
	protected string backgroundSprite;

	// Token: 0x040000FC RID: 252
	[SerializeField]
	protected Color32 backgroundColor = UnityEngine.Color.white;

	// Token: 0x040000FD RID: 253
	[SerializeField]
	protected RectOffset padding = new RectOffset();
}

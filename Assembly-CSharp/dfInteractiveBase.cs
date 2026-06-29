using System;
using UnityEngine;

// Token: 0x0200000C RID: 12
[ExecuteInEditMode]
[Serializable]
public class dfInteractiveBase : dfControl
{
	// Token: 0x1700006C RID: 108
	// (get) Token: 0x06000210 RID: 528 RVA: 0x0000A0B0 File Offset: 0x000082B0
	// (set) Token: 0x06000211 RID: 529 RVA: 0x0000A0F1 File Offset: 0x000082F1
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

	// Token: 0x1700006D RID: 109
	// (get) Token: 0x06000212 RID: 530 RVA: 0x0000A10E File Offset: 0x0000830E
	// (set) Token: 0x06000213 RID: 531 RVA: 0x0000A116 File Offset: 0x00008316
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
				this.setDefaultSize(value);
				this.Invalidate();
			}
		}
	}

	// Token: 0x1700006E RID: 110
	// (get) Token: 0x06000214 RID: 532 RVA: 0x0000A13A File Offset: 0x0000833A
	// (set) Token: 0x06000215 RID: 533 RVA: 0x0000A142 File Offset: 0x00008342
	public string DisabledSprite
	{
		get
		{
			return this.disabledSprite;
		}
		set
		{
			if (value != this.disabledSprite)
			{
				this.disabledSprite = value;
				this.Invalidate();
			}
		}
	}

	// Token: 0x1700006F RID: 111
	// (get) Token: 0x06000216 RID: 534 RVA: 0x0000A15F File Offset: 0x0000835F
	// (set) Token: 0x06000217 RID: 535 RVA: 0x0000A167 File Offset: 0x00008367
	public string FocusSprite
	{
		get
		{
			return this.focusSprite;
		}
		set
		{
			if (value != this.focusSprite)
			{
				this.focusSprite = value;
				this.Invalidate();
			}
		}
	}

	// Token: 0x17000070 RID: 112
	// (get) Token: 0x06000218 RID: 536 RVA: 0x0000A184 File Offset: 0x00008384
	// (set) Token: 0x06000219 RID: 537 RVA: 0x0000A18C File Offset: 0x0000838C
	public string HoverSprite
	{
		get
		{
			return this.hoverSprite;
		}
		set
		{
			if (value != this.hoverSprite)
			{
				this.hoverSprite = value;
				this.Invalidate();
			}
		}
	}

	// Token: 0x17000071 RID: 113
	// (get) Token: 0x0600021A RID: 538 RVA: 0x0000A1A9 File Offset: 0x000083A9
	public override bool CanFocus
	{
		get
		{
			return (base.IsEnabled && base.IsVisible) || base.CanFocus;
		}
	}

	// Token: 0x0600021B RID: 539 RVA: 0x0000A1C3 File Offset: 0x000083C3
	protected internal override void OnGotFocus(dfFocusEventArgs args)
	{
		base.OnGotFocus(args);
		this.Invalidate();
	}

	// Token: 0x0600021C RID: 540 RVA: 0x0000A1D2 File Offset: 0x000083D2
	protected internal override void OnLostFocus(dfFocusEventArgs args)
	{
		base.OnLostFocus(args);
		this.Invalidate();
	}

	// Token: 0x0600021D RID: 541 RVA: 0x0000A1E1 File Offset: 0x000083E1
	protected internal override void OnMouseEnter(dfMouseEventArgs args)
	{
		base.OnMouseEnter(args);
		this.Invalidate();
	}

	// Token: 0x0600021E RID: 542 RVA: 0x0000A1F0 File Offset: 0x000083F0
	protected internal override void OnMouseLeave(dfMouseEventArgs args)
	{
		base.OnMouseLeave(args);
		this.Invalidate();
	}

	// Token: 0x0600021F RID: 543 RVA: 0x0000A200 File Offset: 0x00008400
	public override Vector2 CalculateMinimumSize()
	{
		dfAtlas.ItemInfo itemInfo = this.getBackgroundSprite();
		if (itemInfo == null)
		{
			return base.CalculateMinimumSize();
		}
		RectOffset border = itemInfo.border;
		if (border.horizontal > 0 || border.vertical > 0)
		{
			return Vector2.Max(base.CalculateMinimumSize(), new Vector2((float)border.horizontal, (float)border.vertical));
		}
		return base.CalculateMinimumSize();
	}

	// Token: 0x06000220 RID: 544 RVA: 0x0000A264 File Offset: 0x00008464
	protected internal virtual void renderBackground()
	{
		if (this.Atlas == null)
		{
			return;
		}
		dfAtlas.ItemInfo itemInfo = this.getBackgroundSprite();
		if (itemInfo == null)
		{
			return;
		}
		Color32 color = base.ApplyOpacity(this.getActiveColor());
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

	// Token: 0x06000221 RID: 545 RVA: 0x0000A340 File Offset: 0x00008540
	protected virtual Color32 getActiveColor()
	{
		if (base.IsEnabled)
		{
			return this.color;
		}
		if (!string.IsNullOrEmpty(this.disabledSprite) && this.Atlas != null && this.Atlas[this.DisabledSprite] != null)
		{
			return this.color;
		}
		return this.disabledColor;
	}

	// Token: 0x06000222 RID: 546 RVA: 0x0000A3A0 File Offset: 0x000085A0
	protected internal virtual dfAtlas.ItemInfo getBackgroundSprite()
	{
		if (this.Atlas == null)
		{
			return null;
		}
		if (!base.IsEnabled)
		{
			dfAtlas.ItemInfo itemInfo = this.atlas[this.DisabledSprite];
			if (itemInfo != null)
			{
				return itemInfo;
			}
			return this.atlas[this.BackgroundSprite];
		}
		else
		{
			if (!this.HasFocus)
			{
				if (this.isMouseHovering)
				{
					dfAtlas.ItemInfo itemInfo2 = this.atlas[this.HoverSprite];
					if (itemInfo2 != null)
					{
						return itemInfo2;
					}
				}
				return this.Atlas[this.BackgroundSprite];
			}
			dfAtlas.ItemInfo itemInfo3 = this.atlas[this.FocusSprite];
			if (itemInfo3 != null)
			{
				return itemInfo3;
			}
			return this.atlas[this.BackgroundSprite];
		}
	}

	// Token: 0x06000223 RID: 547 RVA: 0x0000A464 File Offset: 0x00008664
	private void setDefaultSize(string spriteName)
	{
		if (this.Atlas == null)
		{
			return;
		}
		dfAtlas.ItemInfo itemInfo = this.Atlas[spriteName];
		if (this.size == Vector2.zero && itemInfo != null)
		{
			base.Size = itemInfo.sizeInPixels;
		}
	}

	// Token: 0x040000BA RID: 186
	[SerializeField]
	protected dfAtlas atlas;

	// Token: 0x040000BB RID: 187
	[SerializeField]
	protected string backgroundSprite;

	// Token: 0x040000BC RID: 188
	[SerializeField]
	protected string hoverSprite;

	// Token: 0x040000BD RID: 189
	[SerializeField]
	protected string disabledSprite;

	// Token: 0x040000BE RID: 190
	[SerializeField]
	protected string focusSprite;
}

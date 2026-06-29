using System;
using UnityEngine;

// Token: 0x0200000E RID: 14
[dfCategory("Basic Controls")]
[dfTooltip("Allows the user to select from a list of options")]
[dfHelp("http://www.daikonforge.com/docs/df-gui/classdf_listbox.html")]
[ExecuteInEditMode]
[AddComponentMenu("Daikon Forge/User Interface/Listbox")]
[Serializable]
public class dfListbox : dfInteractiveBase, IDFMultiRender, IRendersText
{
	// Token: 0x1400002E RID: 46
	// (add) Token: 0x06000270 RID: 624 RVA: 0x0000B724 File Offset: 0x00009924
	// (remove) Token: 0x06000271 RID: 625 RVA: 0x0000B75C File Offset: 0x0000995C
	public event PropertyChangedEventHandler<int> SelectedIndexChanged;

	// Token: 0x1400002F RID: 47
	// (add) Token: 0x06000272 RID: 626 RVA: 0x0000B794 File Offset: 0x00009994
	// (remove) Token: 0x06000273 RID: 627 RVA: 0x0000B7CC File Offset: 0x000099CC
	public event PropertyChangedEventHandler<int> ItemClicked;

	// Token: 0x1700008C RID: 140
	// (get) Token: 0x06000274 RID: 628 RVA: 0x0000B804 File Offset: 0x00009A04
	// (set) Token: 0x06000275 RID: 629 RVA: 0x0000B841 File Offset: 0x00009A41
	public dfFontBase Font
	{
		get
		{
			if (this.font == null)
			{
				dfGUIManager manager = base.GetManager();
				if (manager != null)
				{
					this.font = manager.DefaultFont;
				}
			}
			return this.font;
		}
		set
		{
			if (value != this.font)
			{
				this.unbindTextureRebuildCallback();
				this.font = value;
				this.bindTextureRebuildCallback();
				this.Invalidate();
			}
		}
	}

	// Token: 0x1700008D RID: 141
	// (get) Token: 0x06000276 RID: 630 RVA: 0x0000B86A File Offset: 0x00009A6A
	// (set) Token: 0x06000277 RID: 631 RVA: 0x0000B872 File Offset: 0x00009A72
	public float ScrollPosition
	{
		get
		{
			return this.scrollPosition;
		}
		set
		{
			if (!Mathf.Approximately(value, this.scrollPosition))
			{
				this.scrollPosition = this.constrainScrollPosition(value);
				this.Invalidate();
			}
		}
	}

	// Token: 0x1700008E RID: 142
	// (get) Token: 0x06000278 RID: 632 RVA: 0x0000B895 File Offset: 0x00009A95
	// (set) Token: 0x06000279 RID: 633 RVA: 0x0000B89D File Offset: 0x00009A9D
	public TextAlignment ItemAlignment
	{
		get
		{
			return this.itemAlignment;
		}
		set
		{
			if (value != this.itemAlignment)
			{
				this.itemAlignment = value;
				this.Invalidate();
			}
		}
	}

	// Token: 0x1700008F RID: 143
	// (get) Token: 0x0600027A RID: 634 RVA: 0x0000B8B5 File Offset: 0x00009AB5
	// (set) Token: 0x0600027B RID: 635 RVA: 0x0000B8BD File Offset: 0x00009ABD
	public string ItemHighlight
	{
		get
		{
			return this.itemHighlight;
		}
		set
		{
			if (value != this.itemHighlight)
			{
				this.itemHighlight = value;
				this.Invalidate();
			}
		}
	}

	// Token: 0x17000090 RID: 144
	// (get) Token: 0x0600027C RID: 636 RVA: 0x0000B8DA File Offset: 0x00009ADA
	// (set) Token: 0x0600027D RID: 637 RVA: 0x0000B8E2 File Offset: 0x00009AE2
	public string ItemHover
	{
		get
		{
			return this.itemHover;
		}
		set
		{
			if (value != this.itemHover)
			{
				this.itemHover = value;
				this.Invalidate();
			}
		}
	}

	// Token: 0x17000091 RID: 145
	// (get) Token: 0x0600027E RID: 638 RVA: 0x0000B8FF File Offset: 0x00009AFF
	public string SelectedItem
	{
		get
		{
			if (this.selectedIndex == -1)
			{
				return null;
			}
			return this.items[this.selectedIndex];
		}
	}

	// Token: 0x17000092 RID: 146
	// (get) Token: 0x0600027F RID: 639 RVA: 0x0000B919 File Offset: 0x00009B19
	// (set) Token: 0x06000280 RID: 640 RVA: 0x0000B928 File Offset: 0x00009B28
	public string SelectedValue
	{
		get
		{
			return this.items[this.selectedIndex];
		}
		set
		{
			this.selectedIndex = -1;
			for (int i = 0; i < this.items.Length; i++)
			{
				if (this.items[i] == value)
				{
					this.selectedIndex = i;
					return;
				}
			}
		}
	}

	// Token: 0x17000093 RID: 147
	// (get) Token: 0x06000281 RID: 641 RVA: 0x0000B967 File Offset: 0x00009B67
	// (set) Token: 0x06000282 RID: 642 RVA: 0x0000B96F File Offset: 0x00009B6F
	public int SelectedIndex
	{
		get
		{
			return this.selectedIndex;
		}
		set
		{
			value = Mathf.Max(-1, value);
			value = Mathf.Min(this.items.Length - 1, value);
			if (value != this.selectedIndex)
			{
				this.selectedIndex = value;
				this.EnsureVisible(value);
				this.OnSelectedIndexChanged();
				this.Invalidate();
			}
		}
	}

	// Token: 0x17000094 RID: 148
	// (get) Token: 0x06000283 RID: 643 RVA: 0x0000B9AF File Offset: 0x00009BAF
	// (set) Token: 0x06000284 RID: 644 RVA: 0x0000B9CA File Offset: 0x00009BCA
	public RectOffset ItemPadding
	{
		get
		{
			if (this.itemPadding == null)
			{
				this.itemPadding = new RectOffset();
			}
			return this.itemPadding;
		}
		set
		{
			value = value.ConstrainPadding();
			if (!value.Equals(this.itemPadding))
			{
				this.itemPadding = value;
				this.Invalidate();
			}
		}
	}

	// Token: 0x17000095 RID: 149
	// (get) Token: 0x06000285 RID: 645 RVA: 0x0000B9EF File Offset: 0x00009BEF
	// (set) Token: 0x06000286 RID: 646 RVA: 0x0000B9F7 File Offset: 0x00009BF7
	public Color32 ItemTextColor
	{
		get
		{
			return this.itemTextColor;
		}
		set
		{
			if (!value.Equals(this.itemTextColor))
			{
				this.itemTextColor = value;
				this.Invalidate();
			}
		}
	}

	// Token: 0x17000096 RID: 150
	// (get) Token: 0x06000287 RID: 647 RVA: 0x0000BA20 File Offset: 0x00009C20
	// (set) Token: 0x06000288 RID: 648 RVA: 0x0000BA28 File Offset: 0x00009C28
	public float ItemTextScale
	{
		get
		{
			return this.itemTextScale;
		}
		set
		{
			value = Mathf.Max(0.1f, value);
			if (!Mathf.Approximately(this.itemTextScale, value))
			{
				this.itemTextScale = value;
				this.Invalidate();
			}
		}
	}

	// Token: 0x17000097 RID: 151
	// (get) Token: 0x06000289 RID: 649 RVA: 0x0000BA52 File Offset: 0x00009C52
	// (set) Token: 0x0600028A RID: 650 RVA: 0x0000BA5A File Offset: 0x00009C5A
	public int ItemHeight
	{
		get
		{
			return this.itemHeight;
		}
		set
		{
			this.scrollPosition = 0f;
			value = Mathf.Max(1, value);
			if (value != this.itemHeight)
			{
				this.itemHeight = value;
				this.Invalidate();
			}
		}
	}

	// Token: 0x17000098 RID: 152
	// (get) Token: 0x0600028B RID: 651 RVA: 0x0000BA86 File Offset: 0x00009C86
	// (set) Token: 0x0600028C RID: 652 RVA: 0x0000BAA2 File Offset: 0x00009CA2
	public string[] Items
	{
		get
		{
			if (this.items == null)
			{
				this.items = new string[0];
			}
			return this.items;
		}
		set
		{
			if (value != this.items)
			{
				this.scrollPosition = 0f;
				if (value == null)
				{
					value = new string[0];
				}
				this.items = value;
				this.Invalidate();
			}
		}
	}

	// Token: 0x17000099 RID: 153
	// (get) Token: 0x0600028D RID: 653 RVA: 0x0000BAD0 File Offset: 0x00009CD0
	// (set) Token: 0x0600028E RID: 654 RVA: 0x0000BAD8 File Offset: 0x00009CD8
	public dfScrollbar Scrollbar
	{
		get
		{
			return this.scrollbar;
		}
		set
		{
			this.scrollPosition = 0f;
			if (value != this.scrollbar)
			{
				this.detachScrollbarEvents();
				this.scrollbar = value;
				this.attachScrollbarEvents();
				this.Invalidate();
			}
		}
	}

	// Token: 0x1700009A RID: 154
	// (get) Token: 0x0600028F RID: 655 RVA: 0x0000BB0C File Offset: 0x00009D0C
	// (set) Token: 0x06000290 RID: 656 RVA: 0x0000BB27 File Offset: 0x00009D27
	public RectOffset ListPadding
	{
		get
		{
			if (this.listPadding == null)
			{
				this.listPadding = new RectOffset();
			}
			return this.listPadding;
		}
		set
		{
			value = value.ConstrainPadding();
			if (!object.Equals(value, this.listPadding))
			{
				this.listPadding = value;
				this.Invalidate();
			}
		}
	}

	// Token: 0x1700009B RID: 155
	// (get) Token: 0x06000291 RID: 657 RVA: 0x0000BB4C File Offset: 0x00009D4C
	// (set) Token: 0x06000292 RID: 658 RVA: 0x0000BB54 File Offset: 0x00009D54
	public bool Shadow
	{
		get
		{
			return this.shadow;
		}
		set
		{
			if (value != this.shadow)
			{
				this.shadow = value;
				this.Invalidate();
			}
		}
	}

	// Token: 0x1700009C RID: 156
	// (get) Token: 0x06000293 RID: 659 RVA: 0x0000BB6C File Offset: 0x00009D6C
	// (set) Token: 0x06000294 RID: 660 RVA: 0x0000BB74 File Offset: 0x00009D74
	public Color32 ShadowColor
	{
		get
		{
			return this.shadowColor;
		}
		set
		{
			if (!value.Equals(this.shadowColor))
			{
				this.shadowColor = value;
				this.Invalidate();
			}
		}
	}

	// Token: 0x1700009D RID: 157
	// (get) Token: 0x06000295 RID: 661 RVA: 0x0000BB9D File Offset: 0x00009D9D
	// (set) Token: 0x06000296 RID: 662 RVA: 0x0000BBA5 File Offset: 0x00009DA5
	public Vector2 ShadowOffset
	{
		get
		{
			return this.shadowOffset;
		}
		set
		{
			if (value != this.shadowOffset)
			{
				this.shadowOffset = value;
				this.Invalidate();
			}
		}
	}

	// Token: 0x1700009E RID: 158
	// (get) Token: 0x06000297 RID: 663 RVA: 0x0000BBC2 File Offset: 0x00009DC2
	// (set) Token: 0x06000298 RID: 664 RVA: 0x0000BBCA File Offset: 0x00009DCA
	public bool AnimateHover
	{
		get
		{
			return this.animateHover;
		}
		set
		{
			this.animateHover = value;
		}
	}

	// Token: 0x1700009F RID: 159
	// (get) Token: 0x06000299 RID: 665 RVA: 0x0000BBD3 File Offset: 0x00009DD3
	// (set) Token: 0x0600029A RID: 666 RVA: 0x0000BBDB File Offset: 0x00009DDB
	public dfTextScaleMode TextScaleMode
	{
		get
		{
			return this.textScaleMode;
		}
		set
		{
			this.textScaleMode = value;
			this.Invalidate();
		}
	}

	// Token: 0x0600029B RID: 667 RVA: 0x0000BBEA File Offset: 0x00009DEA
	public override void Awake()
	{
		base.Awake();
		this.startSize = base.Size;
	}

	// Token: 0x0600029C RID: 668 RVA: 0x0000BC00 File Offset: 0x00009E00
	public override void Update()
	{
		base.Update();
		if (this.size.magnitude == 0f)
		{
			this.size = new Vector2(200f, 150f);
		}
		if (this.animateHover && this.hoverIndex != -1)
		{
			float num = (float)(this.hoverIndex * this.itemHeight) * base.PixelsToUnits();
			if (Mathf.Abs(this.hoverTweenLocation - num) < 1f)
			{
				this.Invalidate();
			}
		}
		if (this.isControlInvalidated)
		{
			this.synchronizeScrollbar();
		}
	}

	// Token: 0x0600029D RID: 669 RVA: 0x0000BC89 File Offset: 0x00009E89
	public override void LateUpdate()
	{
		base.LateUpdate();
		if (!Application.isPlaying)
		{
			return;
		}
		this.attachScrollbarEvents();
	}

	// Token: 0x0600029E RID: 670 RVA: 0x0000BC9F File Offset: 0x00009E9F
	public override void OnEnable()
	{
		base.OnEnable();
		this.bindTextureRebuildCallback();
	}

	// Token: 0x0600029F RID: 671 RVA: 0x0000BCAD File Offset: 0x00009EAD
	public override void OnDestroy()
	{
		base.OnDestroy();
		this.detachScrollbarEvents();
	}

	// Token: 0x060002A0 RID: 672 RVA: 0x0000BCBB File Offset: 0x00009EBB
	public override void OnDisable()
	{
		base.OnDisable();
		this.unbindTextureRebuildCallback();
		this.detachScrollbarEvents();
	}

	// Token: 0x060002A1 RID: 673 RVA: 0x0000BCD0 File Offset: 0x00009ED0
	protected internal override void OnLocalize()
	{
		base.OnLocalize();
		bool flag = false;
		for (int i = 0; i < this.items.Length; i++)
		{
			string localizedValue = base.getLocalizedValue(this.items[i]);
			if (localizedValue != this.items[i])
			{
				flag = true;
				this.items[i] = localizedValue;
			}
		}
		if (flag)
		{
			this.Invalidate();
		}
	}

	// Token: 0x060002A2 RID: 674 RVA: 0x0000BD2B File Offset: 0x00009F2B
	protected internal virtual void OnSelectedIndexChanged()
	{
		base.SignalHierarchy("OnSelectedIndexChanged", new object[]
		{
			this,
			this.selectedIndex
		});
		if (this.SelectedIndexChanged != null)
		{
			this.SelectedIndexChanged(this, this.selectedIndex);
		}
	}

	// Token: 0x060002A3 RID: 675 RVA: 0x0000BD6B File Offset: 0x00009F6B
	protected internal virtual void OnItemClicked()
	{
		base.Signal("OnItemClicked", this, this.selectedIndex);
		if (this.ItemClicked != null)
		{
			this.ItemClicked(this, this.selectedIndex);
		}
	}

	// Token: 0x060002A4 RID: 676 RVA: 0x0000BDA0 File Offset: 0x00009FA0
	protected internal override void OnMouseMove(dfMouseEventArgs args)
	{
		base.OnMouseMove(args);
		if (!(args is dfTouchEventArgs))
		{
			this.updateItemHover(args);
			return;
		}
		if (Mathf.Abs(args.Position.y - this.touchStartPosition.y) < (float)(this.itemHeight / 2))
		{
			return;
		}
		this.ScrollPosition = Mathf.Max(0f, this.ScrollPosition + args.MoveDelta.y);
		this.synchronizeScrollbar();
		this.hoverIndex = -1;
	}

	// Token: 0x060002A5 RID: 677 RVA: 0x0000BE1B File Offset: 0x0000A01B
	protected internal override void OnMouseEnter(dfMouseEventArgs args)
	{
		base.OnMouseEnter(args);
		this.touchStartPosition = args.Position;
	}

	// Token: 0x060002A6 RID: 678 RVA: 0x0000BE30 File Offset: 0x0000A030
	protected internal override void OnMouseLeave(dfMouseEventArgs args)
	{
		base.OnMouseLeave(args);
		this.hoverIndex = -1;
	}

	// Token: 0x060002A7 RID: 679 RVA: 0x0000BE40 File Offset: 0x0000A040
	protected internal override void OnMouseWheel(dfMouseEventArgs args)
	{
		base.OnMouseWheel(args);
		this.ScrollPosition = Mathf.Max(0f, this.ScrollPosition - (float)((int)args.WheelDelta * this.ItemHeight));
		this.synchronizeScrollbar();
		this.updateItemHover(args);
	}

	// Token: 0x060002A8 RID: 680 RVA: 0x0000BE7C File Offset: 0x0000A07C
	protected internal override void OnMouseUp(dfMouseEventArgs args)
	{
		this.hoverIndex = -1;
		base.OnMouseUp(args);
		if (args is dfTouchEventArgs && Mathf.Abs(args.Position.y - this.touchStartPosition.y) < (float)this.itemHeight)
		{
			this.selectItemUnderMouse(args);
		}
	}

	// Token: 0x060002A9 RID: 681 RVA: 0x0000BECB File Offset: 0x0000A0CB
	protected internal override void OnMouseDown(dfMouseEventArgs args)
	{
		base.OnMouseDown(args);
		if (args is dfTouchEventArgs)
		{
			this.touchStartPosition = args.Position;
			return;
		}
		this.selectItemUnderMouse(args);
	}

	// Token: 0x060002AA RID: 682 RVA: 0x0000BEF0 File Offset: 0x0000A0F0
	protected internal override void OnKeyDown(dfKeyEventArgs args)
	{
		switch (args.KeyCode)
		{
		case KeyCode.UpArrow:
			this.SelectedIndex = Mathf.Max(0, this.selectedIndex - 1);
			break;
		case KeyCode.DownArrow:
			this.SelectedIndex++;
			break;
		case KeyCode.Home:
			this.SelectedIndex = 0;
			break;
		case KeyCode.End:
			this.SelectedIndex = this.items.Length;
			break;
		case KeyCode.PageUp:
		{
			int b = this.SelectedIndex - Mathf.FloorToInt((this.size.y - (float)this.listPadding.vertical) / (float)this.itemHeight);
			this.SelectedIndex = Mathf.Max(0, b);
			break;
		}
		case KeyCode.PageDown:
			this.SelectedIndex += Mathf.FloorToInt((this.size.y - (float)this.listPadding.vertical) / (float)this.itemHeight);
			break;
		}
		base.OnKeyDown(args);
	}

	// Token: 0x060002AB RID: 683 RVA: 0x0000BFF0 File Offset: 0x0000A1F0
	public void AddItem(string item)
	{
		string[] array = new string[this.items.Length + 1];
		Array.Copy(this.items, array, this.items.Length);
		array[this.items.Length] = item;
		this.items = array;
		this.Invalidate();
	}

	// Token: 0x060002AC RID: 684 RVA: 0x0000C03C File Offset: 0x0000A23C
	public void EnsureVisible(int index)
	{
		int num = index * this.ItemHeight;
		if (this.scrollPosition > (float)num)
		{
			this.ScrollPosition = (float)num;
		}
		float num2 = this.size.y - (float)this.listPadding.vertical;
		if (this.scrollPosition + num2 < (float)(num + this.itemHeight))
		{
			this.ScrollPosition = (float)num - num2 + (float)this.itemHeight;
		}
	}

	// Token: 0x060002AD RID: 685 RVA: 0x0000C0A4 File Offset: 0x0000A2A4
	private void selectItemUnderMouse(dfMouseEventArgs args)
	{
		float num = this.pivot.TransformToUpperLeft(base.Size).y + ((float)(-(float)this.itemHeight) * ((float)this.selectedIndex - this.scrollPosition) - (float)this.listPadding.top);
		float num2 = ((float)this.selectedIndex - this.scrollPosition + 1f) * (float)this.itemHeight + (float)this.listPadding.vertical - this.size.y;
		if (num2 > 0f)
		{
			num += num2;
		}
		float num3 = base.GetHitPosition(args).y - (float)this.listPadding.top;
		if (num3 < 0f || num3 > this.size.y - (float)this.listPadding.bottom)
		{
			return;
		}
		this.SelectedIndex = (int)((this.scrollPosition + num3) / (float)this.itemHeight);
		this.OnItemClicked();
	}

	// Token: 0x060002AE RID: 686 RVA: 0x0000C18C File Offset: 0x0000A38C
	private void renderHover()
	{
		if (!Application.isPlaying)
		{
			return;
		}
		if (base.Atlas == null || !base.IsEnabled || this.hoverIndex < 0 || this.hoverIndex > this.items.Length - 1 || string.IsNullOrEmpty(this.ItemHover))
		{
			return;
		}
		dfAtlas.ItemInfo itemInfo = base.Atlas[this.ItemHover];
		if (itemInfo == null)
		{
			return;
		}
		Vector3 vector = this.pivot.TransformToUpperLeft(base.Size);
		Vector3 offset = new Vector3(vector.x + (float)this.listPadding.left, vector.y - (float)this.listPadding.top + this.scrollPosition, 0f);
		float num = base.PixelsToUnits();
		int num2 = this.hoverIndex * this.itemHeight;
		if (this.animateHover)
		{
			float num3 = Mathf.Abs(this.hoverTweenLocation - (float)num2);
			float num4 = (this.size.y - (float)this.listPadding.vertical) * 0.5f;
			if (num3 > num4)
			{
				this.hoverTweenLocation = (float)num2 + Mathf.Sign(this.hoverTweenLocation - (float)num2) * num4;
			}
			float maxDelta = Time.deltaTime / num * 2f;
			this.hoverTweenLocation = Mathf.MoveTowards(this.hoverTweenLocation, (float)num2, maxDelta);
		}
		else
		{
			this.hoverTweenLocation = (float)num2;
		}
		offset.y -= this.hoverTweenLocation.Quantize(num);
		Color32 color = base.ApplyOpacity(this.color);
		dfSprite.RenderOptions options = new dfSprite.RenderOptions
		{
			atlas = this.atlas,
			color = color,
			fillAmount = 1f,
			flip = dfSpriteFlip.None,
			pixelsToUnits = base.PixelsToUnits(),
			size = new Vector3(this.size.x - (float)this.listPadding.horizontal, (float)this.itemHeight),
			spriteInfo = itemInfo,
			offset = offset
		};
		if (itemInfo.border.horizontal > 0 || itemInfo.border.vertical > 0)
		{
			dfSlicedSprite.renderSprite(this.renderData, options);
		}
		else
		{
			dfSprite.renderSprite(this.renderData, options);
		}
		if ((float)num2 != this.hoverTweenLocation)
		{
			this.Invalidate();
		}
	}

	// Token: 0x060002AF RID: 687 RVA: 0x0000C3D8 File Offset: 0x0000A5D8
	private void renderSelection()
	{
		if (base.Atlas == null || this.selectedIndex < 0)
		{
			return;
		}
		dfAtlas.ItemInfo itemInfo = base.Atlas[this.ItemHighlight];
		if (itemInfo == null)
		{
			return;
		}
		float pixelsToUnits = base.PixelsToUnits();
		Vector3 vector = this.pivot.TransformToUpperLeft(base.Size);
		Vector3 offset = new Vector3(vector.x + (float)this.listPadding.left, vector.y - (float)this.listPadding.top + this.scrollPosition, 0f);
		offset.y -= (float)(this.selectedIndex * this.itemHeight);
		Color32 color = base.ApplyOpacity(this.color);
		dfSprite.RenderOptions options = new dfSprite.RenderOptions
		{
			atlas = this.atlas,
			color = color,
			fillAmount = 1f,
			flip = dfSpriteFlip.None,
			pixelsToUnits = pixelsToUnits,
			size = new Vector3(this.size.x - (float)this.listPadding.horizontal, (float)this.itemHeight),
			spriteInfo = itemInfo,
			offset = offset
		};
		if (itemInfo.border.horizontal > 0 || itemInfo.border.vertical > 0)
		{
			dfSlicedSprite.renderSprite(this.renderData, options);
			return;
		}
		dfSprite.renderSprite(this.renderData, options);
	}

	// Token: 0x060002B0 RID: 688 RVA: 0x0000C548 File Offset: 0x0000A748
	private float getTextScaleMultiplier()
	{
		if (this.textScaleMode == dfTextScaleMode.None || !Application.isPlaying)
		{
			return 1f;
		}
		if (this.textScaleMode == dfTextScaleMode.ScreenResolution)
		{
			return (float)Screen.height / (float)this.cachedManager.FixedHeight;
		}
		return base.Size.y / this.startSize.y;
	}

	// Token: 0x060002B1 RID: 689 RVA: 0x0000C5A0 File Offset: 0x0000A7A0
	private void renderItems(dfRenderData buffer)
	{
		if (this.font == null || this.items == null || this.items.Length == 0)
		{
			return;
		}
		float num = base.PixelsToUnits();
		Vector2 maxSize = new Vector2(this.size.x - (float)this.itemPadding.horizontal - (float)this.listPadding.horizontal, (float)(this.itemHeight - this.itemPadding.vertical));
		Vector3 vector = this.pivot.TransformToUpperLeft(base.Size);
		Vector3 vector2 = new Vector3(vector.x + (float)this.itemPadding.left + (float)this.listPadding.left, vector.y - (float)this.itemPadding.top - (float)this.listPadding.top, 0f) * num;
		vector2.y += this.scrollPosition * num;
		Color32 defaultColor = base.IsEnabled ? this.ItemTextColor : base.DisabledColor;
		float num2 = vector.y * num;
		float num3 = num2 - this.size.y * num;
		for (int i = 0; i < this.items.Length; i++)
		{
			using (dfFontRendererBase dfFontRendererBase = this.font.ObtainRenderer())
			{
				dfFontRendererBase.WordWrap = false;
				dfFontRendererBase.MaxSize = maxSize;
				dfFontRendererBase.PixelRatio = num;
				dfFontRendererBase.TextScale = this.ItemTextScale * this.getTextScaleMultiplier();
				dfFontRendererBase.VectorOffset = vector2;
				dfFontRendererBase.MultiLine = false;
				dfFontRendererBase.TextAlign = this.ItemAlignment;
				dfFontRendererBase.ProcessMarkup = true;
				dfFontRendererBase.DefaultColor = defaultColor;
				dfFontRendererBase.OverrideMarkupColors = false;
				dfFontRendererBase.Opacity = base.CalculateOpacity();
				dfFontRendererBase.Shadow = this.Shadow;
				dfFontRendererBase.ShadowColor = this.ShadowColor;
				dfFontRendererBase.ShadowOffset = this.ShadowOffset;
				dfDynamicFont.DynamicFontRenderer dynamicFontRenderer = dfFontRendererBase as dfDynamicFont.DynamicFontRenderer;
				if (dynamicFontRenderer != null)
				{
					dynamicFontRenderer.SpriteAtlas = base.Atlas;
					dynamicFontRenderer.SpriteBuffer = this.renderData;
				}
				if (vector2.y - (float)this.itemHeight * num <= num2)
				{
					dfFontRendererBase.Render(this.items[i], buffer);
				}
				vector2.y -= (float)this.itemHeight * num;
				dfFontRendererBase.VectorOffset = vector2;
				if (vector2.y < num3)
				{
					break;
				}
			}
		}
	}

	// Token: 0x060002B2 RID: 690 RVA: 0x0000C81C File Offset: 0x0000AA1C
	private void clipQuads(dfRenderData buffer, int startIndex)
	{
		dfList<Vector3> vertices = buffer.Vertices;
		dfList<Vector2> uv = buffer.UV;
		float num = base.PixelsToUnits();
		float num2 = (base.Pivot.TransformToUpperLeft(base.Size).y - (float)this.listPadding.top) * num;
		float num3 = num2 - (this.size.y - (float)this.listPadding.vertical) * num;
		for (int i = startIndex; i < vertices.Count; i += 4)
		{
			Vector3 vector = vertices[i];
			Vector3 vector2 = vertices[i + 1];
			Vector3 vector3 = vertices[i + 2];
			Vector3 vector4 = vertices[i + 3];
			float num4 = vector.y - vector4.y;
			if (vector4.y < num3)
			{
				float t = 1f - Mathf.Abs(-num3 + vector.y) / num4;
				dfList<Vector3> dfList = vertices;
				int index = i;
				vector = new Vector3(vector.x, Mathf.Max(vector.y, num3), vector2.z);
				dfList[index] = vector;
				dfList<Vector3> dfList2 = vertices;
				int index2 = i + 1;
				vector2 = new Vector3(vector2.x, Mathf.Max(vector2.y, num3), vector2.z);
				dfList2[index2] = vector2;
				dfList<Vector3> dfList3 = vertices;
				int index3 = i + 2;
				vector3 = new Vector3(vector3.x, Mathf.Max(vector3.y, num3), vector3.z);
				dfList3[index3] = vector3;
				dfList<Vector3> dfList4 = vertices;
				int index4 = i + 3;
				vector4 = new Vector3(vector4.x, Mathf.Max(vector4.y, num3), vector4.z);
				dfList4[index4] = vector4;
				float y = Mathf.Lerp(uv[i + 3].y, uv[i].y, t);
				uv[i + 3] = new Vector2(uv[i + 3].x, y);
				uv[i + 2] = new Vector2(uv[i + 2].x, y);
				num4 = Mathf.Abs(vector4.y - vector.y);
			}
			if (vector.y > num2)
			{
				float t2 = Mathf.Abs(num2 - vector.y) / num4;
				vertices[i] = new Vector3(vector.x, Mathf.Min(num2, vector.y), vector.z);
				vertices[i + 1] = new Vector3(vector2.x, Mathf.Min(num2, vector2.y), vector2.z);
				vertices[i + 2] = new Vector3(vector3.x, Mathf.Min(num2, vector3.y), vector3.z);
				vertices[i + 3] = new Vector3(vector4.x, Mathf.Min(num2, vector4.y), vector4.z);
				float y2 = Mathf.Lerp(uv[i].y, uv[i + 3].y, t2);
				uv[i] = new Vector2(uv[i].x, y2);
				uv[i + 1] = new Vector2(uv[i + 1].x, y2);
			}
		}
	}

	// Token: 0x060002B3 RID: 691 RVA: 0x0000CB64 File Offset: 0x0000AD64
	private void updateItemHover(dfMouseEventArgs args)
	{
		if (!Application.isPlaying)
		{
			return;
		}
		Ray ray = args.Ray;
		RaycastHit raycastHit;
		if (!base.GetComponent<Collider>().Raycast(ray, out raycastHit, 1000f))
		{
			this.hoverIndex = -1;
			this.hoverTweenLocation = 0f;
			return;
		}
		Vector2 vector;
		base.GetHitPosition(ray, out vector);
		float num = base.Pivot.TransformToUpperLeft(base.Size).y + ((float)(-(float)this.itemHeight) * ((float)this.selectedIndex - this.scrollPosition) - (float)this.listPadding.top);
		float num2 = ((float)this.selectedIndex - this.scrollPosition + 1f) * (float)this.itemHeight + (float)this.listPadding.vertical - this.size.y;
		if (num2 > 0f)
		{
			num += num2;
		}
		float num3 = vector.y - (float)this.listPadding.top;
		int num4 = (int)(this.scrollPosition + num3) / this.itemHeight;
		if (num4 != this.hoverIndex)
		{
			this.hoverIndex = num4;
			this.Invalidate();
		}
	}

	// Token: 0x060002B4 RID: 692 RVA: 0x0000CC74 File Offset: 0x0000AE74
	private float constrainScrollPosition(float value)
	{
		value = Mathf.Max(0f, value);
		int num = this.items.Length * this.itemHeight;
		float num2 = this.size.y - (float)this.listPadding.vertical;
		if ((float)num < num2)
		{
			return 0f;
		}
		return Mathf.Min(value, (float)num - num2);
	}

	// Token: 0x060002B5 RID: 693 RVA: 0x0000CCCC File Offset: 0x0000AECC
	private void attachScrollbarEvents()
	{
		if (this.scrollbar == null || this.eventsAttached)
		{
			return;
		}
		this.eventsAttached = true;
		this.scrollbar.ValueChanged += this.scrollbar_ValueChanged;
		this.scrollbar.GotFocus += this.scrollbar_GotFocus;
	}

	// Token: 0x060002B6 RID: 694 RVA: 0x0000CD28 File Offset: 0x0000AF28
	private void detachScrollbarEvents()
	{
		if (this.scrollbar == null || !this.eventsAttached)
		{
			return;
		}
		this.eventsAttached = false;
		this.scrollbar.ValueChanged -= this.scrollbar_ValueChanged;
		this.scrollbar.GotFocus -= this.scrollbar_GotFocus;
	}

	// Token: 0x060002B7 RID: 695 RVA: 0x0000CD81 File Offset: 0x0000AF81
	private void scrollbar_GotFocus(dfControl control, dfFocusEventArgs args)
	{
		base.Focus();
	}

	// Token: 0x060002B8 RID: 696 RVA: 0x0000CD89 File Offset: 0x0000AF89
	private void scrollbar_ValueChanged(dfControl control, float value)
	{
		this.ScrollPosition = value;
	}

	// Token: 0x060002B9 RID: 697 RVA: 0x0000CD94 File Offset: 0x0000AF94
	private void synchronizeScrollbar()
	{
		if (this.scrollbar == null)
		{
			return;
		}
		int num = this.items.Length * this.itemHeight;
		float scrollSize = this.size.y - (float)this.listPadding.vertical;
		this.scrollbar.IncrementAmount = (float)this.itemHeight;
		this.scrollbar.MinValue = 0f;
		this.scrollbar.MaxValue = (float)num;
		this.scrollbar.ScrollSize = scrollSize;
		this.scrollbar.Value = this.scrollPosition;
	}

	// Token: 0x060002BA RID: 698 RVA: 0x0000CE28 File Offset: 0x0000B028
	public dfList<dfRenderData> RenderMultiple()
	{
		if (base.Atlas == null || this.Font == null)
		{
			return null;
		}
		if (!this.isVisible)
		{
			return null;
		}
		if (this.renderData == null)
		{
			this.renderData = dfRenderData.Obtain();
			this.textRenderData = dfRenderData.Obtain();
			this.isControlInvalidated = true;
		}
		Matrix4x4 localToWorldMatrix = base.transform.localToWorldMatrix;
		if (!this.isControlInvalidated)
		{
			for (int i = 0; i < this.buffers.Count; i++)
			{
				this.buffers[i].Transform = localToWorldMatrix;
			}
			return this.buffers;
		}
		this.buffers.Clear();
		this.renderData.Clear();
		this.renderData.Material = base.Atlas.Material;
		this.renderData.Transform = localToWorldMatrix;
		this.buffers.Add(this.renderData);
		this.textRenderData.Clear();
		this.textRenderData.Material = base.Atlas.Material;
		this.textRenderData.Transform = localToWorldMatrix;
		this.buffers.Add(this.textRenderData);
		this.renderBackground();
		int count = this.renderData.Vertices.Count;
		this.renderHover();
		this.renderSelection();
		this.renderItems(this.textRenderData);
		this.clipQuads(this.renderData, count);
		this.clipQuads(this.textRenderData, 0);
		this.isControlInvalidated = false;
		base.updateCollider();
		return this.buffers;
	}

	// Token: 0x060002BB RID: 699 RVA: 0x0000CFA8 File Offset: 0x0000B1A8
	private void bindTextureRebuildCallback()
	{
		if (this.isFontCallbackAssigned || this.Font == null)
		{
			return;
		}
		if (this.Font is dfDynamicFont)
		{
			Font baseFont = (this.Font as dfDynamicFont).BaseFont;
			baseFont.textureRebuildCallback = (Font.FontTextureRebuildCallback)Delegate.Combine(baseFont.textureRebuildCallback, new Font.FontTextureRebuildCallback(this.onFontTextureRebuilt));
			this.isFontCallbackAssigned = true;
		}
	}

	// Token: 0x060002BC RID: 700 RVA: 0x0000D014 File Offset: 0x0000B214
	private void unbindTextureRebuildCallback()
	{
		if (!this.isFontCallbackAssigned || this.Font == null)
		{
			return;
		}
		if (this.Font is dfDynamicFont)
		{
			Font baseFont = (this.Font as dfDynamicFont).BaseFont;
			baseFont.textureRebuildCallback = (Font.FontTextureRebuildCallback)Delegate.Remove(baseFont.textureRebuildCallback, new Font.FontTextureRebuildCallback(this.onFontTextureRebuilt));
		}
		this.isFontCallbackAssigned = false;
	}

	// Token: 0x060002BD RID: 701 RVA: 0x0000D080 File Offset: 0x0000B280
	private void requestCharacterInfo()
	{
		dfDynamicFont dfDynamicFont = this.Font as dfDynamicFont;
		if (dfDynamicFont == null)
		{
			return;
		}
		if (!dfFontManager.IsDirty(this.Font))
		{
			return;
		}
		if (this.items == null || this.items.Length == 0)
		{
			return;
		}
		float textScaleMultiplier = this.getTextScaleMultiplier();
		int fontSize = Mathf.CeilToInt((float)this.font.FontSize * textScaleMultiplier);
		for (int i = 0; i < this.items.Length; i++)
		{
			dfDynamicFont.AddCharacterRequest(this.items[i], fontSize, FontStyle.Normal);
		}
	}

	// Token: 0x060002BE RID: 702 RVA: 0x0000D101 File Offset: 0x0000B301
	private void onFontTextureRebuilt()
	{
		this.requestCharacterInfo();
		this.Invalidate();
	}

	// Token: 0x060002BF RID: 703 RVA: 0x0000D10F File Offset: 0x0000B30F
	public void UpdateFontInfo()
	{
		this.requestCharacterInfo();
	}

	// Token: 0x040000E0 RID: 224
	[SerializeField]
	protected dfFontBase font;

	// Token: 0x040000E1 RID: 225
	[SerializeField]
	protected RectOffset listPadding = new RectOffset();

	// Token: 0x040000E2 RID: 226
	[SerializeField]
	protected int selectedIndex = -1;

	// Token: 0x040000E3 RID: 227
	[SerializeField]
	protected Color32 itemTextColor = UnityEngine.Color.white;

	// Token: 0x040000E4 RID: 228
	[SerializeField]
	protected float itemTextScale = 1f;

	// Token: 0x040000E5 RID: 229
	[SerializeField]
	protected int itemHeight = 25;

	// Token: 0x040000E6 RID: 230
	[SerializeField]
	protected RectOffset itemPadding = new RectOffset();

	// Token: 0x040000E7 RID: 231
	[SerializeField]
	protected string[] items = new string[0];

	// Token: 0x040000E8 RID: 232
	[SerializeField]
	protected string itemHighlight = "";

	// Token: 0x040000E9 RID: 233
	[SerializeField]
	protected string itemHover = "";

	// Token: 0x040000EA RID: 234
	[SerializeField]
	protected dfScrollbar scrollbar;

	// Token: 0x040000EB RID: 235
	[SerializeField]
	protected bool animateHover;

	// Token: 0x040000EC RID: 236
	[SerializeField]
	protected bool shadow;

	// Token: 0x040000ED RID: 237
	[SerializeField]
	protected dfTextScaleMode textScaleMode;

	// Token: 0x040000EE RID: 238
	[SerializeField]
	protected Color32 shadowColor = UnityEngine.Color.black;

	// Token: 0x040000EF RID: 239
	[SerializeField]
	protected Vector2 shadowOffset = new Vector2(1f, -1f);

	// Token: 0x040000F0 RID: 240
	[SerializeField]
	protected TextAlignment itemAlignment;

	// Token: 0x040000F1 RID: 241
	private bool isFontCallbackAssigned;

	// Token: 0x040000F2 RID: 242
	private bool eventsAttached;

	// Token: 0x040000F3 RID: 243
	private float scrollPosition;

	// Token: 0x040000F4 RID: 244
	private int hoverIndex = -1;

	// Token: 0x040000F5 RID: 245
	private float hoverTweenLocation;

	// Token: 0x040000F6 RID: 246
	private Vector2 touchStartPosition = Vector2.zero;

	// Token: 0x040000F7 RID: 247
	private Vector2 startSize = Vector2.zero;

	// Token: 0x040000F8 RID: 248
	private dfRenderData textRenderData;

	// Token: 0x040000F9 RID: 249
	private dfList<dfRenderData> buffers = dfList<dfRenderData>.Obtain();
}

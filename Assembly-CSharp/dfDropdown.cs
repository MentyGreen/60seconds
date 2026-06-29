using System;
using UnityEngine;

// Token: 0x0200000B RID: 11
[ExecuteInEditMode]
[dfCategory("Basic Controls")]
[dfTooltip("Implements a drop-down list control")]
[dfHelp("http://www.daikonforge.com/docs/df-gui/classdf_dropdown.html")]
[AddComponentMenu("Daikon Forge/User Interface/Dropdown List")]
[Serializable]
public class dfDropdown : dfInteractiveBase, IDFMultiRender, IRendersText
{
	// Token: 0x1400002A RID: 42
	// (add) Token: 0x060001BA RID: 442 RVA: 0x00008A60 File Offset: 0x00006C60
	// (remove) Token: 0x060001BB RID: 443 RVA: 0x00008A98 File Offset: 0x00006C98
	public event dfDropdown.PopupEventHandler DropdownOpen;

	// Token: 0x1400002B RID: 43
	// (add) Token: 0x060001BC RID: 444 RVA: 0x00008AD0 File Offset: 0x00006CD0
	// (remove) Token: 0x060001BD RID: 445 RVA: 0x00008B08 File Offset: 0x00006D08
	public event dfDropdown.PopupEventHandler DropdownClose;

	// Token: 0x1400002C RID: 44
	// (add) Token: 0x060001BE RID: 446 RVA: 0x00008B40 File Offset: 0x00006D40
	// (remove) Token: 0x060001BF RID: 447 RVA: 0x00008B78 File Offset: 0x00006D78
	public event PropertyChangedEventHandler<int> SelectedIndexChanged;

	// Token: 0x17000054 RID: 84
	// (get) Token: 0x060001C0 RID: 448 RVA: 0x00008BAD File Offset: 0x00006DAD
	// (set) Token: 0x060001C1 RID: 449 RVA: 0x00008BB5 File Offset: 0x00006DB5
	public bool ClickWhenSpacePressed
	{
		get
		{
			return this.clickWhenSpacePressed;
		}
		set
		{
			this.clickWhenSpacePressed = value;
		}
	}

	// Token: 0x17000055 RID: 85
	// (get) Token: 0x060001C2 RID: 450 RVA: 0x00008BC0 File Offset: 0x00006DC0
	// (set) Token: 0x060001C3 RID: 451 RVA: 0x00008BFD File Offset: 0x00006DFD
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
				this.ClosePopup();
				this.unbindTextureRebuildCallback();
				this.font = value;
				this.bindTextureRebuildCallback();
				this.Invalidate();
			}
		}
	}

	// Token: 0x17000056 RID: 86
	// (get) Token: 0x060001C4 RID: 452 RVA: 0x00008C2C File Offset: 0x00006E2C
	// (set) Token: 0x060001C5 RID: 453 RVA: 0x00008C34 File Offset: 0x00006E34
	public dfScrollbar ListScrollbar
	{
		get
		{
			return this.listScrollbar;
		}
		set
		{
			if (value != this.listScrollbar)
			{
				this.listScrollbar = value;
				this.Invalidate();
			}
		}
	}

	// Token: 0x17000057 RID: 87
	// (get) Token: 0x060001C6 RID: 454 RVA: 0x00008C51 File Offset: 0x00006E51
	// (set) Token: 0x060001C7 RID: 455 RVA: 0x00008C59 File Offset: 0x00006E59
	public Vector2 ListOffset
	{
		get
		{
			return this.listOffset;
		}
		set
		{
			if (Vector2.Distance(this.listOffset, value) > 1f)
			{
				this.listOffset = value;
				this.Invalidate();
			}
		}
	}

	// Token: 0x17000058 RID: 88
	// (get) Token: 0x060001C8 RID: 456 RVA: 0x00008C7B File Offset: 0x00006E7B
	// (set) Token: 0x060001C9 RID: 457 RVA: 0x00008C83 File Offset: 0x00006E83
	public string ListBackground
	{
		get
		{
			return this.listBackground;
		}
		set
		{
			if (value != this.listBackground)
			{
				this.ClosePopup();
				this.listBackground = value;
				this.Invalidate();
			}
		}
	}

	// Token: 0x17000059 RID: 89
	// (get) Token: 0x060001CA RID: 458 RVA: 0x00008CA6 File Offset: 0x00006EA6
	// (set) Token: 0x060001CB RID: 459 RVA: 0x00008CAE File Offset: 0x00006EAE
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

	// Token: 0x1700005A RID: 90
	// (get) Token: 0x060001CC RID: 460 RVA: 0x00008CCB File Offset: 0x00006ECB
	// (set) Token: 0x060001CD RID: 461 RVA: 0x00008CD3 File Offset: 0x00006ED3
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
				this.ClosePopup();
				this.itemHighlight = value;
				this.Invalidate();
			}
		}
	}

	// Token: 0x1700005B RID: 91
	// (get) Token: 0x060001CE RID: 462 RVA: 0x00008CF6 File Offset: 0x00006EF6
	// (set) Token: 0x060001CF RID: 463 RVA: 0x00008D08 File Offset: 0x00006F08
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
					break;
				}
			}
			this.Invalidate();
		}
	}

	// Token: 0x1700005C RID: 92
	// (get) Token: 0x060001D0 RID: 464 RVA: 0x00008D4E File Offset: 0x00006F4E
	// (set) Token: 0x060001D1 RID: 465 RVA: 0x00008D58 File Offset: 0x00006F58
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
				if (this.popup != null)
				{
					this.popup.SelectedIndex = value;
				}
				this.selectedIndex = value;
				this.OnSelectedIndexChanged();
				this.Invalidate();
			}
		}
	}

	// Token: 0x1700005D RID: 93
	// (get) Token: 0x060001D2 RID: 466 RVA: 0x00008DB6 File Offset: 0x00006FB6
	// (set) Token: 0x060001D3 RID: 467 RVA: 0x00008DD1 File Offset: 0x00006FD1
	public RectOffset TextFieldPadding
	{
		get
		{
			if (this.textFieldPadding == null)
			{
				this.textFieldPadding = new RectOffset();
			}
			return this.textFieldPadding;
		}
		set
		{
			value = value.ConstrainPadding();
			if (!object.Equals(value, this.textFieldPadding))
			{
				this.textFieldPadding = value;
				this.Invalidate();
			}
		}
	}

	// Token: 0x1700005E RID: 94
	// (get) Token: 0x060001D4 RID: 468 RVA: 0x00008DF6 File Offset: 0x00006FF6
	// (set) Token: 0x060001D5 RID: 469 RVA: 0x00008DFE File Offset: 0x00006FFE
	public Color32 TextColor
	{
		get
		{
			return this.textColor;
		}
		set
		{
			this.ClosePopup();
			this.textColor = value;
			this.Invalidate();
		}
	}

	// Token: 0x1700005F RID: 95
	// (get) Token: 0x060001D6 RID: 470 RVA: 0x00008E13 File Offset: 0x00007013
	// (set) Token: 0x060001D7 RID: 471 RVA: 0x00008E1B File Offset: 0x0000701B
	public Color32 DisabledTextColor
	{
		get
		{
			return this.disabledTextColor;
		}
		set
		{
			this.ClosePopup();
			this.disabledTextColor = value;
			this.Invalidate();
		}
	}

	// Token: 0x17000060 RID: 96
	// (get) Token: 0x060001D8 RID: 472 RVA: 0x00008E30 File Offset: 0x00007030
	// (set) Token: 0x060001D9 RID: 473 RVA: 0x00008E38 File Offset: 0x00007038
	public float TextScale
	{
		get
		{
			return this.textScale;
		}
		set
		{
			value = Mathf.Max(0.1f, value);
			if (!Mathf.Approximately(this.textScale, value))
			{
				this.ClosePopup();
				dfFontManager.Invalidate(this.Font);
				this.textScale = value;
				this.Invalidate();
			}
		}
	}

	// Token: 0x17000061 RID: 97
	// (get) Token: 0x060001DA RID: 474 RVA: 0x00008E73 File Offset: 0x00007073
	// (set) Token: 0x060001DB RID: 475 RVA: 0x00008E7B File Offset: 0x0000707B
	public int ItemHeight
	{
		get
		{
			return this.itemHeight;
		}
		set
		{
			value = Mathf.Max(1, value);
			if (value != this.itemHeight)
			{
				this.ClosePopup();
				this.itemHeight = value;
				this.Invalidate();
			}
		}
	}

	// Token: 0x17000062 RID: 98
	// (get) Token: 0x060001DC RID: 476 RVA: 0x00008EA2 File Offset: 0x000070A2
	// (set) Token: 0x060001DD RID: 477 RVA: 0x00008EBE File Offset: 0x000070BE
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
			this.ClosePopup();
			if (value == null)
			{
				value = new string[0];
			}
			this.items = value;
			this.Invalidate();
		}
	}

	// Token: 0x17000063 RID: 99
	// (get) Token: 0x060001DE RID: 478 RVA: 0x00008EDE File Offset: 0x000070DE
	// (set) Token: 0x060001DF RID: 479 RVA: 0x00008EF9 File Offset: 0x000070F9
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

	// Token: 0x17000064 RID: 100
	// (get) Token: 0x060001E0 RID: 480 RVA: 0x00008F1E File Offset: 0x0000711E
	// (set) Token: 0x060001E1 RID: 481 RVA: 0x00008F26 File Offset: 0x00007126
	public dfDropdown.PopupListPosition ListPosition
	{
		get
		{
			return this.listPosition;
		}
		set
		{
			if (value != this.ListPosition)
			{
				this.ClosePopup();
				this.listPosition = value;
				this.Invalidate();
			}
		}
	}

	// Token: 0x17000065 RID: 101
	// (get) Token: 0x060001E2 RID: 482 RVA: 0x00008F44 File Offset: 0x00007144
	// (set) Token: 0x060001E3 RID: 483 RVA: 0x00008F4C File Offset: 0x0000714C
	public int MaxListWidth
	{
		get
		{
			return this.listWidth;
		}
		set
		{
			this.listWidth = value;
		}
	}

	// Token: 0x17000066 RID: 102
	// (get) Token: 0x060001E4 RID: 484 RVA: 0x00008F55 File Offset: 0x00007155
	// (set) Token: 0x060001E5 RID: 485 RVA: 0x00008F5D File Offset: 0x0000715D
	public int MaxListHeight
	{
		get
		{
			return this.listHeight;
		}
		set
		{
			this.listHeight = value;
			this.Invalidate();
		}
	}

	// Token: 0x17000067 RID: 103
	// (get) Token: 0x060001E6 RID: 486 RVA: 0x00008F6C File Offset: 0x0000716C
	// (set) Token: 0x060001E7 RID: 487 RVA: 0x00008F74 File Offset: 0x00007174
	public dfControl TriggerButton
	{
		get
		{
			return this.triggerButton;
		}
		set
		{
			if (value != this.triggerButton)
			{
				this.detachChildEvents();
				this.triggerButton = value;
				this.attachChildEvents();
				this.Invalidate();
			}
		}
	}

	// Token: 0x17000068 RID: 104
	// (get) Token: 0x060001E8 RID: 488 RVA: 0x00008F9D File Offset: 0x0000719D
	// (set) Token: 0x060001E9 RID: 489 RVA: 0x00008FA5 File Offset: 0x000071A5
	public bool OpenOnMouseDown
	{
		get
		{
			return this.openOnMouseDown;
		}
		set
		{
			this.openOnMouseDown = value;
		}
	}

	// Token: 0x17000069 RID: 105
	// (get) Token: 0x060001EA RID: 490 RVA: 0x00008FAE File Offset: 0x000071AE
	// (set) Token: 0x060001EB RID: 491 RVA: 0x00008FB6 File Offset: 0x000071B6
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

	// Token: 0x1700006A RID: 106
	// (get) Token: 0x060001EC RID: 492 RVA: 0x00008FCE File Offset: 0x000071CE
	// (set) Token: 0x060001ED RID: 493 RVA: 0x00008FD6 File Offset: 0x000071D6
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

	// Token: 0x1700006B RID: 107
	// (get) Token: 0x060001EE RID: 494 RVA: 0x00008FFF File Offset: 0x000071FF
	// (set) Token: 0x060001EF RID: 495 RVA: 0x00009007 File Offset: 0x00007207
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

	// Token: 0x060001F0 RID: 496 RVA: 0x00009024 File Offset: 0x00007224
	protected internal override void OnMouseWheel(dfMouseEventArgs args)
	{
		this.SelectedIndex = Mathf.Max(0, this.SelectedIndex - Mathf.RoundToInt(args.WheelDelta));
		args.Use();
		base.OnMouseWheel(args);
	}

	// Token: 0x060001F1 RID: 497 RVA: 0x00009054 File Offset: 0x00007254
	protected internal override void OnMouseDown(dfMouseEventArgs args)
	{
		if (!this.openOnMouseDown || args.Used || args.Buttons != dfMouseButtons.Left || !(args.Source == this))
		{
			base.OnMouseDown(args);
			return;
		}
		args.Use();
		base.OnMouseDown(args);
		if (this.popup == null)
		{
			this.OpenPopup();
			return;
		}
		this.ClosePopup();
	}

	// Token: 0x060001F2 RID: 498 RVA: 0x000090B8 File Offset: 0x000072B8
	protected internal override void OnKeyDown(dfKeyEventArgs args)
	{
		KeyCode keyCode = args.KeyCode;
		if (keyCode != KeyCode.Return && keyCode != KeyCode.Space)
		{
			switch (keyCode)
			{
			case KeyCode.UpArrow:
				this.SelectedIndex = Mathf.Max(0, this.selectedIndex - 1);
				break;
			case KeyCode.DownArrow:
				this.SelectedIndex = Mathf.Min(this.items.Length - 1, this.selectedIndex + 1);
				break;
			case KeyCode.Home:
				this.SelectedIndex = 0;
				break;
			case KeyCode.End:
				this.SelectedIndex = this.items.Length - 1;
				break;
			}
		}
		else if (this.ClickWhenSpacePressed && this.IsInteractive)
		{
			this.OpenPopup();
		}
		base.OnKeyDown(args);
	}

	// Token: 0x060001F3 RID: 499 RVA: 0x00009170 File Offset: 0x00007370
	public override void OnEnable()
	{
		base.OnEnable();
		bool flag = this.Font != null && this.Font.IsValid;
		if (Application.isPlaying && !flag)
		{
			this.Font = base.GetManager().DefaultFont;
		}
		this.bindTextureRebuildCallback();
	}

	// Token: 0x060001F4 RID: 500 RVA: 0x000091C1 File Offset: 0x000073C1
	public override void OnDisable()
	{
		base.OnDisable();
		this.unbindTextureRebuildCallback();
		this.ClosePopup(false);
	}

	// Token: 0x060001F5 RID: 501 RVA: 0x000091D6 File Offset: 0x000073D6
	public override void OnDestroy()
	{
		base.OnDestroy();
		this.ClosePopup(false);
		this.detachChildEvents();
	}

	// Token: 0x060001F6 RID: 502 RVA: 0x000091EB File Offset: 0x000073EB
	public override void Update()
	{
		base.Update();
		this.checkForPopupClose();
	}

	// Token: 0x060001F7 RID: 503 RVA: 0x000091FC File Offset: 0x000073FC
	private void checkForPopupClose()
	{
		if (this.popup == null || !Input.GetMouseButtonDown(0))
		{
			return;
		}
		Camera camera = base.GetCamera();
		Ray ray = camera.ScreenPointToRay(Input.mousePosition);
		RaycastHit raycastHit;
		if (this.triggerButton != null && this.triggerButton.GetComponent<Collider>().Raycast(ray, out raycastHit, camera.farClipPlane))
		{
			return;
		}
		if (this.popup.GetComponent<Collider>().Raycast(ray, out raycastHit, camera.farClipPlane))
		{
			return;
		}
		if (this.popup.Scrollbar != null && this.popup.Scrollbar.GetComponent<Collider>().Raycast(ray, out raycastHit, camera.farClipPlane))
		{
			return;
		}
		if (base.GetComponent<Collider>().Raycast(ray, out raycastHit, camera.farClipPlane))
		{
			return;
		}
		this.ClosePopup();
	}

	// Token: 0x060001F8 RID: 504 RVA: 0x000092CA File Offset: 0x000074CA
	public override void LateUpdate()
	{
		base.LateUpdate();
		if (!Application.isPlaying)
		{
			return;
		}
		if (!this.eventsAttached)
		{
			this.attachChildEvents();
		}
		if (this.popup != null && !this.popup.ContainsFocus)
		{
			this.ClosePopup();
		}
	}

	// Token: 0x060001F9 RID: 505 RVA: 0x00009309 File Offset: 0x00007509
	private void attachChildEvents()
	{
		if (this.triggerButton != null && !this.eventsAttached)
		{
			this.eventsAttached = true;
			this.triggerButton.Click += this.trigger_Click;
		}
	}

	// Token: 0x060001FA RID: 506 RVA: 0x0000933F File Offset: 0x0000753F
	private void detachChildEvents()
	{
		if (this.triggerButton != null && this.eventsAttached)
		{
			this.triggerButton.Click -= this.trigger_Click;
			this.eventsAttached = false;
		}
	}

	// Token: 0x060001FB RID: 507 RVA: 0x00009375 File Offset: 0x00007575
	private void trigger_Click(dfControl control, dfMouseEventArgs mouseEvent)
	{
		if (mouseEvent.Source == this.triggerButton && !mouseEvent.Used)
		{
			mouseEvent.Use();
			if (this.popup == null)
			{
				this.OpenPopup();
				return;
			}
			this.ClosePopup();
		}
	}

	// Token: 0x060001FC RID: 508 RVA: 0x000093B3 File Offset: 0x000075B3
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

	// Token: 0x060001FD RID: 509 RVA: 0x000093F4 File Offset: 0x000075F4
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

	// Token: 0x060001FE RID: 510 RVA: 0x00009450 File Offset: 0x00007650
	private void renderText(dfRenderData buffer)
	{
		if (this.selectedIndex < 0 || this.selectedIndex >= this.items.Length)
		{
			return;
		}
		string text = this.items[this.selectedIndex];
		float num = base.PixelsToUnits();
		Vector2 maxSize = new Vector2(this.size.x - (float)this.textFieldPadding.horizontal, this.size.y - (float)this.textFieldPadding.vertical);
		Vector3 vector = this.pivot.TransformToUpperLeft(base.Size);
		Vector3 vectorOffset = new Vector3(vector.x + (float)this.textFieldPadding.left, vector.y - (float)this.textFieldPadding.top, 0f) * num;
		Color32 defaultColor = base.IsEnabled ? this.TextColor : this.DisabledTextColor;
		using (dfFontRendererBase dfFontRendererBase = this.font.ObtainRenderer())
		{
			dfFontRendererBase.WordWrap = false;
			dfFontRendererBase.MaxSize = maxSize;
			dfFontRendererBase.PixelRatio = num;
			dfFontRendererBase.TextScale = this.TextScale;
			dfFontRendererBase.VectorOffset = vectorOffset;
			dfFontRendererBase.MultiLine = false;
			dfFontRendererBase.TextAlign = TextAlignment.Left;
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
				dynamicFontRenderer.SpriteBuffer = buffer;
			}
			dfFontRendererBase.Render(text, buffer);
		}
	}

	// Token: 0x060001FF RID: 511 RVA: 0x00009608 File Offset: 0x00007808
	public void AddItem(string item)
	{
		string[] array = new string[this.items.Length + 1];
		Array.Copy(this.items, array, this.items.Length);
		array[this.items.Length] = item;
		this.items = array;
	}

	// Token: 0x06000200 RID: 512 RVA: 0x0000964C File Offset: 0x0000784C
	public void OpenPopup()
	{
		if (this.popup != null || this.items.Length == 0)
		{
			return;
		}
		Vector2 vector = this.calculatePopupSize();
		this.popup = base.GetManager().AddControl<dfListbox>();
		this.popup.name = base.name + " - Dropdown List";
		this.popup.gameObject.hideFlags = HideFlags.DontSave;
		this.popup.Atlas = base.Atlas;
		this.popup.Anchor = (dfAnchorStyle.Top | dfAnchorStyle.Left);
		this.popup.Color = base.Color;
		this.popup.Font = this.Font;
		this.popup.Pivot = dfPivotPoint.TopLeft;
		this.popup.Size = vector;
		this.popup.Font = this.Font;
		this.popup.ItemHeight = this.ItemHeight;
		this.popup.ItemHighlight = this.ItemHighlight;
		this.popup.ItemHover = this.ItemHover;
		this.popup.ItemPadding = this.TextFieldPadding;
		this.popup.ItemTextColor = this.TextColor;
		this.popup.ItemTextScale = this.TextScale;
		this.popup.Items = this.Items;
		this.popup.ListPadding = this.ListPadding;
		this.popup.BackgroundSprite = this.ListBackground;
		this.popup.Shadow = this.Shadow;
		this.popup.ShadowColor = this.ShadowColor;
		this.popup.ShadowOffset = this.ShadowOffset;
		this.popup.BringToFront();
		if (dfGUIManager.GetModalControl() != null)
		{
			dfGUIManager.PushModal(this.popup);
		}
		if (vector.y >= (float)this.MaxListHeight && this.listScrollbar != null)
		{
			GameObject gameObject = Object.Instantiate<GameObject>(this.listScrollbar.gameObject);
			dfScrollbar activeScrollbar = gameObject.GetComponent<dfScrollbar>();
			float d = base.PixelsToUnits();
			Vector3 a = this.popup.transform.TransformDirection(Vector3.right);
			Vector3 position = this.popup.transform.position + a * (vector.x - activeScrollbar.Width) * d;
			this.popup.AddControl(activeScrollbar);
			this.popup.Width -= activeScrollbar.Width;
			this.popup.Scrollbar = activeScrollbar;
			this.popup.SizeChanged += delegate(dfControl control, Vector2 size)
			{
				activeScrollbar.Height = control.Height;
			};
			activeScrollbar.transform.parent = this.popup.transform;
			activeScrollbar.transform.position = position;
			activeScrollbar.Anchor = (dfAnchorStyle.Top | dfAnchorStyle.Bottom);
			activeScrollbar.Height = this.popup.Height;
		}
		Vector3 position2 = this.calculatePopupPosition((int)this.popup.Size.y);
		this.popup.transform.position = position2;
		this.popup.transform.rotation = base.transform.rotation;
		this.popup.SelectedIndexChanged += this.popup_SelectedIndexChanged;
		this.popup.LeaveFocus += this.popup_LostFocus;
		this.popup.ItemClicked += this.popup_ItemClicked;
		this.popup.KeyDown += this.popup_KeyDown;
		this.popup.SelectedIndex = Mathf.Max(0, this.SelectedIndex);
		this.popup.EnsureVisible(this.popup.SelectedIndex);
		this.popup.Focus();
		if (this.DropdownOpen != null)
		{
			bool flag = false;
			this.DropdownOpen(this, this.popup, ref flag);
		}
		base.Signal("OnDropdownOpen", this, this.popup);
	}

	// Token: 0x06000201 RID: 513 RVA: 0x00009A58 File Offset: 0x00007C58
	public void ClosePopup()
	{
		this.ClosePopup(true);
	}

	// Token: 0x06000202 RID: 514 RVA: 0x00009A64 File Offset: 0x00007C64
	public void ClosePopup(bool allowOverride)
	{
		if (this.popup == null)
		{
			return;
		}
		if (dfGUIManager.GetModalControl() == this.popup)
		{
			dfGUIManager.PopModal();
		}
		this.popup.LostFocus -= this.popup_LostFocus;
		this.popup.SelectedIndexChanged -= this.popup_SelectedIndexChanged;
		this.popup.ItemClicked -= this.popup_ItemClicked;
		this.popup.KeyDown -= this.popup_KeyDown;
		if (!allowOverride)
		{
			Object.Destroy(this.popup.gameObject);
			this.popup = null;
			return;
		}
		bool flag = false;
		if (this.DropdownClose != null)
		{
			this.DropdownClose(this, this.popup, ref flag);
		}
		if (!flag)
		{
			base.Signal("OnDropdownClose", this, this.popup);
		}
		if (!flag)
		{
			Object.Destroy(this.popup.gameObject);
		}
		this.popup = null;
	}

	// Token: 0x06000203 RID: 515 RVA: 0x00009B5C File Offset: 0x00007D5C
	private Vector3 calculatePopupPosition(int height)
	{
		float d = base.PixelsToUnits();
		Vector3 a = base.transform.position + this.pivot.TransformToUpperLeft(this.size) * d;
		Vector3 scaledDirection = base.getScaledDirection(Vector3.down);
		Vector3 a2 = base.transformOffset(this.listOffset);
		Vector3 result = a + (a2 + scaledDirection * base.Size.y) * d;
		Vector3 result2 = a + (a2 - scaledDirection * this.popup.Size.y) * d;
		if (this.listPosition == dfDropdown.PopupListPosition.Above)
		{
			return result2;
		}
		if (this.listPosition == dfDropdown.PopupListPosition.Below)
		{
			return result;
		}
		Vector2 screenSize = base.GetManager().GetScreenSize();
		if (base.GetAbsolutePosition().y + base.Height + (float)height >= screenSize.y)
		{
			return result2;
		}
		return result;
	}

	// Token: 0x06000204 RID: 516 RVA: 0x00009C48 File Offset: 0x00007E48
	private Vector2 calculatePopupSize()
	{
		float x = (this.MaxListWidth > 0) ? ((float)this.MaxListWidth) : this.size.x;
		int b = this.items.Length * this.itemHeight + this.listPadding.vertical;
		if (this.items.Length == 0)
		{
			b = this.itemHeight / 2 + this.listPadding.vertical;
		}
		return new Vector2(x, (float)Mathf.Min(this.MaxListHeight, b));
	}

	// Token: 0x06000205 RID: 517 RVA: 0x00009CBE File Offset: 0x00007EBE
	private void popup_KeyDown(dfControl control, dfKeyEventArgs args)
	{
		if (args.KeyCode == KeyCode.Escape || args.KeyCode == KeyCode.Return)
		{
			this.ClosePopup();
			base.Focus();
		}
	}

	// Token: 0x06000206 RID: 518 RVA: 0x00009CE0 File Offset: 0x00007EE0
	private void popup_ItemClicked(dfControl control, int selectedIndex)
	{
		base.Focus();
	}

	// Token: 0x06000207 RID: 519 RVA: 0x00009CE8 File Offset: 0x00007EE8
	private void popup_LostFocus(dfControl control, dfFocusEventArgs args)
	{
		if (this.popup != null && !this.popup.ContainsFocus)
		{
			this.ClosePopup();
		}
	}

	// Token: 0x06000208 RID: 520 RVA: 0x00009D0B File Offset: 0x00007F0B
	private void popup_SelectedIndexChanged(dfControl control, int selectedIndex)
	{
		this.SelectedIndex = selectedIndex;
		this.Invalidate();
	}

	// Token: 0x06000209 RID: 521 RVA: 0x00009D1C File Offset: 0x00007F1C
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
		if (!this.isControlInvalidated)
		{
			for (int i = 0; i < this.buffers.Count; i++)
			{
				this.buffers[i].Transform = base.transform.localToWorldMatrix;
			}
			return this.buffers;
		}
		this.buffers.Clear();
		this.renderData.Clear();
		this.renderData.Material = base.Atlas.Material;
		this.renderData.Transform = base.transform.localToWorldMatrix;
		this.buffers.Add(this.renderData);
		this.textRenderData.Clear();
		this.textRenderData.Material = base.Atlas.Material;
		this.textRenderData.Transform = base.transform.localToWorldMatrix;
		this.buffers.Add(this.textRenderData);
		this.renderBackground();
		this.renderText(this.textRenderData);
		this.isControlInvalidated = false;
		base.updateCollider();
		return this.buffers;
	}

	// Token: 0x0600020A RID: 522 RVA: 0x00009E78 File Offset: 0x00008078
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

	// Token: 0x0600020B RID: 523 RVA: 0x00009EE4 File Offset: 0x000080E4
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

	// Token: 0x0600020C RID: 524 RVA: 0x00009F50 File Offset: 0x00008150
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
		string selectedValue = this.SelectedValue;
		if (string.IsNullOrEmpty(selectedValue))
		{
			return;
		}
		float num = this.TextScale;
		int fontSize = Mathf.CeilToInt((float)this.font.FontSize * num);
		dfDynamicFont.AddCharacterRequest(selectedValue, fontSize, FontStyle.Normal);
	}

	// Token: 0x0600020D RID: 525 RVA: 0x00009FB5 File Offset: 0x000081B5
	private void onFontTextureRebuilt()
	{
		this.requestCharacterInfo();
		this.Invalidate();
	}

	// Token: 0x0600020E RID: 526 RVA: 0x00009FC3 File Offset: 0x000081C3
	public void UpdateFontInfo()
	{
		this.requestCharacterInfo();
	}

	// Token: 0x0400009E RID: 158
	[SerializeField]
	protected dfFontBase font;

	// Token: 0x0400009F RID: 159
	[SerializeField]
	protected int selectedIndex = -1;

	// Token: 0x040000A0 RID: 160
	[SerializeField]
	protected dfControl triggerButton;

	// Token: 0x040000A1 RID: 161
	[SerializeField]
	protected Color32 disabledTextColor = UnityEngine.Color.gray;

	// Token: 0x040000A2 RID: 162
	[SerializeField]
	protected Color32 textColor = UnityEngine.Color.white;

	// Token: 0x040000A3 RID: 163
	[SerializeField]
	protected float textScale = 1f;

	// Token: 0x040000A4 RID: 164
	[SerializeField]
	protected RectOffset textFieldPadding = new RectOffset();

	// Token: 0x040000A5 RID: 165
	[SerializeField]
	protected dfDropdown.PopupListPosition listPosition;

	// Token: 0x040000A6 RID: 166
	[SerializeField]
	protected int listWidth;

	// Token: 0x040000A7 RID: 167
	[SerializeField]
	protected int listHeight = 200;

	// Token: 0x040000A8 RID: 168
	[SerializeField]
	protected RectOffset listPadding = new RectOffset();

	// Token: 0x040000A9 RID: 169
	[SerializeField]
	protected dfScrollbar listScrollbar;

	// Token: 0x040000AA RID: 170
	[SerializeField]
	protected int itemHeight = 25;

	// Token: 0x040000AB RID: 171
	[SerializeField]
	protected string itemHighlight = "";

	// Token: 0x040000AC RID: 172
	[SerializeField]
	protected string itemHover = "";

	// Token: 0x040000AD RID: 173
	[SerializeField]
	protected string listBackground = "";

	// Token: 0x040000AE RID: 174
	[SerializeField]
	protected Vector2 listOffset = Vector2.zero;

	// Token: 0x040000AF RID: 175
	[SerializeField]
	protected string[] items = new string[0];

	// Token: 0x040000B0 RID: 176
	[SerializeField]
	protected bool shadow;

	// Token: 0x040000B1 RID: 177
	[SerializeField]
	protected Color32 shadowColor = UnityEngine.Color.black;

	// Token: 0x040000B2 RID: 178
	[SerializeField]
	protected Vector2 shadowOffset = new Vector2(1f, -1f);

	// Token: 0x040000B3 RID: 179
	[SerializeField]
	protected bool openOnMouseDown = true;

	// Token: 0x040000B4 RID: 180
	[SerializeField]
	protected bool clickWhenSpacePressed = true;

	// Token: 0x040000B5 RID: 181
	private bool eventsAttached;

	// Token: 0x040000B6 RID: 182
	private bool isFontCallbackAssigned;

	// Token: 0x040000B7 RID: 183
	private dfListbox popup;

	// Token: 0x040000B8 RID: 184
	private dfRenderData textRenderData;

	// Token: 0x040000B9 RID: 185
	private dfList<dfRenderData> buffers = dfList<dfRenderData>.Obtain();

	// Token: 0x02000356 RID: 854
	public enum PopupListPosition
	{
		// Token: 0x040015E8 RID: 5608
		Below,
		// Token: 0x040015E9 RID: 5609
		Above,
		// Token: 0x040015EA RID: 5610
		Automatic
	}

	// Token: 0x02000357 RID: 855
	// (Invoke) Token: 0x06001C2C RID: 7212
	[dfEventCategory("Popup")]
	public delegate void PopupEventHandler(dfDropdown dropdown, dfListbox popup, ref bool overridden);
}

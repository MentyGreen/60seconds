using System;
using UnityEngine;

// Token: 0x0200001A RID: 26
[dfCategory("Basic Controls")]
[dfTooltip("Used in conjunction with the dfTabContainer class to implement tabbed containers. This control maintains the tabs that are displayed for the user to select, and the dfTabContainer class manages the display of the tab pages themselves.")]
[dfHelp("http://www.daikonforge.com/docs/df-gui/classdf_tabstrip.html")]
[ExecuteInEditMode]
[AddComponentMenu("Daikon Forge/User Interface/Containers/Tab Control/Tab Strip")]
[Serializable]
public class dfTabstrip : dfControl
{
	// Token: 0x14000036 RID: 54
	// (add) Token: 0x06000421 RID: 1057 RVA: 0x00014EB0 File Offset: 0x000130B0
	// (remove) Token: 0x06000422 RID: 1058 RVA: 0x00014EE8 File Offset: 0x000130E8
	public event PropertyChangedEventHandler<int> SelectedIndexChanged;

	// Token: 0x170000F5 RID: 245
	// (get) Token: 0x06000423 RID: 1059 RVA: 0x00014F1D File Offset: 0x0001311D
	// (set) Token: 0x06000424 RID: 1060 RVA: 0x00014F28 File Offset: 0x00013128
	public dfTabContainer TabPages
	{
		get
		{
			return this.pageContainer;
		}
		set
		{
			if (this.pageContainer != value)
			{
				this.pageContainer = value;
				if (value != null)
				{
					while (value.Controls.Count < this.controls.Count)
					{
						value.AddTabPage();
					}
				}
				this.pageContainer.SelectedIndex = this.SelectedIndex;
				this.Invalidate();
			}
		}
	}

	// Token: 0x170000F6 RID: 246
	// (get) Token: 0x06000425 RID: 1061 RVA: 0x00014F8B File Offset: 0x0001318B
	// (set) Token: 0x06000426 RID: 1062 RVA: 0x00014F93 File Offset: 0x00013193
	public int SelectedIndex
	{
		get
		{
			return this.selectedIndex;
		}
		set
		{
			if (value != this.selectedIndex)
			{
				this.selectTabByIndex(value);
			}
		}
	}

	// Token: 0x170000F7 RID: 247
	// (get) Token: 0x06000427 RID: 1063 RVA: 0x00014FA8 File Offset: 0x000131A8
	// (set) Token: 0x06000428 RID: 1064 RVA: 0x00014FE9 File Offset: 0x000131E9
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

	// Token: 0x170000F8 RID: 248
	// (get) Token: 0x06000429 RID: 1065 RVA: 0x00015006 File Offset: 0x00013206
	// (set) Token: 0x0600042A RID: 1066 RVA: 0x0001500E File Offset: 0x0001320E
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

	// Token: 0x170000F9 RID: 249
	// (get) Token: 0x0600042B RID: 1067 RVA: 0x0001502B File Offset: 0x0001322B
	// (set) Token: 0x0600042C RID: 1068 RVA: 0x00015046 File Offset: 0x00013246
	public RectOffset LayoutPadding
	{
		get
		{
			if (this.layoutPadding == null)
			{
				this.layoutPadding = new RectOffset();
			}
			return this.layoutPadding;
		}
		set
		{
			value = value.ConstrainPadding();
			if (!object.Equals(value, this.layoutPadding))
			{
				this.layoutPadding = value;
				this.arrangeTabs();
			}
		}
	}

	// Token: 0x170000FA RID: 250
	// (get) Token: 0x0600042D RID: 1069 RVA: 0x0001506B File Offset: 0x0001326B
	// (set) Token: 0x0600042E RID: 1070 RVA: 0x00015073 File Offset: 0x00013273
	public bool AllowKeyboardNavigation
	{
		get
		{
			return this.allowKeyboardNavigation;
		}
		set
		{
			this.allowKeyboardNavigation = value;
		}
	}

	// Token: 0x0600042F RID: 1071 RVA: 0x0001507C File Offset: 0x0001327C
	public void EnableTab(int index)
	{
		if (this.selectedIndex >= 0 && this.selectedIndex <= this.controls.Count - 1)
		{
			this.controls[index].Enable();
		}
	}

	// Token: 0x06000430 RID: 1072 RVA: 0x000150AD File Offset: 0x000132AD
	public void DisableTab(int index)
	{
		if (this.selectedIndex >= 0 && this.selectedIndex <= this.controls.Count - 1)
		{
			this.controls[index].Disable();
		}
	}

	// Token: 0x06000431 RID: 1073 RVA: 0x000150E0 File Offset: 0x000132E0
	public dfControl AddTab(string Text)
	{
		if (Text == null)
		{
			Text = string.Empty;
		}
		dfButton dfButton = (from i in this.controls
		where i is dfButton
		select i).FirstOrDefault() as dfButton;
		string text = "Tab " + (this.controls.Count + 1).ToString();
		if (string.IsNullOrEmpty(Text))
		{
			Text = text;
		}
		dfButton dfButton2 = base.AddControl<dfButton>();
		dfButton2.name = text;
		dfButton2.Atlas = this.Atlas;
		dfButton2.Text = Text;
		dfButton2.ButtonGroup = this;
		if (dfButton != null)
		{
			dfButton2.Atlas = dfButton.Atlas;
			dfButton2.Font = dfButton.Font;
			dfButton2.AutoSize = dfButton.AutoSize;
			dfButton2.Size = dfButton.Size;
			dfButton2.BackgroundSprite = dfButton.BackgroundSprite;
			dfButton2.DisabledSprite = dfButton.DisabledSprite;
			dfButton2.FocusSprite = dfButton.FocusSprite;
			dfButton2.HoverSprite = dfButton.HoverSprite;
			dfButton2.PressedSprite = dfButton.PressedSprite;
			dfButton2.Shadow = dfButton.Shadow;
			dfButton2.ShadowColor = dfButton.ShadowColor;
			dfButton2.ShadowOffset = dfButton.ShadowOffset;
			dfButton2.TextColor = dfButton.TextColor;
			dfButton2.TextAlignment = dfButton.TextAlignment;
			RectOffset padding = dfButton.Padding;
			dfButton2.Padding = new RectOffset(padding.left, padding.right, padding.top, padding.bottom);
		}
		if (this.pageContainer != null)
		{
			this.pageContainer.AddTabPage();
		}
		this.arrangeTabs();
		this.Invalidate();
		return dfButton2;
	}

	// Token: 0x06000432 RID: 1074 RVA: 0x00015289 File Offset: 0x00013489
	protected internal override void OnGotFocus(dfFocusEventArgs args)
	{
		if (this.controls.Contains(args.GotFocus))
		{
			this.SelectedIndex = args.GotFocus.ZOrder;
		}
		base.OnGotFocus(args);
	}

	// Token: 0x06000433 RID: 1075 RVA: 0x000152B6 File Offset: 0x000134B6
	protected internal override void OnLostFocus(dfFocusEventArgs args)
	{
		base.OnLostFocus(args);
		if (this.controls.Contains(args.LostFocus))
		{
			this.showSelectedTab();
		}
	}

	// Token: 0x06000434 RID: 1076 RVA: 0x000152D8 File Offset: 0x000134D8
	protected internal override void OnClick(dfMouseEventArgs args)
	{
		if (this.controls.Contains(args.Source))
		{
			this.SelectedIndex = args.Source.ZOrder;
		}
		base.OnClick(args);
	}

	// Token: 0x06000435 RID: 1077 RVA: 0x00015305 File Offset: 0x00013505
	private void OnClick(dfControl sender, dfMouseEventArgs args)
	{
		if (!this.controls.Contains(args.Source))
		{
			return;
		}
		this.SelectedIndex = args.Source.ZOrder;
	}

	// Token: 0x06000436 RID: 1078 RVA: 0x0001532C File Offset: 0x0001352C
	protected internal override void OnKeyDown(dfKeyEventArgs args)
	{
		if (args.Used)
		{
			return;
		}
		if (this.allowKeyboardNavigation)
		{
			if (args.KeyCode == KeyCode.LeftArrow || (args.KeyCode == KeyCode.Tab && args.Shift))
			{
				this.SelectedIndex = Mathf.Max(0, this.SelectedIndex - 1);
				args.Use();
				return;
			}
			if (args.KeyCode == KeyCode.RightArrow || args.KeyCode == KeyCode.Tab)
			{
				this.SelectedIndex++;
				args.Use();
				return;
			}
		}
		base.OnKeyDown(args);
	}

	// Token: 0x06000437 RID: 1079 RVA: 0x000153B7 File Offset: 0x000135B7
	protected internal override void OnControlAdded(dfControl child)
	{
		base.OnControlAdded(child);
		this.attachEvents(child);
		this.arrangeTabs();
	}

	// Token: 0x06000438 RID: 1080 RVA: 0x000153CD File Offset: 0x000135CD
	protected internal override void OnControlRemoved(dfControl child)
	{
		base.OnControlRemoved(child);
		this.detachEvents(child);
		this.arrangeTabs();
	}

	// Token: 0x06000439 RID: 1081 RVA: 0x000153E4 File Offset: 0x000135E4
	public override void OnEnable()
	{
		base.OnEnable();
		if (this.size.sqrMagnitude < 1E-45f)
		{
			base.Size = new Vector2(256f, 26f);
		}
		if (Application.isPlaying)
		{
			this.selectTabByIndex(Mathf.Max(this.selectedIndex, 0));
		}
	}

	// Token: 0x0600043A RID: 1082 RVA: 0x00015437 File Offset: 0x00013637
	public override void Update()
	{
		base.Update();
		if (this.isControlInvalidated)
		{
			this.arrangeTabs();
		}
		this.showSelectedTab();
	}

	// Token: 0x0600043B RID: 1083 RVA: 0x00015453 File Offset: 0x00013653
	protected internal virtual void OnSelectedIndexChanged()
	{
		base.SignalHierarchy("OnSelectedIndexChanged", new object[]
		{
			this,
			this.SelectedIndex
		});
		if (this.SelectedIndexChanged != null)
		{
			this.SelectedIndexChanged(this, this.SelectedIndex);
		}
	}

	// Token: 0x0600043C RID: 1084 RVA: 0x00015494 File Offset: 0x00013694
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

	// Token: 0x0600043D RID: 1085 RVA: 0x000155AC File Offset: 0x000137AC
	private void showSelectedTab()
	{
		if (this.selectedIndex >= 0 && this.selectedIndex <= this.controls.Count - 1)
		{
			dfButton dfButton = this.controls[this.selectedIndex] as dfButton;
			if (dfButton != null && !dfButton.ContainsMouse)
			{
				dfButton.State = dfButton.ButtonState.Focus;
			}
		}
	}

	// Token: 0x0600043E RID: 1086 RVA: 0x00015608 File Offset: 0x00013808
	private void selectTabByIndex(int value)
	{
		value = Mathf.Max(Mathf.Min(value, this.controls.Count - 1), -1);
		if (value == this.selectedIndex)
		{
			return;
		}
		this.selectedIndex = value;
		for (int i = 0; i < this.controls.Count; i++)
		{
			dfButton dfButton = this.controls[i] as dfButton;
			if (!(dfButton == null))
			{
				if (i == value)
				{
					dfButton.State = dfButton.ButtonState.Focus;
				}
				else
				{
					dfButton.State = dfButton.ButtonState.Default;
				}
			}
		}
		this.Invalidate();
		this.OnSelectedIndexChanged();
		if (this.pageContainer != null)
		{
			this.pageContainer.SelectedIndex = value;
		}
	}

	// Token: 0x0600043F RID: 1087 RVA: 0x000156AC File Offset: 0x000138AC
	private void arrangeTabs()
	{
		base.SuspendLayout();
		try
		{
			this.layoutPadding = this.layoutPadding.ConstrainPadding();
			float num = (float)this.layoutPadding.left - this.scrollPosition.x;
			float y = (float)this.layoutPadding.top - this.scrollPosition.y;
			float b = 0f;
			float b2 = 0f;
			for (int i = 0; i < base.Controls.Count; i++)
			{
				dfControl dfControl = this.controls[i];
				if (dfControl.IsVisible && dfControl.enabled && dfControl.gameObject.activeSelf)
				{
					Vector2 v = new Vector2(num, y);
					dfControl.RelativePosition = v;
					float num2 = dfControl.Width + (float)this.layoutPadding.horizontal;
					float a = dfControl.Height + (float)this.layoutPadding.vertical;
					b = Mathf.Max(num2, b);
					b2 = Mathf.Max(a, b2);
					num += num2;
				}
			}
		}
		finally
		{
			base.ResumeLayout();
		}
	}

	// Token: 0x06000440 RID: 1088 RVA: 0x000157CC File Offset: 0x000139CC
	private void attachEvents(dfControl control)
	{
		control.IsVisibleChanged += this.control_IsVisibleChanged;
		control.PositionChanged += this.childControlInvalidated;
		control.SizeChanged += this.childControlInvalidated;
		control.ZOrderChanged += this.childControlZOrderChanged;
	}

	// Token: 0x06000441 RID: 1089 RVA: 0x00015821 File Offset: 0x00013A21
	private void detachEvents(dfControl control)
	{
		control.IsVisibleChanged -= this.control_IsVisibleChanged;
		control.PositionChanged -= this.childControlInvalidated;
		control.SizeChanged -= this.childControlInvalidated;
	}

	// Token: 0x06000442 RID: 1090 RVA: 0x00015859 File Offset: 0x00013A59
	private void childControlZOrderChanged(dfControl control, int value)
	{
		this.onChildControlInvalidatedLayout();
	}

	// Token: 0x06000443 RID: 1091 RVA: 0x00015861 File Offset: 0x00013A61
	private void control_IsVisibleChanged(dfControl control, bool value)
	{
		this.onChildControlInvalidatedLayout();
	}

	// Token: 0x06000444 RID: 1092 RVA: 0x00015869 File Offset: 0x00013A69
	private void childControlInvalidated(dfControl control, Vector2 value)
	{
		this.onChildControlInvalidatedLayout();
	}

	// Token: 0x06000445 RID: 1093 RVA: 0x00015871 File Offset: 0x00013A71
	private void onChildControlInvalidatedLayout()
	{
		if (base.IsLayoutSuspended)
		{
			return;
		}
		this.arrangeTabs();
		this.Invalidate();
	}

	// Token: 0x04000168 RID: 360
	[SerializeField]
	protected dfAtlas atlas;

	// Token: 0x04000169 RID: 361
	[SerializeField]
	protected string backgroundSprite;

	// Token: 0x0400016A RID: 362
	[SerializeField]
	protected RectOffset layoutPadding = new RectOffset();

	// Token: 0x0400016B RID: 363
	[SerializeField]
	protected Vector2 scrollPosition = Vector2.zero;

	// Token: 0x0400016C RID: 364
	[SerializeField]
	protected int selectedIndex;

	// Token: 0x0400016D RID: 365
	[SerializeField]
	protected dfTabContainer pageContainer;

	// Token: 0x0400016E RID: 366
	[SerializeField]
	protected bool allowKeyboardNavigation = true;
}

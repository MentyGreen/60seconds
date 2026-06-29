using System;
using UnityEngine;

// Token: 0x02000007 RID: 7
[ExecuteInEditMode]
[dfCategory("Basic Controls")]
[dfTooltip("Provides a basic Button implementation that allows the developer to specify individual sprite images to be used to represent common button states.")]
[dfHelp("http://www.daikonforge.com/docs/df-gui/classdf_button.html")]
[AddComponentMenu("Daikon Forge/User Interface/Button")]
[Serializable]
public class dfButton : dfInteractiveBase, IDFMultiRender, IRendersText
{
	// Token: 0x14000001 RID: 1
	// (add) Token: 0x06000038 RID: 56 RVA: 0x00002AD0 File Offset: 0x00000CD0
	// (remove) Token: 0x06000039 RID: 57 RVA: 0x00002B08 File Offset: 0x00000D08
	public event PropertyChangedEventHandler<dfButton.ButtonState> ButtonStateChanged;

	// Token: 0x1700000E RID: 14
	// (get) Token: 0x0600003A RID: 58 RVA: 0x00002B3D File Offset: 0x00000D3D
	// (set) Token: 0x0600003B RID: 59 RVA: 0x00002B45 File Offset: 0x00000D45
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

	// Token: 0x1700000F RID: 15
	// (get) Token: 0x0600003C RID: 60 RVA: 0x00002B4E File Offset: 0x00000D4E
	// (set) Token: 0x0600003D RID: 61 RVA: 0x00002B56 File Offset: 0x00000D56
	public dfButton.ButtonState State
	{
		get
		{
			return this.state;
		}
		set
		{
			if (value != this.state)
			{
				this.OnButtonStateChanged(value);
				this.Invalidate();
			}
		}
	}

	// Token: 0x17000010 RID: 16
	// (get) Token: 0x0600003E RID: 62 RVA: 0x00002B6E File Offset: 0x00000D6E
	// (set) Token: 0x0600003F RID: 63 RVA: 0x00002B76 File Offset: 0x00000D76
	public string PressedSprite
	{
		get
		{
			return this.pressedSprite;
		}
		set
		{
			if (value != this.pressedSprite)
			{
				this.pressedSprite = value;
				this.Invalidate();
			}
		}
	}

	// Token: 0x17000011 RID: 17
	// (get) Token: 0x06000040 RID: 64 RVA: 0x00002B93 File Offset: 0x00000D93
	// (set) Token: 0x06000041 RID: 65 RVA: 0x00002B9B File Offset: 0x00000D9B
	public dfControl ButtonGroup
	{
		get
		{
			return this.group;
		}
		set
		{
			if (value != this.group)
			{
				this.group = value;
				this.Invalidate();
			}
		}
	}

	// Token: 0x17000012 RID: 18
	// (get) Token: 0x06000042 RID: 66 RVA: 0x00002BB8 File Offset: 0x00000DB8
	// (set) Token: 0x06000043 RID: 67 RVA: 0x00002BC0 File Offset: 0x00000DC0
	public bool AutoSize
	{
		get
		{
			return this.autoSize;
		}
		set
		{
			if (value != this.autoSize)
			{
				this.autoSize = value;
				if (value)
				{
					this.textAlign = TextAlignment.Left;
				}
				this.Invalidate();
			}
		}
	}

	// Token: 0x17000013 RID: 19
	// (get) Token: 0x06000044 RID: 68 RVA: 0x00002BE2 File Offset: 0x00000DE2
	// (set) Token: 0x06000045 RID: 69 RVA: 0x00002BF4 File Offset: 0x00000DF4
	public TextAlignment TextAlignment
	{
		get
		{
			if (this.autoSize)
			{
				return TextAlignment.Left;
			}
			return this.textAlign;
		}
		set
		{
			if (value != this.textAlign)
			{
				this.textAlign = value;
				this.Invalidate();
			}
		}
	}

	// Token: 0x17000014 RID: 20
	// (get) Token: 0x06000046 RID: 70 RVA: 0x00002C0C File Offset: 0x00000E0C
	// (set) Token: 0x06000047 RID: 71 RVA: 0x00002C14 File Offset: 0x00000E14
	public dfVerticalAlignment VerticalAlignment
	{
		get
		{
			return this.vertAlign;
		}
		set
		{
			if (value != this.vertAlign)
			{
				this.vertAlign = value;
				this.Invalidate();
			}
		}
	}

	// Token: 0x17000015 RID: 21
	// (get) Token: 0x06000048 RID: 72 RVA: 0x00002C2C File Offset: 0x00000E2C
	// (set) Token: 0x06000049 RID: 73 RVA: 0x00002C47 File Offset: 0x00000E47
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

	// Token: 0x17000016 RID: 22
	// (get) Token: 0x0600004A RID: 74 RVA: 0x00002C6C File Offset: 0x00000E6C
	// (set) Token: 0x0600004B RID: 75 RVA: 0x00002CA9 File Offset: 0x00000EA9
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
			}
			this.Invalidate();
		}
	}

	// Token: 0x17000017 RID: 23
	// (get) Token: 0x0600004C RID: 76 RVA: 0x00002CD2 File Offset: 0x00000ED2
	// (set) Token: 0x0600004D RID: 77 RVA: 0x00002CDA File Offset: 0x00000EDA
	public string Text
	{
		get
		{
			return this.text;
		}
		set
		{
			if (value != this.text)
			{
				dfFontManager.Invalidate(this.Font);
				this.localizationKey = value;
				this.text = base.getLocalizedValue(value);
				this.Invalidate();
			}
		}
	}

	// Token: 0x17000018 RID: 24
	// (get) Token: 0x0600004E RID: 78 RVA: 0x00002D0F File Offset: 0x00000F0F
	// (set) Token: 0x0600004F RID: 79 RVA: 0x00002D17 File Offset: 0x00000F17
	public Color32 TextColor
	{
		get
		{
			return this.textColor;
		}
		set
		{
			this.textColor = value;
			this.Invalidate();
		}
	}

	// Token: 0x17000019 RID: 25
	// (get) Token: 0x06000050 RID: 80 RVA: 0x00002D26 File Offset: 0x00000F26
	// (set) Token: 0x06000051 RID: 81 RVA: 0x00002D2E File Offset: 0x00000F2E
	public Color32 HoverTextColor
	{
		get
		{
			return this.hoverText;
		}
		set
		{
			this.hoverText = value;
			this.Invalidate();
		}
	}

	// Token: 0x1700001A RID: 26
	// (get) Token: 0x06000052 RID: 82 RVA: 0x00002D3D File Offset: 0x00000F3D
	// (set) Token: 0x06000053 RID: 83 RVA: 0x00002D45 File Offset: 0x00000F45
	public Color32 NormalBackgroundColor
	{
		get
		{
			return this.normalColor;
		}
		set
		{
			this.normalColor = value;
			this.Invalidate();
		}
	}

	// Token: 0x1700001B RID: 27
	// (get) Token: 0x06000054 RID: 84 RVA: 0x00002D54 File Offset: 0x00000F54
	// (set) Token: 0x06000055 RID: 85 RVA: 0x00002D5C File Offset: 0x00000F5C
	public Color32 HoverBackgroundColor
	{
		get
		{
			return this.hoverColor;
		}
		set
		{
			this.hoverColor = value;
			this.Invalidate();
		}
	}

	// Token: 0x1700001C RID: 28
	// (get) Token: 0x06000056 RID: 86 RVA: 0x00002D6B File Offset: 0x00000F6B
	// (set) Token: 0x06000057 RID: 87 RVA: 0x00002D73 File Offset: 0x00000F73
	public Color32 PressedTextColor
	{
		get
		{
			return this.pressedText;
		}
		set
		{
			this.pressedText = value;
			this.Invalidate();
		}
	}

	// Token: 0x1700001D RID: 29
	// (get) Token: 0x06000058 RID: 88 RVA: 0x00002D82 File Offset: 0x00000F82
	// (set) Token: 0x06000059 RID: 89 RVA: 0x00002D8A File Offset: 0x00000F8A
	public Color32 PressedBackgroundColor
	{
		get
		{
			return this.pressedColor;
		}
		set
		{
			this.pressedColor = value;
			this.Invalidate();
		}
	}

	// Token: 0x1700001E RID: 30
	// (get) Token: 0x0600005A RID: 90 RVA: 0x00002D99 File Offset: 0x00000F99
	// (set) Token: 0x0600005B RID: 91 RVA: 0x00002DA1 File Offset: 0x00000FA1
	public Color32 FocusTextColor
	{
		get
		{
			return this.focusText;
		}
		set
		{
			this.focusText = value;
			this.Invalidate();
		}
	}

	// Token: 0x1700001F RID: 31
	// (get) Token: 0x0600005C RID: 92 RVA: 0x00002DB0 File Offset: 0x00000FB0
	// (set) Token: 0x0600005D RID: 93 RVA: 0x00002DB8 File Offset: 0x00000FB8
	public Color32 FocusBackgroundColor
	{
		get
		{
			return this.focusColor;
		}
		set
		{
			this.focusColor = value;
			this.Invalidate();
		}
	}

	// Token: 0x17000020 RID: 32
	// (get) Token: 0x0600005E RID: 94 RVA: 0x00002DC7 File Offset: 0x00000FC7
	// (set) Token: 0x0600005F RID: 95 RVA: 0x00002DCF File Offset: 0x00000FCF
	public Color32 DisabledTextColor
	{
		get
		{
			return this.disabledText;
		}
		set
		{
			this.disabledText = value;
			this.Invalidate();
		}
	}

	// Token: 0x17000021 RID: 33
	// (get) Token: 0x06000060 RID: 96 RVA: 0x00002DDE File Offset: 0x00000FDE
	// (set) Token: 0x06000061 RID: 97 RVA: 0x00002DE6 File Offset: 0x00000FE6
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
				dfFontManager.Invalidate(this.Font);
				this.textScale = value;
				this.Invalidate();
			}
		}
	}

	// Token: 0x17000022 RID: 34
	// (get) Token: 0x06000062 RID: 98 RVA: 0x00002E1B File Offset: 0x0000101B
	// (set) Token: 0x06000063 RID: 99 RVA: 0x00002E23 File Offset: 0x00001023
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

	// Token: 0x17000023 RID: 35
	// (get) Token: 0x06000064 RID: 100 RVA: 0x00002E32 File Offset: 0x00001032
	// (set) Token: 0x06000065 RID: 101 RVA: 0x00002E3A File Offset: 0x0000103A
	public bool WordWrap
	{
		get
		{
			return this.wordWrap;
		}
		set
		{
			if (value != this.wordWrap)
			{
				this.wordWrap = value;
				this.Invalidate();
			}
		}
	}

	// Token: 0x17000024 RID: 36
	// (get) Token: 0x06000066 RID: 102 RVA: 0x00002E52 File Offset: 0x00001052
	// (set) Token: 0x06000067 RID: 103 RVA: 0x00002E5A File Offset: 0x0000105A
	public bool Shadow
	{
		get
		{
			return this.textShadow;
		}
		set
		{
			if (value != this.textShadow)
			{
				this.textShadow = value;
				this.Invalidate();
			}
		}
	}

	// Token: 0x17000025 RID: 37
	// (get) Token: 0x06000068 RID: 104 RVA: 0x00002E72 File Offset: 0x00001072
	// (set) Token: 0x06000069 RID: 105 RVA: 0x00002E7A File Offset: 0x0000107A
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

	// Token: 0x17000026 RID: 38
	// (get) Token: 0x0600006A RID: 106 RVA: 0x00002EA3 File Offset: 0x000010A3
	// (set) Token: 0x0600006B RID: 107 RVA: 0x00002EAB File Offset: 0x000010AB
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

	// Token: 0x0600006C RID: 108 RVA: 0x00002EC8 File Offset: 0x000010C8
	protected internal override void OnLocalize()
	{
		base.OnLocalize();
		this.Text = base.getLocalizedValue(this.localizationKey ?? this.text);
		if (base.CustomWordWrapAllowed && Settings.Data != null && Settings.Data.DoesCurrentLanguageWordwrap())
		{
			WordWrapper instance = WordWrapper.GetInstance();
			int lineWidth = (int)(base.Width / (float)this.font.FontSize);
			this.Text = instance.WrapText(this.text, lineWidth, WordWrapper.EWrapAlgorithm.Dynamic);
		}
	}

	// Token: 0x0600006D RID: 109 RVA: 0x00002F47 File Offset: 0x00001147
	[HideInInspector]
	public override void Invalidate()
	{
		base.Invalidate();
		if (this.AutoSize)
		{
			this.autoSizeToText();
		}
	}

	// Token: 0x0600006E RID: 110 RVA: 0x00002F5D File Offset: 0x0000115D
	public override void Start()
	{
		base.Start();
		this.localizationKey = this.Text;
	}

	// Token: 0x0600006F RID: 111 RVA: 0x00002F74 File Offset: 0x00001174
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

	// Token: 0x06000070 RID: 112 RVA: 0x00002FC5 File Offset: 0x000011C5
	public override void OnDisable()
	{
		base.OnDisable();
		this.unbindTextureRebuildCallback();
	}

	// Token: 0x06000071 RID: 113 RVA: 0x00002FD3 File Offset: 0x000011D3
	public override void Awake()
	{
		base.Awake();
		this.startSize = base.Size;
	}

	// Token: 0x06000072 RID: 114 RVA: 0x00002FE7 File Offset: 0x000011E7
	protected internal override void OnEnterFocus(dfFocusEventArgs args)
	{
		if (this.State != dfButton.ButtonState.Pressed)
		{
			this.State = dfButton.ButtonState.Focus;
		}
		base.OnEnterFocus(args);
	}

	// Token: 0x06000073 RID: 115 RVA: 0x00003000 File Offset: 0x00001200
	protected internal override void OnLeaveFocus(dfFocusEventArgs args)
	{
		this.State = dfButton.ButtonState.Default;
		base.OnLeaveFocus(args);
	}

	// Token: 0x06000074 RID: 116 RVA: 0x00003010 File Offset: 0x00001210
	protected internal override void OnKeyPress(dfKeyEventArgs args)
	{
		if (this.ClickWhenSpacePressed && this.IsInteractive && args.KeyCode == KeyCode.Space)
		{
			this.OnClick(new dfMouseEventArgs(this, dfMouseButtons.Left, 1, default(Ray), Vector2.zero, 0f));
			return;
		}
		base.OnKeyPress(args);
	}

	// Token: 0x06000075 RID: 117 RVA: 0x00003060 File Offset: 0x00001260
	protected internal override void OnClick(dfMouseEventArgs args)
	{
		if (this.group != null)
		{
			foreach (dfButton dfButton in base.transform.parent.GetComponentsInChildren<dfButton>())
			{
				if (dfButton != this && dfButton.ButtonGroup == this.ButtonGroup && dfButton != this)
				{
					dfButton.State = dfButton.ButtonState.Default;
				}
			}
			if (!base.transform.IsChildOf(this.group.transform))
			{
				base.Signal(this.group.gameObject, "OnClick", args);
			}
		}
		base.OnClick(args);
	}

	// Token: 0x06000076 RID: 118 RVA: 0x00003101 File Offset: 0x00001301
	protected internal override void OnMouseDown(dfMouseEventArgs args)
	{
		if (!(this.parent is dfTabstrip) || this.State != dfButton.ButtonState.Focus)
		{
			this.State = dfButton.ButtonState.Pressed;
		}
		base.OnMouseDown(args);
	}

	// Token: 0x06000077 RID: 119 RVA: 0x00003128 File Offset: 0x00001328
	protected internal override void OnMouseUp(dfMouseEventArgs args)
	{
		if (!base.IsEnabled)
		{
			this.State = dfButton.ButtonState.Disabled;
			base.OnMouseUp(args);
			return;
		}
		if (this.isMouseHovering)
		{
			if (this.parent is dfTabstrip && this.ContainsFocus)
			{
				this.State = dfButton.ButtonState.Focus;
			}
			else
			{
				this.State = dfButton.ButtonState.Hover;
			}
		}
		else if (this.HasFocus)
		{
			this.State = dfButton.ButtonState.Focus;
		}
		else
		{
			this.State = dfButton.ButtonState.Default;
		}
		base.OnMouseUp(args);
	}

	// Token: 0x06000078 RID: 120 RVA: 0x0000319A File Offset: 0x0000139A
	protected internal override void OnMouseEnter(dfMouseEventArgs args)
	{
		if (!(this.parent is dfTabstrip) || this.State != dfButton.ButtonState.Focus)
		{
			this.State = dfButton.ButtonState.Hover;
		}
		base.OnMouseEnter(args);
	}

	// Token: 0x06000079 RID: 121 RVA: 0x000031C0 File Offset: 0x000013C0
	protected internal override void OnMouseLeave(dfMouseEventArgs args)
	{
		if (this.ContainsFocus)
		{
			this.State = dfButton.ButtonState.Focus;
		}
		else
		{
			this.State = dfButton.ButtonState.Default;
		}
		base.OnMouseLeave(args);
	}

	// Token: 0x0600007A RID: 122 RVA: 0x000031E1 File Offset: 0x000013E1
	protected internal override void OnIsEnabledChanged()
	{
		if (!base.IsEnabled)
		{
			this.State = dfButton.ButtonState.Disabled;
		}
		else
		{
			this.State = dfButton.ButtonState.Default;
		}
		base.OnIsEnabledChanged();
	}

	// Token: 0x0600007B RID: 123 RVA: 0x00003204 File Offset: 0x00001404
	protected virtual void OnButtonStateChanged(dfButton.ButtonState value)
	{
		if (value != dfButton.ButtonState.Disabled && !base.IsEnabled)
		{
			value = dfButton.ButtonState.Disabled;
		}
		this.state = value;
		base.Signal("OnButtonStateChanged", this, value);
		if (this.ButtonStateChanged != null)
		{
			this.ButtonStateChanged(this, value);
		}
		this.Invalidate();
	}

	// Token: 0x0600007C RID: 124 RVA: 0x00003258 File Offset: 0x00001458
	protected override Color32 getActiveColor()
	{
		switch (this.State)
		{
		case dfButton.ButtonState.Focus:
			return this.FocusBackgroundColor;
		case dfButton.ButtonState.Hover:
			return this.HoverBackgroundColor;
		case dfButton.ButtonState.Pressed:
			return this.PressedBackgroundColor;
		case dfButton.ButtonState.Disabled:
			return base.DisabledColor;
		default:
			return this.NormalBackgroundColor;
		}
	}

	// Token: 0x0600007D RID: 125 RVA: 0x000032A8 File Offset: 0x000014A8
	private void autoSizeToText()
	{
		if (this.Font == null || !this.Font.IsValid || string.IsNullOrEmpty(this.Text))
		{
			return;
		}
		using (dfFontRendererBase dfFontRendererBase = this.obtainTextRenderer())
		{
			Vector2 vector = dfFontRendererBase.MeasureString(this.Text);
			Vector2 vector2 = new Vector2(vector.x + (float)this.padding.horizontal, vector.y + (float)this.padding.vertical);
			if (this.size != vector2)
			{
				base.SuspendLayout();
				base.Size = vector2;
				base.ResumeLayout();
			}
		}
	}

	// Token: 0x0600007E RID: 126 RVA: 0x0000335C File Offset: 0x0000155C
	private dfRenderData renderText()
	{
		if (this.Font == null || !this.Font.IsValid || string.IsNullOrEmpty(this.Text))
		{
			return null;
		}
		dfRenderData renderData = this.renderData;
		if (this.font is dfDynamicFont)
		{
			dfDynamicFont dfDynamicFont = (dfDynamicFont)this.font;
			renderData = this.textRenderData;
			renderData.Clear();
			renderData.Material = dfDynamicFont.Material;
		}
		using (dfFontRendererBase dfFontRendererBase = this.obtainTextRenderer())
		{
			dfFontRendererBase.Render(this.text, renderData);
		}
		return renderData;
	}

	// Token: 0x0600007F RID: 127 RVA: 0x00003400 File Offset: 0x00001600
	private dfFontRendererBase obtainTextRenderer()
	{
		Vector2 vector = base.Size - new Vector2((float)this.padding.horizontal, (float)this.padding.vertical);
		Vector2 maxSize = this.autoSize ? (Vector2.one * 2.1474836E+09f) : vector;
		float num = base.PixelsToUnits();
		Vector3 vector2 = (this.pivot.TransformToUpperLeft(base.Size) + new Vector3((float)this.padding.left, (float)(-(float)this.padding.top))) * num;
		float num2 = this.TextScale * this.getTextScaleMultiplier();
		Color32 defaultColor = base.ApplyOpacity(this.getTextColorForState());
		dfFontRendererBase dfFontRendererBase = this.Font.ObtainRenderer();
		dfFontRendererBase.WordWrap = this.WordWrap;
		dfFontRendererBase.MultiLine = this.WordWrap;
		dfFontRendererBase.MaxSize = maxSize;
		dfFontRendererBase.PixelRatio = num;
		dfFontRendererBase.TextScale = num2;
		dfFontRendererBase.CharacterSpacing = 0;
		dfFontRendererBase.VectorOffset = vector2.Quantize(num);
		dfFontRendererBase.TabSize = 0;
		dfFontRendererBase.TextAlign = (this.autoSize ? TextAlignment.Left : this.TextAlignment);
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
		if (this.vertAlign != dfVerticalAlignment.Top)
		{
			dfFontRendererBase.VectorOffset = this.getVertAlignOffset(dfFontRendererBase);
		}
		return dfFontRendererBase;
	}

	// Token: 0x06000080 RID: 128 RVA: 0x000035B4 File Offset: 0x000017B4
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
		if (this.autoSize)
		{
			return 1f;
		}
		return base.Size.y / this.startSize.y;
	}

	// Token: 0x06000081 RID: 129 RVA: 0x00003618 File Offset: 0x00001818
	private Color32 getTextColorForState()
	{
		if (!base.IsEnabled)
		{
			return this.DisabledTextColor;
		}
		switch (this.state)
		{
		case dfButton.ButtonState.Default:
			return this.TextColor;
		case dfButton.ButtonState.Focus:
			return this.FocusTextColor;
		case dfButton.ButtonState.Hover:
			return this.HoverTextColor;
		case dfButton.ButtonState.Pressed:
			return this.PressedTextColor;
		case dfButton.ButtonState.Disabled:
			return this.DisabledTextColor;
		default:
			return UnityEngine.Color.white;
		}
	}

	// Token: 0x06000082 RID: 130 RVA: 0x00003684 File Offset: 0x00001884
	private Vector3 getVertAlignOffset(dfFontRendererBase textRenderer)
	{
		float num = base.PixelsToUnits();
		Vector2 vector = textRenderer.MeasureString(this.text) * num;
		Vector3 vectorOffset = textRenderer.VectorOffset;
		float num2 = (base.Height - (float)this.padding.vertical) * num;
		if (vector.y >= num2)
		{
			return vectorOffset;
		}
		dfVerticalAlignment dfVerticalAlignment = this.vertAlign;
		if (dfVerticalAlignment != dfVerticalAlignment.Middle)
		{
			if (dfVerticalAlignment == dfVerticalAlignment.Bottom)
			{
				vectorOffset.y -= num2 - vector.y;
			}
		}
		else
		{
			vectorOffset.y -= (num2 - vector.y) * 0.5f;
		}
		return vectorOffset;
	}

	// Token: 0x06000083 RID: 131 RVA: 0x00003718 File Offset: 0x00001918
	protected internal override dfAtlas.ItemInfo getBackgroundSprite()
	{
		if (base.Atlas == null)
		{
			return null;
		}
		dfAtlas.ItemInfo itemInfo = null;
		switch (this.state)
		{
		case dfButton.ButtonState.Default:
			itemInfo = this.atlas[this.backgroundSprite];
			break;
		case dfButton.ButtonState.Focus:
			itemInfo = this.atlas[this.focusSprite];
			break;
		case dfButton.ButtonState.Hover:
			itemInfo = this.atlas[this.hoverSprite];
			break;
		case dfButton.ButtonState.Pressed:
			itemInfo = this.atlas[this.pressedSprite];
			break;
		case dfButton.ButtonState.Disabled:
			itemInfo = this.atlas[this.disabledSprite];
			break;
		}
		if (itemInfo == null)
		{
			itemInfo = this.atlas[this.backgroundSprite];
		}
		return itemInfo;
	}

	// Token: 0x06000084 RID: 132 RVA: 0x000037D8 File Offset: 0x000019D8
	public dfList<dfRenderData> RenderMultiple()
	{
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
		this.isControlInvalidated = false;
		this.buffers.Clear();
		this.renderData.Clear();
		if (base.Atlas != null)
		{
			this.renderData.Material = base.Atlas.Material;
			this.renderData.Transform = localToWorldMatrix;
			this.renderBackground();
			this.buffers.Add(this.renderData);
		}
		dfRenderData dfRenderData = this.renderText();
		if (dfRenderData != null && dfRenderData != this.renderData)
		{
			dfRenderData.Transform = localToWorldMatrix;
			this.buffers.Add(dfRenderData);
		}
		base.updateCollider();
		return this.buffers;
	}

	// Token: 0x06000085 RID: 133 RVA: 0x000038E4 File Offset: 0x00001AE4
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

	// Token: 0x06000086 RID: 134 RVA: 0x00003950 File Offset: 0x00001B50
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

	// Token: 0x06000087 RID: 135 RVA: 0x000039BC File Offset: 0x00001BBC
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
		if (string.IsNullOrEmpty(this.text))
		{
			return;
		}
		float num = this.TextScale * this.getTextScaleMultiplier();
		int fontSize = Mathf.CeilToInt((float)this.font.FontSize * num);
		dfDynamicFont.AddCharacterRequest(this.text, fontSize, FontStyle.Normal);
	}

	// Token: 0x06000088 RID: 136 RVA: 0x00003A2B File Offset: 0x00001C2B
	private void onFontTextureRebuilt()
	{
		this.requestCharacterInfo();
		this.Invalidate();
	}

	// Token: 0x06000089 RID: 137 RVA: 0x00003A39 File Offset: 0x00001C39
	public void UpdateFontInfo()
	{
		this.requestCharacterInfo();
	}

	// Token: 0x04000018 RID: 24
	[SerializeField]
	protected dfFontBase font;

	// Token: 0x04000019 RID: 25
	[SerializeField]
	protected string pressedSprite;

	// Token: 0x0400001A RID: 26
	[SerializeField]
	protected dfButton.ButtonState state;

	// Token: 0x0400001B RID: 27
	[SerializeField]
	protected dfControl group;

	// Token: 0x0400001C RID: 28
	[SerializeField]
	protected string text = "";

	// Token: 0x0400001D RID: 29
	[SerializeField]
	protected TextAlignment textAlign = TextAlignment.Center;

	// Token: 0x0400001E RID: 30
	[SerializeField]
	protected dfVerticalAlignment vertAlign = dfVerticalAlignment.Middle;

	// Token: 0x0400001F RID: 31
	[SerializeField]
	protected Color32 normalColor = UnityEngine.Color.white;

	// Token: 0x04000020 RID: 32
	[SerializeField]
	protected Color32 textColor = UnityEngine.Color.white;

	// Token: 0x04000021 RID: 33
	[SerializeField]
	protected Color32 hoverText = UnityEngine.Color.white;

	// Token: 0x04000022 RID: 34
	[SerializeField]
	protected Color32 pressedText = UnityEngine.Color.white;

	// Token: 0x04000023 RID: 35
	[SerializeField]
	protected Color32 focusText = UnityEngine.Color.white;

	// Token: 0x04000024 RID: 36
	[SerializeField]
	protected Color32 disabledText = UnityEngine.Color.white;

	// Token: 0x04000025 RID: 37
	[SerializeField]
	protected Color32 hoverColor = UnityEngine.Color.white;

	// Token: 0x04000026 RID: 38
	[SerializeField]
	protected Color32 pressedColor = UnityEngine.Color.white;

	// Token: 0x04000027 RID: 39
	[SerializeField]
	protected Color32 focusColor = UnityEngine.Color.white;

	// Token: 0x04000028 RID: 40
	[SerializeField]
	protected float textScale = 1f;

	// Token: 0x04000029 RID: 41
	[SerializeField]
	protected dfTextScaleMode textScaleMode;

	// Token: 0x0400002A RID: 42
	[SerializeField]
	protected bool wordWrap;

	// Token: 0x0400002B RID: 43
	[SerializeField]
	protected RectOffset padding = new RectOffset();

	// Token: 0x0400002C RID: 44
	[SerializeField]
	protected bool textShadow;

	// Token: 0x0400002D RID: 45
	[SerializeField]
	protected Color32 shadowColor = UnityEngine.Color.black;

	// Token: 0x0400002E RID: 46
	[SerializeField]
	protected Vector2 shadowOffset = new Vector2(1f, -1f);

	// Token: 0x0400002F RID: 47
	[SerializeField]
	protected bool autoSize;

	// Token: 0x04000030 RID: 48
	[SerializeField]
	protected bool clickWhenSpacePressed = true;

	// Token: 0x04000031 RID: 49
	private Vector2 startSize = Vector2.zero;

	// Token: 0x04000032 RID: 50
	private bool isFontCallbackAssigned;

	// Token: 0x04000033 RID: 51
	private dfRenderData textRenderData;

	// Token: 0x04000034 RID: 52
	private dfList<dfRenderData> buffers = dfList<dfRenderData>.Obtain();

	// Token: 0x02000350 RID: 848
	public enum ButtonState
	{
		// Token: 0x040015D3 RID: 5587
		Default,
		// Token: 0x040015D4 RID: 5588
		Focus,
		// Token: 0x040015D5 RID: 5589
		Hover,
		// Token: 0x040015D6 RID: 5590
		Pressed,
		// Token: 0x040015D7 RID: 5591
		Disabled
	}
}

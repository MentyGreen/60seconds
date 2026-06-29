using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200000D RID: 13
[ExecuteInEditMode]
[dfCategory("Basic Controls")]
[dfTooltip("Displays text information, optionally allowing the use of markup to specify colors and embedded sprites")]
[dfHelp("http://www.daikonforge.com/docs/df-gui/classdf_label.html")]
[AddComponentMenu("Daikon Forge/User Interface/Label")]
[Serializable]
public class dfLabel : dfControl, IDFMultiRender, IRendersText
{
	// Token: 0x1400002D RID: 45
	// (add) Token: 0x06000225 RID: 549 RVA: 0x0000A4BC File Offset: 0x000086BC
	// (remove) Token: 0x06000226 RID: 550 RVA: 0x0000A4F4 File Offset: 0x000086F4
	public event PropertyChangedEventHandler<string> TextChanged;

	// Token: 0x17000072 RID: 114
	// (get) Token: 0x06000227 RID: 551 RVA: 0x0000A52C File Offset: 0x0000872C
	// (set) Token: 0x06000228 RID: 552 RVA: 0x0000A56D File Offset: 0x0000876D
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

	// Token: 0x17000073 RID: 115
	// (get) Token: 0x06000229 RID: 553 RVA: 0x0000A58C File Offset: 0x0000878C
	// (set) Token: 0x0600022A RID: 554 RVA: 0x0000A5C9 File Offset: 0x000087C9
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

	// Token: 0x17000074 RID: 116
	// (get) Token: 0x0600022B RID: 555 RVA: 0x0000A5F2 File Offset: 0x000087F2
	// (set) Token: 0x0600022C RID: 556 RVA: 0x0000A5FA File Offset: 0x000087FA
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

	// Token: 0x17000075 RID: 117
	// (get) Token: 0x0600022D RID: 557 RVA: 0x0000A617 File Offset: 0x00008817
	// (set) Token: 0x0600022E RID: 558 RVA: 0x0000A61F File Offset: 0x0000881F
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

	// Token: 0x17000076 RID: 118
	// (get) Token: 0x0600022F RID: 559 RVA: 0x0000A646 File Offset: 0x00008846
	// (set) Token: 0x06000230 RID: 560 RVA: 0x0000A64E File Offset: 0x0000884E
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

	// Token: 0x17000077 RID: 119
	// (get) Token: 0x06000231 RID: 561 RVA: 0x0000A683 File Offset: 0x00008883
	// (set) Token: 0x06000232 RID: 562 RVA: 0x0000A68B File Offset: 0x0000888B
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

	// Token: 0x17000078 RID: 120
	// (get) Token: 0x06000233 RID: 563 RVA: 0x0000A69A File Offset: 0x0000889A
	// (set) Token: 0x06000234 RID: 564 RVA: 0x0000A6A2 File Offset: 0x000088A2
	public int CharacterSpacing
	{
		get
		{
			return this.charSpacing;
		}
		set
		{
			value = Mathf.Max(0, value);
			if (value != this.charSpacing)
			{
				this.charSpacing = value;
				this.Invalidate();
			}
		}
	}

	// Token: 0x17000079 RID: 121
	// (get) Token: 0x06000235 RID: 565 RVA: 0x0000A6C3 File Offset: 0x000088C3
	// (set) Token: 0x06000236 RID: 566 RVA: 0x0000A6CB File Offset: 0x000088CB
	public bool ColorizeSymbols
	{
		get
		{
			return this.colorizeSymbols;
		}
		set
		{
			if (value != this.colorizeSymbols)
			{
				this.colorizeSymbols = value;
				this.Invalidate();
			}
		}
	}

	// Token: 0x1700007A RID: 122
	// (get) Token: 0x06000237 RID: 567 RVA: 0x0000A6E3 File Offset: 0x000088E3
	// (set) Token: 0x06000238 RID: 568 RVA: 0x0000A6EB File Offset: 0x000088EB
	public bool ProcessMarkup
	{
		get
		{
			return this.processMarkup;
		}
		set
		{
			if (value != this.processMarkup)
			{
				this.processMarkup = value;
				this.Invalidate();
			}
		}
	}

	// Token: 0x1700007B RID: 123
	// (get) Token: 0x06000239 RID: 569 RVA: 0x0000A703 File Offset: 0x00008903
	// (set) Token: 0x0600023A RID: 570 RVA: 0x0000A70B File Offset: 0x0000890B
	public bool ShowGradient
	{
		get
		{
			return this.enableGradient;
		}
		set
		{
			if (value != this.enableGradient)
			{
				this.enableGradient = value;
				this.Invalidate();
			}
		}
	}

	// Token: 0x1700007C RID: 124
	// (get) Token: 0x0600023B RID: 571 RVA: 0x0000A723 File Offset: 0x00008923
	// (set) Token: 0x0600023C RID: 572 RVA: 0x0000A72B File Offset: 0x0000892B
	public Color32 BottomColor
	{
		get
		{
			return this.bottomColor;
		}
		set
		{
			if (!this.bottomColor.Equals(value))
			{
				this.bottomColor = value;
				this.OnColorChanged();
			}
		}
	}

	// Token: 0x1700007D RID: 125
	// (get) Token: 0x0600023D RID: 573 RVA: 0x0000A753 File Offset: 0x00008953
	// (set) Token: 0x0600023E RID: 574 RVA: 0x0000A75C File Offset: 0x0000895C
	public string Text
	{
		get
		{
			return this.text;
		}
		set
		{
			if (value == null)
			{
				value = string.Empty;
			}
			else
			{
				value = value.Replace("\\t", "\t").Replace("\\n", "\n");
			}
			if (!string.Equals(value, this.text))
			{
				dfFontManager.Invalidate(this.Font);
				this.localizationKey = value;
				this.text = base.getLocalizedValue(value);
				this.OnTextChanged();
			}
		}
	}

	// Token: 0x1700007E RID: 126
	// (get) Token: 0x0600023F RID: 575 RVA: 0x0000A7C9 File Offset: 0x000089C9
	// (set) Token: 0x06000240 RID: 576 RVA: 0x0000A7D1 File Offset: 0x000089D1
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
				if (value)
				{
					this.autoHeight = false;
				}
				this.autoSize = value;
				this.Invalidate();
			}
		}
	}

	// Token: 0x1700007F RID: 127
	// (get) Token: 0x06000241 RID: 577 RVA: 0x0000A7F3 File Offset: 0x000089F3
	// (set) Token: 0x06000242 RID: 578 RVA: 0x0000A808 File Offset: 0x00008A08
	public bool AutoHeight
	{
		get
		{
			return this.autoHeight && !this.autoSize;
		}
		set
		{
			if (value != this.autoHeight)
			{
				if (value)
				{
					this.autoSize = false;
				}
				this.autoHeight = value;
				this.Invalidate();
			}
		}
	}

	// Token: 0x17000080 RID: 128
	// (get) Token: 0x06000243 RID: 579 RVA: 0x0000A82A File Offset: 0x00008A2A
	// (set) Token: 0x06000244 RID: 580 RVA: 0x0000A832 File Offset: 0x00008A32
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

	// Token: 0x17000081 RID: 129
	// (get) Token: 0x06000245 RID: 581 RVA: 0x0000A84A File Offset: 0x00008A4A
	// (set) Token: 0x06000246 RID: 582 RVA: 0x0000A852 File Offset: 0x00008A52
	public TextAlignment TextAlignment
	{
		get
		{
			return this.align;
		}
		set
		{
			if (value != this.align)
			{
				this.align = value;
				this.Invalidate();
			}
		}
	}

	// Token: 0x17000082 RID: 130
	// (get) Token: 0x06000247 RID: 583 RVA: 0x0000A86A File Offset: 0x00008A6A
	// (set) Token: 0x06000248 RID: 584 RVA: 0x0000A872 File Offset: 0x00008A72
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

	// Token: 0x17000083 RID: 131
	// (get) Token: 0x06000249 RID: 585 RVA: 0x0000A88A File Offset: 0x00008A8A
	// (set) Token: 0x0600024A RID: 586 RVA: 0x0000A892 File Offset: 0x00008A92
	public bool Outline
	{
		get
		{
			return this.outline;
		}
		set
		{
			if (value != this.outline)
			{
				this.outline = value;
				this.Invalidate();
			}
		}
	}

	// Token: 0x17000084 RID: 132
	// (get) Token: 0x0600024B RID: 587 RVA: 0x0000A8AA File Offset: 0x00008AAA
	// (set) Token: 0x0600024C RID: 588 RVA: 0x0000A8B2 File Offset: 0x00008AB2
	public int OutlineSize
	{
		get
		{
			return this.outlineWidth;
		}
		set
		{
			value = Mathf.Max(0, value);
			if (value != this.outlineWidth)
			{
				this.outlineWidth = value;
				this.Invalidate();
			}
		}
	}

	// Token: 0x17000085 RID: 133
	// (get) Token: 0x0600024D RID: 589 RVA: 0x0000A8D3 File Offset: 0x00008AD3
	// (set) Token: 0x0600024E RID: 590 RVA: 0x0000A8DB File Offset: 0x00008ADB
	public Color32 OutlineColor
	{
		get
		{
			return this.outlineColor;
		}
		set
		{
			if (!value.Equals(this.outlineColor))
			{
				this.outlineColor = value;
				this.Invalidate();
			}
		}
	}

	// Token: 0x17000086 RID: 134
	// (get) Token: 0x0600024F RID: 591 RVA: 0x0000A904 File Offset: 0x00008B04
	// (set) Token: 0x06000250 RID: 592 RVA: 0x0000A90C File Offset: 0x00008B0C
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

	// Token: 0x17000087 RID: 135
	// (get) Token: 0x06000251 RID: 593 RVA: 0x0000A924 File Offset: 0x00008B24
	// (set) Token: 0x06000252 RID: 594 RVA: 0x0000A92C File Offset: 0x00008B2C
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

	// Token: 0x17000088 RID: 136
	// (get) Token: 0x06000253 RID: 595 RVA: 0x0000A955 File Offset: 0x00008B55
	// (set) Token: 0x06000254 RID: 596 RVA: 0x0000A95D File Offset: 0x00008B5D
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

	// Token: 0x17000089 RID: 137
	// (get) Token: 0x06000255 RID: 597 RVA: 0x0000A97A File Offset: 0x00008B7A
	// (set) Token: 0x06000256 RID: 598 RVA: 0x0000A995 File Offset: 0x00008B95
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

	// Token: 0x1700008A RID: 138
	// (get) Token: 0x06000257 RID: 599 RVA: 0x0000A9BA File Offset: 0x00008BBA
	// (set) Token: 0x06000258 RID: 600 RVA: 0x0000A9C2 File Offset: 0x00008BC2
	public int TabSize
	{
		get
		{
			return this.tabSize;
		}
		set
		{
			value = Mathf.Max(0, value);
			if (value != this.tabSize)
			{
				this.tabSize = value;
				this.Invalidate();
			}
		}
	}

	// Token: 0x1700008B RID: 139
	// (get) Token: 0x06000259 RID: 601 RVA: 0x0000A9E3 File Offset: 0x00008BE3
	public List<int> TabStops
	{
		get
		{
			return this.tabStops;
		}
	}

	// Token: 0x0600025A RID: 602 RVA: 0x0000A9EB File Offset: 0x00008BEB
	protected internal override void OnLocalize()
	{
		base.OnLocalize();
		this.Text = base.getLocalizedValue(this.localizationKey ?? this.text);
	}

	// Token: 0x0600025B RID: 603 RVA: 0x0000AA10 File Offset: 0x00008C10
	protected internal void OnTextChanged()
	{
		if (base.CustomWordWrapAllowed && Settings.Data != null && Settings.Data.DoesCurrentLanguageWordwrap())
		{
			WordWrapper instance = WordWrapper.GetInstance();
			int num = Mathf.CeilToInt((float)this.Font.FontSize * this.TextScale);
			this.text = instance.WrapText(this.text, (int)(base.Width / (float)num), WordWrapper.EWrapAlgorithm.Dynamic);
		}
		this.Invalidate();
		base.Signal("OnTextChanged", this, this.text);
		if (this.TextChanged != null)
		{
			this.TextChanged(this, this.text);
		}
	}

	// Token: 0x0600025C RID: 604 RVA: 0x0000AAAD File Offset: 0x00008CAD
	public override void Start()
	{
		base.Start();
		this.localizationKey = this.Text;
	}

	// Token: 0x0600025D RID: 605 RVA: 0x0000AAC4 File Offset: 0x00008CC4
	public override void OnEnable()
	{
		base.OnEnable();
		bool flag = this.Font != null && this.Font.IsValid;
		if (Application.isPlaying && !flag)
		{
			this.Font = base.GetManager().DefaultFont;
		}
		this.bindTextureRebuildCallback();
		if (this.size.sqrMagnitude <= 1E-45f)
		{
			base.Size = new Vector2(150f, 25f);
		}
	}

	// Token: 0x0600025E RID: 606 RVA: 0x0000AB3C File Offset: 0x00008D3C
	public override void OnDisable()
	{
		base.OnDisable();
		this.unbindTextureRebuildCallback();
	}

	// Token: 0x0600025F RID: 607 RVA: 0x0000AB4A File Offset: 0x00008D4A
	public override void Update()
	{
		if (this.autoSize)
		{
			this.autoHeight = false;
		}
		if (this.Font == null)
		{
			this.Font = base.GetManager().DefaultFont;
		}
		base.Update();
	}

	// Token: 0x06000260 RID: 608 RVA: 0x0000AB80 File Offset: 0x00008D80
	public override void Awake()
	{
		base.Awake();
		this.startSize = (Application.isPlaying ? base.Size : Vector2.zero);
	}

	// Token: 0x06000261 RID: 609 RVA: 0x0000ABA4 File Offset: 0x00008DA4
	public override Vector2 CalculateMinimumSize()
	{
		if (this.Font != null)
		{
			float num = (float)this.Font.FontSize * this.TextScale * 0.75f;
			return Vector2.Max(base.CalculateMinimumSize(), new Vector2(num, num));
		}
		return base.CalculateMinimumSize();
	}

	// Token: 0x06000262 RID: 610 RVA: 0x0000ABF4 File Offset: 0x00008DF4
	[HideInInspector]
	public override void Invalidate()
	{
		base.Invalidate();
		if (this.Font == null || !this.Font.IsValid || base.GetManager() == null)
		{
			return;
		}
		bool flag = this.size.sqrMagnitude <= float.Epsilon;
		if (!this.autoSize && !this.autoHeight && !flag)
		{
			return;
		}
		if (string.IsNullOrEmpty(this.Text))
		{
			Vector2 size;
			Vector2 lhs = size = this.size;
			if (flag)
			{
				size = new Vector2(150f, 24f);
			}
			if (this.AutoSize || this.AutoHeight)
			{
				size.y = (float)Mathf.CeilToInt((float)this.Font.LineHeight * this.TextScale);
			}
			if (lhs != size)
			{
				base.SuspendLayout();
				base.Size = size;
				base.ResumeLayout();
			}
			return;
		}
		Vector2 size2 = this.size;
		using (dfFontRendererBase dfFontRendererBase = this.obtainRenderer())
		{
			Vector2 vector = dfFontRendererBase.MeasureString(this.text).RoundToInt();
			if (this.AutoSize || flag)
			{
				this.size = vector + new Vector2((float)this.padding.horizontal, (float)this.padding.vertical);
			}
			else if (this.AutoHeight)
			{
				this.size = new Vector2(this.size.x, vector.y + (float)this.padding.vertical);
			}
		}
		if ((this.size - size2).sqrMagnitude >= 1f)
		{
			base.raiseSizeChangedEvent();
		}
	}

	// Token: 0x06000263 RID: 611 RVA: 0x0000AD98 File Offset: 0x00008F98
	private dfFontRendererBase obtainRenderer()
	{
		bool flag = base.Size.sqrMagnitude <= float.Epsilon;
		Vector2 vector = base.Size - new Vector2((float)this.padding.horizontal, (float)this.padding.vertical);
		Vector2 maxSize = (this.autoSize || flag) ? this.getAutoSizeDefault() : vector;
		if (this.autoHeight)
		{
			maxSize = new Vector2(vector.x, 2.1474836E+09f);
		}
		float num = base.PixelsToUnits();
		Vector3 vector2 = (this.pivot.TransformToUpperLeft(base.Size) + new Vector3((float)this.padding.left, (float)(-(float)this.padding.top))) * num;
		float num2 = this.TextScale * this.getTextScaleMultiplier();
		dfFontRendererBase dfFontRendererBase = this.Font.ObtainRenderer();
		dfFontRendererBase.WordWrap = this.WordWrap;
		dfFontRendererBase.MaxSize = maxSize;
		dfFontRendererBase.PixelRatio = num;
		dfFontRendererBase.TextScale = num2;
		dfFontRendererBase.CharacterSpacing = this.CharacterSpacing;
		dfFontRendererBase.VectorOffset = vector2.Quantize(num);
		dfFontRendererBase.MultiLine = true;
		dfFontRendererBase.TabSize = this.TabSize;
		dfFontRendererBase.TabStops = this.TabStops;
		dfFontRendererBase.TextAlign = (this.autoSize ? TextAlignment.Left : this.TextAlignment);
		dfFontRendererBase.ColorizeSymbols = this.ColorizeSymbols;
		dfFontRendererBase.ProcessMarkup = this.ProcessMarkup;
		dfFontRendererBase.DefaultColor = (base.IsEnabled ? base.Color : base.DisabledColor);
		dfFontRendererBase.BottomColor = (this.enableGradient ? new Color32?(this.BottomColor) : null);
		dfFontRendererBase.OverrideMarkupColors = !base.IsEnabled;
		dfFontRendererBase.Opacity = base.CalculateOpacity();
		dfFontRendererBase.Outline = this.Outline;
		dfFontRendererBase.OutlineSize = this.OutlineSize;
		dfFontRendererBase.OutlineColor = this.OutlineColor;
		dfFontRendererBase.Shadow = this.Shadow;
		dfFontRendererBase.ShadowColor = this.ShadowColor;
		dfFontRendererBase.ShadowOffset = this.ShadowOffset;
		dfDynamicFont.DynamicFontRenderer dynamicFontRenderer = dfFontRendererBase as dfDynamicFont.DynamicFontRenderer;
		if (dynamicFontRenderer != null)
		{
			dynamicFontRenderer.SpriteAtlas = this.Atlas;
			dynamicFontRenderer.SpriteBuffer = this.renderData;
		}
		if (this.vertAlign != dfVerticalAlignment.Top)
		{
			dfFontRendererBase.VectorOffset = this.getVertAlignOffset(dfFontRendererBase);
		}
		return dfFontRendererBase;
	}

	// Token: 0x06000264 RID: 612 RVA: 0x0000AFFC File Offset: 0x000091FC
	private float getTextScaleMultiplier()
	{
		if (this.textScaleMode == dfTextScaleMode.None || !Application.isPlaying)
		{
			return 1f;
		}
		if (this.textScaleMode == dfTextScaleMode.ScreenResolution)
		{
			return (float)Screen.height / (float)base.GetManager().FixedHeight;
		}
		if (this.autoSize)
		{
			return 1f;
		}
		return base.Size.y / this.startSize.y;
	}

	// Token: 0x06000265 RID: 613 RVA: 0x0000B060 File Offset: 0x00009260
	private Vector2 getAutoSizeDefault()
	{
		float x = (this.maxSize.x > float.Epsilon) ? this.maxSize.x : 2.1474836E+09f;
		float y = (this.maxSize.y > float.Epsilon) ? this.maxSize.y : 2.1474836E+09f;
		return new Vector2(x, y);
	}

	// Token: 0x06000266 RID: 614 RVA: 0x0000B0C0 File Offset: 0x000092C0
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

	// Token: 0x06000267 RID: 615 RVA: 0x0000B154 File Offset: 0x00009354
	protected internal virtual void renderBackground()
	{
		if (this.Atlas == null)
		{
			return;
		}
		dfAtlas.ItemInfo itemInfo = this.Atlas[this.backgroundSprite];
		if (itemInfo == null)
		{
			return;
		}
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

	// Token: 0x06000268 RID: 616 RVA: 0x0000B23C File Offset: 0x0000943C
	public dfList<dfRenderData> RenderMultiple()
	{
		dfList<dfRenderData> result;
		try
		{
			if (!this.isControlInvalidated && (this.textRenderData != null || this.renderData != null))
			{
				Matrix4x4 localToWorldMatrix = base.transform.localToWorldMatrix;
				for (int i = 0; i < this.renderDataBuffers.Count; i++)
				{
					this.renderDataBuffers[i].Transform = localToWorldMatrix;
				}
				result = this.renderDataBuffers;
			}
			else if (this.Atlas == null || this.Font == null || !this.isVisible)
			{
				result = null;
			}
			else
			{
				if (this.renderData == null)
				{
					this.renderData = dfRenderData.Obtain();
					this.textRenderData = dfRenderData.Obtain();
					this.isControlInvalidated = true;
				}
				this.resetRenderBuffers();
				this.renderBackground();
				if (string.IsNullOrEmpty(this.Text))
				{
					if (this.AutoSize || this.AutoHeight)
					{
						base.Height = (float)Mathf.CeilToInt((float)this.Font.LineHeight * this.TextScale);
					}
					result = this.renderDataBuffers;
				}
				else
				{
					bool flag = this.size.sqrMagnitude <= float.Epsilon;
					using (dfFontRendererBase dfFontRendererBase = this.obtainRenderer())
					{
						dfFontRendererBase.Render(this.text, this.textRenderData);
						if (this.AutoSize || flag)
						{
							base.Size = (dfFontRendererBase.RenderedSize + new Vector2((float)this.padding.horizontal, (float)this.padding.vertical)).CeilToInt();
						}
						else if (this.AutoHeight)
						{
							base.Size = new Vector2(this.size.x, dfFontRendererBase.RenderedSize.y + (float)this.padding.vertical).CeilToInt();
						}
					}
					base.updateCollider();
					result = this.renderDataBuffers;
				}
			}
		}
		finally
		{
			this.isControlInvalidated = false;
		}
		return result;
	}

	// Token: 0x06000269 RID: 617 RVA: 0x0000B44C File Offset: 0x0000964C
	private void resetRenderBuffers()
	{
		this.renderDataBuffers.Clear();
		Matrix4x4 localToWorldMatrix = base.transform.localToWorldMatrix;
		if (this.renderData != null)
		{
			this.renderData.Clear();
			this.renderData.Material = this.Atlas.Material;
			this.renderData.Transform = localToWorldMatrix;
			this.renderDataBuffers.Add(this.renderData);
		}
		if (this.textRenderData != null)
		{
			this.textRenderData.Clear();
			this.textRenderData.Material = this.Atlas.Material;
			this.textRenderData.Transform = localToWorldMatrix;
			this.renderDataBuffers.Add(this.textRenderData);
		}
	}

	// Token: 0x0600026A RID: 618 RVA: 0x0000B4FC File Offset: 0x000096FC
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

	// Token: 0x0600026B RID: 619 RVA: 0x0000B568 File Offset: 0x00009768
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

	// Token: 0x0600026C RID: 620 RVA: 0x0000B5D4 File Offset: 0x000097D4
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

	// Token: 0x0600026D RID: 621 RVA: 0x0000B643 File Offset: 0x00009843
	private void onFontTextureRebuilt()
	{
		this.requestCharacterInfo();
		this.Invalidate();
	}

	// Token: 0x0600026E RID: 622 RVA: 0x0000B651 File Offset: 0x00009851
	public void UpdateFontInfo()
	{
		this.requestCharacterInfo();
	}

	// Token: 0x040000C0 RID: 192
	[SerializeField]
	protected dfAtlas atlas;

	// Token: 0x040000C1 RID: 193
	[SerializeField]
	protected dfFontBase font;

	// Token: 0x040000C2 RID: 194
	[SerializeField]
	protected string backgroundSprite;

	// Token: 0x040000C3 RID: 195
	[SerializeField]
	protected Color32 backgroundColor = UnityEngine.Color.white;

	// Token: 0x040000C4 RID: 196
	[SerializeField]
	protected bool autoSize;

	// Token: 0x040000C5 RID: 197
	[SerializeField]
	protected bool autoHeight;

	// Token: 0x040000C6 RID: 198
	[SerializeField]
	protected bool wordWrap;

	// Token: 0x040000C7 RID: 199
	[SerializeField]
	protected string text = "Label";

	// Token: 0x040000C8 RID: 200
	[SerializeField]
	protected Color32 bottomColor = new Color32(byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue);

	// Token: 0x040000C9 RID: 201
	[SerializeField]
	protected TextAlignment align;

	// Token: 0x040000CA RID: 202
	[SerializeField]
	protected dfVerticalAlignment vertAlign;

	// Token: 0x040000CB RID: 203
	[SerializeField]
	protected float textScale = 1f;

	// Token: 0x040000CC RID: 204
	[SerializeField]
	protected dfTextScaleMode textScaleMode;

	// Token: 0x040000CD RID: 205
	[SerializeField]
	protected int charSpacing;

	// Token: 0x040000CE RID: 206
	[SerializeField]
	protected bool colorizeSymbols;

	// Token: 0x040000CF RID: 207
	[SerializeField]
	protected bool processMarkup;

	// Token: 0x040000D0 RID: 208
	[SerializeField]
	protected bool outline;

	// Token: 0x040000D1 RID: 209
	[SerializeField]
	protected int outlineWidth = 1;

	// Token: 0x040000D2 RID: 210
	[SerializeField]
	protected bool enableGradient;

	// Token: 0x040000D3 RID: 211
	[SerializeField]
	protected Color32 outlineColor = UnityEngine.Color.black;

	// Token: 0x040000D4 RID: 212
	[SerializeField]
	protected bool shadow;

	// Token: 0x040000D5 RID: 213
	[SerializeField]
	protected Color32 shadowColor = UnityEngine.Color.black;

	// Token: 0x040000D6 RID: 214
	[SerializeField]
	protected Vector2 shadowOffset = new Vector2(1f, -1f);

	// Token: 0x040000D7 RID: 215
	[SerializeField]
	protected RectOffset padding = new RectOffset();

	// Token: 0x040000D8 RID: 216
	[SerializeField]
	protected int tabSize = 48;

	// Token: 0x040000D9 RID: 217
	[SerializeField]
	protected List<int> tabStops = new List<int>();

	// Token: 0x040000DA RID: 218
	private Vector2 startSize = Vector2.zero;

	// Token: 0x040000DB RID: 219
	private bool isFontCallbackAssigned;

	// Token: 0x040000DC RID: 220
	private dfRenderData textRenderData;

	// Token: 0x040000DD RID: 221
	private dfList<dfRenderData> renderDataBuffers = dfList<dfRenderData>.Obtain();
}

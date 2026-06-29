using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

// Token: 0x0200009D RID: 157
[ExecuteInEditMode]
[AddComponentMenu("Daikon Forge/User Interface/Rich Text Label")]
[Serializable]
public class dfRichTextLabel : dfControl, IDFMultiRender, IRendersText
{
	// Token: 0x1400004D RID: 77
	// (add) Token: 0x06000900 RID: 2304 RVA: 0x00028250 File Offset: 0x00026450
	// (remove) Token: 0x06000901 RID: 2305 RVA: 0x00028288 File Offset: 0x00026488
	public event PropertyChangedEventHandler<string> TextChanged;

	// Token: 0x1400004E RID: 78
	// (add) Token: 0x06000902 RID: 2306 RVA: 0x000282C0 File Offset: 0x000264C0
	// (remove) Token: 0x06000903 RID: 2307 RVA: 0x000282F8 File Offset: 0x000264F8
	public event PropertyChangedEventHandler<Vector2> ScrollPositionChanged;

	// Token: 0x1400004F RID: 79
	// (add) Token: 0x06000904 RID: 2308 RVA: 0x00028330 File Offset: 0x00026530
	// (remove) Token: 0x06000905 RID: 2309 RVA: 0x00028368 File Offset: 0x00026568
	public event dfRichTextLabel.LinkClickEventHandler LinkClicked;

	// Token: 0x170001FD RID: 509
	// (get) Token: 0x06000906 RID: 2310 RVA: 0x0002839D File Offset: 0x0002659D
	// (set) Token: 0x06000907 RID: 2311 RVA: 0x000283A5 File Offset: 0x000265A5
	public bool ForceWordwrap
	{
		get
		{
			return this.forceWordwrap;
		}
		set
		{
			this.forceWordwrap = value;
			this.Invalidate();
		}
	}

	// Token: 0x170001FE RID: 510
	// (get) Token: 0x06000908 RID: 2312 RVA: 0x000283B4 File Offset: 0x000265B4
	// (set) Token: 0x06000909 RID: 2313 RVA: 0x000283BC File Offset: 0x000265BC
	public bool AutoHeight
	{
		get
		{
			return this.autoHeight;
		}
		set
		{
			if (this.autoHeight != value)
			{
				this.autoHeight = value;
				this.scrollPosition = Vector2.zero;
				this.Invalidate();
			}
		}
	}

	// Token: 0x170001FF RID: 511
	// (get) Token: 0x0600090A RID: 2314 RVA: 0x000283E0 File Offset: 0x000265E0
	// (set) Token: 0x0600090B RID: 2315 RVA: 0x00028421 File Offset: 0x00026621
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

	// Token: 0x17000200 RID: 512
	// (get) Token: 0x0600090C RID: 2316 RVA: 0x0002843E File Offset: 0x0002663E
	// (set) Token: 0x0600090D RID: 2317 RVA: 0x00028446 File Offset: 0x00026646
	public dfDynamicFont Font
	{
		get
		{
			return this.font;
		}
		set
		{
			if (value != this.font)
			{
				this.unbindTextureRebuildCallback();
				this.font = value;
				this.bindTextureRebuildCallback();
				this.LineHeight = value.FontSize;
				dfFontManager.Invalidate(this.Font);
				this.Invalidate();
			}
		}
	}

	// Token: 0x17000201 RID: 513
	// (get) Token: 0x0600090E RID: 2318 RVA: 0x00028486 File Offset: 0x00026686
	// (set) Token: 0x0600090F RID: 2319 RVA: 0x0002848E File Offset: 0x0002668E
	public string BlankTextureSprite
	{
		get
		{
			return this.blankTextureSprite;
		}
		set
		{
			if (value != this.blankTextureSprite)
			{
				this.blankTextureSprite = value;
				this.Invalidate();
			}
		}
	}

	// Token: 0x17000202 RID: 514
	// (get) Token: 0x06000910 RID: 2320 RVA: 0x000284AB File Offset: 0x000266AB
	// (set) Token: 0x06000911 RID: 2321 RVA: 0x000284B4 File Offset: 0x000266B4
	public string Text
	{
		get
		{
			return this.text;
		}
		set
		{
			value = base.getLocalizedValue(value);
			if (!string.Equals(this.text, value))
			{
				dfFontManager.Invalidate(this.Font);
				this.text = value;
				this.scrollPosition = Vector2.zero;
				this.Invalidate();
				this.OnTextChanged();
			}
		}
	}

	// Token: 0x17000203 RID: 515
	// (get) Token: 0x06000912 RID: 2322 RVA: 0x00028501 File Offset: 0x00026701
	// (set) Token: 0x06000913 RID: 2323 RVA: 0x00028509 File Offset: 0x00026709
	public int FontSize
	{
		get
		{
			return this.fontSize;
		}
		set
		{
			value = Mathf.Max(6, value);
			if (value != this.fontSize)
			{
				dfFontManager.Invalidate(this.Font);
				this.fontSize = value;
				this.Invalidate();
			}
			this.LineHeight = value;
		}
	}

	// Token: 0x17000204 RID: 516
	// (get) Token: 0x06000914 RID: 2324 RVA: 0x0002853C File Offset: 0x0002673C
	// (set) Token: 0x06000915 RID: 2325 RVA: 0x00028544 File Offset: 0x00026744
	public int LineHeight
	{
		get
		{
			return this.lineheight;
		}
		set
		{
			value = Mathf.Max(this.FontSize, value);
			if (value != this.lineheight)
			{
				this.lineheight = value;
				this.Invalidate();
			}
		}
	}

	// Token: 0x17000205 RID: 517
	// (get) Token: 0x06000916 RID: 2326 RVA: 0x0002856A File Offset: 0x0002676A
	// (set) Token: 0x06000917 RID: 2327 RVA: 0x00028572 File Offset: 0x00026772
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

	// Token: 0x17000206 RID: 518
	// (get) Token: 0x06000918 RID: 2328 RVA: 0x00028581 File Offset: 0x00026781
	// (set) Token: 0x06000919 RID: 2329 RVA: 0x00028589 File Offset: 0x00026789
	public bool PreserveWhitespace
	{
		get
		{
			return this.preserveWhitespace;
		}
		set
		{
			if (value != this.preserveWhitespace)
			{
				this.preserveWhitespace = value;
				this.Invalidate();
			}
		}
	}

	// Token: 0x17000207 RID: 519
	// (get) Token: 0x0600091A RID: 2330 RVA: 0x000285A1 File Offset: 0x000267A1
	// (set) Token: 0x0600091B RID: 2331 RVA: 0x000285A9 File Offset: 0x000267A9
	public FontStyle FontStyle
	{
		get
		{
			return this.fontStyle;
		}
		set
		{
			if (value != this.fontStyle)
			{
				this.fontStyle = value;
				this.Invalidate();
			}
		}
	}

	// Token: 0x17000208 RID: 520
	// (get) Token: 0x0600091C RID: 2332 RVA: 0x000285C1 File Offset: 0x000267C1
	// (set) Token: 0x0600091D RID: 2333 RVA: 0x000285C9 File Offset: 0x000267C9
	public dfMarkupTextAlign TextAlignment
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

	// Token: 0x17000209 RID: 521
	// (get) Token: 0x0600091E RID: 2334 RVA: 0x000285E1 File Offset: 0x000267E1
	// (set) Token: 0x0600091F RID: 2335 RVA: 0x000285E9 File Offset: 0x000267E9
	public bool AllowScrolling
	{
		get
		{
			return this.allowScrolling;
		}
		set
		{
			this.allowScrolling = value;
			if (!value)
			{
				this.ScrollPosition = Vector2.zero;
			}
		}
	}

	// Token: 0x06000920 RID: 2336 RVA: 0x00028600 File Offset: 0x00026800
	public void ForceScrollPosition(Vector2 pos)
	{
		if ((pos - this.scrollPosition).sqrMagnitude > 1E-45f)
		{
			this.scrollPosition = pos;
			this.updateScrollbars();
			this.OnScrollPositionChanged();
		}
	}

	// Token: 0x1700020A RID: 522
	// (get) Token: 0x06000921 RID: 2337 RVA: 0x0002863B File Offset: 0x0002683B
	// (set) Token: 0x06000922 RID: 2338 RVA: 0x00028644 File Offset: 0x00026844
	public Vector2 ScrollPosition
	{
		get
		{
			return this.scrollPosition;
		}
		set
		{
			if (!this.allowScrolling || this.autoHeight)
			{
				value = Vector2.zero;
			}
			if (this.isMarkupInvalidated)
			{
				this.processMarkup();
			}
			value = Vector2.Min(this.ContentSize - base.Size, value);
			value = Vector2.Max(Vector2.zero, value);
			value = value.RoundToInt();
			if ((value - this.scrollPosition).sqrMagnitude > 1E-45f)
			{
				this.scrollPosition = value;
				this.updateScrollbars();
				this.OnScrollPositionChanged();
			}
		}
	}

	// Token: 0x1700020B RID: 523
	// (get) Token: 0x06000923 RID: 2339 RVA: 0x000286D2 File Offset: 0x000268D2
	// (set) Token: 0x06000924 RID: 2340 RVA: 0x000286DA File Offset: 0x000268DA
	public dfScrollbar HorizontalScrollbar
	{
		get
		{
			return this.horzScrollbar;
		}
		set
		{
			this.horzScrollbar = value;
			this.updateScrollbars();
		}
	}

	// Token: 0x1700020C RID: 524
	// (get) Token: 0x06000925 RID: 2341 RVA: 0x000286E9 File Offset: 0x000268E9
	// (set) Token: 0x06000926 RID: 2342 RVA: 0x000286F1 File Offset: 0x000268F1
	public dfScrollbar VerticalScrollbar
	{
		get
		{
			return this.vertScrollbar;
		}
		set
		{
			this.vertScrollbar = value;
			this.updateScrollbars();
		}
	}

	// Token: 0x1700020D RID: 525
	// (get) Token: 0x06000927 RID: 2343 RVA: 0x00028700 File Offset: 0x00026900
	public Vector2 ContentSize
	{
		get
		{
			if (this.viewportBox != null)
			{
				return this.viewportBox.Size;
			}
			return base.Size;
		}
	}

	// Token: 0x1700020E RID: 526
	// (get) Token: 0x06000928 RID: 2344 RVA: 0x0002871C File Offset: 0x0002691C
	// (set) Token: 0x06000929 RID: 2345 RVA: 0x00028724 File Offset: 0x00026924
	public bool UseScrollMomentum
	{
		get
		{
			return this.useScrollMomentum;
		}
		set
		{
			this.useScrollMomentum = value;
			this.scrollMomentum = Vector2.zero;
		}
	}

	// Token: 0x0600092A RID: 2346 RVA: 0x00028738 File Offset: 0x00026938
	protected internal override void OnLocalize()
	{
		base.OnLocalize();
		this.Text = base.getLocalizedValue(this.text);
	}

	// Token: 0x0600092B RID: 2347 RVA: 0x00028754 File Offset: 0x00026954
	[HideInInspector]
	public override void Invalidate()
	{
		base.Invalidate();
		dfFontManager.Invalidate(this.Font);
		this.isMarkupInvalidated = true;
		if (base.CustomWordWrapAllowed && Settings.Data != null && Settings.Data.DoesCurrentLanguageWordwrap() && Settings.Data.LanguageSet)
		{
			this.WrapText();
		}
	}

	// Token: 0x0600092C RID: 2348 RVA: 0x000287AC File Offset: 0x000269AC
	public override void Awake()
	{
		base.Awake();
		this.startSize = base.Size;
	}

	// Token: 0x0600092D RID: 2349 RVA: 0x000287C0 File Offset: 0x000269C0
	public override void OnEnable()
	{
		base.OnEnable();
		this.bindTextureRebuildCallback();
		if (this.size.sqrMagnitude <= 1E-45f)
		{
			base.Size = new Vector2(320f, 200f);
			this.FontSize = (this.LineHeight = 16);
		}
	}

	// Token: 0x0600092E RID: 2350 RVA: 0x00028811 File Offset: 0x00026A11
	public override void OnDisable()
	{
		base.OnDisable();
		this.unbindTextureRebuildCallback();
	}

	// Token: 0x0600092F RID: 2351 RVA: 0x00028820 File Offset: 0x00026A20
	public override void Update()
	{
		base.Update();
		if (this.useScrollMomentum && !this.isMouseDown && this.scrollMomentum.magnitude > 0.5f)
		{
			this.ScrollPosition += this.scrollMomentum;
			this.scrollMomentum *= 0.95f - Time.deltaTime;
		}
	}

	// Token: 0x06000930 RID: 2352 RVA: 0x00028888 File Offset: 0x00026A88
	public override void LateUpdate()
	{
		base.LateUpdate();
		this.initialize();
	}

	// Token: 0x06000931 RID: 2353 RVA: 0x00028898 File Offset: 0x00026A98
	private void WrapText()
	{
		WordWrapper instance = WordWrapper.GetInstance();
		if (!this.text.Contains(instance.DefaultDelimiter))
		{
			return;
		}
		int lineWidth = (int)(base.Width / (float)this.FontSize);
		StringBuilder stringBuilder = new StringBuilder();
		List<string> list = this.parseText(this.Text);
		for (int i = 0; i < list.Count; i++)
		{
			if (list[i].Length != 0)
			{
				if (char.IsWhiteSpace(list[i][0]) || list[i][0] == '<')
				{
					stringBuilder.Append(list[i]);
				}
				else
				{
					stringBuilder.Append(instance.WrapText(list[i].TrimEnd(Array.Empty<char>()), lineWidth, WordWrapper.EWrapAlgorithm.Dynamic));
				}
			}
		}
		this.text = stringBuilder.ToString();
	}

	// Token: 0x06000932 RID: 2354 RVA: 0x0002896C File Offset: 0x00026B6C
	private List<string> parseText(string message)
	{
		List<string> list = new List<string>();
		int i = 0;
		while (i < message.Length)
		{
			int num = message.IndexOf('<', i);
			if (num == i)
			{
				int num2 = message.IndexOf('>', i);
				list.Add(message.Substring(i, num2 - num + 1));
				i = num2 + 1;
			}
			else
			{
				if (num == -1)
				{
					list.Add(message.Substring(i, message.Length - i));
					break;
				}
				list.Add(message.Substring(i, num - i));
				i = num;
			}
		}
		return list;
	}

	// Token: 0x06000933 RID: 2355 RVA: 0x000289EA File Offset: 0x00026BEA
	protected internal void OnTextChanged()
	{
		this.Invalidate();
		base.Signal("OnTextChanged", this, this.text);
		if (this.TextChanged != null)
		{
			this.TextChanged(this, this.text);
		}
	}

	// Token: 0x06000934 RID: 2356 RVA: 0x00028A20 File Offset: 0x00026C20
	protected internal void OnScrollPositionChanged()
	{
		base.Invalidate();
		base.SignalHierarchy("OnScrollPositionChanged", new object[]
		{
			this,
			this.ScrollPosition
		});
		if (this.ScrollPositionChanged != null)
		{
			this.ScrollPositionChanged(this, this.ScrollPosition);
		}
	}

	// Token: 0x06000935 RID: 2357 RVA: 0x00028A74 File Offset: 0x00026C74
	protected internal override void OnKeyDown(dfKeyEventArgs args)
	{
		if (args.Used)
		{
			base.OnKeyDown(args);
			return;
		}
		int num = this.FontSize;
		int num2 = this.FontSize;
		switch (args.KeyCode)
		{
		case KeyCode.UpArrow:
			this.ScrollPosition += new Vector2(0f, (float)(-(float)num2));
			args.Use();
			break;
		case KeyCode.DownArrow:
			this.ScrollPosition += new Vector2(0f, (float)num2);
			args.Use();
			break;
		case KeyCode.RightArrow:
			this.ScrollPosition += new Vector2((float)num, 0f);
			args.Use();
			break;
		case KeyCode.LeftArrow:
			this.ScrollPosition += new Vector2((float)(-(float)num), 0f);
			args.Use();
			break;
		case KeyCode.Home:
			this.ScrollToTop();
			args.Use();
			break;
		case KeyCode.End:
			this.ScrollToBottom();
			args.Use();
			break;
		}
		base.OnKeyDown(args);
	}

	// Token: 0x06000936 RID: 2358 RVA: 0x00028B8D File Offset: 0x00026D8D
	internal override void OnDragEnd(dfDragEventArgs args)
	{
		base.OnDragEnd(args);
		this.isMouseDown = false;
	}

	// Token: 0x06000937 RID: 2359 RVA: 0x00028B9D File Offset: 0x00026D9D
	protected internal override void OnMouseEnter(dfMouseEventArgs args)
	{
		base.OnMouseEnter(args);
		this.touchStartPosition = args.Position;
	}

	// Token: 0x06000938 RID: 2360 RVA: 0x00028BB2 File Offset: 0x00026DB2
	protected internal override void OnMouseDown(dfMouseEventArgs args)
	{
		base.OnMouseDown(args);
		this.mouseDownTag = this.hitTestTag(args);
		this.mouseDownScrollPosition = this.scrollPosition;
		this.scrollMomentum = Vector2.zero;
		this.touchStartPosition = args.Position;
		this.isMouseDown = true;
	}

	// Token: 0x06000939 RID: 2361 RVA: 0x00028BF4 File Offset: 0x00026DF4
	protected internal override void OnMouseUp(dfMouseEventArgs args)
	{
		base.OnMouseUp(args);
		this.isMouseDown = false;
		if (Vector2.Distance(this.scrollPosition, this.mouseDownScrollPosition) <= 2f && this.hitTestTag(args) == this.mouseDownTag)
		{
			dfMarkupTag dfMarkupTag = this.mouseDownTag;
			while (dfMarkupTag != null && !(dfMarkupTag is dfMarkupTagAnchor))
			{
				dfMarkupTag = (dfMarkupTag.Parent as dfMarkupTag);
			}
			if (dfMarkupTag is dfMarkupTagAnchor)
			{
				base.Signal("OnLinkClicked", this, dfMarkupTag);
				if (this.LinkClicked != null)
				{
					this.LinkClicked(this, dfMarkupTag as dfMarkupTagAnchor);
				}
			}
		}
		this.mouseDownTag = null;
		this.mouseDownScrollPosition = this.scrollPosition;
	}

	// Token: 0x0600093A RID: 2362 RVA: 0x00028C9C File Offset: 0x00026E9C
	protected internal override void OnMouseMove(dfMouseEventArgs args)
	{
		base.OnMouseMove(args);
		if (!this.allowScrolling || this.autoHeight)
		{
			return;
		}
		if ((args is dfTouchEventArgs || this.isMouseDown) && (args.Position - this.touchStartPosition).magnitude > 5f)
		{
			Vector2 vector = args.MoveDelta.Scale(-1f, 1f);
			Vector2 screenSize = base.GetManager().GetScreenSize();
			Camera camera = Camera.main ?? base.GetCamera();
			vector.x = screenSize.x * (vector.x / (float)camera.pixelWidth);
			vector.y = screenSize.y * (vector.y / (float)camera.pixelHeight);
			this.ScrollPosition += vector;
			this.scrollMomentum = (this.scrollMomentum + vector) * 0.5f;
		}
	}

	// Token: 0x0600093B RID: 2363 RVA: 0x00028D94 File Offset: 0x00026F94
	protected internal override void OnMouseWheel(dfMouseEventArgs args)
	{
		try
		{
			if (!args.Used && this.allowScrolling && !this.autoHeight)
			{
				int num = this.UseScrollMomentum ? 1 : 3;
				float num2 = (this.vertScrollbar != null) ? this.vertScrollbar.IncrementAmount : ((float)(this.FontSize * num));
				this.ScrollPosition = new Vector2(this.scrollPosition.x, this.scrollPosition.y - num2 * args.WheelDelta);
				this.scrollMomentum = new Vector2(0f, -num2 * args.WheelDelta);
				args.Use();
				base.Signal("OnMouseWheel", this, args);
			}
		}
		finally
		{
			base.OnMouseWheel(args);
		}
	}

	// Token: 0x0600093C RID: 2364 RVA: 0x00028E64 File Offset: 0x00027064
	public void ScrollToTop()
	{
		this.ScrollPosition = new Vector2(this.scrollPosition.x, 0f);
	}

	// Token: 0x0600093D RID: 2365 RVA: 0x00028E81 File Offset: 0x00027081
	public void ScrollToBottom()
	{
		this.ScrollPosition = new Vector2(this.scrollPosition.x, 2.1474836E+09f);
	}

	// Token: 0x0600093E RID: 2366 RVA: 0x00028E9E File Offset: 0x0002709E
	public void ScrollToLeft()
	{
		this.ScrollPosition = new Vector2(0f, this.scrollPosition.y);
	}

	// Token: 0x0600093F RID: 2367 RVA: 0x00028EBB File Offset: 0x000270BB
	public void ScrollToRight()
	{
		this.ScrollPosition = new Vector2(2.1474836E+09f, this.scrollPosition.y);
	}

	// Token: 0x06000940 RID: 2368 RVA: 0x00028ED8 File Offset: 0x000270D8
	public dfList<dfRenderData> RenderMultiple()
	{
		if (!this.isVisible || this.Font == null)
		{
			return null;
		}
		Matrix4x4 localToWorldMatrix = base.transform.localToWorldMatrix;
		if (!this.isControlInvalidated && this.viewportBox != null)
		{
			for (int i = 0; i < this.buffers.Count; i++)
			{
				this.buffers[i].Transform = localToWorldMatrix;
			}
			return this.buffers;
		}
		dfList<dfRenderData> result;
		try
		{
			this.isControlInvalidated = false;
			if (this.isMarkupInvalidated)
			{
				this.isMarkupInvalidated = false;
				this.processMarkup();
			}
			this.viewportBox.FitToContents();
			if (this.autoHeight)
			{
				base.Height = (float)this.viewportBox.Height;
			}
			this.updateScrollbars();
			this.buffers.Clear();
			this.gatherRenderBuffers(this.viewportBox, this.buffers);
			result = this.buffers;
		}
		finally
		{
			base.updateCollider();
		}
		return result;
	}

	// Token: 0x06000941 RID: 2369 RVA: 0x00028FD0 File Offset: 0x000271D0
	private dfMarkupTag hitTestTag(dfMouseEventArgs args)
	{
		Vector2 point = base.GetHitPosition(args) + this.scrollPosition;
		dfMarkupBox dfMarkupBox = this.viewportBox.HitTest(point);
		if (dfMarkupBox != null)
		{
			dfMarkupElement dfMarkupElement = dfMarkupBox.Element;
			while (dfMarkupElement != null && !(dfMarkupElement is dfMarkupTag))
			{
				dfMarkupElement = dfMarkupElement.Parent;
			}
			return dfMarkupElement as dfMarkupTag;
		}
		return null;
	}

	// Token: 0x06000942 RID: 2370 RVA: 0x00029024 File Offset: 0x00027224
	private void processMarkup()
	{
		this.releaseMarkupReferences();
		this.elements = dfMarkupParser.Parse(this, this.text);
		float textScaleMultiplier = this.getTextScaleMultiplier();
		int num = Mathf.CeilToInt((float)this.FontSize * textScaleMultiplier);
		int lineHeight = Mathf.CeilToInt((float)this.LineHeight * textScaleMultiplier);
		dfMarkupStyle style = new dfMarkupStyle
		{
			Host = this,
			Atlas = this.Atlas,
			Font = this.Font,
			FontSize = num,
			FontStyle = this.FontStyle,
			LineHeight = lineHeight,
			Color = base.ApplyOpacity(base.Color),
			Opacity = base.CalculateOpacity(),
			Align = this.TextAlignment,
			PreserveWhitespace = this.preserveWhitespace
		};
		this.viewportBox = new dfMarkupBox(null, dfMarkupDisplayType.block, style)
		{
			Size = base.Size
		};
		for (int i = 0; i < this.elements.Count; i++)
		{
			dfMarkupElement dfMarkupElement = this.elements[i];
			if (dfMarkupElement != null)
			{
				dfMarkupElement.PerformLayout(this.viewportBox, style);
			}
		}
	}

	// Token: 0x06000943 RID: 2371 RVA: 0x0002914C File Offset: 0x0002734C
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

	// Token: 0x06000944 RID: 2372 RVA: 0x000291A4 File Offset: 0x000273A4
	private void releaseMarkupReferences()
	{
		this.mouseDownTag = null;
		if (this.viewportBox != null)
		{
			this.viewportBox.Release();
		}
		if (this.elements != null)
		{
			for (int i = 0; i < this.elements.Count; i++)
			{
				this.elements[i].Release();
			}
			this.elements.Release();
		}
	}

	// Token: 0x06000945 RID: 2373 RVA: 0x00029208 File Offset: 0x00027408
	[HideInInspector]
	private void initialize()
	{
		if (this.initialized)
		{
			return;
		}
		this.initialized = true;
		if (Application.isPlaying)
		{
			if (this.horzScrollbar != null)
			{
				this.horzScrollbar.ValueChanged += this.horzScroll_ValueChanged;
			}
			if (this.vertScrollbar != null)
			{
				this.vertScrollbar.ValueChanged += this.vertScroll_ValueChanged;
			}
		}
		this.Invalidate();
		this.ScrollPosition = Vector2.zero;
		this.updateScrollbars();
	}

	// Token: 0x06000946 RID: 2374 RVA: 0x0002928D File Offset: 0x0002748D
	private void vertScroll_ValueChanged(dfControl control, float value)
	{
		this.ScrollPosition = new Vector2(this.scrollPosition.x, value);
	}

	// Token: 0x06000947 RID: 2375 RVA: 0x000292A6 File Offset: 0x000274A6
	private void horzScroll_ValueChanged(dfControl control, float value)
	{
		this.ScrollPosition = new Vector2(value, this.ScrollPosition.y);
	}

	// Token: 0x06000948 RID: 2376 RVA: 0x000292C0 File Offset: 0x000274C0
	private void updateScrollbars()
	{
		if (this.horzScrollbar != null)
		{
			this.horzScrollbar.MinValue = 0f;
			this.horzScrollbar.MaxValue = this.ContentSize.x;
			this.horzScrollbar.ScrollSize = base.Size.x;
			this.horzScrollbar.Value = this.ScrollPosition.x;
		}
		if (this.vertScrollbar != null)
		{
			this.vertScrollbar.MinValue = 0f;
			this.vertScrollbar.MaxValue = this.ContentSize.y;
			this.vertScrollbar.ScrollSize = base.Size.y;
			this.vertScrollbar.Value = this.ScrollPosition.y;
		}
	}

	// Token: 0x06000949 RID: 2377 RVA: 0x00029390 File Offset: 0x00027590
	private void gatherRenderBuffers(dfMarkupBox box, dfList<dfRenderData> buffers)
	{
		dfIntersectionType viewportIntersection = this.getViewportIntersection(box);
		if (viewportIntersection == dfIntersectionType.None)
		{
			return;
		}
		dfRenderData dfRenderData = box.Render();
		if (dfRenderData != null)
		{
			if (dfRenderData.Material == null && this.atlas != null)
			{
				dfRenderData.Material = this.atlas.Material;
			}
			float d = base.PixelsToUnits();
			Vector3 a = -this.scrollPosition.Scale(1f, -1f).RoundToInt() + box.GetOffset().Scale(1f, -1f) + this.pivot.TransformToUpperLeft(base.Size);
			dfList<Vector3> vertices = dfRenderData.Vertices;
			for (int i = 0; i < dfRenderData.Vertices.Count; i++)
			{
				vertices[i] = (a + vertices[i]) * d;
			}
			if (viewportIntersection == dfIntersectionType.Intersecting)
			{
				this.clipToViewport(dfRenderData);
			}
			dfRenderData.Transform = base.transform.localToWorldMatrix;
			buffers.Add(dfRenderData);
		}
		for (int j = 0; j < box.Children.Count; j++)
		{
			this.gatherRenderBuffers(box.Children[j], buffers);
		}
	}

	// Token: 0x0600094A RID: 2378 RVA: 0x000294D0 File Offset: 0x000276D0
	private dfIntersectionType getViewportIntersection(dfMarkupBox box)
	{
		if (box.Display == dfMarkupDisplayType.none)
		{
			return dfIntersectionType.None;
		}
		Vector2 size = base.Size;
		Vector2 vector = box.GetOffset() - this.scrollPosition;
		Vector2 vector2 = vector + box.Size;
		if (vector2.x <= 0f || vector2.y <= 0f)
		{
			return dfIntersectionType.None;
		}
		if (vector.x >= size.x || vector.y >= size.y)
		{
			return dfIntersectionType.None;
		}
		if (vector.x < 0f || vector.y < 0f || vector2.x > size.x || vector2.y > size.y)
		{
			return dfIntersectionType.Intersecting;
		}
		return dfIntersectionType.Inside;
	}

	// Token: 0x0600094B RID: 2379 RVA: 0x00029584 File Offset: 0x00027784
	private void clipToViewport(dfRenderData renderData)
	{
		IList<Plane> viewportClippingPlanes = this.getViewportClippingPlanes();
		Material material = renderData.Material;
		Matrix4x4 transform = renderData.Transform;
		dfRichTextLabel.clipBuffer.Clear();
		dfClippingUtil.Clip(viewportClippingPlanes, renderData, dfRichTextLabel.clipBuffer);
		renderData.Clear();
		renderData.Merge(dfRichTextLabel.clipBuffer, false);
		renderData.Material = material;
		renderData.Transform = transform;
	}

	// Token: 0x0600094C RID: 2380 RVA: 0x000295DC File Offset: 0x000277DC
	private Plane[] getViewportClippingPlanes()
	{
		Vector3[] corners = base.GetCorners();
		Matrix4x4 worldToLocalMatrix = base.transform.worldToLocalMatrix;
		for (int i = 0; i < corners.Length; i++)
		{
			corners[i] = worldToLocalMatrix.MultiplyPoint(corners[i]);
		}
		this.cachedClippingPlanes[0] = new Plane(Vector3.right, corners[0]);
		this.cachedClippingPlanes[1] = new Plane(Vector3.left, corners[1]);
		this.cachedClippingPlanes[2] = new Plane(Vector3.up, corners[2]);
		this.cachedClippingPlanes[3] = new Plane(Vector3.down, corners[0]);
		return this.cachedClippingPlanes;
	}

	// Token: 0x0600094D RID: 2381 RVA: 0x00029699 File Offset: 0x00027899
	public void UpdateFontInfo()
	{
		if (!dfFontManager.IsDirty(this.Font))
		{
			return;
		}
		if (string.IsNullOrEmpty(this.text))
		{
			return;
		}
		this.updateFontInfo(this.viewportBox);
	}

	// Token: 0x0600094E RID: 2382 RVA: 0x000296C4 File Offset: 0x000278C4
	private void updateFontInfo(dfMarkupBox box)
	{
		if (box == null)
		{
			return;
		}
		if (box != this.viewportBox && this.getViewportIntersection(box) == dfIntersectionType.None)
		{
			return;
		}
		dfMarkupBoxText dfMarkupBoxText = box as dfMarkupBoxText;
		if (dfMarkupBoxText != null)
		{
			this.font.AddCharacterRequest(dfMarkupBoxText.Text, dfMarkupBoxText.Style.FontSize, dfMarkupBoxText.Style.FontStyle);
		}
		for (int i = 0; i < box.Children.Count; i++)
		{
			this.updateFontInfo(box.Children[i]);
		}
	}

	// Token: 0x0600094F RID: 2383 RVA: 0x00029744 File Offset: 0x00027944
	private void onFontTextureRebuilt()
	{
		this.Invalidate();
		this.updateFontInfo(this.viewportBox);
	}

	// Token: 0x06000950 RID: 2384 RVA: 0x00029758 File Offset: 0x00027958
	private void bindTextureRebuildCallback()
	{
		if (this.isFontCallbackAssigned || this.Font == null)
		{
			return;
		}
		Font baseFont = this.Font.BaseFont;
		baseFont.textureRebuildCallback = (Font.FontTextureRebuildCallback)Delegate.Combine(baseFont.textureRebuildCallback, new Font.FontTextureRebuildCallback(this.onFontTextureRebuilt));
		this.isFontCallbackAssigned = true;
	}

	// Token: 0x06000951 RID: 2385 RVA: 0x000297B0 File Offset: 0x000279B0
	private void unbindTextureRebuildCallback()
	{
		if (!this.isFontCallbackAssigned || this.Font == null)
		{
			return;
		}
		Font baseFont = this.Font.BaseFont;
		baseFont.textureRebuildCallback = (Font.FontTextureRebuildCallback)Delegate.Remove(baseFont.textureRebuildCallback, new Font.FontTextureRebuildCallback(this.onFontTextureRebuilt));
		this.isFontCallbackAssigned = false;
	}

	// Token: 0x0400045B RID: 1115
	[SerializeField]
	protected dfAtlas atlas;

	// Token: 0x0400045C RID: 1116
	[SerializeField]
	protected dfDynamicFont font;

	// Token: 0x0400045D RID: 1117
	[SerializeField]
	protected string text = "Rich Text Label";

	// Token: 0x0400045E RID: 1118
	[SerializeField]
	protected int fontSize = 16;

	// Token: 0x0400045F RID: 1119
	[SerializeField]
	protected int lineheight = 16;

	// Token: 0x04000460 RID: 1120
	[SerializeField]
	protected dfTextScaleMode textScaleMode;

	// Token: 0x04000461 RID: 1121
	[SerializeField]
	protected FontStyle fontStyle;

	// Token: 0x04000462 RID: 1122
	[SerializeField]
	protected bool preserveWhitespace;

	// Token: 0x04000463 RID: 1123
	[SerializeField]
	protected string blankTextureSprite;

	// Token: 0x04000464 RID: 1124
	[SerializeField]
	protected dfMarkupTextAlign align;

	// Token: 0x04000465 RID: 1125
	[SerializeField]
	protected bool allowScrolling;

	// Token: 0x04000466 RID: 1126
	[SerializeField]
	protected dfScrollbar horzScrollbar;

	// Token: 0x04000467 RID: 1127
	[SerializeField]
	protected dfScrollbar vertScrollbar;

	// Token: 0x04000468 RID: 1128
	[SerializeField]
	protected bool useScrollMomentum;

	// Token: 0x04000469 RID: 1129
	[SerializeField]
	protected bool autoHeight;

	// Token: 0x0400046A RID: 1130
	[SerializeField]
	private bool forceWordwrap;

	// Token: 0x0400046B RID: 1131
	private static dfRenderData clipBuffer = new dfRenderData();

	// Token: 0x0400046C RID: 1132
	private dfList<dfRenderData> buffers = new dfList<dfRenderData>();

	// Token: 0x0400046D RID: 1133
	private dfList<dfMarkupElement> elements;

	// Token: 0x0400046E RID: 1134
	private dfMarkupBox viewportBox;

	// Token: 0x0400046F RID: 1135
	private dfMarkupTag mouseDownTag;

	// Token: 0x04000470 RID: 1136
	private Vector2 mouseDownScrollPosition = Vector2.zero;

	// Token: 0x04000471 RID: 1137
	private Vector2 scrollPosition = Vector2.zero;

	// Token: 0x04000472 RID: 1138
	private bool initialized;

	// Token: 0x04000473 RID: 1139
	private bool isMouseDown;

	// Token: 0x04000474 RID: 1140
	private Vector2 touchStartPosition = Vector2.zero;

	// Token: 0x04000475 RID: 1141
	private Vector2 scrollMomentum = Vector2.zero;

	// Token: 0x04000476 RID: 1142
	private bool isMarkupInvalidated = true;

	// Token: 0x04000477 RID: 1143
	private Vector2 startSize = Vector2.zero;

	// Token: 0x04000478 RID: 1144
	private bool isFontCallbackAssigned;

	// Token: 0x0200037B RID: 891
	// (Invoke) Token: 0x06001CE2 RID: 7394
	[dfEventCategory("Markup")]
	public delegate void LinkClickEventHandler(dfRichTextLabel sender, dfMarkupTagAnchor tag);
}

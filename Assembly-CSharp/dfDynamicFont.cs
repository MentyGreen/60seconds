using System;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

// Token: 0x0200003C RID: 60
[ExecuteInEditMode]
[AddComponentMenu("Daikon Forge/User Interface/Dynamic Font")]
[Serializable]
public class dfDynamicFont : dfFontBase
{
	// Token: 0x17000168 RID: 360
	// (get) Token: 0x06000604 RID: 1540 RVA: 0x0001C9A4 File Offset: 0x0001ABA4
	// (set) Token: 0x06000605 RID: 1541 RVA: 0x0001C9FF File Offset: 0x0001ABFF
	public override Material Material
	{
		get
		{
			if (this.baseFont != null && this.material != null)
			{
				this.material.mainTexture = this.baseFont.material.mainTexture;
				this.material.shader = this.Shader;
			}
			return this.material;
		}
		set
		{
			if (value != this.material)
			{
				this.material = value;
				dfGUIManager.RefreshAll();
			}
		}
	}

	// Token: 0x17000169 RID: 361
	// (get) Token: 0x06000606 RID: 1542 RVA: 0x0001CA1B File Offset: 0x0001AC1B
	// (set) Token: 0x06000607 RID: 1543 RVA: 0x0001CA41 File Offset: 0x0001AC41
	public Shader Shader
	{
		get
		{
			if (this.shader == null)
			{
				this.shader = Shader.Find("Daikon Forge/Dynamic Font Shader");
			}
			return this.shader;
		}
		set
		{
			this.shader = value;
			dfGUIManager.RefreshAll();
		}
	}

	// Token: 0x1700016A RID: 362
	// (get) Token: 0x06000608 RID: 1544 RVA: 0x0001CA4F File Offset: 0x0001AC4F
	public override Texture Texture
	{
		get
		{
			return this.baseFont.material.mainTexture;
		}
	}

	// Token: 0x1700016B RID: 363
	// (get) Token: 0x06000609 RID: 1545 RVA: 0x0001CA61 File Offset: 0x0001AC61
	public override bool IsValid
	{
		get
		{
			return this.baseFont != null && this.Material != null && this.Texture != null;
		}
	}

	// Token: 0x1700016C RID: 364
	// (get) Token: 0x0600060A RID: 1546 RVA: 0x0001CA8D File Offset: 0x0001AC8D
	// (set) Token: 0x0600060B RID: 1547 RVA: 0x0001CA95 File Offset: 0x0001AC95
	public override int FontSize
	{
		get
		{
			return this.baseFontSize;
		}
		set
		{
			if (value != this.baseFontSize)
			{
				this.baseFontSize = value;
				dfGUIManager.RefreshAll();
			}
		}
	}

	// Token: 0x1700016D RID: 365
	// (get) Token: 0x0600060C RID: 1548 RVA: 0x0001CAAC File Offset: 0x0001ACAC
	// (set) Token: 0x0600060D RID: 1549 RVA: 0x0001CAB4 File Offset: 0x0001ACB4
	public override int LineHeight
	{
		get
		{
			return this.lineHeight;
		}
		set
		{
			if (value != this.lineHeight)
			{
				this.lineHeight = value;
				dfGUIManager.RefreshAll();
			}
		}
	}

	// Token: 0x0600060E RID: 1550 RVA: 0x0001CACB File Offset: 0x0001ACCB
	public override dfFontRendererBase ObtainRenderer()
	{
		return dfDynamicFont.DynamicFontRenderer.Obtain(this);
	}

	// Token: 0x1700016E RID: 366
	// (get) Token: 0x0600060F RID: 1551 RVA: 0x0001CAD3 File Offset: 0x0001ACD3
	// (set) Token: 0x06000610 RID: 1552 RVA: 0x0001CADB File Offset: 0x0001ACDB
	public Font BaseFont
	{
		get
		{
			return this.baseFont;
		}
		set
		{
			if (value != this.baseFont)
			{
				this.baseFont = value;
				dfGUIManager.RefreshAll();
			}
		}
	}

	// Token: 0x1700016F RID: 367
	// (get) Token: 0x06000611 RID: 1553 RVA: 0x0001CAF7 File Offset: 0x0001ACF7
	// (set) Token: 0x06000612 RID: 1554 RVA: 0x0001CAFF File Offset: 0x0001ACFF
	public int Baseline
	{
		get
		{
			return this.baseline;
		}
		set
		{
			if (value != this.baseline)
			{
				this.baseline = value;
				dfGUIManager.RefreshAll();
			}
		}
	}

	// Token: 0x17000170 RID: 368
	// (get) Token: 0x06000613 RID: 1555 RVA: 0x0001CB16 File Offset: 0x0001AD16
	public int Descent
	{
		get
		{
			return this.LineHeight - this.baseline;
		}
	}

	// Token: 0x06000614 RID: 1556 RVA: 0x0001CB28 File Offset: 0x0001AD28
	public static dfDynamicFont FindByName(string name)
	{
		for (int i = 0; i < dfDynamicFont.loadedFonts.Count; i++)
		{
			if (string.Equals(dfDynamicFont.loadedFonts[i].name, name, StringComparison.OrdinalIgnoreCase))
			{
				return dfDynamicFont.loadedFonts[i];
			}
		}
		GameObject gameObject = Resources.Load(name) as GameObject;
		if (gameObject == null)
		{
			return null;
		}
		dfDynamicFont component = gameObject.GetComponent<dfDynamicFont>();
		if (component == null)
		{
			return null;
		}
		dfDynamicFont.loadedFonts.Add(component);
		return component;
	}

	// Token: 0x06000615 RID: 1557 RVA: 0x0001CBA4 File Offset: 0x0001ADA4
	public Vector2 MeasureText(string text, int size, FontStyle style)
	{
		this.RequestCharacters(text, size, style);
		float num = (float)size / (float)this.FontSize;
		int num2 = Mathf.CeilToInt((float)this.Baseline * num);
		Vector2 result = new Vector2(0f, (float)num2);
		CharacterInfo characterInfo = default(CharacterInfo);
		for (int i = 0; i < text.Length; i++)
		{
			this.BaseFont.GetCharacterInfo(text[i], out characterInfo, size, style);
			float num3 = Mathf.Ceil(characterInfo.vert.x + characterInfo.vert.width);
			if (text[i] == ' ')
			{
				num3 = Mathf.Ceil(characterInfo.width);
			}
			else if (text[i] == '\t')
			{
				num3 += (float)(size * 4);
			}
			result.x += num3;
		}
		return result;
	}

	// Token: 0x06000616 RID: 1558 RVA: 0x0001CC78 File Offset: 0x0001AE78
	public void RequestCharacters(string text, int size, FontStyle style)
	{
		if (this.baseFont == null)
		{
			throw new NullReferenceException("Base Font not assigned: " + base.name);
		}
		dfFontManager.Invalidate(this);
		this.baseFont.RequestCharactersInTexture(text, size, style);
	}

	// Token: 0x06000617 RID: 1559 RVA: 0x0001CCB4 File Offset: 0x0001AEB4
	public virtual void AddCharacterRequest(string characters, int fontSize, FontStyle style)
	{
		dfFontManager.FlagPendingRequests(this);
		dfDynamicFont.FontCharacterRequest fontCharacterRequest = dfDynamicFont.FontCharacterRequest.Obtain();
		fontCharacterRequest.Characters = characters;
		fontCharacterRequest.FontSize = fontSize;
		fontCharacterRequest.FontStyle = style;
		this.requests.Add(fontCharacterRequest);
	}

	// Token: 0x06000618 RID: 1560 RVA: 0x0001CCF0 File Offset: 0x0001AEF0
	public virtual void FlushCharacterRequests()
	{
		for (int i = 0; i < this.requests.Count; i++)
		{
			dfDynamicFont.FontCharacterRequest fontCharacterRequest = this.requests[i];
			this.baseFont.RequestCharactersInTexture(fontCharacterRequest.Characters, fontCharacterRequest.FontSize, fontCharacterRequest.FontStyle);
		}
		this.requests.ReleaseItems();
	}

	// Token: 0x04000222 RID: 546
	private static List<dfDynamicFont> loadedFonts = new List<dfDynamicFont>();

	// Token: 0x04000223 RID: 547
	[SerializeField]
	private Font baseFont;

	// Token: 0x04000224 RID: 548
	[SerializeField]
	private Material material;

	// Token: 0x04000225 RID: 549
	[SerializeField]
	private Shader shader;

	// Token: 0x04000226 RID: 550
	[SerializeField]
	private int baseFontSize = -1;

	// Token: 0x04000227 RID: 551
	[SerializeField]
	private int baseline = -1;

	// Token: 0x04000228 RID: 552
	[SerializeField]
	private int lineHeight;

	// Token: 0x04000229 RID: 553
	protected dfList<dfDynamicFont.FontCharacterRequest> requests = new dfList<dfDynamicFont.FontCharacterRequest>();

	// Token: 0x02000369 RID: 873
	protected class FontCharacterRequest : IPoolable
	{
		// Token: 0x06001C68 RID: 7272 RVA: 0x000794D2 File Offset: 0x000776D2
		public static dfDynamicFont.FontCharacterRequest Obtain()
		{
			if (dfDynamicFont.FontCharacterRequest.pool.Count <= 0)
			{
				return new dfDynamicFont.FontCharacterRequest();
			}
			return dfDynamicFont.FontCharacterRequest.pool.Pop();
		}

		// Token: 0x06001C69 RID: 7273 RVA: 0x000794F1 File Offset: 0x000776F1
		public void Release()
		{
			this.Characters = null;
			this.FontSize = 0;
			this.FontStyle = FontStyle.Normal;
			dfDynamicFont.FontCharacterRequest.pool.Add(this);
		}

		// Token: 0x04001632 RID: 5682
		private static dfList<dfDynamicFont.FontCharacterRequest> pool = new dfList<dfDynamicFont.FontCharacterRequest>();

		// Token: 0x04001633 RID: 5683
		public string Characters;

		// Token: 0x04001634 RID: 5684
		public int FontSize;

		// Token: 0x04001635 RID: 5685
		public FontStyle FontStyle;
	}

	// Token: 0x0200036A RID: 874
	public class DynamicFontRenderer : dfFontRendererBase, IPoolable
	{
		// Token: 0x17000502 RID: 1282
		// (get) Token: 0x06001C6C RID: 7276 RVA: 0x00079527 File Offset: 0x00077727
		public int LineCount
		{
			get
			{
				return this.lines.Count;
			}
		}

		// Token: 0x17000503 RID: 1283
		// (get) Token: 0x06001C6D RID: 7277 RVA: 0x00079534 File Offset: 0x00077734
		// (set) Token: 0x06001C6E RID: 7278 RVA: 0x0007953C File Offset: 0x0007773C
		public dfAtlas SpriteAtlas { get; set; }

		// Token: 0x17000504 RID: 1284
		// (get) Token: 0x06001C6F RID: 7279 RVA: 0x00079545 File Offset: 0x00077745
		// (set) Token: 0x06001C70 RID: 7280 RVA: 0x0007954D File Offset: 0x0007774D
		public dfRenderData SpriteBuffer { get; set; }

		// Token: 0x06001C71 RID: 7281 RVA: 0x00079556 File Offset: 0x00077756
		internal DynamicFontRenderer()
		{
		}

		// Token: 0x06001C72 RID: 7282 RVA: 0x0007955E File Offset: 0x0007775E
		public static dfFontRendererBase Obtain(dfDynamicFont font)
		{
			dfDynamicFont.DynamicFontRenderer dynamicFontRenderer = (dfDynamicFont.DynamicFontRenderer.objectPool.Count > 0) ? dfDynamicFont.DynamicFontRenderer.objectPool.Dequeue() : new dfDynamicFont.DynamicFontRenderer();
			dynamicFontRenderer.Reset();
			dynamicFontRenderer.Font = font;
			dynamicFontRenderer.inUse = true;
			return dynamicFontRenderer;
		}

		// Token: 0x06001C73 RID: 7283 RVA: 0x00079594 File Offset: 0x00077794
		public override void Release()
		{
			if (!this.inUse)
			{
				return;
			}
			this.inUse = false;
			this.Reset();
			if (this.tokens != null)
			{
				this.tokens.Release();
				this.tokens = null;
			}
			if (this.lines != null)
			{
				this.lines.ReleaseItems();
				this.lines.Release();
				this.lines = null;
			}
			base.BottomColor = null;
			dfDynamicFont.DynamicFontRenderer.objectPool.Enqueue(this);
		}

		// Token: 0x06001C74 RID: 7284 RVA: 0x00079610 File Offset: 0x00077810
		public override float[] GetCharacterWidths(string text)
		{
			float num = 0f;
			return this.GetCharacterWidths(text, 0, text.Length - 1, out num);
		}

		// Token: 0x06001C75 RID: 7285 RVA: 0x00079638 File Offset: 0x00077838
		public float[] GetCharacterWidths(string text, int startIndex, int endIndex, out float totalWidth)
		{
			totalWidth = 0f;
			dfDynamicFont dfDynamicFont = (dfDynamicFont)base.Font;
			int size = Mathf.CeilToInt((float)dfDynamicFont.FontSize * base.TextScale);
			float[] array = new float[text.Length];
			float num = 0f;
			float num2 = 0f;
			dfDynamicFont.RequestCharacters(text, size, FontStyle.Normal);
			CharacterInfo characterInfo = default(CharacterInfo);
			int i = startIndex;
			while (i <= endIndex)
			{
				if (dfDynamicFont.BaseFont.GetCharacterInfo(text[i], out characterInfo, size, FontStyle.Normal))
				{
					if (text[i] == '\t')
					{
						num2 += (float)base.TabSize;
					}
					else if (text[i] == ' ')
					{
						num2 += characterInfo.width;
					}
					else
					{
						num2 += characterInfo.vert.x + characterInfo.vert.width;
					}
					array[i] = (num2 - num) * base.PixelRatio;
				}
				i++;
				num = num2;
			}
			return array;
		}

		// Token: 0x06001C76 RID: 7286 RVA: 0x0007972C File Offset: 0x0007792C
		public override Vector2 MeasureString(string text)
		{
			dfDynamicFont dfDynamicFont = (dfDynamicFont)base.Font;
			int size = Mathf.CeilToInt((float)dfDynamicFont.FontSize * base.TextScale);
			dfDynamicFont.RequestCharacters(text, size, FontStyle.Normal);
			this.tokenize(text);
			dfList<dfDynamicFont.LineRenderInfo> dfList = this.calculateLinebreaks();
			float num = 0f;
			float num2 = 0f;
			for (int i = 0; i < dfList.Count; i++)
			{
				num = Mathf.Max(dfList[i].lineWidth, num);
				num2 += dfList[i].lineHeight;
			}
			this.tokens.Release();
			this.tokens = null;
			return new Vector2(num, num2);
		}

		// Token: 0x06001C77 RID: 7287 RVA: 0x000797CC File Offset: 0x000779CC
		public override void Render(string text, dfRenderData destination)
		{
			dfDynamicFont.DynamicFontRenderer.textColors.Clear();
			dfDynamicFont.DynamicFontRenderer.textColors.Push(Color.white);
			dfDynamicFont dfDynamicFont = (dfDynamicFont)base.Font;
			int size = Mathf.CeilToInt((float)dfDynamicFont.FontSize * base.TextScale);
			dfDynamicFont.RequestCharacters(text, size, FontStyle.Normal);
			this.tokenize(text);
			dfList<dfDynamicFont.LineRenderInfo> dfList = this.calculateLinebreaks();
			destination.EnsureCapacity(this.getAnticipatedVertCount(this.tokens));
			int num = 0;
			int num2 = 0;
			Vector3 position = (base.VectorOffset / base.PixelRatio).CeilToInt();
			for (int i = 0; i < dfList.Count; i++)
			{
				dfDynamicFont.LineRenderInfo lineRenderInfo = dfList[i];
				int count = destination.Vertices.Count;
				int startIndex = (this.SpriteBuffer != null) ? this.SpriteBuffer.Vertices.Count : 0;
				this.renderLine(dfList[i], dfDynamicFont.DynamicFontRenderer.textColors, position, destination);
				position.y -= lineRenderInfo.lineHeight;
				num = Mathf.Max((int)lineRenderInfo.lineWidth, num);
				num2 += Mathf.CeilToInt(lineRenderInfo.lineHeight);
				if (lineRenderInfo.lineWidth > base.MaxSize.x)
				{
					this.clipRight(destination, count);
					this.clipRight(this.SpriteBuffer, startIndex);
				}
				this.clipBottom(destination, count);
				this.clipBottom(this.SpriteBuffer, startIndex);
			}
			base.RenderedSize = new Vector2(Mathf.Min(base.MaxSize.x, (float)num), Mathf.Min(base.MaxSize.y, (float)num2)) * base.TextScale;
			this.tokens.Release();
			this.tokens = null;
		}

		// Token: 0x06001C78 RID: 7288 RVA: 0x00079980 File Offset: 0x00077B80
		private int getAnticipatedVertCount(dfList<dfMarkupToken> tokens)
		{
			int num = 4 + (base.Shadow ? 4 : 0) + (base.Outline ? 4 : 0);
			int num2 = 0;
			for (int i = 0; i < tokens.Count; i++)
			{
				dfMarkupToken dfMarkupToken = tokens[i];
				if (dfMarkupToken.TokenType == dfMarkupTokenType.Text)
				{
					num2 += num * dfMarkupToken.Length;
				}
				else if (dfMarkupToken.TokenType == dfMarkupTokenType.StartTag)
				{
					num2 += 4;
				}
			}
			return num2;
		}

		// Token: 0x06001C79 RID: 7289 RVA: 0x000799E8 File Offset: 0x00077BE8
		private void renderLine(dfDynamicFont.LineRenderInfo line, Stack<Color32> colors, Vector3 position, dfRenderData destination)
		{
			position.x += (float)this.calculateLineAlignment(line);
			for (int i = line.startOffset; i <= line.endOffset; i++)
			{
				dfMarkupToken dfMarkupToken = this.tokens[i];
				dfMarkupTokenType tokenType = dfMarkupToken.TokenType;
				if (tokenType == dfMarkupTokenType.Text)
				{
					this.renderText(dfMarkupToken, colors.Peek(), position, destination);
				}
				else if (tokenType == dfMarkupTokenType.StartTag)
				{
					if (dfMarkupToken.Matches("sprite") && this.SpriteAtlas != null && this.SpriteBuffer != null)
					{
						this.renderSprite(dfMarkupToken, colors.Peek(), position, this.SpriteBuffer);
					}
					else if (dfMarkupToken.Matches("color"))
					{
						colors.Push(this.parseColor(dfMarkupToken));
					}
				}
				else if (tokenType == dfMarkupTokenType.EndTag && dfMarkupToken.Matches("color") && colors.Count > 1)
				{
					colors.Pop();
				}
				position.x += (float)dfMarkupToken.Width;
			}
		}

		// Token: 0x06001C7A RID: 7290 RVA: 0x00079ADC File Offset: 0x00077CDC
		private void renderText(dfMarkupToken token, Color32 color, Vector3 position, dfRenderData renderData)
		{
			dfDynamicFont dfDynamicFont = (dfDynamicFont)base.Font;
			int num = Mathf.CeilToInt((float)dfDynamicFont.FontSize * base.TextScale);
			FontStyle style = FontStyle.Normal;
			CharacterInfo glyph = default(CharacterInfo);
			int descent = dfDynamicFont.Descent;
			dfList<Vector3> vertices = renderData.Vertices;
			dfList<int> triangles = renderData.Triangles;
			dfList<Vector2> uv = renderData.UV;
			dfList<Color32> colors = renderData.Colors;
			float num2 = position.x;
			float y = position.y;
			renderData.Material = dfDynamicFont.Material;
			Color32 color2 = this.applyOpacity(this.multiplyColors(color, base.DefaultColor));
			Color32 item = color2;
			if (base.BottomColor != null)
			{
				item = this.applyOpacity(this.multiplyColors(color, base.BottomColor.Value));
			}
			for (int i = 0; i < token.Length; i++)
			{
				if (i > 0)
				{
					num2 += (float)base.CharacterSpacing * base.TextScale;
				}
				if (dfDynamicFont.baseFont.GetCharacterInfo(token[i], out glyph, num, style))
				{
					float num3 = (float)dfDynamicFont.FontSize + glyph.vert.y - (float)num + (float)descent;
					float num4 = num2 + glyph.vert.x;
					float num5 = y + num3;
					float x = num4 + glyph.vert.width;
					float y2 = num5 + glyph.vert.height;
					Vector3 vector = new Vector3(num4, num5) * base.PixelRatio;
					Vector3 vector2 = new Vector3(x, num5) * base.PixelRatio;
					Vector3 vector3 = new Vector3(x, y2) * base.PixelRatio;
					Vector3 vector4 = new Vector3(num4, y2) * base.PixelRatio;
					if (base.Shadow)
					{
						dfDynamicFont.DynamicFontRenderer.addTriangleIndices(vertices, triangles);
						Vector3 b = base.ShadowOffset * base.PixelRatio;
						vertices.Add(vector + b);
						vertices.Add(vector2 + b);
						vertices.Add(vector3 + b);
						vertices.Add(vector4 + b);
						Color32 item2 = this.applyOpacity(base.ShadowColor);
						colors.Add(item2);
						colors.Add(item2);
						colors.Add(item2);
						colors.Add(item2);
						dfDynamicFont.DynamicFontRenderer.addUVCoords(uv, glyph);
					}
					if (base.Outline)
					{
						for (int j = 0; j < dfDynamicFont.DynamicFontRenderer.OUTLINE_OFFSETS.Length; j++)
						{
							dfDynamicFont.DynamicFontRenderer.addTriangleIndices(vertices, triangles);
							Vector3 b2 = dfDynamicFont.DynamicFontRenderer.OUTLINE_OFFSETS[j] * (float)base.OutlineSize * base.PixelRatio;
							vertices.Add(vector + b2);
							vertices.Add(vector2 + b2);
							vertices.Add(vector3 + b2);
							vertices.Add(vector4 + b2);
							Color32 item3 = this.applyOpacity(base.OutlineColor);
							colors.Add(item3);
							colors.Add(item3);
							colors.Add(item3);
							colors.Add(item3);
							dfDynamicFont.DynamicFontRenderer.addUVCoords(uv, glyph);
						}
					}
					dfDynamicFont.DynamicFontRenderer.addTriangleIndices(vertices, triangles);
					vertices.Add(vector);
					vertices.Add(vector2);
					vertices.Add(vector3);
					vertices.Add(vector4);
					colors.Add(color2);
					colors.Add(color2);
					colors.Add(item);
					colors.Add(item);
					dfDynamicFont.DynamicFontRenderer.addUVCoords(uv, glyph);
					num2 += (float)Mathf.CeilToInt(glyph.vert.x + glyph.vert.width);
				}
			}
		}

		// Token: 0x06001C7B RID: 7291 RVA: 0x00079EB4 File Offset: 0x000780B4
		private static void addUVCoords(dfList<Vector2> uvs, CharacterInfo glyph)
		{
			Rect uv = glyph.uv;
			float x = uv.x;
			float y = uv.y + uv.height;
			float x2 = x + uv.width;
			float y2 = uv.y;
			if (glyph.flipped)
			{
				uvs.Add(new Vector2(x2, y2));
				uvs.Add(new Vector2(x2, y));
				uvs.Add(new Vector2(x, y));
				uvs.Add(new Vector2(x, y2));
				return;
			}
			uvs.Add(new Vector2(x, y));
			uvs.Add(new Vector2(x2, y));
			uvs.Add(new Vector2(x2, y2));
			uvs.Add(new Vector2(x, y2));
		}

		// Token: 0x06001C7C RID: 7292 RVA: 0x00079F68 File Offset: 0x00078168
		private void renderSprite(dfMarkupToken token, Color32 color, Vector3 position, dfRenderData destination)
		{
			string value = token.GetAttribute(0).Value.Value;
			dfAtlas.ItemInfo itemInfo = this.SpriteAtlas[value];
			if (itemInfo == null)
			{
				return;
			}
			dfSprite.RenderOptions options = new dfSprite.RenderOptions
			{
				atlas = this.SpriteAtlas,
				color = color,
				fillAmount = 1f,
				flip = dfSpriteFlip.None,
				offset = position,
				pixelsToUnits = base.PixelRatio,
				size = new Vector2((float)token.Width, (float)token.Height),
				spriteInfo = itemInfo
			};
			dfSprite.renderSprite(this.SpriteBuffer, options);
		}

		// Token: 0x06001C7D RID: 7293 RVA: 0x0007A014 File Offset: 0x00078214
		private Color32 parseColor(dfMarkupToken token)
		{
			Color c = Color.white;
			if (token.AttributeCount == 1)
			{
				string value = token.GetAttribute(0).Value.Value;
				if (value.Length == 7 && value[0] == '#')
				{
					uint num = 0U;
					uint.TryParse(value.Substring(1), NumberStyles.HexNumber, null, out num);
					c = this.UIntToColor(num | 4278190080U);
				}
				else
				{
					c = dfMarkupStyle.ParseColor(value, base.DefaultColor);
				}
			}
			return this.applyOpacity(c);
		}

		// Token: 0x06001C7E RID: 7294 RVA: 0x0007A0A0 File Offset: 0x000782A0
		private Color32 UIntToColor(uint color)
		{
			byte a = (byte)(color >> 24);
			byte r = (byte)(color >> 16);
			byte g = (byte)(color >> 8);
			byte b = (byte)color;
			return new Color32(r, g, b, a);
		}

		// Token: 0x06001C7F RID: 7295 RVA: 0x0007A0C8 File Offset: 0x000782C8
		private dfList<dfDynamicFont.LineRenderInfo> calculateLinebreaks()
		{
			if (this.lines != null)
			{
				return this.lines;
			}
			this.lines = dfList<dfDynamicFont.LineRenderInfo>.Obtain();
			dfDynamicFont dfDynamicFont = (dfDynamicFont)base.Font;
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			int num4 = 0;
			float num5 = (float)dfDynamicFont.Baseline * base.TextScale;
			while (num3 < this.tokens.Count && (float)this.lines.Count * num5 <= base.MaxSize.y + num5)
			{
				dfMarkupToken dfMarkupToken = this.tokens[num3];
				dfMarkupTokenType tokenType = dfMarkupToken.TokenType;
				if (tokenType == dfMarkupTokenType.Newline)
				{
					this.lines.Add(dfDynamicFont.LineRenderInfo.Obtain(num2, num3));
					num = (num2 = ++num3);
					num4 = 0;
				}
				else
				{
					int num6 = Mathf.CeilToInt((float)dfMarkupToken.Width);
					if (base.WordWrap && num > num2 && (tokenType == dfMarkupTokenType.Text || (tokenType == dfMarkupTokenType.StartTag && dfMarkupToken.Matches("sprite"))) && (float)(num4 + num6) >= base.MaxSize.x)
					{
						if (num > num2)
						{
							this.lines.Add(dfDynamicFont.LineRenderInfo.Obtain(num2, num - 1));
							num3 = (num2 = ++num);
							num4 = 0;
						}
						else
						{
							this.lines.Add(dfDynamicFont.LineRenderInfo.Obtain(num2, num - 1));
							num = (num2 = ++num3);
							num4 = 0;
						}
					}
					else
					{
						if (tokenType == dfMarkupTokenType.Whitespace)
						{
							num = num3;
						}
						num4 += num6;
						num3++;
					}
				}
			}
			if (num2 < this.tokens.Count)
			{
				this.lines.Add(dfDynamicFont.LineRenderInfo.Obtain(num2, this.tokens.Count - 1));
			}
			for (int i = 0; i < this.lines.Count; i++)
			{
				this.calculateLineSize(this.lines[i]);
			}
			return this.lines;
		}

		// Token: 0x06001C80 RID: 7296 RVA: 0x0007A284 File Offset: 0x00078484
		private int calculateLineAlignment(dfDynamicFont.LineRenderInfo line)
		{
			float lineWidth = line.lineWidth;
			if (base.TextAlign == TextAlignment.Left || lineWidth < 1f)
			{
				return 0;
			}
			float b;
			if (base.TextAlign == TextAlignment.Right)
			{
				b = base.MaxSize.x - lineWidth;
			}
			else
			{
				b = (base.MaxSize.x - lineWidth) * 0.5f;
			}
			return Mathf.CeilToInt(Mathf.Max(0f, b));
		}

		// Token: 0x06001C81 RID: 7297 RVA: 0x0007A2F0 File Offset: 0x000784F0
		private void calculateLineSize(dfDynamicFont.LineRenderInfo line)
		{
			dfDynamicFont dfDynamicFont = (dfDynamicFont)base.Font;
			line.lineHeight = (float)dfDynamicFont.Baseline * base.TextScale;
			int num = 0;
			for (int i = line.startOffset; i <= line.endOffset; i++)
			{
				num += this.tokens[i].Width;
			}
			line.lineWidth = (float)num;
		}

		// Token: 0x06001C82 RID: 7298 RVA: 0x0007A354 File Offset: 0x00078554
		private void tokenize(string text)
		{
			if (base.ProcessMarkup)
			{
				this.tokens = dfMarkupTokenizer.Tokenize(text);
			}
			else
			{
				this.tokens = dfPlainTextTokenizer.Tokenize(text);
			}
			for (int i = 0; i < this.tokens.Count; i++)
			{
				this.calculateTokenRenderSize(this.tokens[i]);
			}
		}

		// Token: 0x06001C83 RID: 7299 RVA: 0x0007A3AC File Offset: 0x000785AC
		private void calculateTokenRenderSize(dfMarkupToken token)
		{
			float num = 0f;
			dfDynamicFont dfDynamicFont = (dfDynamicFont)base.Font;
			CharacterInfo characterInfo = default(CharacterInfo);
			if (token.TokenType == dfMarkupTokenType.Text)
			{
				int size = Mathf.CeilToInt((float)dfDynamicFont.FontSize * base.TextScale);
				for (int i = 0; i < token.Length; i++)
				{
					char c = token[i];
					dfDynamicFont.baseFont.GetCharacterInfo(c, out characterInfo, size, FontStyle.Normal);
					if (c == '\t')
					{
						num += (float)base.TabSize;
					}
					else
					{
						num += ((c != ' ') ? (characterInfo.vert.x + characterInfo.vert.width) : (characterInfo.width + (float)base.CharacterSpacing * base.TextScale));
					}
				}
				if (token.Length > 2)
				{
					num += (float)((token.Length - 2) * base.CharacterSpacing) * base.TextScale;
				}
				token.Height = base.Font.LineHeight;
				token.Width = Mathf.CeilToInt(num);
				return;
			}
			if (token.TokenType == dfMarkupTokenType.Whitespace)
			{
				int size2 = Mathf.CeilToInt((float)dfDynamicFont.FontSize * base.TextScale);
				float num2 = (float)base.CharacterSpacing * base.TextScale;
				for (int j = 0; j < token.Length; j++)
				{
					char c = token[j];
					if (c == '\t')
					{
						num += (float)base.TabSize;
					}
					else if (c == ' ')
					{
						dfDynamicFont.baseFont.GetCharacterInfo(c, out characterInfo, size2, FontStyle.Normal);
						num += characterInfo.width + num2;
					}
				}
				token.Height = base.Font.LineHeight;
				token.Width = Mathf.CeilToInt(num);
				return;
			}
			if (token.TokenType == dfMarkupTokenType.StartTag && token.Matches("sprite") && this.SpriteAtlas != null && token.AttributeCount == 1)
			{
				Texture2D texture = this.SpriteAtlas.Texture;
				float num3 = (float)dfDynamicFont.Baseline * base.TextScale;
				string value = token.GetAttribute(0).Value.Value;
				dfAtlas.ItemInfo itemInfo = this.SpriteAtlas[value];
				if (itemInfo != null)
				{
					float num4 = itemInfo.region.width * (float)texture.width / (itemInfo.region.height * (float)texture.height);
					num = (float)Mathf.CeilToInt(num3 * num4);
				}
				token.Height = Mathf.CeilToInt(num3);
				token.Width = Mathf.CeilToInt(num);
			}
		}

		// Token: 0x06001C84 RID: 7300 RVA: 0x0007A628 File Offset: 0x00078828
		private float getTabStop(float position)
		{
			float num = base.PixelRatio * base.TextScale;
			if (base.TabStops != null && base.TabStops.Count > 0)
			{
				for (int i = 0; i < base.TabStops.Count; i++)
				{
					if ((float)base.TabStops[i] * num > position)
					{
						return (float)base.TabStops[i] * num;
					}
				}
			}
			if (base.TabSize > 0)
			{
				return position + (float)base.TabSize * num;
			}
			return position + (float)(base.Font.FontSize * 4) * num;
		}

		// Token: 0x06001C85 RID: 7301 RVA: 0x0007A6B8 File Offset: 0x000788B8
		private void clipRight(dfRenderData destination, int startIndex)
		{
			if (destination == null)
			{
				return;
			}
			float num = base.VectorOffset.x + base.MaxSize.x * base.PixelRatio;
			dfList<Vector3> vertices = destination.Vertices;
			dfList<Vector2> uv = destination.UV;
			for (int i = startIndex; i < vertices.Count; i += 4)
			{
				Vector3 vector = vertices[i];
				Vector3 vector2 = vertices[i + 1];
				Vector3 vector3 = vertices[i + 2];
				Vector3 vector4 = vertices[i + 3];
				float num2 = vector2.x - vector.x;
				if (vector2.x > num)
				{
					float t = 1f - (num - vector2.x + num2) / num2;
					dfList<Vector3> dfList = vertices;
					int index = i;
					vector = new Vector3(Mathf.Min(vector.x, num), vector.y, vector.z);
					dfList[index] = vector;
					dfList<Vector3> dfList2 = vertices;
					int index2 = i + 1;
					vector2 = new Vector3(Mathf.Min(vector2.x, num), vector2.y, vector2.z);
					dfList2[index2] = vector2;
					dfList<Vector3> dfList3 = vertices;
					int index3 = i + 2;
					vector3 = new Vector3(Mathf.Min(vector3.x, num), vector3.y, vector3.z);
					dfList3[index3] = vector3;
					dfList<Vector3> dfList4 = vertices;
					int index4 = i + 3;
					vector4 = new Vector3(Mathf.Min(vector4.x, num), vector4.y, vector4.z);
					dfList4[index4] = vector4;
					float x = Mathf.Lerp(uv[i + 1].x, uv[i].x, t);
					uv[i + 1] = new Vector2(x, uv[i + 1].y);
					uv[i + 2] = new Vector2(x, uv[i + 2].y);
					num2 = vector2.x - vector.x;
				}
			}
		}

		// Token: 0x06001C86 RID: 7302 RVA: 0x0007A890 File Offset: 0x00078A90
		private void clipBottom(dfRenderData destination, int startIndex)
		{
			if (destination == null)
			{
				return;
			}
			float num = base.VectorOffset.y - base.MaxSize.y * base.PixelRatio;
			dfList<Vector3> vertices = destination.Vertices;
			dfList<Vector2> uv = destination.UV;
			dfList<Color32> colors = destination.Colors;
			for (int i = startIndex; i < vertices.Count; i += 4)
			{
				Vector3 vector = vertices[i];
				Vector3 vector2 = vertices[i + 1];
				Vector3 vector3 = vertices[i + 2];
				Vector3 vector4 = vertices[i + 3];
				float num2 = vector.y - vector4.y;
				if (vector4.y <= num)
				{
					float t = 1f - Mathf.Abs(-num + vector.y) / num2;
					dfList<Vector3> dfList = vertices;
					int index = i;
					vector = new Vector3(vector.x, Mathf.Max(vector.y, num), vector2.z);
					dfList[index] = vector;
					dfList<Vector3> dfList2 = vertices;
					int index2 = i + 1;
					vector2 = new Vector3(vector2.x, Mathf.Max(vector2.y, num), vector2.z);
					dfList2[index2] = vector2;
					dfList<Vector3> dfList3 = vertices;
					int index3 = i + 2;
					vector3 = new Vector3(vector3.x, Mathf.Max(vector3.y, num), vector3.z);
					dfList3[index3] = vector3;
					dfList<Vector3> dfList4 = vertices;
					int index4 = i + 3;
					vector4 = new Vector3(vector4.x, Mathf.Max(vector4.y, num), vector4.z);
					dfList4[index4] = vector4;
					uv[i + 3] = Vector2.Lerp(uv[i + 3], uv[i], t);
					uv[i + 2] = Vector2.Lerp(uv[i + 2], uv[i + 1], t);
					Color c = Color.Lerp(colors[i + 3], colors[i], t);
					colors[i + 3] = c;
					colors[i + 2] = c;
				}
			}
		}

		// Token: 0x06001C87 RID: 7303 RVA: 0x0007AA9B File Offset: 0x00078C9B
		private Color32 applyOpacity(Color32 color)
		{
			color.a = (byte)(base.Opacity * 255f);
			return color;
		}

		// Token: 0x06001C88 RID: 7304 RVA: 0x0007AAB4 File Offset: 0x00078CB4
		private static void addTriangleIndices(dfList<Vector3> verts, dfList<int> triangles)
		{
			int count = verts.Count;
			int[] triangle_INDICES = dfDynamicFont.DynamicFontRenderer.TRIANGLE_INDICES;
			for (int i = 0; i < triangle_INDICES.Length; i++)
			{
				triangles.Add(count + triangle_INDICES[i]);
			}
		}

		// Token: 0x06001C89 RID: 7305 RVA: 0x0007AAE7 File Offset: 0x00078CE7
		private Color multiplyColors(Color lhs, Color rhs)
		{
			return new Color(lhs.r * rhs.r, lhs.g * rhs.g, lhs.b * rhs.b, lhs.a * rhs.a);
		}

		// Token: 0x04001636 RID: 5686
		private static Queue<dfDynamicFont.DynamicFontRenderer> objectPool = new Queue<dfDynamicFont.DynamicFontRenderer>();

		// Token: 0x04001637 RID: 5687
		private static Vector2[] OUTLINE_OFFSETS = new Vector2[]
		{
			new Vector2(-1f, -1f),
			new Vector2(-1f, 1f),
			new Vector2(1f, -1f),
			new Vector2(1f, 1f)
		};

		// Token: 0x04001638 RID: 5688
		private static int[] TRIANGLE_INDICES = new int[]
		{
			0,
			1,
			3,
			3,
			1,
			2
		};

		// Token: 0x04001639 RID: 5689
		private static Stack<Color32> textColors = new Stack<Color32>();

		// Token: 0x0400163C RID: 5692
		private dfList<dfDynamicFont.LineRenderInfo> lines;

		// Token: 0x0400163D RID: 5693
		private dfList<dfMarkupToken> tokens;

		// Token: 0x0400163E RID: 5694
		private bool inUse;
	}

	// Token: 0x0200036B RID: 875
	private class LineRenderInfo : IPoolable
	{
		// Token: 0x17000505 RID: 1285
		// (get) Token: 0x06001C8B RID: 7307 RVA: 0x0007ABBE File Offset: 0x00078DBE
		public int length
		{
			get
			{
				return this.endOffset - this.startOffset + 1;
			}
		}

		// Token: 0x06001C8C RID: 7308 RVA: 0x0007ABCF File Offset: 0x00078DCF
		private LineRenderInfo()
		{
		}

		// Token: 0x06001C8D RID: 7309 RVA: 0x0007ABD7 File Offset: 0x00078DD7
		public static dfDynamicFont.LineRenderInfo Obtain(int start, int end)
		{
			dfDynamicFont.LineRenderInfo lineRenderInfo = (dfDynamicFont.LineRenderInfo.pool.Count > 0) ? dfDynamicFont.LineRenderInfo.pool.Pop() : new dfDynamicFont.LineRenderInfo();
			lineRenderInfo.startOffset = start;
			lineRenderInfo.endOffset = end;
			lineRenderInfo.lineHeight = 0f;
			return lineRenderInfo;
		}

		// Token: 0x06001C8E RID: 7310 RVA: 0x0007AC10 File Offset: 0x00078E10
		public void Release()
		{
			this.startOffset = (this.endOffset = 0);
			this.lineWidth = (this.lineHeight = 0f);
			dfDynamicFont.LineRenderInfo.pool.Add(this);
		}

		// Token: 0x0400163F RID: 5695
		public int startOffset;

		// Token: 0x04001640 RID: 5696
		public int endOffset;

		// Token: 0x04001641 RID: 5697
		public float lineWidth;

		// Token: 0x04001642 RID: 5698
		public float lineHeight;

		// Token: 0x04001643 RID: 5699
		private static dfList<dfDynamicFont.LineRenderInfo> pool = new dfList<dfDynamicFont.LineRenderInfo>();
	}
}

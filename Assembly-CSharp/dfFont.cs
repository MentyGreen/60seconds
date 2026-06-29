using System;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

// Token: 0x02000060 RID: 96
[dfHelp("http://www.daikonforge.com/docs/df-gui/classdf_font.html")]
[AddComponentMenu("Daikon Forge/User Interface/Font Definition")]
[Serializable]
public class dfFont : dfFontBase
{
	// Token: 0x17000189 RID: 393
	// (get) Token: 0x06000687 RID: 1671 RVA: 0x0001D56A File Offset: 0x0001B76A
	public List<dfFont.GlyphDefinition> Glyphs
	{
		get
		{
			return this.glyphs;
		}
	}

	// Token: 0x1700018A RID: 394
	// (get) Token: 0x06000688 RID: 1672 RVA: 0x0001D572 File Offset: 0x0001B772
	public List<dfFont.GlyphKerning> KerningInfo
	{
		get
		{
			return this.kerning;
		}
	}

	// Token: 0x1700018B RID: 395
	// (get) Token: 0x06000689 RID: 1673 RVA: 0x0001D57A File Offset: 0x0001B77A
	// (set) Token: 0x0600068A RID: 1674 RVA: 0x0001D582 File Offset: 0x0001B782
	public dfAtlas Atlas
	{
		get
		{
			return this.atlas;
		}
		set
		{
			if (value != this.atlas)
			{
				this.atlas = value;
				this.glyphMap = null;
			}
		}
	}

	// Token: 0x1700018C RID: 396
	// (get) Token: 0x0600068B RID: 1675 RVA: 0x0001D5A0 File Offset: 0x0001B7A0
	// (set) Token: 0x0600068C RID: 1676 RVA: 0x0001D5AD File Offset: 0x0001B7AD
	public override Material Material
	{
		get
		{
			return this.Atlas.Material;
		}
		set
		{
			throw new InvalidOperationException();
		}
	}

	// Token: 0x1700018D RID: 397
	// (get) Token: 0x0600068D RID: 1677 RVA: 0x0001D5B4 File Offset: 0x0001B7B4
	public override Texture Texture
	{
		get
		{
			return this.Atlas.Texture;
		}
	}

	// Token: 0x1700018E RID: 398
	// (get) Token: 0x0600068E RID: 1678 RVA: 0x0001D5C1 File Offset: 0x0001B7C1
	// (set) Token: 0x0600068F RID: 1679 RVA: 0x0001D5C9 File Offset: 0x0001B7C9
	public string Sprite
	{
		get
		{
			return this.sprite;
		}
		set
		{
			if (value != this.sprite)
			{
				this.sprite = value;
				this.glyphMap = null;
			}
		}
	}

	// Token: 0x1700018F RID: 399
	// (get) Token: 0x06000690 RID: 1680 RVA: 0x0001D5E7 File Offset: 0x0001B7E7
	public override bool IsValid
	{
		get
		{
			return !(this.Atlas == null) && !(this.Atlas[this.Sprite] == null);
		}
	}

	// Token: 0x17000190 RID: 400
	// (get) Token: 0x06000691 RID: 1681 RVA: 0x0001D613 File Offset: 0x0001B813
	public string FontFace
	{
		get
		{
			return this.face;
		}
	}

	// Token: 0x17000191 RID: 401
	// (get) Token: 0x06000692 RID: 1682 RVA: 0x0001D61B File Offset: 0x0001B81B
	// (set) Token: 0x06000693 RID: 1683 RVA: 0x0001D623 File Offset: 0x0001B823
	public override int FontSize
	{
		get
		{
			return this.size;
		}
		set
		{
			throw new InvalidOperationException();
		}
	}

	// Token: 0x17000192 RID: 402
	// (get) Token: 0x06000694 RID: 1684 RVA: 0x0001D62A File Offset: 0x0001B82A
	// (set) Token: 0x06000695 RID: 1685 RVA: 0x0001D632 File Offset: 0x0001B832
	public override int LineHeight
	{
		get
		{
			return this.lineHeight;
		}
		set
		{
			throw new InvalidOperationException();
		}
	}

	// Token: 0x17000193 RID: 403
	// (get) Token: 0x06000696 RID: 1686 RVA: 0x0001D639 File Offset: 0x0001B839
	public bool Bold
	{
		get
		{
			return this.bold;
		}
	}

	// Token: 0x17000194 RID: 404
	// (get) Token: 0x06000697 RID: 1687 RVA: 0x0001D641 File Offset: 0x0001B841
	public bool Italic
	{
		get
		{
			return this.italic;
		}
	}

	// Token: 0x17000195 RID: 405
	// (get) Token: 0x06000698 RID: 1688 RVA: 0x0001D649 File Offset: 0x0001B849
	public int[] Padding
	{
		get
		{
			return this.padding;
		}
	}

	// Token: 0x17000196 RID: 406
	// (get) Token: 0x06000699 RID: 1689 RVA: 0x0001D651 File Offset: 0x0001B851
	public int[] Spacing
	{
		get
		{
			return this.spacing;
		}
	}

	// Token: 0x17000197 RID: 407
	// (get) Token: 0x0600069A RID: 1690 RVA: 0x0001D659 File Offset: 0x0001B859
	public int Outline
	{
		get
		{
			return this.outline;
		}
	}

	// Token: 0x17000198 RID: 408
	// (get) Token: 0x0600069B RID: 1691 RVA: 0x0001D661 File Offset: 0x0001B861
	public int Count
	{
		get
		{
			return this.glyphs.Count;
		}
	}

	// Token: 0x0600069C RID: 1692 RVA: 0x0001D66E File Offset: 0x0001B86E
	public void OnEnable()
	{
		this.glyphMap = null;
	}

	// Token: 0x0600069D RID: 1693 RVA: 0x0001D677 File Offset: 0x0001B877
	public override dfFontRendererBase ObtainRenderer()
	{
		return dfFont.BitmappedFontRenderer.Obtain(this);
	}

	// Token: 0x0600069E RID: 1694 RVA: 0x0001D67F File Offset: 0x0001B87F
	public void AddKerning(int first, int second, int amount)
	{
		this.kerning.Add(new dfFont.GlyphKerning
		{
			first = first,
			second = second,
			amount = amount
		});
	}

	// Token: 0x0600069F RID: 1695 RVA: 0x0001D6A8 File Offset: 0x0001B8A8
	public int GetKerning(char previousChar, char currentChar)
	{
		if (this.kerningMap == null)
		{
			this.buildKerningMap();
		}
		dfFont.GlyphKerningList glyphKerningList = null;
		if (!this.kerningMap.TryGetValue((int)previousChar, out glyphKerningList))
		{
			return 0;
		}
		return glyphKerningList.GetKerning((int)previousChar, (int)currentChar);
	}

	// Token: 0x060006A0 RID: 1696 RVA: 0x0001D6E0 File Offset: 0x0001B8E0
	private void buildKerningMap()
	{
		Dictionary<int, dfFont.GlyphKerningList> dictionary = this.kerningMap = new Dictionary<int, dfFont.GlyphKerningList>();
		for (int i = 0; i < this.kerning.Count; i++)
		{
			dfFont.GlyphKerning glyphKerning = this.kerning[i];
			if (!dictionary.ContainsKey(glyphKerning.first))
			{
				dictionary[glyphKerning.first] = new dfFont.GlyphKerningList();
			}
			dictionary[glyphKerning.first].Add(glyphKerning);
		}
	}

	// Token: 0x060006A1 RID: 1697 RVA: 0x0001D750 File Offset: 0x0001B950
	public dfFont.GlyphDefinition GetGlyph(char id)
	{
		if (this.glyphMap == null)
		{
			this.glyphMap = new Dictionary<int, dfFont.GlyphDefinition>();
			for (int i = 0; i < this.glyphs.Count; i++)
			{
				dfFont.GlyphDefinition glyphDefinition = this.glyphs[i];
				this.glyphMap[glyphDefinition.id] = glyphDefinition;
			}
		}
		dfFont.GlyphDefinition result = null;
		this.glyphMap.TryGetValue((int)id, out result);
		return result;
	}

	// Token: 0x04000287 RID: 647
	[SerializeField]
	protected dfAtlas atlas;

	// Token: 0x04000288 RID: 648
	[SerializeField]
	protected string sprite;

	// Token: 0x04000289 RID: 649
	[SerializeField]
	protected string face = "";

	// Token: 0x0400028A RID: 650
	[SerializeField]
	protected int size;

	// Token: 0x0400028B RID: 651
	[SerializeField]
	protected bool bold;

	// Token: 0x0400028C RID: 652
	[SerializeField]
	protected bool italic;

	// Token: 0x0400028D RID: 653
	[SerializeField]
	protected string charset;

	// Token: 0x0400028E RID: 654
	[SerializeField]
	protected int stretchH;

	// Token: 0x0400028F RID: 655
	[SerializeField]
	protected bool smooth;

	// Token: 0x04000290 RID: 656
	[SerializeField]
	protected int aa;

	// Token: 0x04000291 RID: 657
	[SerializeField]
	protected int[] padding;

	// Token: 0x04000292 RID: 658
	[SerializeField]
	protected int[] spacing;

	// Token: 0x04000293 RID: 659
	[SerializeField]
	protected int outline;

	// Token: 0x04000294 RID: 660
	[SerializeField]
	protected int lineHeight;

	// Token: 0x04000295 RID: 661
	[SerializeField]
	private List<dfFont.GlyphDefinition> glyphs = new List<dfFont.GlyphDefinition>();

	// Token: 0x04000296 RID: 662
	[SerializeField]
	protected List<dfFont.GlyphKerning> kerning = new List<dfFont.GlyphKerning>();

	// Token: 0x04000297 RID: 663
	private Dictionary<int, dfFont.GlyphDefinition> glyphMap;

	// Token: 0x04000298 RID: 664
	private Dictionary<int, dfFont.GlyphKerningList> kerningMap;

	// Token: 0x0200036C RID: 876
	private class GlyphKerningList
	{
		// Token: 0x06001C90 RID: 7312 RVA: 0x0007AC58 File Offset: 0x00078E58
		public void Add(dfFont.GlyphKerning kerning)
		{
			this.list[kerning.second] = kerning.amount;
		}

		// Token: 0x06001C91 RID: 7313 RVA: 0x0007AC74 File Offset: 0x00078E74
		public int GetKerning(int firstCharacter, int secondCharacter)
		{
			int result = 0;
			this.list.TryGetValue(secondCharacter, out result);
			return result;
		}

		// Token: 0x04001644 RID: 5700
		private Dictionary<int, int> list = new Dictionary<int, int>();
	}

	// Token: 0x0200036D RID: 877
	[Serializable]
	public class GlyphKerning : IComparable<dfFont.GlyphKerning>
	{
		// Token: 0x06001C93 RID: 7315 RVA: 0x0007ACA6 File Offset: 0x00078EA6
		public int CompareTo(dfFont.GlyphKerning other)
		{
			if (this.first == other.first)
			{
				return this.second.CompareTo(other.second);
			}
			return this.first.CompareTo(other.first);
		}

		// Token: 0x04001645 RID: 5701
		public int first;

		// Token: 0x04001646 RID: 5702
		public int second;

		// Token: 0x04001647 RID: 5703
		public int amount;
	}

	// Token: 0x0200036E RID: 878
	[Serializable]
	public class GlyphDefinition : IComparable<dfFont.GlyphDefinition>
	{
		// Token: 0x06001C95 RID: 7317 RVA: 0x0007ACE1 File Offset: 0x00078EE1
		public int CompareTo(dfFont.GlyphDefinition other)
		{
			return this.id.CompareTo(other.id);
		}

		// Token: 0x04001648 RID: 5704
		[SerializeField]
		public int id;

		// Token: 0x04001649 RID: 5705
		[SerializeField]
		public int x;

		// Token: 0x0400164A RID: 5706
		[SerializeField]
		public int y;

		// Token: 0x0400164B RID: 5707
		[SerializeField]
		public int width;

		// Token: 0x0400164C RID: 5708
		[SerializeField]
		public int height;

		// Token: 0x0400164D RID: 5709
		[SerializeField]
		public int xoffset;

		// Token: 0x0400164E RID: 5710
		[SerializeField]
		public int yoffset;

		// Token: 0x0400164F RID: 5711
		[SerializeField]
		public int xadvance;

		// Token: 0x04001650 RID: 5712
		[SerializeField]
		public bool rotated;
	}

	// Token: 0x0200036F RID: 879
	public class BitmappedFontRenderer : dfFontRendererBase, IPoolable
	{
		// Token: 0x17000506 RID: 1286
		// (get) Token: 0x06001C97 RID: 7319 RVA: 0x0007ACFC File Offset: 0x00078EFC
		public int LineCount
		{
			get
			{
				return this.lines.Count;
			}
		}

		// Token: 0x06001C98 RID: 7320 RVA: 0x0007AD09 File Offset: 0x00078F09
		internal BitmappedFontRenderer()
		{
		}

		// Token: 0x06001C99 RID: 7321 RVA: 0x0007AD11 File Offset: 0x00078F11
		public static dfFontRendererBase Obtain(dfFont font)
		{
			dfFont.BitmappedFontRenderer bitmappedFontRenderer = (dfFont.BitmappedFontRenderer.objectPool.Count > 0) ? dfFont.BitmappedFontRenderer.objectPool.Dequeue() : new dfFont.BitmappedFontRenderer();
			bitmappedFontRenderer.Reset();
			bitmappedFontRenderer.Font = font;
			return bitmappedFontRenderer;
		}

		// Token: 0x06001C9A RID: 7322 RVA: 0x0007AD40 File Offset: 0x00078F40
		public override void Release()
		{
			this.Reset();
			if (this.tokens != null)
			{
				this.tokens.ReleaseItems();
				this.tokens.Release();
			}
			this.tokens = null;
			if (this.lines != null)
			{
				this.lines.Release();
				this.lines = null;
			}
			dfFont.LineRenderInfo.ResetPool();
			base.BottomColor = null;
			dfFont.BitmappedFontRenderer.objectPool.Enqueue(this);
		}

		// Token: 0x06001C9B RID: 7323 RVA: 0x0007ADB4 File Offset: 0x00078FB4
		public override float[] GetCharacterWidths(string text)
		{
			float num = 0f;
			return this.GetCharacterWidths(text, 0, text.Length - 1, out num);
		}

		// Token: 0x06001C9C RID: 7324 RVA: 0x0007ADDC File Offset: 0x00078FDC
		public float[] GetCharacterWidths(string text, int startIndex, int endIndex, out float totalWidth)
		{
			totalWidth = 0f;
			dfFont dfFont = (dfFont)base.Font;
			float[] array = new float[text.Length];
			float num = base.TextScale * base.PixelRatio;
			float num2 = (float)base.CharacterSpacing * num;
			for (int i = startIndex; i <= endIndex; i++)
			{
				dfFont.GlyphDefinition glyph = dfFont.GetGlyph(text[i]);
				if (glyph != null)
				{
					if (i > 0)
					{
						array[i - 1] += num2;
						totalWidth += num2;
					}
					float num3 = (float)glyph.xadvance * num;
					array[i] = num3;
					totalWidth += num3;
				}
			}
			return array;
		}

		// Token: 0x06001C9D RID: 7325 RVA: 0x0007AE80 File Offset: 0x00079080
		public override Vector2 MeasureString(string text)
		{
			this.tokenize(text);
			dfList<dfFont.LineRenderInfo> dfList = this.calculateLinebreaks();
			int num = 0;
			int num2 = 0;
			for (int i = 0; i < dfList.Count; i++)
			{
				num = Mathf.Max((int)dfList[i].lineWidth, num);
				num2 += (int)dfList[i].lineHeight;
			}
			return new Vector2((float)num, (float)num2) * base.TextScale;
		}

		// Token: 0x06001C9E RID: 7326 RVA: 0x0007AEEC File Offset: 0x000790EC
		public override void Render(string text, dfRenderData destination)
		{
			dfFont.BitmappedFontRenderer.textColors.Clear();
			dfFont.BitmappedFontRenderer.textColors.Push(Color.white);
			this.tokenize(text);
			dfList<dfFont.LineRenderInfo> dfList = this.calculateLinebreaks();
			destination.EnsureCapacity(this.getAnticipatedVertCount(this.tokens));
			int num = 0;
			int num2 = 0;
			Vector3 vectorOffset = base.VectorOffset;
			float num3 = base.TextScale * base.PixelRatio;
			for (int i = 0; i < dfList.Count; i++)
			{
				dfFont.LineRenderInfo lineRenderInfo = dfList[i];
				int count = destination.Vertices.Count;
				this.renderLine(dfList[i], dfFont.BitmappedFontRenderer.textColors, vectorOffset, destination);
				vectorOffset.y -= (float)base.Font.LineHeight * num3;
				num = Mathf.Max((int)lineRenderInfo.lineWidth, num);
				num2 += (int)lineRenderInfo.lineHeight;
				if (lineRenderInfo.lineWidth * base.TextScale > base.MaxSize.x)
				{
					this.clipRight(destination, count);
				}
				if ((float)num2 * base.TextScale > base.MaxSize.y)
				{
					this.clipBottom(destination, count);
				}
			}
			base.RenderedSize = new Vector2(Mathf.Min(base.MaxSize.x, (float)num), Mathf.Min(base.MaxSize.y, (float)num2)) * base.TextScale;
		}

		// Token: 0x06001C9F RID: 7327 RVA: 0x0007B04C File Offset: 0x0007924C
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

		// Token: 0x06001CA0 RID: 7328 RVA: 0x0007B0B4 File Offset: 0x000792B4
		private void renderLine(dfFont.LineRenderInfo line, Stack<Color32> colors, Vector3 position, dfRenderData destination)
		{
			float num = base.TextScale * base.PixelRatio;
			position.x += (float)this.calculateLineAlignment(line) * num;
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
					if (dfMarkupToken.Matches("sprite"))
					{
						this.renderSprite(dfMarkupToken, colors.Peek(), position, destination);
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
				position.x += (float)dfMarkupToken.Width * num;
			}
		}

		// Token: 0x06001CA1 RID: 7329 RVA: 0x0007B1A0 File Offset: 0x000793A0
		private void renderText(dfMarkupToken token, Color32 color, Vector3 position, dfRenderData destination)
		{
			dfList<Vector3> vertices = destination.Vertices;
			dfList<int> triangles = destination.Triangles;
			dfList<Color32> colors = destination.Colors;
			dfList<Vector2> uv = destination.UV;
			dfFont dfFont = (dfFont)base.Font;
			dfAtlas.ItemInfo itemInfo = dfFont.Atlas[dfFont.sprite];
			Texture texture = dfFont.Texture;
			float num = 1f / (float)texture.width;
			float num2 = 1f / (float)texture.height;
			float num3 = num * 0.125f;
			float num4 = num2 * 0.125f;
			float num5 = base.TextScale * base.PixelRatio;
			char previousChar = '\0';
			Color32 color2 = this.applyOpacity(this.multiplyColors(color, base.DefaultColor));
			Color32 item = color2;
			if (base.BottomColor != null)
			{
				item = this.applyOpacity(this.multiplyColors(color, base.BottomColor.Value));
			}
			int i = 0;
			while (i < token.Length)
			{
				char c = token[i];
				if (c != '\0')
				{
					dfFont.GlyphDefinition glyph = dfFont.GetGlyph(c);
					if (glyph != null)
					{
						int kerning = dfFont.GetKerning(previousChar, c);
						float num6 = position.x + (float)(glyph.xoffset + kerning) * num5;
						float num7 = position.y - (float)glyph.yoffset * num5;
						float num8 = (float)glyph.width * num5;
						float num9 = (float)glyph.height * num5;
						float x = num6 + num8;
						float y = num7 - num9;
						Vector3 vector = new Vector3(num6, num7);
						Vector3 vector2 = new Vector3(x, num7);
						Vector3 vector3 = new Vector3(x, y);
						Vector3 vector4 = new Vector3(num6, y);
						float num10 = itemInfo.region.x + (float)glyph.x * num - num3;
						float num11 = itemInfo.region.yMax - (float)glyph.y * num2 - num4;
						float x2 = num10 + (float)glyph.width * num - num3;
						float y2 = num11 - (float)glyph.height * num2 + num4;
						if (base.Shadow)
						{
							dfFont.BitmappedFontRenderer.addTriangleIndices(vertices, triangles);
							Vector3 b = base.ShadowOffset * num5;
							vertices.Add(vector + b);
							vertices.Add(vector2 + b);
							vertices.Add(vector3 + b);
							vertices.Add(vector4 + b);
							Color32 item2 = this.applyOpacity(base.ShadowColor);
							colors.Add(item2);
							colors.Add(item2);
							colors.Add(item2);
							colors.Add(item2);
							uv.Add(new Vector2(num10, num11));
							uv.Add(new Vector2(x2, num11));
							uv.Add(new Vector2(x2, y2));
							uv.Add(new Vector2(num10, y2));
						}
						if (base.Outline)
						{
							for (int j = 0; j < dfFont.BitmappedFontRenderer.OUTLINE_OFFSETS.Length; j++)
							{
								dfFont.BitmappedFontRenderer.addTriangleIndices(vertices, triangles);
								Vector3 b2 = dfFont.BitmappedFontRenderer.OUTLINE_OFFSETS[j] * (float)base.OutlineSize * num5;
								vertices.Add(vector + b2);
								vertices.Add(vector2 + b2);
								vertices.Add(vector3 + b2);
								vertices.Add(vector4 + b2);
								Color32 item3 = this.applyOpacity(base.OutlineColor);
								colors.Add(item3);
								colors.Add(item3);
								colors.Add(item3);
								colors.Add(item3);
								uv.Add(new Vector2(num10, num11));
								uv.Add(new Vector2(x2, num11));
								uv.Add(new Vector2(x2, y2));
								uv.Add(new Vector2(num10, y2));
							}
						}
						dfFont.BitmappedFontRenderer.addTriangleIndices(vertices, triangles);
						vertices.Add(vector);
						vertices.Add(vector2);
						vertices.Add(vector3);
						vertices.Add(vector4);
						colors.Add(color2);
						colors.Add(color2);
						colors.Add(item);
						colors.Add(item);
						uv.Add(new Vector2(num10, num11));
						uv.Add(new Vector2(x2, num11));
						uv.Add(new Vector2(x2, y2));
						uv.Add(new Vector2(num10, y2));
						position.x += (float)(glyph.xadvance + kerning + base.CharacterSpacing) * num5;
					}
				}
				i++;
				previousChar = c;
			}
		}

		// Token: 0x06001CA2 RID: 7330 RVA: 0x0007B640 File Offset: 0x00079840
		private void renderSprite(dfMarkupToken token, Color32 color, Vector3 position, dfRenderData destination)
		{
			dfList<Vector3> vertices = destination.Vertices;
			dfList<int> triangles = destination.Triangles;
			dfList<Color32> colors = destination.Colors;
			dfList<Vector2> uv = destination.UV;
			dfFont dfFont = (dfFont)base.Font;
			string value = token.GetAttribute(0).Value.Value;
			dfAtlas.ItemInfo itemInfo = dfFont.Atlas[value];
			if (itemInfo == null)
			{
				return;
			}
			float num = (float)token.Height * base.TextScale * base.PixelRatio;
			float num2 = (float)token.Width * base.TextScale * base.PixelRatio;
			float x = position.x;
			float y = position.y;
			int count = vertices.Count;
			vertices.Add(new Vector3(x, y));
			vertices.Add(new Vector3(x + num2, y));
			vertices.Add(new Vector3(x + num2, y - num));
			vertices.Add(new Vector3(x, y - num));
			triangles.Add(count);
			triangles.Add(count + 1);
			triangles.Add(count + 3);
			triangles.Add(count + 3);
			triangles.Add(count + 1);
			triangles.Add(count + 2);
			Color32 item = base.ColorizeSymbols ? this.applyOpacity(color) : this.applyOpacity(base.DefaultColor);
			colors.Add(item);
			colors.Add(item);
			colors.Add(item);
			colors.Add(item);
			Rect region = itemInfo.region;
			uv.Add(new Vector2(region.x, region.yMax));
			uv.Add(new Vector2(region.xMax, region.yMax));
			uv.Add(new Vector2(region.xMax, region.y));
			uv.Add(new Vector2(region.x, region.y));
		}

		// Token: 0x06001CA3 RID: 7331 RVA: 0x0007B818 File Offset: 0x00079A18
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

		// Token: 0x06001CA4 RID: 7332 RVA: 0x0007B8A4 File Offset: 0x00079AA4
		private Color32 UIntToColor(uint color)
		{
			byte a = (byte)(color >> 24);
			byte r = (byte)(color >> 16);
			byte g = (byte)(color >> 8);
			byte b = (byte)color;
			return new Color32(r, g, b, a);
		}

		// Token: 0x06001CA5 RID: 7333 RVA: 0x0007B8CC File Offset: 0x00079ACC
		private dfList<dfFont.LineRenderInfo> calculateLinebreaks()
		{
			if (this.lines != null)
			{
				return this.lines;
			}
			this.lines = dfList<dfFont.LineRenderInfo>.Obtain();
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			int num4 = 0;
			float num5 = (float)base.Font.LineHeight * base.TextScale;
			while (num3 < this.tokens.Count && (float)this.lines.Count * num5 < base.MaxSize.y)
			{
				dfMarkupToken dfMarkupToken = this.tokens[num3];
				dfMarkupTokenType tokenType = dfMarkupToken.TokenType;
				if (tokenType == dfMarkupTokenType.Newline)
				{
					this.lines.Add(dfFont.LineRenderInfo.Obtain(num2, num3));
					num = (num2 = ++num3);
					num4 = 0;
				}
				else
				{
					int num6 = Mathf.CeilToInt((float)dfMarkupToken.Width * base.TextScale);
					if (base.WordWrap && num > num2 && (tokenType == dfMarkupTokenType.Text || (tokenType == dfMarkupTokenType.StartTag && dfMarkupToken.Matches("sprite"))) && (float)(num4 + num6) >= base.MaxSize.x)
					{
						if (num > num2)
						{
							this.lines.Add(dfFont.LineRenderInfo.Obtain(num2, num - 1));
							num3 = (num2 = ++num);
							num4 = 0;
						}
						else
						{
							this.lines.Add(dfFont.LineRenderInfo.Obtain(num2, num - 1));
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
				this.lines.Add(dfFont.LineRenderInfo.Obtain(num2, this.tokens.Count - 1));
			}
			for (int i = 0; i < this.lines.Count; i++)
			{
				this.calculateLineSize(this.lines[i]);
			}
			return this.lines;
		}

		// Token: 0x06001CA6 RID: 7334 RVA: 0x0007BA88 File Offset: 0x00079C88
		private int calculateLineAlignment(dfFont.LineRenderInfo line)
		{
			float lineWidth = line.lineWidth;
			if (base.TextAlign == TextAlignment.Left || lineWidth == 0f)
			{
				return 0;
			}
			int b;
			if (base.TextAlign == TextAlignment.Right)
			{
				b = Mathf.FloorToInt(base.MaxSize.x / base.TextScale - lineWidth);
			}
			else
			{
				b = Mathf.FloorToInt((base.MaxSize.x / base.TextScale - lineWidth) * 0.5f);
			}
			return Mathf.Max(0, b);
		}

		// Token: 0x06001CA7 RID: 7335 RVA: 0x0007BAFC File Offset: 0x00079CFC
		private void calculateLineSize(dfFont.LineRenderInfo line)
		{
			line.lineHeight = (float)base.Font.LineHeight;
			int num = 0;
			for (int i = line.startOffset; i <= line.endOffset; i++)
			{
				num += this.tokens[i].Width;
			}
			line.lineWidth = (float)num;
		}

		// Token: 0x06001CA8 RID: 7336 RVA: 0x0007BB50 File Offset: 0x00079D50
		private dfList<dfMarkupToken> tokenize(string text)
		{
			if (this.tokens != null)
			{
				if (this.tokens[0].Source == text)
				{
					return this.tokens;
				}
				this.tokens.ReleaseItems();
				this.tokens.Release();
			}
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
			return this.tokens;
		}

		// Token: 0x06001CA9 RID: 7337 RVA: 0x0007BBE8 File Offset: 0x00079DE8
		private void calculateTokenRenderSize(dfMarkupToken token)
		{
			dfFont dfFont = (dfFont)base.Font;
			int num = 0;
			char previousChar = '\0';
			if (token.TokenType == dfMarkupTokenType.Whitespace || token.TokenType == dfMarkupTokenType.Text)
			{
				int i = 0;
				while (i < token.Length)
				{
					char c = token[i];
					if (c == '\t')
					{
						num += base.TabSize;
					}
					else
					{
						dfFont.GlyphDefinition glyph = dfFont.GetGlyph(c);
						if (glyph != null)
						{
							if (i > 0)
							{
								num += dfFont.GetKerning(previousChar, c);
								num += base.CharacterSpacing;
							}
							num += glyph.xadvance;
						}
					}
					i++;
					previousChar = c;
				}
			}
			else if (token.TokenType == dfMarkupTokenType.StartTag && token.Matches("sprite"))
			{
				if (token.AttributeCount < 1)
				{
					throw new Exception("Missing sprite name in markup");
				}
				Texture texture = dfFont.Texture;
				int lineHeight = dfFont.LineHeight;
				string value = token.GetAttribute(0).Value.Value;
				dfAtlas.ItemInfo itemInfo = dfFont.atlas[value];
				if (itemInfo != null)
				{
					float num2 = itemInfo.region.width * (float)texture.width / (itemInfo.region.height * (float)texture.height);
					num = Mathf.CeilToInt((float)lineHeight * num2);
				}
			}
			token.Height = base.Font.LineHeight;
			token.Width = num;
		}

		// Token: 0x06001CAA RID: 7338 RVA: 0x0007BD40 File Offset: 0x00079F40
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

		// Token: 0x06001CAB RID: 7339 RVA: 0x0007BDD0 File Offset: 0x00079FD0
		private void clipRight(dfRenderData destination, int startIndex)
		{
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

		// Token: 0x06001CAC RID: 7340 RVA: 0x0007BFA4 File Offset: 0x0007A1A4
		private void clipBottom(dfRenderData destination, int startIndex)
		{
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
					float y = Mathf.Lerp(uv[i + 3].y, uv[i].y, t);
					uv[i + 3] = new Vector2(uv[i + 3].x, y);
					uv[i + 2] = new Vector2(uv[i + 2].x, y);
					Color c = Color.Lerp(colors[i + 3], colors[i], t);
					colors[i + 3] = c;
					colors[i + 2] = c;
				}
			}
		}

		// Token: 0x06001CAD RID: 7341 RVA: 0x0007C1C8 File Offset: 0x0007A3C8
		private Color32 applyOpacity(Color32 color)
		{
			color.a = (byte)(base.Opacity * 255f);
			return color;
		}

		// Token: 0x06001CAE RID: 7342 RVA: 0x0007C1E0 File Offset: 0x0007A3E0
		private static void addTriangleIndices(dfList<Vector3> verts, dfList<int> triangles)
		{
			int count = verts.Count;
			for (int i = 0; i < dfFont.BitmappedFontRenderer.TRIANGLE_INDICES.Length; i++)
			{
				triangles.Add(count + dfFont.BitmappedFontRenderer.TRIANGLE_INDICES[i]);
			}
		}

		// Token: 0x06001CAF RID: 7343 RVA: 0x0007C215 File Offset: 0x0007A415
		private Color multiplyColors(Color lhs, Color rhs)
		{
			return new Color(lhs.r * rhs.r, lhs.g * rhs.g, lhs.b * rhs.b, lhs.a * rhs.a);
		}

		// Token: 0x04001651 RID: 5713
		private static Queue<dfFont.BitmappedFontRenderer> objectPool = new Queue<dfFont.BitmappedFontRenderer>();

		// Token: 0x04001652 RID: 5714
		private static Vector2[] OUTLINE_OFFSETS = new Vector2[]
		{
			new Vector2(-1f, -1f),
			new Vector2(-1f, 1f),
			new Vector2(1f, -1f),
			new Vector2(1f, 1f)
		};

		// Token: 0x04001653 RID: 5715
		private static int[] TRIANGLE_INDICES = new int[]
		{
			0,
			1,
			3,
			3,
			1,
			2
		};

		// Token: 0x04001654 RID: 5716
		private static Stack<Color32> textColors = new Stack<Color32>();

		// Token: 0x04001655 RID: 5717
		private dfList<dfFont.LineRenderInfo> lines;

		// Token: 0x04001656 RID: 5718
		private dfList<dfMarkupToken> tokens;
	}

	// Token: 0x02000370 RID: 880
	private class LineRenderInfo
	{
		// Token: 0x17000507 RID: 1287
		// (get) Token: 0x06001CB1 RID: 7345 RVA: 0x0007C2EA File Offset: 0x0007A4EA
		public int length
		{
			get
			{
				return this.endOffset - this.startOffset + 1;
			}
		}

		// Token: 0x06001CB2 RID: 7346 RVA: 0x0007C2FB File Offset: 0x0007A4FB
		private LineRenderInfo()
		{
		}

		// Token: 0x06001CB3 RID: 7347 RVA: 0x0007C303 File Offset: 0x0007A503
		public static void ResetPool()
		{
			dfFont.LineRenderInfo.poolIndex = 0;
		}

		// Token: 0x06001CB4 RID: 7348 RVA: 0x0007C30C File Offset: 0x0007A50C
		public static dfFont.LineRenderInfo Obtain(int start, int end)
		{
			if (dfFont.LineRenderInfo.poolIndex >= dfFont.LineRenderInfo.pool.Count - 1)
			{
				dfFont.LineRenderInfo.pool.Add(new dfFont.LineRenderInfo());
			}
			dfFont.LineRenderInfo lineRenderInfo = dfFont.LineRenderInfo.pool[dfFont.LineRenderInfo.poolIndex++];
			lineRenderInfo.startOffset = start;
			lineRenderInfo.endOffset = end;
			lineRenderInfo.lineHeight = 0f;
			return lineRenderInfo;
		}

		// Token: 0x04001657 RID: 5719
		public int startOffset;

		// Token: 0x04001658 RID: 5720
		public int endOffset;

		// Token: 0x04001659 RID: 5721
		public float lineWidth;

		// Token: 0x0400165A RID: 5722
		public float lineHeight;

		// Token: 0x0400165B RID: 5723
		private static dfList<dfFont.LineRenderInfo> pool = new dfList<dfFont.LineRenderInfo>();

		// Token: 0x0400165C RID: 5724
		private static int poolIndex = 0;
	}
}

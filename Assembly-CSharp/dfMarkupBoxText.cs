using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

// Token: 0x02000083 RID: 131
public class dfMarkupBoxText : dfMarkupBox
{
	// Token: 0x170001E9 RID: 489
	// (get) Token: 0x06000872 RID: 2162 RVA: 0x0002575F File Offset: 0x0002395F
	// (set) Token: 0x06000873 RID: 2163 RVA: 0x00025767 File Offset: 0x00023967
	public string Text { get; private set; }

	// Token: 0x170001EA RID: 490
	// (get) Token: 0x06000874 RID: 2164 RVA: 0x00025770 File Offset: 0x00023970
	public bool IsWhitespace
	{
		get
		{
			return this.isWhitespace;
		}
	}

	// Token: 0x06000875 RID: 2165 RVA: 0x00025778 File Offset: 0x00023978
	public dfMarkupBoxText(dfMarkupElement element, dfMarkupDisplayType display, dfMarkupStyle style) : base(element, display, style)
	{
	}

	// Token: 0x06000876 RID: 2166 RVA: 0x00025790 File Offset: 0x00023990
	public static dfMarkupBoxText Obtain(dfMarkupElement element, dfMarkupDisplayType display, dfMarkupStyle style)
	{
		if (dfMarkupBoxText.objectPool.Count > 0)
		{
			dfMarkupBoxText dfMarkupBoxText = dfMarkupBoxText.objectPool.Dequeue();
			dfMarkupBoxText.Element = element;
			dfMarkupBoxText.Display = display;
			dfMarkupBoxText.Style = style;
			dfMarkupBoxText.Position = Vector2.zero;
			dfMarkupBoxText.Size = Vector2.zero;
			dfMarkupBoxText.Baseline = (int)((float)style.FontSize * 1.1f);
			dfMarkupBoxText.Margins = default(dfMarkupBorders);
			dfMarkupBoxText.Padding = default(dfMarkupBorders);
			return dfMarkupBoxText;
		}
		return new dfMarkupBoxText(element, display, style);
	}

	// Token: 0x06000877 RID: 2167 RVA: 0x00025814 File Offset: 0x00023A14
	public override void Release()
	{
		base.Release();
		this.Text = "";
		this.renderData.Clear();
		dfMarkupBoxText.objectPool.Enqueue(this);
	}

	// Token: 0x06000878 RID: 2168 RVA: 0x00025840 File Offset: 0x00023A40
	internal void SetText(string text)
	{
		this.Text = text;
		if (this.Style.Font == null)
		{
			return;
		}
		this.isWhitespace = dfMarkupBoxText.whitespacePattern.IsMatch(this.Text);
		string text2 = (this.Style.PreserveWhitespace || !this.isWhitespace) ? this.Text : " ";
		int fontSize = this.Style.FontSize;
		Vector2 size = new Vector2(0f, (float)this.Style.LineHeight);
		this.Style.Font.RequestCharacters(text2, this.Style.FontSize, this.Style.FontStyle);
		CharacterInfo characterInfo = default(CharacterInfo);
		for (int i = 0; i < text2.Length; i++)
		{
			if (this.Style.Font.BaseFont.GetCharacterInfo(text2[i], out characterInfo, fontSize, this.Style.FontStyle))
			{
				float num = characterInfo.vert.x + characterInfo.vert.width;
				if (text2[i] == ' ')
				{
					num = Mathf.Max(num, (float)fontSize * 0.33f);
				}
				else if (text2[i] == '\t')
				{
					num += (float)(fontSize * 3);
				}
				size.x += num;
			}
		}
		this.Size = size;
		dfDynamicFont font = this.Style.Font;
		float num2 = (float)fontSize / (float)font.FontSize;
		this.Baseline = Mathf.CeilToInt((float)font.Baseline * num2);
	}

	// Token: 0x06000879 RID: 2169 RVA: 0x000259D0 File Offset: 0x00023BD0
	protected override dfRenderData OnRebuildRenderData()
	{
		this.renderData.Clear();
		if (this.Style.Font == null)
		{
			return null;
		}
		if (this.Style.TextDecoration == dfMarkupTextDecoration.Underline)
		{
			this.renderUnderline();
		}
		this.renderText(this.Text);
		return this.renderData;
	}

	// Token: 0x0600087A RID: 2170 RVA: 0x00025A23 File Offset: 0x00023C23
	private void renderUnderline()
	{
	}

	// Token: 0x0600087B RID: 2171 RVA: 0x00025A28 File Offset: 0x00023C28
	private void renderText(string text)
	{
		dfDynamicFont font = this.Style.Font;
		int fontSize = this.Style.FontSize;
		FontStyle fontStyle = this.Style.FontStyle;
		CharacterInfo characterInfo = default(CharacterInfo);
		dfList<Vector3> vertices = this.renderData.Vertices;
		dfList<int> triangles = this.renderData.Triangles;
		dfList<Vector2> uv = this.renderData.UV;
		dfList<Color32> colors = this.renderData.Colors;
		float num = (float)fontSize / (float)font.FontSize;
		float num2 = (float)font.Descent * num;
		float num3 = 0f;
		font.RequestCharacters(text, fontSize, fontStyle);
		this.renderData.Material = font.Material;
		for (int i = 0; i < text.Length; i++)
		{
			if (font.BaseFont.GetCharacterInfo(text[i], out characterInfo, fontSize, fontStyle))
			{
				dfMarkupBoxText.addTriangleIndices(vertices, triangles);
				float num4 = (float)font.FontSize + characterInfo.vert.y - (float)fontSize + num2;
				float num5 = num3 + characterInfo.vert.x;
				float num6 = num4;
				float x = num5 + characterInfo.vert.width;
				float y = num6 + characterInfo.vert.height;
				Vector3 item = new Vector3(num5, num6);
				Vector3 item2 = new Vector3(x, num6);
				Vector3 item3 = new Vector3(x, y);
				Vector3 item4 = new Vector3(num5, y);
				vertices.Add(item);
				vertices.Add(item2);
				vertices.Add(item3);
				vertices.Add(item4);
				Color color = this.Style.Color;
				colors.Add(color);
				colors.Add(color);
				colors.Add(color);
				colors.Add(color);
				Rect uv2 = characterInfo.uv;
				float x2 = uv2.x;
				float y2 = uv2.y + uv2.height;
				float x3 = x2 + uv2.width;
				float y3 = uv2.y;
				if (characterInfo.flipped)
				{
					uv.Add(new Vector2(x3, y3));
					uv.Add(new Vector2(x3, y2));
					uv.Add(new Vector2(x2, y2));
					uv.Add(new Vector2(x2, y3));
				}
				else
				{
					uv.Add(new Vector2(x2, y2));
					uv.Add(new Vector2(x3, y2));
					uv.Add(new Vector2(x3, y3));
					uv.Add(new Vector2(x2, y3));
				}
				num3 += (float)Mathf.CeilToInt(characterInfo.vert.x + characterInfo.vert.width);
			}
		}
	}

	// Token: 0x0600087C RID: 2172 RVA: 0x00025CD8 File Offset: 0x00023ED8
	private static void addTriangleIndices(dfList<Vector3> verts, dfList<int> triangles)
	{
		int count = verts.Count;
		int[] triangle_INDICES = dfMarkupBoxText.TRIANGLE_INDICES;
		for (int i = 0; i < triangle_INDICES.Length; i++)
		{
			triangles.Add(count + triangle_INDICES[i]);
		}
	}

	// Token: 0x0400040D RID: 1037
	private static int[] TRIANGLE_INDICES = new int[]
	{
		0,
		1,
		2,
		0,
		2,
		3
	};

	// Token: 0x0400040E RID: 1038
	private static Queue<dfMarkupBoxText> objectPool = new Queue<dfMarkupBoxText>();

	// Token: 0x0400040F RID: 1039
	private static Regex whitespacePattern = new Regex("\\s+");

	// Token: 0x04000411 RID: 1041
	private dfRenderData renderData = new dfRenderData();

	// Token: 0x04000412 RID: 1042
	private bool isWhitespace;
}

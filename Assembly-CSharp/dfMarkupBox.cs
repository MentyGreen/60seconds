using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000080 RID: 128
public class dfMarkupBox
{
	// Token: 0x170001E1 RID: 481
	// (get) Token: 0x06000844 RID: 2116 RVA: 0x0002434E File Offset: 0x0002254E
	// (set) Token: 0x06000845 RID: 2117 RVA: 0x00024356 File Offset: 0x00022556
	public dfMarkupBox Parent { get; protected set; }

	// Token: 0x170001E2 RID: 482
	// (get) Token: 0x06000846 RID: 2118 RVA: 0x0002435F File Offset: 0x0002255F
	// (set) Token: 0x06000847 RID: 2119 RVA: 0x00024367 File Offset: 0x00022567
	public dfMarkupElement Element { get; protected set; }

	// Token: 0x170001E3 RID: 483
	// (get) Token: 0x06000848 RID: 2120 RVA: 0x00024370 File Offset: 0x00022570
	public List<dfMarkupBox> Children
	{
		get
		{
			return this.children;
		}
	}

	// Token: 0x170001E4 RID: 484
	// (get) Token: 0x06000849 RID: 2121 RVA: 0x00024378 File Offset: 0x00022578
	// (set) Token: 0x0600084A RID: 2122 RVA: 0x00024386 File Offset: 0x00022586
	public int Width
	{
		get
		{
			return (int)this.Size.x;
		}
		set
		{
			this.Size = new Vector2((float)value, this.Size.y);
		}
	}

	// Token: 0x170001E5 RID: 485
	// (get) Token: 0x0600084B RID: 2123 RVA: 0x000243A0 File Offset: 0x000225A0
	// (set) Token: 0x0600084C RID: 2124 RVA: 0x000243AE File Offset: 0x000225AE
	public int Height
	{
		get
		{
			return (int)this.Size.y;
		}
		set
		{
			this.Size = new Vector2(this.Size.x, (float)value);
		}
	}

	// Token: 0x0600084D RID: 2125 RVA: 0x000243C8 File Offset: 0x000225C8
	private dfMarkupBox()
	{
		throw new NotImplementedException();
	}

	// Token: 0x0600084E RID: 2126 RVA: 0x00024420 File Offset: 0x00022620
	public dfMarkupBox(dfMarkupElement element, dfMarkupDisplayType display, dfMarkupStyle style)
	{
		this.Element = element;
		this.Display = display;
		this.Style = style;
		this.Baseline = style.FontSize;
	}

	// Token: 0x0600084F RID: 2127 RVA: 0x00024494 File Offset: 0x00022694
	internal dfMarkupBox HitTest(Vector2 point)
	{
		Vector2 offset = this.GetOffset();
		Vector2 vector = offset + this.Size;
		if (point.x < offset.x || point.x > vector.x || point.y < offset.y || point.y > vector.y)
		{
			return null;
		}
		for (int i = 0; i < this.children.Count; i++)
		{
			dfMarkupBox dfMarkupBox = this.children[i].HitTest(point);
			if (dfMarkupBox != null)
			{
				return dfMarkupBox;
			}
		}
		return this;
	}

	// Token: 0x06000850 RID: 2128 RVA: 0x0002451E File Offset: 0x0002271E
	internal dfRenderData Render()
	{
		this.endCurrentLine();
		return this.OnRebuildRenderData();
	}

	// Token: 0x06000851 RID: 2129 RVA: 0x0002452C File Offset: 0x0002272C
	public virtual Vector2 GetOffset()
	{
		Vector2 vector = Vector2.zero;
		for (dfMarkupBox dfMarkupBox = this; dfMarkupBox != null; dfMarkupBox = dfMarkupBox.Parent)
		{
			vector += dfMarkupBox.Position;
		}
		return vector;
	}

	// Token: 0x06000852 RID: 2130 RVA: 0x0002455C File Offset: 0x0002275C
	internal void AddLineBreak()
	{
		if (this.currentLine != null)
		{
			this.currentLine.IsNewline = true;
		}
		int verticalPosition = this.getVerticalPosition(0);
		this.endCurrentLine();
		dfMarkupBox containingBlock = this.GetContainingBlock();
		this.currentLine = new dfMarkupBox(this.Element, dfMarkupDisplayType.block, this.Style)
		{
			Size = new Vector2(containingBlock.Size.x, (float)this.Style.FontSize),
			Position = new Vector2(0f, (float)verticalPosition),
			Parent = this
		};
		this.children.Add(this.currentLine);
	}

	// Token: 0x06000853 RID: 2131 RVA: 0x000245F8 File Offset: 0x000227F8
	public virtual void AddChild(dfMarkupBox box)
	{
		dfMarkupDisplayType display = box.Display;
		if (display == dfMarkupDisplayType.block || display == dfMarkupDisplayType.table || display == dfMarkupDisplayType.listItem || display == dfMarkupDisplayType.tableRow)
		{
			this.addBlock(box);
			return;
		}
		this.addInline(box);
	}

	// Token: 0x06000854 RID: 2132 RVA: 0x00024634 File Offset: 0x00022834
	public virtual void Release()
	{
		for (int i = 0; i < this.children.Count; i++)
		{
			this.children[i].Release();
		}
		this.children.Clear();
		this.Element = null;
		this.Parent = null;
		this.Margins = default(dfMarkupBorders);
	}

	// Token: 0x06000855 RID: 2133 RVA: 0x0002468D File Offset: 0x0002288D
	protected virtual dfRenderData OnRebuildRenderData()
	{
		return null;
	}

	// Token: 0x06000856 RID: 2134 RVA: 0x00024690 File Offset: 0x00022890
	protected void renderDebugBox(dfRenderData renderData)
	{
		Vector3 zero = Vector3.zero;
		Vector3 vector = zero + Vector3.right * this.Size.x;
		Vector3 item = vector + Vector3.down * this.Size.y;
		Vector3 item2 = zero + Vector3.down * this.Size.y;
		renderData.Vertices.Add(zero);
		renderData.Vertices.Add(vector);
		renderData.Vertices.Add(item);
		renderData.Vertices.Add(item2);
		renderData.Triangles.AddRange(new int[]
		{
			0,
			1,
			3,
			3,
			1,
			2
		});
		renderData.UV.Add(Vector2.zero);
		renderData.UV.Add(Vector2.zero);
		renderData.UV.Add(Vector2.zero);
		renderData.UV.Add(Vector2.zero);
		Color backgroundColor = this.Style.BackgroundColor;
		renderData.Colors.Add(backgroundColor);
		renderData.Colors.Add(backgroundColor);
		renderData.Colors.Add(backgroundColor);
		renderData.Colors.Add(backgroundColor);
	}

	// Token: 0x06000857 RID: 2135 RVA: 0x000247D8 File Offset: 0x000229D8
	public void FitToContents()
	{
		this.FitToContents(false);
	}

	// Token: 0x06000858 RID: 2136 RVA: 0x000247E4 File Offset: 0x000229E4
	public void FitToContents(bool recursive)
	{
		if (this.children.Count == 0)
		{
			this.Size = new Vector2(this.Size.x, 0f);
			return;
		}
		this.endCurrentLine();
		Vector2 vector = Vector2.zero;
		for (int i = 0; i < this.children.Count; i++)
		{
			dfMarkupBox dfMarkupBox = this.children[i];
			vector = Vector2.Max(vector, dfMarkupBox.Position + dfMarkupBox.Size);
		}
		this.Size = vector;
	}

	// Token: 0x06000859 RID: 2137 RVA: 0x00024868 File Offset: 0x00022A68
	private dfMarkupBox GetContainingBlock()
	{
		for (dfMarkupBox dfMarkupBox = this; dfMarkupBox != null; dfMarkupBox = dfMarkupBox.Parent)
		{
			dfMarkupDisplayType display = dfMarkupBox.Display;
			if (display == dfMarkupDisplayType.block || display == dfMarkupDisplayType.inlineBlock || display == dfMarkupDisplayType.listItem || display == dfMarkupDisplayType.table || display == dfMarkupDisplayType.tableRow || display == dfMarkupDisplayType.tableCell)
			{
				return dfMarkupBox;
			}
		}
		return null;
	}

	// Token: 0x0600085A RID: 2138 RVA: 0x000248AC File Offset: 0x00022AAC
	private void addInline(dfMarkupBox box)
	{
		dfMarkupBorders margins = box.Margins;
		bool flag = !this.Style.Preformatted && this.currentLine != null && (float)this.currentLinePos + box.Size.x > this.currentLine.Size.x;
		if (this.currentLine == null || flag)
		{
			this.endCurrentLine();
			int verticalPosition = this.getVerticalPosition(margins.top);
			dfMarkupBox containingBlock = this.GetContainingBlock();
			if (containingBlock == null)
			{
				Debug.LogError("Containing block not found");
				return;
			}
			dfDynamicFont dfDynamicFont = this.Style.Font ?? this.Style.Host.Font;
			float num = (float)dfDynamicFont.FontSize / (float)dfDynamicFont.FontSize;
			float num2 = (float)dfDynamicFont.Baseline * num;
			this.currentLine = new dfMarkupBox(this.Element, dfMarkupDisplayType.block, this.Style)
			{
				Size = new Vector2(containingBlock.Size.x, (float)this.Style.LineHeight),
				Position = new Vector2(0f, (float)verticalPosition),
				Parent = this,
				Baseline = (int)num2
			};
			this.children.Add(this.currentLine);
		}
		if (this.currentLinePos == 0 && !box.Style.PreserveWhitespace && box is dfMarkupBoxText && (box as dfMarkupBoxText).IsWhitespace)
		{
			return;
		}
		Vector2 vector = new Vector2((float)(this.currentLinePos + margins.left), (float)margins.top);
		box.Position = vector;
		box.Parent = this.currentLine;
		this.currentLine.children.Add(box);
		this.currentLinePos = (int)(vector.x + box.Size.x + (float)box.Margins.right);
		float x = Mathf.Max(this.currentLine.Size.x, vector.x + box.Size.x);
		float y = Mathf.Max(this.currentLine.Size.y, vector.y + box.Size.y);
		this.currentLine.Size = new Vector2(x, y);
	}

	// Token: 0x0600085B RID: 2139 RVA: 0x00024AE4 File Offset: 0x00022CE4
	private int getVerticalPosition(int topMargin)
	{
		if (this.children.Count == 0)
		{
			return topMargin;
		}
		int num = 0;
		int index = 0;
		for (int i = 0; i < this.children.Count; i++)
		{
			dfMarkupBox dfMarkupBox = this.children[i];
			float num2 = dfMarkupBox.Position.y + dfMarkupBox.Size.y + (float)dfMarkupBox.Margins.bottom;
			if (num2 > (float)num)
			{
				num = (int)num2;
				index = i;
			}
		}
		dfMarkupBox dfMarkupBox2 = this.children[index];
		int num3 = Mathf.Max(dfMarkupBox2.Margins.bottom, topMargin);
		return (int)(dfMarkupBox2.Position.y + dfMarkupBox2.Size.y + (float)num3);
	}

	// Token: 0x0600085C RID: 2140 RVA: 0x00024BA0 File Offset: 0x00022DA0
	private void addBlock(dfMarkupBox box)
	{
		if (this.currentLine != null)
		{
			this.currentLine.IsNewline = true;
			this.endCurrentLine(true);
		}
		dfMarkupBox containingBlock = this.GetContainingBlock();
		if (box.Size.sqrMagnitude <= 1E-45f)
		{
			box.Size = new Vector2(containingBlock.Size.x - (float)box.Margins.horizontal, (float)this.Style.FontSize);
		}
		int verticalPosition = this.getVerticalPosition(box.Margins.top);
		box.Position = new Vector2((float)box.Margins.left, (float)verticalPosition);
		this.Size = new Vector2(this.Size.x, Mathf.Max(this.Size.y, box.Position.y + box.Size.y));
		box.Parent = this;
		this.children.Add(box);
	}

	// Token: 0x0600085D RID: 2141 RVA: 0x00024C8B File Offset: 0x00022E8B
	private void endCurrentLine()
	{
		this.endCurrentLine(false);
	}

	// Token: 0x0600085E RID: 2142 RVA: 0x00024C94 File Offset: 0x00022E94
	private void endCurrentLine(bool removeEmpty)
	{
		if (this.currentLine == null)
		{
			return;
		}
		if (this.currentLinePos == 0)
		{
			if (removeEmpty)
			{
				this.children.Remove(this.currentLine);
			}
		}
		else
		{
			this.currentLine.doHorizontalAlignment();
			this.currentLine.doVerticalAlignment();
		}
		this.currentLine = null;
		this.currentLinePos = 0;
	}

	// Token: 0x0600085F RID: 2143 RVA: 0x00024CF0 File Offset: 0x00022EF0
	private void doVerticalAlignment()
	{
		if (this.children.Count == 0)
		{
			return;
		}
		float num = float.MinValue;
		float num2 = float.MaxValue;
		float num3 = float.MinValue;
		this.Baseline = (int)(this.Size.y * 0.95f);
		for (int i = 0; i < this.children.Count; i++)
		{
			dfMarkupBox dfMarkupBox = this.children[i];
			num3 = Mathf.Max(num3, dfMarkupBox.Position.y + (float)dfMarkupBox.Baseline);
		}
		for (int j = 0; j < this.children.Count; j++)
		{
			dfMarkupBox dfMarkupBox2 = this.children[j];
			bool verticalAlign = dfMarkupBox2.Style.VerticalAlign != dfMarkupVerticalAlign.Baseline;
			Vector2 position = dfMarkupBox2.Position;
			if (!verticalAlign)
			{
				position.y = num3 - (float)dfMarkupBox2.Baseline;
			}
			dfMarkupBox2.Position = position;
		}
		for (int k = 0; k < this.children.Count; k++)
		{
			dfMarkupBox dfMarkupBox3 = this.children[k];
			Vector2 position2 = dfMarkupBox3.Position;
			Vector2 size = dfMarkupBox3.Size;
			num2 = Mathf.Min(num2, position2.y);
			num = Mathf.Max(num, position2.y + size.y);
		}
		for (int l = 0; l < this.children.Count; l++)
		{
			dfMarkupBox dfMarkupBox4 = this.children[l];
			dfMarkupVerticalAlign verticalAlign2 = dfMarkupBox4.Style.VerticalAlign;
			Vector2 position3 = dfMarkupBox4.Position;
			Vector2 size2 = dfMarkupBox4.Size;
			if (verticalAlign2 == dfMarkupVerticalAlign.Top)
			{
				position3.y = num2;
			}
			else if (verticalAlign2 == dfMarkupVerticalAlign.Bottom)
			{
				position3.y = num - size2.y;
			}
			else if (verticalAlign2 == dfMarkupVerticalAlign.Middle)
			{
				position3.y = (this.Size.y - size2.y) * 0.5f;
			}
			dfMarkupBox4.Position = position3;
		}
		int num4 = int.MaxValue;
		for (int m = 0; m < this.children.Count; m++)
		{
			num4 = Mathf.Min(num4, (int)this.children[m].Position.y);
		}
		for (int n = 0; n < this.children.Count; n++)
		{
			Vector2 position4 = this.children[n].Position;
			position4.y -= (float)num4;
			this.children[n].Position = position4;
		}
	}

	// Token: 0x06000860 RID: 2144 RVA: 0x00024F58 File Offset: 0x00023158
	private void doHorizontalAlignment()
	{
		if (this.Style.Align == dfMarkupTextAlign.Left || this.children.Count == 0)
		{
			return;
		}
		int i;
		for (i = this.children.Count - 1; i > 0; i--)
		{
			dfMarkupBoxText dfMarkupBoxText = this.children[i] as dfMarkupBoxText;
			if (dfMarkupBoxText == null || !dfMarkupBoxText.IsWhitespace)
			{
				break;
			}
		}
		if (this.Style.Align == dfMarkupTextAlign.Center)
		{
			float num = 0f;
			for (int j = 0; j <= i; j++)
			{
				num += this.children[j].Size.x;
			}
			float num2 = (this.Size.x - (float)this.Padding.horizontal - num) * 0.5f;
			for (int k = 0; k <= i; k++)
			{
				Vector2 position = this.children[k].Position;
				position.x += num2;
				this.children[k].Position = position;
			}
			return;
		}
		if (this.Style.Align == dfMarkupTextAlign.Right)
		{
			float num3 = this.Size.x - (float)this.Padding.horizontal;
			for (int l = i; l >= 0; l--)
			{
				Vector2 position2 = this.children[l].Position;
				position2.x = num3 - this.children[l].Size.x;
				this.children[l].Position = position2;
				num3 -= this.children[l].Size.x;
			}
			return;
		}
		if (this.Style.Align != dfMarkupTextAlign.Justify)
		{
			throw new NotImplementedException("text-align: " + this.Style.Align.ToString() + " is not implemented");
		}
		if (this.children.Count <= 1)
		{
			return;
		}
		if (this.IsNewline || this.children[this.children.Count - 1].IsNewline)
		{
			return;
		}
		float num4 = 0f;
		for (int m = 0; m <= i; m++)
		{
			dfMarkupBox dfMarkupBox = this.children[m];
			num4 = Mathf.Max(num4, dfMarkupBox.Position.x + dfMarkupBox.Size.x);
		}
		float num5 = (this.Size.x - (float)this.Padding.horizontal - num4) / (float)this.children.Count;
		for (int n = 1; n <= i; n++)
		{
			this.children[n].Position += new Vector2((float)n * num5, 0f);
		}
		dfMarkupBox dfMarkupBox2 = this.children[i];
		Vector2 position3 = dfMarkupBox2.Position;
		position3.x = this.Size.x - (float)this.Padding.horizontal - dfMarkupBox2.Size.x;
		dfMarkupBox2.Position = position3;
	}

	// Token: 0x040003FA RID: 1018
	public Vector2 Position = Vector2.zero;

	// Token: 0x040003FB RID: 1019
	public Vector2 Size = Vector2.zero;

	// Token: 0x040003FC RID: 1020
	public dfMarkupDisplayType Display;

	// Token: 0x040003FD RID: 1021
	public dfMarkupBorders Margins = new dfMarkupBorders(0, 0, 0, 0);

	// Token: 0x040003FE RID: 1022
	public dfMarkupBorders Padding = new dfMarkupBorders(0, 0, 0, 0);

	// Token: 0x040003FF RID: 1023
	public dfMarkupStyle Style;

	// Token: 0x04000400 RID: 1024
	public bool IsNewline;

	// Token: 0x04000401 RID: 1025
	public int Baseline;

	// Token: 0x04000402 RID: 1026
	private List<dfMarkupBox> children = new List<dfMarkupBox>();

	// Token: 0x04000403 RID: 1027
	private dfMarkupBox currentLine;

	// Token: 0x04000404 RID: 1028
	private int currentLinePos;
}

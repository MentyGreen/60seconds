using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

// Token: 0x02000087 RID: 135
public class dfMarkupString : dfMarkupElement
{
	// Token: 0x170001EE RID: 494
	// (get) Token: 0x0600088E RID: 2190 RVA: 0x00025F67 File Offset: 0x00024167
	// (set) Token: 0x0600088F RID: 2191 RVA: 0x00025F6F File Offset: 0x0002416F
	public string Text { get; set; }

	// Token: 0x170001EF RID: 495
	// (get) Token: 0x06000890 RID: 2192 RVA: 0x00025F78 File Offset: 0x00024178
	public bool IsWhitespace
	{
		get
		{
			return this.isWhitespace;
		}
	}

	// Token: 0x06000891 RID: 2193 RVA: 0x00025F80 File Offset: 0x00024180
	public dfMarkupString(string text)
	{
		this.Text = this.processWhitespace(dfMarkupEntity.Replace(text));
		this.isWhitespace = dfMarkupString.whitespacePattern.IsMatch(this.Text);
	}

	// Token: 0x06000892 RID: 2194 RVA: 0x00025FB0 File Offset: 0x000241B0
	public override string ToString()
	{
		return this.Text;
	}

	// Token: 0x06000893 RID: 2195 RVA: 0x00025FB8 File Offset: 0x000241B8
	internal dfMarkupElement SplitWords()
	{
		dfMarkupTagSpan dfMarkupTagSpan = dfMarkupTagSpan.Obtain();
		int i = 0;
		int num = 0;
		int length = this.Text.Length;
		while (i < length)
		{
			while (i < length && !char.IsWhiteSpace(this.Text[i]))
			{
				i++;
			}
			if (i > num)
			{
				dfMarkupTagSpan.AddChildNode(dfMarkupString.Obtain(this.Text.Substring(num, i - num)));
				num = i;
			}
			while (i < length && this.Text[i] != '\n' && char.IsWhiteSpace(this.Text[i]))
			{
				i++;
			}
			if (i > num)
			{
				dfMarkupTagSpan.AddChildNode(dfMarkupString.Obtain(this.Text.Substring(num, i - num)));
				num = i;
			}
			if (i < length && this.Text[i] == '\n')
			{
				dfMarkupTagSpan.AddChildNode(dfMarkupString.Obtain("\n"));
				i = (num = i + 1);
			}
		}
		return dfMarkupTagSpan;
	}

	// Token: 0x06000894 RID: 2196 RVA: 0x0002609C File Offset: 0x0002429C
	protected override void _PerformLayoutImpl(dfMarkupBox container, dfMarkupStyle style)
	{
		if (style.Font == null)
		{
			return;
		}
		string text = (style.PreserveWhitespace || !this.isWhitespace) ? this.Text : " ";
		dfMarkupBoxText dfMarkupBoxText = dfMarkupBoxText.Obtain(this, dfMarkupDisplayType.inline, style);
		dfMarkupBoxText.SetText(text);
		container.AddChild(dfMarkupBoxText);
	}

	// Token: 0x06000895 RID: 2197 RVA: 0x000260F0 File Offset: 0x000242F0
	internal static dfMarkupString Obtain(string text)
	{
		if (dfMarkupString.objectPool.Count > 0)
		{
			dfMarkupString dfMarkupString = dfMarkupString.objectPool.Dequeue();
			dfMarkupString.Text = dfMarkupEntity.Replace(text);
			dfMarkupString.isWhitespace = dfMarkupString.whitespacePattern.IsMatch(dfMarkupString.Text);
			return dfMarkupString;
		}
		return new dfMarkupString(text);
	}

	// Token: 0x06000896 RID: 2198 RVA: 0x0002613F File Offset: 0x0002433F
	internal override void Release()
	{
		base.Release();
		dfMarkupString.objectPool.Enqueue(this);
	}

	// Token: 0x06000897 RID: 2199 RVA: 0x00026154 File Offset: 0x00024354
	private string processWhitespace(string text)
	{
		dfMarkupString.buffer.Length = 0;
		dfMarkupString.buffer.Append(text);
		dfMarkupString.buffer.Replace("\r\n", "\n");
		dfMarkupString.buffer.Replace("\r", "\n");
		dfMarkupString.buffer.Replace("\t", "    ");
		return dfMarkupString.buffer.ToString();
	}

	// Token: 0x0400041A RID: 1050
	private static StringBuilder buffer = new StringBuilder();

	// Token: 0x0400041B RID: 1051
	private static Regex whitespacePattern = new Regex("\\s+");

	// Token: 0x0400041C RID: 1052
	private static Queue<dfMarkupString> objectPool = new Queue<dfMarkupString>();

	// Token: 0x0400041E RID: 1054
	private bool isWhitespace;
}

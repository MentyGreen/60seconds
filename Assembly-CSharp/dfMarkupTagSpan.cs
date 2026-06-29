using System;
using System.Collections.Generic;
using System.Text;

// Token: 0x02000091 RID: 145
[dfMarkupTagInfo("span")]
public class dfMarkupTagSpan : dfMarkupTag
{
	// Token: 0x060008D2 RID: 2258 RVA: 0x0002734C File Offset: 0x0002554C
	public dfMarkupTagSpan() : base("span")
	{
	}

	// Token: 0x060008D3 RID: 2259 RVA: 0x00027359 File Offset: 0x00025559
	public dfMarkupTagSpan(dfMarkupTag original) : base(original)
	{
	}

	// Token: 0x060008D4 RID: 2260 RVA: 0x00027364 File Offset: 0x00025564
	protected override void _PerformLayoutImpl(dfMarkupBox container, dfMarkupStyle style)
	{
		style = base.applyTextStyleAttributes(style);
		dfMarkupBox dfMarkupBox = container;
		dfMarkupAttribute dfMarkupAttribute = base.findAttribute(new string[]
		{
			"margin"
		});
		if (dfMarkupAttribute != null)
		{
			dfMarkupBox = new dfMarkupBox(this, dfMarkupDisplayType.inlineBlock, style);
			dfMarkupBox.Margins = dfMarkupBorders.Parse(dfMarkupAttribute.Value);
			dfMarkupBox.Margins.top = 0;
			dfMarkupBox.Margins.bottom = 0;
			container.AddChild(dfMarkupBox);
		}
		StringBuilder stringBuilder = new StringBuilder();
		int i = 0;
		while (i < base.ChildNodes.Count)
		{
			dfMarkupElement dfMarkupElement = base.ChildNodes[i];
			if (!(dfMarkupElement is dfMarkupString))
			{
				goto IL_209;
			}
			dfMarkupString dfMarkupString = dfMarkupElement as dfMarkupString;
			if (dfMarkupString.Text == "\n")
			{
				if (style.PreserveWhitespace)
				{
					dfMarkupBox.AddLineBreak();
				}
			}
			else
			{
				if (base.Owner.ForceWordwrap && base.Owner.Font.MeasureText(dfMarkupString.Text, base.Owner.FontSize, base.Owner.FontStyle).x >= (float)dfMarkupBox.Width)
				{
					stringBuilder.Remove(0, stringBuilder.Length);
					stringBuilder.Append(dfMarkupString.Text);
					base.InsertChildNode(new dfMarkupString(string.Empty), i + 1);
					dfMarkupString dfMarkupString2 = base.ChildNodes[i + 1] as dfMarkupString;
					bool flag = true;
					int num = 10;
					int num2 = num;
					while (flag && num2 > 1)
					{
						int num3 = (int)((float)dfMarkupString.Text.Length * ((float)(num2 - 1) / (float)num));
						dfMarkupString2.Text = stringBuilder.ToString(num3, stringBuilder.Length - num3);
						dfMarkupString.Text = stringBuilder.ToString(0, num3);
						flag = (base.Owner.Font.MeasureText(dfMarkupString.Text, base.Owner.FontSize, base.Owner.FontStyle).x >= (float)dfMarkupBox.Width);
						if (!flag)
						{
							dfMarkupString dfMarkupString3 = dfMarkupString;
							dfMarkupString3.Text += "\n";
						}
						num2--;
					}
					goto IL_209;
				}
				goto IL_209;
			}
			IL_212:
			i++;
			continue;
			IL_209:
			dfMarkupElement.PerformLayout(dfMarkupBox, style);
			goto IL_212;
		}
	}

	// Token: 0x060008D5 RID: 2261 RVA: 0x00027598 File Offset: 0x00025798
	internal static dfMarkupTagSpan Obtain()
	{
		if (dfMarkupTagSpan.objectPool.Count > 0)
		{
			return dfMarkupTagSpan.objectPool.Dequeue();
		}
		return new dfMarkupTagSpan();
	}

	// Token: 0x060008D6 RID: 2262 RVA: 0x000275B7 File Offset: 0x000257B7
	internal override void Release()
	{
		base.Release();
		dfMarkupTagSpan.objectPool.Enqueue(this);
	}

	// Token: 0x04000456 RID: 1110
	private static Queue<dfMarkupTagSpan> objectPool = new Queue<dfMarkupTagSpan>();
}

using System;
using UnityEngine;

// Token: 0x02000096 RID: 150
[dfMarkupTagInfo("strong")]
[dfMarkupTagInfo("b")]
public class dfMarkupTagBold : dfMarkupTag
{
	// Token: 0x060008E8 RID: 2280 RVA: 0x00027B40 File Offset: 0x00025D40
	public dfMarkupTagBold() : base("b")
	{
	}

	// Token: 0x060008E9 RID: 2281 RVA: 0x00027B4D File Offset: 0x00025D4D
	public dfMarkupTagBold(dfMarkupTag original) : base(original)
	{
	}

	// Token: 0x060008EA RID: 2282 RVA: 0x00027B56 File Offset: 0x00025D56
	protected override void _PerformLayoutImpl(dfMarkupBox container, dfMarkupStyle style)
	{
		style = base.applyTextStyleAttributes(style);
		if (style.FontStyle == FontStyle.Normal)
		{
			style.FontStyle = FontStyle.Bold;
		}
		else if (style.FontStyle == FontStyle.Italic)
		{
			style.FontStyle = FontStyle.BoldAndItalic;
		}
		base._PerformLayoutImpl(container, style);
	}
}

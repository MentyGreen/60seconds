using System;
using UnityEngine;

// Token: 0x02000098 RID: 152
[dfMarkupTagInfo("em")]
[dfMarkupTagInfo("i")]
public class dfMarkupTagItalic : dfMarkupTag
{
	// Token: 0x060008EF RID: 2287 RVA: 0x00027D19 File Offset: 0x00025F19
	public dfMarkupTagItalic() : base("i")
	{
	}

	// Token: 0x060008F0 RID: 2288 RVA: 0x00027D26 File Offset: 0x00025F26
	public dfMarkupTagItalic(dfMarkupTag original) : base(original)
	{
	}

	// Token: 0x060008F1 RID: 2289 RVA: 0x00027D2F File Offset: 0x00025F2F
	protected override void _PerformLayoutImpl(dfMarkupBox container, dfMarkupStyle style)
	{
		style = base.applyTextStyleAttributes(style);
		if (style.FontStyle == FontStyle.Normal)
		{
			style.FontStyle = FontStyle.Italic;
		}
		else if (style.FontStyle == FontStyle.Bold)
		{
			style.FontStyle = FontStyle.BoldAndItalic;
		}
		base._PerformLayoutImpl(container, style);
	}
}

using System;
using UnityEngine;

// Token: 0x0200009C RID: 156
[dfMarkupTagInfo("font")]
public class dfMarkupTagFont : dfMarkupTag
{
	// Token: 0x060008FD RID: 2301 RVA: 0x0002813B File Offset: 0x0002633B
	public dfMarkupTagFont() : base("font")
	{
	}

	// Token: 0x060008FE RID: 2302 RVA: 0x00028148 File Offset: 0x00026348
	public dfMarkupTagFont(dfMarkupTag original) : base(original)
	{
	}

	// Token: 0x060008FF RID: 2303 RVA: 0x00028154 File Offset: 0x00026354
	protected override void _PerformLayoutImpl(dfMarkupBox container, dfMarkupStyle style)
	{
		dfMarkupAttribute dfMarkupAttribute = base.findAttribute(new string[]
		{
			"name",
			"face"
		});
		if (dfMarkupAttribute != null)
		{
			style.Font = (dfDynamicFont.FindByName(dfMarkupAttribute.Value) ?? style.Font);
		}
		dfMarkupAttribute dfMarkupAttribute2 = base.findAttribute(new string[]
		{
			"size",
			"font-size"
		});
		if (dfMarkupAttribute2 != null)
		{
			style.FontSize = dfMarkupStyle.ParseSize(dfMarkupAttribute2.Value, style.FontSize);
		}
		dfMarkupAttribute dfMarkupAttribute3 = base.findAttribute(new string[]
		{
			"color"
		});
		if (dfMarkupAttribute3 != null)
		{
			style.Color = dfMarkupStyle.ParseColor(dfMarkupAttribute3.Value, Color.red);
			style.Color.a = style.Opacity;
		}
		dfMarkupAttribute dfMarkupAttribute4 = base.findAttribute(new string[]
		{
			"style"
		});
		if (dfMarkupAttribute4 != null)
		{
			style.FontStyle = dfMarkupStyle.ParseFontStyle(dfMarkupAttribute4.Value, style.FontStyle);
		}
		base._PerformLayoutImpl(container, style);
	}
}

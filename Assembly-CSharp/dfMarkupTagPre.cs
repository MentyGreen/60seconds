using System;

// Token: 0x02000099 RID: 153
[dfMarkupTagInfo("pre")]
public class dfMarkupTagPre : dfMarkupTag
{
	// Token: 0x060008F2 RID: 2290 RVA: 0x00027D65 File Offset: 0x00025F65
	public dfMarkupTagPre() : base("pre")
	{
	}

	// Token: 0x060008F3 RID: 2291 RVA: 0x00027D72 File Offset: 0x00025F72
	public dfMarkupTagPre(dfMarkupTag original) : base(original)
	{
	}

	// Token: 0x060008F4 RID: 2292 RVA: 0x00027D7C File Offset: 0x00025F7C
	protected override void _PerformLayoutImpl(dfMarkupBox container, dfMarkupStyle style)
	{
		style = base.applyTextStyleAttributes(style);
		style.PreserveWhitespace = true;
		style.Preformatted = true;
		if (style.Align == dfMarkupTextAlign.Justify)
		{
			style.Align = dfMarkupTextAlign.Left;
		}
		dfMarkupBox dfMarkupBox;
		if (style.BackgroundColor.a > 0.1f)
		{
			dfMarkupBoxSprite dfMarkupBoxSprite = new dfMarkupBoxSprite(this, dfMarkupDisplayType.block, style);
			dfMarkupBoxSprite.LoadImage(base.Owner.Atlas, base.Owner.BlankTextureSprite);
			dfMarkupBoxSprite.Style.Color = style.BackgroundColor;
			dfMarkupBox = dfMarkupBoxSprite;
		}
		else
		{
			dfMarkupBox = new dfMarkupBox(this, dfMarkupDisplayType.block, style);
		}
		dfMarkupAttribute dfMarkupAttribute = base.findAttribute(new string[]
		{
			"margin"
		});
		if (dfMarkupAttribute != null)
		{
			dfMarkupBox.Margins = dfMarkupBorders.Parse(dfMarkupAttribute.Value);
		}
		dfMarkupAttribute dfMarkupAttribute2 = base.findAttribute(new string[]
		{
			"padding"
		});
		if (dfMarkupAttribute2 != null)
		{
			dfMarkupBox.Padding = dfMarkupBorders.Parse(dfMarkupAttribute2.Value);
		}
		container.AddChild(dfMarkupBox);
		base._PerformLayoutImpl(dfMarkupBox, style);
		dfMarkupBox.FitToContents();
	}
}

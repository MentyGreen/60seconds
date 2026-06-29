using System;

// Token: 0x02000095 RID: 149
[dfMarkupTagInfo("p")]
public class dfMarkupTagParagraph : dfMarkupTag
{
	// Token: 0x060008E5 RID: 2277 RVA: 0x000279EA File Offset: 0x00025BEA
	public dfMarkupTagParagraph() : base("p")
	{
	}

	// Token: 0x060008E6 RID: 2278 RVA: 0x000279F7 File Offset: 0x00025BF7
	public dfMarkupTagParagraph(dfMarkupTag original) : base(original)
	{
	}

	// Token: 0x060008E7 RID: 2279 RVA: 0x00027A00 File Offset: 0x00025C00
	protected override void _PerformLayoutImpl(dfMarkupBox container, dfMarkupStyle style)
	{
		if (base.ChildNodes.Count == 0)
		{
			return;
		}
		style = base.applyTextStyleAttributes(style);
		int top = (container.Children.Count == 0) ? 0 : style.LineHeight;
		dfMarkupBox dfMarkupBox;
		if (style.BackgroundColor.a > 0.005f)
		{
			dfMarkupBoxSprite dfMarkupBoxSprite = new dfMarkupBoxSprite(this, dfMarkupDisplayType.block, style);
			dfMarkupBoxSprite.Atlas = base.Owner.Atlas;
			dfMarkupBoxSprite.Source = base.Owner.BlankTextureSprite;
			dfMarkupBoxSprite.Style.Color = style.BackgroundColor;
			dfMarkupBox = dfMarkupBoxSprite;
		}
		else
		{
			dfMarkupBox = new dfMarkupBox(this, dfMarkupDisplayType.block, style);
		}
		dfMarkupBox.Margins = new dfMarkupBorders(0, 0, top, style.LineHeight);
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
		if (dfMarkupBox.Children.Count > 0)
		{
			dfMarkupBox.Children[dfMarkupBox.Children.Count - 1].IsNewline = true;
		}
		dfMarkupBox.FitToContents(true);
	}
}

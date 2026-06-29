using System;
using UnityEngine;

// Token: 0x02000097 RID: 151
[dfMarkupTagInfo("h1")]
[dfMarkupTagInfo("h2")]
[dfMarkupTagInfo("h3")]
[dfMarkupTagInfo("h4")]
[dfMarkupTagInfo("h5")]
[dfMarkupTagInfo("h6")]
public class dfMarkupTagHeading : dfMarkupTag
{
	// Token: 0x060008EB RID: 2283 RVA: 0x00027B8C File Offset: 0x00025D8C
	public dfMarkupTagHeading() : base("h1")
	{
	}

	// Token: 0x060008EC RID: 2284 RVA: 0x00027B99 File Offset: 0x00025D99
	public dfMarkupTagHeading(dfMarkupTag original) : base(original)
	{
	}

	// Token: 0x060008ED RID: 2285 RVA: 0x00027BA4 File Offset: 0x00025DA4
	protected override void _PerformLayoutImpl(dfMarkupBox container, dfMarkupStyle style)
	{
		dfMarkupBorders margins = default(dfMarkupBorders);
		dfMarkupStyle style2 = this.applyDefaultStyles(style, ref margins);
		style2 = base.applyTextStyleAttributes(style2);
		dfMarkupAttribute dfMarkupAttribute = base.findAttribute(new string[]
		{
			"margin"
		});
		if (dfMarkupAttribute != null)
		{
			margins = dfMarkupBorders.Parse(dfMarkupAttribute.Value);
		}
		dfMarkupBox dfMarkupBox = new dfMarkupBox(this, dfMarkupDisplayType.block, style2);
		dfMarkupBox.Margins = margins;
		container.AddChild(dfMarkupBox);
		base._PerformLayoutImpl(dfMarkupBox, style2);
		dfMarkupBox.FitToContents();
	}

	// Token: 0x060008EE RID: 2286 RVA: 0x00027C14 File Offset: 0x00025E14
	private dfMarkupStyle applyDefaultStyles(dfMarkupStyle style, ref dfMarkupBorders margins)
	{
		float num = 1f;
		float num2 = 1f;
		string tagName = base.TagName;
		if (tagName != null)
		{
			if (!(tagName == "h1"))
			{
				if (!(tagName == "h2"))
				{
					if (!(tagName == "h3"))
					{
						if (!(tagName == "h4"))
						{
							if (!(tagName == "h5"))
							{
								if (tagName == "h6")
								{
									num2 = 0.75f;
									num = 1.75f;
								}
							}
							else
							{
								num2 = 0.85f;
								num = 1.5f;
							}
						}
						else
						{
							num2 = 1.15f;
							num = 0f;
						}
					}
					else
					{
						num2 = 1.35f;
						num = 0.85f;
					}
				}
				else
				{
					num2 = 1.5f;
					num = 0.75f;
				}
			}
			else
			{
				num2 = 2f;
				num = 0.65f;
			}
		}
		style.FontSize = (int)((float)style.FontSize * num2);
		style.FontStyle = FontStyle.Bold;
		style.Align = dfMarkupTextAlign.Left;
		num *= (float)style.FontSize;
		margins.top = (margins.bottom = (int)num);
		return style;
	}
}

using System;

// Token: 0x02000092 RID: 146
[dfMarkupTagInfo("a")]
public class dfMarkupTagAnchor : dfMarkupTag
{
	// Token: 0x170001FB RID: 507
	// (get) Token: 0x060008D8 RID: 2264 RVA: 0x000275D8 File Offset: 0x000257D8
	public string HRef
	{
		get
		{
			dfMarkupAttribute dfMarkupAttribute = base.findAttribute(new string[]
			{
				"href"
			});
			if (dfMarkupAttribute == null)
			{
				return "";
			}
			return dfMarkupAttribute.Value;
		}
	}

	// Token: 0x060008D9 RID: 2265 RVA: 0x00027609 File Offset: 0x00025809
	public dfMarkupTagAnchor() : base("a")
	{
	}

	// Token: 0x060008DA RID: 2266 RVA: 0x00027616 File Offset: 0x00025816
	public dfMarkupTagAnchor(dfMarkupTag original) : base(original)
	{
	}

	// Token: 0x060008DB RID: 2267 RVA: 0x00027620 File Offset: 0x00025820
	protected override void _PerformLayoutImpl(dfMarkupBox container, dfMarkupStyle style)
	{
		style.TextDecoration = dfMarkupTextDecoration.Underline;
		style = base.applyTextStyleAttributes(style);
		for (int i = 0; i < base.ChildNodes.Count; i++)
		{
			dfMarkupElement dfMarkupElement = base.ChildNodes[i];
			if (dfMarkupElement is dfMarkupString && (dfMarkupElement as dfMarkupString).Text == "\n")
			{
				if (style.PreserveWhitespace)
				{
					container.AddLineBreak();
				}
			}
			else
			{
				dfMarkupElement.PerformLayout(container, style);
			}
		}
	}
}

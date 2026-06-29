using System;
using UnityEngine;

// Token: 0x02000094 RID: 148
[dfMarkupTagInfo("li")]
public class dfMarkupTagListItem : dfMarkupTag
{
	// Token: 0x060008E2 RID: 2274 RVA: 0x00027831 File Offset: 0x00025A31
	public dfMarkupTagListItem() : base("li")
	{
	}

	// Token: 0x060008E3 RID: 2275 RVA: 0x0002783E File Offset: 0x00025A3E
	public dfMarkupTagListItem(dfMarkupTag original) : base(original)
	{
	}

	// Token: 0x060008E4 RID: 2276 RVA: 0x00027848 File Offset: 0x00025A48
	protected override void _PerformLayoutImpl(dfMarkupBox container, dfMarkupStyle style)
	{
		if (base.ChildNodes.Count == 0)
		{
			return;
		}
		float x = container.Size.x;
		dfMarkupBox dfMarkupBox = new dfMarkupBox(this, dfMarkupDisplayType.listItem, style);
		dfMarkupBox.Margins.top = 10;
		container.AddChild(dfMarkupBox);
		dfMarkupTagList dfMarkupTagList = base.Parent as dfMarkupTagList;
		if (dfMarkupTagList == null)
		{
			base._PerformLayoutImpl(container, style);
			return;
		}
		style.VerticalAlign = dfMarkupVerticalAlign.Baseline;
		string text = "•";
		if (dfMarkupTagList.TagName == "ol")
		{
			text = container.Children.Count.ToString() + ".";
		}
		dfMarkupStyle style2 = style;
		style2.VerticalAlign = dfMarkupVerticalAlign.Baseline;
		style2.Align = dfMarkupTextAlign.Right;
		dfMarkupBoxText dfMarkupBoxText = dfMarkupBoxText.Obtain(this, dfMarkupDisplayType.inlineBlock, style2);
		dfMarkupBoxText.SetText(text);
		dfMarkupBoxText.Width = dfMarkupTagList.BulletWidth;
		dfMarkupBoxText.Margins.left = style.FontSize * 2;
		dfMarkupBox.AddChild(dfMarkupBoxText);
		dfMarkupBox dfMarkupBox2 = new dfMarkupBox(this, dfMarkupDisplayType.inlineBlock, style);
		int fontSize = style.FontSize;
		float x2 = x - dfMarkupBoxText.Size.x - (float)dfMarkupBoxText.Margins.left - (float)fontSize;
		dfMarkupBox2.Size = new Vector2(x2, (float)fontSize);
		dfMarkupBox2.Margins.left = (int)((float)style.FontSize * 0.5f);
		dfMarkupBox.AddChild(dfMarkupBox2);
		for (int i = 0; i < base.ChildNodes.Count; i++)
		{
			base.ChildNodes[i].PerformLayout(dfMarkupBox2, style);
		}
		dfMarkupBox2.FitToContents();
		if (dfMarkupBox2.Parent != null)
		{
			dfMarkupBox2.Parent.FitToContents();
		}
		dfMarkupBox.FitToContents();
	}
}

using System;
using UnityEngine;

// Token: 0x02000093 RID: 147
[dfMarkupTagInfo("ul")]
[dfMarkupTagInfo("ol")]
public class dfMarkupTagList : dfMarkupTag
{
	// Token: 0x170001FC RID: 508
	// (get) Token: 0x060008DC RID: 2268 RVA: 0x00027698 File Offset: 0x00025898
	// (set) Token: 0x060008DD RID: 2269 RVA: 0x000276A0 File Offset: 0x000258A0
	internal int BulletWidth { get; private set; }

	// Token: 0x060008DE RID: 2270 RVA: 0x000276A9 File Offset: 0x000258A9
	public dfMarkupTagList() : base("ul")
	{
	}

	// Token: 0x060008DF RID: 2271 RVA: 0x000276B6 File Offset: 0x000258B6
	public dfMarkupTagList(dfMarkupTag original) : base(original)
	{
	}

	// Token: 0x060008E0 RID: 2272 RVA: 0x000276C0 File Offset: 0x000258C0
	protected override void _PerformLayoutImpl(dfMarkupBox container, dfMarkupStyle style)
	{
		if (base.ChildNodes.Count == 0)
		{
			return;
		}
		style = base.applyTextStyleAttributes(style);
		style.Align = dfMarkupTextAlign.Left;
		dfMarkupBox dfMarkupBox = new dfMarkupBox(this, dfMarkupDisplayType.block, style);
		container.AddChild(dfMarkupBox);
		this.calculateBulletWidth(style);
		for (int i = 0; i < base.ChildNodes.Count; i++)
		{
			dfMarkupTag dfMarkupTag = base.ChildNodes[i] as dfMarkupTag;
			if (dfMarkupTag != null && !(dfMarkupTag.TagName != "li"))
			{
				dfMarkupTag.PerformLayout(dfMarkupBox, style);
			}
		}
		dfMarkupBox.FitToContents();
	}

	// Token: 0x060008E1 RID: 2273 RVA: 0x00027750 File Offset: 0x00025950
	private void calculateBulletWidth(dfMarkupStyle style)
	{
		if (base.TagName == "ul")
		{
			Vector2 vector = style.Font.MeasureText("•", style.FontSize, style.FontStyle);
			this.BulletWidth = Mathf.CeilToInt(vector.x);
			return;
		}
		int num = 0;
		for (int i = 0; i < base.ChildNodes.Count; i++)
		{
			dfMarkupTag dfMarkupTag = base.ChildNodes[i] as dfMarkupTag;
			if (dfMarkupTag != null && dfMarkupTag.TagName == "li")
			{
				num++;
			}
		}
		string text = new string('X', num.ToString().Length) + ".";
		Vector2 vector2 = style.Font.MeasureText(text, style.FontSize, style.FontStyle);
		this.BulletWidth = Mathf.CeilToInt(vector2.x);
	}
}

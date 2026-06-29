using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

// Token: 0x02000090 RID: 144
public class dfMarkupTag : dfMarkupElement
{
	// Token: 0x170001F5 RID: 501
	// (get) Token: 0x060008BF RID: 2239 RVA: 0x00026E13 File Offset: 0x00025013
	// (set) Token: 0x060008C0 RID: 2240 RVA: 0x00026E1B File Offset: 0x0002501B
	public string TagName { get; set; }

	// Token: 0x170001F6 RID: 502
	// (get) Token: 0x060008C1 RID: 2241 RVA: 0x00026E24 File Offset: 0x00025024
	public string ID
	{
		get
		{
			return this.id;
		}
	}

	// Token: 0x170001F7 RID: 503
	// (get) Token: 0x060008C2 RID: 2242 RVA: 0x00026E2C File Offset: 0x0002502C
	// (set) Token: 0x060008C3 RID: 2243 RVA: 0x00026E34 File Offset: 0x00025034
	public virtual bool IsEndTag { get; set; }

	// Token: 0x170001F8 RID: 504
	// (get) Token: 0x060008C4 RID: 2244 RVA: 0x00026E3D File Offset: 0x0002503D
	// (set) Token: 0x060008C5 RID: 2245 RVA: 0x00026E45 File Offset: 0x00025045
	public virtual bool IsClosedTag { get; set; }

	// Token: 0x170001F9 RID: 505
	// (get) Token: 0x060008C6 RID: 2246 RVA: 0x00026E4E File Offset: 0x0002504E
	// (set) Token: 0x060008C7 RID: 2247 RVA: 0x00026E56 File Offset: 0x00025056
	public virtual bool IsInline { get; set; }

	// Token: 0x170001FA RID: 506
	// (get) Token: 0x060008C8 RID: 2248 RVA: 0x00026E5F File Offset: 0x0002505F
	// (set) Token: 0x060008C9 RID: 2249 RVA: 0x00026E68 File Offset: 0x00025068
	public dfRichTextLabel Owner
	{
		get
		{
			return this.owner;
		}
		set
		{
			this.owner = value;
			for (int i = 0; i < base.ChildNodes.Count; i++)
			{
				dfMarkupTag dfMarkupTag = base.ChildNodes[i] as dfMarkupTag;
				if (dfMarkupTag != null)
				{
					dfMarkupTag.Owner = value;
				}
			}
		}
	}

	// Token: 0x060008CA RID: 2250 RVA: 0x00026EB0 File Offset: 0x000250B0
	public dfMarkupTag(string tagName)
	{
		this.Attributes = new List<dfMarkupAttribute>();
		this.TagName = tagName;
		this.id = tagName + dfMarkupTag.ELEMENTID++.ToString("X");
	}

	// Token: 0x060008CB RID: 2251 RVA: 0x00026EFC File Offset: 0x000250FC
	public dfMarkupTag(dfMarkupTag original)
	{
		this.TagName = original.TagName;
		this.Attributes = original.Attributes;
		this.IsEndTag = original.IsEndTag;
		this.IsClosedTag = original.IsClosedTag;
		this.IsInline = original.IsInline;
		this.id = original.id;
		List<dfMarkupElement> childNodes = original.ChildNodes;
		for (int i = 0; i < childNodes.Count; i++)
		{
			dfMarkupElement node = childNodes[i];
			base.AddChildNode(node);
		}
	}

	// Token: 0x060008CC RID: 2252 RVA: 0x00026F7E File Offset: 0x0002517E
	internal override void Release()
	{
		base.Release();
	}

	// Token: 0x060008CD RID: 2253 RVA: 0x00026F88 File Offset: 0x00025188
	protected override void _PerformLayoutImpl(dfMarkupBox container, dfMarkupStyle style)
	{
		if (this.IsEndTag)
		{
			return;
		}
		this.findAttribute(new string[]
		{
			"margin"
		});
		for (int i = 0; i < base.ChildNodes.Count; i++)
		{
			base.ChildNodes[i].PerformLayout(container, style);
		}
	}

	// Token: 0x060008CE RID: 2254 RVA: 0x00026FDC File Offset: 0x000251DC
	protected dfMarkupStyle applyTextStyleAttributes(dfMarkupStyle style)
	{
		dfMarkupAttribute dfMarkupAttribute = this.findAttribute(new string[]
		{
			"font",
			"font-family"
		});
		if (dfMarkupAttribute != null)
		{
			style.Font = (dfDynamicFont.FindByName(dfMarkupAttribute.Value) ?? this.owner.Font);
		}
		dfMarkupAttribute dfMarkupAttribute2 = this.findAttribute(new string[]
		{
			"style",
			"font-style"
		});
		if (dfMarkupAttribute2 != null)
		{
			style.FontStyle = dfMarkupStyle.ParseFontStyle(dfMarkupAttribute2.Value, style.FontStyle);
		}
		dfMarkupAttribute dfMarkupAttribute3 = this.findAttribute(new string[]
		{
			"size",
			"font-size"
		});
		if (dfMarkupAttribute3 != null)
		{
			style.FontSize = dfMarkupStyle.ParseSize(dfMarkupAttribute3.Value, style.FontSize);
		}
		dfMarkupAttribute dfMarkupAttribute4 = this.findAttribute(new string[]
		{
			"color"
		});
		if (dfMarkupAttribute4 != null)
		{
			Color color = dfMarkupStyle.ParseColor(dfMarkupAttribute4.Value, style.Color);
			color.a = style.Opacity;
			style.Color = color;
		}
		dfMarkupAttribute dfMarkupAttribute5 = this.findAttribute(new string[]
		{
			"align",
			"text-align"
		});
		if (dfMarkupAttribute5 != null)
		{
			style.Align = dfMarkupStyle.ParseTextAlignment(dfMarkupAttribute5.Value);
		}
		dfMarkupAttribute dfMarkupAttribute6 = this.findAttribute(new string[]
		{
			"valign",
			"vertical-align"
		});
		if (dfMarkupAttribute6 != null)
		{
			style.VerticalAlign = dfMarkupStyle.ParseVerticalAlignment(dfMarkupAttribute6.Value);
		}
		dfMarkupAttribute dfMarkupAttribute7 = this.findAttribute(new string[]
		{
			"line-height"
		});
		if (dfMarkupAttribute7 != null)
		{
			style.LineHeight = dfMarkupStyle.ParseSize(dfMarkupAttribute7.Value, style.LineHeight);
		}
		dfMarkupAttribute dfMarkupAttribute8 = this.findAttribute(new string[]
		{
			"text-decoration"
		});
		if (dfMarkupAttribute8 != null)
		{
			style.TextDecoration = dfMarkupStyle.ParseTextDecoration(dfMarkupAttribute8.Value);
		}
		dfMarkupAttribute dfMarkupAttribute9 = this.findAttribute(new string[]
		{
			"background",
			"background-color"
		});
		if (dfMarkupAttribute9 != null)
		{
			style.BackgroundColor = dfMarkupStyle.ParseColor(dfMarkupAttribute9.Value, Color.clear);
			style.BackgroundColor.a = style.Opacity;
		}
		return style;
	}

	// Token: 0x060008CF RID: 2255 RVA: 0x000271F4 File Offset: 0x000253F4
	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.Append("[");
		if (this.IsEndTag)
		{
			stringBuilder.Append("/");
		}
		stringBuilder.Append(this.TagName);
		for (int i = 0; i < this.Attributes.Count; i++)
		{
			stringBuilder.Append(" ");
			stringBuilder.Append(this.Attributes[i].ToString());
		}
		if (this.IsClosedTag)
		{
			stringBuilder.Append("/");
		}
		stringBuilder.Append("]");
		if (!this.IsClosedTag)
		{
			for (int j = 0; j < base.ChildNodes.Count; j++)
			{
				stringBuilder.Append(base.ChildNodes[j].ToString());
			}
			stringBuilder.Append("[/");
			stringBuilder.Append(this.TagName);
			stringBuilder.Append("]");
		}
		return stringBuilder.ToString();
	}

	// Token: 0x060008D0 RID: 2256 RVA: 0x000272F0 File Offset: 0x000254F0
	protected dfMarkupAttribute findAttribute(params string[] names)
	{
		for (int i = 0; i < this.Attributes.Count; i++)
		{
			for (int j = 0; j < names.Length; j++)
			{
				if (this.Attributes[i].Name == names[j])
				{
					return this.Attributes[i];
				}
			}
		}
		return null;
	}

	// Token: 0x0400044E RID: 1102
	private static int ELEMENTID;

	// Token: 0x04000453 RID: 1107
	public List<dfMarkupAttribute> Attributes;

	// Token: 0x04000454 RID: 1108
	private dfRichTextLabel owner;

	// Token: 0x04000455 RID: 1109
	private string id;
}

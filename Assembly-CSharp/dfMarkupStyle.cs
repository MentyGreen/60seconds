using System;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

// Token: 0x0200008F RID: 143
public struct dfMarkupStyle
{
	// Token: 0x170001F4 RID: 500
	// (get) Token: 0x060008B4 RID: 2228 RVA: 0x000269B1 File Offset: 0x00024BB1
	// (set) Token: 0x060008B5 RID: 2229 RVA: 0x000269D9 File Offset: 0x00024BD9
	public int LineHeight
	{
		get
		{
			if (this.lineHeight == 0)
			{
				return Mathf.CeilToInt((float)this.FontSize);
			}
			return Mathf.Max(this.FontSize, this.lineHeight);
		}
		set
		{
			this.lineHeight = value;
		}
	}

	// Token: 0x060008B6 RID: 2230 RVA: 0x000269E4 File Offset: 0x00024BE4
	public dfMarkupStyle(dfDynamicFont Font, int FontSize, FontStyle FontStyle)
	{
		this.Host = null;
		this.Atlas = null;
		this.Version = 0;
		this.Font = Font;
		this.FontSize = FontSize;
		this.FontStyle = FontStyle;
		this.Align = dfMarkupTextAlign.Left;
		this.VerticalAlign = dfMarkupVerticalAlign.Baseline;
		this.Color = Color.white;
		this.BackgroundColor = Color.clear;
		this.TextDecoration = dfMarkupTextDecoration.None;
		this.PreserveWhitespace = false;
		this.Preformatted = false;
		this.WordSpacing = 0;
		this.CharacterSpacing = 0;
		this.lineHeight = 0;
		this.Opacity = 1f;
	}

	// Token: 0x060008B7 RID: 2231 RVA: 0x00026A74 File Offset: 0x00024C74
	public static dfMarkupTextDecoration ParseTextDecoration(string value)
	{
		if (value == "underline")
		{
			return dfMarkupTextDecoration.Underline;
		}
		if (value == "overline")
		{
			return dfMarkupTextDecoration.Overline;
		}
		if (value == "line-through")
		{
			return dfMarkupTextDecoration.LineThrough;
		}
		return dfMarkupTextDecoration.None;
	}

	// Token: 0x060008B8 RID: 2232 RVA: 0x00026AA4 File Offset: 0x00024CA4
	public static dfMarkupVerticalAlign ParseVerticalAlignment(string value)
	{
		if (value == "top")
		{
			return dfMarkupVerticalAlign.Top;
		}
		if (value == "center" || value == "middle")
		{
			return dfMarkupVerticalAlign.Middle;
		}
		if (value == "bottom")
		{
			return dfMarkupVerticalAlign.Bottom;
		}
		return dfMarkupVerticalAlign.Baseline;
	}

	// Token: 0x060008B9 RID: 2233 RVA: 0x00026AE1 File Offset: 0x00024CE1
	public static dfMarkupTextAlign ParseTextAlignment(string value)
	{
		if (value == "right")
		{
			return dfMarkupTextAlign.Right;
		}
		if (value == "center")
		{
			return dfMarkupTextAlign.Center;
		}
		if (value == "justify")
		{
			return dfMarkupTextAlign.Justify;
		}
		return dfMarkupTextAlign.Left;
	}

	// Token: 0x060008BA RID: 2234 RVA: 0x00026B14 File Offset: 0x00024D14
	public static FontStyle ParseFontStyle(string value, FontStyle baseStyle)
	{
		if (value == "normal")
		{
			return FontStyle.Normal;
		}
		if (value == "bold")
		{
			if (baseStyle == FontStyle.Normal)
			{
				return FontStyle.Bold;
			}
			if (baseStyle == FontStyle.Italic)
			{
				return FontStyle.BoldAndItalic;
			}
		}
		else if (value == "italic")
		{
			if (baseStyle == FontStyle.Normal)
			{
				return FontStyle.Italic;
			}
			if (baseStyle == FontStyle.Bold)
			{
				return FontStyle.BoldAndItalic;
			}
		}
		return baseStyle;
	}

	// Token: 0x060008BB RID: 2235 RVA: 0x00026B64 File Offset: 0x00024D64
	public static int ParseSize(string value, int baseValue)
	{
		int num;
		if (value.Length > 1 && value.EndsWith("%") && int.TryParse(value.TrimEnd(new char[]
		{
			'%'
		}), out num))
		{
			return (int)((float)baseValue * ((float)num / 100f));
		}
		if (value.EndsWith("px"))
		{
			value = value.Substring(0, value.Length - 2);
		}
		int result;
		if (int.TryParse(value, out result))
		{
			return result;
		}
		return baseValue;
	}

	// Token: 0x060008BC RID: 2236 RVA: 0x00026BDC File Offset: 0x00024DDC
	public static Color ParseColor(string color, Color defaultColor)
	{
		Color result = defaultColor;
		Color color3;
		if (color.StartsWith("#"))
		{
			uint color2 = 0U;
			if (uint.TryParse(color.Substring(1), NumberStyles.HexNumber, null, out color2))
			{
				result = dfMarkupStyle.UIntToColor(color2);
			}
			else
			{
				result = Color.red;
			}
		}
		else if (dfMarkupStyle.namedColors.TryGetValue(color.ToLowerInvariant(), out color3))
		{
			result = color3;
		}
		return result;
	}

	// Token: 0x060008BD RID: 2237 RVA: 0x00026C40 File Offset: 0x00024E40
	private static Color32 UIntToColor(uint color)
	{
		byte r = (byte)(color >> 16);
		byte g = (byte)(color >> 8);
		byte b = (byte)color;
		return new Color32(r, g, b, byte.MaxValue);
	}

	// Token: 0x0400043C RID: 1084
	private static Dictionary<string, Color> namedColors = new Dictionary<string, Color>
	{
		{
			"aqua",
			dfMarkupStyle.UIntToColor(65535U)
		},
		{
			"black",
			Color.black
		},
		{
			"blue",
			Color.blue
		},
		{
			"cyan",
			Color.cyan
		},
		{
			"fuchsia",
			dfMarkupStyle.UIntToColor(16711935U)
		},
		{
			"gray",
			Color.gray
		},
		{
			"green",
			Color.green
		},
		{
			"lime",
			dfMarkupStyle.UIntToColor(65280U)
		},
		{
			"magenta",
			Color.magenta
		},
		{
			"maroon",
			dfMarkupStyle.UIntToColor(8388608U)
		},
		{
			"navy",
			dfMarkupStyle.UIntToColor(128U)
		},
		{
			"olive",
			dfMarkupStyle.UIntToColor(8421376U)
		},
		{
			"orange",
			dfMarkupStyle.UIntToColor(16753920U)
		},
		{
			"purple",
			dfMarkupStyle.UIntToColor(8388736U)
		},
		{
			"red",
			Color.red
		},
		{
			"silver",
			dfMarkupStyle.UIntToColor(12632256U)
		},
		{
			"teal",
			dfMarkupStyle.UIntToColor(32896U)
		},
		{
			"white",
			Color.white
		},
		{
			"yellow",
			Color.yellow
		}
	};

	// Token: 0x0400043D RID: 1085
	internal int Version;

	// Token: 0x0400043E RID: 1086
	public dfRichTextLabel Host;

	// Token: 0x0400043F RID: 1087
	public dfAtlas Atlas;

	// Token: 0x04000440 RID: 1088
	public dfDynamicFont Font;

	// Token: 0x04000441 RID: 1089
	public int FontSize;

	// Token: 0x04000442 RID: 1090
	public FontStyle FontStyle;

	// Token: 0x04000443 RID: 1091
	public dfMarkupTextDecoration TextDecoration;

	// Token: 0x04000444 RID: 1092
	public dfMarkupTextAlign Align;

	// Token: 0x04000445 RID: 1093
	public dfMarkupVerticalAlign VerticalAlign;

	// Token: 0x04000446 RID: 1094
	public Color Color;

	// Token: 0x04000447 RID: 1095
	public Color BackgroundColor;

	// Token: 0x04000448 RID: 1096
	public float Opacity;

	// Token: 0x04000449 RID: 1097
	public bool PreserveWhitespace;

	// Token: 0x0400044A RID: 1098
	public bool Preformatted;

	// Token: 0x0400044B RID: 1099
	public int WordSpacing;

	// Token: 0x0400044C RID: 1100
	public int CharacterSpacing;

	// Token: 0x0400044D RID: 1101
	private int lineHeight;
}

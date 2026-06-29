using System;
using System.Text.RegularExpressions;

// Token: 0x0200008B RID: 139
public struct dfMarkupBorders
{
	// Token: 0x170001F2 RID: 498
	// (get) Token: 0x060008AF RID: 2223 RVA: 0x000267C3 File Offset: 0x000249C3
	public int horizontal
	{
		get
		{
			return this.left + this.right;
		}
	}

	// Token: 0x170001F3 RID: 499
	// (get) Token: 0x060008B0 RID: 2224 RVA: 0x000267D2 File Offset: 0x000249D2
	public int vertical
	{
		get
		{
			return this.top + this.bottom;
		}
	}

	// Token: 0x060008B1 RID: 2225 RVA: 0x000267E1 File Offset: 0x000249E1
	public dfMarkupBorders(int left, int right, int top, int bottom)
	{
		this.left = left;
		this.top = top;
		this.right = right;
		this.bottom = bottom;
	}

	// Token: 0x060008B2 RID: 2226 RVA: 0x00026800 File Offset: 0x00024A00
	public static dfMarkupBorders Parse(string value)
	{
		dfMarkupBorders result = default(dfMarkupBorders);
		value = Regex.Replace(value, "\\s+", " ");
		string[] array = value.Split(new char[]
		{
			' '
		});
		if (array.Length == 1)
		{
			int num = dfMarkupStyle.ParseSize(value, 0);
			result.left = (result.right = num);
			result.top = (result.bottom = num);
		}
		else if (array.Length == 2)
		{
			int num2 = dfMarkupStyle.ParseSize(array[0], 0);
			result.top = (result.bottom = num2);
			int num3 = dfMarkupStyle.ParseSize(array[1], 0);
			result.left = (result.right = num3);
		}
		else if (array.Length == 3)
		{
			int num4 = dfMarkupStyle.ParseSize(array[0], 0);
			result.top = num4;
			int num5 = dfMarkupStyle.ParseSize(array[1], 0);
			result.left = (result.right = num5);
			int num6 = dfMarkupStyle.ParseSize(array[2], 0);
			result.bottom = num6;
		}
		else if (array.Length == 4)
		{
			int num7 = dfMarkupStyle.ParseSize(array[0], 0);
			result.top = num7;
			int num8 = dfMarkupStyle.ParseSize(array[1], 0);
			result.right = num8;
			int num9 = dfMarkupStyle.ParseSize(array[2], 0);
			result.bottom = num9;
			int num10 = dfMarkupStyle.ParseSize(array[3], 0);
			result.left = num10;
		}
		return result;
	}

	// Token: 0x060008B3 RID: 2227 RVA: 0x0002695C File Offset: 0x00024B5C
	public override string ToString()
	{
		return string.Format("[T:{0},R:{1},L:{2},B:{3}]", new object[]
		{
			this.top,
			this.right,
			this.left,
			this.bottom
		});
	}

	// Token: 0x04000428 RID: 1064
	public int left;

	// Token: 0x04000429 RID: 1065
	public int top;

	// Token: 0x0400042A RID: 1066
	public int right;

	// Token: 0x0400042B RID: 1067
	public int bottom;
}

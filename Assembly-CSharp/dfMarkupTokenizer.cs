using System;
using System.Collections.Generic;

// Token: 0x0200006D RID: 109
public class dfMarkupTokenizer : IDisposable, IPoolable
{
	// Token: 0x060007CB RID: 1995 RVA: 0x00021A48 File Offset: 0x0001FC48
	public static dfList<dfMarkupToken> Tokenize(string source)
	{
		dfList<dfMarkupToken> result;
		using (dfMarkupTokenizer dfMarkupTokenizer = (dfMarkupTokenizer.pool.Count > 0) ? dfMarkupTokenizer.pool.Pop() : new dfMarkupTokenizer())
		{
			result = dfMarkupTokenizer.tokenize(source);
		}
		return result;
	}

	// Token: 0x060007CC RID: 1996 RVA: 0x00021A9C File Offset: 0x0001FC9C
	public void Release()
	{
		this.source = null;
		this.index = 0;
		if (!dfMarkupTokenizer.pool.Contains(this))
		{
			dfMarkupTokenizer.pool.Add(this);
		}
	}

	// Token: 0x060007CD RID: 1997 RVA: 0x00021AC4 File Offset: 0x0001FCC4
	private dfList<dfMarkupToken> tokenize(string source)
	{
		dfList<dfMarkupToken> dfList = dfList<dfMarkupToken>.Obtain();
		dfList.EnsureCapacity(this.estimateTokenCount(source));
		dfList.AutoReleaseItems = true;
		this.source = source;
		this.index = 0;
		while (this.index < source.Length)
		{
			char c = this.Peek();
			if (this.AtTagPosition())
			{
				dfMarkupToken dfMarkupToken = this.parseTag();
				if (dfMarkupToken != null)
				{
					dfList.Add(dfMarkupToken);
				}
			}
			else
			{
				dfMarkupToken dfMarkupToken2 = null;
				if (char.IsWhiteSpace(c))
				{
					if (c != '\r')
					{
						dfMarkupToken2 = this.parseWhitespace();
					}
				}
				else
				{
					dfMarkupToken2 = this.parseNonWhitespace();
				}
				if (dfMarkupToken2 == null)
				{
					this.Advance();
				}
				else
				{
					dfList.Add(dfMarkupToken2);
				}
			}
		}
		return dfList;
	}

	// Token: 0x060007CE RID: 1998 RVA: 0x00021B60 File Offset: 0x0001FD60
	private int estimateTokenCount(string source)
	{
		if (string.IsNullOrEmpty(source))
		{
			return 0;
		}
		int num = 1;
		bool flag = char.IsWhiteSpace(source[0]);
		for (int i = 1; i < source.Length; i++)
		{
			char c = source[i];
			if (char.IsControl(c) || c == '<')
			{
				num++;
			}
			else
			{
				bool flag2 = char.IsWhiteSpace(c);
				if (flag2 != flag)
				{
					num++;
					flag = flag2;
				}
			}
		}
		return num;
	}

	// Token: 0x060007CF RID: 1999 RVA: 0x00021BC8 File Offset: 0x0001FDC8
	private bool AtTagPosition()
	{
		if (this.Peek() != '[')
		{
			return false;
		}
		char c = this.Peek(1);
		if (c == '/')
		{
			return char.IsLetter(this.Peek(2)) && this.isValidTag(this.index + 2, true);
		}
		return char.IsLetter(c) && this.isValidTag(this.index + 1, false);
	}

	// Token: 0x060007D0 RID: 2000 RVA: 0x00021C28 File Offset: 0x0001FE28
	private bool isValidTag(int index, bool endTag)
	{
		for (int i = 0; i < dfMarkupTokenizer.validTags.Count; i++)
		{
			string text = dfMarkupTokenizer.validTags[i];
			bool flag = true;
			int num = 0;
			while (num < text.Length - 1 && num + index < this.source.Length - 1 && (endTag || this.source[num + index] != ' ') && this.source[num + index] != ']')
			{
				if (char.ToLowerInvariant(text[num]) != char.ToLowerInvariant(this.source[num + index]))
				{
					flag = false;
					break;
				}
				num++;
			}
			if (flag)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x060007D1 RID: 2001 RVA: 0x00021CD8 File Offset: 0x0001FED8
	private dfMarkupToken parseQuotedString()
	{
		char c = this.Peek();
		if (c != '"' && c != '\'')
		{
			return null;
		}
		this.Advance();
		int startIndex = this.index;
		int num = this.index;
		while (this.index < this.source.Length && this.Advance() != c)
		{
			num++;
		}
		if (this.Peek() == c)
		{
			this.Advance();
		}
		return dfMarkupToken.Obtain(this.source, dfMarkupTokenType.Text, startIndex, num);
	}

	// Token: 0x060007D2 RID: 2002 RVA: 0x00021D50 File Offset: 0x0001FF50
	private dfMarkupToken parseNonWhitespace()
	{
		int startIndex = this.index;
		int num = this.index;
		while (this.index < this.source.Length && !char.IsWhiteSpace(this.Advance()) && !this.AtTagPosition())
		{
			num++;
		}
		return dfMarkupToken.Obtain(this.source, dfMarkupTokenType.Text, startIndex, num);
	}

	// Token: 0x060007D3 RID: 2003 RVA: 0x00021DA8 File Offset: 0x0001FFA8
	private dfMarkupToken parseWhitespace()
	{
		int num = this.index;
		int num2 = this.index;
		if (this.Peek() == '\n')
		{
			this.Advance();
			return dfMarkupToken.Obtain(this.source, dfMarkupTokenType.Newline, num, num);
		}
		while (this.index < this.source.Length)
		{
			char c = this.Advance();
			if (c == '\n' || c == '\r' || !char.IsWhiteSpace(c))
			{
				break;
			}
			num2++;
		}
		return dfMarkupToken.Obtain(this.source, dfMarkupTokenType.Whitespace, num, num2);
	}

	// Token: 0x060007D4 RID: 2004 RVA: 0x00021E24 File Offset: 0x00020024
	private dfMarkupToken parseWord()
	{
		if (!char.IsLetter(this.Peek()))
		{
			return null;
		}
		int startIndex = this.index;
		int num = this.index;
		while (this.index < this.source.Length && char.IsLetter(this.Advance()))
		{
			num++;
		}
		return dfMarkupToken.Obtain(this.source, dfMarkupTokenType.Text, startIndex, num);
	}

	// Token: 0x060007D5 RID: 2005 RVA: 0x00021E84 File Offset: 0x00020084
	private dfMarkupToken parseTag()
	{
		if (this.Peek() != '[')
		{
			return null;
		}
		if (this.Peek(1) == '/')
		{
			return this.parseEndTag();
		}
		this.Advance();
		if (!char.IsLetterOrDigit(this.Peek()))
		{
			return null;
		}
		int startIndex = this.index;
		int num = this.index;
		while (this.index < this.source.Length && char.IsLetterOrDigit(this.Advance()))
		{
			num++;
		}
		dfMarkupToken dfMarkupToken = dfMarkupToken.Obtain(this.source, dfMarkupTokenType.StartTag, startIndex, num);
		if (this.index < this.source.Length && this.Peek() != ']')
		{
			if (char.IsWhiteSpace(this.Peek()))
			{
				this.parseWhitespace();
			}
			int startIndex2 = this.index;
			int num2 = this.index;
			if (this.Peek() == '"')
			{
				dfMarkupToken dfMarkupToken2 = this.parseQuotedString();
				dfMarkupToken.AddAttribute(dfMarkupToken2, dfMarkupToken2);
			}
			else
			{
				while (this.index < this.source.Length && this.Advance() != ']')
				{
					num2++;
				}
				dfMarkupToken dfMarkupToken3 = dfMarkupToken.Obtain(this.source, dfMarkupTokenType.Text, startIndex2, num2);
				dfMarkupToken.AddAttribute(dfMarkupToken3, dfMarkupToken3);
			}
		}
		if (this.Peek() == ']')
		{
			this.Advance();
		}
		return dfMarkupToken;
	}

	// Token: 0x060007D6 RID: 2006 RVA: 0x00021FBC File Offset: 0x000201BC
	private dfMarkupToken parseAttributeValue()
	{
		int startIndex = this.index;
		int num = this.index;
		while (this.index < this.source.Length)
		{
			char c = this.Advance();
			if (c == ']' || char.IsWhiteSpace(c))
			{
				break;
			}
			num++;
		}
		return dfMarkupToken.Obtain(this.source, dfMarkupTokenType.Text, startIndex, num);
	}

	// Token: 0x060007D7 RID: 2007 RVA: 0x00022014 File Offset: 0x00020214
	private dfMarkupToken parseEndTag()
	{
		this.Advance(2);
		int startIndex = this.index;
		int num = this.index;
		while (this.index < this.source.Length && char.IsLetterOrDigit(this.Advance()))
		{
			num++;
		}
		if (this.Peek() == ']')
		{
			this.Advance();
		}
		return dfMarkupToken.Obtain(this.source, dfMarkupTokenType.EndTag, startIndex, num);
	}

	// Token: 0x060007D8 RID: 2008 RVA: 0x0002207C File Offset: 0x0002027C
	private char Peek()
	{
		return this.Peek(0);
	}

	// Token: 0x060007D9 RID: 2009 RVA: 0x00022085 File Offset: 0x00020285
	private char Peek(int offset)
	{
		if (this.index + offset > this.source.Length - 1)
		{
			return '\0';
		}
		return this.source[this.index + offset];
	}

	// Token: 0x060007DA RID: 2010 RVA: 0x000220B3 File Offset: 0x000202B3
	private char Advance()
	{
		return this.Advance(1);
	}

	// Token: 0x060007DB RID: 2011 RVA: 0x000220BC File Offset: 0x000202BC
	private char Advance(int amount)
	{
		this.index += amount;
		return this.Peek();
	}

	// Token: 0x060007DC RID: 2012 RVA: 0x000220D2 File Offset: 0x000202D2
	public void Dispose()
	{
		this.Release();
	}

	// Token: 0x040003B1 RID: 945
	private static dfList<dfMarkupTokenizer> pool = new dfList<dfMarkupTokenizer>();

	// Token: 0x040003B2 RID: 946
	private static List<string> validTags = new List<string>
	{
		"color",
		"sprite"
	};

	// Token: 0x040003B3 RID: 947
	private string source;

	// Token: 0x040003B4 RID: 948
	private int index;
}

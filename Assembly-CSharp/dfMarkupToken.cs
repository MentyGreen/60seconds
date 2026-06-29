using System;
using UnityEngine;

// Token: 0x0200006B RID: 107
public class dfMarkupToken : IPoolable
{
	// Token: 0x060007AF RID: 1967 RVA: 0x0002168F File Offset: 0x0001F88F
	protected dfMarkupToken()
	{
	}

	// Token: 0x060007B0 RID: 1968 RVA: 0x000216A4 File Offset: 0x0001F8A4
	public static dfMarkupToken Obtain(string source, dfMarkupTokenType type, int startIndex, int endIndex)
	{
		dfMarkupToken dfMarkupToken = (dfMarkupToken.pool.Count > 0) ? dfMarkupToken.pool.Pop() : new dfMarkupToken();
		dfMarkupToken.inUse = true;
		dfMarkupToken.Source = source;
		dfMarkupToken.TokenType = type;
		dfMarkupToken.StartOffset = startIndex;
		dfMarkupToken.EndOffset = Mathf.Min(source.Length - 1, endIndex);
		return dfMarkupToken;
	}

	// Token: 0x060007B1 RID: 1969 RVA: 0x00021700 File Offset: 0x0001F900
	public void Release()
	{
		if (!this.inUse)
		{
			return;
		}
		this.inUse = false;
		this.value = null;
		this.Source = null;
		this.TokenType = dfMarkupTokenType.Invalid;
		this.Width = (this.Height = 0);
		this.StartOffset = (this.EndOffset = 0);
		this.attributes.ReleaseItems();
		dfMarkupToken.pool.Add(this);
	}

	// Token: 0x170001D5 RID: 469
	// (get) Token: 0x060007B2 RID: 1970 RVA: 0x00021768 File Offset: 0x0001F968
	public int AttributeCount
	{
		get
		{
			return this.attributes.Count;
		}
	}

	// Token: 0x170001D6 RID: 470
	// (get) Token: 0x060007B3 RID: 1971 RVA: 0x00021775 File Offset: 0x0001F975
	// (set) Token: 0x060007B4 RID: 1972 RVA: 0x0002177D File Offset: 0x0001F97D
	public dfMarkupTokenType TokenType { get; private set; }

	// Token: 0x170001D7 RID: 471
	// (get) Token: 0x060007B5 RID: 1973 RVA: 0x00021786 File Offset: 0x0001F986
	// (set) Token: 0x060007B6 RID: 1974 RVA: 0x0002178E File Offset: 0x0001F98E
	public string Source { get; private set; }

	// Token: 0x170001D8 RID: 472
	// (get) Token: 0x060007B7 RID: 1975 RVA: 0x00021797 File Offset: 0x0001F997
	// (set) Token: 0x060007B8 RID: 1976 RVA: 0x0002179F File Offset: 0x0001F99F
	public int StartOffset { get; private set; }

	// Token: 0x170001D9 RID: 473
	// (get) Token: 0x060007B9 RID: 1977 RVA: 0x000217A8 File Offset: 0x0001F9A8
	// (set) Token: 0x060007BA RID: 1978 RVA: 0x000217B0 File Offset: 0x0001F9B0
	public int EndOffset { get; private set; }

	// Token: 0x170001DA RID: 474
	// (get) Token: 0x060007BB RID: 1979 RVA: 0x000217B9 File Offset: 0x0001F9B9
	// (set) Token: 0x060007BC RID: 1980 RVA: 0x000217C1 File Offset: 0x0001F9C1
	public int Width { get; internal set; }

	// Token: 0x170001DB RID: 475
	// (get) Token: 0x060007BD RID: 1981 RVA: 0x000217CA File Offset: 0x0001F9CA
	// (set) Token: 0x060007BE RID: 1982 RVA: 0x000217D2 File Offset: 0x0001F9D2
	public int Height { get; set; }

	// Token: 0x170001DC RID: 476
	// (get) Token: 0x060007BF RID: 1983 RVA: 0x000217DB File Offset: 0x0001F9DB
	public int Length
	{
		get
		{
			return this.EndOffset - this.StartOffset + 1;
		}
	}

	// Token: 0x170001DD RID: 477
	// (get) Token: 0x060007C0 RID: 1984 RVA: 0x000217EC File Offset: 0x0001F9EC
	public string Value
	{
		get
		{
			if (this.value == null)
			{
				int length = Mathf.Min(this.EndOffset - this.StartOffset + 1, this.Source.Length - this.StartOffset);
				this.value = this.Source.Substring(this.StartOffset, length);
			}
			return this.value;
		}
	}

	// Token: 0x170001DE RID: 478
	public char this[int index]
	{
		get
		{
			if (index < 0 || index >= this.Length)
			{
				throw new IndexOutOfRangeException(string.Format("Index {0} is out of range ({2}:{1})", index, this.Length, this.Value));
			}
			return this.Source[this.StartOffset + index];
		}
	}

	// Token: 0x060007C2 RID: 1986 RVA: 0x0002189C File Offset: 0x0001FA9C
	internal bool Matches(dfMarkupToken other)
	{
		int length = this.Length;
		if (length != other.Length)
		{
			return false;
		}
		for (int i = 0; i < length; i++)
		{
			if (char.ToLower(this.Source[this.StartOffset + i]) != char.ToLower(other.Source[other.StartOffset + i]))
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x060007C3 RID: 1987 RVA: 0x000218FC File Offset: 0x0001FAFC
	internal bool Matches(string value)
	{
		int length = this.Length;
		if (length != value.Length)
		{
			return false;
		}
		for (int i = 0; i < length; i++)
		{
			if (char.ToLower(this.Source[this.StartOffset + i]) != char.ToLower(value[i]))
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x060007C4 RID: 1988 RVA: 0x00021950 File Offset: 0x0001FB50
	internal void AddAttribute(dfMarkupToken key, dfMarkupToken value)
	{
		this.attributes.Add(dfMarkupTokenAttribute.Obtain(key, value));
	}

	// Token: 0x060007C5 RID: 1989 RVA: 0x00021964 File Offset: 0x0001FB64
	public dfMarkupTokenAttribute GetAttribute(int index)
	{
		if (index < 0 || index >= this.attributes.Count)
		{
			throw new IndexOutOfRangeException("Invalid attribute index: " + index.ToString());
		}
		return this.attributes[index];
	}

	// Token: 0x040003A4 RID: 932
	private static dfList<dfMarkupToken> pool = new dfList<dfMarkupToken>();

	// Token: 0x040003A5 RID: 933
	private bool inUse;

	// Token: 0x040003A6 RID: 934
	private string value;

	// Token: 0x040003A7 RID: 935
	private dfList<dfMarkupTokenAttribute> attributes = new dfList<dfMarkupTokenAttribute>();
}

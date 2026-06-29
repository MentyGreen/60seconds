using System;

// Token: 0x0200006C RID: 108
public class dfMarkupTokenAttribute : IPoolable
{
	// Token: 0x060007C7 RID: 1991 RVA: 0x000219A7 File Offset: 0x0001FBA7
	private dfMarkupTokenAttribute()
	{
	}

	// Token: 0x060007C8 RID: 1992 RVA: 0x000219AF File Offset: 0x0001FBAF
	public static dfMarkupTokenAttribute Obtain(dfMarkupToken key, dfMarkupToken value)
	{
		dfMarkupTokenAttribute dfMarkupTokenAttribute = (dfMarkupTokenAttribute.pool.Count > 0) ? dfMarkupTokenAttribute.pool.Pop() : new dfMarkupTokenAttribute();
		dfMarkupTokenAttribute.Key = key;
		dfMarkupTokenAttribute.Value = value;
		return dfMarkupTokenAttribute;
	}

	// Token: 0x060007C9 RID: 1993 RVA: 0x000219E0 File Offset: 0x0001FBE0
	public void Release()
	{
		if (this.Key != null)
		{
			this.Key.Release();
			this.Key = null;
		}
		if (this.Value != null)
		{
			this.Value.Release();
			this.Value = null;
		}
		if (!dfMarkupTokenAttribute.pool.Contains(this))
		{
			dfMarkupTokenAttribute.pool.Add(this);
		}
	}

	// Token: 0x040003AE RID: 942
	public dfMarkupToken Key;

	// Token: 0x040003AF RID: 943
	public dfMarkupToken Value;

	// Token: 0x040003B0 RID: 944
	private static dfList<dfMarkupTokenAttribute> pool = new dfList<dfMarkupTokenAttribute>();
}

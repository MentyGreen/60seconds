using System;

// Token: 0x02000036 RID: 54
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field)]
public class dfCategoryAttribute : Attribute
{
	// Token: 0x17000165 RID: 357
	// (get) Token: 0x060005E9 RID: 1513 RVA: 0x0001BEF0 File Offset: 0x0001A0F0
	// (set) Token: 0x060005EA RID: 1514 RVA: 0x0001BEF8 File Offset: 0x0001A0F8
	public string Category { get; private set; }

	// Token: 0x060005EB RID: 1515 RVA: 0x0001BF01 File Offset: 0x0001A101
	public dfCategoryAttribute(string category)
	{
		this.Category = category;
	}
}

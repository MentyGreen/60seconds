using System;

// Token: 0x02000059 RID: 89
[AttributeUsage(AttributeTargets.Delegate, Inherited = true, AllowMultiple = false)]
public class dfEventCategoryAttribute : Attribute
{
	// Token: 0x17000171 RID: 369
	// (get) Token: 0x0600064C RID: 1612 RVA: 0x0001D1E0 File Offset: 0x0001B3E0
	// (set) Token: 0x0600064D RID: 1613 RVA: 0x0001D1E8 File Offset: 0x0001B3E8
	public string Category { get; private set; }

	// Token: 0x0600064E RID: 1614 RVA: 0x0001D1F1 File Offset: 0x0001B3F1
	public dfEventCategoryAttribute(string category)
	{
		this.Category = category;
	}
}

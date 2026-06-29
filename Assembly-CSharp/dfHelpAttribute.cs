using System;

// Token: 0x02000038 RID: 56
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field)]
public class dfHelpAttribute : Attribute
{
	// Token: 0x17000167 RID: 359
	// (get) Token: 0x060005EF RID: 1519 RVA: 0x0001BF30 File Offset: 0x0001A130
	// (set) Token: 0x060005F0 RID: 1520 RVA: 0x0001BF38 File Offset: 0x0001A138
	public string HelpURL { get; private set; }

	// Token: 0x060005F1 RID: 1521 RVA: 0x0001BF41 File Offset: 0x0001A141
	public dfHelpAttribute(string url)
	{
		this.HelpURL = url;
	}
}

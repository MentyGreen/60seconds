using System;

// Token: 0x02000085 RID: 133
[AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
public class dfMarkupTagInfoAttribute : Attribute
{
	// Token: 0x170001EB RID: 491
	// (get) Token: 0x06000881 RID: 2177 RVA: 0x00025EAA File Offset: 0x000240AA
	// (set) Token: 0x06000882 RID: 2178 RVA: 0x00025EB2 File Offset: 0x000240B2
	public string TagName { get; set; }

	// Token: 0x06000883 RID: 2179 RVA: 0x00025EBB File Offset: 0x000240BB
	public dfMarkupTagInfoAttribute(string tagName)
	{
		this.TagName = tagName;
	}
}

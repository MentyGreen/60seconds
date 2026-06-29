using System;

// Token: 0x02000088 RID: 136
public class dfMarkupAttribute
{
	// Token: 0x170001F0 RID: 496
	// (get) Token: 0x06000899 RID: 2201 RVA: 0x000261E6 File Offset: 0x000243E6
	// (set) Token: 0x0600089A RID: 2202 RVA: 0x000261EE File Offset: 0x000243EE
	public string Name { get; set; }

	// Token: 0x170001F1 RID: 497
	// (get) Token: 0x0600089B RID: 2203 RVA: 0x000261F7 File Offset: 0x000243F7
	// (set) Token: 0x0600089C RID: 2204 RVA: 0x000261FF File Offset: 0x000243FF
	public string Value { get; set; }

	// Token: 0x0600089D RID: 2205 RVA: 0x00026208 File Offset: 0x00024408
	public dfMarkupAttribute(string name, string value)
	{
		this.Name = name;
		this.Value = value;
	}

	// Token: 0x0600089E RID: 2206 RVA: 0x0002621E File Offset: 0x0002441E
	public override string ToString()
	{
		return string.Format("{0}='{1}'", this.Name, this.Value);
	}
}

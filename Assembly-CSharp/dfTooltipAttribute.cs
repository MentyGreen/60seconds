using System;

// Token: 0x02000037 RID: 55
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field)]
public class dfTooltipAttribute : Attribute
{
	// Token: 0x17000166 RID: 358
	// (get) Token: 0x060005EC RID: 1516 RVA: 0x0001BF10 File Offset: 0x0001A110
	// (set) Token: 0x060005ED RID: 1517 RVA: 0x0001BF18 File Offset: 0x0001A118
	public string Tooltip { get; private set; }

	// Token: 0x060005EE RID: 1518 RVA: 0x0001BF21 File Offset: 0x0001A121
	public dfTooltipAttribute(string tooltip)
	{
		this.Tooltip = tooltip;
	}
}

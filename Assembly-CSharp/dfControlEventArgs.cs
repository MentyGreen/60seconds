using System;

// Token: 0x0200005A RID: 90
public class dfControlEventArgs
{
	// Token: 0x17000172 RID: 370
	// (get) Token: 0x0600064F RID: 1615 RVA: 0x0001D200 File Offset: 0x0001B400
	// (set) Token: 0x06000650 RID: 1616 RVA: 0x0001D208 File Offset: 0x0001B408
	public dfControl Source { get; internal set; }

	// Token: 0x17000173 RID: 371
	// (get) Token: 0x06000651 RID: 1617 RVA: 0x0001D211 File Offset: 0x0001B411
	// (set) Token: 0x06000652 RID: 1618 RVA: 0x0001D219 File Offset: 0x0001B419
	public bool Used { get; private set; }

	// Token: 0x06000653 RID: 1619 RVA: 0x0001D222 File Offset: 0x0001B422
	internal dfControlEventArgs(dfControl Target)
	{
		this.Source = Target;
	}

	// Token: 0x06000654 RID: 1620 RVA: 0x0001D231 File Offset: 0x0001B431
	public void Use()
	{
		this.Used = true;
	}
}

using System;

// Token: 0x0200005B RID: 91
public class dfFocusEventArgs : dfControlEventArgs
{
	// Token: 0x17000174 RID: 372
	// (get) Token: 0x06000655 RID: 1621 RVA: 0x0001D23A File Offset: 0x0001B43A
	public dfControl GotFocus
	{
		get
		{
			return base.Source;
		}
	}

	// Token: 0x17000175 RID: 373
	// (get) Token: 0x06000656 RID: 1622 RVA: 0x0001D242 File Offset: 0x0001B442
	// (set) Token: 0x06000657 RID: 1623 RVA: 0x0001D24A File Offset: 0x0001B44A
	public dfControl LostFocus { get; private set; }

	// Token: 0x06000658 RID: 1624 RVA: 0x0001D253 File Offset: 0x0001B453
	internal dfFocusEventArgs(dfControl GotFocus, dfControl LostFocus) : base(GotFocus)
	{
		this.LostFocus = LostFocus;
	}
}

using System;
using UnityEngine;

// Token: 0x0200005D RID: 93
public class dfKeyEventArgs : dfControlEventArgs
{
	// Token: 0x1700017B RID: 379
	// (get) Token: 0x06000665 RID: 1637 RVA: 0x0001D2EF File Offset: 0x0001B4EF
	// (set) Token: 0x06000666 RID: 1638 RVA: 0x0001D2F7 File Offset: 0x0001B4F7
	public KeyCode KeyCode { get; set; }

	// Token: 0x1700017C RID: 380
	// (get) Token: 0x06000667 RID: 1639 RVA: 0x0001D300 File Offset: 0x0001B500
	// (set) Token: 0x06000668 RID: 1640 RVA: 0x0001D308 File Offset: 0x0001B508
	public char Character { get; set; }

	// Token: 0x1700017D RID: 381
	// (get) Token: 0x06000669 RID: 1641 RVA: 0x0001D311 File Offset: 0x0001B511
	// (set) Token: 0x0600066A RID: 1642 RVA: 0x0001D319 File Offset: 0x0001B519
	public bool Control { get; set; }

	// Token: 0x1700017E RID: 382
	// (get) Token: 0x0600066B RID: 1643 RVA: 0x0001D322 File Offset: 0x0001B522
	// (set) Token: 0x0600066C RID: 1644 RVA: 0x0001D32A File Offset: 0x0001B52A
	public bool Shift { get; set; }

	// Token: 0x1700017F RID: 383
	// (get) Token: 0x0600066D RID: 1645 RVA: 0x0001D333 File Offset: 0x0001B533
	// (set) Token: 0x0600066E RID: 1646 RVA: 0x0001D33B File Offset: 0x0001B53B
	public bool Alt { get; set; }

	// Token: 0x0600066F RID: 1647 RVA: 0x0001D344 File Offset: 0x0001B544
	internal dfKeyEventArgs(dfControl source, KeyCode Key, bool Control, bool Shift, bool Alt) : base(source)
	{
		this.KeyCode = Key;
		this.Control = Control;
		this.Shift = Shift;
		this.Alt = Alt;
	}

	// Token: 0x06000670 RID: 1648 RVA: 0x0001D36C File Offset: 0x0001B56C
	public override string ToString()
	{
		return string.Format("Key: {0}, Control: {1}, Shift: {2}, Alt: {3}", new object[]
		{
			this.KeyCode,
			this.Control,
			this.Shift,
			this.Alt
		});
	}
}

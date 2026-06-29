using System;
using UnityEngine;

// Token: 0x0200005C RID: 92
public class dfDragEventArgs : dfControlEventArgs
{
	// Token: 0x17000176 RID: 374
	// (get) Token: 0x06000659 RID: 1625 RVA: 0x0001D263 File Offset: 0x0001B463
	// (set) Token: 0x0600065A RID: 1626 RVA: 0x0001D26B File Offset: 0x0001B46B
	public dfDragDropState State { get; set; }

	// Token: 0x17000177 RID: 375
	// (get) Token: 0x0600065B RID: 1627 RVA: 0x0001D274 File Offset: 0x0001B474
	// (set) Token: 0x0600065C RID: 1628 RVA: 0x0001D27C File Offset: 0x0001B47C
	public object Data { get; set; }

	// Token: 0x17000178 RID: 376
	// (get) Token: 0x0600065D RID: 1629 RVA: 0x0001D285 File Offset: 0x0001B485
	// (set) Token: 0x0600065E RID: 1630 RVA: 0x0001D28D File Offset: 0x0001B48D
	public Vector2 Position { get; set; }

	// Token: 0x17000179 RID: 377
	// (get) Token: 0x0600065F RID: 1631 RVA: 0x0001D296 File Offset: 0x0001B496
	// (set) Token: 0x06000660 RID: 1632 RVA: 0x0001D29E File Offset: 0x0001B49E
	public dfControl Target { get; set; }

	// Token: 0x1700017A RID: 378
	// (get) Token: 0x06000661 RID: 1633 RVA: 0x0001D2A7 File Offset: 0x0001B4A7
	// (set) Token: 0x06000662 RID: 1634 RVA: 0x0001D2AF File Offset: 0x0001B4AF
	public Ray Ray { get; set; }

	// Token: 0x06000663 RID: 1635 RVA: 0x0001D2B8 File Offset: 0x0001B4B8
	internal dfDragEventArgs(dfControl source) : base(source)
	{
		this.State = dfDragDropState.None;
	}

	// Token: 0x06000664 RID: 1636 RVA: 0x0001D2C8 File Offset: 0x0001B4C8
	internal dfDragEventArgs(dfControl source, dfDragDropState state, object data, Ray ray, Vector2 position) : base(source)
	{
		this.Data = data;
		this.State = state;
		this.Position = position;
		this.Ray = ray;
	}
}

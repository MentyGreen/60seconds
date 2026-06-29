using System;
using UnityEngine;

// Token: 0x0200005E RID: 94
public class dfMouseEventArgs : dfControlEventArgs
{
	// Token: 0x17000180 RID: 384
	// (get) Token: 0x06000671 RID: 1649 RVA: 0x0001D3C1 File Offset: 0x0001B5C1
	// (set) Token: 0x06000672 RID: 1650 RVA: 0x0001D3C9 File Offset: 0x0001B5C9
	public dfMouseButtons Buttons { get; private set; }

	// Token: 0x17000181 RID: 385
	// (get) Token: 0x06000673 RID: 1651 RVA: 0x0001D3D2 File Offset: 0x0001B5D2
	// (set) Token: 0x06000674 RID: 1652 RVA: 0x0001D3DA File Offset: 0x0001B5DA
	public int Clicks { get; private set; }

	// Token: 0x17000182 RID: 386
	// (get) Token: 0x06000675 RID: 1653 RVA: 0x0001D3E3 File Offset: 0x0001B5E3
	// (set) Token: 0x06000676 RID: 1654 RVA: 0x0001D3EB File Offset: 0x0001B5EB
	public float WheelDelta { get; private set; }

	// Token: 0x17000183 RID: 387
	// (get) Token: 0x06000677 RID: 1655 RVA: 0x0001D3F4 File Offset: 0x0001B5F4
	// (set) Token: 0x06000678 RID: 1656 RVA: 0x0001D3FC File Offset: 0x0001B5FC
	public Vector2 MoveDelta { get; set; }

	// Token: 0x17000184 RID: 388
	// (get) Token: 0x06000679 RID: 1657 RVA: 0x0001D405 File Offset: 0x0001B605
	// (set) Token: 0x0600067A RID: 1658 RVA: 0x0001D40D File Offset: 0x0001B60D
	public Vector2 Position { get; set; }

	// Token: 0x17000185 RID: 389
	// (get) Token: 0x0600067B RID: 1659 RVA: 0x0001D416 File Offset: 0x0001B616
	// (set) Token: 0x0600067C RID: 1660 RVA: 0x0001D41E File Offset: 0x0001B61E
	public Ray Ray { get; set; }

	// Token: 0x0600067D RID: 1661 RVA: 0x0001D427 File Offset: 0x0001B627
	public dfMouseEventArgs(dfControl Source, dfMouseButtons button, int clicks, Ray ray, Vector2 location, float wheel) : base(Source)
	{
		this.Buttons = button;
		this.Clicks = clicks;
		this.Position = location;
		this.WheelDelta = wheel;
		this.Ray = ray;
	}

	// Token: 0x0600067E RID: 1662 RVA: 0x0001D456 File Offset: 0x0001B656
	public dfMouseEventArgs(dfControl Source) : base(Source)
	{
		this.Buttons = dfMouseButtons.None;
		this.Clicks = 0;
		this.Position = Vector2.zero;
		this.WheelDelta = 0f;
	}
}

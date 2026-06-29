using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000062 RID: 98
public abstract class dfFontRendererBase : IDisposable
{
	// Token: 0x1700019E RID: 414
	// (get) Token: 0x060006AD RID: 1709 RVA: 0x0001D7E8 File Offset: 0x0001B9E8
	// (set) Token: 0x060006AE RID: 1710 RVA: 0x0001D7F0 File Offset: 0x0001B9F0
	public dfFontBase Font { get; protected set; }

	// Token: 0x1700019F RID: 415
	// (get) Token: 0x060006AF RID: 1711 RVA: 0x0001D7F9 File Offset: 0x0001B9F9
	// (set) Token: 0x060006B0 RID: 1712 RVA: 0x0001D801 File Offset: 0x0001BA01
	public Vector2 MaxSize { get; set; }

	// Token: 0x170001A0 RID: 416
	// (get) Token: 0x060006B1 RID: 1713 RVA: 0x0001D80A File Offset: 0x0001BA0A
	// (set) Token: 0x060006B2 RID: 1714 RVA: 0x0001D812 File Offset: 0x0001BA12
	public float PixelRatio { get; set; }

	// Token: 0x170001A1 RID: 417
	// (get) Token: 0x060006B3 RID: 1715 RVA: 0x0001D81B File Offset: 0x0001BA1B
	// (set) Token: 0x060006B4 RID: 1716 RVA: 0x0001D823 File Offset: 0x0001BA23
	public float TextScale { get; set; }

	// Token: 0x170001A2 RID: 418
	// (get) Token: 0x060006B5 RID: 1717 RVA: 0x0001D82C File Offset: 0x0001BA2C
	// (set) Token: 0x060006B6 RID: 1718 RVA: 0x0001D834 File Offset: 0x0001BA34
	public int CharacterSpacing { get; set; }

	// Token: 0x170001A3 RID: 419
	// (get) Token: 0x060006B7 RID: 1719 RVA: 0x0001D83D File Offset: 0x0001BA3D
	// (set) Token: 0x060006B8 RID: 1720 RVA: 0x0001D845 File Offset: 0x0001BA45
	public Vector3 VectorOffset { get; set; }

	// Token: 0x170001A4 RID: 420
	// (get) Token: 0x060006B9 RID: 1721 RVA: 0x0001D84E File Offset: 0x0001BA4E
	// (set) Token: 0x060006BA RID: 1722 RVA: 0x0001D856 File Offset: 0x0001BA56
	public bool ProcessMarkup { get; set; }

	// Token: 0x170001A5 RID: 421
	// (get) Token: 0x060006BB RID: 1723 RVA: 0x0001D85F File Offset: 0x0001BA5F
	// (set) Token: 0x060006BC RID: 1724 RVA: 0x0001D867 File Offset: 0x0001BA67
	public bool WordWrap { get; set; }

	// Token: 0x170001A6 RID: 422
	// (get) Token: 0x060006BD RID: 1725 RVA: 0x0001D870 File Offset: 0x0001BA70
	// (set) Token: 0x060006BE RID: 1726 RVA: 0x0001D878 File Offset: 0x0001BA78
	public bool MultiLine { get; set; }

	// Token: 0x170001A7 RID: 423
	// (get) Token: 0x060006BF RID: 1727 RVA: 0x0001D881 File Offset: 0x0001BA81
	// (set) Token: 0x060006C0 RID: 1728 RVA: 0x0001D889 File Offset: 0x0001BA89
	public bool OverrideMarkupColors { get; set; }

	// Token: 0x170001A8 RID: 424
	// (get) Token: 0x060006C1 RID: 1729 RVA: 0x0001D892 File Offset: 0x0001BA92
	// (set) Token: 0x060006C2 RID: 1730 RVA: 0x0001D89A File Offset: 0x0001BA9A
	public bool ColorizeSymbols { get; set; }

	// Token: 0x170001A9 RID: 425
	// (get) Token: 0x060006C3 RID: 1731 RVA: 0x0001D8A3 File Offset: 0x0001BAA3
	// (set) Token: 0x060006C4 RID: 1732 RVA: 0x0001D8AB File Offset: 0x0001BAAB
	public TextAlignment TextAlign { get; set; }

	// Token: 0x170001AA RID: 426
	// (get) Token: 0x060006C5 RID: 1733 RVA: 0x0001D8B4 File Offset: 0x0001BAB4
	// (set) Token: 0x060006C6 RID: 1734 RVA: 0x0001D8BC File Offset: 0x0001BABC
	public Color32 DefaultColor { get; set; }

	// Token: 0x170001AB RID: 427
	// (get) Token: 0x060006C7 RID: 1735 RVA: 0x0001D8C5 File Offset: 0x0001BAC5
	// (set) Token: 0x060006C8 RID: 1736 RVA: 0x0001D8CD File Offset: 0x0001BACD
	public Color32? BottomColor { get; set; }

	// Token: 0x170001AC RID: 428
	// (get) Token: 0x060006C9 RID: 1737 RVA: 0x0001D8D6 File Offset: 0x0001BAD6
	// (set) Token: 0x060006CA RID: 1738 RVA: 0x0001D8DE File Offset: 0x0001BADE
	public float Opacity { get; set; }

	// Token: 0x170001AD RID: 429
	// (get) Token: 0x060006CB RID: 1739 RVA: 0x0001D8E7 File Offset: 0x0001BAE7
	// (set) Token: 0x060006CC RID: 1740 RVA: 0x0001D8EF File Offset: 0x0001BAEF
	public bool Outline { get; set; }

	// Token: 0x170001AE RID: 430
	// (get) Token: 0x060006CD RID: 1741 RVA: 0x0001D8F8 File Offset: 0x0001BAF8
	// (set) Token: 0x060006CE RID: 1742 RVA: 0x0001D900 File Offset: 0x0001BB00
	public int OutlineSize { get; set; }

	// Token: 0x170001AF RID: 431
	// (get) Token: 0x060006CF RID: 1743 RVA: 0x0001D909 File Offset: 0x0001BB09
	// (set) Token: 0x060006D0 RID: 1744 RVA: 0x0001D911 File Offset: 0x0001BB11
	public Color32 OutlineColor { get; set; }

	// Token: 0x170001B0 RID: 432
	// (get) Token: 0x060006D1 RID: 1745 RVA: 0x0001D91A File Offset: 0x0001BB1A
	// (set) Token: 0x060006D2 RID: 1746 RVA: 0x0001D922 File Offset: 0x0001BB22
	public bool Shadow { get; set; }

	// Token: 0x170001B1 RID: 433
	// (get) Token: 0x060006D3 RID: 1747 RVA: 0x0001D92B File Offset: 0x0001BB2B
	// (set) Token: 0x060006D4 RID: 1748 RVA: 0x0001D933 File Offset: 0x0001BB33
	public Color32 ShadowColor { get; set; }

	// Token: 0x170001B2 RID: 434
	// (get) Token: 0x060006D5 RID: 1749 RVA: 0x0001D93C File Offset: 0x0001BB3C
	// (set) Token: 0x060006D6 RID: 1750 RVA: 0x0001D944 File Offset: 0x0001BB44
	public Vector2 ShadowOffset { get; set; }

	// Token: 0x170001B3 RID: 435
	// (get) Token: 0x060006D7 RID: 1751 RVA: 0x0001D94D File Offset: 0x0001BB4D
	// (set) Token: 0x060006D8 RID: 1752 RVA: 0x0001D955 File Offset: 0x0001BB55
	public int TabSize { get; set; }

	// Token: 0x170001B4 RID: 436
	// (get) Token: 0x060006D9 RID: 1753 RVA: 0x0001D95E File Offset: 0x0001BB5E
	// (set) Token: 0x060006DA RID: 1754 RVA: 0x0001D966 File Offset: 0x0001BB66
	public List<int> TabStops { get; set; }

	// Token: 0x170001B5 RID: 437
	// (get) Token: 0x060006DB RID: 1755 RVA: 0x0001D96F File Offset: 0x0001BB6F
	// (set) Token: 0x060006DC RID: 1756 RVA: 0x0001D977 File Offset: 0x0001BB77
	public Vector2 RenderedSize { get; internal set; }

	// Token: 0x170001B6 RID: 438
	// (get) Token: 0x060006DD RID: 1757 RVA: 0x0001D980 File Offset: 0x0001BB80
	// (set) Token: 0x060006DE RID: 1758 RVA: 0x0001D988 File Offset: 0x0001BB88
	public int LinesRendered { get; internal set; }

	// Token: 0x060006DF RID: 1759
	public abstract void Release();

	// Token: 0x060006E0 RID: 1760
	public abstract float[] GetCharacterWidths(string text);

	// Token: 0x060006E1 RID: 1761
	public abstract Vector2 MeasureString(string text);

	// Token: 0x060006E2 RID: 1762
	public abstract void Render(string text, dfRenderData destination);

	// Token: 0x060006E3 RID: 1763 RVA: 0x0001D994 File Offset: 0x0001BB94
	protected virtual void Reset()
	{
		this.Font = null;
		this.PixelRatio = 0f;
		this.TextScale = 1f;
		this.CharacterSpacing = 0;
		this.VectorOffset = Vector3.zero;
		this.ProcessMarkup = false;
		this.WordWrap = false;
		this.MultiLine = false;
		this.OverrideMarkupColors = false;
		this.ColorizeSymbols = false;
		this.TextAlign = TextAlignment.Left;
		this.DefaultColor = Color.white;
		this.BottomColor = null;
		this.Opacity = 1f;
		this.Outline = false;
		this.Shadow = false;
	}

	// Token: 0x060006E4 RID: 1764 RVA: 0x0001DA32 File Offset: 0x0001BC32
	public void Dispose()
	{
		this.Release();
	}
}

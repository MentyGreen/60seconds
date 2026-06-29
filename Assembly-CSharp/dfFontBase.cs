using System;
using UnityEngine;

// Token: 0x02000061 RID: 97
[Serializable]
public abstract class dfFontBase : MonoBehaviour
{
	// Token: 0x17000199 RID: 409
	// (get) Token: 0x060006A3 RID: 1699
	// (set) Token: 0x060006A4 RID: 1700
	public abstract Material Material { get; set; }

	// Token: 0x1700019A RID: 410
	// (get) Token: 0x060006A5 RID: 1701
	public abstract Texture Texture { get; }

	// Token: 0x1700019B RID: 411
	// (get) Token: 0x060006A6 RID: 1702
	public abstract bool IsValid { get; }

	// Token: 0x1700019C RID: 412
	// (get) Token: 0x060006A7 RID: 1703
	// (set) Token: 0x060006A8 RID: 1704
	public abstract int FontSize { get; set; }

	// Token: 0x1700019D RID: 413
	// (get) Token: 0x060006A9 RID: 1705
	// (set) Token: 0x060006AA RID: 1706
	public abstract int LineHeight { get; set; }

	// Token: 0x060006AB RID: 1707
	public abstract dfFontRendererBase ObtainRenderer();
}

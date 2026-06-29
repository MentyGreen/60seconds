using System;
using UnityEngine;

// Token: 0x0200002C RID: 44
public abstract class dfTouchInputSourceComponent : MonoBehaviour
{
	// Token: 0x1700013B RID: 315
	// (get) Token: 0x0600057F RID: 1407
	public abstract IDFTouchInputSource Source { get; }

	// Token: 0x040001E2 RID: 482
	public int Priority;
}

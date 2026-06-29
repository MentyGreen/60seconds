using System;
using UnityEngine;

// Token: 0x02000024 RID: 36
public abstract class dfGestureBase : MonoBehaviour
{
	// Token: 0x1700012C RID: 300
	// (get) Token: 0x06000522 RID: 1314 RVA: 0x000198DD File Offset: 0x00017ADD
	// (set) Token: 0x06000523 RID: 1315 RVA: 0x000198E5 File Offset: 0x00017AE5
	public dfGestureState State { get; protected set; }

	// Token: 0x1700012D RID: 301
	// (get) Token: 0x06000524 RID: 1316 RVA: 0x000198EE File Offset: 0x00017AEE
	// (set) Token: 0x06000525 RID: 1317 RVA: 0x000198F6 File Offset: 0x00017AF6
	public Vector2 StartPosition { get; protected set; }

	// Token: 0x1700012E RID: 302
	// (get) Token: 0x06000526 RID: 1318 RVA: 0x000198FF File Offset: 0x00017AFF
	// (set) Token: 0x06000527 RID: 1319 RVA: 0x00019907 File Offset: 0x00017B07
	public Vector2 CurrentPosition { get; protected set; }

	// Token: 0x1700012F RID: 303
	// (get) Token: 0x06000528 RID: 1320 RVA: 0x00019910 File Offset: 0x00017B10
	// (set) Token: 0x06000529 RID: 1321 RVA: 0x00019918 File Offset: 0x00017B18
	public float StartTime { get; protected set; }

	// Token: 0x17000130 RID: 304
	// (get) Token: 0x0600052A RID: 1322 RVA: 0x00019921 File Offset: 0x00017B21
	public dfControl Control
	{
		get
		{
			if (this.control == null)
			{
				this.control = base.GetComponent<dfControl>();
			}
			return this.control;
		}
	}

	// Token: 0x040001BB RID: 443
	private dfControl control;
}

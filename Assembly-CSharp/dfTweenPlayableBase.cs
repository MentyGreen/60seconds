using System;
using UnityEngine;

// Token: 0x020000D1 RID: 209
public abstract class dfTweenPlayableBase : MonoBehaviour
{
	// Token: 0x1700024B RID: 587
	// (get) Token: 0x06000B2E RID: 2862
	// (set) Token: 0x06000B2F RID: 2863
	public abstract string TweenName { get; set; }

	// Token: 0x1700024C RID: 588
	// (get) Token: 0x06000B30 RID: 2864
	public abstract bool IsPlaying { get; }

	// Token: 0x06000B31 RID: 2865
	public abstract void Play();

	// Token: 0x06000B32 RID: 2866
	public abstract void Stop();

	// Token: 0x06000B33 RID: 2867
	public abstract void Reset();

	// Token: 0x06000B34 RID: 2868 RVA: 0x0002F9D5 File Offset: 0x0002DBD5
	public void Enable()
	{
		base.enabled = true;
	}

	// Token: 0x06000B35 RID: 2869 RVA: 0x0002F9DE File Offset: 0x0002DBDE
	public void Disable()
	{
		base.enabled = false;
	}

	// Token: 0x06000B36 RID: 2870 RVA: 0x0002F9E7 File Offset: 0x0002DBE7
	public override string ToString()
	{
		return this.TweenName + " - " + base.ToString();
	}
}

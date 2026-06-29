using System;
using UnityEngine;

// Token: 0x0200010C RID: 268
[Serializable]
public struct SDifficultyCollection
{
	// Token: 0x170002AB RID: 683
	// (get) Token: 0x06000CFF RID: 3327 RVA: 0x000370DC File Offset: 0x000352DC
	public SDifficultyData Easy
	{
		get
		{
			return this._easy;
		}
	}

	// Token: 0x170002AC RID: 684
	// (get) Token: 0x06000D00 RID: 3328 RVA: 0x000370E4 File Offset: 0x000352E4
	public SDifficultyData Normal
	{
		get
		{
			return this._normal;
		}
	}

	// Token: 0x170002AD RID: 685
	// (get) Token: 0x06000D01 RID: 3329 RVA: 0x000370EC File Offset: 0x000352EC
	public SDifficultyData Hard
	{
		get
		{
			return this._hard;
		}
	}

	// Token: 0x0400070F RID: 1807
	[SerializeField]
	private SDifficultyData _easy;

	// Token: 0x04000710 RID: 1808
	[SerializeField]
	private SDifficultyData _normal;

	// Token: 0x04000711 RID: 1809
	[SerializeField]
	private SDifficultyData _hard;
}

using System;
using UnityEngine;

namespace DunGen
{
	// Token: 0x020001DC RID: 476
	[Serializable]
	public sealed class GameObjectChance
	{
		// Token: 0x06001392 RID: 5010 RVA: 0x00058809 File Offset: 0x00056A09
		public GameObjectChance() : this(null, 1f, 1f)
		{
		}

		// Token: 0x06001393 RID: 5011 RVA: 0x0005881C File Offset: 0x00056A1C
		public GameObjectChance(GameObject value) : this(value, 1f, 1f)
		{
		}

		// Token: 0x06001394 RID: 5012 RVA: 0x0005882F File Offset: 0x00056A2F
		public GameObjectChance(GameObject value, float mainPathWeight, float branchPathWeight)
		{
			this.Value = value;
			this.MainPathWeight = mainPathWeight;
			this.BranchPathWeight = branchPathWeight;
		}

		// Token: 0x06001395 RID: 5013 RVA: 0x0005886B File Offset: 0x00056A6B
		public float GetWeight(bool isOnMainPath, float normalizedDepth)
		{
			if (!isOnMainPath)
			{
				return this.BranchPathWeight;
			}
			return this.MainPathWeight;
		}

		// Token: 0x04000CE7 RID: 3303
		public GameObject Value;

		// Token: 0x04000CE8 RID: 3304
		public float MainPathWeight;

		// Token: 0x04000CE9 RID: 3305
		public float BranchPathWeight;

		// Token: 0x04000CEA RID: 3306
		public bool UseDepthScale;

		// Token: 0x04000CEB RID: 3307
		public AnimationCurve DepthWeightScale = AnimationCurve.Linear(0f, 1f, 1f, 1f);
	}
}

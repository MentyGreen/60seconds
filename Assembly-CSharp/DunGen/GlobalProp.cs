using System;
using UnityEngine;

namespace DunGen
{
	// Token: 0x020001E1 RID: 481
	[AddComponentMenu("DunGen/Random Props/Global Prop")]
	public class GlobalProp : MonoBehaviour
	{
		// Token: 0x04000D01 RID: 3329
		public int PropGroupID;

		// Token: 0x04000D02 RID: 3330
		public float MainPathWeight = 1f;

		// Token: 0x04000D03 RID: 3331
		public float BranchPathWeight = 1f;

		// Token: 0x04000D04 RID: 3332
		public AnimationCurve DepthWeightScale = AnimationCurve.Linear(0f, 1f, 1f, 1f);
	}
}

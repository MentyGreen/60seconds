using System;
using System.Collections.Generic;
using RG.Core.Base;
using RG.SecondsRemaster.Menu;
using UnityEngine;

namespace RG.SecondsRemaster.ChallengeConclusion
{
	// Token: 0x020002C2 RID: 706
	[CreateAssetMenu(menuName = "60 Seconds Remaster!/New Challenge Rewards", fileName = "New Challenge Rewards")]
	[Serializable]
	public class ChallengeRewards : RGScriptableObject
	{
		// Token: 0x17000489 RID: 1161
		// (get) Token: 0x060018F3 RID: 6387 RVA: 0x0006D098 File Offset: 0x0006B298
		public List<RewardItem> Rewards
		{
			get
			{
				return this._rewards;
			}
		}

		// Token: 0x040012B8 RID: 4792
		[SerializeField]
		private List<RewardItem> _rewards;
	}
}

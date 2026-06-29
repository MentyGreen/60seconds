using System;
using RG.Core.Base;
using RG.Parsecs.EventEditor;
using UnityEngine;

namespace RG.SecondsRemaster.Menu
{
	// Token: 0x02000296 RID: 662
	[CreateAssetMenu(menuName = "60 Seconds Remaster!/Challenges/New Reward", fileName = "New Reward")]
	public class RewardItem : RGScriptableObject
	{
		// Token: 0x1700047F RID: 1151
		// (get) Token: 0x0600182C RID: 6188 RVA: 0x00069DC3 File Offset: 0x00067FC3
		public GlobalBoolVariable ScavengeRewardIsUnlockedVariable
		{
			get
			{
				return this._scavengeRewardIsUnlockedVariable;
			}
		}

		// Token: 0x17000480 RID: 1152
		// (get) Token: 0x0600182D RID: 6189 RVA: 0x00069DCB File Offset: 0x00067FCB
		// (set) Token: 0x0600182E RID: 6190 RVA: 0x00069DD3 File Offset: 0x00067FD3
		public Sprite Icon
		{
			get
			{
				return this._icon;
			}
			set
			{
				this._icon = value;
			}
		}

		// Token: 0x17000481 RID: 1153
		// (get) Token: 0x0600182F RID: 6191 RVA: 0x00069DDC File Offset: 0x00067FDC
		public Sprite ConclusionIcon
		{
			get
			{
				return this._conclusionIcon;
			}
		}

		// Token: 0x040011CF RID: 4559
		[SerializeField]
		private Sprite _icon;

		// Token: 0x040011D0 RID: 4560
		[SerializeField]
		private Sprite _conclusionIcon;

		// Token: 0x040011D1 RID: 4561
		[SerializeField]
		private GlobalBoolVariable _scavengeRewardIsUnlockedVariable;
	}
}

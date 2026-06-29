using System;
using I2.Loc;
using RG.Core.Base;
using UnityEngine;

namespace RG.SecondsRemaster
{
	// Token: 0x02000245 RID: 581
	[CreateAssetMenu(menuName = "60 Seconds Remaster!/New Difficulty Level", fileName = "New Difficulty Level")]
	public class DifficultyLevel : RGScriptableObject
	{
		// Token: 0x17000439 RID: 1081
		// (get) Token: 0x06001609 RID: 5641 RVA: 0x00060D0C File Offset: 0x0005EF0C
		public EGameDifficulty ScavengeDifficulty
		{
			get
			{
				return this._scavengeDifficulty;
			}
		}

		// Token: 0x1700043A RID: 1082
		// (get) Token: 0x0600160A RID: 5642 RVA: 0x00060D14 File Offset: 0x0005EF14
		public LocalizedString Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x1700043B RID: 1083
		// (get) Token: 0x0600160B RID: 5643 RVA: 0x00060D1C File Offset: 0x0005EF1C
		public int SurvivalDifficulty
		{
			get
			{
				return (int)this._survivalDifficulty;
			}
		}

		// Token: 0x1700043C RID: 1084
		// (get) Token: 0x0600160C RID: 5644 RVA: 0x00060D24 File Offset: 0x0005EF24
		public LocalizedString Description
		{
			get
			{
				return this._description;
			}
		}

		// Token: 0x1700043D RID: 1085
		// (get) Token: 0x0600160D RID: 5645 RVA: 0x00060D2C File Offset: 0x0005EF2C
		public GameSetup Setup
		{
			get
			{
				return this._setup;
			}
		}

		// Token: 0x04000EC5 RID: 3781
		[Tooltip("This field allows to choose difficulty level in Scavenge from original 60 Seconds data.")]
		[SerializeField]
		private EGameDifficulty _scavengeDifficulty;

		// Token: 0x04000EC6 RID: 3782
		[Tooltip("This field allows to set difficulty level in Survival which base on 60 Parsecs style. Design control the system in events based on the value in the global int variable.")]
		[SerializeField]
		private uint _survivalDifficulty;

		// Token: 0x04000EC7 RID: 3783
		[Tooltip("Reference to original settings for game - from 60 Seconds.")]
		[SerializeField]
		private GameSetup _setup;

		// Token: 0x04000EC8 RID: 3784
		[SerializeField]
		private LocalizedString _name;

		// Token: 0x04000EC9 RID: 3785
		[SerializeField]
		private LocalizedString _description;
	}
}

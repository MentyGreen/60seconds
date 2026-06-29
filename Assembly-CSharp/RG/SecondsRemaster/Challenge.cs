using System;
using System.Collections.Generic;
using I2.Loc;
using RG.Core.Base;
using RG.Core.SaveSystem;
using RG.Parsecs.EventEditor;
using RG.Parsecs.Survival;
using RG.SecondsRemaster.Menu;
using RG.SecondsRemaster.Scavenge;
using UnityEngine;

namespace RG.SecondsRemaster
{
	// Token: 0x0200023F RID: 575
	[CreateAssetMenu(menuName = "60 Seconds Remaster!/Challenges/New Challenge", fileName = "New Challenge")]
	public class Challenge : RGScriptableObject, ISaveable
	{
		// Token: 0x17000424 RID: 1060
		// (get) Token: 0x060015DC RID: 5596 RVA: 0x00060A71 File Offset: 0x0005EC71
		public LocalizedString Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x17000425 RID: 1061
		// (get) Token: 0x060015DD RID: 5597 RVA: 0x00060A79 File Offset: 0x0005EC79
		public LocalizedString RewardName
		{
			get
			{
				return this._rewardName;
			}
		}

		// Token: 0x17000426 RID: 1062
		// (get) Token: 0x060015DE RID: 5598 RVA: 0x00060A81 File Offset: 0x0005EC81
		public Mission Mission
		{
			get
			{
				return this._mission;
			}
		}

		// Token: 0x17000427 RID: 1063
		// (get) Token: 0x060015DF RID: 5599 RVA: 0x00060A89 File Offset: 0x0005EC89
		public string ScavengeLevel
		{
			get
			{
				return this._scavengeLevel;
			}
		}

		// Token: 0x17000428 RID: 1064
		// (get) Token: 0x060015E0 RID: 5600 RVA: 0x00060A91 File Offset: 0x0005EC91
		public LocalizedString Description
		{
			get
			{
				return this._description;
			}
		}

		// Token: 0x17000429 RID: 1065
		// (get) Token: 0x060015E1 RID: 5601 RVA: 0x00060A99 File Offset: 0x0005EC99
		public List<ScavengeItem> Collectables
		{
			get
			{
				return this._collectables;
			}
		}

		// Token: 0x1700042A RID: 1066
		// (get) Token: 0x060015E2 RID: 5602 RVA: 0x00060AA1 File Offset: 0x0005ECA1
		public List<RewardItem> Rewards
		{
			get
			{
				return this._rewards;
			}
		}

		// Token: 0x1700042B RID: 1067
		// (get) Token: 0x060015E3 RID: 5603 RVA: 0x00060AA9 File Offset: 0x0005ECA9
		public bool IsUnlocked
		{
			get
			{
				return this._challengeCompleted.Value;
			}
		}

		// Token: 0x1700042C RID: 1068
		// (get) Token: 0x060015E4 RID: 5604 RVA: 0x00060AB6 File Offset: 0x0005ECB6
		public float Time
		{
			get
			{
				return this._runtimeData.Time;
			}
		}

		// Token: 0x1700042D RID: 1069
		// (get) Token: 0x060015E5 RID: 5605 RVA: 0x00060AC3 File Offset: 0x0005ECC3
		public string UnlockDate
		{
			get
			{
				return this._runtimeData.UnlockDate;
			}
		}

		// Token: 0x1700042E RID: 1070
		// (get) Token: 0x060015E6 RID: 5606 RVA: 0x00060AD0 File Offset: 0x0005ECD0
		public GameSetup GameSetup
		{
			get
			{
				return this._gameSetup;
			}
		}

		// Token: 0x060015E7 RID: 5607 RVA: 0x00060AD8 File Offset: 0x0005ECD8
		public void Unlock(float time)
		{
			if (!this._challengeCompleted.Value || this._runtimeData.Time > time)
			{
				this._runtimeData.Time = Mathf.Clamp(time, 0f, 60f);
				this._runtimeData.UnlockDate = DateTime.Now.ToString("dd/MM/yyyy");
				this._challengeCompleted.Value = true;
			}
		}

		// Token: 0x060015E8 RID: 5608 RVA: 0x00060B44 File Offset: 0x0005ED44
		private void OnEnable()
		{
			this.Register();
		}

		// Token: 0x060015E9 RID: 5609 RVA: 0x00060B4C File Offset: 0x0005ED4C
		private void OnDestroy()
		{
			this.Unregister();
		}

		// Token: 0x060015EA RID: 5610 RVA: 0x00060B54 File Offset: 0x0005ED54
		public string Serialize()
		{
			return JsonUtility.ToJson(this._runtimeData);
		}

		// Token: 0x060015EB RID: 5611 RVA: 0x00060B61 File Offset: 0x0005ED61
		public void Deserialize(string jsonData)
		{
			this._runtimeData = JsonUtility.FromJson<ChallengeRuntimeData>(jsonData);
		}

		// Token: 0x060015EC RID: 5612 RVA: 0x00060B6F File Offset: 0x0005ED6F
		public void Register()
		{
			this._saveEvent.RegisterListener(this);
		}

		// Token: 0x060015ED RID: 5613 RVA: 0x00060B7D File Offset: 0x0005ED7D
		public void Unregister()
		{
			this._saveEvent.UnregisterListener(this);
		}

		// Token: 0x1700042F RID: 1071
		// (get) Token: 0x060015EE RID: 5614 RVA: 0x00060B8B File Offset: 0x0005ED8B
		public string ID
		{
			get
			{
				return this.Guid;
			}
		}

		// Token: 0x17000430 RID: 1072
		// (get) Token: 0x060015EF RID: 5615 RVA: 0x00060B93 File Offset: 0x0005ED93
		public Sprite ChallengeGraphic
		{
			get
			{
				return this._challengeGraphic;
			}
		}

		// Token: 0x17000431 RID: 1073
		// (get) Token: 0x060015F0 RID: 5616 RVA: 0x00060B9B File Offset: 0x0005ED9B
		public Challenge.EChallengeType ChallengeType
		{
			get
			{
				return this._challengeType;
			}
		}

		// Token: 0x17000432 RID: 1074
		// (get) Token: 0x060015F1 RID: 5617 RVA: 0x00060BA3 File Offset: 0x0005EDA3
		public List<LocalizedString> Objectives
		{
			get
			{
				return this._objectives;
			}
		}

		// Token: 0x060015F2 RID: 5618 RVA: 0x00060BAB File Offset: 0x0005EDAB
		public void ResetData()
		{
			this._runtimeData = new ChallengeRuntimeData();
		}

		// Token: 0x04000EB0 RID: 3760
		[SerializeField]
		[Header("Save System")]
		private SaveEvent _saveEvent;

		// Token: 0x04000EB1 RID: 3761
		[SerializeField]
		[Header("Main Data")]
		private Challenge.EChallengeType _challengeType;

		// Token: 0x04000EB2 RID: 3762
		[SerializeField]
		private LocalizedString _name;

		// Token: 0x04000EB3 RID: 3763
		[SerializeField]
		private LocalizedString _description;

		// Token: 0x04000EB4 RID: 3764
		[SerializeField]
		private Sprite _challengeGraphic;

		// Token: 0x04000EB5 RID: 3765
		[SerializeField]
		private List<RewardItem> _rewards;

		// Token: 0x04000EB6 RID: 3766
		[SerializeField]
		private LocalizedString _rewardName;

		// Token: 0x04000EB7 RID: 3767
		[SerializeField]
		[Header("Survival Data")]
		private Mission _mission;

		// Token: 0x04000EB8 RID: 3768
		[SerializeField]
		private List<LocalizedString> _objectives;

		// Token: 0x04000EB9 RID: 3769
		[SerializeField]
		[Header("Scavenge Data")]
		private GameSetup _gameSetup;

		// Token: 0x04000EBA RID: 3770
		[SerializeField]
		private string _scavengeLevel;

		// Token: 0x04000EBB RID: 3771
		[SerializeField]
		private List<ScavengeItem> _collectables;

		// Token: 0x04000EBC RID: 3772
		[SerializeField]
		private GlobalBoolVariable _challengeCompleted;

		// Token: 0x04000EBD RID: 3773
		private ChallengeRuntimeData _runtimeData;

		// Token: 0x0200041D RID: 1053
		public enum EChallengeType
		{
			// Token: 0x040018B8 RID: 6328
			NONE,
			// Token: 0x040018B9 RID: 6329
			SURVIVAL,
			// Token: 0x040018BA RID: 6330
			SCAVENGE
		}
	}
}

using System;
using RG.Parsecs.Survival;
using UnityEngine;

namespace RG.SecondsRemaster.Survival
{
	// Token: 0x02000320 RID: 800
	[Serializable]
	public class ExpeditionDestinationListEntry
	{
		// Token: 0x170004DC RID: 1244
		// (get) Token: 0x06001AFD RID: 6909 RVA: 0x00074752 File Offset: 0x00072952
		public ExpeditionDestination Destination
		{
			get
			{
				return this._expeditionDestination;
			}
		}

		// Token: 0x170004DD RID: 1245
		// (get) Token: 0x06001AFE RID: 6910 RVA: 0x0007475A File Offset: 0x0007295A
		public Character[] RequiredCharacters
		{
			get
			{
				return this._requiredCharacters;
			}
		}

		// Token: 0x170004DE RID: 1246
		// (get) Token: 0x06001AFF RID: 6911 RVA: 0x00074762 File Offset: 0x00072962
		public CharacterStatus[] RequiredStatuses
		{
			get
			{
				return this._requiredStatuses;
			}
		}

		// Token: 0x170004DF RID: 1247
		// (get) Token: 0x06001B00 RID: 6912 RVA: 0x0007476A File Offset: 0x0007296A
		public bool AvailableInTutorial
		{
			get
			{
				return this._availableInTutorial;
			}
		}

		// Token: 0x06001B01 RID: 6913 RVA: 0x00074774 File Offset: 0x00072974
		public bool CanDestinationBeConsideredInExpedition(Character expeditionCharacter)
		{
			bool flag = this._requiredCharacters == null || this._requiredCharacters.Length == 0;
			if (this._requiredCharacters != null)
			{
				for (int i = 0; i < this._requiredCharacters.Length; i++)
				{
					if (expeditionCharacter == this._requiredCharacters[i])
					{
						flag = true;
						break;
					}
				}
			}
			bool flag2 = this._requiredStatuses == null || this._requiredStatuses.Length == 0;
			if (this._requiredStatuses != null)
			{
				for (int j = 0; j < this._requiredStatuses.Length; j++)
				{
					if (expeditionCharacter.RuntimeData.HasStatus(this._requiredStatuses[j].Id))
					{
						flag2 = true;
						break;
					}
				}
			}
			return flag && flag2 && this._expeditionDestination.Enabled;
		}

		// Token: 0x040014B6 RID: 5302
		[SerializeField]
		private ExpeditionDestination _expeditionDestination;

		// Token: 0x040014B7 RID: 5303
		[SerializeField]
		private Character[] _requiredCharacters;

		// Token: 0x040014B8 RID: 5304
		[SerializeField]
		private CharacterStatus[] _requiredStatuses;

		// Token: 0x040014B9 RID: 5305
		[SerializeField]
		private bool _availableInTutorial;
	}
}

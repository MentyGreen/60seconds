using System;
using System.Collections.Generic;
using RG.Core.Base;
using RG.Parsecs.Survival;
using UnityEngine;

namespace RG.SecondsRemaster.Survival
{
	// Token: 0x0200031F RID: 799
	[CreateAssetMenu(fileName = "New Expedition Destination List", menuName = "60 Seconds Remaster!/New Expedition Destination List")]
	public class ExpeditionDestinationList : RGScriptableObject
	{
		// Token: 0x170004DB RID: 1243
		// (get) Token: 0x06001AFA RID: 6906 RVA: 0x0007460D File Offset: 0x0007280D
		public List<ExpeditionDestinationListEntry> ExpeditionDestinations
		{
			get
			{
				return this._expeditionDestinations;
			}
		}

		// Token: 0x06001AFB RID: 6907 RVA: 0x00074618 File Offset: 0x00072818
		public ExpeditionDestination GetRandomDestination(Character character, bool isTutorial)
		{
			if (character == null)
			{
				return null;
			}
			int num = 0;
			for (int i = 0; i < this._expeditionDestinations.Count; i++)
			{
				if (this._expeditionDestinations[i].Destination.Enabled)
				{
					num++;
				}
			}
			if (num <= 2)
			{
				for (int j = 0; j < this._expeditionDestinations.Count; j++)
				{
					if (!this._expeditionDestinations[j].Destination.DynamicData.Enabled)
					{
						this._expeditionDestinations[j].Destination.DynamicData.Enabled = true;
					}
				}
			}
			if (character.RuntimeData.HasStatus(this._specialCharacterStatus.Id))
			{
				return this._mutantExpeditionDestination;
			}
			List<ExpeditionDestination> list = new List<ExpeditionDestination>();
			for (int k = 0; k < this._expeditionDestinations.Count; k++)
			{
				if (this._expeditionDestinations[k].CanDestinationBeConsideredInExpedition(character) && this._expeditionDestinations[k].AvailableInTutorial == isTutorial)
				{
					list.Add(this._expeditionDestinations[k].Destination);
				}
			}
			return list[Random.Range(0, list.Count)];
		}

		// Token: 0x040014B2 RID: 5298
		[SerializeField]
		private List<ExpeditionDestinationListEntry> _expeditionDestinations;

		// Token: 0x040014B3 RID: 5299
		[Tooltip("This field is used as default destination for mutant character")]
		[SerializeField]
		private ExpeditionDestination _mutantExpeditionDestination;

		// Token: 0x040014B4 RID: 5300
		[SerializeField]
		private CharacterStatus _specialCharacterStatus;

		// Token: 0x040014B5 RID: 5301
		private const int ACTIVE_DESTINATIONS_THRESHOLD_TO_ACTIVATE_INACTIVE_DESTINATIONS = 2;
	}
}

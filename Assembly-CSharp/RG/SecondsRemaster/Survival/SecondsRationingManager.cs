using System;
using RG.Parsecs.EventEditor;
using RG.Parsecs.Survival;
using UnityEngine;

namespace RG.SecondsRemaster.Survival
{
	// Token: 0x02000337 RID: 823
	public class SecondsRationingManager : RationingManager
	{
		// Token: 0x170004E2 RID: 1250
		// (get) Token: 0x06001B7F RID: 7039 RVA: 0x00076793 File Offset: 0x00074993
		public TimeRationing TimeRationing
		{
			get
			{
				return this._timeRationing;
			}
		}

		// Token: 0x170004E3 RID: 1251
		// (get) Token: 0x06001B80 RID: 7040 RVA: 0x0007679B File Offset: 0x0007499B
		public new static SecondsRationingManager Instance
		{
			get
			{
				return SecondsRationingManager._instance;
			}
		}

		// Token: 0x06001B81 RID: 7041 RVA: 0x000767A2 File Offset: 0x000749A2
		protected override void CustomStart()
		{
			base.CustomStart();
			SecondsRationingManager._instance = this;
		}

		// Token: 0x06001B82 RID: 7042 RVA: 0x000767B0 File Offset: 0x000749B0
		protected override void ConsumeRation(RationingData rationingData)
		{
			this._timeRationing.IncrementTime();
			for (int i = 0; i < rationingData.Rations.Count; i++)
			{
				if (rationingData.Rations[i].RationedItem is ConsumableRemedium)
				{
					ConsumableRemedium consumableRemedium = (ConsumableRemedium)rationingData.Rations[i].RationedItem;
					if (consumableRemedium.StaticData.ItemId.Equals("item_water") && !this._isTutorial.Value && this._currentChallengeData.RuntimeData.Challenge == null)
					{
						this._waterConsumedVariable.SetValue(this._waterConsumedVariable.Value + 0.25f);
					}
					if (consumableRemedium.StaticData.ItemId.Equals("item_food") && !this._isTutorial.Value && this._currentChallengeData.RuntimeData.Challenge == null)
					{
						this._soupConsumedVariable.SetValue(this._soupConsumedVariable.Value + 0.25f);
					}
					this._timeRationing.ResetLastRationingTime(consumableRemedium, rationingData.Rations[i].CharacterIndex);
				}
			}
		}

		// Token: 0x0400153E RID: 5438
		[SerializeField]
		private TimeRationing _timeRationing;

		// Token: 0x0400153F RID: 5439
		[SerializeField]
		private GlobalFloatVariable _waterConsumedVariable;

		// Token: 0x04001540 RID: 5440
		[SerializeField]
		private GlobalFloatVariable _soupConsumedVariable;

		// Token: 0x04001541 RID: 5441
		[SerializeField]
		private CurrentChallengeData _currentChallengeData;

		// Token: 0x04001542 RID: 5442
		[SerializeField]
		private GlobalBoolVariable _isTutorial;

		// Token: 0x04001543 RID: 5443
		private const float SERVING_SIZE = 0.25f;

		// Token: 0x04001544 RID: 5444
		private static SecondsRationingManager _instance;
	}
}

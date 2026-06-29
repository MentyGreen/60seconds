using System;
using System.Collections.Generic;
using RG.Parsecs.Common;
using RG.Parsecs.Survival;
using UnityEngine;

namespace RG.SecondsRemaster.Survival
{
	// Token: 0x02000335 RID: 821
	public class RemasterExpeditionManager : ExpeditionManager
	{
		// Token: 0x06001B79 RID: 7033 RVA: 0x000764EC File Offset: 0x000746EC
		protected override void Awake()
		{
			if (ExpeditionManager.Instance == null)
			{
				ExpeditionManager.Instance = this;
			}
			this._endOfDayListenerList.RegisterOnEndOfDay(new EndOfDayListenerList.OnEndOfDay(base.SendExpeditionIfPlannedAndResetView), "PreEventSystems", 4, this, false);
			this._endOfDayListenerList.RegisterOnEndOfDay(new EndOfDayListenerList.OnEndOfDay(base.RevokeExpeditionAndUpdateExpeditionTime), "Update", 10, this, false);
			this._endOfDayListenerList.RegisterOnEndOfDay(new EndOfDayListenerList.OnEndOfDay(base.LandOnPlanet), "SystemEvents", 25, this, false);
			this._endOfDayListenerList.RegisterOnEndOfDay(new EndOfDayListenerList.OnEndOfDay(this.AddFailedExpeditionStatEntry), "Reset", 998, this, false);
			this._statsManager = StatsManager.Instance;
		}

		// Token: 0x06001B7A RID: 7034 RVA: 0x0007659C File Offset: 0x0007479C
		protected override void OnDestroy()
		{
			this._endOfDayListenerList.UnregisterOnEndOfDay(new EndOfDayListenerList.OnEndOfDay(base.SendExpeditionIfPlannedAndResetView), "PreEventSystems");
			this._endOfDayListenerList.UnregisterOnEndOfDay(new EndOfDayListenerList.OnEndOfDay(base.RevokeExpeditionAndUpdateExpeditionTime), "Update");
			this._endOfDayListenerList.UnregisterOnEndOfDay(new EndOfDayListenerList.OnEndOfDay(base.LandOnPlanet), "SystemEvents");
			this._endOfDayListenerList.UnregisterOnEndOfDay(new EndOfDayListenerList.OnEndOfDay(this.AddFailedExpeditionStatEntry), "Reset");
		}

		// Token: 0x06001B7B RID: 7035 RVA: 0x0007661C File Offset: 0x0007481C
		private void AddFailedExpeditionStatEntry()
		{
			if (this._endGameData != null && this._endGameData.RuntimeData.ShouldEndGame && this._expeditionData.RuntimeData.IsOngoing && this._expeditionData.RuntimeData.OngoingExpeditionData.ExpeditionCharacter.RuntimeData.HasStatusPreventingReturnFromExpedition())
			{
				ExpeditionStatsEntry expeditionStatsEntry = new ExpeditionStatsEntry();
				expeditionStatsEntry.DestinationId = this._expeditionData.RuntimeData.OngoingExpeditionData.ChosenDestination.StaticData.Id;
				expeditionStatsEntry.CharacterId = this._expeditionData.RuntimeData.OngoingExpeditionData.ExpeditionCharacter.ID;
				expeditionStatsEntry.IdsOfItemsTaken = new List<string>();
				for (int i = 0; i < this._expeditionData.RuntimeData.OngoingExpeditionData.ExpeditionItems.Count; i++)
				{
					expeditionStatsEntry.IdsOfItemsTaken.Add(this._expeditionData.RuntimeData.OngoingExpeditionData.ExpeditionItems[i].BaseStaticData.ItemId);
				}
				expeditionStatsEntry.IsSuccessful = false;
				this._statsManager.AddExpeditionStatsEntry(expeditionStatsEntry);
			}
		}

		// Token: 0x0400153B RID: 5435
		[SerializeField]
		private EndGameData _endGameData;
	}
}

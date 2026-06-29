using System;
using RG.Parsecs.Common;
using RG.Parsecs.Survival;
using RG.SecondsRemaster;
using UnityEngine;

namespace RG.Remaster.Common
{
	// Token: 0x0200021E RID: 542
	public class RemasterStatsManager : StatsManager
	{
		// Token: 0x0600152A RID: 5418 RVA: 0x0005DF80 File Offset: 0x0005C180
		protected override bool CanAddExpeditionEntryToVariables()
		{
			return this._isTutorial != null && this._challengeData != null && this._expeditionsMadeVariable != null && this._expeditionsSuccessfulVariable != null && !this._isTutorial.Value && this._challengeData.RuntimeData.Challenge == null;
		}

		// Token: 0x0600152B RID: 5419 RVA: 0x0005DFEC File Offset: 0x0005C1EC
		protected override bool CanAddToGlobalStat()
		{
			Mission currentMission = MissionManager.Instance.GetCurrentMission();
			return !this._isTutorial.Value && (currentMission == null || currentMission.ID == "ms_SurvivalMission" || currentMission.ID == "ms_ScavengeMission");
		}

		// Token: 0x04000E08 RID: 3592
		[SerializeField]
		private CurrentChallengeData _challengeData;
	}
}

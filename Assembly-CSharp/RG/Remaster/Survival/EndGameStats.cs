using System;
using RG.Parsecs.EventEditor;
using RG.Parsecs.Survival;
using RG.SecondsRemaster;
using UnityEngine;

namespace RG.Remaster.Survival
{
	// Token: 0x02000230 RID: 560
	public class EndGameStats : MonoBehaviour
	{
		// Token: 0x06001589 RID: 5513 RVA: 0x0005F38C File Offset: 0x0005D58C
		private void Start()
		{
			if (this._endGameData.RuntimeData.ShouldEndGame && !this._isTutorial.Value && this._currentChallengeData.RuntimeData.Challenge == null && !this._isScavengeOnly.Value)
			{
				if (this._longestSurvivalVariable.Value < this._survivalData.CurrentDay)
				{
					this._longestSurvivalVariable.SetValue(this._survivalData.CurrentDay);
				}
				this._totalDaysSurvivedVariable.SetValue(this._totalDaysSurvivedVariable.Value + this._survivalData.CurrentDay);
				if (this._endGameData.RuntimeData.IsGameWon)
				{
					this._survivalGamesSurvivedVariable.SetValue(this._survivalGamesSurvivedVariable.Value + 1);
					this._totalWinDayCount.SetValue(this._totalWinDayCount.Value + this._survivalData.CurrentDay);
					this._averageSurvivalTimeVariable.SetValue(this._totalWinDayCount.Value / this._survivalGamesSurvivedVariable.Value);
				}
				else
				{
					this._survivalGamesLostVariable.SetValue(this._survivalGamesLostVariable.Value + 1);
				}
				if (this._survivalGamesSurvivedVariable.Value + this._survivalGamesLostVariable.Value > 0)
				{
					this._winRatioVariable.SetValue((float)(this._survivalGamesSurvivedVariable.Value * 100 / (this._survivalGamesSurvivedVariable.Value + this._survivalGamesLostVariable.Value)));
					return;
				}
				this._winRatioVariable.SetValue(0f);
			}
		}

		// Token: 0x04000E74 RID: 3700
		[SerializeField]
		private EndGameData _endGameData;

		// Token: 0x04000E75 RID: 3701
		[SerializeField]
		private SurvivalData _survivalData;

		// Token: 0x04000E76 RID: 3702
		[SerializeField]
		private GlobalIntVariable _survivalGamesSurvivedVariable;

		// Token: 0x04000E77 RID: 3703
		[SerializeField]
		private GlobalIntVariable _survivalGamesLostVariable;

		// Token: 0x04000E78 RID: 3704
		[SerializeField]
		private GlobalIntVariable _totalDaysSurvivedVariable;

		// Token: 0x04000E79 RID: 3705
		[SerializeField]
		private GlobalIntVariable _longestSurvivalVariable;

		// Token: 0x04000E7A RID: 3706
		[SerializeField]
		private GlobalFloatVariable _winRatioVariable;

		// Token: 0x04000E7B RID: 3707
		[SerializeField]
		private GlobalIntVariable _averageSurvivalTimeVariable;

		// Token: 0x04000E7C RID: 3708
		[SerializeField]
		private GlobalIntVariable _totalWinDayCount;

		// Token: 0x04000E7D RID: 3709
		[SerializeField]
		private CurrentChallengeData _currentChallengeData;

		// Token: 0x04000E7E RID: 3710
		[SerializeField]
		private GlobalBoolVariable _isTutorial;

		// Token: 0x04000E7F RID: 3711
		[SerializeField]
		private GlobalBoolVariable _isScavengeOnly;
	}
}

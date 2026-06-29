using System;
using I2.Loc;
using RG.Parsecs.Common;
using RG.Parsecs.EventEditor;
using RG.Parsecs.Survival;
using RG.Remaster.Common;
using TMPro;
using UnityEngine;

namespace RG.SecondsRemaster.Survival
{
	// Token: 0x0200030E RID: 782
	public class JournalDayController : MonoBehaviour
	{
		// Token: 0x06001A7A RID: 6778 RVA: 0x00072F0D File Offset: 0x0007110D
		private void Awake()
		{
			this.ShowCurrentDay();
		}

		// Token: 0x06001A7B RID: 6779 RVA: 0x00072F18 File Offset: 0x00071118
		private void ShowCurrentDay()
		{
			if (this._dayTerm != null && !string.IsNullOrEmpty(this._dayTerm.mTerm))
			{
				this._dayTextField.text = string.Format(this._dayTerm, this._survivalData.DisplayDay);
				RG.Remaster.Common.RichPresenceManager richPresenceManager = Singleton<PlatformManager>.Instance.RichPresenceManager;
				if ((this._survivalChallenge != null && this._survivalChallenge.Value) || richPresenceManager.CurrentRichPresenceStatus == ERichPresenceStatus.CHALLENGE_SURVIVAL_INTRO)
				{
					richPresenceManager.SetParametrizedRichPresence(ERichPresenceStatus.CHALLENGE_SURVIVAL, ERichPresenceParameter.SURVIVAL_DAY, this._survivalData.DisplayDay.ToString());
					return;
				}
				richPresenceManager.SetParametrizedRichPresence(ERichPresenceStatus.SURVIVAL, ERichPresenceParameter.SURVIVAL_DAY, this._survivalData.DisplayDay.ToString());
			}
		}

		// Token: 0x06001A7C RID: 6780 RVA: 0x00072FDC File Offset: 0x000711DC
		public void SetCurrentDayText()
		{
			if (this._endGameData.RuntimeData.ShouldEndGame && this._dayTerm != null && !string.IsNullOrEmpty(this._endGameTerm))
			{
				this._dayTextField.text = this._endGameTerm;
				return;
			}
			this.ShowCurrentDay();
		}

		// Token: 0x0400144C RID: 5196
		[SerializeField]
		private LocalizedString _dayTerm;

		// Token: 0x0400144D RID: 5197
		[SerializeField]
		private LocalizedString _endGameTerm;

		// Token: 0x0400144E RID: 5198
		[SerializeField]
		private TextMeshProUGUI _dayTextField;

		// Token: 0x0400144F RID: 5199
		[SerializeField]
		private SurvivalData _survivalData;

		// Token: 0x04001450 RID: 5200
		[SerializeField]
		private EndGameData _endGameData;

		// Token: 0x04001451 RID: 5201
		[SerializeField]
		private GlobalBoolVariable _survivalChallenge;
	}
}

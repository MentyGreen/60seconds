using System;
using System.Collections.Generic;
using RG.Parsecs.Common;
using UnityEngine;
using UnityEngine.UI;

namespace RG.SecondsRemaster.ChallengeConclusion
{
	// Token: 0x020002C4 RID: 708
	public class UnlocksController : MonoBehaviour
	{
		// Token: 0x060018FA RID: 6394 RVA: 0x0006D184 File Offset: 0x0006B384
		public void Start()
		{
			if (this.IsSetupValid())
			{
				for (int i = 0; i < this._challengeRewards.RuntimeData.Challenge.Rewards.Count; i++)
				{
					this._challengeRewardRepresentations[i].Image.sprite = this._challengeRewards.RuntimeData.Challenge.Rewards[i].ConclusionIcon;
					this._challengeRewardRepresentations[i].UnlockObject.SetActive(true);
				}
			}
			if (this.IsAchievementSetupValid())
			{
				for (int j = 0; j < this._challengeAchievements.Count; j++)
				{
					if (this._challengeAchievements[j].ChallengeToComplete != null && this._challengeAchievements[j].AchievementToUnlock != null && this._challengeRewards.RuntimeData.Challenge.Equals(this._challengeAchievements[j].ChallengeToComplete))
					{
						AchievementsSystem.UnlockAchievement(this._challengeAchievements[j].AchievementToUnlock);
					}
				}
			}
			if (this._challengerAchievement != null && !this._challengerAchievement.IsAchieved && this._challengeRewards.RuntimeData.Challenge.ChallengeType == Challenge.EChallengeType.SCAVENGE)
			{
				AchievementsSystem.UnlockAchievement(this._challengerAchievement);
			}
			if (this._rogueOneAchievement != null && !this._rogueOneAchievement.IsAchieved && this._challengeRewards.RuntimeData.Challenge.ChallengeType == Challenge.EChallengeType.SURVIVAL)
			{
				AchievementsSystem.UnlockAchievement(this._rogueOneAchievement);
			}
		}

		// Token: 0x060018FB RID: 6395 RVA: 0x0006D31B File Offset: 0x0006B51B
		private bool IsAchievementSetupValid()
		{
			return this._challengeRewardRepresentations != null && this._challengeRewardRepresentations.Count > 0;
		}

		// Token: 0x060018FC RID: 6396 RVA: 0x0006D338 File Offset: 0x0006B538
		private bool IsSetupValid()
		{
			return this._challengeRewards != null && this._challengeRewardRepresentations != null && this._challengeRewards.RuntimeData.Challenge.Rewards != null && this._challengeRewards.RuntimeData.Challenge.Rewards.Count <= this._challengeRewardRepresentations.Count;
		}

		// Token: 0x040012BF RID: 4799
		[SerializeField]
		private CurrentChallengeData _challengeRewards;

		// Token: 0x040012C0 RID: 4800
		[SerializeField]
		private Achievement _challengerAchievement;

		// Token: 0x040012C1 RID: 4801
		[SerializeField]
		private Achievement _rogueOneAchievement;

		// Token: 0x040012C2 RID: 4802
		[SerializeField]
		private List<UnlocksController.ChallengeAchievementPair> _challengeAchievements;

		// Token: 0x040012C3 RID: 4803
		[SerializeField]
		private List<UnlocksController.RewardSceneRepresentation> _challengeRewardRepresentations;

		// Token: 0x0200042A RID: 1066
		[Serializable]
		public struct RewardSceneRepresentation
		{
			// Token: 0x040018F2 RID: 6386
			public GameObject UnlockObject;

			// Token: 0x040018F3 RID: 6387
			public Image Image;
		}

		// Token: 0x0200042B RID: 1067
		[Serializable]
		public struct ChallengeAchievementPair
		{
			// Token: 0x040018F4 RID: 6388
			public Challenge ChallengeToComplete;

			// Token: 0x040018F5 RID: 6389
			public Achievement AchievementToUnlock;
		}
	}
}

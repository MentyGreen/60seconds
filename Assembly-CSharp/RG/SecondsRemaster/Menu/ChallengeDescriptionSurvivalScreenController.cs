using System;
using System.Collections.Generic;
using I2.Loc;
using RG.Parsecs.Survival;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RG.SecondsRemaster.Menu
{
	// Token: 0x02000291 RID: 657
	public class ChallengeDescriptionSurvivalScreenController : MonoBehaviour
	{
		// Token: 0x06001813 RID: 6163 RVA: 0x0006975A File Offset: 0x0006795A
		public void Awake()
		{
			this._alreadyTriggered = false;
		}

		// Token: 0x06001814 RID: 6164 RVA: 0x00069764 File Offset: 0x00067964
		private void OnEnable()
		{
			if (this._remasterMenuManager == null)
			{
				this._remasterMenuManager = Object.FindObjectOfType<RemasterMenuManager>();
			}
			this._currentChallengeIndex = this._challengeList.Challenges.IndexOf(this._remasterMenuManager.CurrentChallenge);
			this.SetChallengeData();
		}

		// Token: 0x06001815 RID: 6165 RVA: 0x000697B4 File Offset: 0x000679B4
		public void SetNextChallenge()
		{
			if (this._currentChallengeIndex + 1 >= this._challengeList.Challenges.Count)
			{
				this._currentChallengeIndex = 0;
			}
			else
			{
				this._currentChallengeIndex++;
			}
			this._remasterMenuManager.CurrentChallenge = this._challengeList.Challenges[this._currentChallengeIndex];
			this._tvButtonsController.SwitchRandomSelectable();
			this.SetChallengeData();
		}

		// Token: 0x06001816 RID: 6166 RVA: 0x00069824 File Offset: 0x00067A24
		public void SetPreviousChallenge()
		{
			if (this._currentChallengeIndex - 1 < 0)
			{
				this._currentChallengeIndex = this._challengeList.Challenges.Count - 1;
			}
			else
			{
				this._currentChallengeIndex--;
			}
			this._remasterMenuManager.CurrentChallenge = this._challengeList.Challenges[this._currentChallengeIndex];
			this._tvButtonsController.SwitchRandomSelectable();
			this.SetChallengeData();
		}

		// Token: 0x06001817 RID: 6167 RVA: 0x00069896 File Offset: 0x00067A96
		private void SetChallengeData()
		{
			this.SetChallengeImage();
			this.SetChallengeTitle();
			this.SetChallengeDescription();
			this.SetRules();
			this.SetRewardIcons();
			this.SetRewardTerm();
			this.SetChallengeCompleted();
		}

		// Token: 0x06001818 RID: 6168 RVA: 0x000698C2 File Offset: 0x00067AC2
		private void SetChallengeImage()
		{
			this._challengeImage.sprite = this._remasterMenuManager.CurrentChallenge.ChallengeGraphic;
		}

		// Token: 0x06001819 RID: 6169 RVA: 0x000698DF File Offset: 0x00067ADF
		private void SetChallengeTitle()
		{
			this._challengeTitle.text = this._remasterMenuManager.CurrentChallenge.Name;
		}

		// Token: 0x0600181A RID: 6170 RVA: 0x00069901 File Offset: 0x00067B01
		private void SetChallengeDescription()
		{
			this._challengeDescription.text = this._remasterMenuManager.CurrentChallenge.Description;
		}

		// Token: 0x0600181B RID: 6171 RVA: 0x00069924 File Offset: 0x00067B24
		private void SetRules()
		{
			if (this._currentRules == null)
			{
				this._currentRules = new List<RuleController>();
			}
			List<MedalReward> goals = this._remasterMenuManager.CurrentChallenge.Mission.Goals;
			int num = 0;
			for (int i = 0; i < goals.Count; i++)
			{
				if (num < this._currentRules.Count)
				{
					this._currentRules[num].SetRule(goals[i].Goal.Description, this._goalRuleSprite);
					this._currentRules[num].gameObject.SetActive(true);
				}
				else
				{
					RuleController component = Object.Instantiate<RuleController>(this._rulePrefab, this._rulesHolder).GetComponent<RuleController>();
					component.SetRule(goals[i].Goal.Description, this._goalRuleSprite);
					this._currentRules.Add(component);
				}
				num++;
			}
			List<LocalizedString> objectives = this._remasterMenuManager.CurrentChallenge.Objectives;
			for (int j = 0; j < objectives.Count; j++)
			{
				if (num < this._currentRules.Count)
				{
					this._currentRules[num].SetRule(objectives[j], this._objectiveRuleSprite);
					this._currentRules[num].gameObject.SetActive(true);
				}
				else
				{
					RuleController component2 = Object.Instantiate<RuleController>(this._rulePrefab, this._rulesHolder).GetComponent<RuleController>();
					component2.SetRule(objectives[j], this._objectiveRuleSprite);
					this._currentRules.Add(component2);
				}
				num++;
			}
			for (int k = num; k < this._currentRules.Count; k++)
			{
				this._currentRules[k].gameObject.SetActive(false);
			}
		}

		// Token: 0x0600181C RID: 6172 RVA: 0x00069AEC File Offset: 0x00067CEC
		private void SetRewardIcons()
		{
			this._rewardIcons.DisableAllIcons();
			for (int i = 0; i < this._remasterMenuManager.CurrentChallenge.Rewards.Count; i++)
			{
				RewardItem rewardItem = this._remasterMenuManager.CurrentChallenge.Rewards[i];
				this._rewardIcons.SetNextIcon(rewardItem.Icon);
			}
		}

		// Token: 0x0600181D RID: 6173 RVA: 0x00069B4C File Offset: 0x00067D4C
		private void SetRewardTerm()
		{
			this._rewardName.text = string.Format(this._rewardForTerm.ToString(), (this._remasterMenuManager.CurrentChallenge.RewardName != null) ? this._remasterMenuManager.CurrentChallenge.RewardName.ToString() : string.Empty);
		}

		// Token: 0x0600181E RID: 6174 RVA: 0x00069BB6 File Offset: 0x00067DB6
		private void SetChallengeCompleted()
		{
			this._challengeCompleted.SetActive(this._remasterMenuManager.CurrentChallenge.IsUnlocked);
		}

		// Token: 0x0600181F RID: 6175 RVA: 0x00069BD3 File Offset: 0x00067DD3
		public void StartChallenge()
		{
			if (!this._alreadyTriggered)
			{
				this._alreadyTriggered = true;
				this._remasterMenuManager.StartChallenge();
			}
		}

		// Token: 0x040011B5 RID: 4533
		[SerializeField]
		private ChallengeList _challengeList;

		// Token: 0x040011B6 RID: 4534
		[SerializeField]
		private Image _challengeImage;

		// Token: 0x040011B7 RID: 4535
		[SerializeField]
		private GameObject _challengeCompleted;

		// Token: 0x040011B8 RID: 4536
		[SerializeField]
		private TextMeshProUGUI _challengeTitle;

		// Token: 0x040011B9 RID: 4537
		[SerializeField]
		private TextMeshProUGUI _challengeDescription;

		// Token: 0x040011BA RID: 4538
		[SerializeField]
		private TextMeshProUGUI _rewardName;

		// Token: 0x040011BB RID: 4539
		[SerializeField]
		private RuleController _rulePrefab;

		// Token: 0x040011BC RID: 4540
		[SerializeField]
		private Transform _rulesHolder;

		// Token: 0x040011BD RID: 4541
		[SerializeField]
		private Sprite _goalRuleSprite;

		// Token: 0x040011BE RID: 4542
		[SerializeField]
		private Sprite _objectiveRuleSprite;

		// Token: 0x040011BF RID: 4543
		[SerializeField]
		private ChallengeIconsController _rewardIcons;

		// Token: 0x040011C0 RID: 4544
		[SerializeField]
		private RemasterMenuManager _remasterMenuManager;

		// Token: 0x040011C1 RID: 4545
		[SerializeField]
		private LocalizedString _rewardForTerm;

		// Token: 0x040011C2 RID: 4546
		[SerializeField]
		private TvButtonsController _tvButtonsController;

		// Token: 0x040011C3 RID: 4547
		private int _currentChallengeIndex;

		// Token: 0x040011C4 RID: 4548
		private List<RuleController> _currentRules;

		// Token: 0x040011C5 RID: 4549
		private bool _alreadyTriggered;
	}
}

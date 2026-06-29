using System;
using I2.Loc;
using RG.SecondsRemaster.Scavenge;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RG.SecondsRemaster.Menu
{
	// Token: 0x02000290 RID: 656
	public class ChallengeDescriptionScavengeScreenController : MonoBehaviour
	{
		// Token: 0x06001805 RID: 6149 RVA: 0x000693DB File Offset: 0x000675DB
		public void Awake()
		{
			this._alreadyTriggered = false;
		}

		// Token: 0x06001806 RID: 6150 RVA: 0x000693E4 File Offset: 0x000675E4
		private void OnEnable()
		{
			if (this._remasterMenuManager == null)
			{
				this._remasterMenuManager = Object.FindObjectOfType<RemasterMenuManager>();
			}
			this._currentChallengeIndex = this._challengeList.Challenges.IndexOf(this._remasterMenuManager.CurrentChallenge);
			this.SetChallengeData();
		}

		// Token: 0x06001807 RID: 6151 RVA: 0x00069434 File Offset: 0x00067634
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

		// Token: 0x06001808 RID: 6152 RVA: 0x000694A4 File Offset: 0x000676A4
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

		// Token: 0x06001809 RID: 6153 RVA: 0x00069516 File Offset: 0x00067716
		private void SetChallengeData()
		{
			this.SetChallengeImage();
			this.SetChallengeTitle();
			this.SetChallengeDescription();
			this.SetCollectIcons();
			this.SetRewardIcons();
			this.SetRewardTerm();
			this.SetChallengeCompleted();
		}

		// Token: 0x0600180A RID: 6154 RVA: 0x00069542 File Offset: 0x00067742
		private void SetChallengeImage()
		{
			this._challengeImage.sprite = this._remasterMenuManager.CurrentChallenge.ChallengeGraphic;
		}

		// Token: 0x0600180B RID: 6155 RVA: 0x0006955F File Offset: 0x0006775F
		private void SetChallengeTitle()
		{
			this._challengeTitle.text = this._remasterMenuManager.CurrentChallenge.Name;
		}

		// Token: 0x0600180C RID: 6156 RVA: 0x00069581 File Offset: 0x00067781
		private void SetChallengeDescription()
		{
			this._challengeDescription.text = this._remasterMenuManager.CurrentChallenge.Description;
		}

		// Token: 0x0600180D RID: 6157 RVA: 0x000695A4 File Offset: 0x000677A4
		private void SetCollectIcons()
		{
			this._collectIcons.DisableAllIcons();
			for (int i = 0; i < this._remasterMenuManager.CurrentChallenge.Collectables.Count; i++)
			{
				ScavengeItem scavengeItem = this._remasterMenuManager.CurrentChallenge.Collectables[i];
				this._collectIcons.SetNextIcon(scavengeItem.MenuIcon);
			}
		}

		// Token: 0x0600180E RID: 6158 RVA: 0x00069604 File Offset: 0x00067804
		private void SetRewardIcons()
		{
			this._rewardIcons.DisableAllIcons();
			for (int i = 0; i < this._remasterMenuManager.CurrentChallenge.Rewards.Count; i++)
			{
				RewardItem rewardItem = this._remasterMenuManager.CurrentChallenge.Rewards[i];
				this._rewardIcons.SetNextIcon(rewardItem.Icon);
			}
		}

		// Token: 0x0600180F RID: 6159 RVA: 0x00069664 File Offset: 0x00067864
		private void SetRewardTerm()
		{
			this._rewardName.text = string.Format(this._rewardForTerm.ToString(), this._remasterMenuManager.CurrentChallenge.RewardName);
		}

		// Token: 0x06001810 RID: 6160 RVA: 0x0006969C File Offset: 0x0006789C
		private void SetChallengeCompleted()
		{
			if (this._remasterMenuManager.CurrentChallenge.IsUnlocked)
			{
				this._challengeCompletedTime.gameObject.SetActive(true);
				this._challengeCompletedTime.text = string.Format(this._challengeCompletedTerm.ToString(), string.Format("{0:0.00}", this._remasterMenuManager.CurrentChallenge.Time));
				this._challengeCompleted.SetActive(true);
				return;
			}
			this._challengeCompletedTime.gameObject.SetActive(false);
			this._challengeCompleted.SetActive(false);
		}

		// Token: 0x06001811 RID: 6161 RVA: 0x00069736 File Offset: 0x00067936
		public void StartChallenge()
		{
			if (!this._alreadyTriggered)
			{
				this._alreadyTriggered = true;
				this._remasterMenuManager.StartChallenge();
			}
		}

		// Token: 0x040011A6 RID: 4518
		[SerializeField]
		private ChallengeList _challengeList;

		// Token: 0x040011A7 RID: 4519
		[SerializeField]
		private Image _challengeImage;

		// Token: 0x040011A8 RID: 4520
		[SerializeField]
		private GameObject _challengeCompleted;

		// Token: 0x040011A9 RID: 4521
		[SerializeField]
		private TextMeshProUGUI _challengeTitle;

		// Token: 0x040011AA RID: 4522
		[SerializeField]
		private TextMeshProUGUI _challengeDescription;

		// Token: 0x040011AB RID: 4523
		[SerializeField]
		private TextMeshProUGUI _rewardName;

		// Token: 0x040011AC RID: 4524
		[SerializeField]
		private ChallengeIconsController _collectIcons;

		// Token: 0x040011AD RID: 4525
		[SerializeField]
		private ChallengeIconsController _rewardIcons;

		// Token: 0x040011AE RID: 4526
		[SerializeField]
		private RemasterMenuManager _remasterMenuManager;

		// Token: 0x040011AF RID: 4527
		[SerializeField]
		private LocalizedString _rewardForTerm;

		// Token: 0x040011B0 RID: 4528
		[SerializeField]
		private TvButtonsController _tvButtonsController;

		// Token: 0x040011B1 RID: 4529
		[SerializeField]
		private TextMeshProUGUI _challengeCompletedTime;

		// Token: 0x040011B2 RID: 4530
		[SerializeField]
		private LocalizedString _challengeCompletedTerm;

		// Token: 0x040011B3 RID: 4531
		private int _currentChallengeIndex;

		// Token: 0x040011B4 RID: 4532
		private bool _alreadyTriggered;
	}
}

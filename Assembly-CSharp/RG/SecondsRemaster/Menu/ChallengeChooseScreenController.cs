using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace RG.SecondsRemaster.Menu
{
	// Token: 0x0200028F RID: 655
	public class ChallengeChooseScreenController : MonoBehaviour
	{
		// Token: 0x060017FA RID: 6138 RVA: 0x00069100 File Offset: 0x00067300
		public void SetChallengeOnCenter(ChallengeSlotController challengeSlot)
		{
			for (int i = 0; i < this._allChallenges.Count; i++)
			{
				if (challengeSlot.Challenge == this._allChallenges[i])
				{
					this._currentIndex = RemasterMath.Modulo(i - 2, this._allChallenges.Count);
					this.UpdateChallengeDisplay();
					return;
				}
			}
		}

		// Token: 0x060017FB RID: 6139 RVA: 0x0006915C File Offset: 0x0006735C
		public void SetCurrentChallenge()
		{
			this._remasterMenuManager.CurrentChallenge = this._allChallenges[this.GetCycledIndexOfNumber(2)];
		}

		// Token: 0x060017FC RID: 6140 RVA: 0x0006917C File Offset: 0x0006737C
		private void GetAllChallenges()
		{
			if (this._allChallenges != null)
			{
				return;
			}
			this._allChallenges = new List<Challenge>();
			List<Challenge> challenges = this._challenges.Challenges;
			for (int i = 0; i < challenges.Count; i++)
			{
				this._allChallenges.Add(challenges[i]);
			}
		}

		// Token: 0x060017FD RID: 6141 RVA: 0x000691CC File Offset: 0x000673CC
		private void OnEnable()
		{
			if (this._remasterMenuManager == null)
			{
				this._remasterMenuManager = Object.FindObjectOfType<RemasterMenuManager>();
			}
			this.GetAllChallenges();
			if (this._remasterMenuManager.CurrentChallenge == null)
			{
				this.SetIndexOnFirstActiveChallenge();
			}
			else
			{
				this.SetIndexOnCurrentChallenge();
			}
			this.UpdateChallengeDisplay();
		}

		// Token: 0x060017FE RID: 6142 RVA: 0x0006921F File Offset: 0x0006741F
		public void SetIndexOnFirstActiveChallenge()
		{
			this._currentIndex = 0;
		}

		// Token: 0x060017FF RID: 6143 RVA: 0x00069228 File Offset: 0x00067428
		public void SetIndexOnCurrentChallenge()
		{
			Challenge currentChallenge = this._remasterMenuManager.CurrentChallenge;
			for (int i = 0; i < this._allChallenges.Count; i++)
			{
				if (this._allChallenges[i] == currentChallenge)
				{
					this._currentIndex = RemasterMath.Modulo(i - 2, this._allChallenges.Count);
					return;
				}
			}
			this._currentIndex = 0;
		}

		// Token: 0x06001800 RID: 6144 RVA: 0x0006928C File Offset: 0x0006748C
		public int GetCycledIndexOfNumber(int number)
		{
			return (this._currentIndex + number) % this._allChallenges.Count;
		}

		// Token: 0x06001801 RID: 6145 RVA: 0x000692A2 File Offset: 0x000674A2
		public void CycleChallengeForth()
		{
			this._currentIndex++;
			if (this._currentIndex >= this._allChallenges.Count)
			{
				this._currentIndex = 0;
			}
			this.UpdateChallengeDisplay();
		}

		// Token: 0x06001802 RID: 6146 RVA: 0x000692D2 File Offset: 0x000674D2
		public void CycleChallengeBack()
		{
			this._currentIndex--;
			if (this._currentIndex < 0)
			{
				this._currentIndex = this._allChallenges.Count - 1;
			}
			this.UpdateChallengeDisplay();
		}

		// Token: 0x06001803 RID: 6147 RVA: 0x00069304 File Offset: 0x00067504
		public void UpdateChallengeDisplay()
		{
			this._challengesSlotController[0].SetChallengeData(this._allChallenges[this.GetCycledIndexOfNumber(0)]);
			this._challengesSlotController[1].SetChallengeData(this._allChallenges[this.GetCycledIndexOfNumber(1)]);
			this._challengesSlotController[2].SetChallengeData(this._allChallenges[this.GetCycledIndexOfNumber(2)]);
			this._challengesSlotController[3].SetChallengeData(this._allChallenges[this.GetCycledIndexOfNumber(3)]);
			this._challengesSlotController[4].SetChallengeData(this._allChallenges[this.GetCycledIndexOfNumber(4)]);
			this._challengeTitle.text = this._allChallenges[this.GetCycledIndexOfNumber(2)].Name;
		}

		// Token: 0x0400119F RID: 4511
		[SerializeField]
		private ChallengeSlotController[] _challengesSlotController;

		// Token: 0x040011A0 RID: 4512
		[SerializeField]
		private TextMeshProUGUI _challengeTitle;

		// Token: 0x040011A1 RID: 4513
		[SerializeField]
		private ChallengeList _challenges;

		// Token: 0x040011A2 RID: 4514
		[SerializeField]
		private RemasterMenuManager _remasterMenuManager;

		// Token: 0x040011A3 RID: 4515
		private int _currentIndex;

		// Token: 0x040011A4 RID: 4516
		private List<Challenge> _allChallenges;

		// Token: 0x040011A5 RID: 4517
		private const int CENTER_INDEX = 2;
	}
}

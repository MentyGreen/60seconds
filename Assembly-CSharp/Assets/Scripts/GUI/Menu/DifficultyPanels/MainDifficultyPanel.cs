using System;
using UnityEngine;

namespace Assets.Scripts.GUI.Menu.DifficultyPanels
{
	// Token: 0x02000176 RID: 374
	internal class MainDifficultyPanel : DifficultyPanel
	{
		// Token: 0x06001080 RID: 4224 RVA: 0x00045D2F File Offset: 0x00043F2F
		public override string SetDifficulty(EGameDifficulty difficulty)
		{
			if (this._arrowsDifficulty != null)
			{
				return this._arrowsDifficulty.SetDifficulty(difficulty);
			}
			if (this._difficultyButtons != null)
			{
				return this._difficultyButtons.SetDifficulty(difficulty);
			}
			return null;
		}

		// Token: 0x06001081 RID: 4225 RVA: 0x00045D68 File Offset: 0x00043F68
		public override void SetGameType(EGameType type)
		{
			bool isVisible = this._difficultyInfoPanelSurvival.IsVisible = (type == EGameType.SURVIVAL);
			if (this._arrowsDifficulty != null)
			{
				this._arrowsDifficulty.IsVisible = isVisible;
			}
			else if (this._difficultyButtons != null)
			{
				this._difficultyButtons.IsVisible = isVisible;
			}
			this._challengeDataPanelScavenge.IsVisible = (type == EGameType.CHALLENGE_SCAVENGE);
			this._challengeDataPanelSurvival.IsVisible = (type == EGameType.CHALLENGE_SURVIVAL);
		}

		// Token: 0x06001082 RID: 4226 RVA: 0x00045DDC File Offset: 0x00043FDC
		public void LoadDifficulties()
		{
			if (this._arrowsDifficulty != null)
			{
				this._arrowsDifficulty.Easy.Tag = EGameDifficulty.EASY;
				this._arrowsDifficulty.Normal.Tag = EGameDifficulty.NORMAL;
				this._arrowsDifficulty.Hard.Tag = EGameDifficulty.HARD;
				return;
			}
			if (this._difficultyButtons != null)
			{
				this._difficultyButtons.Easy.Tag = EGameDifficulty.EASY;
				this._difficultyButtons.Normal.Tag = EGameDifficulty.NORMAL;
				this._difficultyButtons.Hard.Tag = EGameDifficulty.HARD;
			}
		}

		// Token: 0x06001083 RID: 4227 RVA: 0x00045E8C File Offset: 0x0004408C
		public void SetButtonsTextScale(float diffButtonScale)
		{
			dfButton easy = this._difficultyButtons.Easy;
			dfButton normal = this._difficultyButtons.Normal;
			this._difficultyButtons.Hard.TextScale = diffButtonScale;
			normal.TextScale = diffButtonScale;
			easy.TextScale = diffButtonScale;
		}

		// Token: 0x04000AA1 RID: 2721
		[SerializeField]
		private dfPanel _difficultyInfoPanelSurvival;

		// Token: 0x04000AA2 RID: 2722
		[SerializeField]
		private dfPanel _challengeDataPanelScavenge;

		// Token: 0x04000AA3 RID: 2723
		[SerializeField]
		private dfPanel _challengeDataPanelSurvival;

		// Token: 0x04000AA4 RID: 2724
		[SerializeField]
		private DifficultyButtons _difficultyButtons;

		// Token: 0x04000AA5 RID: 2725
		[SerializeField]
		private DifficultyArrows _arrowsDifficulty;
	}
}

using System;
using UnityEngine;

namespace Assets.Scripts.GUI.Menu.DifficultyPanels
{
	// Token: 0x02000170 RID: 368
	internal class CharacterDifficultyPanel : DifficultyPanel
	{
		// Token: 0x06001065 RID: 4197 RVA: 0x000459FB File Offset: 0x00043BFB
		public override string SetDifficulty(EGameDifficulty difficulty)
		{
			return this._arrowsDifficulty.SetDifficulty(difficulty);
		}

		// Token: 0x06001066 RID: 4198 RVA: 0x00045A09 File Offset: 0x00043C09
		public override void SetGameType(EGameType type)
		{
			this._difficultyInfoPanelFull.IsVisible = (type == EGameType.FULL);
			this._difficultyInfoPanelScavenge.IsVisible = (type == EGameType.SCAVENGE);
		}

		// Token: 0x06001067 RID: 4199 RVA: 0x00045A29 File Offset: 0x00043C29
		public void SetCharacter(ECharacter character)
		{
			this._characterPanel.SetCharacter(character);
		}

		// Token: 0x04000A8D RID: 2701
		[SerializeField]
		private DifficultyArrows _arrowsDifficulty;

		// Token: 0x04000A8E RID: 2702
		[SerializeField]
		private CharacterPanel _characterPanel;

		// Token: 0x04000A8F RID: 2703
		[SerializeField]
		private dfPanel _difficultyInfoPanelFull;

		// Token: 0x04000A90 RID: 2704
		[SerializeField]
		private dfPanel _difficultyInfoPanelScavenge;
	}
}

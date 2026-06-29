using System;
using UnityEngine;

namespace Assets.Scripts.GUI.Menu.DifficultyPanels
{
	// Token: 0x02000175 RID: 373
	internal abstract class DifficultyPanel : ControlPanel
	{
		// Token: 0x0600107B RID: 4219 RVA: 0x00045CF1 File Offset: 0x00043EF1
		public void SetTitleLabel(string text)
		{
			this._gameModeTitleLabel.Text = text;
		}

		// Token: 0x0600107C RID: 4220 RVA: 0x00045CFF File Offset: 0x00043EFF
		public void SetDescriptionLabel(string text)
		{
			this._gameModeDescriptionLabel.Text = text;
		}

		// Token: 0x0600107D RID: 4221 RVA: 0x00045D0D File Offset: 0x00043F0D
		public void SetImage(string sprite, dfAtlas atlas)
		{
			this._gameModeImg.SpriteName = sprite;
			this._gameModeImg.Atlas = atlas;
		}

		// Token: 0x0600107E RID: 4222
		public abstract void SetGameType(EGameType type);

		// Token: 0x04000A9E RID: 2718
		[SerializeField]
		private dfLabel _gameModeTitleLabel;

		// Token: 0x04000A9F RID: 2719
		[SerializeField]
		private dfSprite _gameModeImg;

		// Token: 0x04000AA0 RID: 2720
		[SerializeField]
		private dfRichTextLabel _gameModeDescriptionLabel;
	}
}

using System;
using UnityEngine;

namespace Assets.Scripts.GUI.Menu.DifficultyPanels
{
	// Token: 0x02000173 RID: 371
	internal class DifficultyArrows : ControlPanel
	{
		// Token: 0x17000349 RID: 841
		// (get) Token: 0x06001071 RID: 4209 RVA: 0x00045AB1 File Offset: 0x00043CB1
		public dfLabel Easy
		{
			get
			{
				return this.easy;
			}
		}

		// Token: 0x1700034A RID: 842
		// (get) Token: 0x06001072 RID: 4210 RVA: 0x00045AB9 File Offset: 0x00043CB9
		public dfLabel Normal
		{
			get
			{
				return this.normal;
			}
		}

		// Token: 0x1700034B RID: 843
		// (get) Token: 0x06001073 RID: 4211 RVA: 0x00045AC1 File Offset: 0x00043CC1
		public dfLabel Hard
		{
			get
			{
				return this.hard;
			}
		}

		// Token: 0x06001074 RID: 4212 RVA: 0x00045ACC File Offset: 0x00043CCC
		public override string SetDifficulty(EGameDifficulty difficulty)
		{
			string result = string.Empty;
			switch (difficulty)
			{
			case EGameDifficulty.EASY:
				this.Easy.IsVisible = true;
				this.Normal.IsVisible = (this.Hard.IsVisible = false);
				result = "easy";
				break;
			case EGameDifficulty.NORMAL:
				this.Normal.IsVisible = true;
				this.Easy.IsVisible = (this.Hard.IsVisible = false);
				result = "normal";
				break;
			case EGameDifficulty.HARD:
				this.Hard.IsVisible = true;
				this.Easy.IsVisible = (this.Normal.IsVisible = false);
				result = "hard";
				break;
			}
			return result;
		}

		// Token: 0x04000A96 RID: 2710
		[SerializeField]
		private dfLabel easy;

		// Token: 0x04000A97 RID: 2711
		[SerializeField]
		private dfLabel normal;

		// Token: 0x04000A98 RID: 2712
		[SerializeField]
		private dfLabel hard;

		// Token: 0x04000A99 RID: 2713
		[SerializeField]
		private dfButton leftButton;

		// Token: 0x04000A9A RID: 2714
		[SerializeField]
		private dfButton rightButton;
	}
}

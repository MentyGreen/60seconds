using System;
using UnityEngine;

namespace Assets.Scripts.GUI.Menu.DifficultyPanels
{
	// Token: 0x02000174 RID: 372
	[RequireComponent(typeof(dfControl))]
	internal class DifficultyButtons : ControlPanel
	{
		// Token: 0x1700034C RID: 844
		// (get) Token: 0x06001076 RID: 4214 RVA: 0x00045B89 File Offset: 0x00043D89
		public dfButton Easy
		{
			get
			{
				return this.easy;
			}
		}

		// Token: 0x1700034D RID: 845
		// (get) Token: 0x06001077 RID: 4215 RVA: 0x00045B91 File Offset: 0x00043D91
		public dfButton Normal
		{
			get
			{
				return this.normal;
			}
		}

		// Token: 0x1700034E RID: 846
		// (get) Token: 0x06001078 RID: 4216 RVA: 0x00045B99 File Offset: 0x00043D99
		public dfButton Hard
		{
			get
			{
				return this.hard;
			}
		}

		// Token: 0x06001079 RID: 4217 RVA: 0x00045BA4 File Offset: 0x00043DA4
		public override string SetDifficulty(EGameDifficulty difficulty)
		{
			string result = string.Empty;
			switch (difficulty)
			{
			case EGameDifficulty.EASY:
				this.Easy.TextColor = Color.red;
				this.Normal.TextColor = Color.black;
				this.Hard.TextColor = Color.black;
				result = "easy";
				break;
			case EGameDifficulty.NORMAL:
				this.Normal.TextColor = Color.red;
				this.Easy.TextColor = Color.black;
				this.Hard.TextColor = Color.black;
				result = "normal";
				break;
			case EGameDifficulty.HARD:
				this.Hard.TextColor = Color.red;
				this.Normal.TextColor = Color.black;
				this.Easy.TextColor = Color.black;
				result = "hard";
				break;
			}
			this.Easy.HoverTextColor = this.Easy.TextColor;
			this.Normal.HoverTextColor = this.Normal.TextColor;
			this.Hard.HoverTextColor = this.Hard.TextColor;
			return result;
		}

		// Token: 0x04000A9B RID: 2715
		[SerializeField]
		private dfButton easy;

		// Token: 0x04000A9C RID: 2716
		[SerializeField]
		private dfButton normal;

		// Token: 0x04000A9D RID: 2717
		[SerializeField]
		private dfButton hard;
	}
}

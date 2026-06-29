using System;
using UnityEngine;

namespace Assets.Scripts.GUI.Menu.DifficultyPanels
{
	// Token: 0x02000172 RID: 370
	[RequireComponent(typeof(dfControl))]
	internal abstract class ControlPanel : MonoBehaviour
	{
		// Token: 0x17000348 RID: 840
		// (get) Token: 0x0600106D RID: 4205 RVA: 0x00045A8E File Offset: 0x00043C8E
		// (set) Token: 0x0600106E RID: 4206 RVA: 0x00045A9B File Offset: 0x00043C9B
		public bool IsVisible
		{
			get
			{
				return this.control.IsVisible;
			}
			set
			{
				this.control.IsVisible = value;
			}
		}

		// Token: 0x0600106F RID: 4207
		public abstract string SetDifficulty(EGameDifficulty difficulty);

		// Token: 0x04000A95 RID: 2709
		[SerializeField]
		private dfControl control;
	}
}

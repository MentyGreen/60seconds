using System;
using UnityEngine;

namespace Assets.Scripts.GUI.Menu.DifficultyPanels
{
	// Token: 0x02000171 RID: 369
	[RequireComponent(typeof(dfControl))]
	internal class CharacterPanel : MonoBehaviour
	{
		// Token: 0x17000347 RID: 839
		// (get) Token: 0x06001069 RID: 4201 RVA: 0x00045A3F File Offset: 0x00043C3F
		// (set) Token: 0x0600106A RID: 4202 RVA: 0x00045A4C File Offset: 0x00043C4C
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

		// Token: 0x0600106B RID: 4203 RVA: 0x00045A5A File Offset: 0x00043C5A
		public void SetCharacter(ECharacter character)
		{
			if (character == ECharacter.DAD)
			{
				this.charIcon.SpriteName = "icon_dad";
				return;
			}
			if (character != ECharacter.MOM)
			{
				return;
			}
			this.charIcon.SpriteName = "icon_mom";
		}

		// Token: 0x04000A91 RID: 2705
		[SerializeField]
		private dfControl control;

		// Token: 0x04000A92 RID: 2706
		[SerializeField]
		private dfButton nextButton;

		// Token: 0x04000A93 RID: 2707
		[SerializeField]
		private dfButton prevButton;

		// Token: 0x04000A94 RID: 2708
		[SerializeField]
		private dfSprite charIcon;
	}
}

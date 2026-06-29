using System;
using RG.Parsecs.Survival;
using UnityEngine;

namespace RG.SecondsRemaster.Menu
{
	// Token: 0x02000295 RID: 661
	public class CharacterEventToggleController : UnityEventToggleController
	{
		// Token: 0x1700047E RID: 1150
		// (get) Token: 0x0600182A RID: 6186 RVA: 0x00069DB3 File Offset: 0x00067FB3
		public Character Character
		{
			get
			{
				return this._character;
			}
		}

		// Token: 0x040011CE RID: 4558
		[SerializeField]
		private Character _character;
	}
}

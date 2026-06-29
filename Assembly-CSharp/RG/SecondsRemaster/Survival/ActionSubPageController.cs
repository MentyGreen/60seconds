using System;
using UnityEngine;

namespace RG.SecondsRemaster.Survival
{
	// Token: 0x02000315 RID: 789
	public class ActionSubPageController : SubPageController
	{
		// Token: 0x06001AB0 RID: 6832 RVA: 0x00073B53 File Offset: 0x00071D53
		public void SetDoodlesHolder(GameObject doodlesHolder)
		{
			this._doodlesHolder = doodlesHolder;
		}

		// Token: 0x06001AB1 RID: 6833 RVA: 0x00073B5C File Offset: 0x00071D5C
		public override void Show()
		{
			base.Show();
			if (this._doodlesHolder != null)
			{
				this._doodlesHolder.SetActive(true);
			}
		}

		// Token: 0x06001AB2 RID: 6834 RVA: 0x00073B7E File Offset: 0x00071D7E
		public override void Hide()
		{
			base.Hide();
			if (this._doodlesHolder != null)
			{
				this._doodlesHolder.SetActive(false);
			}
		}

		// Token: 0x04001487 RID: 5255
		[SerializeField]
		private GameObject _doodlesHolder;
	}
}

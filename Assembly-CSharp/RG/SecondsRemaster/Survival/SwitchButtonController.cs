using System;
using RG.Parsecs.Survival;
using UnityEngine;
using UnityEngine.UI;

namespace RG.SecondsRemaster.Survival
{
	// Token: 0x02000311 RID: 785
	public class SwitchButtonController : MonoBehaviour
	{
		// Token: 0x06001A89 RID: 6793 RVA: 0x00073330 File Offset: 0x00071530
		public void SetSelectable(Button selectable)
		{
			this._selectable = selectable;
		}

		// Token: 0x06001A8A RID: 6794 RVA: 0x00073339 File Offset: 0x00071539
		public void SetButtonInteractable(bool interactable)
		{
			if (this._selectable != null)
			{
				this._selectable.interactable = interactable;
			}
			this._survivalData.DailyEventResolved = interactable;
		}

		// Token: 0x04001462 RID: 5218
		[SerializeField]
		private SurvivalData _survivalData;

		// Token: 0x04001463 RID: 5219
		private Selectable _selectable;
	}
}

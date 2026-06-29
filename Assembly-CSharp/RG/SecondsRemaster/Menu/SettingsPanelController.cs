using System;
using System.Collections.Generic;
using UnityEngine;

namespace RG.SecondsRemaster.Menu
{
	// Token: 0x020002B0 RID: 688
	public class SettingsPanelController : MonoBehaviour
	{
		// Token: 0x060018A6 RID: 6310 RVA: 0x0006C560 File Offset: 0x0006A760
		private void Start()
		{
			if (SteamManager.IsRunningOnSteamDeck())
			{
				for (int i = 0; i <= this._canvasGroupsToDisableOnSteamDeck.Count - 1; i++)
				{
					this._canvasGroupsToDisableOnSteamDeck[i].alpha = 0.3f;
					this._canvasGroupsToDisableOnSteamDeck[i].interactable = false;
					this._canvasGroupsToDisableOnSteamDeck[i].blocksRaycasts = false;
				}
			}
		}

		// Token: 0x060018A7 RID: 6311 RVA: 0x0006C5C6 File Offset: 0x0006A7C6
		private void OnDisable()
		{
			if (this._controlsPanelController != null)
			{
				this._controlsPanelController.SaveSettings();
			}
		}

		// Token: 0x0400126E RID: 4718
		[SerializeField]
		private List<CanvasGroup> _canvasGroupsToDisableOnSteamDeck = new List<CanvasGroup>();

		// Token: 0x0400126F RID: 4719
		[SerializeField]
		private ControlsPanelController _controlsPanelController;
	}
}

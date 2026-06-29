using System;
using Rewired;
using UnityEngine;

namespace RG.SecondsRemaster.Menu
{
	// Token: 0x020002A1 RID: 673
	public class GoBackOnCancelPress : MonoBehaviour
	{
		// Token: 0x06001857 RID: 6231 RVA: 0x0006A283 File Offset: 0x00068483
		private void Awake()
		{
			this._player = ReInput.players.GetPlayer(0);
		}

		// Token: 0x06001858 RID: 6232 RVA: 0x0006A296 File Offset: 0x00068496
		private void Update()
		{
			if (this._blocked)
			{
				return;
			}
			if (this._player.GetButtonDown(30))
			{
				this._screensController.GoBack();
			}
		}

		// Token: 0x06001859 RID: 6233 RVA: 0x0006A2BB File Offset: 0x000684BB
		public void SetCloseActionBlocked(bool block)
		{
			this._blocked = block;
		}

		// Token: 0x040011F7 RID: 4599
		[SerializeField]
		private ScreensController _screensController;

		// Token: 0x040011F8 RID: 4600
		private Player _player;

		// Token: 0x040011F9 RID: 4601
		private bool _blocked;
	}
}

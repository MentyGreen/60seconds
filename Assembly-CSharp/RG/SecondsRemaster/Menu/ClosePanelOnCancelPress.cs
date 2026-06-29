using System;
using Rewired;
using RG.Parsecs.Common;
using UnityEngine;

namespace RG.SecondsRemaster.Menu
{
	// Token: 0x020002A0 RID: 672
	public class ClosePanelOnCancelPress : MonoBehaviour
	{
		// Token: 0x06001853 RID: 6227 RVA: 0x0006A220 File Offset: 0x00068420
		private void Awake()
		{
			this._player = ReInput.players.GetPlayer(0);
		}

		// Token: 0x06001854 RID: 6228 RVA: 0x0006A233 File Offset: 0x00068433
		private void Update()
		{
			if (this._blocked)
			{
				return;
			}
			if (this._player.GetButtonDown(30))
			{
				this._panelToClose.SetActive(false);
				if (this._soundEmitter != null)
				{
					this._soundEmitter.PlaySound();
				}
			}
		}

		// Token: 0x06001855 RID: 6229 RVA: 0x0006A272 File Offset: 0x00068472
		public void SetCloseActionBlocked(bool block)
		{
			this._blocked = block;
		}

		// Token: 0x040011F3 RID: 4595
		[SerializeField]
		private GameObject _panelToClose;

		// Token: 0x040011F4 RID: 4596
		[SerializeField]
		private OnUIClickedSoundPlayer _soundEmitter;

		// Token: 0x040011F5 RID: 4597
		private Player _player;

		// Token: 0x040011F6 RID: 4598
		private bool _blocked;
	}
}

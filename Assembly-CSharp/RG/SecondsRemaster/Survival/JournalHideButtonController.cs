using System;
using Rewired;
using RG.VirtualInput;
using UnityEngine;
using UnityEngine.UI;

namespace RG.SecondsRemaster.Survival
{
	// Token: 0x0200030F RID: 783
	public class JournalHideButtonController : MonoBehaviour
	{
		// Token: 0x06001A7E RID: 6782 RVA: 0x0007303F File Offset: 0x0007123F
		public void Awake()
		{
			this._player = ReInput.players.GetPlayer(0);
		}

		// Token: 0x06001A7F RID: 6783 RVA: 0x00073054 File Offset: 0x00071254
		public void Update()
		{
			if (Time.timeScale == 0f)
			{
				return;
			}
			if (this._player != null && this._player.GetButtonDown(38))
			{
				this._shouldCheck = (this._journalController.CurrentJournalState == JournalController.JournalState.VISIBLE || this._journalController.CurrentJournalState == JournalController.JournalState.PARTIALLY_HIDDEN);
			}
			if (this._player != null && this._player.GetButton(38) && this._shouldCheck)
			{
				this._buttonHeldDownTime += Time.deltaTime;
			}
			if (this._player != null && this._player.GetButtonUp(38) && this._shouldCheck)
			{
				if (this._buttonHeldDownTime <= 0.17f)
				{
					this._button.onClick.Invoke();
				}
				this._buttonHeldDownTime = 0f;
			}
		}

		// Token: 0x06001A80 RID: 6784 RVA: 0x00073122 File Offset: 0x00071322
		public void OnClick()
		{
			this.Activate();
		}

		// Token: 0x06001A81 RID: 6785 RVA: 0x0007312A File Offset: 0x0007132A
		public bool Show()
		{
			if (this._journalController.CurrentJournalState == JournalController.JournalState.PARTIALLY_HIDDEN)
			{
				this._journalController.Show();
				this._closed.Hide();
				this._journal.Show();
				return true;
			}
			return false;
		}

		// Token: 0x06001A82 RID: 6786 RVA: 0x00073160 File Offset: 0x00071360
		public void Activate()
		{
			if (this._journalController.CurrentJournalState == JournalController.JournalState.HIDE || this._journalController.CurrentJournalState == JournalController.JournalState.HIDDEN)
			{
				return;
			}
			if (!this.Show())
			{
				this._journalController.PartiallyHide();
				this._journal.Hide();
				this._closed.Show();
			}
		}

		// Token: 0x04001452 RID: 5202
		[SerializeField]
		private JournalController _journalController;

		// Token: 0x04001453 RID: 5203
		[SerializeField]
		private VirtualInputClosablePanel _journal;

		// Token: 0x04001454 RID: 5204
		[SerializeField]
		private VirtualInputClosablePanel _closed;

		// Token: 0x04001455 RID: 5205
		[SerializeField]
		private Button _button;

		// Token: 0x04001456 RID: 5206
		private Player _player;

		// Token: 0x04001457 RID: 5207
		private float _buttonHeldDownTime;

		// Token: 0x04001458 RID: 5208
		private bool _shouldCheck;
	}
}

using System;
using System.Collections;
using Rewired;
using UnityEngine;
using UnityEngine.UI;

namespace RG.SecondsRemaster.Survival
{
	// Token: 0x02000310 RID: 784
	[RequireComponent(typeof(Button))]
	public class JournalPageButtonHelper : MonoBehaviour
	{
		// Token: 0x06001A84 RID: 6788 RVA: 0x000731BA File Offset: 0x000713BA
		private void Awake()
		{
			this._player = ReInput.players.GetPlayer(0);
			this._button = base.GetComponent<Button>();
		}

		// Token: 0x06001A85 RID: 6789 RVA: 0x000731DC File Offset: 0x000713DC
		public void Update()
		{
			int actionId = this._isNextPageButton ? 39 : 40;
			if (this._player.GetButtonDown(actionId) && this._button.interactable && (this._journalController.CurrentJournalState == JournalController.JournalState.VISIBLE || this._ignoreJournal))
			{
				this._startedOnThisButton = true;
			}
			if (this._player.GetButton(actionId) && this._startedOnThisButton)
			{
				this._timePassed += Time.deltaTime;
				this._fillImage.fillAmount = this._timePassed / this._timeNeededToClick;
				if (this._timePassed >= this._timeNeededToClick)
				{
					this._button.onClick.Invoke();
					if (!this._ignoreJournal)
					{
						this._journalController.StartCoroutine(this.WaitTimeAndResetFillAmount());
					}
					this._timePassed = 0f;
					this._startedOnThisButton = false;
				}
			}
			if (this._player.GetButtonUp(actionId))
			{
				this._fillImage.fillAmount = 0f;
				this._timePassed = 0f;
				this._startedOnThisButton = false;
			}
		}

		// Token: 0x06001A86 RID: 6790 RVA: 0x000732EA File Offset: 0x000714EA
		public void ResetButton()
		{
			this._fillImage.fillAmount = 0f;
			this._timePassed = 0f;
			this._startedOnThisButton = false;
		}

		// Token: 0x06001A87 RID: 6791 RVA: 0x0007330E File Offset: 0x0007150E
		private IEnumerator WaitTimeAndResetFillAmount()
		{
			yield return new WaitForSeconds(0.15f);
			this._fillImage.fillAmount = 0f;
			yield break;
		}

		// Token: 0x04001459 RID: 5209
		[SerializeField]
		private JournalController _journalController;

		// Token: 0x0400145A RID: 5210
		[SerializeField]
		private float _timeNeededToClick = 1f;

		// Token: 0x0400145B RID: 5211
		[SerializeField]
		private Image _fillImage;

		// Token: 0x0400145C RID: 5212
		[SerializeField]
		private bool _isNextPageButton;

		// Token: 0x0400145D RID: 5213
		[SerializeField]
		private bool _ignoreJournal;

		// Token: 0x0400145E RID: 5214
		private Button _button;

		// Token: 0x0400145F RID: 5215
		private Player _player;

		// Token: 0x04001460 RID: 5216
		private bool _startedOnThisButton;

		// Token: 0x04001461 RID: 5217
		private float _timePassed;
	}
}

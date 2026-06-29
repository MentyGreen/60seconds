using System;
using System.Collections;
using Rewired;
using RG.Parsecs.EventEditor;
using RG.Parsecs.Survival;
using RG.VirtualInput;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace RG.SecondsRemaster.Survival
{
	// Token: 0x02000312 RID: 786
	public class JournalButtonController : MonoBehaviour, IRefreshInteractable
	{
		// Token: 0x06001A8C RID: 6796 RVA: 0x00073369 File Offset: 0x00071569
		public void Start()
		{
			this._isInteractable = false;
			base.Invoke("ShowDelayed", this._journalFirstDayShowDelay);
			this._player = ReInput.players.GetPlayer(0);
		}

		// Token: 0x06001A8D RID: 6797 RVA: 0x00073394 File Offset: 0x00071594
		public void Update()
		{
			if (Time.timeScale == 0f)
			{
				return;
			}
			if (this._player != null && this._button.interactable && this._player.GetButtonDown(38))
			{
				base.StartCoroutine(this.WaitFrameAndClickJournal());
			}
		}

		// Token: 0x06001A8E RID: 6798 RVA: 0x000733D4 File Offset: 0x000715D4
		private IEnumerator WaitFrameAndClickJournal()
		{
			yield return new WaitForEndOfFrame();
			yield return new WaitForEndOfFrame();
			this._button.onClick.Invoke();
			yield break;
		}

		// Token: 0x06001A8F RID: 6799 RVA: 0x000733E4 File Offset: 0x000715E4
		public void OnClick()
		{
			this._button.interactable = false;
			this._isInteractable = false;
			if (this._isDemoVariable.Value && this._survivalData.CurrentDay >= 11 && !this._isTutorialVariable.Value)
			{
				this._survivalDemoEndPopup.SetActive(true);
			}
			else if (!this._showJournal.Value)
			{
				this._endGameData.RuntimeData.ShouldEndGame = true;
				EndGameManager.Instance.LoadEndGameScene();
			}
			else
			{
				EventSystem.current.SetSelectedGameObject(null);
				this._journalController.Show();
			}
			this._showJournalPrompt.gameObject.SetActive(false);
			this._animator.SetTrigger("Hide");
		}

		// Token: 0x06001A90 RID: 6800 RVA: 0x0007349C File Offset: 0x0007169C
		public void Show()
		{
			if (this._displayJournalDestroyed != null)
			{
				this.SetJournalGraphics(this._displayJournalDestroyed.Value);
			}
			this._showJournalPrompt.gameObject.SetActive(true);
			base.Invoke("ShowDelayed", this._journalShowDelay);
		}

		// Token: 0x06001A91 RID: 6801 RVA: 0x000734EA File Offset: 0x000716EA
		private void ShowDelayed()
		{
			this._button.interactable = true;
			this._isInteractable = true;
			this._animator.SetTrigger("Show");
			this._gamepadButton.SelectThisSelectable();
		}

		// Token: 0x06001A92 RID: 6802 RVA: 0x0007351A File Offset: 0x0007171A
		public void Hide()
		{
			this._showJournalPrompt.gameObject.SetActive(false);
			this._animator.SetTrigger("Hide");
		}

		// Token: 0x06001A93 RID: 6803 RVA: 0x0007353D File Offset: 0x0007173D
		public void RefreshInteractable()
		{
			this._button.interactable = this._isInteractable;
			this._gamepadButton.SetInteractable();
		}

		// Token: 0x06001A94 RID: 6804 RVA: 0x0007355C File Offset: 0x0007175C
		public void SetJournalGraphics(bool isDestroyed)
		{
			for (int i = 0; i < this._normalJournalButtonObjects.Length; i++)
			{
				this._normalJournalButtonObjects[i].SetActive(!isDestroyed);
			}
			for (int j = 0; j < this._destroyedJournalButtonObjects.Length; j++)
			{
				this._destroyedJournalButtonObjects[j].SetActive(isDestroyed);
			}
		}

		// Token: 0x04001464 RID: 5220
		[SerializeField]
		private JournalController _journalController;

		// Token: 0x04001465 RID: 5221
		[SerializeField]
		private float _journalShowDelay = 1.5f;

		// Token: 0x04001466 RID: 5222
		[SerializeField]
		private float _journalFirstDayShowDelay = 4.5f;

		// Token: 0x04001467 RID: 5223
		[SerializeField]
		private Animator _animator;

		// Token: 0x04001468 RID: 5224
		[SerializeField]
		private EndGameData _endGameData;

		// Token: 0x04001469 RID: 5225
		[SerializeField]
		private GlobalBoolVariable _showJournal;

		// Token: 0x0400146A RID: 5226
		[SerializeField]
		private GameObject[] _normalJournalButtonObjects;

		// Token: 0x0400146B RID: 5227
		[SerializeField]
		private GameObject[] _destroyedJournalButtonObjects;

		// Token: 0x0400146C RID: 5228
		[SerializeField]
		private GlobalBoolVariable _displayJournalDestroyed;

		// Token: 0x0400146D RID: 5229
		[SerializeField]
		private Button _button;

		// Token: 0x0400146E RID: 5230
		[SerializeField]
		private VirtualInputButton _gamepadButton;

		// Token: 0x0400146F RID: 5231
		[SerializeField]
		private SurvivalPrompt _showJournalPrompt;

		// Token: 0x04001470 RID: 5232
		[Header("Demo")]
		[SerializeField]
		private GlobalBoolVariable _isDemoVariable;

		// Token: 0x04001471 RID: 5233
		[SerializeField]
		private SurvivalData _survivalData;

		// Token: 0x04001472 RID: 5234
		[SerializeField]
		private GlobalBoolVariable _isTutorialVariable;

		// Token: 0x04001473 RID: 5235
		[SerializeField]
		private GameObject _survivalDemoEndPopup;

		// Token: 0x04001474 RID: 5236
		private const string SHOW_TRIGGER_NAME = "Show";

		// Token: 0x04001475 RID: 5237
		private const string HIDE_TRIGGER_NAME = "Hide";

		// Token: 0x04001476 RID: 5238
		private bool _isInteractable;

		// Token: 0x04001477 RID: 5239
		private Player _player;
	}
}

using System;
using RG.Parsecs.Common;
using RG.Parsecs.EventEditor;
using RG.Parsecs.Survival;
using RG.VirtualInput;
using UnityEngine;
using UnityEngine.EventSystems;

namespace RG.SecondsRemaster.Survival
{
	// Token: 0x0200030D RID: 781
	public class JournalController : MonoBehaviour
	{
		// Token: 0x170004CF RID: 1231
		// (get) Token: 0x06001A6B RID: 6763 RVA: 0x00072C5B File Offset: 0x00070E5B
		public JournalController.JournalState CurrentJournalState
		{
			get
			{
				return this._currentJournalState;
			}
		}

		// Token: 0x06001A6C RID: 6764 RVA: 0x00072C63 File Offset: 0x00070E63
		private void Awake()
		{
			this.SetHidden();
			this._journalGamepadCloseable.Hide();
		}

		// Token: 0x06001A6D RID: 6765 RVA: 0x00072C78 File Offset: 0x00070E78
		public void RenderPages()
		{
			this._journalTabsController.ResetFirstEnabledPage();
			this._actionPageController.Show(true);
			this._reportPageController.Show(true);
			this._goalPageController.Show(true);
			this._reportPageController.RenderPages();
			this._actionPageController.RenderPages();
			this._goalPageController.RenderPages();
			for (int i = 0; i < this._pagesDisplayControllers.Length; i++)
			{
				this._pagesDisplayControllers[i].ResetPages();
				this._pagesDisplayControllers[i].GetAllPages();
				this._pagesDisplayControllers[i].InitializePages();
				this._pagesDisplayControllers[i].SetPagesInitialVisibility();
				this._pagesDisplayControllers[i].RefreshTabs();
			}
		}

		// Token: 0x06001A6E RID: 6766 RVA: 0x00072D29 File Offset: 0x00070F29
		public void Show()
		{
			this.SetJournalState(JournalController.JournalState.VISIBLE);
			this._hiddenJournalGamepadCloseable.Hide();
			this._journalGamepadCloseable.Show();
		}

		// Token: 0x06001A6F RID: 6767 RVA: 0x00072D48 File Offset: 0x00070F48
		public void Hide()
		{
			this.SetJournalState(JournalController.JournalState.HIDE);
			this._hiddenJournalGamepadCloseable.Hide();
			this._journalGamepadCloseable.Hide();
		}

		// Token: 0x06001A70 RID: 6768 RVA: 0x00072D67 File Offset: 0x00070F67
		public void PartiallyHide()
		{
			this.SetJournalState(JournalController.JournalState.PARTIALLY_HIDDEN);
			this._journalGamepadCloseable.Hide();
			this._hiddenJournalGamepadCloseable.Show();
		}

		// Token: 0x06001A71 RID: 6769 RVA: 0x00072D86 File Offset: 0x00070F86
		public void SetHidden()
		{
			this.SetJournalState(JournalController.JournalState.HIDDEN);
		}

		// Token: 0x06001A72 RID: 6770 RVA: 0x00072D90 File Offset: 0x00070F90
		private void SetJournalState(JournalController.JournalState state)
		{
			switch (state)
			{
			case JournalController.JournalState.HIDDEN:
				this._animator.SetTrigger("ResetJournal");
				this._animator.SetBool("Visible", false);
				this._animator.SetBool("PartiallyHidden", false);
				break;
			case JournalController.JournalState.PARTIALLY_HIDDEN:
				this._animator.SetBool("PartiallyHidden", true);
				break;
			case JournalController.JournalState.VISIBLE:
				this._animator.SetBool("PartiallyHidden", false);
				this._animator.SetBool("Visible", true);
				break;
			case JournalController.JournalState.HIDE:
				this._animator.SetBool("PartiallyHidden", false);
				this._animator.SetBool("Visible", false);
				break;
			default:
				throw new ArgumentOutOfRangeException("state", state, null);
			}
			this._currentJournalState = state;
		}

		// Token: 0x06001A73 RID: 6771 RVA: 0x00072E60 File Offset: 0x00071060
		public void SetJournal()
		{
			this._dayController.SetCurrentDayText();
		}

		// Token: 0x06001A74 RID: 6772 RVA: 0x00072E6D File Offset: 0x0007106D
		public void JournalOnDayStart()
		{
			this._journalButtonController.Show();
			HandHintController.ShouldShowHint = true;
		}

		// Token: 0x06001A75 RID: 6773 RVA: 0x00072E80 File Offset: 0x00071080
		public void TryToEndDay()
		{
			Singleton<VirtualInputManager>.Instance.IsGamepadInputBlocked = true;
			this.Hide();
			Singleton<GameManager>.Instance.RaycastCatcher.SetClickawayBlocked(true);
			SecondsEventManager.UnlockChoice();
			this._onActionDoodlesController.ResetVisualsData();
			EventSystem.current.SetSelectedGameObject(null);
			base.Invoke("EndDay", this._endDayDelay);
		}

		// Token: 0x06001A76 RID: 6774 RVA: 0x00072EDA File Offset: 0x000710DA
		private void EndDay()
		{
			this._endOfDayManager.EndOfDay();
		}

		// Token: 0x170004D0 RID: 1232
		// (get) Token: 0x06001A77 RID: 6775 RVA: 0x00072EE7 File Offset: 0x000710E7
		public JournalTabsController Tabs
		{
			get
			{
				return this._journalTabsController;
			}
		}

		// Token: 0x06001A78 RID: 6776 RVA: 0x00072EEF File Offset: 0x000710EF
		public bool CanSwitchPage()
		{
			return this._currentJournalState == JournalController.JournalState.VISIBLE;
		}

		// Token: 0x0400143A RID: 5178
		[SerializeField]
		private Animator _animator;

		// Token: 0x0400143B RID: 5179
		[SerializeField]
		private JournalDayController _dayController;

		// Token: 0x0400143C RID: 5180
		[SerializeField]
		private JournalButtonController _journalButtonController;

		// Token: 0x0400143D RID: 5181
		[SerializeField]
		private ReportPageController _reportPageController;

		// Token: 0x0400143E RID: 5182
		[SerializeField]
		private ActionPageController _actionPageController;

		// Token: 0x0400143F RID: 5183
		[SerializeField]
		private GoalPageController _goalPageController;

		// Token: 0x04001440 RID: 5184
		[SerializeField]
		private EndOfDayManager _endOfDayManager;

		// Token: 0x04001441 RID: 5185
		[SerializeField]
		private BasePagesDisplayController[] _pagesDisplayControllers;

		// Token: 0x04001442 RID: 5186
		[SerializeField]
		private GlobalBoolVariable _shouldDisplaySendExpeditionPage;

		// Token: 0x04001443 RID: 5187
		[SerializeField]
		private VisualsController _onActionDoodlesController;

		// Token: 0x04001444 RID: 5188
		[SerializeField]
		private VirtualInputClosablePanel _journalGamepadCloseable;

		// Token: 0x04001445 RID: 5189
		[SerializeField]
		private VirtualInputClosablePanel _hiddenJournalGamepadCloseable;

		// Token: 0x04001446 RID: 5190
		[SerializeField]
		private JournalTabsController _journalTabsController;

		// Token: 0x04001447 RID: 5191
		[SerializeField]
		private float _endDayDelay = 1f;

		// Token: 0x04001448 RID: 5192
		private JournalController.JournalState _currentJournalState;

		// Token: 0x04001449 RID: 5193
		private const string RESET_JOURNAL_TRIGGER_NAME = "ResetJournal";

		// Token: 0x0400144A RID: 5194
		private const string JOURNAL_VISIBLE_VAR_NAME = "Visible";

		// Token: 0x0400144B RID: 5195
		private const string JOURNAL_PARTIALLY_HIDDEN_VAR_NAME = "PartiallyHidden";

		// Token: 0x02000430 RID: 1072
		public enum JournalState
		{
			// Token: 0x0400190C RID: 6412
			HIDDEN,
			// Token: 0x0400190D RID: 6413
			PARTIALLY_HIDDEN,
			// Token: 0x0400190E RID: 6414
			VISIBLE,
			// Token: 0x0400190F RID: 6415
			HIDE
		}
	}
}

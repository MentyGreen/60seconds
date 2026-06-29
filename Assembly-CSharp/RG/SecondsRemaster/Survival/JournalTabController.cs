using System;
using RG.Parsecs.Common;
using RG.Parsecs.EventEditor;
using UnityEngine;
using UnityEngine.UI;

namespace RG.SecondsRemaster.Survival
{
	// Token: 0x02000332 RID: 818
	public class JournalTabController : MonoBehaviour
	{
		// Token: 0x06001B63 RID: 7011 RVA: 0x000760C9 File Offset: 0x000742C9
		public void ResetFirstEnabledPage()
		{
			this._firstPageEnabled = null;
		}

		// Token: 0x06001B64 RID: 7012 RVA: 0x000760D4 File Offset: 0x000742D4
		public void RefreshTab()
		{
			if (this._isTabEnabled != null && this._associatedPages.Length != 0)
			{
				this._button.interactable = (this._isTabEnabled.Value && this.IsAnyOfPagesEnabled());
			}
			else if (this._isTabEnabled != null)
			{
				this._button.interactable = this._isTabEnabled.Value;
			}
			else if (this._associatedPages.Length != 0)
			{
				this._button.interactable = this.IsAnyOfPagesEnabled();
			}
			else
			{
				this._button.interactable = false;
			}
			if (this._raycastBlocker != null)
			{
				this._raycastBlocker.SetActive(this._button.interactable);
			}
			this.SetExclamationMarksVisibility();
		}

		// Token: 0x06001B65 RID: 7013 RVA: 0x00076194 File Offset: 0x00074394
		public void SetExclamationMarksVisibility()
		{
			for (int i = 0; i < this._exclamationMarks.Length; i++)
			{
				this._exclamationMarks[i].SetActive(this._isExclamationMarkVisible.Value);
			}
		}

		// Token: 0x06001B66 RID: 7014 RVA: 0x000761CC File Offset: 0x000743CC
		public bool IsAnyOfPagesEnabled()
		{
			for (int i = 0; i < this._associatedPages.Length; i++)
			{
				if (!(this._associatedPages[i] == null) && this._associatedPages[i].IsEnabled())
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001B67 RID: 7015 RVA: 0x00076210 File Offset: 0x00074410
		public void ShowFirstEnabledPage()
		{
			for (int i = 0; i < this._associatedPages.Length; i++)
			{
				if (this._associatedPages[i] != null && this._associatedPages[i].IsEnabled())
				{
					PageController pageController;
					if (this._associatedPages[i].HasSubpages)
					{
						pageController = this._associatedPages[i].GetFirstSubpage();
					}
					else
					{
						pageController = this._associatedPages[i];
					}
					if (pageController != null)
					{
						this._pagesDisplayController.ShowSpecificPage(pageController);
						this._firstPageEnabled = pageController;
					}
				}
			}
		}

		// Token: 0x06001B68 RID: 7016 RVA: 0x00076298 File Offset: 0x00074498
		private void SetFirstEnabledPage()
		{
			if (this._firstPageEnabled != null)
			{
				return;
			}
			for (int i = 0; i < this._associatedPages.Length; i++)
			{
				if (this._associatedPages[i] != null && this._associatedPages[i].IsEnabled())
				{
					PageController pageController;
					if (this._associatedPages[i].HasSubpages)
					{
						pageController = this._associatedPages[i].GetFirstSubpage();
					}
					else
					{
						pageController = this._associatedPages[i];
					}
					if (pageController != null)
					{
						this._firstPageEnabled = pageController;
					}
				}
			}
		}

		// Token: 0x06001B69 RID: 7017 RVA: 0x00076320 File Offset: 0x00074520
		public void DisplayPage()
		{
			if (this._tabsController.IsThisTabCurrentTab(this) && this._pagesDisplayController.GetCurrentPage() == this._firstPageEnabled)
			{
				return;
			}
			this.ShowFirstEnabledPage();
			this._onUiClickedSoundPlayer.PlaySound();
		}

		// Token: 0x06001B6A RID: 7018 RVA: 0x0007635C File Offset: 0x0007455C
		public void ActivateTab(bool setFirstPage = true)
		{
			this._tabsController.SetTabActive(this);
			if (setFirstPage)
			{
				this.SetFirstEnabledPage();
			}
			JournalHideButtonController journalHideButtonController = Object.FindObjectOfType<JournalHideButtonController>();
			if (journalHideButtonController != null)
			{
				journalHideButtonController.Show();
			}
		}

		// Token: 0x06001B6B RID: 7019 RVA: 0x00076394 File Offset: 0x00074594
		public void SetActiveAnimationParameter(bool value)
		{
			this._animator.SetBool("Active", value);
		}

		// Token: 0x04001528 RID: 5416
		[SerializeField]
		private JournalTabsController _tabsController;

		// Token: 0x04001529 RID: 5417
		[SerializeField]
		private BasePagesDisplayController _pagesDisplayController;

		// Token: 0x0400152A RID: 5418
		[SerializeField]
		private PageController[] _associatedPages;

		// Token: 0x0400152B RID: 5419
		[SerializeField]
		private Button _button;

		// Token: 0x0400152C RID: 5420
		[SerializeField]
		private GlobalBoolVariable _isTabEnabled;

		// Token: 0x0400152D RID: 5421
		[SerializeField]
		private Animator _animator;

		// Token: 0x0400152E RID: 5422
		[SerializeField]
		private GlobalBoolVariable _isExclamationMarkVisible;

		// Token: 0x0400152F RID: 5423
		[SerializeField]
		private GameObject[] _exclamationMarks;

		// Token: 0x04001530 RID: 5424
		[SerializeField]
		private GameObject _raycastBlocker;

		// Token: 0x04001531 RID: 5425
		[SerializeField]
		private OnUIClickedSoundPlayer _onUiClickedSoundPlayer;

		// Token: 0x04001532 RID: 5426
		private PageController _firstPageEnabled;
	}
}

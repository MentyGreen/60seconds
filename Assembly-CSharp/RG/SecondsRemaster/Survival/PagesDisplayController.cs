using System;
using System.Collections.Generic;
using RG.Parsecs.Common;
using RG.Parsecs.Survival;
using UnityEngine;

namespace RG.SecondsRemaster.Survival
{
	// Token: 0x02000319 RID: 793
	public class PagesDisplayController : BasePagesDisplayController
	{
		// Token: 0x06001AE1 RID: 6881 RVA: 0x00074130 File Offset: 0x00072330
		public override void SetPagesInitialVisibility()
		{
			bool flag = false;
			PageController y = null;
			for (int i = 0; i < this._displayablePages.Count; i++)
			{
				if (!flag)
				{
					flag = true;
					this._currentPageIndex = i;
					this._currentPage = this._displayablePages[i];
					this.SetButtonsVisibility(this._currentPageIndex == this._displayablePages.Count - 1);
					this._displayablePages[i].Show();
					if (this._displayablePages[i].IsSubpage())
					{
						y = this._displayablePages[i].ParentPage;
					}
				}
				else if (!this._displayablePages[i].IsSubpage() || this._displayablePages[i].ParentPage != y)
				{
					this._displayablePages[i].Hide();
				}
			}
			BasePagesDisplayController.BlockSwitchingPages = false;
			this._visible = true;
		}

		// Token: 0x06001AE2 RID: 6882 RVA: 0x00074218 File Offset: 0x00072418
		public override void ShowNextPage()
		{
			base.ShowNextPage();
			if (!this._journalController.CanSwitchPage())
			{
				return;
			}
			if (BasePagesDisplayController.BlockSwitchingPages)
			{
				return;
			}
			BasePagesDisplayController.BlockSwitchingPages = true;
			this._currentPage.OnPageSwitched();
			PageController pageController = null;
			int num = this._currentPageIndex + 1;
			if (num < this._displayablePages.Count)
			{
				pageController = this._displayablePages[num];
				this._currentPageIndex = num;
			}
			if (pageController == null)
			{
				if (this._endGameData.RuntimeData.ShouldEndGame)
				{
					this._journalController.Hide();
					Singleton<GameManager>.Instance.RaycastCatcher.SetClickawayBlocked(true);
					EndGameManager.Instance.LoadEndGameScene();
				}
				else
				{
					this._journalController.TryToEndDay();
				}
				base.Invoke("UnblockSwitchingPages", this._timePageSwitchingIsBlockedWhenEndingDay);
				return;
			}
			base.ShowPage(pageController);
		}

		// Token: 0x06001AE3 RID: 6883 RVA: 0x000742E9 File Offset: 0x000724E9
		private void UnblockSwitchingPages()
		{
			BasePagesDisplayController.BlockSwitchingPages = false;
		}

		// Token: 0x06001AE4 RID: 6884 RVA: 0x000742F4 File Offset: 0x000724F4
		public override void ShowPreviousPage()
		{
			base.ShowPreviousPage();
			if (BasePagesDisplayController.BlockSwitchingPages)
			{
				return;
			}
			BasePagesDisplayController.BlockSwitchingPages = true;
			this._currentPage.OnPageSwitched();
			PageController pageController = null;
			int num = this._currentPageIndex - 1;
			if (num >= 0)
			{
				pageController = this._displayablePages[num];
				this._currentPageIndex = num;
			}
			if (pageController == null)
			{
				BasePagesDisplayController.BlockSwitchingPages = false;
				return;
			}
			base.ShowPage(pageController);
		}

		// Token: 0x06001AE5 RID: 6885 RVA: 0x00074360 File Offset: 0x00072560
		private void SetButtonsVisibility(bool endGameButtonVisible)
		{
			if (this._journalButtonPrevious != null)
			{
				this._journalButtonPrevious.SetActive(true);
			}
			if (this._journalButtonNext != null)
			{
				this._journalButtonNext.SetActive(!endGameButtonVisible);
			}
			if (this._journalButtonNextEndDay != null)
			{
				this._journalButtonNextEndDay.SetActive(endGameButtonVisible);
			}
		}

		// Token: 0x06001AE6 RID: 6886 RVA: 0x000743BE File Offset: 0x000725BE
		public override void OnPageChange()
		{
			this.SetButtonsVisibility(this._currentPageIndex == this._displayablePages.Count - 1);
		}

		// Token: 0x06001AE7 RID: 6887 RVA: 0x000743DC File Offset: 0x000725DC
		public void ResetPageButtonHelpers()
		{
			foreach (JournalPageButtonHelper journalPageButtonHelper in this._pageButtonHelpers)
			{
				journalPageButtonHelper.ResetButton();
			}
		}

		// Token: 0x040014A5 RID: 5285
		[SerializeField]
		private EndGameData _endGameData;

		// Token: 0x040014A6 RID: 5286
		[SerializeField]
		private float _timePageSwitchingIsBlockedWhenEndingDay = 2f;

		// Token: 0x040014A7 RID: 5287
		[SerializeField]
		private List<JournalPageButtonHelper> _pageButtonHelpers = new List<JournalPageButtonHelper>();

		// Token: 0x040014A8 RID: 5288
		private const string UNBLOCK_SWITCHING_PAGES_METHOD_NAME = "UnblockSwitchingPages";
	}
}

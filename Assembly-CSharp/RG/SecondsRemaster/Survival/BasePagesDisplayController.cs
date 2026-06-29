using System;
using System.Collections;
using System.Collections.Generic;
using FMODUnity;
using RG.Parsecs.Common;
using UnityEngine;

namespace RG.SecondsRemaster.Survival
{
	// Token: 0x02000317 RID: 791
	public abstract class BasePagesDisplayController : MonoBehaviour
	{
		// Token: 0x170004D2 RID: 1234
		// (get) Token: 0x06001AB7 RID: 6839 RVA: 0x00073C0D File Offset: 0x00071E0D
		public int CurrentPageIndex
		{
			get
			{
				return this._currentPageIndex;
			}
		}

		// Token: 0x170004D3 RID: 1235
		// (get) Token: 0x06001AB8 RID: 6840 RVA: 0x00073C15 File Offset: 0x00071E15
		public bool Visible
		{
			get
			{
				return this._visible;
			}
		}

		// Token: 0x06001AB9 RID: 6841 RVA: 0x00073C20 File Offset: 0x00071E20
		public void ResetPages()
		{
			for (int i = 0; i < this._rootPageList.Pages.Count; i++)
			{
				this._rootPageList.Pages[i].ResetPage();
			}
			for (int j = 0; j < this._staticPages.Length; j++)
			{
				this._staticPages[j].ResetPage();
			}
		}

		// Token: 0x06001ABA RID: 6842 RVA: 0x00073C7E File Offset: 0x00071E7E
		public PageController GetCurrentPage()
		{
			return this._currentPage;
		}

		// Token: 0x06001ABB RID: 6843 RVA: 0x00073C88 File Offset: 0x00071E88
		public void InitializePages()
		{
			for (int i = 0; i < this._rootPageList.Pages.Count; i++)
			{
				this._rootPageList.Pages[i].InitializePage();
			}
			for (int j = 0; j < this._staticPages.Length; j++)
			{
				this._staticPages[j].InitializePage();
			}
			if (this._doodlesOnAction != null)
			{
				this._doodlesOnAction.SetActive(false);
			}
		}

		// Token: 0x06001ABC RID: 6844 RVA: 0x00073D00 File Offset: 0x00071F00
		public void RefreshTabs()
		{
			this._tabsController.RefreshAllTabs();
		}

		// Token: 0x06001ABD RID: 6845 RVA: 0x00073D10 File Offset: 0x00071F10
		public void GetAllPages()
		{
			if (this._displayablePages == null)
			{
				this._displayablePages = new List<PageController>();
			}
			else
			{
				for (int i = 0; i < this._displayablePages.Count; i++)
				{
					this._displayablePages[i].Hide();
					this._displayablePages[i].SetEnabled(false);
				}
				this._displayablePages.Clear();
			}
			this.GetAllPagesRecursive(this._displayablePages, this._rootPageList);
		}

		// Token: 0x06001ABE RID: 6846 RVA: 0x00073D88 File Offset: 0x00071F88
		protected void GetAllPagesRecursive(List<PageController> pages, PagesListController pagesList)
		{
			for (int i = 0; i < pagesList.Pages.Count; i++)
			{
				if (!pagesList.Pages[i].CanBeDisplayed())
				{
					pagesList.Pages[i].SetEnabled(false);
				}
				else
				{
					PagesListController subpagesList = pagesList.Pages[i].GetSubpagesList();
					pagesList.Pages[i].SetEnabled(true);
					if (subpagesList != null)
					{
						this.GetAllPagesRecursive(pages, subpagesList);
					}
					else
					{
						this._displayablePages.Add(pagesList.Pages[i]);
					}
				}
			}
		}

		// Token: 0x06001ABF RID: 6847 RVA: 0x00073E24 File Offset: 0x00072024
		public void ShowSpecificPage(PageController page)
		{
			if (BasePagesDisplayController.BlockSwitchingPages)
			{
				return;
			}
			BasePagesDisplayController.BlockSwitchingPages = true;
			this.InvokePageSwitched();
			for (int i = 0; i < this._otherPagesDisplayControllers.Length; i++)
			{
				if (this._otherPagesDisplayControllers[i].Visible)
				{
					this._otherPagesDisplayControllers[i].InvokePageSwitched();
				}
			}
			if (this._displayablePages.Contains(page))
			{
				for (int j = 0; j < this._displayablePages.Count; j++)
				{
					if (this._displayablePages[j] == page)
					{
						this._currentPageIndex = j;
					}
				}
			}
			else
			{
				this._currentPageIndex = -1;
			}
			this.ShowPage(page);
		}

		// Token: 0x06001AC0 RID: 6848
		public abstract void SetPagesInitialVisibility();

		// Token: 0x06001AC1 RID: 6849 RVA: 0x00073EC3 File Offset: 0x000720C3
		public virtual void ShowNextPage()
		{
			if (this._currentPageIndex < 0)
			{
				Debug.LogError("You're trying to show next page when current page is not in _displayablePages array");
			}
		}

		// Token: 0x06001AC2 RID: 6850 RVA: 0x00073ED8 File Offset: 0x000720D8
		public virtual void ShowPreviousPage()
		{
			if (this._currentPageIndex < 0)
			{
				Debug.LogError("You're trying to show next page when current page is not in _displayablePages array");
			}
		}

		// Token: 0x06001AC3 RID: 6851 RVA: 0x00073EED File Offset: 0x000720ED
		public virtual void OnPageChange()
		{
		}

		// Token: 0x06001AC4 RID: 6852 RVA: 0x00073EEF File Offset: 0x000720EF
		public void ShowPage(PageController page)
		{
			base.StartCoroutine(this.ShowPageCoroutine(page));
			this.OnPageChange();
		}

		// Token: 0x06001AC5 RID: 6853 RVA: 0x00073F05 File Offset: 0x00072105
		private IEnumerator ShowPageCoroutine(PageController page)
		{
			BasePagesDisplayController.BlockSwitchingPages = true;
			if (this._visible)
			{
				yield return this._canvasGroupLerp.HideCanvasGroup();
			}
			else
			{
				int num;
				for (int i = 0; i < this._otherPagesDisplayControllers.Length; i = num + 1)
				{
					if (this._otherPagesDisplayControllers[i].Visible)
					{
						yield return this._otherPagesDisplayControllers[i].HidePages();
					}
					num = i;
				}
			}
			if (this._currentPage != null && this._currentPage != page)
			{
				this._currentPage.Hide();
			}
			this.OnPageChange();
			this._currentPage = page;
			if (this._actionPage != null && this._doodlesOnAction != null && this._doodlesOnAction.activeSelf && page.RootPage != this._actionPage)
			{
				this._doodlesOnAction.SetActive(false);
			}
			page.Show();
			AudioManager.PlaySound(this._changePageSound, 1f, 1f, 0f);
			yield return this._canvasGroupLerp.ShowCanvasGroup();
			BasePagesDisplayController.BlockSwitchingPages = false;
			this._visible = true;
			yield break;
		}

		// Token: 0x06001AC6 RID: 6854 RVA: 0x00073F1B File Offset: 0x0007211B
		public IEnumerator HidePages()
		{
			yield return this._canvasGroupLerp.HideCanvasGroup();
			if (this._currentPage != null)
			{
				this._currentPage.Hide();
			}
			if (this._journalButtonNext != null)
			{
				this._journalButtonNext.SetActive(false);
			}
			if (this._journalButtonPrevious != null)
			{
				this._journalButtonPrevious.SetActive(false);
			}
			if (this._journalButtonNextEndDay != null)
			{
				this._journalButtonNextEndDay.SetActive(false);
			}
			this._visible = false;
			yield break;
		}

		// Token: 0x06001AC7 RID: 6855 RVA: 0x00073F2A File Offset: 0x0007212A
		protected void InvokePageSwitched()
		{
			if (this._currentPage != null)
			{
				this._currentPage.OnPageSwitched();
			}
		}

		// Token: 0x0400148C RID: 5260
		[SerializeField]
		protected CanvasGroupLerp _canvasGroupLerp;

		// Token: 0x0400148D RID: 5261
		[SerializeField]
		protected PagesListController _rootPageList;

		// Token: 0x0400148E RID: 5262
		[SerializeField]
		protected PageController[] _staticPages;

		// Token: 0x0400148F RID: 5263
		[SerializeField]
		protected JournalController _journalController;

		// Token: 0x04001490 RID: 5264
		[SerializeField]
		protected JournalTabsController _tabsController;

		// Token: 0x04001491 RID: 5265
		[EventRef]
		[SerializeField]
		protected string _changePageSound;

		// Token: 0x04001492 RID: 5266
		[SerializeField]
		protected GameObject _journalButtonNext;

		// Token: 0x04001493 RID: 5267
		[SerializeField]
		protected GameObject _journalButtonPrevious;

		// Token: 0x04001494 RID: 5268
		[SerializeField]
		protected GameObject _journalButtonNextEndDay;

		// Token: 0x04001495 RID: 5269
		[SerializeField]
		private BasePagesDisplayController[] _otherPagesDisplayControllers;

		// Token: 0x04001496 RID: 5270
		[SerializeField]
		private GameObject _doodlesOnAction;

		// Token: 0x04001497 RID: 5271
		[SerializeField]
		private PageController _actionPage;

		// Token: 0x04001498 RID: 5272
		protected static bool BlockSwitchingPages;

		// Token: 0x04001499 RID: 5273
		protected int _currentPageIndex;

		// Token: 0x0400149A RID: 5274
		protected PageController _currentPage;

		// Token: 0x0400149B RID: 5275
		protected List<PageController> _displayablePages;

		// Token: 0x0400149C RID: 5276
		protected bool _visible;

		// Token: 0x0400149D RID: 5277
		private const int PAGE_NOT_AVAILABLE_IN_ARRAY = -1;
	}
}

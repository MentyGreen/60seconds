using System;

namespace RG.SecondsRemaster.Survival
{
	// Token: 0x02000329 RID: 809
	public class GoalsPageDisplayController : BasePagesDisplayController
	{
		// Token: 0x06001B30 RID: 6960 RVA: 0x0007540C File Offset: 0x0007360C
		public override void SetPagesInitialVisibility()
		{
			bool flag = false;
			for (int i = 0; i < this._displayablePages.Count; i++)
			{
				if (!flag)
				{
					flag = true;
					this._currentPageIndex = i;
					this._currentPage = this._displayablePages[i];
					this._journalButtonNext.SetActive(false);
					this._journalButtonPrevious.SetActive(false);
				}
				this._displayablePages[i].Hide();
			}
			BasePagesDisplayController.BlockSwitchingPages = false;
			this._visible = false;
		}

		// Token: 0x06001B31 RID: 6961 RVA: 0x00075488 File Offset: 0x00073688
		public override void ShowNextPage()
		{
			base.ShowNextPage();
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
				BasePagesDisplayController.BlockSwitchingPages = false;
				return;
			}
			base.ShowPage(pageController);
		}

		// Token: 0x06001B32 RID: 6962 RVA: 0x000754FC File Offset: 0x000736FC
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

		// Token: 0x06001B33 RID: 6963 RVA: 0x00075566 File Offset: 0x00073766
		public override void OnPageChange()
		{
			this.UpdateArrowsVisibility();
		}

		// Token: 0x06001B34 RID: 6964 RVA: 0x0007556E File Offset: 0x0007376E
		private void UpdateArrowsVisibility()
		{
			this._journalButtonNext.SetActive(this._currentPageIndex != this._displayablePages.Count - 1);
			this._journalButtonPrevious.SetActive(this._currentPageIndex > 0);
		}
	}
}

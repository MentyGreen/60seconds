using System;
using RG.Parsecs.Survival;
using UnityEngine;

namespace RG.SecondsRemaster.Survival
{
	// Token: 0x02000318 RID: 792
	public class PageController : MonoBehaviour
	{
		// Token: 0x06001AC9 RID: 6857 RVA: 0x00073F4D File Offset: 0x0007214D
		protected void SetPageNotRefreshableToday()
		{
			this._lastDayWhenDataSet = EndOfDayManager.Instance.AcutalDay;
		}

		// Token: 0x06001ACA RID: 6858 RVA: 0x00073F5F File Offset: 0x0007215F
		protected bool CanRefreshPageToday()
		{
			return this._lastDayWhenDataSet != EndOfDayManager.Instance.AcutalDay;
		}

		// Token: 0x06001ACB RID: 6859 RVA: 0x00073F76 File Offset: 0x00072176
		public virtual void SetPageData(bool visible)
		{
		}

		// Token: 0x06001ACC RID: 6860 RVA: 0x00073F78 File Offset: 0x00072178
		public virtual bool CanBeDisplayed()
		{
			return !this._endGameData.RuntimeData.ShouldEndGame;
		}

		// Token: 0x06001ACD RID: 6861 RVA: 0x00073F8D File Offset: 0x0007218D
		public virtual void InitializePage()
		{
			if (this._initialized)
			{
				return;
			}
			if (this.ParentPage != null)
			{
				this.ParentPage.InitializePage();
			}
		}

		// Token: 0x06001ACE RID: 6862 RVA: 0x00073FB1 File Offset: 0x000721B1
		public void SetEnabled(bool value)
		{
			this._enabled = value;
		}

		// Token: 0x06001ACF RID: 6863 RVA: 0x00073FBA File Offset: 0x000721BA
		public bool IsEnabled()
		{
			return this._enabled;
		}

		// Token: 0x06001AD0 RID: 6864 RVA: 0x00073FC2 File Offset: 0x000721C2
		public virtual bool IsSubpage()
		{
			return false;
		}

		// Token: 0x170004D4 RID: 1236
		// (get) Token: 0x06001AD1 RID: 6865 RVA: 0x00073FC5 File Offset: 0x000721C5
		// (set) Token: 0x06001AD2 RID: 6866 RVA: 0x00073FC8 File Offset: 0x000721C8
		public virtual PageController ParentPage
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		// Token: 0x170004D5 RID: 1237
		// (get) Token: 0x06001AD3 RID: 6867 RVA: 0x00073FCA File Offset: 0x000721CA
		public PageController RootPage
		{
			get
			{
				if (this.ParentPage == null)
				{
					return this;
				}
				return this.ParentPage.RootPage;
			}
		}

		// Token: 0x170004D6 RID: 1238
		// (get) Token: 0x06001AD4 RID: 6868 RVA: 0x00073FE7 File Offset: 0x000721E7
		public bool HasSubpages
		{
			get
			{
				return this._hasSubpages;
			}
		}

		// Token: 0x170004D7 RID: 1239
		// (get) Token: 0x06001AD5 RID: 6869 RVA: 0x00073FEF File Offset: 0x000721EF
		// (set) Token: 0x06001AD6 RID: 6870 RVA: 0x00073FF7 File Offset: 0x000721F7
		public bool Initialized
		{
			get
			{
				return this._initialized;
			}
			set
			{
				this._initialized = value;
			}
		}

		// Token: 0x06001AD7 RID: 6871 RVA: 0x00074000 File Offset: 0x00072200
		public virtual void Show()
		{
			if (this._associatedTab != null)
			{
				this._associatedTab.ActivateTab(true);
			}
			this.SetPageData(true);
			base.gameObject.SetActive(true);
		}

		// Token: 0x06001AD8 RID: 6872 RVA: 0x0007402F File Offset: 0x0007222F
		public virtual void Show(bool doNotSetPageData)
		{
			if (this._associatedTab != null)
			{
				this._associatedTab.ActivateTab(!doNotSetPageData);
			}
			if (!doNotSetPageData)
			{
				this.SetPageData(true);
			}
			base.gameObject.SetActive(true);
		}

		// Token: 0x06001AD9 RID: 6873 RVA: 0x00074064 File Offset: 0x00072264
		public PageController GetFirstSubpage()
		{
			if (!this._hasSubpages)
			{
				return null;
			}
			return this._subPagesList.Pages[0];
		}

		// Token: 0x06001ADA RID: 6874 RVA: 0x00074081 File Offset: 0x00072281
		public virtual void Hide()
		{
			this.SetPageData(false);
			base.gameObject.SetActive(false);
		}

		// Token: 0x06001ADB RID: 6875 RVA: 0x00074096 File Offset: 0x00072296
		public virtual void OnPageSwitched()
		{
			if (this._associatedTab != null)
			{
				this._associatedTab.SetExclamationMarksVisibility();
			}
			if (this.ParentPage != null)
			{
				this.ParentPage.OnPageSwitched();
			}
		}

		// Token: 0x06001ADC RID: 6876 RVA: 0x000740CA File Offset: 0x000722CA
		public PagesListController GetSubpagesList()
		{
			if (!this._hasSubpages)
			{
				return null;
			}
			return this._subPagesList;
		}

		// Token: 0x06001ADD RID: 6877 RVA: 0x000740DC File Offset: 0x000722DC
		public void AddNewSubPage(PageController subPage)
		{
			this._subPagesList.Pages.Add(subPage);
			if (subPage is SubPageController)
			{
				((SubPageController)subPage).ParentPage = this;
			}
		}

		// Token: 0x06001ADE RID: 6878 RVA: 0x00074103 File Offset: 0x00072303
		public void ClearSubpages()
		{
			if (this._subPagesList != null)
			{
				this._subPagesList.ClearPages();
			}
		}

		// Token: 0x06001ADF RID: 6879 RVA: 0x0007411E File Offset: 0x0007231E
		public void ResetPage()
		{
			this._initialized = false;
		}

		// Token: 0x0400149E RID: 5278
		[SerializeField]
		private bool _hasSubpages;

		// Token: 0x0400149F RID: 5279
		[SerializeField]
		private PagesListController _subPagesList;

		// Token: 0x040014A0 RID: 5280
		[SerializeField]
		protected EndGameData _endGameData;

		// Token: 0x040014A1 RID: 5281
		[SerializeField]
		private JournalTabController _associatedTab;

		// Token: 0x040014A2 RID: 5282
		[SerializeField]
		private bool _enabled;

		// Token: 0x040014A3 RID: 5283
		private bool _initialized;

		// Token: 0x040014A4 RID: 5284
		private int _lastDayWhenDataSet;
	}
}

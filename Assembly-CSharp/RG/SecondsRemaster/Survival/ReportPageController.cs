using System;
using System.Collections.Generic;
using RG.Parsecs.EventEditor;
using RG.Parsecs.Survival;
using UnityEngine;
using UnityEngine.UI;

namespace RG.SecondsRemaster.Survival
{
	// Token: 0x02000330 RID: 816
	public class ReportPageController : PageController
	{
		// Token: 0x06001B52 RID: 6994 RVA: 0x00075D84 File Offset: 0x00073F84
		private void SetRandomReportDoodles()
		{
			PagesListController subpagesList = base.GetSubpagesList();
			if (subpagesList == null || subpagesList.Pages == null)
			{
				return;
			}
			this._subPages = subpagesList.Pages;
			if (this._subPages.Count > 1 && this._eventsRendererController.CanShowDoodleOnLastPage(0.5f) && Random.Range(0, 100) < this._chanceToShowDoodle && this.ShowRandomDoodle(this._bigDoodlesData, this._bigDoodlesIds))
			{
				this.AssignDoodlesToLastPage(this._bigDoodlesVisualsController.gameObject);
			}
			if (Random.Range(0, 100) < this._chanceToShowDoodle && this.ShowRandomDoodle(this._topDoodlesData, this._topDoodlesIds))
			{
				this.AssignDoodlesToRandomPage(this._topDoodlesVisualsController.gameObject);
			}
			if (this._bigDoodlesVisualsController != null)
			{
				this._bigDoodlesVisualsController.RefreshVisualsState();
			}
			if (this._topDoodlesVisualsController != null)
			{
				this._topDoodlesVisualsController.RefreshVisualsState();
			}
		}

		// Token: 0x06001B53 RID: 6995 RVA: 0x00075E74 File Offset: 0x00074074
		private bool ShowRandomDoodle(VisualsData doodlesVisualsData, VisualId[] visualIds)
		{
			if (doodlesVisualsData == null || visualIds == null || visualIds.Length == 0)
			{
				return false;
			}
			int num = Random.Range(0, visualIds.Length);
			doodlesVisualsData.RuntimeData.VisualToDisplay = visualIds[num];
			return true;
		}

		// Token: 0x06001B54 RID: 6996 RVA: 0x00075EAC File Offset: 0x000740AC
		private void AssignDoodlesToRandomPage(GameObject doodlesHolder)
		{
			if (this._subPages.Count == 1)
			{
				this.AssignDoodlesToPage(this._subPages[0] as ReportSubPageController, doodlesHolder);
				return;
			}
			int index = Random.Range(0, this._subPages.Count - 1);
			this.AssignDoodlesToPage(this._subPages[index] as ReportSubPageController, doodlesHolder);
		}

		// Token: 0x06001B55 RID: 6997 RVA: 0x00075F0C File Offset: 0x0007410C
		private void AssignDoodlesToLastPage(GameObject doodlesHolder)
		{
			this.AssignDoodlesToPage(this._subPages[this._subPages.Count - 1] as ReportSubPageController, doodlesHolder);
		}

		// Token: 0x06001B56 RID: 6998 RVA: 0x00075F32 File Offset: 0x00074132
		private void AssignDoodlesToPage(ReportSubPageController subPageController, GameObject doodlesHolder)
		{
			if (subPageController == null)
			{
				return;
			}
			subPageController.SetDoodlesHolder(doodlesHolder);
		}

		// Token: 0x06001B57 RID: 6999 RVA: 0x00075F45 File Offset: 0x00074145
		public void RenderPages()
		{
			if (this._endGameData.RuntimeData.ShouldEndGame)
			{
				this._displayHistoryTextFunction.Execute(null);
			}
			this._eventsRendererController.RenderContents();
			this.SetRandomReportDoodles();
			this.DisableDoodlesHolders();
		}

		// Token: 0x06001B58 RID: 7000 RVA: 0x00075F7C File Offset: 0x0007417C
		private void DisableDoodlesHolders()
		{
			if (this._bigDoodlesVisualsController != null)
			{
				this._bigDoodlesVisualsController.gameObject.SetActive(false);
			}
			if (this._topDoodlesVisualsController != null)
			{
				this._topDoodlesVisualsController.gameObject.SetActive(false);
			}
		}

		// Token: 0x06001B59 RID: 7001 RVA: 0x00075FBC File Offset: 0x000741BC
		public override bool CanBeDisplayed()
		{
			return true;
		}

		// Token: 0x06001B5A RID: 7002 RVA: 0x00075FC0 File Offset: 0x000741C0
		public override void SetPageData(bool visible)
		{
			this._previousPageButton.interactable = (this._pagesDisplayController.CurrentPageIndex != 0);
			if (visible && this._attentionVariable != null && this._attentionVariable.Value)
			{
				this._attentionVariable.Value = false;
			}
		}

		// Token: 0x06001B5B RID: 7003 RVA: 0x00076010 File Offset: 0x00074210
		public override void InitializePage()
		{
			base.InitializePage();
			if (base.IsEnabled())
			{
				this._attentionVariable.Value = true;
			}
		}

		// Token: 0x0400151A RID: 5402
		[SerializeField]
		private EventsRendererController _eventsRendererController;

		// Token: 0x0400151B RID: 5403
		[SerializeField]
		private VisualsData _bigDoodlesData;

		// Token: 0x0400151C RID: 5404
		[SerializeField]
		private VisualId[] _bigDoodlesIds;

		// Token: 0x0400151D RID: 5405
		[SerializeField]
		private VisualsController _bigDoodlesVisualsController;

		// Token: 0x0400151E RID: 5406
		[SerializeField]
		private VisualsData _topDoodlesData;

		// Token: 0x0400151F RID: 5407
		[SerializeField]
		private VisualId[] _topDoodlesIds;

		// Token: 0x04001520 RID: 5408
		[SerializeField]
		private VisualsController _topDoodlesVisualsController;

		// Token: 0x04001521 RID: 5409
		[SerializeField]
		private int _chanceToShowDoodle = 20;

		// Token: 0x04001522 RID: 5410
		[SerializeField]
		private NodeFunction _displayHistoryTextFunction;

		// Token: 0x04001523 RID: 5411
		[SerializeField]
		private Button _previousPageButton;

		// Token: 0x04001524 RID: 5412
		[SerializeField]
		private GlobalBoolVariable _attentionVariable;

		// Token: 0x04001525 RID: 5413
		[SerializeField]
		private PagesDisplayController _pagesDisplayController;

		// Token: 0x04001526 RID: 5414
		private List<PageController> _subPages;
	}
}

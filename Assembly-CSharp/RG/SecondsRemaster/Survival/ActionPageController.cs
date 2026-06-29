using System;
using System.Collections;
using System.Collections.Generic;
using RG.Parsecs.EventEditor;
using RG.Parsecs.Survival;
using UnityEngine;
using UnityEngine.UI;

namespace RG.SecondsRemaster.Survival
{
	// Token: 0x02000314 RID: 788
	public class ActionPageController : PageController
	{
		// Token: 0x06001AA5 RID: 6821 RVA: 0x00073871 File Offset: 0x00071A71
		private void Awake()
		{
			this._survivalData.DailyEventResolved = true;
		}

		// Token: 0x06001AA6 RID: 6822 RVA: 0x0007387F File Offset: 0x00071A7F
		private void OnEnable()
		{
			base.StartCoroutine(this.WaitFrameAndRefreshMappedTarget());
		}

		// Token: 0x06001AA7 RID: 6823 RVA: 0x0007388E File Offset: 0x00071A8E
		public void RenderPages()
		{
			this._eventsRendererController.RenderContents();
			this.AssignDoodlesToLastPage();
			this._doodlesVisualsController.RefreshVisualsState();
		}

		// Token: 0x06001AA8 RID: 6824 RVA: 0x000738AC File Offset: 0x00071AAC
		private void AssignDoodlesToLastPage()
		{
			List<PageController> pages = base.GetSubpagesList().Pages;
			ActionSubPageController actionSubPageController = pages[pages.Count - 1] as ActionSubPageController;
			VisualId visualToDisplay = this._onActionDoodlesVisualData.RuntimeData.VisualToDisplay;
			if (visualToDisplay != null)
			{
				float pageHeight = this._doodlePageHeightsDefinitions.GetPageHeight(visualToDisplay);
				if (actionSubPageController != null && this._eventsRendererController.CanShowDoodleOnLastPage(pageHeight))
				{
					actionSubPageController.SetDoodlesHolder(this._doodlesVisualsController.gameObject);
				}
			}
		}

		// Token: 0x06001AA9 RID: 6825 RVA: 0x00073928 File Offset: 0x00071B28
		public override void SetPageData(bool visible)
		{
			this._journalButtonNext.interactable = this._survivalData.DailyEventResolved;
			if (visible && this._attentionVariable != null && this._attentionVariable.Value)
			{
				this._attentionVariable.Value = !this._survivalData.DailyEventResolved;
			}
		}

		// Token: 0x06001AAA RID: 6826 RVA: 0x00073982 File Offset: 0x00071B82
		public override bool CanBeDisplayed()
		{
			return base.CanBeDisplayed() && !this._shouldDisplaySendExpeditionPage.Value;
		}

		// Token: 0x06001AAB RID: 6827 RVA: 0x0007399C File Offset: 0x00071B9C
		public override void InitializePage()
		{
			base.InitializePage();
			if (base.IsEnabled())
			{
				this._attentionVariable.Value = true;
			}
			this.InitializeDynamicNavigation();
		}

		// Token: 0x06001AAC RID: 6828 RVA: 0x000739C0 File Offset: 0x00071BC0
		private void InitializeDynamicNavigation()
		{
			NavigationMappingTemplate component = base.GetComponent<NavigationMappingTemplate>();
			if (component != null)
			{
				component.CurrentlyMappedTarget = null;
				List<PageController> pages = base.GetSubpagesList().Pages;
				if (pages.Count > 0)
				{
					for (int i = 0; i < pages.Count; i++)
					{
						INavigationMappable[] componentsInChildren = pages[i].gameObject.GetComponentsInChildren<INavigationMappable>();
						if (componentsInChildren != null)
						{
							for (int j = 0; j < componentsInChildren.Length; j++)
							{
								Selectable selectable = componentsInChildren[j] as Selectable;
								if (component.CurrentlyMappedTarget == null && selectable.interactable)
								{
									component.CurrentlyMappedTarget = selectable;
								}
								NavigationMappingTemplate.NavigationMappingEntry navigationMappingEntry = component.GetNavigationMappingEntry(componentsInChildren[j].MappingTag);
								if (navigationMappingEntry != null)
								{
									componentsInChildren[j].AddNavigationMapping(navigationMappingEntry.Down, navigationMappingEntry.Left, navigationMappingEntry.Up, navigationMappingEntry.Right);
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x06001AAD RID: 6829 RVA: 0x00073AA1 File Offset: 0x00071CA1
		private IEnumerator WaitFrameAndRefreshMappedTarget()
		{
			yield return new WaitForEndOfFrame();
			this.RefreshNavigationMappingCurrentlyMappedTarget();
			yield break;
		}

		// Token: 0x06001AAE RID: 6830 RVA: 0x00073AB0 File Offset: 0x00071CB0
		private void RefreshNavigationMappingCurrentlyMappedTarget()
		{
			NavigationMappingTemplate component = base.GetComponent<NavigationMappingTemplate>();
			if (component != null)
			{
				component.CurrentlyMappedTarget = null;
				List<PageController> pages = base.GetSubpagesList().Pages;
				if (pages.Count > 0)
				{
					for (int i = 0; i < pages.Count; i++)
					{
						INavigationMappable[] componentsInChildren = pages[i].gameObject.GetComponentsInChildren<INavigationMappable>();
						if (componentsInChildren != null)
						{
							for (int j = 0; j < componentsInChildren.Length; j++)
							{
								Selectable selectable = componentsInChildren[j] as Selectable;
								if (component.CurrentlyMappedTarget == null && selectable.interactable)
								{
									component.CurrentlyMappedTarget = selectable;
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x0400147F RID: 5247
		[SerializeField]
		private EventsRendererController _eventsRendererController;

		// Token: 0x04001480 RID: 5248
		[SerializeField]
		private VisualsController _doodlesVisualsController;

		// Token: 0x04001481 RID: 5249
		[SerializeField]
		private Selectable _journalButtonNext;

		// Token: 0x04001482 RID: 5250
		[SerializeField]
		private SurvivalData _survivalData;

		// Token: 0x04001483 RID: 5251
		[SerializeField]
		private GlobalBoolVariable _shouldDisplaySendExpeditionPage;

		// Token: 0x04001484 RID: 5252
		[SerializeField]
		private GlobalBoolVariable _attentionVariable;

		// Token: 0x04001485 RID: 5253
		[SerializeField]
		private VisualsData _onActionDoodlesVisualData;

		// Token: 0x04001486 RID: 5254
		[SerializeField]
		private DoodlePageHeightsDefinitions _doodlePageHeightsDefinitions;
	}
}

using System;
using RG.Parsecs.EventEditor;
using UnityEngine;
using UnityEngine.UI;

namespace RG.SecondsRemaster.Survival
{
	// Token: 0x0200032B RID: 811
	public class MetagamePageController : PageController
	{
		// Token: 0x06001B38 RID: 6968 RVA: 0x000755BC File Offset: 0x000737BC
		public override void SetPageData(bool visible)
		{
			if (visible && this._attentionVariable != null && this._attentionVariable.Value)
			{
				this._attentionVariable.Value = false;
			}
			if (visible)
			{
				this._endDayButtonStateWhenHidden = this._nextPageButtonEndDay.interactable;
				this._nextButtonStateWhenHidden = this._nextPageButton.interactable;
				this._previousButtonStateWhenHidden = this._previousPageButton.interactable;
				this._nextPageButton.interactable = false;
				this._nextPageButtonEndDay.interactable = false;
				this._previousPageButton.interactable = false;
				return;
			}
			this._nextPageButton.interactable = this._nextButtonStateWhenHidden;
			this._nextPageButtonEndDay.interactable = this._endDayButtonStateWhenHidden;
			this._previousPageButton.interactable = this._previousButtonStateWhenHidden;
		}

		// Token: 0x06001B39 RID: 6969 RVA: 0x00075681 File Offset: 0x00073881
		public override bool CanBeDisplayed()
		{
			return true;
		}

		// Token: 0x040014F4 RID: 5364
		[SerializeField]
		private Button _nextPageButton;

		// Token: 0x040014F5 RID: 5365
		[SerializeField]
		private Button _previousPageButton;

		// Token: 0x040014F6 RID: 5366
		[SerializeField]
		private Button _nextPageButtonEndDay;

		// Token: 0x040014F7 RID: 5367
		[SerializeField]
		private GlobalBoolVariable _attentionVariable;

		// Token: 0x040014F8 RID: 5368
		private bool _endDayButtonStateWhenHidden;

		// Token: 0x040014F9 RID: 5369
		private bool _nextButtonStateWhenHidden;

		// Token: 0x040014FA RID: 5370
		private bool _previousButtonStateWhenHidden;
	}
}

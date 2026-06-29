using System;
using RG.Parsecs.EventEditor;
using RG.Parsecs.Survival;
using UnityEngine;

namespace RG.SecondsRemaster.Survival
{
	// Token: 0x02000322 RID: 802
	public class PlanExpeditionPageController : PageController
	{
		// Token: 0x06001B06 RID: 6918 RVA: 0x000748A0 File Offset: 0x00072AA0
		public override void SetPageData(bool visible)
		{
			if (visible && this._attentionVariable != null && this._attentionVariable.Value)
			{
				this._attentionVariable.Value = false;
			}
			if (!base.CanRefreshPageToday())
			{
				return;
			}
			base.SetPageNotRefreshableToday();
			for (int i = 0; i < this._functionTextControllers.Length; i++)
			{
				this._functionTextControllers[i].RefreshText();
			}
			for (int j = 0; j < this._characterAvailabilityControllers.Length; j++)
			{
				this._characterAvailabilityControllers[j].RefreshCharacterAvailability();
			}
			this._planExpeditionToggleController.SetToggleWithoutInvokingValueChange(false);
		}

		// Token: 0x06001B07 RID: 6919 RVA: 0x00074934 File Offset: 0x00072B34
		public override bool CanBeDisplayed()
		{
			return base.CanBeDisplayed() && EndOfDayManager.Instance.AcutalDay > 1 && this._expeditionData.RuntimeData.IsActive && !this._expeditionData.RuntimeData.IsOngoing && !this.IsCurrentEventInForcedExpeditionList() && !this._shouldDisplaySendExpeditionPageVariable.Value && !this._isTutorialVariable.Value && this.CanBeDisplayedInDemo();
		}

		// Token: 0x06001B08 RID: 6920 RVA: 0x000749A4 File Offset: 0x00072BA4
		private bool CanBeDisplayedInDemo()
		{
			return !DemoManager.IS_DEMO_VERSION || EndOfDayManager.Instance.AcutalDay < 8;
		}

		// Token: 0x06001B09 RID: 6921 RVA: 0x000749BC File Offset: 0x00072BBC
		private bool IsCurrentEventInForcedExpeditionList()
		{
			return this._canvases.Contains(this._scheduler.CurrentScheduledDay.SurvivalEvent);
		}

		// Token: 0x06001B0A RID: 6922 RVA: 0x000749D9 File Offset: 0x00072BD9
		public override void InitializePage()
		{
			base.InitializePage();
			if (base.IsEnabled())
			{
				this._attentionVariable.Value = true;
			}
		}

		// Token: 0x040014BE RID: 5310
		[SerializeField]
		private ExpeditionData _expeditionData;

		// Token: 0x040014BF RID: 5311
		[SerializeField]
		private GlobalBoolVariable _shouldDisplaySendExpeditionPageVariable;

		// Token: 0x040014C0 RID: 5312
		[SerializeField]
		private GlobalBoolVariable _isTutorialVariable;

		// Token: 0x040014C1 RID: 5313
		[SerializeField]
		private CanvasesList _canvases;

		// Token: 0x040014C2 RID: 5314
		[SerializeField]
		private Scheduler _scheduler;

		// Token: 0x040014C3 RID: 5315
		[SerializeField]
		private FunctionTextController[] _functionTextControllers;

		// Token: 0x040014C4 RID: 5316
		[SerializeField]
		private CharacterAvailabilityController[] _characterAvailabilityControllers;

		// Token: 0x040014C5 RID: 5317
		[SerializeField]
		private PlanExpeditionToggleController _planExpeditionToggleController;

		// Token: 0x040014C6 RID: 5318
		[SerializeField]
		private GlobalBoolVariable _attentionVariable;

		// Token: 0x040014C7 RID: 5319
		private bool _isVisible;
	}
}

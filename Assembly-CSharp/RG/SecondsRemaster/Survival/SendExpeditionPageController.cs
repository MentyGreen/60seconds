using System;
using RG.Parsecs.EventEditor;
using RG.Parsecs.Survival;
using UnityEngine;

namespace RG.SecondsRemaster.Survival
{
	// Token: 0x02000327 RID: 807
	public class SendExpeditionPageController : PageController
	{
		// Token: 0x06001B24 RID: 6948 RVA: 0x00075024 File Offset: 0x00073224
		public override void SetPageData(bool visible)
		{
			if (visible && this._attentionVariable != null && this._attentionVariable.Value)
			{
				this._attentionVariable.Value = false;
			}
			if (this._isTutorialExpedition.Value)
			{
				this._handsContainer.SetActive(false);
			}
			else
			{
				this._handsContainer.SetActive(true);
			}
			if (!base.CanRefreshPageToday())
			{
				return;
			}
			base.SetPageNotRefreshableToday();
			this._shouldDisplaySendExpeditionPageVariable.SetValue(false);
			for (int i = 0; i < this._characterAvailabilityControllers.Length; i++)
			{
				this._characterAvailabilityControllers[i].RefreshCharacterAvailability();
			}
			for (int j = 0; j < this._sendExpeditionCharacterToggles.Length; j++)
			{
				this._sendExpeditionCharacterToggles[j].SetToggleWithoutInvokingValueChange(false);
			}
			this._remasterItemsSlotsController.SetHandButtonsDefaultState();
			this._reassuringDescription.RefreshText();
		}

		// Token: 0x06001B25 RID: 6949 RVA: 0x000750F4 File Offset: 0x000732F4
		public void SetExpedtionCharacter(Character character)
		{
			this._expeditionData.RuntimeData.PlannedExpeditionData.ExpeditionCharacter = character;
		}

		// Token: 0x06001B26 RID: 6950 RVA: 0x0007510C File Offset: 0x0007330C
		public void SetExpeditionItems(IItem[] items)
		{
			this._expeditionData.RuntimeData.PlannedExpeditionData.ExpeditionItems.Clear();
			for (int i = 0; i < items.Length; i++)
			{
				if (items[i] != null)
				{
					this._expeditionData.RuntimeData.PlannedExpeditionData.ExpeditionItems.Add(items[i]);
				}
			}
		}

		// Token: 0x06001B27 RID: 6951 RVA: 0x0007516C File Offset: 0x0007336C
		public override void OnPageSwitched()
		{
			base.OnPageSwitched();
			this._expeditionData.RuntimeData.IsPlanned = (this._expeditionData.RuntimeData.PlannedExpeditionData.ExpeditionCharacter != null);
			if (DemoManager.IS_DEMO_VERSION && this._survivalData.CurrentDay < 11)
			{
				this._expeditionData.RuntimeData.PlannedExpeditionData.ChosenDestination = (this._expeditionData.RuntimeData.IsPlanned ? this._demoExpeditionDestinationList.GetRandomDestination(this._expeditionData.RuntimeData.PlannedExpeditionData.ExpeditionCharacter, this._isTutorialVariable.Value) : null);
				return;
			}
			this._expeditionData.RuntimeData.PlannedExpeditionData.ChosenDestination = (this._expeditionData.RuntimeData.IsPlanned ? this._expeditionDestinationList.GetRandomDestination(this._expeditionData.RuntimeData.PlannedExpeditionData.ExpeditionCharacter, this._isTutorialVariable.Value) : null);
		}

		// Token: 0x06001B28 RID: 6952 RVA: 0x0007526B File Offset: 0x0007346B
		public override bool CanBeDisplayed()
		{
			return base.CanBeDisplayed() && this._shouldDisplaySendExpeditionPageVariable.Value && this._expeditionData.RuntimeData.IsActive && !this._expeditionData.RuntimeData.IsOngoing;
		}

		// Token: 0x06001B29 RID: 6953 RVA: 0x000752A9 File Offset: 0x000734A9
		public override void InitializePage()
		{
			base.InitializePage();
			if (base.IsEnabled())
			{
				this._attentionVariable.Value = true;
			}
		}

		// Token: 0x040014E4 RID: 5348
		[SerializeField]
		private GlobalBoolVariable _shouldDisplaySendExpeditionPageVariable;

		// Token: 0x040014E5 RID: 5349
		[SerializeField]
		private ExpeditionData _expeditionData;

		// Token: 0x040014E6 RID: 5350
		[SerializeField]
		private SurvivalData _survivalData;

		// Token: 0x040014E7 RID: 5351
		[SerializeField]
		private CharacterAvailabilityController[] _characterAvailabilityControllers;

		// Token: 0x040014E8 RID: 5352
		[SerializeField]
		private SendExpeditionCharacterToggle[] _sendExpeditionCharacterToggles;

		// Token: 0x040014E9 RID: 5353
		[SerializeField]
		private ExpeditionDestinationList _demoExpeditionDestinationList;

		// Token: 0x040014EA RID: 5354
		[SerializeField]
		private ExpeditionDestinationList _expeditionDestinationList;

		// Token: 0x040014EB RID: 5355
		[SerializeField]
		private RemasterItemsSlotsController _remasterItemsSlotsController;

		// Token: 0x040014EC RID: 5356
		[SerializeField]
		private GlobalBoolVariable _isTutorialVariable;

		// Token: 0x040014ED RID: 5357
		[SerializeField]
		private FunctionTextController _reassuringDescription;

		// Token: 0x040014EE RID: 5358
		[SerializeField]
		private GlobalBoolVariable _attentionVariable;

		// Token: 0x040014EF RID: 5359
		[SerializeField]
		private GlobalBoolVariable _isTutorialExpedition;

		// Token: 0x040014F0 RID: 5360
		[SerializeField]
		private GameObject _handsContainer;
	}
}

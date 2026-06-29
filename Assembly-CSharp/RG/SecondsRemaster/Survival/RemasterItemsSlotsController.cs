using System;
using RG.Parsecs.Survival;
using UnityEngine;

namespace RG.SecondsRemaster.Survival
{
	// Token: 0x02000325 RID: 805
	public class RemasterItemsSlotsController : MonoBehaviour
	{
		// Token: 0x06001B1A RID: 6938 RVA: 0x00074E50 File Offset: 0x00073050
		public void SetHandButtonsDefaultState()
		{
			if (this._remasterItemSlotControllers == null)
			{
				return;
			}
			bool flag = false;
			for (int i = 0; i < this._sendExpeditionCharacterToggles.Length; i++)
			{
				flag |= this._sendExpeditionCharacterToggles[i].Toggle.isOn;
			}
			this._remasterItemSlotControllers[0].SetButtonInteractable(flag);
			this.SetAdditionalItemSlotsInteractable(false);
			for (int j = 0; j < this._remasterItemSlotControllers.Length; j++)
			{
				this._remasterItemSlotControllers[j].SetCurrentItem(null);
			}
		}

		// Token: 0x06001B1B RID: 6939 RVA: 0x00074EC6 File Offset: 0x000730C6
		public void RemoveItemFromExpeditionItems(IItem item)
		{
			this._expeditionData.RuntimeData.PlannedExpeditionData.ExpeditionItems.Remove(item);
			if (item == this._suitcase)
			{
				this.SetAdditionalItemSlotsInteractable(false);
			}
		}

		// Token: 0x06001B1C RID: 6940 RVA: 0x00074EF9 File Offset: 0x000730F9
		public void AddItemToExpeditionItems(IItem item)
		{
			this._expeditionData.RuntimeData.PlannedExpeditionData.ExpeditionItems.Add(item);
			if (item == this._suitcase)
			{
				this.SetAdditionalItemSlotsInteractable(true);
			}
		}

		// Token: 0x06001B1D RID: 6941 RVA: 0x00074F2C File Offset: 0x0007312C
		private void SetAdditionalItemSlotsInteractable(bool interactable)
		{
			for (int i = 1; i < this._remasterItemSlotControllers.Length; i++)
			{
				this._remasterItemSlotControllers[i].SetButtonInteractable(interactable);
			}
		}

		// Token: 0x06001B1E RID: 6942 RVA: 0x00074F5C File Offset: 0x0007315C
		public void SetHandsInteractable()
		{
			if (this._remasterItemSlotControllers == null)
			{
				return;
			}
			bool flag = false;
			for (int i = 0; i < this._sendExpeditionCharacterToggles.Length; i++)
			{
				if (!this._sendExpeditionCharacterToggles[i].Character.RuntimeData.CurrentStatuses.Contains(this._specialCharacterStatus))
				{
					flag |= this._sendExpeditionCharacterToggles[i].Toggle.isOn;
				}
			}
			this._remasterItemSlotControllers[0].SetButtonInteractable(flag);
		}

		// Token: 0x040014DA RID: 5338
		[SerializeField]
		private RemasterItemSlotController[] _remasterItemSlotControllers;

		// Token: 0x040014DB RID: 5339
		[SerializeField]
		private ItemList _itemList;

		// Token: 0x040014DC RID: 5340
		[SerializeField]
		private ExpeditionData _expeditionData;

		// Token: 0x040014DD RID: 5341
		[SerializeField]
		private IItem _suitcase;

		// Token: 0x040014DE RID: 5342
		[SerializeField]
		private SendExpeditionCharacterToggle[] _sendExpeditionCharacterToggles;

		// Token: 0x040014DF RID: 5343
		[SerializeField]
		private CharacterStatus _specialCharacterStatus;
	}
}

using System;
using System.Collections.Generic;
using Rewired;
using RG.Parsecs.Common;
using RG.Parsecs.Survival;
using RG.VirtualInput;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace RG.SecondsRemaster.Survival
{
	// Token: 0x02000324 RID: 804
	public class RemasterItemSlotController : MonoBehaviour, IPointerClickHandler, IEventSystemHandler
	{
		// Token: 0x06001B0E RID: 6926 RVA: 0x00074A1E File Offset: 0x00072C1E
		public void Awake()
		{
			this._player = ReInput.players.GetPlayer(0);
			this._virtualInputButton = base.GetComponent<VirtualInputButton>();
			this._rectTransform = base.GetComponent<RectTransform>();
		}

		// Token: 0x06001B0F RID: 6927 RVA: 0x00074A4C File Offset: 0x00072C4C
		public void Update()
		{
			if (!this._button.interactable)
			{
				return;
			}
			if (this._player.GetButtonDown("NextItem") && this.IsPointerOverThisItemSlot())
			{
				this.SetNextItem();
			}
			if (this._player.GetButtonDown("PreviousItem") && this.IsPointerOverThisItemSlot())
			{
				this.SetPreviousItem();
			}
		}

		// Token: 0x06001B10 RID: 6928 RVA: 0x00074AA7 File Offset: 0x00072CA7
		public void SetButtonInteractable(bool interactable)
		{
			this._button.interactable = interactable;
			if (!interactable && this._currentItem != null)
			{
				this.SetCurrentItem(null);
			}
		}

		// Token: 0x06001B11 RID: 6929 RVA: 0x00074AD0 File Offset: 0x00072CD0
		public void SetCurrentItem(IItem item)
		{
			this.UnlockCurrentItem();
			if (item == null)
			{
				this._image.sprite = this._handIcon;
				this._currentItem = null;
				this._currentItemIndex = -1;
				return;
			}
			this._image.sprite = item.BaseStaticData.IconJournal;
			this._currentItem = item;
			this._currentItem.Lock();
			this._remasterItemsSlotsController.AddItemToExpeditionItems(item);
			if (this._itemList.Items.Contains(item))
			{
				this._currentItemIndex = this._itemList.Items.IndexOf(item);
				return;
			}
			this._currentItemIndex = 0;
		}

		// Token: 0x06001B12 RID: 6930 RVA: 0x00074B74 File Offset: 0x00072D74
		public void SetNextItem()
		{
			HandHintController.ShouldShowHint = false;
			if (this._onUiClickedSoundPlayer != null)
			{
				this._onUiClickedSoundPlayer.PlaySound();
			}
			this.UnlockCurrentItem();
			List<IItem> items = this._itemList.Items;
			int num;
			if (this._currentItemIndex == -1)
			{
				num = 0;
			}
			else
			{
				num = this._currentItemIndex + 1;
			}
			for (int i = num; i < items.Count; i++)
			{
				if (!items[i].IsDamaged() && items[i].BaseRuntimeData.IsAvailable && !this._expeditionData.RuntimeData.PlannedExpeditionData.ExpeditionItems.Contains(items[i]) && items[i].IsLockable())
				{
					this.SetCurrentItem(items[i]);
					return;
				}
			}
			if (this._currentItem != null)
			{
				this.SetCurrentItem(null);
			}
		}

		// Token: 0x06001B13 RID: 6931 RVA: 0x00074C50 File Offset: 0x00072E50
		private void SetPreviousItem()
		{
			HandHintController.ShouldShowHint = false;
			if (this._onUiClickedSoundPlayer != null)
			{
				this._onUiClickedSoundPlayer.PlaySound();
			}
			this.UnlockCurrentItem();
			List<IItem> items = this._itemList.Items;
			int num;
			if (this._currentItemIndex == -1)
			{
				num = items.Count - 1;
			}
			else
			{
				num = this._currentItemIndex - 1;
			}
			for (int i = num; i >= 0; i--)
			{
				if (!items[i].IsDamaged() && items[i].BaseRuntimeData.IsAvailable && !this._expeditionData.RuntimeData.PlannedExpeditionData.ExpeditionItems.Contains(items[i]) && items[i].IsLockable())
				{
					this.SetCurrentItem(items[i]);
					return;
				}
			}
			if (this._currentItem != null)
			{
				this.SetCurrentItem(null);
			}
		}

		// Token: 0x06001B14 RID: 6932 RVA: 0x00074D2C File Offset: 0x00072F2C
		private void UnlockCurrentItem()
		{
			if (this._currentItem != null)
			{
				this._remasterItemsSlotsController.RemoveItemFromExpeditionItems(this._currentItem);
				this._currentItem.Unlock();
			}
		}

		// Token: 0x06001B15 RID: 6933 RVA: 0x00074D58 File Offset: 0x00072F58
		private void OnMouseUpAsButton()
		{
			if (!this._button.interactable)
			{
				return;
			}
			this.SetPreviousItem();
		}

		// Token: 0x06001B16 RID: 6934 RVA: 0x00074D6E File Offset: 0x00072F6E
		public void GamepadNextItem()
		{
			if (this._player.controllers.GetLastActiveController().type == ControllerType.Joystick)
			{
				this.SetNextItem();
			}
		}

		// Token: 0x06001B17 RID: 6935 RVA: 0x00074D8E File Offset: 0x00072F8E
		public void OnPointerClick(PointerEventData eventData)
		{
			if (!this._button.interactable)
			{
				return;
			}
			if (eventData.button == PointerEventData.InputButton.Right)
			{
				this.SetPreviousItem();
			}
			if (eventData.button == PointerEventData.InputButton.Left)
			{
				this.SetNextItem();
			}
		}

		// Token: 0x06001B18 RID: 6936 RVA: 0x00074DBC File Offset: 0x00072FBC
		private bool IsPointerOverThisItemSlot()
		{
			Vector2 vector = this._rectTransform.sizeDelta * this._parentCanvas.scaleFactor;
			Vector3 mousePosition = Singleton<VirtualInputManager>.Instance.GetMousePosition();
			return Mathf.Abs(mousePosition.x - this._rectTransform.position.x) < vector.x / 2f && Mathf.Abs(mousePosition.y - this._rectTransform.position.y) < vector.y / 2f;
		}

		// Token: 0x040014CA RID: 5322
		[SerializeField]
		private Button _button;

		// Token: 0x040014CB RID: 5323
		[SerializeField]
		private Image _image;

		// Token: 0x040014CC RID: 5324
		[SerializeField]
		private Sprite _handIcon;

		// Token: 0x040014CD RID: 5325
		[SerializeField]
		private ItemList _itemList;

		// Token: 0x040014CE RID: 5326
		[SerializeField]
		private ExpeditionData _expeditionData;

		// Token: 0x040014CF RID: 5327
		[SerializeField]
		private RemasterItemsSlotsController _remasterItemsSlotsController;

		// Token: 0x040014D0 RID: 5328
		[SerializeField]
		private OnUIClickedSoundPlayer _onUiClickedSoundPlayer;

		// Token: 0x040014D1 RID: 5329
		private IItem _currentItem;

		// Token: 0x040014D2 RID: 5330
		private int _currentItemIndex;

		// Token: 0x040014D3 RID: 5331
		private const int NO_ITEM_INDEX = -1;

		// Token: 0x040014D4 RID: 5332
		private const string NEXT_ITEM_BUTTON = "NextItem";

		// Token: 0x040014D5 RID: 5333
		private const string PREVIOUS_ITEM_BUTTON = "PreviousItem";

		// Token: 0x040014D6 RID: 5334
		private Player _player;

		// Token: 0x040014D7 RID: 5335
		private VirtualInputButton _virtualInputButton;

		// Token: 0x040014D8 RID: 5336
		private RectTransform _rectTransform;

		// Token: 0x040014D9 RID: 5337
		[SerializeField]
		private Canvas _parentCanvas;
	}
}

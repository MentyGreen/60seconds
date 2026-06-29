using System;
using System.Collections.Generic;
using RG.Core.SaveSystem;
using RG.Parsecs.Survival;
using UnityEngine;

namespace RG.SecondsRemaster.Survival
{
	// Token: 0x020002F2 RID: 754
	[Serializable]
	public sealed class ItemChoiceJournalContent : JournalContent
	{
		// Token: 0x060019F8 RID: 6648 RVA: 0x00070D95 File Offset: 0x0006EF95
		public ItemChoiceJournalContent(List<IItem> items) : base(int.MinValue)
		{
			this.type = EJournalContentType.ITEM_CHOICE;
			this._items = items;
		}

		// Token: 0x170004B8 RID: 1208
		// (get) Token: 0x060019F9 RID: 6649 RVA: 0x00070DB0 File Offset: 0x0006EFB0
		public List<IItem> Items
		{
			get
			{
				return this._items;
			}
		}

		// Token: 0x060019FA RID: 6650 RVA: 0x00070DB8 File Offset: 0x0006EFB8
		public ItemChoiceJournalContent(string serializedData, SaveEvent saveEvent) : base(saveEvent)
		{
			this.Deserialize(serializedData, saveEvent);
		}

		// Token: 0x060019FB RID: 6651 RVA: 0x00070DCC File Offset: 0x0006EFCC
		public override string Serialize()
		{
			ItemChoiceJournalContentWrapper itemChoiceJournalContentWrapper = new ItemChoiceJournalContentWrapper
			{
				DisplayOrder = this.displayOrder,
				DisplayPriority = this.displayPriority,
				GroupId = ((this.groupId != null) ? this.groupId.Guid : string.Empty),
				Type = this.type,
				Items = new List<string>()
			};
			if (this._items != null)
			{
				for (int i = 0; i < this._items.Count; i++)
				{
					if (this._items[i] != null)
					{
						itemChoiceJournalContentWrapper.Items.Add(this._items[i].Guid);
					}
				}
			}
			return JsonUtility.ToJson(itemChoiceJournalContentWrapper);
		}

		// Token: 0x060019FC RID: 6652 RVA: 0x00070E88 File Offset: 0x0006F088
		public override void Deserialize(string data, SaveEvent saveEvent)
		{
			ItemChoiceJournalContentWrapper itemChoiceJournalContentWrapper = JsonUtility.FromJson<ItemChoiceJournalContentWrapper>(data);
			base.DeserializeBaseWrapper(itemChoiceJournalContentWrapper, saveEvent);
			for (int i = 0; i < itemChoiceJournalContentWrapper.Items.Count; i++)
			{
				if (!string.IsNullOrEmpty(itemChoiceJournalContentWrapper.Items[i]))
				{
					this._items.Add((IItem)saveEvent.GetReferenceObjectByID(itemChoiceJournalContentWrapper.Items[i]));
				}
			}
		}

		// Token: 0x040013E5 RID: 5093
		[SerializeField]
		private List<IItem> _items;
	}
}

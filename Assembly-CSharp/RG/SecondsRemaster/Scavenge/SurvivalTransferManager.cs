using System;
using System.Collections.Generic;
using RG.Parsecs.Common;
using RG.Parsecs.Survival;
using UnityEngine;

namespace RG.SecondsRemaster.Scavenge
{
	// Token: 0x020002CF RID: 719
	public class SurvivalTransferManager : MonoBehaviour
	{
		// Token: 0x17000497 RID: 1175
		// (get) Token: 0x0600193C RID: 6460 RVA: 0x0006DC96 File Offset: 0x0006BE96
		public ScavengeItemList ItemList
		{
			get
			{
				return this._itemList;
			}
		}

		// Token: 0x0600193D RID: 6461 RVA: 0x0006DC9E File Offset: 0x0006BE9E
		private void Awake()
		{
			this.ResetItems(null, null);
		}

		// Token: 0x0600193E RID: 6462 RVA: 0x0006DCA8 File Offset: 0x0006BEA8
		public void ResetItems(List<ScavengeItem> excludeItems = null, int[] excludeAmount = null)
		{
			for (int i = 0; i < this._itemList.Items.Count; i++)
			{
				if (excludeItems != null && excludeItems.Contains(this._itemList.Items[i]))
				{
					if (excludeAmount == null)
					{
						this._itemList.Items[i].ResetItem(true, 0);
					}
					else
					{
						int num = excludeItems.IndexOf(this._itemList.Items[i]);
						if (excludeAmount[num] != 0)
						{
							this._itemList.Items[i].ResetItem(false, excludeAmount[num]);
							excludeAmount[num] = 0;
						}
					}
				}
				else
				{
					this._itemList.Items[i].ResetItem(false, 0);
				}
			}
		}

		// Token: 0x0600193F RID: 6463 RVA: 0x0006DD64 File Offset: 0x0006BF64
		public void TransferScavengedItems()
		{
			StatsManager instance = StatsManager.Instance;
			for (int i = 0; i < this._itemList.Items.Count; i++)
			{
				ScavengeItem scavengeItem = this._itemList.Items[i];
				if (!(scavengeItem == null) && scavengeItem.WasTaken)
				{
					if (scavengeItem.Character != null)
					{
						if (!this._characterList.CharactersInGame.Contains(scavengeItem.Character))
						{
							this._characterList.AddCharToList(scavengeItem.Character);
						}
					}
					else if (scavengeItem.Item != null)
					{
						ConsumableRemedium consumableRemedium = scavengeItem.Item as ConsumableRemedium;
						instance.AddGlobalData("TotalItemsCollected", scavengeItem.Amount);
						if (consumableRemedium != null)
						{
							if (consumableRemedium.StaticData.ItemId.Equals("item_water"))
							{
								instance.AddGlobalData("ScavengedWater", scavengeItem.Amount);
							}
							else
							{
								instance.AddGlobalData("ScavengedSoups", scavengeItem.Amount);
							}
							consumableRemedium.RuntimeData.Amount = (float)scavengeItem.Amount;
						}
						else
						{
							instance.AddGlobalDataToList("ScavengedItemsIDs", scavengeItem.Item.Guid);
							scavengeItem.Item.BaseRuntimeData.IsAvailable = true;
						}
					}
				}
			}
		}

		// Token: 0x06001940 RID: 6464 RVA: 0x0006DEB0 File Offset: 0x0006C0B0
		public void TransferHeldItems()
		{
			for (int i = 0; i < this._itemList.Items.Count; i++)
			{
				if (this._itemList.Items[i].AmountHolded > 0)
				{
					this._itemList.Items[i].TransferHoldedItems();
				}
			}
		}

		// Token: 0x06001941 RID: 6465 RVA: 0x0006DF08 File Offset: 0x0006C108
		public List<ScavengeItem> GetCurrentInventory()
		{
			if (this._currentItems == null)
			{
				this._currentItems = new List<ScavengeItem>();
			}
			for (int i = 0; i < this._itemList.Items.Count; i++)
			{
				ScavengeItem scavengeItem = this._itemList.Items[i];
				if (scavengeItem.WasTaken && !this._currentItems.Contains(scavengeItem))
				{
					this._currentItems.Add(scavengeItem);
				}
			}
			return this._currentItems;
		}

		// Token: 0x06001942 RID: 6466 RVA: 0x0006DF80 File Offset: 0x0006C180
		public int GetCurrentItemsCount()
		{
			int num = 0;
			for (int i = 0; i < this._itemList.Items.Count; i++)
			{
				ScavengeItem scavengeItem = this._itemList.Items[i];
				if (scavengeItem.WasTaken)
				{
					num += scavengeItem.Amount;
				}
			}
			return num;
		}

		// Token: 0x04001307 RID: 4871
		[SerializeField]
		private CharacterList _characterList;

		// Token: 0x04001308 RID: 4872
		[SerializeField]
		private ScavengeItemList _itemList;

		// Token: 0x04001309 RID: 4873
		[SerializeField]
		private ScavengeItem _soup;

		// Token: 0x0400130A RID: 4874
		[SerializeField]
		private ScavengeItem _water;

		// Token: 0x0400130B RID: 4875
		private List<ScavengeItem> _currentItems;
	}
}

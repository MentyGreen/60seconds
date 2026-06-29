using System;
using RG.Parsecs.Survival;
using RG.SecondsRemaster.Core;

namespace RG.SecondsRemaster.Survival
{
	// Token: 0x02000344 RID: 836
	public class RemasterItemVisibility : ItemVisibility
	{
		// Token: 0x06001BC8 RID: 7112 RVA: 0x00077304 File Offset: 0x00075504
		public override void RefreshDisplay()
		{
			this.SetItemActive(this._item.BaseRuntimeData.IsAvailable);
		}

		// Token: 0x06001BC9 RID: 7113 RVA: 0x0007731C File Offset: 0x0007551C
		private void SetItemActive(bool active)
		{
			bool damaged = false;
			if (this._item is Item)
			{
				damaged = ((Item)this._item).RuntimeData.IsDamaged;
			}
			else if (this._item is SecondsRemedium)
			{
				damaged = ((SecondsRemedium)this._item).SecondsRemediumRuntimeData.IsDamaged;
			}
			for (int i = 0; i < this._itemContainers.Length; i++)
			{
				this._itemContainers[i].SetActive(i + 1 == this._item.BaseRuntimeData.Level && active, damaged);
			}
			this._virtualButton.Selectable.interactable = active;
		}
	}
}

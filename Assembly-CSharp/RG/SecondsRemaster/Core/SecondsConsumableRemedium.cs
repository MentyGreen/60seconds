using System;
using RG.Parsecs.Survival;
using RG.SecondsRemaster.Survival;
using UnityEngine;

namespace RG.SecondsRemaster.Core
{
	// Token: 0x0200024B RID: 587
	[CreateAssetMenu(menuName = "60 Seconds Remaster!/Crafting/New consumable Remedium", fileName = "New Consumable Remedium")]
	public class SecondsConsumableRemedium : ConsumableRemedium
	{
		// Token: 0x06001616 RID: 5654 RVA: 0x00060DD7 File Offset: 0x0005EFD7
		public override void Use()
		{
			base.RuntimeData.Amount -= 1f;
			if (base.RuntimeData.Amount < 0f)
			{
				base.RuntimeData.Amount = 0f;
			}
		}

		// Token: 0x1700043F RID: 1087
		// (get) Token: 0x06001617 RID: 5655 RVA: 0x00060E12 File Offset: 0x0005F012
		public IconSizeDefinition IconSizeDefinition
		{
			get
			{
				return this._iconSizeDefinition;
			}
		}

		// Token: 0x04000ED4 RID: 3796
		private const float WHOLE_CONSUMABLE_AMOUNT = 1f;

		// Token: 0x04000ED5 RID: 3797
		[SerializeField]
		private IconSizeDefinition _iconSizeDefinition;
	}
}

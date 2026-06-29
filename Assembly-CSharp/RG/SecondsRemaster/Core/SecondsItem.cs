using System;
using RG.Parsecs.Survival;
using RG.SecondsRemaster.Survival;
using UnityEngine;

namespace RG.SecondsRemaster.Core
{
	// Token: 0x0200024C RID: 588
	[CreateAssetMenu(menuName = "60 Seconds Remaster!/Crafting/New Item", fileName = "New Item")]
	public class SecondsItem : Item
	{
		// Token: 0x17000440 RID: 1088
		// (get) Token: 0x06001619 RID: 5657 RVA: 0x00060E22 File Offset: 0x0005F022
		public IconSizeDefinition IconSizeDefinition
		{
			get
			{
				return this._iconSizeDefinition;
			}
		}

		// Token: 0x0600161A RID: 5658 RVA: 0x00060E2A File Offset: 0x0005F02A
		public override void SetDamage()
		{
			if (this._isDamageable)
			{
				base.SetDamage();
				return;
			}
			this.Remove();
		}

		// Token: 0x0600161B RID: 5659 RVA: 0x00060E41 File Offset: 0x0005F041
		public override void UseItem(int value)
		{
			this.SetDamage();
		}

		// Token: 0x0600161C RID: 5660 RVA: 0x00060E49 File Offset: 0x0005F049
		public override bool IsLockable()
		{
			return base.IsLockable() && !base.RuntimeData.IsDamaged;
		}

		// Token: 0x04000ED6 RID: 3798
		[SerializeField]
		private bool _isDamageable = true;

		// Token: 0x04000ED7 RID: 3799
		[SerializeField]
		private IconSizeDefinition _iconSizeDefinition;
	}
}

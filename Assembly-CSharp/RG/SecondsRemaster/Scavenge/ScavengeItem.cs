using System;
using RG.Core.Base;
using RG.Parsecs.Survival;
using UnityEngine;

namespace RG.SecondsRemaster.Scavenge
{
	// Token: 0x020002C9 RID: 713
	[CreateAssetMenu(menuName = "60 Seconds Remaster!/Scavenge/Scavenge Item")]
	public class ScavengeItem : RGScriptableObject
	{
		// Token: 0x1700048E RID: 1166
		// (get) Token: 0x06001921 RID: 6433 RVA: 0x0006DAAC File Offset: 0x0006BCAC
		public IItem Item
		{
			get
			{
				return this._item;
			}
		}

		// Token: 0x1700048F RID: 1167
		// (get) Token: 0x06001922 RID: 6434 RVA: 0x0006DAB4 File Offset: 0x0006BCB4
		public Character Character
		{
			get
			{
				return this._character;
			}
		}

		// Token: 0x17000490 RID: 1168
		// (get) Token: 0x06001923 RID: 6435 RVA: 0x0006DABC File Offset: 0x0006BCBC
		public int Weight
		{
			get
			{
				return this._weight;
			}
		}

		// Token: 0x17000491 RID: 1169
		// (get) Token: 0x06001924 RID: 6436 RVA: 0x0006DAC4 File Offset: 0x0006BCC4
		public Sprite Icon
		{
			get
			{
				return this._icon;
			}
		}

		// Token: 0x17000492 RID: 1170
		// (get) Token: 0x06001925 RID: 6437 RVA: 0x0006DACC File Offset: 0x0006BCCC
		public Sprite MenuIcon
		{
			get
			{
				return this._menuIcon;
			}
		}

		// Token: 0x17000493 RID: 1171
		// (get) Token: 0x06001926 RID: 6438 RVA: 0x0006DAD4 File Offset: 0x0006BCD4
		public int Amount
		{
			get
			{
				return this._amount;
			}
		}

		// Token: 0x17000494 RID: 1172
		// (get) Token: 0x06001927 RID: 6439 RVA: 0x0006DADC File Offset: 0x0006BCDC
		public bool WasTaken
		{
			get
			{
				return this._amount > 0;
			}
		}

		// Token: 0x17000495 RID: 1173
		// (get) Token: 0x06001928 RID: 6440 RVA: 0x0006DAE7 File Offset: 0x0006BCE7
		public int AmountHolded
		{
			get
			{
				return this._amountHolded;
			}
		}

		// Token: 0x06001929 RID: 6441 RVA: 0x0006DAEF File Offset: 0x0006BCEF
		private void OnEnable()
		{
			this.ResetItem(false, 0);
		}

		// Token: 0x0600192A RID: 6442 RVA: 0x0006DAF9 File Offset: 0x0006BCF9
		public void SetItemAmount(int amount)
		{
			this._amount = amount;
		}

		// Token: 0x0600192B RID: 6443 RVA: 0x0006DB02 File Offset: 0x0006BD02
		public void AddItem(int amount = 1)
		{
			this._amount += amount;
		}

		// Token: 0x0600192C RID: 6444 RVA: 0x0006DB12 File Offset: 0x0006BD12
		public void AddHeldItem(int amount = 1)
		{
			this._amountHolded += amount;
		}

		// Token: 0x0600192D RID: 6445 RVA: 0x0006DB22 File Offset: 0x0006BD22
		public void TransferHoldedItems()
		{
			this.AddItem(this._amountHolded);
			this._amountHolded = 0;
		}

		// Token: 0x0600192E RID: 6446 RVA: 0x0006DB37 File Offset: 0x0006BD37
		public void ResetItem(bool onlyHoldedAmount = false, int amountToLeave = 0)
		{
			if (!onlyHoldedAmount)
			{
				this._amount = amountToLeave;
			}
			this._amountHolded = 0;
		}

		// Token: 0x040012F1 RID: 4849
		[SerializeField]
		private IItem _item;

		// Token: 0x040012F2 RID: 4850
		[SerializeField]
		private Character _character;

		// Token: 0x040012F3 RID: 4851
		[SerializeField]
		private int _weight = 1;

		// Token: 0x040012F4 RID: 4852
		[SerializeField]
		private Sprite _icon;

		// Token: 0x040012F5 RID: 4853
		[SerializeField]
		private Sprite _menuIcon;

		// Token: 0x040012F6 RID: 4854
		private int _amount;

		// Token: 0x040012F7 RID: 4855
		private int _amountHolded;
	}
}

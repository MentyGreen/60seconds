using System;
using UnityEngine;
using UnityEngine.UI;

namespace RG.SecondsRemaster.Scavenge
{
	// Token: 0x020002C8 RID: 712
	public class HandsController : MonoBehaviour
	{
		// Token: 0x06001919 RID: 6425 RVA: 0x0006D8E9 File Offset: 0x0006BAE9
		private void Awake()
		{
			this._uiImages = base.GetComponentsInChildren<Image>(true);
		}

		// Token: 0x1700048D RID: 1165
		// (get) Token: 0x0600191A RID: 6426 RVA: 0x0006D8F8 File Offset: 0x0006BAF8
		public ScavengeItem LastScavengeItemAdded
		{
			get
			{
				return this._lastScavengeItemAdded;
			}
		}

		// Token: 0x0600191B RID: 6427 RVA: 0x0006D900 File Offset: 0x0006BB00
		public bool AddItem(ScavengeItem item)
		{
			if (!this.WillItemFit(item))
			{
				return false;
			}
			int num = this._currentIndex + item.Weight - 1;
			bool flag = false;
			for (int i = this._currentIndex; i <= num; i++)
			{
				this._itemImages[i].sprite = item.Icon;
				this._itemImages[i].color = (flag ? this.SEMI_TRANSPARENT_COLOR : this.WHITE_COLOR);
				this._handsAnimators[i].SetTrigger("Show_Item");
				if (!flag)
				{
					flag = true;
				}
			}
			this._lastScavengeItemAdded = item;
			this._currentIndex = num + 1;
			if (this._currentIndex >= this._itemImages.Length)
			{
				this._toTheShelterText.ShowTextDelayed(0.5f);
			}
			return true;
		}

		// Token: 0x0600191C RID: 6428 RVA: 0x0006D9B8 File Offset: 0x0006BBB8
		public void Clear()
		{
			for (int i = 0; i < this._itemImages.Length; i++)
			{
				if (this._itemImages[i].gameObject.activeSelf)
				{
					this._handsAnimators[i].SetTrigger("Show_Hand");
				}
			}
			this._currentIndex = 0;
		}

		// Token: 0x0600191D RID: 6429 RVA: 0x0006DA05 File Offset: 0x0006BC05
		public bool AreHandsEmpty()
		{
			return this._currentIndex == 0;
		}

		// Token: 0x0600191E RID: 6430 RVA: 0x0006DA10 File Offset: 0x0006BC10
		public bool WillItemFit(ScavengeItem item)
		{
			return this._currentIndex + item.Weight - 1 < this._itemImages.Length;
		}

		// Token: 0x0600191F RID: 6431 RVA: 0x0006DA2C File Offset: 0x0006BC2C
		public void HideHands()
		{
			for (int i = 0; i < this._uiImages.Length; i++)
			{
				this._uiImages[i].color = Color.clear;
			}
		}

		// Token: 0x040012E6 RID: 4838
		[SerializeField]
		private Image[] _itemImages;

		// Token: 0x040012E7 RID: 4839
		[SerializeField]
		private Animator[] _handsAnimators;

		// Token: 0x040012E8 RID: 4840
		[SerializeField]
		private ScavengeTextController _toTheShelterText;

		// Token: 0x040012E9 RID: 4841
		private const float TO_THE_SHELTER_TEXT_DELAY = 0.5f;

		// Token: 0x040012EA RID: 4842
		private int _currentIndex;

		// Token: 0x040012EB RID: 4843
		private const string SHOW_ITEM_PARAM_NAME = "Show_Item";

		// Token: 0x040012EC RID: 4844
		private const string SHOW_HAND_PARAM_NAME = "Show_Hand";

		// Token: 0x040012ED RID: 4845
		private readonly Color SEMI_TRANSPARENT_COLOR = new Color(1f, 1f, 1f, 0.5f);

		// Token: 0x040012EE RID: 4846
		private readonly Color WHITE_COLOR = new Color(1f, 1f, 1f);

		// Token: 0x040012EF RID: 4847
		private ScavengeItem _lastScavengeItemAdded;

		// Token: 0x040012F0 RID: 4848
		private Image[] _uiImages;
	}
}

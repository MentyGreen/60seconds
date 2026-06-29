using System;
using RG.Parsecs.Survival;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RG.SecondsRemaster.Survival
{
	// Token: 0x0200032D RID: 813
	public class RationAllController : MonoBehaviour
	{
		// Token: 0x06001B42 RID: 6978 RVA: 0x000758F4 File Offset: 0x00073AF4
		private void SetCurrentFill(float amount)
		{
			float num = Mathf.Clamp(amount, 0f, (float)this._icons.Length);
			for (int i = 0; i < this._icons.Length; i++)
			{
				if (num >= 1f)
				{
					this._icons[i].fillAmount = 1f;
				}
				else
				{
					this._icons[i].fillAmount = num;
				}
				num -= 1f;
			}
		}

		// Token: 0x06001B43 RID: 6979 RVA: 0x0007595C File Offset: 0x00073B5C
		private void SetMoreTextField(float amount)
		{
			float num = amount - (float)this._icons.Length;
			this._moreTextField.text = ((num > 0f) ? string.Format("+{0:G}", num) : " ");
		}

		// Token: 0x06001B44 RID: 6980 RVA: 0x000759A0 File Offset: 0x00073BA0
		private void SetRationingInteractable(bool interactable)
		{
			this._rationAllButton.interactable = interactable;
			for (int i = 0; i < this._consumableToggles.Length; i++)
			{
				this._consumableToggles[i].Toggle.interactable = interactable;
			}
		}

		// Token: 0x06001B45 RID: 6981 RVA: 0x000759E0 File Offset: 0x00073BE0
		public void UpdateFill()
		{
			this.SetRationingInteractable(this._consumable.RuntimeData.Amount > 0.001f);
			float num = this._consumable.RuntimeData.Amount - this._consumable.RuntimeData.PlannedConsumption;
			this.SetCurrentFill(num);
			this.SetMoreTextField(num);
		}

		// Token: 0x06001B46 RID: 6982 RVA: 0x00075A3C File Offset: 0x00073C3C
		public void RationAll()
		{
			bool toggleWithoutInvokingValueChange = false;
			if (!this._rationingManager.AreAllEligibleCharactersRationed(this._consumable) && this._rationingManager.IsThereEnoughRations(this._consumable))
			{
				this._rationingManager.RationAllCharacters(this._consumable);
				toggleWithoutInvokingValueChange = true;
			}
			else
			{
				this._rationingManager.UnrationAllCharacters(this._consumable);
			}
			for (int i = 0; i < this._consumableToggles.Length; i++)
			{
				this._consumableToggles[i].SetToggleWithoutInvokingValueChange(toggleWithoutInvokingValueChange);
			}
			this.UpdateFill();
		}

		// Token: 0x04001509 RID: 5385
		[SerializeField]
		private RationingManager _rationingManager;

		// Token: 0x0400150A RID: 5386
		[SerializeField]
		private TextMeshProUGUI _moreTextField;

		// Token: 0x0400150B RID: 5387
		[SerializeField]
		private Image[] _icons;

		// Token: 0x0400150C RID: 5388
		[SerializeField]
		private ConsumableRemedium _consumable;

		// Token: 0x0400150D RID: 5389
		[SerializeField]
		private RationingToggle[] _consumableToggles;

		// Token: 0x0400150E RID: 5390
		[SerializeField]
		private Button _rationAllButton;

		// Token: 0x0400150F RID: 5391
		private const float CONSUMABLE_MIN_AMOUNT = 0.001f;
	}
}

using System;
using I2.Loc;
using RG.Parsecs.Common;
using RG.Parsecs.Survival;
using RG.SecondsRemaster.Core;
using TMPro;
using UnityEngine;

namespace RG.Remaster.Survival
{
	// Token: 0x02000233 RID: 563
	public class RemasterItemTooltipContentHandler : TooltipContentHandler
	{
		// Token: 0x060015A1 RID: 5537 RVA: 0x0005FCE6 File Offset: 0x0005DEE6
		public override void HandleContent(TooltipContent content)
		{
			if (content as ItemTooltipContent)
			{
				this.HandleItemTooltipContent((ItemTooltipContent)content);
			}
		}

		// Token: 0x060015A2 RID: 5538 RVA: 0x0005FD04 File Offset: 0x0005DF04
		private void HandleItemTooltipContent(ItemTooltipContent content)
		{
			if (this._text != null && content.Item != null)
			{
				if (content.Item is Item)
				{
					Item item = (Item)content.Item;
					if (item.RuntimeData.IsDamaged && !string.IsNullOrEmpty(content.DamagedItemName))
					{
						this._nameInfo = content.DamagedItemName;
					}
					else
					{
						this._nameInfo = item.BaseStaticData.Name;
					}
				}
				else if (content.Item is SecondsRemedium)
				{
					SecondsRemedium secondsRemedium = (SecondsRemedium)content.Item;
					if (secondsRemedium.SecondsRemediumRuntimeData.IsDamaged && !string.IsNullOrEmpty(content.DamagedItemName))
					{
						this._nameInfo = content.DamagedItemName;
					}
					else
					{
						this._nameInfo = secondsRemedium.BaseStaticData.Name;
					}
				}
				else if (content.Item is ConsumableRemedium)
				{
					ConsumableRemedium consumableRemedium = (ConsumableRemedium)content.Item;
					this._itemInfo = string.Empty;
					if (content.ItemNamesWithLevel != null && consumableRemedium.RuntimeData.Level - 1 < content.ItemNamesWithLevel.Count)
					{
						this._nameInfo = string.Format(content.ItemNamesWithLevel[consumableRemedium.RuntimeData.Level - 1], consumableRemedium.RuntimeData.Amount.ToString());
					}
				}
				else if (content.Item is Remedium)
				{
					Remedium remedium = (Remedium)content.Item;
					this._itemInfo = string.Empty;
					if (content.ItemNamesWithLevel != null && remedium.RuntimeData.Level - 1 < content.ItemNamesWithLevel.Count)
					{
						this._nameInfo = content.ItemNamesWithLevel[remedium.RuntimeData.Level - 1];
					}
				}
				if (string.IsNullOrEmpty(content.TooltipTerm.Term))
				{
					this._termString = string.Empty;
				}
				else
				{
					this._termString = content.TooltipTerm.Term;
				}
				if (content.Item is Item || content.Item is Remedium)
				{
					this._text.text = this._nameInfo;
					this._text.gameObject.GetComponent<Localize>().OnLocalize(false);
					if (!string.IsNullOrEmpty(this._termString))
					{
						this._additionalText.text = string.Format("<color={1}>{0}</color={1}>", this._termString, this._color);
						this._additionalText.gameObject.SetActive(true);
						return;
					}
					this._additionalText.text = string.Empty;
					this._additionalText.gameObject.SetActive(false);
					return;
				}
				else
				{
					this._text.text = string.Format("{0} {1}{2}{3}", new object[]
					{
						this._nameInfo,
						this._itemInfo,
						Environment.NewLine,
						this._termString
					});
					this._text.gameObject.GetComponent<Localize>().OnLocalize(false);
				}
			}
		}

		// Token: 0x04000E8E RID: 3726
		[Tooltip("Text handler of tooltip.")]
		[SerializeField]
		private TextMeshProUGUI _text;

		// Token: 0x04000E8F RID: 3727
		[Tooltip("Additional text handler of tooltip.")]
		[SerializeField]
		private TextMeshProUGUI _additionalText;

		// Token: 0x04000E90 RID: 3728
		[SerializeField]
		private LocalizedString _color;

		// Token: 0x04000E91 RID: 3729
		private string _nameInfo;

		// Token: 0x04000E92 RID: 3730
		private string _itemInfo;

		// Token: 0x04000E93 RID: 3731
		private string _termString;

		// Token: 0x04000E94 RID: 3732
		private const string ADDITIONAL_MESSAGE_FORMAT = "<color={1}>{0}</color={1}>";
	}
}

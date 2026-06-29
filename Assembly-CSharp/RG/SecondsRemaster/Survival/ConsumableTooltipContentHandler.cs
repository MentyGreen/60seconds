using System;
using I2.Loc;
using RG.Parsecs.Common;
using RG.SecondsRemaster.Core;
using TMPro;
using UnityEngine;

namespace RG.SecondsRemaster.Survival
{
	// Token: 0x02000347 RID: 839
	public class ConsumableTooltipContentHandler : TooltipContentHandler
	{
		// Token: 0x06001BD2 RID: 7122 RVA: 0x0007745D File Offset: 0x0007565D
		public override void HandleContent(TooltipContent content)
		{
			if (content as ConsumableTooltipContent)
			{
				this.HandleConsumableTooltipContent((ConsumableTooltipContent)content);
			}
		}

		// Token: 0x06001BD3 RID: 7123 RVA: 0x00077478 File Offset: 0x00075678
		private void HandleConsumableTooltipContent(ConsumableTooltipContent content)
		{
			SecondsConsumableRemedium consumable = content.Consumable;
			this._text.text = string.Format("{0}:", content.GeneralInfo);
			this._additionalText.text = string.Format("<color={0}>{1}: {2}</color={0}>", this._color, content.ContainerName, consumable.RuntimeData.Amount);
			this._text.gameObject.GetComponent<Localize>().OnLocalize(false);
			this._additionalText.gameObject.GetComponent<Localize>().OnLocalize(false);
		}

		// Token: 0x04001595 RID: 5525
		[SerializeField]
		private TextMeshProUGUI _text;

		// Token: 0x04001596 RID: 5526
		[SerializeField]
		private TextMeshProUGUI _additionalText;

		// Token: 0x04001597 RID: 5527
		[SerializeField]
		private LocalizedString _color;

		// Token: 0x04001598 RID: 5528
		private const string CONSUMABLE_TITLE_FORMAT = "{0}:";

		// Token: 0x04001599 RID: 5529
		private const string ADDITIONAL_TEXT_FORMAT = "<color={0}>{1}: {2}</color={0}>";
	}
}

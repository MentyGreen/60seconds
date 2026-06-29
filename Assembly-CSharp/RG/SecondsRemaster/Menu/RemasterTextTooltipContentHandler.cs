using System;
using RG.Parsecs.Common;
using TMPro;
using UnityEngine;

namespace RG.SecondsRemaster.Menu
{
	// Token: 0x020002BA RID: 698
	public class RemasterTextTooltipContentHandler : TooltipContentHandler
	{
		// Token: 0x060018C7 RID: 6343 RVA: 0x0006C9DA File Offset: 0x0006ABDA
		private void Awake()
		{
			this._tooltipColorHex = ColorUtility.ToHtmlStringRGB(this._tooltipColor);
		}

		// Token: 0x060018C8 RID: 6344 RVA: 0x0006C9F0 File Offset: 0x0006ABF0
		public override void HandleContent(TooltipContent content)
		{
			TextTooltipContent textTooltipContent = content as TextTooltipContent;
			if (textTooltipContent != null)
			{
				this._text.text = string.Format("<color=#{0}>{1}</color>", this._tooltipColorHex, textTooltipContent.TextTerm);
			}
		}

		// Token: 0x04001294 RID: 4756
		[SerializeField]
		private TextMeshProUGUI _text;

		// Token: 0x04001295 RID: 4757
		[SerializeField]
		private Color _tooltipColor;

		// Token: 0x04001296 RID: 4758
		private const string TOOLTIP_FORMAT = "<color=#{0}>{1}</color>";

		// Token: 0x04001297 RID: 4759
		private string _tooltipColorHex;
	}
}

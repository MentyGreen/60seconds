using System;
using RG.Parsecs.Common;
using TMPro;
using UnityEngine;

namespace RG.SecondsRemaster.Menu
{
	// Token: 0x020002B9 RID: 697
	public class NewGameTooltipContentHandler : TooltipContentHandler
	{
		// Token: 0x060018C4 RID: 6340 RVA: 0x0006C97C File Offset: 0x0006AB7C
		public override void HandleContent(TooltipContent content)
		{
			NewGameTooltipContent newGameTooltipContent = content as NewGameTooltipContent;
			if (newGameTooltipContent != null)
			{
				this._text.text = string.Format("<color=#{0}>{1}</color>", this._textColorHex, newGameTooltipContent.TextTerm);
			}
		}

		// Token: 0x060018C5 RID: 6341 RVA: 0x0006C9BF File Offset: 0x0006ABBF
		private void Awake()
		{
			this._textColorHex = ColorUtility.ToHtmlStringRGB(this._textColor);
		}

		// Token: 0x04001290 RID: 4752
		[SerializeField]
		private TextMeshProUGUI _text;

		// Token: 0x04001291 RID: 4753
		[SerializeField]
		private Color _textColor;

		// Token: 0x04001292 RID: 4754
		private string _textColorHex;

		// Token: 0x04001293 RID: 4755
		private const string TEXT_FORMAT = "<color=#{0}>{1}</color>";
	}
}

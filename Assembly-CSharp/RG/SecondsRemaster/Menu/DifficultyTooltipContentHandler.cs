using System;
using I2.Loc;
using RG.Parsecs.Common;
using TMPro;
using UnityEngine;

namespace RG.SecondsRemaster.Menu
{
	// Token: 0x020002B6 RID: 694
	public class DifficultyTooltipContentHandler : TooltipContentHandler
	{
		// Token: 0x060018BA RID: 6330 RVA: 0x0006C80C File Offset: 0x0006AA0C
		public override void HandleContent(TooltipContent content)
		{
			DifficultyTooltipContent difficultyTooltipContent = content as DifficultyTooltipContent;
			if (difficultyTooltipContent != null && this._difficultyHeaders.Length == difficultyTooltipContent.DifficultyLevelTexts.Length)
			{
				this.AppendTexts(this._headersTextFields, this._difficultyHeaders, "<uppercase><color=#{0}>{1}:</uppercase></color>", this._tooltipHeaderColorHex);
				this.AppendTexts(this._paramTextsFields, difficultyTooltipContent.DifficultyLevelTexts, "<color=#{0}>{1}</color>", this._tooltipParamColorHex);
			}
		}

		// Token: 0x060018BB RID: 6331 RVA: 0x0006C875 File Offset: 0x0006AA75
		private void Awake()
		{
			this._tooltipHeaderColorHex = ColorUtility.ToHtmlStringRGB(this._tooltipHeaderColor);
			this._tooltipParamColorHex = ColorUtility.ToHtmlStringRGB(this._tooltipParamColor);
		}

		// Token: 0x060018BC RID: 6332 RVA: 0x0006C89C File Offset: 0x0006AA9C
		private void AppendTexts(TextMeshProUGUI[] textFields, LocalizedString[] stringsToAppend, string format, string hexColor)
		{
			for (int i = 0; i < textFields.Length; i++)
			{
				textFields[i].text = string.Format(format, hexColor, stringsToAppend[i]);
			}
		}

		// Token: 0x04001283 RID: 4739
		[SerializeField]
		private Color _tooltipHeaderColor;

		// Token: 0x04001284 RID: 4740
		[SerializeField]
		private Color _tooltipParamColor;

		// Token: 0x04001285 RID: 4741
		[SerializeField]
		private TextMeshProUGUI[] _headersTextFields;

		// Token: 0x04001286 RID: 4742
		[SerializeField]
		private TextMeshProUGUI[] _paramTextsFields;

		// Token: 0x04001287 RID: 4743
		[SerializeField]
		[Tooltip("Tooltip headers, the number of elements must match the number of content strings!")]
		private LocalizedString[] _difficultyHeaders;

		// Token: 0x04001288 RID: 4744
		private string _tooltipHeaderColorHex;

		// Token: 0x04001289 RID: 4745
		private string _tooltipParamColorHex;

		// Token: 0x0400128A RID: 4746
		private const string DIFFICULTY_TOOLTIP_HEADER_FORMAT = "<uppercase><color=#{0}>{1}:</uppercase></color>";

		// Token: 0x0400128B RID: 4747
		private const string DIFFICULTY_TOOLTIP_PARAM_FORMAT = "<color=#{0}>{1}</color>";
	}
}

using System;
using TMPro;
using UnityEngine;

namespace RG.SecondsRemaster.Survival
{
	// Token: 0x020002FE RID: 766
	[RequireComponent(typeof(TextMeshProUGUI))]
	public class SetLineSpacing : MonoBehaviour
	{
		// Token: 0x06001A17 RID: 6679 RVA: 0x00071405 File Offset: 0x0006F605
		private void OnEnable()
		{
			if (this._textField == null)
			{
				this._textField = base.GetComponent<TextMeshProUGUI>();
			}
			this._textField.lineSpacing = this._linesDescription.GetLinesDescriptionForCurrentLanguage().LineSpacing;
		}

		// Token: 0x040013FB RID: 5115
		[SerializeField]
		private TextMeshProUGUI _textField;

		// Token: 0x040013FC RID: 5116
		[SerializeField]
		private LinesDescription _linesDescription;
	}
}

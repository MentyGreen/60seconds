using System;
using I2.Loc;
using RG.Parsecs.EventEditor;
using TMPro;
using UnityEngine;

namespace RG.SecondsRemaster.Menu
{
	// Token: 0x020002AB RID: 683
	public class QualitySettingController : MonoBehaviour
	{
		// Token: 0x06001887 RID: 6279 RVA: 0x0006B488 File Offset: 0x00069688
		private void OnEnable()
		{
			if (this._valueVariable.Value < 0)
			{
				this._valueVariable.Value = QualitySettings.GetQualityLevel();
			}
			this._valueField.text = this._qualityNames[this._valueVariable.Value];
			LocalizationManager.OnLocalizeEvent += this.TranslateText;
		}

		// Token: 0x06001888 RID: 6280 RVA: 0x0006B4EA File Offset: 0x000696EA
		private void OnDisable()
		{
			LocalizationManager.OnLocalizeEvent -= this.TranslateText;
		}

		// Token: 0x06001889 RID: 6281 RVA: 0x0006B4FD File Offset: 0x000696FD
		private void TranslateText()
		{
			this._valueField.text = this._qualityNames[this._valueVariable.Value];
		}

		// Token: 0x0600188A RID: 6282 RVA: 0x0006B528 File Offset: 0x00069728
		public void SetNext()
		{
			if (this._valueVariable.Value + 1 > this._maxQualityLevel)
			{
				QualitySettings.SetQualityLevel(0, this._applyInstantly);
			}
			else
			{
				QualitySettings.IncreaseLevel(this._applyInstantly);
			}
			this._valueVariable.Value = QualitySettings.GetQualityLevel();
			this._valueField.text = this._qualityNames[this._valueVariable.Value];
		}

		// Token: 0x0600188B RID: 6283 RVA: 0x0006B59C File Offset: 0x0006979C
		public void SetPrevious()
		{
			if (this._valueVariable.Value - 1 < 0)
			{
				QualitySettings.SetQualityLevel(this._maxQualityLevel, this._applyInstantly);
			}
			else
			{
				QualitySettings.DecreaseLevel(this._applyInstantly);
			}
			this._valueVariable.Value = QualitySettings.GetQualityLevel();
			this._valueField.text = this._qualityNames[this._valueVariable.Value];
		}

		// Token: 0x0400123D RID: 4669
		[SerializeField]
		private TextMeshProUGUI _valueField;

		// Token: 0x0400123E RID: 4670
		[SerializeField]
		private GlobalIntVariable _valueVariable;

		// Token: 0x0400123F RID: 4671
		[SerializeField]
		private int _maxQualityLevel = 3;

		// Token: 0x04001240 RID: 4672
		[SerializeField]
		private LocalizedString[] _qualityNames;

		// Token: 0x04001241 RID: 4673
		[SerializeField]
		private bool _applyInstantly;

		// Token: 0x04001242 RID: 4674
		private const int MIN_QUALITY_LEVEL = 0;
	}
}

using System;
using System.Collections.Generic;
using I2.Loc;
using RG.SecondsRemaster.EventEditor;
using TMPro;
using UnityEngine;

namespace RG.SecondsRemaster.Menu
{
	// Token: 0x020002A8 RID: 680
	public class LanguageSettingController : MonoBehaviour
	{
		// Token: 0x0600187D RID: 6269 RVA: 0x0006B0F0 File Offset: 0x000692F0
		private void OnEnable()
		{
			if (string.IsNullOrEmpty(this._valueVariable.Value))
			{
				this._currentIndex = this._languages.IndexOf(LocalizationManager.CurrentLanguage);
			}
			else
			{
				this._currentIndex = this._languageCodes.IndexOf(this._valueVariable.Value);
			}
			this._valueField.text = this._languageTerms[this._currentIndex];
			this._valueVariable.Value = this._languageCodes[this._currentIndex];
		}

		// Token: 0x0600187E RID: 6270 RVA: 0x0006B180 File Offset: 0x00069380
		public void SetNext()
		{
			if (this._currentIndex + 1 >= this._languages.Count)
			{
				this._currentIndex = 0;
			}
			else
			{
				this._currentIndex++;
			}
			this._valueField.text = this._languageTerms[this._currentIndex];
			this._valueVariable.Value = this._languageCodes[this._currentIndex];
			if (this._applyInstantly)
			{
				LocalizationManager.SetLanguageAndCode(this._languages[this._currentIndex], this._languageCodes[this._currentIndex], true, false);
			}
		}

		// Token: 0x0600187F RID: 6271 RVA: 0x0006B228 File Offset: 0x00069428
		public void SetPrevious()
		{
			if (this._currentIndex - 1 < 0)
			{
				this._currentIndex = this._languages.Count - 1;
			}
			else
			{
				this._currentIndex--;
			}
			this._valueField.text = this._languageTerms[this._currentIndex];
			this._valueVariable.Value = this._languageCodes[this._currentIndex];
			if (this._applyInstantly)
			{
				LocalizationManager.SetLanguageAndCode(this._languages[this._currentIndex], this._languageCodes[this._currentIndex], true, false);
			}
		}

		// Token: 0x0400122D RID: 4653
		[SerializeField]
		private TextMeshProUGUI _valueField;

		// Token: 0x0400122E RID: 4654
		[SerializeField]
		private GlobalStringVariable _valueVariable;

		// Token: 0x0400122F RID: 4655
		[SerializeField]
		private List<string> _languages;

		// Token: 0x04001230 RID: 4656
		[SerializeField]
		private List<string> _languageCodes;

		// Token: 0x04001231 RID: 4657
		[SerializeField]
		private List<LocalizedString> _languageTerms;

		// Token: 0x04001232 RID: 4658
		[SerializeField]
		private bool _applyInstantly;

		// Token: 0x04001233 RID: 4659
		private int _currentIndex;
	}
}

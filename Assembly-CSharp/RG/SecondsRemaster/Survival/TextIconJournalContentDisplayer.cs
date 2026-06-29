using System;
using I2.Loc;
using RG.Parsecs.Survival;
using TMPro;
using UnityEngine;

namespace RG.SecondsRemaster.Survival
{
	// Token: 0x02000307 RID: 775
	public class TextIconJournalContentDisplayer : JournalContentDisplayer<TextIconJournalContent>
	{
		// Token: 0x170004CD RID: 1229
		// (get) Token: 0x06001A4F RID: 6735 RVA: 0x00072084 File Offset: 0x00070284
		public override int LinesAmount
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x06001A50 RID: 6736 RVA: 0x00072088 File Offset: 0x00070288
		public override void SetContentData(JournalContent content)
		{
			if (content.Type != EJournalContentType.TEXT_ICON)
			{
				return;
			}
			TextIconJournalContent textIconJournalContent = (TextIconJournalContent)content;
			this._iconTerm = textIconJournalContent.IconTerm;
			this._currentAmount = textIconJournalContent.Amount;
			this._contentType = textIconJournalContent.IconType;
			this.UpdateDisplayedText();
		}

		// Token: 0x06001A51 RID: 6737 RVA: 0x000720D0 File Offset: 0x000702D0
		public void UpdateDisplayedText()
		{
			string arg = string.Empty;
			switch (this._contentType)
			{
			case EventContentData.ETextIconContentType.NONE:
				arg = "";
				break;
			case EventContentData.ETextIconContentType.ADDITION:
				arg = "+";
				break;
			case EventContentData.ETextIconContentType.SUBTRACTION:
				arg = "-";
				break;
			}
			this._textField.text = string.Format("{0}{1} {2} ", arg, (Mathf.Abs(this._currentAmount) > 0) ? this._currentAmount.ToString() : "", this._iconTerm);
		}

		// Token: 0x06001A52 RID: 6738 RVA: 0x00072158 File Offset: 0x00070358
		public bool TryToJoinNextTextIconContent(TextIconJournalContent iconContent)
		{
			if (iconContent.IconType == EventContentData.ETextIconContentType.NONE || iconContent.IconType != this._contentType || !this._iconTerm.ToString().Equals(iconContent.IconTerm.ToString()))
			{
				return false;
			}
			this._currentAmount += iconContent.Amount;
			this.UpdateDisplayedText();
			return true;
		}

		// Token: 0x04001421 RID: 5153
		[SerializeField]
		private TextMeshProUGUI _textField;

		// Token: 0x04001422 RID: 5154
		private int _currentAmount;

		// Token: 0x04001423 RID: 5155
		private LocalizedString _iconTerm;

		// Token: 0x04001424 RID: 5156
		private EventContentData.ETextIconContentType _contentType;
	}
}

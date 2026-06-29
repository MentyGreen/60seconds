using System;
using RG.Parsecs.EventEditor;
using RG.Parsecs.Survival;
using UnityEngine;

namespace RG.SecondsRemaster.Survival
{
	// Token: 0x0200030A RID: 778
	public class YesNoChoiceJournalContentDisplayer : JournalContentDisplayer<YesNoChoiceJournalContent>
	{
		// Token: 0x170004CE RID: 1230
		// (get) Token: 0x06001A57 RID: 6743 RVA: 0x000721F8 File Offset: 0x000703F8
		public override int LinesAmount
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x06001A58 RID: 6744 RVA: 0x000721FB File Offset: 0x000703FB
		public override void SetContentData(JournalContent content)
		{
			if (content.Type != EJournalContentType.YESNO_CHOICE)
			{
				return;
			}
			this._survivalData.DailyEventResolved = false;
			this._choiceCardsController.SetYesNoCards(null, null);
			this._switchButtonController.SetSelectable(SecondsEventManager.GetReferenceToJournalButtonNext());
			SecondsEventManager.SetCurrentChoiceCardsController(this._choiceCardsController);
		}

		// Token: 0x06001A59 RID: 6745 RVA: 0x0007223B File Offset: 0x0007043B
		public void SetAttentionVariableValue(bool value)
		{
			this._attentionVariable.Value = value;
		}

		// Token: 0x04001425 RID: 5157
		[SerializeField]
		private ChoiceCardsController _choiceCardsController;

		// Token: 0x04001426 RID: 5158
		[SerializeField]
		private SwitchButtonController _switchButtonController;

		// Token: 0x04001427 RID: 5159
		[SerializeField]
		private SurvivalData _survivalData;

		// Token: 0x04001428 RID: 5160
		[SerializeField]
		private GlobalBoolVariable _attentionVariable;
	}
}

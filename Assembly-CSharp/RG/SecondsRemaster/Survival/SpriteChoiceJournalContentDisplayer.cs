using System;
using System.Collections.Generic;
using RG.Parsecs.EventEditor;
using RG.Parsecs.Survival;
using UnityEngine;
using UnityEngine.UI;

namespace RG.SecondsRemaster.Survival
{
	// Token: 0x020002FC RID: 764
	public class SpriteChoiceJournalContentDisplayer : JournalContentDisplayer<SpriteChoiceJournalContent>
	{
		// Token: 0x170004BE RID: 1214
		// (get) Token: 0x06001A13 RID: 6675 RVA: 0x0007131E File Offset: 0x0006F51E
		public override int LinesAmount
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x06001A14 RID: 6676 RVA: 0x00071324 File Offset: 0x0006F524
		public override void SetContentData(JournalContent content)
		{
			if (content.Type != EJournalContentType.CUSTOM_CHOICE)
			{
				return;
			}
			SpriteChoiceJournalContent spriteChoiceJournalContent = (SpriteChoiceJournalContent)content;
			this._survivalData.DailyEventResolved = true;
			List<BaseActionCondition> actionConditions = spriteChoiceJournalContent.ActionConditions;
			this._choiceCardsController.SetSpriteCards(actionConditions[0], actionConditions[1], actionConditions[2], actionConditions[3]);
			for (int i = 0; i < this._checkmarks.Count; i++)
			{
				this._checkmarks[i].sprite = this._backgrounds[i].sprite;
				if (actionConditions[i] != null)
				{
					this._actionChoiceTooltipContent[i].SetTermContent(actionConditions[i].SelectableTerm);
				}
			}
			this._switchButtonController.SetSelectable(SecondsEventManager.GetReferenceToJournalButtonNext());
			SecondsEventManager.SetCurrentChoiceCardsController(this._choiceCardsController);
		}

		// Token: 0x040013F5 RID: 5109
		[SerializeField]
		private ChoiceCardsController _choiceCardsController;

		// Token: 0x040013F6 RID: 5110
		[SerializeField]
		private SwitchButtonController _switchButtonController;

		// Token: 0x040013F7 RID: 5111
		[SerializeField]
		private SurvivalData _survivalData;

		// Token: 0x040013F8 RID: 5112
		[SerializeField]
		private List<Image> _checkmarks;

		// Token: 0x040013F9 RID: 5113
		[SerializeField]
		private List<Image> _backgrounds;

		// Token: 0x040013FA RID: 5114
		[SerializeField]
		private ActionChoiceTooltipContent[] _actionChoiceTooltipContent;
	}
}

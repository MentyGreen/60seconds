using System;
using System.Collections.Generic;
using RG.Parsecs.Survival;
using UnityEngine;
using UnityEngine.UI;

namespace RG.SecondsRemaster.Survival
{
	// Token: 0x020002F4 RID: 756
	public class ItemChoiceJournalContentDisplayer : JournalContentDisplayer<ItemChoiceJournalContent>
	{
		// Token: 0x170004B9 RID: 1209
		// (get) Token: 0x060019FE RID: 6654 RVA: 0x00070EF7 File Offset: 0x0006F0F7
		public override int LinesAmount
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x060019FF RID: 6655 RVA: 0x00070EFC File Offset: 0x0006F0FC
		public override void SetContentData(JournalContent content)
		{
			if (content.Type != EJournalContentType.ITEM_CHOICE)
			{
				return;
			}
			ItemChoiceJournalContent itemChoiceJournalContent = (ItemChoiceJournalContent)content;
			this._survivalData.DailyEventResolved = true;
			List<IItem> items = itemChoiceJournalContent.Items;
			this._choiceCardsController.SetItemCards(items[0], items[1], items[2], items[3], false);
			int i = items.Count - 1;
			int num = 0;
			while (i >= 0)
			{
				this._checkmarks[i].sprite = this._backgrounds[i].sprite;
				if (!(items[i] == null))
				{
					this._actionChoiceTooltipContents[num].SetItemContent(items[i]);
					num++;
				}
				i--;
			}
			this._switchButtonController.SetSelectable(SecondsEventManager.GetReferenceToJournalButtonNext());
			SecondsEventManager.SetCurrentChoiceCardsController(this._choiceCardsController);
		}

		// Token: 0x040013E7 RID: 5095
		[SerializeField]
		private ChoiceCardsController _choiceCardsController;

		// Token: 0x040013E8 RID: 5096
		[SerializeField]
		private SwitchButtonController _switchButtonController;

		// Token: 0x040013E9 RID: 5097
		[SerializeField]
		private SurvivalData _survivalData;

		// Token: 0x040013EA RID: 5098
		[SerializeField]
		private List<Image> _checkmarks;

		// Token: 0x040013EB RID: 5099
		[SerializeField]
		private List<Image> _backgrounds;

		// Token: 0x040013EC RID: 5100
		[SerializeField]
		private ActionChoiceTooltipContent[] _actionChoiceTooltipContents;
	}
}

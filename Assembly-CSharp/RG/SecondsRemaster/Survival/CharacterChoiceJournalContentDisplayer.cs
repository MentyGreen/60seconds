using System;
using System.Collections.Generic;
using RG.Parsecs.Survival;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RG.SecondsRemaster.Survival
{
	// Token: 0x020002E7 RID: 743
	public class CharacterChoiceJournalContentDisplayer : JournalContentDisplayer<CharacterChoiceJournalContent>
	{
		// Token: 0x170004AE RID: 1198
		// (get) Token: 0x060019CC RID: 6604 RVA: 0x0006FD48 File Offset: 0x0006DF48
		public override int LinesAmount
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x060019CD RID: 6605 RVA: 0x0006FD4C File Offset: 0x0006DF4C
		public override void SetContentData(JournalContent content)
		{
			if (content.Type != EJournalContentType.CHARACTER_CHOICE)
			{
				return;
			}
			CharacterChoiceJournalContent characterChoiceJournalContent = (CharacterChoiceJournalContent)content;
			this._survivalData.DailyEventResolved = true;
			List<Character> characters = characterChoiceJournalContent.Characters;
			this._choiceCardsController.SetCharacterCards(characters[0], characters[1], characters[2], characters[3]);
			int i = characters.Count - 1;
			int num = 0;
			while (i >= 0)
			{
				this._checkmarks[i].sprite = this._backgrounds[i].sprite;
				if (!(characters[i] == null))
				{
					this._actionChoiceTooltipContents[num].SetCharacterContent(characters[i]);
					num++;
					SecondsCharacter secondsCharacter = characters[i] as SecondsCharacter;
					if (secondsCharacter != null && secondsCharacter.SizeDefinition != null)
					{
						this._rectTransforms[i].sizeDelta = secondsCharacter.SizeDefinition.Size;
						Canvas.ForceUpdateCanvases();
					}
				}
				i--;
			}
			if (characterChoiceJournalContent.CallToActionTerm == null || string.IsNullOrEmpty(characterChoiceJournalContent.CallToActionTerm.mTerm))
			{
				this._callToAction.gameObject.SetActive(false);
			}
			else
			{
				this._callToAction.gameObject.SetActive(true);
				this._callToAction.text = characterChoiceJournalContent.CallToActionTerm;
			}
			this._switchButtonController.SetSelectable(SecondsEventManager.GetReferenceToJournalButtonNext());
			SecondsEventManager.SetCurrentChoiceCardsController(this._choiceCardsController);
		}

		// Token: 0x040013B2 RID: 5042
		[SerializeField]
		private ChoiceCardsController _choiceCardsController;

		// Token: 0x040013B3 RID: 5043
		[SerializeField]
		private SwitchButtonController _switchButtonController;

		// Token: 0x040013B4 RID: 5044
		[SerializeField]
		private SurvivalData _survivalData;

		// Token: 0x040013B5 RID: 5045
		[SerializeField]
		private List<Image> _checkmarks;

		// Token: 0x040013B6 RID: 5046
		[SerializeField]
		private List<Image> _backgrounds;

		// Token: 0x040013B7 RID: 5047
		[SerializeField]
		private List<RectTransform> _rectTransforms;

		// Token: 0x040013B8 RID: 5048
		[SerializeField]
		private TextMeshProUGUI _callToAction;

		// Token: 0x040013B9 RID: 5049
		[SerializeField]
		private ActionChoiceTooltipContent[] _actionChoiceTooltipContents;
	}
}

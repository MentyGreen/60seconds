using System;
using System.Collections.Generic;
using I2.Loc;
using RG.Core.SaveSystem;
using RG.Parsecs.Survival;
using UnityEngine;

namespace RG.SecondsRemaster.Survival
{
	// Token: 0x020002E5 RID: 741
	[Serializable]
	public sealed class CharacterChoiceJournalContent : JournalContent
	{
		// Token: 0x060019C5 RID: 6597 RVA: 0x0006FBF0 File Offset: 0x0006DDF0
		public CharacterChoiceJournalContent(List<Character> characters, LocalizedString callToActionTerm) : base(int.MinValue)
		{
			this.type = EJournalContentType.CHARACTER_CHOICE;
			this._characters = characters;
			this._callToActionTerm = callToActionTerm;
		}

		// Token: 0x170004AC RID: 1196
		// (get) Token: 0x060019C6 RID: 6598 RVA: 0x0006FC12 File Offset: 0x0006DE12
		public List<Character> Characters
		{
			get
			{
				return this._characters;
			}
		}

		// Token: 0x170004AD RID: 1197
		// (get) Token: 0x060019C7 RID: 6599 RVA: 0x0006FC1A File Offset: 0x0006DE1A
		public LocalizedString CallToActionTerm
		{
			get
			{
				return this._callToActionTerm;
			}
		}

		// Token: 0x060019C8 RID: 6600 RVA: 0x0006FC22 File Offset: 0x0006DE22
		public CharacterChoiceJournalContent(string serializedData, SaveEvent saveEvent) : base(saveEvent)
		{
			this.Deserialize(serializedData, saveEvent);
		}

		// Token: 0x060019C9 RID: 6601 RVA: 0x0006FC34 File Offset: 0x0006DE34
		public override string Serialize()
		{
			CharacterChoiceJournalContentWrapper characterChoiceJournalContentWrapper = new CharacterChoiceJournalContentWrapper
			{
				DisplayOrder = this.displayOrder,
				DisplayPriority = this.displayPriority,
				GroupId = ((this.groupId != null) ? this.groupId.Guid : string.Empty),
				Type = this.type,
				Characters = new List<string>(),
				CallToActionTerm = this._callToActionTerm
			};
			for (int i = 0; i < this._characters.Count; i++)
			{
				characterChoiceJournalContentWrapper.Characters.Add(this._characters[i].Guid);
			}
			return JsonUtility.ToJson(characterChoiceJournalContentWrapper);
		}

		// Token: 0x060019CA RID: 6602 RVA: 0x0006FCE0 File Offset: 0x0006DEE0
		public override void Deserialize(string data, SaveEvent saveEvent)
		{
			CharacterChoiceJournalContentWrapper characterChoiceJournalContentWrapper = JsonUtility.FromJson<CharacterChoiceJournalContentWrapper>(data);
			base.DeserializeBaseWrapper(characterChoiceJournalContentWrapper, saveEvent);
			for (int i = 0; i < characterChoiceJournalContentWrapper.Characters.Count; i++)
			{
				this._characters.Add((Character)saveEvent.GetReferenceObjectByID(characterChoiceJournalContentWrapper.Characters[i]));
			}
			this._callToActionTerm = characterChoiceJournalContentWrapper.CallToActionTerm;
		}

		// Token: 0x040013AE RID: 5038
		[SerializeField]
		private List<Character> _characters;

		// Token: 0x040013AF RID: 5039
		[SerializeField]
		private LocalizedString _callToActionTerm;
	}
}

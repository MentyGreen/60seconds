using System;
using RG.Core.SaveSystem;

namespace RG.SecondsRemaster.Survival
{
	// Token: 0x02000309 RID: 777
	[Serializable]
	public sealed class YesNoChoiceJournalContent : JournalContent
	{
		// Token: 0x06001A55 RID: 6741 RVA: 0x000721D3 File Offset: 0x000703D3
		public YesNoChoiceJournalContent() : base(int.MinValue)
		{
			this.type = EJournalContentType.YESNO_CHOICE;
		}

		// Token: 0x06001A56 RID: 6742 RVA: 0x000721E7 File Offset: 0x000703E7
		public YesNoChoiceJournalContent(string serializedData, SaveEvent saveEvent) : base(saveEvent)
		{
			this.Deserialize(serializedData, saveEvent);
		}
	}
}

using System;
using System.Collections.Generic;

namespace RG.SecondsRemaster.Survival
{
	// Token: 0x020002EC RID: 748
	[Serializable]
	public struct JournalContentListWrapper
	{
		// Token: 0x040013C8 RID: 5064
		public List<string> TextJournalContents;

		// Token: 0x040013C9 RID: 5065
		public List<string> TextIconJournalContents;

		// Token: 0x040013CA RID: 5066
		public List<string> SpriteIconJournalContents;

		// Token: 0x040013CB RID: 5067
		public List<string> YesNoChoiceJournalContents;

		// Token: 0x040013CC RID: 5068
		public List<string> ItemChoiceJournalContents;

		// Token: 0x040013CD RID: 5069
		public List<string> CharacterChoiceJournalContents;

		// Token: 0x040013CE RID: 5070
		public List<string> SpriteChoiceJournalContents;

		// Token: 0x040013CF RID: 5071
		public List<string> GoalJournalContents;
	}
}

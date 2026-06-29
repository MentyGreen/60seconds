using System;
using System.Collections.Generic;
using I2.Loc;

namespace RG.SecondsRemaster.Survival
{
	// Token: 0x02000300 RID: 768
	[Serializable]
	public class TextJournalContentWrapper : JournalContentWrapper
	{
		// Token: 0x0400140C RID: 5132
		public string PureText;

		// Token: 0x0400140D RID: 5133
		public LocalizedString Term;

		// Token: 0x0400140E RID: 5134
		public List<string> Characters;

		// Token: 0x0400140F RID: 5135
		public string ExpeditionCharacter;

		// Token: 0x04001410 RID: 5136
		public List<string> Items;

		// Token: 0x04001411 RID: 5137
		public List<int> LocalVariablesInts;

		// Token: 0x04001412 RID: 5138
		public List<LocalizedString> Terms;
	}
}

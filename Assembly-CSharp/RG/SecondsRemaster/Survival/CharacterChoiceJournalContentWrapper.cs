using System;
using System.Collections.Generic;
using I2.Loc;

namespace RG.SecondsRemaster.Survival
{
	// Token: 0x020002E6 RID: 742
	[Serializable]
	public class CharacterChoiceJournalContentWrapper : JournalContentWrapper
	{
		// Token: 0x040013B0 RID: 5040
		public List<string> Characters;

		// Token: 0x040013B1 RID: 5041
		public LocalizedString CallToActionTerm;
	}
}

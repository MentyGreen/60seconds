using System;
using I2.Loc;
using RG.Parsecs.Survival;

namespace RG.SecondsRemaster.Survival
{
	// Token: 0x02000306 RID: 774
	[Serializable]
	public class TextIconJournalContentWrapper : JournalContentWrapper
	{
		// Token: 0x0400141D RID: 5149
		public string Sign;

		// Token: 0x0400141E RID: 5150
		public LocalizedString IconTerm;

		// Token: 0x0400141F RID: 5151
		public EventContentData.ETextIconContentType IconType;

		// Token: 0x04001420 RID: 5152
		public int Amount;
	}
}

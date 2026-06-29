using System;
using I2.Loc;

namespace RG.SecondsRemaster.Survival
{
	// Token: 0x020002EF RID: 751
	[Serializable]
	public class GoalJournalContentWrapper : JournalContentWrapper
	{
		// Token: 0x040013DD RID: 5085
		public LocalizedString Term;

		// Token: 0x040013DE RID: 5086
		public bool IsAchieved;

		// Token: 0x040013DF RID: 5087
		public bool IsFailed;

		// Token: 0x040013E0 RID: 5088
		public int CheckmarkIndex;
	}
}

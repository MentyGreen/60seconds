using System;
using System.Collections.Generic;
using I2.Loc;

namespace RG.SecondsRemaster.Scavenge
{
	// Token: 0x020002CD RID: 717
	[Serializable]
	public class ScavengeTutorialState
	{
		// Token: 0x040012FC RID: 4860
		public ScavengeTutorialDriver.ETutorialStage State;

		// Token: 0x040012FD RID: 4861
		public List<LocalizedString> Texts;

		// Token: 0x040012FE RID: 4862
		public LocalizedString Goal;

		// Token: 0x040012FF RID: 4863
		public List<LocalizedString> Success;

		// Token: 0x04001300 RID: 4864
		public List<LocalizedString> Fail;
	}
}

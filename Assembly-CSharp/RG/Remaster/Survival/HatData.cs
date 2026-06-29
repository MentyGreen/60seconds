using System;
using System.Collections.Generic;
using RG.Parsecs.EventEditor;
using RG.Parsecs.Survival;

namespace RG.Remaster.Survival
{
	// Token: 0x0200022E RID: 558
	[Serializable]
	public class HatData
	{
		// Token: 0x04000E6B RID: 3691
		public GlobalBoolVariable IsUnlockedVariable;

		// Token: 0x04000E6C RID: 3692
		public CharacterStatus IsWornStatus;

		// Token: 0x04000E6D RID: 3693
		public List<Character> AllowedCharacters = new List<Character>();

		// Token: 0x04000E6E RID: 3694
		public List<Mission> DisallowedMissions = new List<Mission>();
	}
}

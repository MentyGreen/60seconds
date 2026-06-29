using System;
using RG.Parsecs.EventEditor;
using UnityEngine;

namespace RG.Remaster.Survival
{
	// Token: 0x02000238 RID: 568
	[Serializable]
	public class SkinData
	{
		// Token: 0x04000EA6 RID: 3750
		public GlobalBoolVariable IsUnlockedVariable;

		// Token: 0x04000EA7 RID: 3751
		[Tooltip("All the variables in this list ,ust also evaluate to true for the skin to be usable")]
		public GlobalBoolVariable[] AdditionalRequirements;

		// Token: 0x04000EA8 RID: 3752
		public SkinId SkinId;
	}
}

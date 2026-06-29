using System;

namespace DunGen
{
	// Token: 0x020001E0 RID: 480
	public enum GenerationStatus
	{
		// Token: 0x04000CFA RID: 3322
		NotStarted,
		// Token: 0x04000CFB RID: 3323
		PreProcessing,
		// Token: 0x04000CFC RID: 3324
		MainPath,
		// Token: 0x04000CFD RID: 3325
		Branching,
		// Token: 0x04000CFE RID: 3326
		PostProcessing,
		// Token: 0x04000CFF RID: 3327
		Complete,
		// Token: 0x04000D00 RID: 3328
		Failed
	}
}

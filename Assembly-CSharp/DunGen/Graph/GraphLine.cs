using System;
using System.Collections.Generic;

namespace DunGen.Graph
{
	// Token: 0x020001FC RID: 508
	[Serializable]
	public class GraphLine
	{
		// Token: 0x0600142B RID: 5163 RVA: 0x0005A505 File Offset: 0x00058705
		public GraphLine(DungeonFlow graph)
		{
			this.Graph = graph;
		}

		// Token: 0x04000D45 RID: 3397
		public DungeonFlow Graph;

		// Token: 0x04000D46 RID: 3398
		public List<DungeonArchetype> DungeonArchetypes = new List<DungeonArchetype>();

		// Token: 0x04000D47 RID: 3399
		public float Position;

		// Token: 0x04000D48 RID: 3400
		public float Length;

		// Token: 0x04000D49 RID: 3401
		public List<KeyLockPlacement> Keys = new List<KeyLockPlacement>();

		// Token: 0x04000D4A RID: 3402
		public List<KeyLockPlacement> Locks = new List<KeyLockPlacement>();
	}
}

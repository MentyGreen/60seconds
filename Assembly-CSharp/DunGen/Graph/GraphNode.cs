using System;
using System.Collections.Generic;

namespace DunGen.Graph
{
	// Token: 0x020001FE RID: 510
	[Serializable]
	public class GraphNode
	{
		// Token: 0x0600142C RID: 5164 RVA: 0x0005A535 File Offset: 0x00058735
		public GraphNode(DungeonFlow graph)
		{
			this.Graph = graph;
		}

		// Token: 0x04000D4F RID: 3407
		public DungeonFlow Graph;

		// Token: 0x04000D50 RID: 3408
		public List<TileSet> TileSets = new List<TileSet>();

		// Token: 0x04000D51 RID: 3409
		public NodeType NodeType;

		// Token: 0x04000D52 RID: 3410
		public float Position;

		// Token: 0x04000D53 RID: 3411
		public string Label;

		// Token: 0x04000D54 RID: 3412
		public List<KeyLockPlacement> Keys = new List<KeyLockPlacement>();

		// Token: 0x04000D55 RID: 3413
		public List<KeyLockPlacement> Locks = new List<KeyLockPlacement>();

		// Token: 0x04000D56 RID: 3414
		public NodeLockPlacement LockPlacement;
	}
}

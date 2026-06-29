using System;
using System.Collections.Generic;
using UnityEngine;

namespace DunGen
{
	// Token: 0x020001D9 RID: 473
	[Serializable]
	public sealed class DungeonArchetype : ScriptableObject
	{
		// Token: 0x04000CCD RID: 3277
		public List<TileSet> TileSets = new List<TileSet>();

		// Token: 0x04000CCE RID: 3278
		public IntRange BranchingDepth = new IntRange(2, 4);

		// Token: 0x04000CCF RID: 3279
		public IntRange BranchCount = new IntRange(0, 2);
	}
}

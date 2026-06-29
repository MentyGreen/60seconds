using System;
using System.Collections.Generic;
using UnityEngine;

namespace DunGen
{
	// Token: 0x020001F4 RID: 500
	[Serializable]
	public sealed class TileSet : ScriptableObject
	{
		// Token: 0x04000D34 RID: 3380
		public GameObjectChanceTable TileWeights = new GameObjectChanceTable();

		// Token: 0x04000D35 RID: 3381
		public List<LockedDoorwayAssociation> LockPrefabs = new List<LockedDoorwayAssociation>();
	}
}

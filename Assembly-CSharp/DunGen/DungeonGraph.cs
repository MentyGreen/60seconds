using System;
using System.Collections.Generic;

namespace DunGen
{
	// Token: 0x020001E2 RID: 482
	public class DungeonGraph
	{
		// Token: 0x060013BA RID: 5050 RVA: 0x00058DE0 File Offset: 0x00056FE0
		public DungeonGraph(Dungeon dungeon)
		{
			Dictionary<Tile, DungeonGraphNode> dictionary = new Dictionary<Tile, DungeonGraphNode>();
			foreach (Tile tile in dungeon.AllTiles)
			{
				DungeonGraphNode dungeonGraphNode = new DungeonGraphNode(tile);
				dictionary[tile] = dungeonGraphNode;
				this.Nodes.Add(dungeonGraphNode);
			}
			foreach (DoorwayConnection doorwayConnection in dungeon.Connections)
			{
				DungeonGraphConnection item = new DungeonGraphConnection(dictionary[doorwayConnection.A.Tile], dictionary[doorwayConnection.B.Tile], doorwayConnection.A, doorwayConnection.B);
				this.Connections.Add(item);
			}
		}

		// Token: 0x04000D05 RID: 3333
		public readonly List<DungeonGraphNode> Nodes = new List<DungeonGraphNode>();

		// Token: 0x04000D06 RID: 3334
		public readonly List<DungeonGraphConnection> Connections = new List<DungeonGraphConnection>();
	}
}

using System;
using System.Collections.Generic;

namespace DunGen
{
	// Token: 0x020001E4 RID: 484
	public sealed class DungeonGraphNode : DungeonGraphObject
	{
		// Token: 0x170003AD RID: 941
		// (get) Token: 0x060013C4 RID: 5060 RVA: 0x00058F5B File Offset: 0x0005715B
		// (set) Token: 0x060013C5 RID: 5061 RVA: 0x00058F63 File Offset: 0x00057163
		public Tile Tile { get; private set; }

		// Token: 0x060013C6 RID: 5062 RVA: 0x00058F6C File Offset: 0x0005716C
		public DungeonGraphNode(Tile tile)
		{
			this.Tile = tile;
		}

		// Token: 0x060013C7 RID: 5063 RVA: 0x00058F86 File Offset: 0x00057186
		internal void AddConnection(DungeonGraphConnection connection)
		{
			this.Connections.Add(connection);
		}

		// Token: 0x04000D0B RID: 3339
		public List<DungeonGraphConnection> Connections = new List<DungeonGraphConnection>();
	}
}

using System;

namespace DunGen
{
	// Token: 0x020001E3 RID: 483
	public sealed class DungeonGraphConnection : DungeonGraphObject
	{
		// Token: 0x170003A9 RID: 937
		// (get) Token: 0x060013BB RID: 5051 RVA: 0x00058EE4 File Offset: 0x000570E4
		// (set) Token: 0x060013BC RID: 5052 RVA: 0x00058EEC File Offset: 0x000570EC
		public DungeonGraphNode A { get; private set; }

		// Token: 0x170003AA RID: 938
		// (get) Token: 0x060013BD RID: 5053 RVA: 0x00058EF5 File Offset: 0x000570F5
		// (set) Token: 0x060013BE RID: 5054 RVA: 0x00058EFD File Offset: 0x000570FD
		public DungeonGraphNode B { get; private set; }

		// Token: 0x170003AB RID: 939
		// (get) Token: 0x060013BF RID: 5055 RVA: 0x00058F06 File Offset: 0x00057106
		// (set) Token: 0x060013C0 RID: 5056 RVA: 0x00058F0E File Offset: 0x0005710E
		public Doorway DoorwayA { get; private set; }

		// Token: 0x170003AC RID: 940
		// (get) Token: 0x060013C1 RID: 5057 RVA: 0x00058F17 File Offset: 0x00057117
		// (set) Token: 0x060013C2 RID: 5058 RVA: 0x00058F1F File Offset: 0x0005711F
		public Doorway DoorwayB { get; private set; }

		// Token: 0x060013C3 RID: 5059 RVA: 0x00058F28 File Offset: 0x00057128
		public DungeonGraphConnection(DungeonGraphNode a, DungeonGraphNode b, Doorway doorwayA, Doorway doorwayB)
		{
			this.A = a;
			this.B = b;
			this.DoorwayA = doorwayA;
			this.DoorwayB = doorwayB;
			a.AddConnection(this);
			b.AddConnection(this);
		}
	}
}

using System;

namespace DunGen
{
	// Token: 0x020001D5 RID: 469
	public sealed class DoorwayConnection
	{
		// Token: 0x17000392 RID: 914
		// (get) Token: 0x06001352 RID: 4946 RVA: 0x00056399 File Offset: 0x00054599
		// (set) Token: 0x06001353 RID: 4947 RVA: 0x000563A1 File Offset: 0x000545A1
		public Doorway A { get; private set; }

		// Token: 0x17000393 RID: 915
		// (get) Token: 0x06001354 RID: 4948 RVA: 0x000563AA File Offset: 0x000545AA
		// (set) Token: 0x06001355 RID: 4949 RVA: 0x000563B2 File Offset: 0x000545B2
		public Doorway B { get; private set; }

		// Token: 0x06001356 RID: 4950 RVA: 0x000563BB File Offset: 0x000545BB
		public DoorwayConnection(Doorway a, Doorway b)
		{
			this.A = a;
			this.B = b;
		}
	}
}

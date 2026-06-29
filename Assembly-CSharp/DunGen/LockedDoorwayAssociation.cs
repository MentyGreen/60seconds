using System;

namespace DunGen
{
	// Token: 0x020001EC RID: 492
	[Serializable]
	public sealed class LockedDoorwayAssociation
	{
		// Token: 0x04000D17 RID: 3351
		public DoorwaySocketType SocketGroup;

		// Token: 0x04000D18 RID: 3352
		public GameObjectChanceTable LockPrefabs = new GameObjectChanceTable();
	}
}

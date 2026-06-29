using System;
using UnityEngine;

namespace DunGen.Graph
{
	// Token: 0x020001FF RID: 511
	[Serializable]
	public abstract class FlowGraphObjectReference
	{
		// Token: 0x170003C3 RID: 963
		// (get) Token: 0x0600142D RID: 5165 RVA: 0x0005A565 File Offset: 0x00058765
		public DungeonFlow Flow
		{
			get
			{
				return this.flow;
			}
		}

		// Token: 0x04000D57 RID: 3415
		[SerializeField]
		protected DungeonFlow flow;

		// Token: 0x04000D58 RID: 3416
		[SerializeField]
		protected int index;
	}
}

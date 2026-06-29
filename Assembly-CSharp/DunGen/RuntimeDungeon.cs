using System;
using UnityEngine;

namespace DunGen
{
	// Token: 0x020001F1 RID: 497
	[AddComponentMenu("DunGen/Runtime Dungeon")]
	public class RuntimeDungeon : MonoBehaviour
	{
		// Token: 0x060013EA RID: 5098 RVA: 0x0005957E File Offset: 0x0005777E
		protected virtual void Start()
		{
			if (this.Generator.Root == null)
			{
				this.Generator.Root = base.gameObject;
			}
			if (this.GenerateOnStart)
			{
				this.Generator.Generate();
			}
		}

		// Token: 0x04000D21 RID: 3361
		public DungeonGenerator Generator = new DungeonGenerator();

		// Token: 0x04000D22 RID: 3362
		public bool GenerateOnStart = true;
	}
}

using System;
using System.Collections.Generic;
using UnityEngine;

namespace DunGen
{
	// Token: 0x020001EB RID: 491
	[AddComponentMenu("DunGen/Random Props/Local Prop Set")]
	public class LocalPropSet : RandomProp
	{
		// Token: 0x060013DB RID: 5083 RVA: 0x0005920C File Offset: 0x0005740C
		public override void Process(Random randomStream, Tile tile)
		{
			GameObjectChanceTable gameObjectChanceTable = this.Props.Clone();
			int num = this.PropCount.GetRandom(randomStream);
			num = Mathf.Clamp(num, 0, this.Props.Weights.Count);
			List<GameObject> list = new List<GameObject>(num);
			for (int i = 0; i < num; i++)
			{
				list.Add(gameObjectChanceTable.GetRandom(randomStream, tile.Placement.IsOnMainPath, tile.Placement.NormalizedDepth, true));
			}
			foreach (GameObjectChance gameObjectChance in this.Props.Weights)
			{
				if (!list.Contains(gameObjectChance.Value))
				{
					Object.DestroyImmediate(gameObjectChance.Value);
				}
			}
		}

		// Token: 0x04000D15 RID: 3349
		public GameObjectChanceTable Props = new GameObjectChanceTable();

		// Token: 0x04000D16 RID: 3350
		public IntRange PropCount = new IntRange(1, 1);
	}
}

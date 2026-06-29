using System;
using UnityEngine;

namespace DunGen
{
	// Token: 0x020001EF RID: 495
	[AddComponentMenu("DunGen/Random Props/Random Prefab")]
	public class RandomPrefab : RandomProp
	{
		// Token: 0x060013E6 RID: 5094 RVA: 0x000594F8 File Offset: 0x000576F8
		public override void Process(Random randomStream, Tile tile)
		{
			if (this.Props.Weights.Count <= 0)
			{
				return;
			}
			GameObject gameObject = Object.Instantiate<GameObject>(this.Props.GetRandom(randomStream, tile.Placement.IsOnMainPath, tile.Placement.NormalizedDepth, true));
			gameObject.transform.parent = base.transform;
			gameObject.transform.localPosition = Vector3.zero;
		}

		// Token: 0x04000D20 RID: 3360
		public GameObjectChanceTable Props = new GameObjectChanceTable();
	}
}

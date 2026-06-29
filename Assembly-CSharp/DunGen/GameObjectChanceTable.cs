using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DunGen
{
	// Token: 0x020001DD RID: 477
	[Serializable]
	public class GameObjectChanceTable
	{
		// Token: 0x06001396 RID: 5014 RVA: 0x00058880 File Offset: 0x00056A80
		public GameObjectChanceTable Clone()
		{
			GameObjectChanceTable gameObjectChanceTable = new GameObjectChanceTable();
			foreach (GameObjectChance gameObjectChance in this.Weights)
			{
				gameObjectChanceTable.Weights.Add(new GameObjectChance(gameObjectChance.Value, gameObjectChance.MainPathWeight, gameObjectChance.BranchPathWeight)
				{
					UseDepthScale = gameObjectChance.UseDepthScale,
					DepthWeightScale = gameObjectChance.DepthWeightScale
				});
			}
			return gameObjectChanceTable;
		}

		// Token: 0x06001397 RID: 5015 RVA: 0x00058910 File Offset: 0x00056B10
		public GameObject GetRandom(Random random, bool isOnMainPath, float normalizedDepth, bool removeFromTable = false)
		{
			float num = (from x in this.Weights
			select x.GetWeight(isOnMainPath, normalizedDepth)).Sum();
			float num2 = (float)(random.NextDouble() * (double)num);
			foreach (GameObjectChance gameObjectChance in this.Weights)
			{
				float weight = gameObjectChance.GetWeight(isOnMainPath, normalizedDepth);
				if (num2 < weight)
				{
					if (removeFromTable)
					{
						this.Weights.Remove(gameObjectChance);
					}
					return gameObjectChance.Value;
				}
				num2 -= weight;
			}
			return null;
		}

		// Token: 0x06001398 RID: 5016 RVA: 0x000589DC File Offset: 0x00056BDC
		public static GameObject GetCombinedRandom(Random random, bool isOnMainPath, float normalizedDepth, params GameObjectChanceTable[] tables)
		{
			Func<GameObjectChance, float> <>9__1;
			float num = tables.SelectMany(delegate(GameObjectChanceTable x)
			{
				IEnumerable<GameObjectChance> weights = x.Weights;
				Func<GameObjectChance, float> selector;
				if ((selector = <>9__1) == null)
				{
					selector = (<>9__1 = ((GameObjectChance y) => y.GetWeight(isOnMainPath, normalizedDepth)));
				}
				return weights.Select(selector);
			}).Sum();
			float num2 = (float)(random.NextDouble() * (double)num);
			foreach (GameObjectChance gameObjectChance in tables.SelectMany((GameObjectChanceTable x) => x.Weights))
			{
				float weight = gameObjectChance.GetWeight(isOnMainPath, normalizedDepth);
				if (num2 < weight)
				{
					return gameObjectChance.Value;
				}
				num2 -= weight;
			}
			return null;
		}

		// Token: 0x04000CEC RID: 3308
		public List<GameObjectChance> Weights = new List<GameObjectChance>();
	}
}

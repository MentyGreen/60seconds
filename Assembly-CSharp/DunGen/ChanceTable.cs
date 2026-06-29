using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DunGen
{
	// Token: 0x020001F6 RID: 502
	public class ChanceTable<T>
	{
		// Token: 0x06001411 RID: 5137 RVA: 0x00059AB1 File Offset: 0x00057CB1
		public void Add(T value, float weight)
		{
			this.Weights.Add(new Chance<T>(value, weight));
		}

		// Token: 0x06001412 RID: 5138 RVA: 0x00059AC8 File Offset: 0x00057CC8
		public void Remove(T value)
		{
			for (int i = 0; i < this.Weights.Count; i++)
			{
				if (this.Weights[i].Value.Equals(value))
				{
					this.Weights.RemoveAt(i);
				}
			}
		}

		// Token: 0x06001413 RID: 5139 RVA: 0x00059B1C File Offset: 0x00057D1C
		public T GetRandom(Random random)
		{
			float num = (from x in this.Weights
			select x.Weight).Sum();
			float num2 = (float)(random.NextDouble() * (double)num);
			foreach (Chance<T> chance in this.Weights)
			{
				if (num2 < chance.Weight)
				{
					return chance.Value;
				}
				num2 -= chance.Weight;
			}
			return default(T);
		}

		// Token: 0x06001414 RID: 5140 RVA: 0x00059BCC File Offset: 0x00057DCC
		public static TVal GetCombinedRandom<TVal, TChance>(Random random, params ChanceTable<TVal>[] tables)
		{
			float num = tables.SelectMany((ChanceTable<TVal> x) => from y in x.Weights
			select y.Weight).Sum();
			float num2 = (float)(random.NextDouble() * (double)num);
			foreach (Chance<TVal> chance in tables.SelectMany((ChanceTable<TVal> x) => x.Weights))
			{
				if (num2 < chance.Weight)
				{
					return chance.Value;
				}
				num2 -= chance.Weight;
			}
			return default(TVal);
		}

		// Token: 0x04000D38 RID: 3384
		[SerializeField]
		public List<Chance<T>> Weights = new List<Chance<T>>();
	}
}

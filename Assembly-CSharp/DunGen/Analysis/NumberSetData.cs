using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DunGen.Analysis
{
	// Token: 0x02000204 RID: 516
	public sealed class NumberSetData
	{
		// Token: 0x170003D5 RID: 981
		// (get) Token: 0x0600145D RID: 5213 RVA: 0x0005AF5B File Offset: 0x0005915B
		// (set) Token: 0x0600145E RID: 5214 RVA: 0x0005AF63 File Offset: 0x00059163
		public float Min { get; private set; }

		// Token: 0x170003D6 RID: 982
		// (get) Token: 0x0600145F RID: 5215 RVA: 0x0005AF6C File Offset: 0x0005916C
		// (set) Token: 0x06001460 RID: 5216 RVA: 0x0005AF74 File Offset: 0x00059174
		public float Max { get; private set; }

		// Token: 0x170003D7 RID: 983
		// (get) Token: 0x06001461 RID: 5217 RVA: 0x0005AF7D File Offset: 0x0005917D
		// (set) Token: 0x06001462 RID: 5218 RVA: 0x0005AF85 File Offset: 0x00059185
		public float Average { get; private set; }

		// Token: 0x170003D8 RID: 984
		// (get) Token: 0x06001463 RID: 5219 RVA: 0x0005AF8E File Offset: 0x0005918E
		// (set) Token: 0x06001464 RID: 5220 RVA: 0x0005AF96 File Offset: 0x00059196
		public float StandardDeviation { get; private set; }

		// Token: 0x06001465 RID: 5221 RVA: 0x0005AFA0 File Offset: 0x000591A0
		public NumberSetData(IEnumerable<float> values)
		{
			this.Min = values.Min();
			this.Max = values.Max();
			this.Average = values.Sum() / (float)values.Count<float>();
			float[] array = new float[values.Count<float>()];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = Mathf.Pow(values.ElementAt(i) - this.Average, 2f);
			}
			this.StandardDeviation = Mathf.Sqrt(array.Sum() / (float)array.Length);
		}

		// Token: 0x06001466 RID: 5222 RVA: 0x0005B02C File Offset: 0x0005922C
		public override string ToString()
		{
			return string.Format("[ Min: {0}, Max: {1}, Average: {2}, Standard Deviation: {3} ]", new object[]
			{
				this.Min,
				this.Max,
				this.Average,
				this.StandardDeviation
			});
		}
	}
}

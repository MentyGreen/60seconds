using System;

namespace DunGen
{
	// Token: 0x020001F5 RID: 501
	[Serializable]
	public class Chance<T>
	{
		// Token: 0x0600140E RID: 5134 RVA: 0x00059A6C File Offset: 0x00057C6C
		public Chance() : this(default(T), 1f)
		{
		}

		// Token: 0x0600140F RID: 5135 RVA: 0x00059A8D File Offset: 0x00057C8D
		public Chance(T value) : this(value, 1f)
		{
		}

		// Token: 0x06001410 RID: 5136 RVA: 0x00059A9B File Offset: 0x00057C9B
		public Chance(T value, float weight)
		{
			this.Value = value;
			this.Weight = weight;
		}

		// Token: 0x04000D36 RID: 3382
		public T Value;

		// Token: 0x04000D37 RID: 3383
		public float Weight;
	}
}

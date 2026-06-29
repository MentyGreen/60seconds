using System;

namespace DunGen
{
	// Token: 0x020001F8 RID: 504
	[Serializable]
	public class IntRange
	{
		// Token: 0x0600141C RID: 5148 RVA: 0x00059FC0 File Offset: 0x000581C0
		public IntRange()
		{
		}

		// Token: 0x0600141D RID: 5149 RVA: 0x00059FC8 File Offset: 0x000581C8
		public IntRange(int min, int max)
		{
			this.Min = min;
			this.Max = max;
		}

		// Token: 0x0600141E RID: 5150 RVA: 0x00059FE0 File Offset: 0x000581E0
		public int GetRandom(Random random)
		{
			if (this.Min > this.Max)
			{
				int min = this.Min;
				this.Max = this.Min;
				this.Min = min;
			}
			return random.Next(this.Min, this.Max + 1);
		}

		// Token: 0x04000D3A RID: 3386
		public int Min;

		// Token: 0x04000D3B RID: 3387
		public int Max;
	}
}

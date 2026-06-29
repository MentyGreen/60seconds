using System;

namespace DunGen
{
	// Token: 0x020001F9 RID: 505
	public class FloatRange
	{
		// Token: 0x0600141F RID: 5151 RVA: 0x0005A029 File Offset: 0x00058229
		public FloatRange()
		{
		}

		// Token: 0x06001420 RID: 5152 RVA: 0x0005A031 File Offset: 0x00058231
		public FloatRange(float min, float max)
		{
			this.Min = min;
			this.Max = max;
		}

		// Token: 0x06001421 RID: 5153 RVA: 0x0005A048 File Offset: 0x00058248
		public float GetRandom(Random random)
		{
			if (this.Min > this.Max)
			{
				float min = this.Min;
				this.Max = this.Min;
				this.Min = min;
			}
			float num = this.Max - this.Min;
			return this.Min + (float)random.NextDouble() * num;
		}

		// Token: 0x04000D3C RID: 3388
		public float Min;

		// Token: 0x04000D3D RID: 3389
		public float Max;
	}
}

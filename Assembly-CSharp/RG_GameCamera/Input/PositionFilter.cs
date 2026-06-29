using System;
using UnityEngine;

namespace RG_GameCamera.Input
{
	// Token: 0x0200019E RID: 414
	public class PositionFilter
	{
		// Token: 0x0600120E RID: 4622 RVA: 0x0004D9E5 File Offset: 0x0004BBE5
		public PositionFilter(int samplesNum, float coef)
		{
			this.value = default(Vector3);
			this.weightCoef = coef;
			this.numSamples = samplesNum;
			this.samples = new Vector3[samplesNum];
		}

		// Token: 0x0600120F RID: 4623 RVA: 0x0004DA14 File Offset: 0x0004BC14
		public void AddSample(Vector3 sample)
		{
			Vector3 a = default(Vector3);
			float num = 0f;
			float num2 = 1f;
			float num3 = 1f;
			Vector3 vector = this.samples[0];
			this.samples[0] = sample;
			for (int i = 1; i < this.numSamples; i++)
			{
				num += num3;
				a += this.samples[i - 1] * num3;
				Vector3 vector2 = this.samples[i];
				this.samples[i] = vector;
				vector = vector2;
				num3 = num2 * this.weightCoef;
				num2 = num3;
			}
			this.value = a / num;
		}

		// Token: 0x06001210 RID: 4624 RVA: 0x0004DAC1 File Offset: 0x0004BCC1
		public Vector3 GetValue()
		{
			return this.value;
		}

		// Token: 0x06001211 RID: 4625 RVA: 0x0004DAC9 File Offset: 0x0004BCC9
		public Vector3[] GetSamples()
		{
			return this.samples;
		}

		// Token: 0x06001212 RID: 4626 RVA: 0x0004DAD4 File Offset: 0x0004BCD4
		public void Reset(Vector3 resetVal)
		{
			for (int i = 0; i < this.numSamples; i++)
			{
				this.samples[i] = resetVal;
			}
		}

		// Token: 0x04000BA8 RID: 2984
		private Vector3 value;

		// Token: 0x04000BA9 RID: 2985
		private readonly Vector3[] samples;

		// Token: 0x04000BAA RID: 2986
		private readonly float weightCoef;

		// Token: 0x04000BAB RID: 2987
		private readonly int numSamples;
	}
}

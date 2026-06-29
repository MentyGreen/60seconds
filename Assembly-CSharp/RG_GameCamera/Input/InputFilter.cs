using System;
using UnityEngine;

namespace RG_GameCamera.Input
{
	// Token: 0x02000198 RID: 408
	public class InputFilter
	{
		// Token: 0x060011F6 RID: 4598 RVA: 0x0004D46E File Offset: 0x0004B66E
		public InputFilter(int samplesNum, float coef)
		{
			this.value = default(Vector2);
			this.weightCoef = coef;
			this.numSamples = samplesNum;
			this.samples = new Vector2[samplesNum];
		}

		// Token: 0x060011F7 RID: 4599 RVA: 0x0004D49C File Offset: 0x0004B69C
		public void AddSample(Vector2 sample)
		{
			Vector2 a = default(Vector2);
			float num = 0f;
			float num2 = 1f;
			float num3 = 1f;
			Vector2 vector = this.samples[0];
			this.samples[0] = sample;
			for (int i = 1; i < this.numSamples; i++)
			{
				num += num3;
				a += this.samples[i - 1] * num3;
				Vector2 vector2 = this.samples[i];
				this.samples[i] = vector;
				vector = vector2;
				num3 = num2 * this.weightCoef;
				num2 = num3;
			}
			this.value = a / num;
		}

		// Token: 0x060011F8 RID: 4600 RVA: 0x0004D549 File Offset: 0x0004B749
		public Vector2 GetValue()
		{
			return this.value;
		}

		// Token: 0x060011F9 RID: 4601 RVA: 0x0004D551 File Offset: 0x0004B751
		public Vector2[] GetSamples()
		{
			return this.samples;
		}

		// Token: 0x060011FA RID: 4602 RVA: 0x0004D55C File Offset: 0x0004B75C
		public void Reset(Vector2 resetVal)
		{
			for (int i = 0; i < this.numSamples; i++)
			{
				this.samples[i] = resetVal;
			}
		}

		// Token: 0x04000B88 RID: 2952
		private Vector2 value;

		// Token: 0x04000B89 RID: 2953
		private readonly Vector2[] samples;

		// Token: 0x04000B8A RID: 2954
		private readonly float weightCoef;

		// Token: 0x04000B8B RID: 2955
		private readonly int numSamples;
	}
}

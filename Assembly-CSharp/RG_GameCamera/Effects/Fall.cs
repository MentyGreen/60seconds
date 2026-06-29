using System;
using RG_GameCamera.Utils;
using UnityEngine;

namespace RG_GameCamera.Effects
{
	// Token: 0x020001B6 RID: 438
	public class Fall : Effect
	{
		// Token: 0x060012A5 RID: 4773 RVA: 0x0005068F File Offset: 0x0004E88F
		public override void Init()
		{
			base.Init();
			this.spring = new Spring();
			this.spring.Setup(this.Mass, this.Distance, this.Strength, this.Damping);
		}

		// Token: 0x060012A6 RID: 4774 RVA: 0x000506C5 File Offset: 0x0004E8C5
		public override void OnPlay()
		{
			this.frameCounter = (float)this.ForceFrames;
			this.spring.Setup(this.Mass, this.Distance, this.Strength, this.Damping);
		}

		// Token: 0x060012A7 RID: 4775 RVA: 0x000506F8 File Offset: 0x0004E8F8
		public override void OnUpdate()
		{
			if (this.frameCounter > 0f)
			{
				this.spring.AddForce(this.Force);
				this.frameCounter -= 1f;
			}
			float num = this.spring.Calculate(Time.deltaTime);
			float d = 1f;
			Effect.FadeState fadeState = this.fadeState;
			if (fadeState != Effect.FadeState.FadeIn)
			{
				if (fadeState == Effect.FadeState.FadeOut)
				{
					d = Interpolation.LerpS2(num, 0f, this.fadeOutNormalized);
				}
			}
			else
			{
				d = Interpolation.LerpS3(0f, num, 1f - this.fadeInNormalized);
			}
			float num2 = Mathf.Clamp01(this.ImpactVelocity / 10f);
			this.DistanceMax = num2 * 2f;
			if (num > this.DistanceMax)
			{
				num = this.DistanceMax;
			}
			this.unityCamera.transform.position += Vector3.up * num * d * -1f;
		}

		// Token: 0x04000C4A RID: 3146
		public float Mass;

		// Token: 0x04000C4B RID: 3147
		public float Distance;

		// Token: 0x04000C4C RID: 3148
		public float Strength;

		// Token: 0x04000C4D RID: 3149
		public float Damping;

		// Token: 0x04000C4E RID: 3150
		public float Force;

		// Token: 0x04000C4F RID: 3151
		public int ForceFrames;

		// Token: 0x04000C50 RID: 3152
		public float ImpactVelocity;

		// Token: 0x04000C51 RID: 3153
		private Spring spring;

		// Token: 0x04000C52 RID: 3154
		private float frameCounter;

		// Token: 0x04000C53 RID: 3155
		private float DistanceMax;
	}
}

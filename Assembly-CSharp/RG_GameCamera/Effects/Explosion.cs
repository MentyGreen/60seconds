using System;
using RG_GameCamera.Utils;
using UnityEngine;

namespace RG_GameCamera.Effects
{
	// Token: 0x020001B5 RID: 437
	public class Explosion : Effect
	{
		// Token: 0x060012A1 RID: 4769 RVA: 0x000504BA File Offset: 0x0004E6BA
		public override void Init()
		{
			base.Init();
			this.posSpring = new Spring();
		}

		// Token: 0x060012A2 RID: 4770 RVA: 0x000504D0 File Offset: 0x0004E6D0
		public override void OnPlay()
		{
			this.posSpring.Setup(this.Mass, this.Distance, this.Strength, this.Damping);
			this.v0 = (this.position - this.unityCamera.transform.position).normalized;
			this.diff = Vector3.zero;
		}

		// Token: 0x060012A3 RID: 4771 RVA: 0x00050534 File Offset: 0x0004E734
		public override void OnUpdate()
		{
			Vector3 eulerAngles = this.unityCamera.transform.rotation.eulerAngles;
			this.size = this.Size;
			float num = this.posSpring.Calculate(Time.deltaTime);
			float d = 1f;
			Effect.FadeState fadeState = this.fadeState;
			if (fadeState != Effect.FadeState.FadeIn)
			{
				if (fadeState == Effect.FadeState.FadeOut)
				{
					d = Interpolation.LerpS2(num, 0f, this.fadeOutNormalized);
					this.size = Interpolation.LerpS2(this.Size, 0f, this.fadeOutNormalized);
				}
			}
			else
			{
				d = Interpolation.LerpS3(0f, num, 1f - this.fadeInNormalized);
				this.size = Interpolation.LerpS3(0f, this.Size, 1f - this.fadeInNormalized);
			}
			Vector3 b = SmoothRandom.GetVector3(this.Speed) * this.size;
			Vector3 euler = eulerAngles - this.diff + b;
			this.diff = b;
			this.unityCamera.transform.rotation = Quaternion.Euler(euler);
			this.unityCamera.transform.position += this.v0 * num * d;
		}

		// Token: 0x04000C3F RID: 3135
		public float Mass;

		// Token: 0x04000C40 RID: 3136
		public float Distance;

		// Token: 0x04000C41 RID: 3137
		public float Strength;

		// Token: 0x04000C42 RID: 3138
		public float Damping;

		// Token: 0x04000C43 RID: 3139
		public Vector3 position;

		// Token: 0x04000C44 RID: 3140
		public float Size = 1f;

		// Token: 0x04000C45 RID: 3141
		public float Speed = 10f;

		// Token: 0x04000C46 RID: 3142
		private float size;

		// Token: 0x04000C47 RID: 3143
		private Spring posSpring;

		// Token: 0x04000C48 RID: 3144
		private Vector3 v0;

		// Token: 0x04000C49 RID: 3145
		private Vector3 diff;
	}
}

using System;
using RG_GameCamera.Utils;
using UnityEngine;

namespace RG_GameCamera.Effects
{
	// Token: 0x020001BE RID: 446
	public class Stomp : Effect
	{
		// Token: 0x060012CA RID: 4810 RVA: 0x000516FE File Offset: 0x0004F8FE
		public override void Init()
		{
			base.Init();
			this.spring = new Spring();
		}

		// Token: 0x060012CB RID: 4811 RVA: 0x00051711 File Offset: 0x0004F911
		public override void OnPlay()
		{
			this.spring.Setup(this.Mass, this.Distance, this.Strength, this.Damping);
		}

		// Token: 0x060012CC RID: 4812 RVA: 0x00051738 File Offset: 0x0004F938
		public override void OnUpdate()
		{
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
			this.unityCamera.transform.position += Vector3.up * num * d;
		}

		// Token: 0x04000C76 RID: 3190
		public float Mass;

		// Token: 0x04000C77 RID: 3191
		public float Distance;

		// Token: 0x04000C78 RID: 3192
		public float Strength;

		// Token: 0x04000C79 RID: 3193
		public float Damping;

		// Token: 0x04000C7A RID: 3194
		private Spring spring;
	}
}

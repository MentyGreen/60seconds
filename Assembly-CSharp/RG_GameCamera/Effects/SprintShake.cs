using System;
using RG_GameCamera.Utils;
using UnityEngine;

namespace RG_GameCamera.Effects
{
	// Token: 0x020001BD RID: 445
	public class SprintShake : Effect
	{
		// Token: 0x060012C7 RID: 4807 RVA: 0x000515F6 File Offset: 0x0004F7F6
		public override void OnPlay()
		{
			this.diff = Vector2.zero;
		}

		// Token: 0x060012C8 RID: 4808 RVA: 0x00051608 File Offset: 0x0004F808
		public override void OnUpdate()
		{
			Vector3 eulerAngles = this.unityCamera.transform.rotation.eulerAngles;
			switch (this.fadeState)
			{
			case Effect.FadeState.FadeIn:
				this.size = Interpolation.LerpS3(0f, this.Size, 1f - this.fadeInNormalized);
				break;
			case Effect.FadeState.Full:
				this.size = this.Size;
				break;
			case Effect.FadeState.FadeOut:
				this.size = Interpolation.LerpS2(this.Size, 0f, this.fadeOutNormalized);
				break;
			}
			Vector3 b = SmoothRandom.GetVector3(this.Speed) * this.size;
			Vector3 euler = eulerAngles - this.diff + b;
			this.diff = b;
			this.unityCamera.transform.rotation = Quaternion.Euler(euler);
		}

		// Token: 0x04000C72 RID: 3186
		public float Size = 1f;

		// Token: 0x04000C73 RID: 3187
		public float Speed = 10f;

		// Token: 0x04000C74 RID: 3188
		private Vector3 diff;

		// Token: 0x04000C75 RID: 3189
		private float size;
	}
}

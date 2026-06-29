using System;
using RG_GameCamera.Utils;
using UnityEngine;

namespace RG_GameCamera.Effects
{
	// Token: 0x020001B1 RID: 433
	public class Earthquake : Effect
	{
		// Token: 0x06001288 RID: 4744 RVA: 0x0004FFDF File Offset: 0x0004E1DF
		public override void OnPlay()
		{
			this.diff = Vector2.zero;
		}

		// Token: 0x06001289 RID: 4745 RVA: 0x0004FFF4 File Offset: 0x0004E1F4
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

		// Token: 0x04000C26 RID: 3110
		public float Size = 1f;

		// Token: 0x04000C27 RID: 3111
		public float Speed = 10f;

		// Token: 0x04000C28 RID: 3112
		private Vector3 diff;

		// Token: 0x04000C29 RID: 3113
		private float size;
	}
}

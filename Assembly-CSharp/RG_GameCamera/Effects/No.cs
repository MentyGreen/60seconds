using System;
using RG_GameCamera.Utils;
using UnityEngine;

namespace RG_GameCamera.Effects
{
	// Token: 0x020001B8 RID: 440
	public class No : Effect
	{
		// Token: 0x060012AC RID: 4780 RVA: 0x000508EE File Offset: 0x0004EAEE
		public override void OnPlay()
		{
			this.diff = 0f;
			this.origPos = this.unityCamera.transform.position;
		}

		// Token: 0x060012AD RID: 4781 RVA: 0x00050914 File Offset: 0x0004EB14
		public override void OnUpdate()
		{
			Vector3 localEulerAngles = this.unityCamera.transform.localEulerAngles;
			switch (this.fadeState)
			{
			case Effect.FadeState.FadeIn:
				this.size = Interpolation.LerpS3(0f, this.Angle, 1f - this.fadeInNormalized);
				this.currPos = this.origPos;
				break;
			case Effect.FadeState.Full:
				this.size = this.Angle;
				this.currPos = this.origPos;
				break;
			case Effect.FadeState.FadeOut:
				this.size = Interpolation.LerpS2(this.Angle, 0f, this.fadeOutNormalized);
				this.currPos = Interpolation.LerpS2(this.origPos, this.unityCamera.transform.position, this.fadeOutNormalized);
				break;
			}
			float num = Mathf.Sin(this.timeout * this.Speed) * this.size;
			float y = localEulerAngles.y - this.diff + num;
			this.diff = num;
			Vector3 euler = localEulerAngles;
			euler.y = y;
			this.unityCamera.transform.position = this.currPos;
			this.unityCamera.transform.localRotation = Quaternion.Euler(euler);
		}

		// Token: 0x04000C58 RID: 3160
		public float Angle = 1f;

		// Token: 0x04000C59 RID: 3161
		public float Speed = 10f;

		// Token: 0x04000C5A RID: 3162
		private float diff;

		// Token: 0x04000C5B RID: 3163
		private float size;

		// Token: 0x04000C5C RID: 3164
		private Vector3 origPos;

		// Token: 0x04000C5D RID: 3165
		private Vector3 currPos;
	}
}

using System;
using RG_GameCamera.Utils;
using UnityEngine;

namespace RG_GameCamera.Effects
{
	// Token: 0x020001BF RID: 447
	public class Yes : Effect
	{
		// Token: 0x060012CE RID: 4814 RVA: 0x000517CC File Offset: 0x0004F9CC
		public override void OnPlay()
		{
			this.diff = 0f;
			this.origPos = this.unityCamera.transform.position;
		}

		// Token: 0x060012CF RID: 4815 RVA: 0x000517F0 File Offset: 0x0004F9F0
		public override void OnUpdate()
		{
			Vector3 eulerAngles = this.unityCamera.transform.rotation.eulerAngles;
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
			float x = eulerAngles.x - this.diff + num;
			this.diff = num;
			Vector3 euler = eulerAngles;
			euler.x = x;
			this.unityCamera.transform.position = this.currPos;
			this.unityCamera.transform.rotation = Quaternion.Euler(euler);
		}

		// Token: 0x04000C7B RID: 3195
		public float Angle = 1f;

		// Token: 0x04000C7C RID: 3196
		public float Speed = 10f;

		// Token: 0x04000C7D RID: 3197
		private float diff;

		// Token: 0x04000C7E RID: 3198
		private float size;

		// Token: 0x04000C7F RID: 3199
		private Vector3 origPos;

		// Token: 0x04000C80 RID: 3200
		private Vector3 currPos;
	}
}

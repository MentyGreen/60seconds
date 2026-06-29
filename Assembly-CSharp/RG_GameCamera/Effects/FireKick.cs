using System;
using RG_GameCamera.Utils;
using UnityEngine;

namespace RG_GameCamera.Effects
{
	// Token: 0x020001B7 RID: 439
	public class FireKick : Effect
	{
		// Token: 0x060012A9 RID: 4777 RVA: 0x000507F5 File Offset: 0x0004E9F5
		public override void OnPlay()
		{
			this.diff = 0f;
			this.KickTime = Mathf.Clamp(this.KickTime, 0f, this.Length);
		}

		// Token: 0x060012AA RID: 4778 RVA: 0x00050820 File Offset: 0x0004EA20
		public override void OnUpdate()
		{
			Vector3 eulerAngles = this.unityCamera.transform.rotation.eulerAngles;
			float num;
			if (this.timeout < this.KickTime)
			{
				float t = this.timeout / this.KickTime;
				num = Interpolation.LerpS2(0f, this.KickAngle, t);
			}
			else
			{
				float t2 = (this.timeout - this.KickTime) / (this.Length - this.KickTime);
				num = Interpolation.LerpS(this.KickAngle, 0f, t2);
			}
			num = -num;
			float x = eulerAngles.x - this.diff + num;
			this.diff = num;
			Vector3 euler = eulerAngles;
			euler.x = x;
			this.unityCamera.transform.rotation = Quaternion.Euler(euler);
		}

		// Token: 0x04000C54 RID: 3156
		public float KickTime;

		// Token: 0x04000C55 RID: 3157
		public float KickAngle;

		// Token: 0x04000C56 RID: 3158
		private float diff;

		// Token: 0x04000C57 RID: 3159
		private float kickTimeout;
	}
}

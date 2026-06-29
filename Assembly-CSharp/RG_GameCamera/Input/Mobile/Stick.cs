using System;
using UnityEngine;

namespace RG_GameCamera.Input.Mobile
{
	// Token: 0x020001AA RID: 426
	public class Stick : BaseControl
	{
		// Token: 0x17000389 RID: 905
		// (get) Token: 0x06001263 RID: 4707 RVA: 0x0004EFD3 File Offset: 0x0004D1D3
		public override ControlType Type
		{
			get
			{
				return ControlType.Stick;
			}
		}

		// Token: 0x06001264 RID: 4708 RVA: 0x0004EFD8 File Offset: 0x0004D1D8
		public override void GameUpdate()
		{
			this.DetectTouches();
			this.input = Vector2.zero;
			if (this.TouchIndex != -1)
			{
				SimTouch touch = this.touchProcessor.GetTouch(this.TouchIndex);
				if (touch.Status != TouchStatus.Invalid)
				{
					Vector2 a = touch.Position - touch.StartPosition;
					float magnitude = a.magnitude;
					if (magnitude > Mathf.Epsilon)
					{
						float num = this.CircleSize / 2f - this.HitSize / 2f;
						float d = magnitude / num;
						Vector2 vector = a * d;
						vector.x = Mathf.Clamp(vector.x, -num, num);
						vector.y = Mathf.Clamp(vector.y, -num, num);
						this.input = vector / num;
						return;
					}
				}
				else
				{
					this.TouchIndex = -1;
				}
			}
		}

		// Token: 0x06001265 RID: 4709 RVA: 0x0004F0AC File Offset: 0x0004D2AC
		public override Vector2 GetInputAxis()
		{
			return this.input;
		}

		// Token: 0x06001266 RID: 4710 RVA: 0x0004F0B4 File Offset: 0x0004D2B4
		public override void Draw()
		{
			if (this.HideGUI)
			{
				return;
			}
			if (this.TouchIndex != -1)
			{
				SimTouch touch = this.touchProcessor.GetTouch(this.TouchIndex);
				float num = -this.CircleSize * 0.5f;
				if (this.MoveControlsCircle)
				{
					GUI.DrawTexture(new Rect(num + touch.StartPosition.x, num + ((float)Screen.height - touch.StartPosition.y), this.CircleSize, this.CircleSize), this.MoveControlsCircle, ScaleMode.StretchToFill);
				}
				if (this.MoveControlsHit)
				{
					num = -this.HitSize * 0.5f;
					GUI.DrawTexture(new Rect(num + touch.Position.x, num + ((float)Screen.height - touch.Position.y), this.HitSize, this.HitSize), this.MoveControlsHit, ScaleMode.StretchToFill);
				}
			}
		}

		// Token: 0x04000BF1 RID: 3057
		public float CircleSize = 160f;

		// Token: 0x04000BF2 RID: 3058
		public float HitSize = 32f;

		// Token: 0x04000BF3 RID: 3059
		public Texture2D MoveControlsCircle;

		// Token: 0x04000BF4 RID: 3060
		public Texture2D MoveControlsHit;

		// Token: 0x04000BF5 RID: 3061
		private Rect rect;

		// Token: 0x04000BF6 RID: 3062
		private bool pressed;

		// Token: 0x04000BF7 RID: 3063
		private Vector2 input;
	}
}

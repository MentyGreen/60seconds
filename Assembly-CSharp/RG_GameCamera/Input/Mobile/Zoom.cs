using System;
using UnityEngine;

namespace RG_GameCamera.Input.Mobile
{
	// Token: 0x020001AC RID: 428
	public class Zoom : BaseControl
	{
		// Token: 0x1700038A RID: 906
		// (get) Token: 0x0600126F RID: 4719 RVA: 0x0004F27F File Offset: 0x0004D47F
		public override ControlType Type
		{
			get
			{
				return ControlType.Zoom;
			}
		}

		// Token: 0x06001270 RID: 4720 RVA: 0x0004F282 File Offset: 0x0004D482
		public override void Init(TouchProcessor processor)
		{
			base.Init(processor);
			this.rect = default(Rect);
			this.UpdateRect();
			this.ZoomDelta = 0f;
			this.Side = ControlSide.Arbitrary;
			this.Priority = 2;
		}

		// Token: 0x06001271 RID: 4721 RVA: 0x0004F2B6 File Offset: 0x0004D4B6
		public bool ContainPoint(Vector2 point)
		{
			point.y = (float)Screen.height - point.y;
			return this.rect.Contains(point);
		}

		// Token: 0x06001272 RID: 4722 RVA: 0x0004F2D8 File Offset: 0x0004D4D8
		public bool IsZooming()
		{
			return this.zooming;
		}

		// Token: 0x06001273 RID: 4723 RVA: 0x0004F2E0 File Offset: 0x0004D4E0
		public override bool AbortUpdateOtherControls()
		{
			return this.zooming;
		}

		// Token: 0x06001274 RID: 4724 RVA: 0x0004F2E8 File Offset: 0x0004D4E8
		protected override void DetectTouches()
		{
			int activeTouchCount = this.touchProcessor.GetActiveTouchCount();
			bool flag = false;
			if (activeTouchCount > 1)
			{
				if (!this.zooming)
				{
					for (int i = 0; i < activeTouchCount; i++)
					{
						SimTouch touch = this.touchProcessor.GetTouch(i);
						if (this.ContainPoint(touch.StartPosition) && touch.Status != TouchStatus.Invalid)
						{
							if (this.TouchIndex == -1)
							{
								this.TouchIndex = i;
							}
							else if (this.TouchIndexAux == -1)
							{
								this.TouchIndexAux = i;
							}
						}
					}
					this.zooming = (this.TouchIndex != -1 && this.TouchIndexAux != -1);
				}
				else
				{
					SimTouch touch2 = this.touchProcessor.GetTouch(this.TouchIndex);
					SimTouch touch3 = this.touchProcessor.GetTouch(this.TouchIndexAux);
					if (touch2.Status != TouchStatus.Invalid && touch3.Status != TouchStatus.Invalid)
					{
						float magnitude = (touch2.Position - touch3.Position).magnitude;
						if (this.lastDistance > 0f)
						{
							this.ZoomDelta = (this.lastDistance - magnitude) * 0.01f * this.Sensitivity;
							if (this.ReverseZoom)
							{
								this.ZoomDelta = -this.ZoomDelta;
							}
						}
						else
						{
							this.ZoomDelta = 0f;
						}
						this.lastDistance = magnitude;
					}
				}
			}
			else
			{
				flag = true;
			}
			if (flag)
			{
				this.lastDistance = 0f;
				this.zooming = false;
				this.TouchIndex = -1;
				this.TouchIndexAux = -1;
				this.ZoomDelta = 0f;
			}
		}

		// Token: 0x06001275 RID: 4725 RVA: 0x0004F461 File Offset: 0x0004D661
		public override void GameUpdate()
		{
			this.DetectTouches();
		}

		// Token: 0x06001276 RID: 4726 RVA: 0x0004F469 File Offset: 0x0004D669
		public override void Draw()
		{
			this.UpdateRect();
			if (this.HideGUI)
			{
				return;
			}
			GUI.Box(this.rect, "Zoom area");
		}

		// Token: 0x06001277 RID: 4727 RVA: 0x0004F48C File Offset: 0x0004D68C
		public void UpdateRect()
		{
			this.rect.x = this.Position.x * (float)Screen.width;
			this.rect.y = this.Position.y * (float)Screen.height;
			this.rect.width = this.Size.x * (float)Screen.width;
			this.rect.height = this.Size.y * (float)Screen.height;
		}

		// Token: 0x04000BF9 RID: 3065
		public float ZoomDelta;

		// Token: 0x04000BFA RID: 3066
		public float Sensitivity = 1f;

		// Token: 0x04000BFB RID: 3067
		public bool ReverseZoom;

		// Token: 0x04000BFC RID: 3068
		private Rect rect;

		// Token: 0x04000BFD RID: 3069
		private bool zooming;

		// Token: 0x04000BFE RID: 3070
		private float lastDistance;
	}
}

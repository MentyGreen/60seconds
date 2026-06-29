using System;
using UnityEngine;

namespace RG_GameCamera.Input.Mobile
{
	// Token: 0x020001A5 RID: 421
	public class Button : BaseControl
	{
		// Token: 0x17000386 RID: 902
		// (get) Token: 0x06001233 RID: 4659 RVA: 0x0004E34A File Offset: 0x0004C54A
		public override ControlType Type
		{
			get
			{
				return ControlType.Button;
			}
		}

		// Token: 0x06001234 RID: 4660 RVA: 0x0004E34D File Offset: 0x0004C54D
		public override void Init(TouchProcessor processor)
		{
			base.Init(processor);
			this.rect = default(Rect);
			this.UpdateRect();
			this.State = Button.ButtonState.None;
			this.Side = ControlSide.Arbitrary;
		}

		// Token: 0x06001235 RID: 4661 RVA: 0x0004E376 File Offset: 0x0004C576
		public bool ContainPoint(Vector2 point)
		{
			point.y = (float)Screen.height - point.y;
			return this.rect.Contains(point);
		}

		// Token: 0x06001236 RID: 4662 RVA: 0x0004E398 File Offset: 0x0004C598
		public void Press()
		{
			if (this.Toggle)
			{
				this.pressed = !this.pressed;
				return;
			}
			this.pressed = true;
		}

		// Token: 0x06001237 RID: 4663 RVA: 0x0004E3B9 File Offset: 0x0004C5B9
		public bool IsPressed()
		{
			return this.pressed;
		}

		// Token: 0x06001238 RID: 4664 RVA: 0x0004E3C1 File Offset: 0x0004C5C1
		public void Reset()
		{
			this.pressed = false;
		}

		// Token: 0x06001239 RID: 4665 RVA: 0x0004E3CC File Offset: 0x0004C5CC
		private void CheckForMove(Vector2 touch)
		{
			if (this.InvalidateOnDrag && (touch - this.startTouch).sqrMagnitude > 10f)
			{
				this.State = Button.ButtonState.None;
				this.pressed = false;
			}
		}

		// Token: 0x0600123A RID: 4666 RVA: 0x0004E40C File Offset: 0x0004C60C
		protected override void DetectTouches()
		{
			int activeTouchCount = this.touchProcessor.GetActiveTouchCount();
			bool flag = false;
			if (activeTouchCount > 0)
			{
				for (int i = 0; i < activeTouchCount; i++)
				{
					SimTouch touch = this.touchProcessor.GetTouch(i);
					if (this.ContainPoint(touch.StartPosition) && touch.Status == TouchStatus.Start)
					{
						this.Press();
						this.State = Button.ButtonState.Begin;
						this.startTouch = touch.StartPosition;
						this.TouchIndex = i;
					}
					if (this.TouchIndex == i)
					{
						switch (touch.Status)
						{
						case TouchStatus.Invalid:
							flag = true;
							break;
						case TouchStatus.Stationary:
						case TouchStatus.Moving:
							this.State = Button.ButtonState.Pressed;
							this.CheckForMove(touch.Position);
							break;
						case TouchStatus.End:
							this.State = Button.ButtonState.End;
							this.CheckForMove(touch.Position);
							flag = true;
							break;
						}
					}
				}
			}
			else
			{
				flag = true;
			}
			if (flag)
			{
				if (this.TouchIndex == -1)
				{
					this.State = Button.ButtonState.None;
				}
				else if (!this.HoldDrag && this.IsHoldDrag())
				{
					this.State = Button.ButtonState.None;
				}
				this.TouchIndex = -1;
				if (!this.Toggle)
				{
					this.Reset();
				}
			}
		}

		// Token: 0x0600123B RID: 4667 RVA: 0x0004E525 File Offset: 0x0004C725
		public override void GameUpdate()
		{
			this.DetectTouches();
		}

		// Token: 0x0600123C RID: 4668 RVA: 0x0004E530 File Offset: 0x0004C730
		public override void Draw()
		{
			this.UpdateRect();
			if (this.HideGUI)
			{
				return;
			}
			Texture2D texture2D = this.pressed ? this.TexturePressed : this.TextureDefault;
			if (texture2D)
			{
				GUI.DrawTexture(this.rect, texture2D);
			}
		}

		// Token: 0x0600123D RID: 4669 RVA: 0x0004E578 File Offset: 0x0004C778
		public void UpdateRect()
		{
			this.rect.x = this.Position.x * (float)Screen.width;
			this.rect.y = this.Position.y * (float)Screen.height;
			this.rect.width = this.Size.x * (float)Screen.width;
			this.rect.height = this.Size.y * (float)Screen.height;
		}

		// Token: 0x0600123E RID: 4670 RVA: 0x0004E5F9 File Offset: 0x0004C7F9
		private bool IsHoldDrag()
		{
			return this.TouchIndex != -1 && this.touchProcessor.GetTouch(this.TouchIndex).PressTime > this.HoldTimeout;
		}

		// Token: 0x04000BCB RID: 3019
		public bool Toggle;

		// Token: 0x04000BCC RID: 3020
		public bool HoldDrag;

		// Token: 0x04000BCD RID: 3021
		public bool InvalidateOnDrag;

		// Token: 0x04000BCE RID: 3022
		public float HoldTimeout = 0.3f;

		// Token: 0x04000BCF RID: 3023
		public Texture2D TextureDefault;

		// Token: 0x04000BD0 RID: 3024
		public Texture2D TexturePressed;

		// Token: 0x04000BD1 RID: 3025
		public Button.ButtonState State;

		// Token: 0x04000BD2 RID: 3026
		private Rect rect;

		// Token: 0x04000BD3 RID: 3027
		private bool pressed;

		// Token: 0x04000BD4 RID: 3028
		private Vector2 startTouch;

		// Token: 0x020003E0 RID: 992
		public enum ButtonState
		{
			// Token: 0x04001807 RID: 6151
			Pressed,
			// Token: 0x04001808 RID: 6152
			Begin,
			// Token: 0x04001809 RID: 6153
			End,
			// Token: 0x0400180A RID: 6154
			None
		}
	}
}

using System;
using UnityEngine;

namespace RG_GameCamera.Input.Mobile
{
	// Token: 0x020001A6 RID: 422
	public class CameraPanel : BaseControl
	{
		// Token: 0x17000387 RID: 903
		// (get) Token: 0x06001240 RID: 4672 RVA: 0x0004E637 File Offset: 0x0004C837
		public override ControlType Type
		{
			get
			{
				return ControlType.CameraPanel;
			}
		}

		// Token: 0x06001241 RID: 4673 RVA: 0x0004E63A File Offset: 0x0004C83A
		public override void Init(TouchProcessor processor)
		{
			base.Init(processor);
			this.cameraFilter = new InputFilter(10, 0.5f);
			this.rect = default(Rect);
			this.UpdateRect();
		}

		// Token: 0x06001242 RID: 4674 RVA: 0x0004E668 File Offset: 0x0004C868
		public override void GameUpdate()
		{
			this.DetectTouches();
			this.input = Vector2.zero;
			if (this.TouchIndex != -1)
			{
				SimTouch touch = this.touchProcessor.GetTouch(this.TouchIndex);
				if (touch.Status != TouchStatus.Invalid)
				{
					Vector2 deltaPosition = touch.DeltaPosition;
					deltaPosition.x *= this.Sensitivity.x;
					deltaPosition.y *= this.Sensitivity.y;
					this.cameraFilter.AddSample(deltaPosition);
					this.input = this.cameraFilter.GetValue();
					return;
				}
				this.TouchIndex = -1;
			}
		}

		// Token: 0x06001243 RID: 4675 RVA: 0x0004E701 File Offset: 0x0004C901
		public override Vector2 GetInputAxis()
		{
			return this.input;
		}

		// Token: 0x06001244 RID: 4676 RVA: 0x0004E70C File Offset: 0x0004C90C
		public void UpdateRect()
		{
			this.rect.x = this.Position.x * (float)Screen.width;
			this.rect.y = this.Position.y * (float)Screen.height;
			this.rect.width = this.Position.x * (float)Screen.width;
			this.rect.height = this.Position.y * (float)Screen.height;
		}

		// Token: 0x06001245 RID: 4677 RVA: 0x0004E78D File Offset: 0x0004C98D
		public override void Draw()
		{
			bool hideGUI = this.HideGUI;
		}

		// Token: 0x04000BD5 RID: 3029
		public Vector2 Sensitivity = new Vector2(0.5f, 0.5f);

		// Token: 0x04000BD6 RID: 3030
		private Rect rect;

		// Token: 0x04000BD7 RID: 3031
		private InputFilter cameraFilter;

		// Token: 0x04000BD8 RID: 3032
		private Vector2 input;
	}
}

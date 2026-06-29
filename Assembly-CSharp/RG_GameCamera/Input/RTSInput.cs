using System;
using UnityEngine;

namespace RG_GameCamera.Input
{
	// Token: 0x020001A0 RID: 416
	[Serializable]
	public class RTSInput : GameInput
	{
		// Token: 0x1700037C RID: 892
		// (get) Token: 0x06001216 RID: 4630 RVA: 0x0004DD6B File Offset: 0x0004BF6B
		public override InputPreset PresetType
		{
			get
			{
				return InputPreset.RTS;
			}
		}

		// Token: 0x06001217 RID: 4631 RVA: 0x0004DD70 File Offset: 0x0004BF70
		public override void UpdateInput(Input[] inputs)
		{
			InputManager instance = InputManager.Instance;
			Vector2 vector = Input.mousePosition;
			if (instance.MobileInput && Input.touchCount > 0)
			{
				vector = Input.GetTouch(0).position;
			}
			if (!instance.MobileInput && instance.FilterInput)
			{
				this.mouseFilter.AddSample(Input.mousePosition);
				vector = this.mouseFilter.GetValue();
			}
			if (InputWrapper.GetButton("RotatePan"))
			{
				bool flag = this.MouseRotateDirection == MousePanRotDirection.Horizontal_L || this.MouseRotateDirection == MousePanRotDirection.Horizontal_R;
				float num = Mathf.Sign((float)this.MouseRotateDirection);
				float axis = InputWrapper.GetAxis("Mouse X");
				float axis2 = InputWrapper.GetAxis("Mouse Y");
				base.SetInput(inputs, InputType.Rotate, new Vector2(flag ? (axis * num) : (axis2 * num), 0f));
			}
			float axis3 = InputWrapper.GetAxis("Mouse ScrollWheel");
			if (Mathf.Abs(axis3) > Mathf.Epsilon)
			{
				base.SetInput(inputs, InputType.Zoom, axis3);
			}
			else
			{
				float num2 = InputWrapper.GetAxis("ZoomIn") * 0.1f;
				float num3 = InputWrapper.GetAxis("ZoomOut") * 0.1f;
				float num4 = num2 - num3;
				if (Mathf.Abs(num4) > Mathf.Epsilon)
				{
					base.SetInput(inputs, InputType.Zoom, num4);
				}
			}
			bool flag2 = false;
			if (InputManager.Instance.MobileInput)
			{
				float zoom = InputWrapper.GetZoom("Zoom");
				if (Mathf.Abs(zoom) > Mathf.Epsilon)
				{
					base.SetInput(inputs, InputType.Zoom, zoom);
					flag2 = true;
				}
			}
			if (!flag2)
			{
				if (InputWrapper.GetButton("Pan"))
				{
					this.panTimeout += Time.deltaTime;
					if (this.panTimeout > 0.01f)
					{
						base.SetInput(inputs, InputType.Pan, vector);
					}
				}
				else
				{
					this.panTimeout = 0f;
				}
			}
			Vector2 vector2 = new Vector2(InputWrapper.GetAxis("Horizontal_R"), InputWrapper.GetAxis("Vertical_R"));
			if (vector2.sqrMagnitude > Mathf.Epsilon)
			{
				base.SetInput(inputs, InputType.Rotate, vector2);
			}
			else
			{
				float num5 = (InputWrapper.GetButton("RotateLeft") ? 1f : 0f) - (InputWrapper.GetButton("RotateRight") ? 1f : 0f);
				if (Mathf.Abs(num5) > Mathf.Epsilon)
				{
					base.SetInput(inputs, InputType.Rotate, new Vector2(num5, 0f));
				}
			}
			base.SetInput(inputs, InputType.Reset, Input.GetKey(KeyCode.R));
			this.doubleClickTimeout += Time.deltaTime;
			float axis4 = InputWrapper.GetAxis("Horizontal");
			float axis5 = InputWrapper.GetAxis("Vertical");
			Vector2 sample = new Vector2(axis4, axis5);
			this.padFilter.AddSample(sample);
			base.SetInput(inputs, InputType.Move, this.padFilter.GetValue());
			Vector3 vector3;
			if (InputWrapper.GetButtonUp("Waypoint") && GameInput.FindWaypointPosition(Input.mousePosition, out vector3))
			{
				base.SetInput(inputs, InputType.WaypointPos, vector3);
			}
		}

		// Token: 0x04000BAD RID: 2989
		public MousePanRotDirection MouseRotateDirection;

		// Token: 0x04000BAE RID: 2990
		private float panTimeout;
	}
}

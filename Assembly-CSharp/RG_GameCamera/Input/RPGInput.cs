using System;
using UnityEngine;

namespace RG_GameCamera.Input
{
	// Token: 0x0200019F RID: 415
	[Serializable]
	public class RPGInput : GameInput
	{
		// Token: 0x1700037B RID: 891
		// (get) Token: 0x06001213 RID: 4627 RVA: 0x0004DAFF File Offset: 0x0004BCFF
		public override InputPreset PresetType
		{
			get
			{
				return InputPreset.RPG;
			}
		}

		// Token: 0x06001214 RID: 4628 RVA: 0x0004DB04 File Offset: 0x0004BD04
		public override void UpdateInput(Input[] inputs)
		{
			this.mouseFilter.AddSample(Input.mousePosition);
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
			if (InputManager.Instance.MobileInput)
			{
				float zoom = InputWrapper.GetZoom("Zoom");
				if (Mathf.Abs(zoom) > Mathf.Epsilon)
				{
					base.SetInput(inputs, InputType.Zoom, zoom);
				}
			}
			Vector2 vector = new Vector2(InputWrapper.GetAxis("Horizontal_R"), InputWrapper.GetAxis("Vertical_R"));
			if (vector.sqrMagnitude > Mathf.Epsilon)
			{
				base.SetInput(inputs, InputType.Rotate, vector);
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
			Vector3 vector2;
			if (InputWrapper.GetButton("Waypoint") && GameInput.FindWaypointPosition(Input.mousePosition, out vector2))
			{
				base.SetInput(inputs, InputType.WaypointPos, vector2);
			}
		}

		// Token: 0x04000BAC RID: 2988
		public MousePanRotDirection MouseRotateDirection;
	}
}

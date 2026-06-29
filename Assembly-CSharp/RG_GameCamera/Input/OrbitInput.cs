using System;
using UnityEngine;

namespace RG_GameCamera.Input
{
	// Token: 0x0200019D RID: 413
	[Serializable]
	public class OrbitInput : GameInput
	{
		// Token: 0x1700037A RID: 890
		// (get) Token: 0x0600120B RID: 4619 RVA: 0x0004D7FF File Offset: 0x0004B9FF
		public override InputPreset PresetType
		{
			get
			{
				return InputPreset.Orbit;
			}
		}

		// Token: 0x0600120C RID: 4620 RVA: 0x0004D804 File Offset: 0x0004BA04
		public override void UpdateInput(Input[] inputs)
		{
			this.mouseFilter.AddSample(Input.mousePosition);
			Vector2 vector = InputManager.Instance.FilterInput ? this.mouseFilter.GetValue() : new Vector2(Input.mousePosition.x, Input.mousePosition.y);
			if (InputWrapper.GetButton("Pan"))
			{
				base.SetInput(inputs, InputType.Pan, vector);
			}
			float axis = InputWrapper.GetAxis("Mouse ScrollWheel");
			if (Mathf.Abs(axis) > Mathf.Epsilon)
			{
				base.SetInput(inputs, InputType.Zoom, axis);
			}
			if (InputManager.Instance.MobileInput)
			{
				float zoom = InputWrapper.GetZoom("Zoom");
				if (Mathf.Abs(zoom) > Mathf.Epsilon)
				{
					base.SetInput(inputs, InputType.Zoom, zoom);
				}
			}
			Vector2 vector2 = new Vector2(InputWrapper.GetAxis("Horizontal_R"), InputWrapper.GetAxis("Vertical_R"));
			if (vector2.sqrMagnitude > Mathf.Epsilon)
			{
				base.SetInput(inputs, InputType.Rotate, new Vector2(vector2.x, vector2.y));
			}
			if (Input.GetMouseButton(1))
			{
				base.SetInput(inputs, InputType.Rotate, new Vector2(InputWrapper.GetAxis("Mouse X"), InputWrapper.GetAxis("Mouse Y")));
			}
			base.SetInput(inputs, InputType.Reset, Input.GetKey(KeyCode.R));
			this.doubleClickTimeout += Time.deltaTime;
			if (Input.GetMouseButtonDown(2))
			{
				if (this.doubleClickTimeout < InputManager.DoubleClickTimeout)
				{
					base.SetInput(inputs, InputType.Reset, true);
				}
				this.doubleClickTimeout = 0f;
			}
			float axis2 = InputWrapper.GetAxis("Horizontal");
			float axis3 = InputWrapper.GetAxis("Vertical");
			Vector2 sample = new Vector2(axis2, axis3);
			this.padFilter.AddSample(sample);
			base.SetInput(inputs, InputType.Move, this.padFilter.GetValue());
		}
	}
}

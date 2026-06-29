using System;
using RG_GameCamera.Utils;
using UnityEngine;

namespace RG_GameCamera.Input
{
	// Token: 0x02000194 RID: 404
	[Serializable]
	public class FPSInput : GameInput
	{
		// Token: 0x17000377 RID: 887
		// (get) Token: 0x060011EE RID: 4590 RVA: 0x0004D1CA File Offset: 0x0004B3CA
		public override InputPreset PresetType
		{
			get
			{
				return InputPreset.FPS;
			}
		}

		// Token: 0x060011EF RID: 4591 RVA: 0x0004D1D0 File Offset: 0x0004B3D0
		public override void UpdateInput(Input[] inputs)
		{
			Vector2 vector = new Vector2(InputWrapper.GetAxis("Horizontal_R"), InputWrapper.GetAxis("Vertical_R"));
			base.SetInput(inputs, InputType.Rotate, vector);
			if (vector.sqrMagnitude < Mathf.Epsilon && CursorLocking.IsLocked)
			{
				base.SetInput(inputs, InputType.Rotate, new Vector2(InputWrapper.GetAxis("Mouse X"), InputWrapper.GetAxis("Mouse Y")));
			}
			float axis = InputWrapper.GetAxis("Horizontal");
			float axis2 = InputWrapper.GetAxis("Vertical");
			Vector2 sample = new Vector2(axis, axis2);
			this.padFilter.AddSample(sample);
			base.SetInput(inputs, InputType.Move, this.padFilter.GetValue());
			float axis3 = InputWrapper.GetAxis("Aim");
			float axis4 = InputWrapper.GetAxis("Fire");
			bool button = InputWrapper.GetButton("Aim");
			bool button2 = InputWrapper.GetButton("Fire");
			base.SetInput(inputs, InputType.Aim, this.AlwaysAim || axis3 > 0.5f || button);
			base.SetInput(inputs, InputType.Fire, axis4 > 0.5f || button2);
			base.SetInput(inputs, InputType.Crouch, Input.GetKey(KeyCode.C) || InputWrapper.GetButton("Crouch"));
			base.SetInput(inputs, InputType.Walk, InputWrapper.GetButton("Walk"));
			base.SetInput(inputs, InputType.Jump, InputWrapper.GetButton("Jump"));
			base.SetInput(inputs, InputType.Sprint, InputWrapper.GetButton("Sprint"));
		}

		// Token: 0x04000B78 RID: 2936
		public bool AlwaysAim;
	}
}

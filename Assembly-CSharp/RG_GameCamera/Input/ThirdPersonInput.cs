using System;
using RG.SecondsRemaster;
using UnityEngine;

namespace RG_GameCamera.Input
{
	// Token: 0x020001A1 RID: 417
	[Serializable]
	public class ThirdPersonInput : GameInput
	{
		// Token: 0x1700037D RID: 893
		// (get) Token: 0x06001219 RID: 4633 RVA: 0x0004E076 File Offset: 0x0004C276
		// (set) Token: 0x0600121A RID: 4634 RVA: 0x0004E07E File Offset: 0x0004C27E
		public bool Paused
		{
			get
			{
				return this._paused;
			}
			set
			{
				this._paused = value;
			}
		}

		// Token: 0x1700037E RID: 894
		// (get) Token: 0x0600121B RID: 4635 RVA: 0x0004E087 File Offset: 0x0004C287
		// (set) Token: 0x0600121C RID: 4636 RVA: 0x0004E08F File Offset: 0x0004C28F
		public float GlobalInputMultiplier
		{
			get
			{
				return this._globalInputMultiplier;
			}
			set
			{
				this._globalInputMultiplier = value;
			}
		}

		// Token: 0x1700037F RID: 895
		// (get) Token: 0x0600121D RID: 4637 RVA: 0x0004E098 File Offset: 0x0004C298
		// (set) Token: 0x0600121E RID: 4638 RVA: 0x0004E0A0 File Offset: 0x0004C2A0
		public float GamepadInputMultiplier
		{
			get
			{
				return this._gamepadInputMultiplier;
			}
			set
			{
				this._gamepadInputMultiplier = value;
			}
		}

		// Token: 0x17000380 RID: 896
		// (get) Token: 0x0600121F RID: 4639 RVA: 0x0004E0A9 File Offset: 0x0004C2A9
		// (set) Token: 0x06001220 RID: 4640 RVA: 0x0004E0B1 File Offset: 0x0004C2B1
		public float MouseInputMultiplier
		{
			get
			{
				return this._mouseInputMultiplier;
			}
			set
			{
				this._mouseInputMultiplier = value;
			}
		}

		// Token: 0x17000381 RID: 897
		// (get) Token: 0x06001221 RID: 4641 RVA: 0x0004E0BA File Offset: 0x0004C2BA
		// (set) Token: 0x06001222 RID: 4642 RVA: 0x0004E0C2 File Offset: 0x0004C2C2
		public float KeyboardInputMultiplier
		{
			get
			{
				return this._keyboardInputMultiplier;
			}
			set
			{
				this._keyboardInputMultiplier = value;
			}
		}

		// Token: 0x17000382 RID: 898
		// (get) Token: 0x06001223 RID: 4643 RVA: 0x0004E0CB File Offset: 0x0004C2CB
		// (set) Token: 0x06001224 RID: 4644 RVA: 0x0004E0D3 File Offset: 0x0004C2D3
		public bool Gamepad
		{
			get
			{
				return this._gamepad;
			}
			set
			{
				this._gamepad = value;
			}
		}

		// Token: 0x17000383 RID: 899
		// (get) Token: 0x06001225 RID: 4645 RVA: 0x0004E0DC File Offset: 0x0004C2DC
		// (set) Token: 0x06001226 RID: 4646 RVA: 0x0004E0E4 File Offset: 0x0004C2E4
		public bool FreeRotation
		{
			get
			{
				return this._freeRotation;
			}
			set
			{
				this._freeRotation = value;
			}
		}

		// Token: 0x17000384 RID: 900
		// (get) Token: 0x06001227 RID: 4647 RVA: 0x0004E0ED File Offset: 0x0004C2ED
		public override InputPreset PresetType
		{
			get
			{
				return InputPreset.ThirdPerson;
			}
		}

		// Token: 0x06001228 RID: 4648 RVA: 0x0004E0F0 File Offset: 0x0004C2F0
		public override void UpdateInput(Input[] inputs)
		{
			if (!this._paused)
			{
				if (this._freeRotation)
				{
					base.SetInput(inputs, InputType.Rotate, new Vector2(Input.GetAxis("Mouse X") * this._mouseInputMultiplier * this._globalInputMultiplier, 0f));
					if (Settings.Data.ControlMode.ScavengeControl != EPlayerInput.MOUSE_ONLY)
					{
						float axis = InputWrapper.GetAxis("Vertical");
						Vector2 sample = new Vector2(0f, axis);
						this.padFilter.AddSample(sample);
						base.SetInput(inputs, InputType.Move, this.padFilter.GetValue());
						return;
					}
				}
				else
				{
					if (this._gamepad)
					{
						base.SetInput(inputs, InputType.Rotate, new Vector2(InputHandler.Instance.GetAxis("Rotate") * this._gamepadInputMultiplier * this._globalInputMultiplier, 0f));
						base.SetInput(inputs, InputType.Move, new Vector2(0f, Input.GetAxis("Vertical")));
						return;
					}
					InputWrapper.GetAxis("Rotate");
					Vector2 vector = new Vector2(InputWrapper.GetAxis("Rotate") * this._keyboardInputMultiplier * this._globalInputMultiplier * 2f, 0f);
					base.SetInput(inputs, InputType.Rotate, vector);
				}
			}
		}

		// Token: 0x04000BAF RID: 2991
		[SerializeField]
		private bool _freeRotation = true;

		// Token: 0x04000BB0 RID: 2992
		[SerializeField]
		private bool _gamepad;

		// Token: 0x04000BB1 RID: 2993
		[SerializeField]
		private float _gamepadInputMultiplier = 1f;

		// Token: 0x04000BB2 RID: 2994
		[SerializeField]
		private float _mouseInputMultiplier = 1f;

		// Token: 0x04000BB3 RID: 2995
		[SerializeField]
		private float _keyboardInputMultiplier = 1f;

		// Token: 0x04000BB4 RID: 2996
		[SerializeField]
		private float _globalInputMultiplier = 1f;

		// Token: 0x04000BB5 RID: 2997
		private bool _paused;
	}
}

using System;
using RG.Parsecs.EventEditor;
using UnityEngine;
using UnityEngine.UI;

namespace RG.SecondsRemaster.Menu
{
	// Token: 0x020002AD RID: 685
	public class RotationSensitivitySettingController : MonoBehaviour
	{
		// Token: 0x06001894 RID: 6292 RVA: 0x0006B8C5 File Offset: 0x00069AC5
		private void OnEnable()
		{
			if (this._controllerForSpecificControlMode)
			{
				this._controlMode = this._specificControlMode;
			}
			else
			{
				this._controlMode = (EPlayerInput)this._controlModeVariable.Value;
			}
			this.SetupSlider();
		}

		// Token: 0x06001895 RID: 6293 RVA: 0x0006B8F4 File Offset: 0x00069AF4
		private void SetupSlider()
		{
			this._blockChangingSensitivity = true;
			this.SetSliderRange();
			this._blockChangingSensitivity = false;
			switch (this._controlMode)
			{
			case EPlayerInput.KEYBOARD:
				if (this._rotationSensitivityKeyboard.Value >= this._slider.minValue)
				{
					this._slider.value = this._rotationSensitivityKeyboard.Value;
					return;
				}
				break;
			case EPlayerInput.KEYBOARD_MOUSE:
				if (this._rotationSensitivityMouse.Value >= this._slider.minValue)
				{
					this._slider.value = this._rotationSensitivityMouse.Value;
				}
				break;
			case EPlayerInput.GAMEPAD:
				if (this._rotationSensitivityGamepad.Value >= this._slider.minValue)
				{
					this._slider.value = this._rotationSensitivityGamepad.Value;
					return;
				}
				break;
			case EPlayerInput.TOUCH_ANALOGUE:
			case EPlayerInput.TOUCH_DIGITAL:
				break;
			case EPlayerInput.MOUSE_ONLY:
				if (this._rotationSensitivityMouse.Value >= this._slider.minValue)
				{
					this._slider.value = this._rotationSensitivityMouse.Value;
					return;
				}
				break;
			default:
				return;
			}
		}

		// Token: 0x06001896 RID: 6294 RVA: 0x0006B9FB File Offset: 0x00069BFB
		private void Update()
		{
			if (!this._controllerForSpecificControlMode && this._controlModeVariable.Value != (int)this._controlMode)
			{
				this._controlMode = (EPlayerInput)this._controlModeVariable.Value;
				this.SetupSlider();
			}
		}

		// Token: 0x06001897 RID: 6295 RVA: 0x0006BA30 File Offset: 0x00069C30
		public void ChangeSensitivity(float value)
		{
			if (this._blockChangingSensitivity)
			{
				return;
			}
			switch (this._controlMode)
			{
			case EPlayerInput.KEYBOARD:
				this._rotationSensitivityKeyboard.Value = value;
				return;
			case EPlayerInput.KEYBOARD_MOUSE:
				this._rotationSensitivityMouse.Value = value;
				break;
			case EPlayerInput.GAMEPAD:
				this._rotationSensitivityGamepad.Value = value;
				return;
			case EPlayerInput.TOUCH_ANALOGUE:
			case EPlayerInput.TOUCH_DIGITAL:
				break;
			case EPlayerInput.MOUSE_ONLY:
				this._rotationSensitivityMouse.Value = value;
				return;
			default:
				return;
			}
		}

		// Token: 0x06001898 RID: 6296 RVA: 0x0006BAA4 File Offset: 0x00069CA4
		private void SetSliderRange()
		{
			switch (this._controlMode)
			{
			case EPlayerInput.KEYBOARD:
				this._slider.minValue = this._keyboardSensitivityMinValue.Value;
				this._slider.maxValue = this._keyboardSensitivityMaxValue.Value;
				return;
			case EPlayerInput.KEYBOARD_MOUSE:
			case EPlayerInput.MOUSE_ONLY:
				this._slider.minValue = this._mouseKeyboardSensitivityMinValue.Value;
				this._slider.maxValue = this._mouseKeyboardSensitivityMaxValue.Value;
				return;
			case EPlayerInput.GAMEPAD:
				this._slider.minValue = this._gamepadSensitivityMinValue.Value;
				this._slider.maxValue = this._gamepadSensitivityMaxValue.Value;
				break;
			case EPlayerInput.TOUCH_ANALOGUE:
			case EPlayerInput.TOUCH_DIGITAL:
				break;
			default:
				return;
			}
		}

		// Token: 0x0400124B RID: 4683
		[SerializeField]
		private Slider _slider;

		// Token: 0x0400124C RID: 4684
		[SerializeField]
		private GlobalFloatVariable _rotationSensitivityMouse;

		// Token: 0x0400124D RID: 4685
		[SerializeField]
		private GlobalFloatVariable _rotationSensitivityGamepad;

		// Token: 0x0400124E RID: 4686
		[SerializeField]
		private GlobalFloatVariable _rotationSensitivityKeyboard;

		// Token: 0x0400124F RID: 4687
		[SerializeField]
		private GlobalIntVariable _controlModeVariable;

		// Token: 0x04001250 RID: 4688
		[SerializeField]
		private bool _controllerForSpecificControlMode;

		// Token: 0x04001251 RID: 4689
		[SerializeField]
		private EPlayerInput _specificControlMode;

		// Token: 0x04001252 RID: 4690
		[SerializeField]
		private GlobalFloatVariable _gamepadSensitivityMinValue;

		// Token: 0x04001253 RID: 4691
		[SerializeField]
		private GlobalFloatVariable _gamepadSensitivityMaxValue;

		// Token: 0x04001254 RID: 4692
		[SerializeField]
		private GlobalFloatVariable _mouseKeyboardSensitivityMinValue;

		// Token: 0x04001255 RID: 4693
		[SerializeField]
		private GlobalFloatVariable _mouseKeyboardSensitivityMaxValue;

		// Token: 0x04001256 RID: 4694
		[SerializeField]
		private GlobalFloatVariable _keyboardSensitivityMinValue;

		// Token: 0x04001257 RID: 4695
		[SerializeField]
		private GlobalFloatVariable _keyboardSensitivityMaxValue;

		// Token: 0x04001258 RID: 4696
		private EPlayerInput _controlMode;

		// Token: 0x04001259 RID: 4697
		private bool _blockChangingSensitivity;
	}
}

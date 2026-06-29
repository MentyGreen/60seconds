using System;
using RG_GameCamera.Utils;
using UnityEngine;

namespace RG_GameCamera.Input
{
	// Token: 0x0200019B RID: 411
	public class InputManager : MonoBehaviour
	{
		// Token: 0x17000379 RID: 889
		// (get) Token: 0x060011FC RID: 4604 RVA: 0x0004D58F File Offset: 0x0004B78F
		public static InputManager Instance
		{
			get
			{
				if (!InputManager.instance)
				{
					InputManager.instance = CameraInstance.CreateInstance<InputManager>("CameraInput");
				}
				return InputManager.instance;
			}
		}

		// Token: 0x060011FD RID: 4605 RVA: 0x0004D5B1 File Offset: 0x0004B7B1
		public Input GetInput(InputType type)
		{
			return this.inputs[(int)type];
		}

		// Token: 0x060011FE RID: 4606 RVA: 0x0004D5BC File Offset: 0x0004B7BC
		public T GetInput<T>(InputType type, T defaultValue)
		{
			Input input = this.inputs[(int)type];
			if (input.Valid)
			{
				return (T)((object)input.Value);
			}
			return defaultValue;
		}

		// Token: 0x060011FF RID: 4607 RVA: 0x0004D5E8 File Offset: 0x0004B7E8
		public void SetInputPreset(InputPreset preset)
		{
			if (preset == InputPreset.None)
			{
				this.currInput = null;
				this.InputPreset = InputPreset.None;
				return;
			}
			foreach (GameInput gameInput in this.GameInputs)
			{
				if (gameInput.PresetType == preset)
				{
					this.currInput = gameInput;
					this.InputPreset = preset;
					return;
				}
			}
		}

		// Token: 0x06001200 RID: 4608 RVA: 0x0004D638 File Offset: 0x0004B838
		private void Awake()
		{
			InputManager.instance = this;
			this.inputs = new Input[14];
			int num = 0;
			foreach (InputType type in (InputType[])Enum.GetValues(typeof(InputType)))
			{
				this.inputs[num++] = new Input
				{
					Type = type,
					Valid = false,
					Value = null
				};
			}
			this.GameInputs = base.gameObject.GetComponents<GameInput>();
			this.SetInputPreset(this.InputPreset);
		}

		// Token: 0x06001201 RID: 4609 RVA: 0x0004D6C4 File Offset: 0x0004B8C4
		private void Start()
		{
		}

		// Token: 0x06001202 RID: 4610 RVA: 0x0004D6C8 File Offset: 0x0004B8C8
		public void GameUpdate()
		{
			InputWrapper.Mobile = this.MobileInput;
			this.IsValid = true;
			Input[] array = this.inputs;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].Valid = false;
			}
			if (this.currInput != null && this.currInput.PresetType != this.InputPreset)
			{
				this.SetInputPreset(this.InputPreset);
			}
			if (this.currInput != null)
			{
				this.currInput.UpdateInput(this.inputs);
			}
		}

		// Token: 0x04000B9E RID: 2974
		public static float DoubleClickTimeout = 0.25f;

		// Token: 0x04000B9F RID: 2975
		public bool FilterInput = true;

		// Token: 0x04000BA0 RID: 2976
		private static InputManager instance;

		// Token: 0x04000BA1 RID: 2977
		public InputPreset InputPreset;

		// Token: 0x04000BA2 RID: 2978
		public bool MobileInput;

		// Token: 0x04000BA3 RID: 2979
		[HideInInspector]
		public bool IsValid;

		// Token: 0x04000BA4 RID: 2980
		private Input[] inputs;

		// Token: 0x04000BA5 RID: 2981
		private GameInput[] GameInputs;

		// Token: 0x04000BA6 RID: 2982
		private GameInput currInput;
	}
}

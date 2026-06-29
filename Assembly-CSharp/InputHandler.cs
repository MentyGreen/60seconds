using System;
using System.Collections.Generic;
using RG.SecondsRemaster;
using UnityEngine;

// Token: 0x0200012F RID: 303
public class InputHandler : MonoBehaviour
{
	// Token: 0x06000ED9 RID: 3801 RVA: 0x0003D88B File Offset: 0x0003BA8B
	private void Awake()
	{
		if (InputHandler.Instance == null)
		{
			InputHandler.Instance = this;
		}
		Object.DontDestroyOnLoad(base.gameObject);
	}

	// Token: 0x06000EDA RID: 3802 RVA: 0x0003D8AB File Offset: 0x0003BAAB
	private void Start()
	{
	}

	// Token: 0x06000EDB RID: 3803 RVA: 0x0003D8AD File Offset: 0x0003BAAD
	private void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			this._lastMouseButtonPressed = 0;
		}
		if (Input.GetMouseButtonDown(1))
		{
			this._lastMouseButtonPressed = 1;
		}
		this.UpdateAxis();
	}

	// Token: 0x06000EDC RID: 3804 RVA: 0x0003D8D4 File Offset: 0x0003BAD4
	private void UpdateAxis()
	{
		for (int i = 0; i < this._axisL.Count; i++)
		{
			if (!this._axisL[i].NativeAxis)
			{
				this._axisL[i].Activate(false, this.IsHeld(this._axisL[i].Min));
				this._axisL[i].Activate(true, this.IsHeld(this._axisL[i].Max));
			}
		}
	}

	// Token: 0x06000EDD RID: 3805 RVA: 0x0003D95C File Offset: 0x0003BB5C
	public void SetKeyboardControl(InputHandler.EControl e, KeyCode k)
	{
		if (this._keyboardSetup.ContainsKey(e))
		{
			this._keyboardSetup[e] = k;
			return;
		}
		this._keyboardSetup.Add(e, k);
	}

	// Token: 0x06000EDE RID: 3806 RVA: 0x0003D988 File Offset: 0x0003BB88
	public void Reload(Dictionary<string, string> controls, ControlMode mode)
	{
		this._axisL.Clear();
		this._axisD.Clear();
		this._keyboardSetup.Clear();
		this._mouseSetup.Clear();
		this._joySetup.Clear();
		this._lastMouseButtonPressed = -1;
		foreach (string text in controls.Keys)
		{
			string text2 = text.Substring(0, text.IndexOf("_"));
			if (text2.Contains(mode.Key))
			{
				int num = text2.Length + 1;
				string text3 = text.Substring(num, text.Length - num);
				try
				{
					InputHandler.EControl econtrol = (InputHandler.EControl)Enum.Parse(typeof(InputHandler.EControl), text3);
					if (econtrol != InputHandler.EControl.NONE && !this._keyboardSetup.ContainsKey(econtrol))
					{
						if (controls[text].Contains("MOUSE_"))
						{
							int num2 = this.ResolveMouseButton(controls[text]);
							if (num2 >= 0)
							{
								this._mouseSetup.Add(econtrol, num2);
							}
						}
						else if (controls[text].Contains("JOY_"))
						{
							int num3 = this.ResolveJoyButton(controls[text]);
							if (num3 >= 0)
							{
								this._joySetup.Add(econtrol, string.Format("joystick {0} button {1}", Settings.Data.ActiveGamepad, num3));
							}
						}
						else if (controls[text].Contains("JoyAxis"))
						{
							this._joySetup.Add(econtrol, text);
							this.RegisterAxis(text, InputHandler.EControl.NONE, InputHandler.EControl.NONE, 1f, controls[text]);
						}
						else
						{
							KeyCode keyCode = this.ResolveKeyboardKey(controls[text]);
							if (keyCode != KeyCode.None)
							{
								this._keyboardSetup.Add(econtrol, keyCode);
							}
						}
					}
				}
				catch
				{
					Debug.LogError(text3);
				}
			}
		}
		switch (mode.ScavengeControl)
		{
		case EPlayerInput.KEYBOARD:
			this.RegisterAxis("Vertical", InputHandler.EControl.SCAVENGE_BACKWARD, InputHandler.EControl.SCAVENGE_FORWARD, 1f, null);
			this.RegisterAxis("Rotate", InputHandler.EControl.SCAVENGE_ROTATE_LEFT, InputHandler.EControl.SCAVENGE_ROTATE_RIGHT, 0.35f, null);
			return;
		case EPlayerInput.KEYBOARD_MOUSE:
			this.RegisterAxis("Vertical", InputHandler.EControl.SCAVENGE_BACKWARD, InputHandler.EControl.SCAVENGE_FORWARD, 1f, null);
			this.RegisterAxis("Horizontal", InputHandler.EControl.SCAVENGE_STRAFE_LEFT, InputHandler.EControl.SCAVENGE_STRAFE_RIGHT, 1f, null);
			return;
		case EPlayerInput.GAMEPAD:
			this.RegisterAxis("Vertical", InputHandler.EControl.NONE, InputHandler.EControl.NONE, 1f, "JoyAxisY");
			this.RegisterAxis("Horizontal", InputHandler.EControl.NONE, InputHandler.EControl.NONE, 1f, "JoyAxisX");
			this.RegisterAxis("Rotate", InputHandler.EControl.NONE, InputHandler.EControl.NONE, 1f, "JoyAxis" + InputHandler.GetJoyAxis(4).ToString());
			return;
		default:
			return;
		}
	}

	// Token: 0x06000EDF RID: 3807 RVA: 0x0003DC70 File Offset: 0x0003BE70
	private KeyCode ResolveKeyboardKey(string val)
	{
		return (KeyCode)Enum.Parse(typeof(KeyCode), val);
	}

	// Token: 0x06000EE0 RID: 3808 RVA: 0x0003DC88 File Offset: 0x0003BE88
	private int ResolveMouseButton(string val)
	{
		int result = -1;
		int.TryParse(val.Substring("MOUSE_".Length, val.Length - "MOUSE_".Length), out result);
		return result;
	}

	// Token: 0x06000EE1 RID: 3809 RVA: 0x0003DCC4 File Offset: 0x0003BEC4
	private int ResolveJoyButton(string val)
	{
		int result = -1;
		string text = "JOY_";
		if (val.Contains("JOY1_"))
		{
			text = val;
		}
		int.TryParse(val.Substring(text.Length, val.Length - text.Length), out result);
		return result;
	}

	// Token: 0x06000EE2 RID: 3810 RVA: 0x0003DD0C File Offset: 0x0003BF0C
	public int GetControlNullable(InputHandler.EControl control)
	{
		if (this._joySetup.ContainsKey(control))
		{
			int num = 0;
			try
			{
				num = (Input.GetKey(this._joySetup[control]) ? 1 : 0);
			}
			catch
			{
			}
			if (num == 0)
			{
				num = this.GetAxisPolar(this._joySetup[control]);
			}
			return num;
		}
		if (this._keyboardSetup.ContainsKey(control))
		{
			if (!Input.GetKey(this._keyboardSetup[control]))
			{
				return 0;
			}
			return 1;
		}
		else
		{
			if (!this._mouseSetup.ContainsKey(control))
			{
				return 0;
			}
			if (!Input.GetMouseButton(this._mouseSetup[control]))
			{
				return 0;
			}
			return 1;
		}
	}

	// Token: 0x06000EE3 RID: 3811 RVA: 0x0003DDBC File Offset: 0x0003BFBC
	public bool GetControl(InputHandler.EControl control)
	{
		if (this._joySetup.ContainsKey(control))
		{
			return Input.GetKey(this._joySetup[control]);
		}
		if (this._keyboardSetup.ContainsKey(control))
		{
			return Input.GetKey(this._keyboardSetup[control]);
		}
		return this._mouseSetup.ContainsKey(control) && Input.GetMouseButton(this._mouseSetup[control]);
	}

	// Token: 0x06000EE4 RID: 3812 RVA: 0x0003DE2C File Offset: 0x0003C02C
	public bool GetControlDownUp(InputHandler.EControl control, bool keyboardTestDown = true, bool mouseTestDown = true, bool gamepadTestDown = true)
	{
		if (this._joySetup.ContainsKey(control))
		{
			if (gamepadTestDown)
			{
				return Input.GetKeyDown(this._joySetup[control]);
			}
			return Input.GetKeyUp(this._joySetup[control]);
		}
		else if (this._keyboardSetup.ContainsKey(control))
		{
			if (keyboardTestDown)
			{
				return Input.GetKeyDown(this._keyboardSetup[control]);
			}
			return Input.GetKeyUp(this._keyboardSetup[control]);
		}
		else
		{
			if (!this._mouseSetup.ContainsKey(control))
			{
				return false;
			}
			if (mouseTestDown)
			{
				return Input.GetMouseButtonDown(this._mouseSetup[control]);
			}
			return Input.GetMouseButtonUp(this._mouseSetup[control]);
		}
	}

	// Token: 0x06000EE5 RID: 3813 RVA: 0x0003DEDA File Offset: 0x0003C0DA
	public bool IsDown(InputHandler.EControl control)
	{
		return Input.GetKeyDown(this._keyboardSetup[control]);
	}

	// Token: 0x06000EE6 RID: 3814 RVA: 0x0003DEED File Offset: 0x0003C0ED
	public bool IsUp(InputHandler.EControl control)
	{
		return Input.GetKeyUp(this._keyboardSetup[control]);
	}

	// Token: 0x06000EE7 RID: 3815 RVA: 0x0003DF00 File Offset: 0x0003C100
	public bool IsHeld(InputHandler.EControl control)
	{
		return Input.GetKey(this._keyboardSetup[control]);
	}

	// Token: 0x06000EE8 RID: 3816 RVA: 0x0003DF14 File Offset: 0x0003C114
	public void RegisterAxis(string name, InputHandler.EControl min, InputHandler.EControl max, float filter = 1f, string nativeAxisName = null)
	{
		InputHandler.ControlAxis controlAxis = new InputHandler.ControlAxis(min, max, filter, nativeAxisName);
		this._axisL.Add(controlAxis);
		this._axisD.Add(name, controlAxis);
	}

	// Token: 0x06000EE9 RID: 3817 RVA: 0x0003DF46 File Offset: 0x0003C146
	public float GetAxis(string key)
	{
		return this._axisD[key].GetAxis();
	}

	// Token: 0x06000EEA RID: 3818 RVA: 0x0003DF5C File Offset: 0x0003C15C
	public bool TestAxisPositive(string key, bool positive)
	{
		float axis = this.GetAxis(key);
		if (positive)
		{
			return axis > 0f;
		}
		return axis < 0f;
	}

	// Token: 0x06000EEB RID: 3819 RVA: 0x0003DF88 File Offset: 0x0003C188
	public int GetAxisPolar(string key)
	{
		float axis = this.GetAxis(key);
		if (axis > 0f)
		{
			return 1;
		}
		if (axis < 0f)
		{
			return -1;
		}
		return 0;
	}

	// Token: 0x06000EEC RID: 3820 RVA: 0x0003DFB2 File Offset: 0x0003C1B2
	public float GetAxisRaw(string key)
	{
		return this._axisD[key].GetAxisRaw();
	}

	// Token: 0x06000EED RID: 3821 RVA: 0x0003DFC8 File Offset: 0x0003C1C8
	public int GetRawAxisPolar(string key)
	{
		float axisRaw = this.GetAxisRaw(key);
		if (axisRaw > 0f)
		{
			return 1;
		}
		if (axisRaw < 0f)
		{
			return -1;
		}
		return 0;
	}

	// Token: 0x06000EEE RID: 3822 RVA: 0x0003DFF2 File Offset: 0x0003C1F2
	public bool WasLastMouseButtonPressed(int button)
	{
		return this._lastMouseButtonPressed == button;
	}

	// Token: 0x17000328 RID: 808
	// (get) Token: 0x06000EEF RID: 3823 RVA: 0x0003DFFD File Offset: 0x0003C1FD
	public int LastMouseButtonPressed
	{
		get
		{
			return this._lastMouseButtonPressed;
		}
	}

	// Token: 0x06000EF0 RID: 3824 RVA: 0x0003E005 File Offset: 0x0003C205
	public static int GetJoyAxis(int code)
	{
		return code;
	}

	// Token: 0x06000EF1 RID: 3825 RVA: 0x0003E008 File Offset: 0x0003C208
	public static int GetJoyButtonCode(int code)
	{
		return code;
	}

	// Token: 0x040008FE RID: 2302
	public static InputHandler Instance;

	// Token: 0x040008FF RID: 2303
	private const string JOY_BUTTON_PREFIX = "joystick {0} button {1}";

	// Token: 0x04000900 RID: 2304
	public const int LEFT_MOUSE_BUTTON = 0;

	// Token: 0x04000901 RID: 2305
	public const int RIGHT_MOUSE_BUTTON = 1;

	// Token: 0x04000902 RID: 2306
	public const int MIDDLE_MOUSE_BUTTON = 2;

	// Token: 0x04000903 RID: 2307
	private Dictionary<InputHandler.EControl, KeyCode> _keyboardSetup = new Dictionary<InputHandler.EControl, KeyCode>();

	// Token: 0x04000904 RID: 2308
	private Dictionary<InputHandler.EControl, int> _mouseSetup = new Dictionary<InputHandler.EControl, int>();

	// Token: 0x04000905 RID: 2309
	private Dictionary<InputHandler.EControl, string> _joySetup = new Dictionary<InputHandler.EControl, string>();

	// Token: 0x04000906 RID: 2310
	private Dictionary<string, InputHandler.ControlAxis> _axisD = new Dictionary<string, InputHandler.ControlAxis>();

	// Token: 0x04000907 RID: 2311
	private List<InputHandler.ControlAxis> _axisL = new List<InputHandler.ControlAxis>();

	// Token: 0x04000908 RID: 2312
	private int _lastMouseButtonPressed = -1;

	// Token: 0x020003C4 RID: 964
	public enum EControl
	{
		// Token: 0x0400177B RID: 6011
		NONE,
		// Token: 0x0400177C RID: 6012
		GLOBAL_MENU,
		// Token: 0x0400177D RID: 6013
		SCAVENGE_FORWARD,
		// Token: 0x0400177E RID: 6014
		SCAVENGE_BACKWARD,
		// Token: 0x0400177F RID: 6015
		SCAVENGE_ROTATE_LEFT,
		// Token: 0x04001780 RID: 6016
		SCAVENGE_ROTATE_RIGHT,
		// Token: 0x04001781 RID: 6017
		SCAVENGE_STRAFE_LEFT,
		// Token: 0x04001782 RID: 6018
		SCAVENGE_STRAFE_RIGHT,
		// Token: 0x04001783 RID: 6019
		SCAVENGE_INTERACTION,
		// Token: 0x04001784 RID: 6020
		GLOBAL_ACTION1,
		// Token: 0x04001785 RID: 6021
		GLOBAL_ACTION2,
		// Token: 0x04001786 RID: 6022
		GLOBAL_ALTCHOICEX,
		// Token: 0x04001787 RID: 6023
		GLOBAL_ALTCHOICEY,
		// Token: 0x04001788 RID: 6024
		GLOBAL_CHOICE1,
		// Token: 0x04001789 RID: 6025
		GLOBAL_CHOICE2,
		// Token: 0x0400178A RID: 6026
		GLOBAL_CHOICE3,
		// Token: 0x0400178B RID: 6027
		GLOBAL_CHOICE4,
		// Token: 0x0400178C RID: 6028
		GLOBAL_NEXT,
		// Token: 0x0400178D RID: 6029
		GLOBAL_PREV
	}

	// Token: 0x020003C5 RID: 965
	private class ControlAxis
	{
		// Token: 0x06001E2D RID: 7725 RVA: 0x00080020 File Offset: 0x0007E220
		public ControlAxis(InputHandler.EControl min, InputHandler.EControl max, float filter, string nativeAxisName = null)
		{
			this._min = min;
			this._max = max;
			this._filter = filter;
			this._nativeAxisName = nativeAxisName;
			this._nativeAxis = !string.IsNullOrEmpty(this._nativeAxisName);
		}

		// Token: 0x06001E2E RID: 7726 RVA: 0x0008005C File Offset: 0x0007E25C
		public float GetAxisRaw()
		{
			if (this._nativeAxis)
			{
				return Input.GetAxisRaw(this._nativeAxisName);
			}
			float num = 0f;
			if (this._maxActive)
			{
				num += 1f;
			}
			if (this._minActive)
			{
				num += -1f;
			}
			return num;
		}

		// Token: 0x06001E2F RID: 7727 RVA: 0x000800A4 File Offset: 0x0007E2A4
		public float GetAxis()
		{
			if (this._nativeAxis)
			{
				return Input.GetAxis(this._nativeAxisName);
			}
			float num = 0f;
			if (this._maxActive)
			{
				num += Mathf.Lerp(0f, 1f, (Time.time - this._maxActivationTime) / this._filter);
			}
			if (this._minActive)
			{
				num += Mathf.Lerp(0f, -1f, (Time.time - this._minActivationTime) / this._filter);
			}
			return num;
		}

		// Token: 0x06001E30 RID: 7728 RVA: 0x00080128 File Offset: 0x0007E328
		public void Activate(bool max, bool activate)
		{
			if (max)
			{
				if (!this._maxActive && activate)
				{
					this._maxActivationTime = Time.time;
				}
				this._maxActive = activate;
				return;
			}
			if (!this._minActive && activate)
			{
				this._minActivationTime = Time.time;
			}
			this._minActive = activate;
		}

		// Token: 0x1700055B RID: 1371
		// (get) Token: 0x06001E31 RID: 7729 RVA: 0x00080177 File Offset: 0x0007E377
		public bool MinActive
		{
			get
			{
				return this._minActive;
			}
		}

		// Token: 0x1700055C RID: 1372
		// (get) Token: 0x06001E32 RID: 7730 RVA: 0x0008017F File Offset: 0x0007E37F
		public bool MaxActive
		{
			get
			{
				return this._maxActive;
			}
		}

		// Token: 0x1700055D RID: 1373
		// (get) Token: 0x06001E33 RID: 7731 RVA: 0x00080187 File Offset: 0x0007E387
		public float MinActivationTime
		{
			get
			{
				return this._minActivationTime;
			}
		}

		// Token: 0x1700055E RID: 1374
		// (get) Token: 0x06001E34 RID: 7732 RVA: 0x0008018F File Offset: 0x0007E38F
		public float MaxActivationTime
		{
			get
			{
				return this._maxActivationTime;
			}
		}

		// Token: 0x1700055F RID: 1375
		// (get) Token: 0x06001E35 RID: 7733 RVA: 0x00080197 File Offset: 0x0007E397
		public InputHandler.EControl Min
		{
			get
			{
				return this._min;
			}
		}

		// Token: 0x17000560 RID: 1376
		// (get) Token: 0x06001E36 RID: 7734 RVA: 0x0008019F File Offset: 0x0007E39F
		public InputHandler.EControl Max
		{
			get
			{
				return this._max;
			}
		}

		// Token: 0x17000561 RID: 1377
		// (get) Token: 0x06001E37 RID: 7735 RVA: 0x000801A7 File Offset: 0x0007E3A7
		public bool NativeAxis
		{
			get
			{
				return this._nativeAxis;
			}
		}

		// Token: 0x0400178E RID: 6030
		private string _nativeAxisName;

		// Token: 0x0400178F RID: 6031
		private InputHandler.EControl _min;

		// Token: 0x04001790 RID: 6032
		private InputHandler.EControl _max;

		// Token: 0x04001791 RID: 6033
		private float _filter;

		// Token: 0x04001792 RID: 6034
		private bool _minActive;

		// Token: 0x04001793 RID: 6035
		private bool _maxActive;

		// Token: 0x04001794 RID: 6036
		private float _minActivationTime;

		// Token: 0x04001795 RID: 6037
		private float _maxActivationTime;

		// Token: 0x04001796 RID: 6038
		private bool _nativeAxis;
	}
}

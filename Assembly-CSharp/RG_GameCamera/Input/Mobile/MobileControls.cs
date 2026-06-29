using System;
using RG_GameCamera.Utils;
using UnityEngine;

namespace RG_GameCamera.Input.Mobile
{
	// Token: 0x020001A7 RID: 423
	[ExecuteInEditMode]
	public class MobileControls : MonoBehaviour
	{
		// Token: 0x17000388 RID: 904
		// (get) Token: 0x06001247 RID: 4679 RVA: 0x0004E7B3 File Offset: 0x0004C9B3
		public static MobileControls Instance
		{
			get
			{
				if (!MobileControls.instance)
				{
					CameraInstance.CreateInstance<MobileControls>("MobileControls");
				}
				return MobileControls.instance;
			}
		}

		// Token: 0x06001248 RID: 4680 RVA: 0x0004E7D1 File Offset: 0x0004C9D1
		private void Awake()
		{
			this.Init();
		}

		// Token: 0x06001249 RID: 4681 RVA: 0x0004E7DC File Offset: 0x0004C9DC
		private void Init()
		{
			if (MobileControls.instance == null)
			{
				MobileControls.instance = this;
				this.touchProcessor = new TouchProcessor(2);
				BaseControl[] controls = this.GetControls();
				if (controls != null)
				{
					BaseControl[] array = controls;
					for (int i = 0; i < array.Length; i++)
					{
						array[i].Init(this.touchProcessor);
					}
				}
			}
		}

		// Token: 0x0600124A RID: 4682 RVA: 0x0004E830 File Offset: 0x0004CA30
		public BaseControl[] GetControls()
		{
			BaseControl[] components = base.gameObject.GetComponents<BaseControl>();
			Array.Sort<BaseControl>(components, (BaseControl a, BaseControl b) => b.Priority.CompareTo(a.Priority));
			return components;
		}

		// Token: 0x0600124B RID: 4683 RVA: 0x0004E862 File Offset: 0x0004CA62
		public Button CreateButton(string btnName)
		{
			Button button = base.gameObject.AddComponent<Button>();
			button.Init(this.touchProcessor);
			button.InputKey0 = btnName;
			return button;
		}

		// Token: 0x0600124C RID: 4684 RVA: 0x0004E882 File Offset: 0x0004CA82
		public Zoom CreateZoom(string btnName)
		{
			Zoom zoom = base.gameObject.AddComponent<Zoom>();
			zoom.Init(this.touchProcessor);
			zoom.InputKey0 = btnName;
			return zoom;
		}

		// Token: 0x0600124D RID: 4685 RVA: 0x0004E8A4 File Offset: 0x0004CAA4
		public void DuplicateButtonValues(Button target, Button source)
		{
			target.Position = source.Position;
			target.Size = source.Size;
			target.PreserveTextureRatio = source.PreserveTextureRatio;
			target.Toggle = source.Toggle;
			target.TextureDefault = source.TextureDefault;
			target.TexturePressed = source.TexturePressed;
		}

		// Token: 0x0600124E RID: 4686 RVA: 0x0004E8F9 File Offset: 0x0004CAF9
		public void DuplicateZoomValues(Zoom target, Zoom source)
		{
			target.Position = source.Position;
			target.Size = source.Size;
		}

		// Token: 0x0600124F RID: 4687 RVA: 0x0004E913 File Offset: 0x0004CB13
		public void RemoveControl(BaseControl button)
		{
			RG_GameCamera.Utils.Debug.Destroy(button, true);
		}

		// Token: 0x06001250 RID: 4688 RVA: 0x0004E91C File Offset: 0x0004CB1C
		private BaseControl DeserializeMasterControl(ControlType type)
		{
			BaseControl baseControl = null;
			if (type != ControlType.Stick)
			{
				if (type == ControlType.CameraPanel)
				{
					baseControl = base.gameObject.AddComponent<CameraPanel>();
				}
			}
			else
			{
				baseControl = base.gameObject.AddComponent<Stick>();
			}
			if (baseControl != null)
			{
				baseControl.Init(this.touchProcessor);
			}
			return baseControl;
		}

		// Token: 0x06001251 RID: 4689 RVA: 0x0004E964 File Offset: 0x0004CB64
		public BaseControl CreateMasterControl(string axis0, string axis1, ControlType type, ControlSide side)
		{
			this.RemoveMasterControl(side);
			BaseControl baseControl = null;
			if (type != ControlType.Stick)
			{
				if (type == ControlType.CameraPanel)
				{
					baseControl = base.gameObject.AddComponent<CameraPanel>();
				}
			}
			else
			{
				baseControl = base.gameObject.AddComponent<Stick>();
			}
			if (baseControl != null)
			{
				baseControl.Init(this.touchProcessor);
				baseControl.Side = side;
				baseControl.InputKey0 = axis0;
				baseControl.InputKey1 = axis1;
			}
			return baseControl;
		}

		// Token: 0x06001252 RID: 4690 RVA: 0x0004E9CC File Offset: 0x0004CBCC
		public void RemoveMasterControl(ControlSide side)
		{
			BaseControl[] controls = this.GetControls();
			if (controls != null)
			{
				foreach (BaseControl baseControl in controls)
				{
					if (baseControl.Side == side)
					{
						this.RemoveControl(baseControl);
					}
				}
			}
		}

		// Token: 0x06001253 RID: 4691 RVA: 0x0004EA08 File Offset: 0x0004CC08
		public bool GetButton(string key)
		{
			BaseControl baseControl;
			return this.TryGetControl(key, out baseControl) && baseControl.Type == ControlType.Button && ((Button)baseControl).IsPressed();
		}

		// Token: 0x06001254 RID: 4692 RVA: 0x0004EA38 File Offset: 0x0004CC38
		public float GetZoom(string key)
		{
			BaseControl baseControl;
			if (this.TryGetControl(key, out baseControl) && baseControl.Type == ControlType.Zoom)
			{
				return ((Zoom)baseControl).ZoomDelta;
			}
			return 0f;
		}

		// Token: 0x06001255 RID: 4693 RVA: 0x0004EA6C File Offset: 0x0004CC6C
		public float GetAxis(string key)
		{
			BaseControl baseControl;
			if (!this.TryGetControl(key, out baseControl) || (baseControl.Type != ControlType.Stick && baseControl.Type != ControlType.CameraPanel))
			{
				return 0f;
			}
			Vector2 inputAxis = baseControl.GetInputAxis();
			if (key == baseControl.InputKey0)
			{
				return inputAxis.x;
			}
			if (key == baseControl.InputKey1)
			{
				return inputAxis.y;
			}
			return 0f;
		}

		// Token: 0x06001256 RID: 4694 RVA: 0x0004EAD4 File Offset: 0x0004CCD4
		public bool GetButtonDown(string buttonName)
		{
			BaseControl baseControl;
			return this.TryGetControl(buttonName, out baseControl) && baseControl.Type == ControlType.Button && ((Button)baseControl).State == Button.ButtonState.Begin;
		}

		// Token: 0x06001257 RID: 4695 RVA: 0x0004EB08 File Offset: 0x0004CD08
		public bool GetButtonUp(string buttonName)
		{
			BaseControl baseControl;
			return this.TryGetControl(buttonName, out baseControl) && baseControl.Type == ControlType.Button && ((Button)baseControl).State == Button.ButtonState.End;
		}

		// Token: 0x06001258 RID: 4696 RVA: 0x0004EB3C File Offset: 0x0004CD3C
		private bool TryGetControl(string key, out BaseControl ctrl)
		{
			BaseControl[] controls = this.GetControls();
			if (controls != null)
			{
				foreach (BaseControl baseControl in controls)
				{
					if (baseControl.InputKey0 == key || baseControl.InputKey1 == key)
					{
						ctrl = baseControl;
						return true;
					}
				}
			}
			ctrl = null;
			return false;
		}

		// Token: 0x06001259 RID: 4697 RVA: 0x0004EB8C File Offset: 0x0004CD8C
		private void Update()
		{
			this.Init();
			this.touchProcessor.ScanInput();
			BaseControl[] controls = this.GetControls();
			if (controls != null)
			{
				foreach (BaseControl baseControl in controls)
				{
					baseControl.GameUpdate();
					if (baseControl.AbortUpdateOtherControls())
					{
						break;
					}
				}
			}
		}

		// Token: 0x0600125A RID: 4698 RVA: 0x0004EBD4 File Offset: 0x0004CDD4
		private void OnGUI()
		{
			if (Event.current.type != EventType.Repaint)
			{
				return;
			}
			BaseControl[] controls = this.GetControls();
			if (controls != null)
			{
				BaseControl[] array = controls;
				for (int i = 0; i < array.Length; i++)
				{
					array[i].Draw();
				}
			}
		}

		// Token: 0x04000BD9 RID: 3033
		private static MobileControls instance;

		// Token: 0x04000BDA RID: 3034
		public int LeftPanelIndex;

		// Token: 0x04000BDB RID: 3035
		public int RightPanelIndex;

		// Token: 0x04000BDC RID: 3036
		private TouchProcessor touchProcessor;
	}
}

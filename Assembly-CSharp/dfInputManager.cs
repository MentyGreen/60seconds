using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

// Token: 0x02000030 RID: 48
[AddComponentMenu("Daikon Forge/User Interface/Input Manager")]
[Serializable]
public class dfInputManager : MonoBehaviour
{
	// Token: 0x17000145 RID: 325
	// (get) Token: 0x06000594 RID: 1428 RVA: 0x0001B0A0 File Offset: 0x000192A0
	public static IList<dfInputManager> ActiveInstances
	{
		get
		{
			return dfInputManager.activeInstances;
		}
	}

	// Token: 0x17000146 RID: 326
	// (get) Token: 0x06000595 RID: 1429 RVA: 0x0001B0A7 File Offset: 0x000192A7
	public static dfControl ControlUnderMouse
	{
		get
		{
			return dfInputManager.controlUnderMouse;
		}
	}

	// Token: 0x17000147 RID: 327
	// (get) Token: 0x06000596 RID: 1430 RVA: 0x0001B0AE File Offset: 0x000192AE
	// (set) Token: 0x06000597 RID: 1431 RVA: 0x0001B0B6 File Offset: 0x000192B6
	public Camera RenderCamera
	{
		get
		{
			return this.renderCamera;
		}
		set
		{
			this.renderCamera = value;
		}
	}

	// Token: 0x17000148 RID: 328
	// (get) Token: 0x06000598 RID: 1432 RVA: 0x0001B0BF File Offset: 0x000192BF
	// (set) Token: 0x06000599 RID: 1433 RVA: 0x0001B0C7 File Offset: 0x000192C7
	public bool UseTouch
	{
		get
		{
			return this.useTouch;
		}
		set
		{
			this.useTouch = value;
		}
	}

	// Token: 0x17000149 RID: 329
	// (get) Token: 0x0600059A RID: 1434 RVA: 0x0001B0D0 File Offset: 0x000192D0
	// (set) Token: 0x0600059B RID: 1435 RVA: 0x0001B0D8 File Offset: 0x000192D8
	public bool UseMouse
	{
		get
		{
			return this.useMouse;
		}
		set
		{
			this.useMouse = value;
		}
	}

	// Token: 0x1700014A RID: 330
	// (get) Token: 0x0600059C RID: 1436 RVA: 0x0001B0E1 File Offset: 0x000192E1
	// (set) Token: 0x0600059D RID: 1437 RVA: 0x0001B0E9 File Offset: 0x000192E9
	public bool UseJoystick
	{
		get
		{
			return this.useJoystick;
		}
		set
		{
			this.useJoystick = value;
		}
	}

	// Token: 0x1700014B RID: 331
	// (get) Token: 0x0600059E RID: 1438 RVA: 0x0001B0F2 File Offset: 0x000192F2
	// (set) Token: 0x0600059F RID: 1439 RVA: 0x0001B0FA File Offset: 0x000192FA
	public KeyCode JoystickClickButton
	{
		get
		{
			return this.joystickClickButton;
		}
		set
		{
			this.joystickClickButton = value;
		}
	}

	// Token: 0x1700014C RID: 332
	// (get) Token: 0x060005A0 RID: 1440 RVA: 0x0001B103 File Offset: 0x00019303
	// (set) Token: 0x060005A1 RID: 1441 RVA: 0x0001B10B File Offset: 0x0001930B
	public string HorizontalAxis
	{
		get
		{
			return this.horizontalAxis;
		}
		set
		{
			this.horizontalAxis = value;
		}
	}

	// Token: 0x1700014D RID: 333
	// (get) Token: 0x060005A2 RID: 1442 RVA: 0x0001B114 File Offset: 0x00019314
	// (set) Token: 0x060005A3 RID: 1443 RVA: 0x0001B11C File Offset: 0x0001931C
	public string VerticalAxis
	{
		get
		{
			return this.verticalAxis;
		}
		set
		{
			this.verticalAxis = value;
		}
	}

	// Token: 0x1700014E RID: 334
	// (get) Token: 0x060005A4 RID: 1444 RVA: 0x0001B125 File Offset: 0x00019325
	// (set) Token: 0x060005A5 RID: 1445 RVA: 0x0001B12D File Offset: 0x0001932D
	public IInputAdapter Adapter
	{
		get
		{
			return this.adapter;
		}
		set
		{
			this.adapter = (value ?? new dfInputManager.DefaultInput());
		}
	}

	// Token: 0x1700014F RID: 335
	// (get) Token: 0x060005A6 RID: 1446 RVA: 0x0001B13F File Offset: 0x0001933F
	// (set) Token: 0x060005A7 RID: 1447 RVA: 0x0001B147 File Offset: 0x00019347
	public bool RetainFocus
	{
		get
		{
			return this.retainFocus;
		}
		set
		{
			this.retainFocus = value;
		}
	}

	// Token: 0x17000150 RID: 336
	// (get) Token: 0x060005A8 RID: 1448 RVA: 0x0001B150 File Offset: 0x00019350
	// (set) Token: 0x060005A9 RID: 1449 RVA: 0x0001B158 File Offset: 0x00019358
	public IDFTouchInputSource TouchInputSource
	{
		get
		{
			return this.touchInputSource;
		}
		set
		{
			this.touchInputSource = value;
		}
	}

	// Token: 0x17000151 RID: 337
	// (get) Token: 0x060005AA RID: 1450 RVA: 0x0001B161 File Offset: 0x00019361
	// (set) Token: 0x060005AB RID: 1451 RVA: 0x0001B169 File Offset: 0x00019369
	public float HoverStartDelay
	{
		get
		{
			return this.hoverStartDelay;
		}
		set
		{
			this.hoverStartDelay = value;
		}
	}

	// Token: 0x17000152 RID: 338
	// (get) Token: 0x060005AC RID: 1452 RVA: 0x0001B172 File Offset: 0x00019372
	// (set) Token: 0x060005AD RID: 1453 RVA: 0x0001B17A File Offset: 0x0001937A
	public float HoverNotificationFrequency
	{
		get
		{
			return this.hoverNotifactionFrequency;
		}
		set
		{
			this.hoverNotifactionFrequency = value;
		}
	}

	// Token: 0x060005AE RID: 1454 RVA: 0x0001B183 File Offset: 0x00019383
	public void Awake()
	{
		base.useGUILayout = false;
	}

	// Token: 0x060005AF RID: 1455 RVA: 0x0001B18C File Offset: 0x0001938C
	public void Start()
	{
	}

	// Token: 0x060005B0 RID: 1456 RVA: 0x0001B190 File Offset: 0x00019390
	public void OnDisable()
	{
		dfInputManager.activeInstances.Remove(this);
		dfControl activeControl = dfGUIManager.ActiveControl;
		if (activeControl != null && activeControl.transform.IsChildOf(base.transform))
		{
			dfGUIManager.SetFocus(null);
		}
	}

	// Token: 0x060005B1 RID: 1457 RVA: 0x0001B1D4 File Offset: 0x000193D4
	public void OnEnable()
	{
		dfInputManager.activeInstances.Add(this);
		this.mouseHandler = new dfInputManager.MouseInputManager();
		if (this.useTouch)
		{
			this.touchHandler = new dfInputManager.TouchInputManager(this);
		}
		if (this.adapter == null)
		{
			Component component = (from c in base.GetComponents(typeof(MonoBehaviour))
			where c != null && c.GetType() != null && typeof(IInputAdapter).IsAssignableFrom(c.GetType())
			select c).FirstOrDefault<Component>();
			this.adapter = (((IInputAdapter)component) ?? new dfInputManager.DefaultInput());
		}
		Input.simulateMouseWithTouches = !this.useTouch;
	}

	// Token: 0x060005B2 RID: 1458 RVA: 0x0001B270 File Offset: 0x00019470
	public void Update()
	{
		if (!Application.isPlaying)
		{
			return;
		}
		if (this.guiManager == null)
		{
			this.guiManager = base.GetComponent<dfGUIManager>();
			if (this.guiManager == null)
			{
				Debug.LogWarning("No associated dfGUIManager instance", this);
				base.enabled = false;
				return;
			}
		}
		dfControl activeControl = dfGUIManager.ActiveControl;
		if (this.useTouch && this.processTouchInput())
		{
			return;
		}
		if (this.useMouse)
		{
			this.processMouseInput();
		}
		if (activeControl == null)
		{
			return;
		}
		if (this.processKeyboard())
		{
			return;
		}
		if (this.useJoystick)
		{
			for (int i = 0; i < dfInputManager.wasd.Length; i++)
			{
				if (Input.GetKey(dfInputManager.wasd[i]) || Input.GetKeyDown(dfInputManager.wasd[i]) || Input.GetKeyUp(dfInputManager.wasd[i]))
				{
					return;
				}
			}
			this.processJoystick();
		}
	}

	// Token: 0x060005B3 RID: 1459 RVA: 0x0001B344 File Offset: 0x00019544
	public void OnGUI()
	{
		Event current = Event.current;
		if (current == null)
		{
			return;
		}
		if (current.isKey && current.keyCode != KeyCode.None)
		{
			this.processKeyEvent(current.type, current.keyCode, current.modifiers);
			return;
		}
	}

	// Token: 0x060005B4 RID: 1460 RVA: 0x0001B384 File Offset: 0x00019584
	private void processJoystick()
	{
		try
		{
			dfControl activeControl = dfGUIManager.ActiveControl;
			if (!(activeControl == null) && activeControl.transform.IsChildOf(base.transform))
			{
				float axis = this.adapter.GetAxis(this.horizontalAxis);
				float axis2 = this.adapter.GetAxis(this.verticalAxis);
				if (Mathf.Abs(axis) < 0.5f && Mathf.Abs(axis2) <= 0.5f)
				{
					this.lastAxisCheck = Time.deltaTime - this.axisPollingInterval;
				}
				if (Time.realtimeSinceStartup - this.lastAxisCheck > this.axisPollingInterval)
				{
					if (Mathf.Abs(axis) >= 0.5f)
					{
						this.lastAxisCheck = Time.realtimeSinceStartup;
						KeyCode key = (axis > 0f) ? KeyCode.RightArrow : KeyCode.LeftArrow;
						activeControl.OnKeyDown(new dfKeyEventArgs(activeControl, key, false, false, false));
					}
					if (Mathf.Abs(axis2) >= 0.5f)
					{
						this.lastAxisCheck = Time.realtimeSinceStartup;
						KeyCode key2 = (axis2 > 0f) ? KeyCode.UpArrow : KeyCode.DownArrow;
						activeControl.OnKeyDown(new dfKeyEventArgs(activeControl, key2, false, false, false));
					}
				}
				if (this.joystickClickButton != KeyCode.None)
				{
					if (this.adapter.GetKeyDown(this.joystickClickButton))
					{
						Vector3 center = activeControl.GetCenter();
						Camera camera = activeControl.GetCamera();
						Ray ray = camera.ScreenPointToRay(camera.WorldToScreenPoint(center));
						dfMouseEventArgs args = new dfMouseEventArgs(activeControl, dfMouseButtons.Left, 0, ray, center, 0f);
						activeControl.OnMouseDown(args);
						this.buttonDownTarget = activeControl;
					}
					if (this.adapter.GetKeyUp(this.joystickClickButton))
					{
						if (this.buttonDownTarget == activeControl)
						{
							activeControl.DoClick();
						}
						Vector3 center2 = activeControl.GetCenter();
						Camera camera2 = activeControl.GetCamera();
						Ray ray2 = camera2.ScreenPointToRay(camera2.WorldToScreenPoint(center2));
						dfMouseEventArgs args2 = new dfMouseEventArgs(activeControl, dfMouseButtons.Left, 0, ray2, center2, 0f);
						activeControl.OnMouseUp(args2);
						this.buttonDownTarget = null;
					}
				}
				for (KeyCode keyCode = KeyCode.Joystick1Button0; keyCode <= KeyCode.Joystick1Button19; keyCode++)
				{
					if (this.adapter.GetKeyDown(keyCode))
					{
						activeControl.OnKeyDown(new dfKeyEventArgs(activeControl, keyCode, false, false, false));
					}
				}
			}
		}
		catch (UnityException ex)
		{
			Debug.LogError(ex.ToString(), this);
			this.useJoystick = false;
		}
	}

	// Token: 0x060005B5 RID: 1461 RVA: 0x0001B5D0 File Offset: 0x000197D0
	private void processKeyEvent(EventType eventType, KeyCode keyCode, EventModifiers modifiers)
	{
		dfControl activeControl = dfGUIManager.ActiveControl;
		if (activeControl == null || !activeControl.IsEnabled || !activeControl.transform.IsChildOf(base.transform))
		{
			return;
		}
		bool control = (modifiers & EventModifiers.Control) == EventModifiers.Control || (modifiers & EventModifiers.Command) == EventModifiers.Command;
		bool flag = (modifiers & EventModifiers.Shift) == EventModifiers.Shift;
		bool alt = (modifiers & EventModifiers.Alt) == EventModifiers.Alt;
		dfKeyEventArgs dfKeyEventArgs = new dfKeyEventArgs(activeControl, keyCode, control, flag, alt);
		if (keyCode >= KeyCode.Space && keyCode <= KeyCode.Z)
		{
			char c = (char)keyCode;
			dfKeyEventArgs.Character = (flag ? char.ToUpper(c) : char.ToLower(c));
		}
		if (eventType == EventType.KeyDown)
		{
			activeControl.OnKeyDown(dfKeyEventArgs);
		}
		else if (eventType == EventType.KeyUp)
		{
			activeControl.OnKeyUp(dfKeyEventArgs);
		}
		if (!dfKeyEventArgs.Used)
		{
		}
	}

	// Token: 0x060005B6 RID: 1462 RVA: 0x0001B684 File Offset: 0x00019884
	private bool processKeyboard()
	{
		dfControl activeControl = dfGUIManager.ActiveControl;
		if (activeControl == null || string.IsNullOrEmpty(Input.inputString) || !activeControl.transform.IsChildOf(base.transform))
		{
			return false;
		}
		foreach (char c in Input.inputString)
		{
			if (c != '\b' && c != '\n')
			{
				KeyCode key = (KeyCode)c;
				activeControl.OnKeyPress(new dfKeyEventArgs(activeControl, key, false, false, false)
				{
					Character = c
				});
			}
		}
		return true;
	}

	// Token: 0x060005B7 RID: 1463 RVA: 0x0001B70C File Offset: 0x0001990C
	private bool processTouchInput()
	{
		if (this.touchInputSource == null)
		{
			foreach (dfTouchInputSourceComponent dfTouchInputSourceComponent in (from x in base.GetComponents<dfTouchInputSourceComponent>()
			orderby x.Priority descending
			select x).ToArray<dfTouchInputSourceComponent>())
			{
				if (dfTouchInputSourceComponent.enabled)
				{
					this.touchInputSource = dfTouchInputSourceComponent.Source;
					if (this.touchInputSource != null)
					{
						break;
					}
				}
			}
			if (this.touchInputSource == null)
			{
				this.touchInputSource = dfMobileTouchInputSource.Instance;
			}
		}
		this.touchInputSource.Update();
		if (this.touchInputSource.TouchCount == 0)
		{
			return false;
		}
		this.touchHandler.Process(base.transform, this.renderCamera, this.touchInputSource, this.retainFocus);
		return true;
	}

	// Token: 0x060005B8 RID: 1464 RVA: 0x0001B7D0 File Offset: 0x000199D0
	private void processMouseInput()
	{
		if (this.guiManager == null)
		{
			return;
		}
		Vector2 mousePosition = this.adapter.GetMousePosition();
		Ray ray = this.renderCamera.ScreenPointToRay(mousePosition);
		dfInputManager.controlUnderMouse = dfGUIManager.HitTestAll(mousePosition);
		if (dfInputManager.controlUnderMouse != null && !dfInputManager.controlUnderMouse.transform.IsChildOf(base.transform))
		{
			dfInputManager.controlUnderMouse = null;
		}
		this.mouseHandler.ProcessInput(this, this.adapter, ray, dfInputManager.controlUnderMouse, this.retainFocus);
	}

	// Token: 0x060005B9 RID: 1465 RVA: 0x0001B860 File Offset: 0x00019A60
	internal static int raycastHitSorter(RaycastHit lhs, RaycastHit rhs)
	{
		return lhs.distance.CompareTo(rhs.distance);
	}

	// Token: 0x060005BA RID: 1466 RVA: 0x0001B884 File Offset: 0x00019A84
	internal dfControl clipCast(RaycastHit[] hits)
	{
		if (hits == null || hits.Length == 0)
		{
			return null;
		}
		dfControl dfControl = null;
		dfControl modalControl = dfGUIManager.GetModalControl();
		for (int i = hits.Length - 1; i >= 0; i--)
		{
			RaycastHit raycastHit = hits[i];
			dfControl component = raycastHit.transform.GetComponent<dfControl>();
			if (!(component == null) && (!(modalControl != null) || component.transform.IsChildOf(modalControl.transform)) && component.enabled && dfInputManager.combinedOpacity(component) > 0.01f && component.IsEnabled && component.IsVisible && component.transform.IsChildOf(base.transform) && dfInputManager.isInsideClippingRegion(raycastHit.point, component) && (dfControl == null || component.RenderOrder > dfControl.RenderOrder))
			{
				dfControl = component;
			}
		}
		return dfControl;
	}

	// Token: 0x060005BB RID: 1467 RVA: 0x0001B964 File Offset: 0x00019B64
	internal static bool isInsideClippingRegion(Vector3 point, dfControl control)
	{
		while (control != null)
		{
			Plane[] array = control.ClipChildren ? control.GetClippingPlanes() : null;
			if (array != null && array.Length != 0)
			{
				for (int i = 0; i < array.Length; i++)
				{
					if (!array[i].GetSide(point))
					{
						return false;
					}
				}
			}
			control = control.Parent;
		}
		return true;
	}

	// Token: 0x060005BC RID: 1468 RVA: 0x0001B9C0 File Offset: 0x00019BC0
	private static float combinedOpacity(dfControl control)
	{
		float num = 1f;
		while (control != null)
		{
			num *= control.Opacity;
			control = control.Parent;
		}
		return num;
	}

	// Token: 0x060005BE RID: 1470 RVA: 0x0001BA5B File Offset: 0x00019C5B
	// Note: this type is marked as 'beforefieldinit'.
	static dfInputManager()
	{
		KeyCode[] array = new KeyCode[8];
		RuntimeHelpers.InitializeArray(array, fieldof(<PrivateImplementationDetails>.AE50F2AB37F1ADE8ECE5FD08643A65E6415F828B93AF81FB54FB65A2B3ADFF32).FieldHandle);
		dfInputManager.wasd = array;
		dfInputManager.controlUnderMouse = null;
		dfInputManager.activeInstances = new dfList<dfInputManager>();
	}

	// Token: 0x040001EA RID: 490
	private static KeyCode[] wasd;

	// Token: 0x040001EB RID: 491
	private static dfControl controlUnderMouse;

	// Token: 0x040001EC RID: 492
	private static dfList<dfInputManager> activeInstances;

	// Token: 0x040001ED RID: 493
	[SerializeField]
	protected Camera renderCamera;

	// Token: 0x040001EE RID: 494
	[SerializeField]
	protected bool useTouch = true;

	// Token: 0x040001EF RID: 495
	[SerializeField]
	protected bool useMouse = true;

	// Token: 0x040001F0 RID: 496
	[SerializeField]
	protected bool useJoystick;

	// Token: 0x040001F1 RID: 497
	[SerializeField]
	protected KeyCode joystickClickButton = KeyCode.Joystick1Button1;

	// Token: 0x040001F2 RID: 498
	[SerializeField]
	protected string horizontalAxis = "Horizontal";

	// Token: 0x040001F3 RID: 499
	[SerializeField]
	protected string verticalAxis = "Vertical";

	// Token: 0x040001F4 RID: 500
	[SerializeField]
	protected float axisPollingInterval = 0.15f;

	// Token: 0x040001F5 RID: 501
	[SerializeField]
	protected bool retainFocus;

	// Token: 0x040001F6 RID: 502
	[HideInInspector]
	[SerializeField]
	protected int touchClickRadius = 125;

	// Token: 0x040001F7 RID: 503
	[SerializeField]
	protected float hoverStartDelay = 0.25f;

	// Token: 0x040001F8 RID: 504
	[SerializeField]
	protected float hoverNotifactionFrequency = 0.1f;

	// Token: 0x040001F9 RID: 505
	private IDFTouchInputSource touchInputSource;

	// Token: 0x040001FA RID: 506
	private dfInputManager.TouchInputManager touchHandler;

	// Token: 0x040001FB RID: 507
	private dfInputManager.MouseInputManager mouseHandler;

	// Token: 0x040001FC RID: 508
	private dfGUIManager guiManager;

	// Token: 0x040001FD RID: 509
	private dfControl buttonDownTarget;

	// Token: 0x040001FE RID: 510
	private IInputAdapter adapter;

	// Token: 0x040001FF RID: 511
	private float lastAxisCheck;

	// Token: 0x02000362 RID: 866
	private class TouchInputManager
	{
		// Token: 0x06001C4A RID: 7242 RVA: 0x000787C3 File Offset: 0x000769C3
		private TouchInputManager()
		{
		}

		// Token: 0x06001C4B RID: 7243 RVA: 0x000787E1 File Offset: 0x000769E1
		public TouchInputManager(dfInputManager manager)
		{
			this.manager = manager;
		}

		// Token: 0x06001C4C RID: 7244 RVA: 0x00078808 File Offset: 0x00076A08
		internal void Process(Transform transform, Camera renderCamera, IDFTouchInputSource input, bool retainFocusSetting)
		{
			dfInputManager.controlUnderMouse = null;
			IList<dfTouchInfo> touches = input.Touches;
			for (int i = 0; i < touches.Count; i++)
			{
				dfTouchInfo touch = touches[i];
				dfControl dfControl = dfGUIManager.HitTestAll(touch.position);
				if (dfControl != null && dfControl.transform.IsChildOf(this.manager.transform))
				{
					dfInputManager.controlUnderMouse = dfControl;
				}
				if (dfInputManager.controlUnderMouse == null && touch.phase == TouchPhase.Began)
				{
					if (!retainFocusSetting && this.untracked.Count == 0)
					{
						dfControl activeControl = dfGUIManager.ActiveControl;
						if (activeControl != null && activeControl.transform.IsChildOf(this.manager.transform))
						{
							activeControl.Unfocus();
						}
					}
					this.untracked.Add(touch.fingerId);
				}
				else if (this.untracked.Contains(touch.fingerId))
				{
					if (touch.phase == TouchPhase.Ended)
					{
						this.untracked.Remove(touch.fingerId);
					}
				}
				else
				{
					Ray ray = renderCamera.ScreenPointToRay(touch.position);
					dfInputManager.TouchInputManager.TouchRaycast info = new dfInputManager.TouchInputManager.TouchRaycast(dfInputManager.controlUnderMouse, touch, ray);
					dfInputManager.TouchInputManager.ControlTouchTracker controlTouchTracker = this.tracked.FirstOrDefault((dfInputManager.TouchInputManager.ControlTouchTracker x) => x.IsTrackingFinger(info.FingerID));
					if (controlTouchTracker != null)
					{
						controlTouchTracker.Process(info);
					}
					else
					{
						bool flag = false;
						for (int j = 0; j < this.tracked.Count; j++)
						{
							if (this.tracked[j].Process(info))
							{
								flag = true;
								break;
							}
						}
						if (!flag && dfInputManager.controlUnderMouse != null)
						{
							if (!this.tracked.Any((dfInputManager.TouchInputManager.ControlTouchTracker x) => x.control == dfInputManager.controlUnderMouse))
							{
								dfInputManager.TouchInputManager.ControlTouchTracker controlTouchTracker2 = new dfInputManager.TouchInputManager.ControlTouchTracker(this.manager, dfInputManager.controlUnderMouse);
								this.tracked.Add(controlTouchTracker2);
								controlTouchTracker2.Process(info);
							}
						}
					}
				}
			}
		}

		// Token: 0x06001C4D RID: 7245 RVA: 0x00078A24 File Offset: 0x00076C24
		private dfControl clipCast(Transform transform, RaycastHit[] hits)
		{
			if (hits == null || hits.Length == 0)
			{
				return null;
			}
			dfControl dfControl = null;
			dfControl modalControl = dfGUIManager.GetModalControl();
			for (int i = hits.Length - 1; i >= 0; i--)
			{
				RaycastHit hit = hits[i];
				dfControl component = hit.transform.GetComponent<dfControl>();
				if (!(component == null) && (!(modalControl != null) || component.transform.IsChildOf(modalControl.transform)) && component.enabled && component.Opacity >= 0.01f && component.IsEnabled && component.IsVisible && component.transform.IsChildOf(transform) && this.isInsideClippingRegion(hit, component) && (dfControl == null || component.RenderOrder > dfControl.RenderOrder))
				{
					dfControl = component;
				}
			}
			return dfControl;
		}

		// Token: 0x06001C4E RID: 7246 RVA: 0x00078AFC File Offset: 0x00076CFC
		private bool isInsideClippingRegion(RaycastHit hit, dfControl control)
		{
			Vector3 point = hit.point;
			while (control != null)
			{
				Plane[] array = control.ClipChildren ? control.GetClippingPlanes() : null;
				if (array != null && array.Length != 0)
				{
					for (int i = 0; i < array.Length; i++)
					{
						if (!array[i].GetSide(point))
						{
							return false;
						}
					}
				}
				control = control.Parent;
			}
			return true;
		}

		// Token: 0x0400160F RID: 5647
		private List<dfInputManager.TouchInputManager.ControlTouchTracker> tracked = new List<dfInputManager.TouchInputManager.ControlTouchTracker>();

		// Token: 0x04001610 RID: 5648
		private List<int> untracked = new List<int>();

		// Token: 0x04001611 RID: 5649
		private dfInputManager manager;

		// Token: 0x02000445 RID: 1093
		private class ControlTouchTracker
		{
			// Token: 0x170005A9 RID: 1449
			// (get) Token: 0x06001FCF RID: 8143 RVA: 0x000837DB File Offset: 0x000819DB
			public bool IsDragging
			{
				get
				{
					return this.dragState == dfDragDropState.Dragging;
				}
			}

			// Token: 0x170005AA RID: 1450
			// (get) Token: 0x06001FD0 RID: 8144 RVA: 0x000837E6 File Offset: 0x000819E6
			public int TouchCount
			{
				get
				{
					return this.touches.Count;
				}
			}

			// Token: 0x06001FD1 RID: 8145 RVA: 0x000837F3 File Offset: 0x000819F3
			public ControlTouchTracker(dfInputManager manager, dfControl control)
			{
				this.manager = manager;
				this.control = control;
				this.controlStartPosition = control.transform.position;
			}

			// Token: 0x06001FD2 RID: 8146 RVA: 0x00083830 File Offset: 0x00081A30
			public bool IsTrackingFinger(int fingerID)
			{
				return this.touches.ContainsKey(fingerID);
			}

			// Token: 0x06001FD3 RID: 8147 RVA: 0x00083840 File Offset: 0x00081A40
			public bool Process(dfInputManager.TouchInputManager.TouchRaycast info)
			{
				if (this.IsDragging)
				{
					if (!this.capture.Contains(info.FingerID))
					{
						return false;
					}
					if (info.Phase == TouchPhase.Stationary)
					{
						return true;
					}
					if (info.Phase == TouchPhase.Canceled)
					{
						this.control.OnDragEnd(new dfDragEventArgs(this.control, dfDragDropState.Cancelled, this.dragData, info.ray, info.position));
						this.dragState = dfDragDropState.None;
						this.touches.Clear();
						this.capture.Clear();
						return true;
					}
					if (info.Phase != TouchPhase.Ended)
					{
						return true;
					}
					if (info.control == null || info.control == this.control)
					{
						this.control.OnDragEnd(new dfDragEventArgs(this.control, dfDragDropState.CancelledNoTarget, this.dragData, info.ray, info.position));
						this.dragState = dfDragDropState.None;
						this.touches.Clear();
						this.capture.Clear();
						return true;
					}
					dfDragEventArgs dfDragEventArgs = new dfDragEventArgs(info.control, dfDragDropState.Dragging, this.dragData, info.ray, info.position);
					info.control.OnDragDrop(dfDragEventArgs);
					if (!dfDragEventArgs.Used || dfDragEventArgs.State != dfDragDropState.Dropped)
					{
						dfDragEventArgs.State = dfDragDropState.Cancelled;
					}
					dfDragEventArgs dfDragEventArgs2 = new dfDragEventArgs(this.control, dfDragEventArgs.State, this.dragData, info.ray, info.position);
					dfDragEventArgs2.Target = info.control;
					this.control.OnDragEnd(dfDragEventArgs2);
					this.dragState = dfDragDropState.None;
					this.touches.Clear();
					this.capture.Clear();
					return true;
				}
				else if (!this.touches.ContainsKey(info.FingerID))
				{
					if (info.control != this.control)
					{
						return false;
					}
					this.touches[info.FingerID] = info;
					if (this.touches.Count == 1)
					{
						this.control.OnMouseEnter(info);
						if (info.Phase == TouchPhase.Began)
						{
							this.capture.Add(info.FingerID);
							this.controlStartPosition = this.control.transform.position;
							this.control.OnMouseDown(info);
							if (Event.current != null)
							{
								Event.current.Use();
							}
						}
						return true;
					}
					if (info.Phase == TouchPhase.Began)
					{
						List<dfTouchInfo> activeTouches = this.getActiveTouches();
						dfTouchEventArgs args = new dfTouchEventArgs(this.control, activeTouches, info.ray);
						this.control.OnMultiTouch(args);
					}
					return true;
				}
				else
				{
					if (info.Phase == TouchPhase.Canceled || info.Phase == TouchPhase.Ended)
					{
						TouchPhase phase = info.Phase;
						dfInputManager.TouchInputManager.TouchRaycast touch = this.touches[info.FingerID];
						this.touches.Remove(info.FingerID);
						if (this.touches.Count == 0 && phase != TouchPhase.Canceled)
						{
							if (this.capture.Contains(info.FingerID))
							{
								if (this.canFireClickEvent(info, touch) && info.control == this.control)
								{
									if (info.touch.tapCount > 1)
									{
										this.control.OnDoubleClick(info);
									}
									else
									{
										this.control.OnClick(info);
									}
								}
								info.control = this.control;
								if (this.control)
								{
									this.control.OnMouseUp(info);
								}
							}
							this.capture.Remove(info.FingerID);
							return true;
						}
						this.capture.Remove(info.FingerID);
						if (this.touches.Count == 1)
						{
							this.control.OnMultiTouchEnd();
							return true;
						}
					}
					if (this.touches.Count > 1)
					{
						List<dfTouchInfo> activeTouches2 = this.getActiveTouches();
						dfTouchEventArgs args2 = new dfTouchEventArgs(this.control, activeTouches2, info.ray);
						this.control.OnMultiTouch(args2);
						return true;
					}
					if (!this.IsDragging && info.Phase == TouchPhase.Stationary)
					{
						if (info.control == this.control)
						{
							this.control.OnMouseHover(info);
							return true;
						}
						return false;
					}
					else
					{
						if (this.capture.Contains(info.FingerID) && this.dragState == dfDragDropState.None && info.Phase == TouchPhase.Moved)
						{
							dfDragEventArgs dfDragEventArgs3 = info;
							this.control.OnDragStart(dfDragEventArgs3);
							if (dfDragEventArgs3.State == dfDragDropState.Dragging && dfDragEventArgs3.Used)
							{
								this.dragState = dfDragDropState.Dragging;
								this.dragData = dfDragEventArgs3.Data;
								return true;
							}
							this.dragState = dfDragDropState.Denied;
						}
						if (info.control != this.control && !this.capture.Contains(info.FingerID))
						{
							info.control = this.control;
							this.control.OnMouseLeave(info);
							this.touches.Remove(info.FingerID);
							return true;
						}
						info.control = this.control;
						this.control.OnMouseMove(info);
						return true;
					}
				}
			}

			// Token: 0x06001FD4 RID: 8148 RVA: 0x00083D3C File Offset: 0x00081F3C
			private bool canFireClickEvent(dfInputManager.TouchInputManager.TouchRaycast info, dfInputManager.TouchInputManager.TouchRaycast touch)
			{
				if (this.control == null)
				{
					return false;
				}
				float d = this.control.PixelsToUnits();
				Vector3 a = this.controlStartPosition / d;
				Vector3 b = this.control.transform.position / d;
				return Vector3.Distance(a, b) <= 1f;
			}

			// Token: 0x06001FD5 RID: 8149 RVA: 0x00083D98 File Offset: 0x00081F98
			private List<dfTouchInfo> getActiveTouches()
			{
				IList<dfTouchInfo> list = this.manager.touchInputSource.Touches;
				List<dfTouchInfo> result = (from x in this.touches
				select x.Value.touch).ToList<dfTouchInfo>();
				int i = 0;
				Func<dfTouchInfo, bool> <>9__1;
				while (i < result.Count)
				{
					bool flag = false;
					int num = 0;
					while (i < list.Count)
					{
						if (list[num].fingerId == result[i].fingerId)
						{
							flag = true;
							break;
						}
						num++;
					}
					if (flag)
					{
						List<dfTouchInfo> result2 = result;
						int j = i;
						IEnumerable<dfTouchInfo> source = list;
						Func<dfTouchInfo, bool> predicate;
						if ((predicate = <>9__1) == null)
						{
							predicate = (<>9__1 = ((dfTouchInfo x) => x.fingerId == result[i].fingerId));
						}
						result2[j] = source.First(predicate);
						i++;
					}
					else
					{
						result.RemoveAt(i);
					}
				}
				return result;
			}

			// Token: 0x04001942 RID: 6466
			public readonly dfControl control;

			// Token: 0x04001943 RID: 6467
			public readonly Dictionary<int, dfInputManager.TouchInputManager.TouchRaycast> touches = new Dictionary<int, dfInputManager.TouchInputManager.TouchRaycast>();

			// Token: 0x04001944 RID: 6468
			public readonly List<int> capture = new List<int>();

			// Token: 0x04001945 RID: 6469
			private dfInputManager manager;

			// Token: 0x04001946 RID: 6470
			private Vector3 controlStartPosition;

			// Token: 0x04001947 RID: 6471
			private dfDragDropState dragState;

			// Token: 0x04001948 RID: 6472
			private object dragData;
		}

		// Token: 0x02000446 RID: 1094
		private class TouchRaycast
		{
			// Token: 0x170005AB RID: 1451
			// (get) Token: 0x06001FD6 RID: 8150 RVA: 0x00083EBD File Offset: 0x000820BD
			public int FingerID
			{
				get
				{
					return this.touch.fingerId;
				}
			}

			// Token: 0x170005AC RID: 1452
			// (get) Token: 0x06001FD7 RID: 8151 RVA: 0x00083ECA File Offset: 0x000820CA
			public TouchPhase Phase
			{
				get
				{
					return this.touch.phase;
				}
			}

			// Token: 0x06001FD8 RID: 8152 RVA: 0x00083ED7 File Offset: 0x000820D7
			public TouchRaycast(dfControl control, dfTouchInfo touch, Ray ray)
			{
				this.control = control;
				this.touch = touch;
				this.ray = ray;
				this.position = touch.position;
			}

			// Token: 0x06001FD9 RID: 8153 RVA: 0x00083F01 File Offset: 0x00082101
			public static implicit operator dfTouchEventArgs(dfInputManager.TouchInputManager.TouchRaycast touch)
			{
				return new dfTouchEventArgs(touch.control, touch.touch, touch.ray);
			}

			// Token: 0x06001FDA RID: 8154 RVA: 0x00083F1A File Offset: 0x0008211A
			public static implicit operator dfDragEventArgs(dfInputManager.TouchInputManager.TouchRaycast touch)
			{
				return new dfDragEventArgs(touch.control, dfDragDropState.None, null, touch.ray, touch.position);
			}

			// Token: 0x04001949 RID: 6473
			public dfControl control;

			// Token: 0x0400194A RID: 6474
			public dfTouchInfo touch;

			// Token: 0x0400194B RID: 6475
			public Ray ray;

			// Token: 0x0400194C RID: 6476
			public Vector2 position;
		}
	}

	// Token: 0x02000363 RID: 867
	private class MouseInputManager
	{
		// Token: 0x06001C4F RID: 7247 RVA: 0x00078B60 File Offset: 0x00076D60
		public void ProcessInput(dfInputManager manager, IInputAdapter adapter, Ray ray, dfControl control, bool retainFocusSetting)
		{
			Vector2 mousePosition = adapter.GetMousePosition();
			this.buttonsDown = dfMouseButtons.None;
			this.buttonsReleased = dfMouseButtons.None;
			this.buttonsPressed = dfMouseButtons.None;
			dfInputManager.MouseInputManager.getMouseButtonInfo(adapter, ref this.buttonsDown, ref this.buttonsReleased, ref this.buttonsPressed);
			float num = adapter.GetAxis("Mouse ScrollWheel");
			if (!Mathf.Approximately(num, 0f))
			{
				num = Mathf.Sign(num) * Mathf.Max(1f, Mathf.Abs(num));
			}
			this.mouseMoveDelta = mousePosition - this.lastPosition;
			this.lastPosition = mousePosition;
			if (this.dragState == dfDragDropState.Dragging)
			{
				if (this.buttonsReleased != dfMouseButtons.None)
				{
					if (control != null && control != this.activeControl)
					{
						dfDragEventArgs dfDragEventArgs = new dfDragEventArgs(control, dfDragDropState.Dragging, this.dragData, ray, mousePosition);
						control.OnDragDrop(dfDragEventArgs);
						if (!dfDragEventArgs.Used || dfDragEventArgs.State == dfDragDropState.Dragging)
						{
							dfDragEventArgs.State = dfDragDropState.Cancelled;
						}
						dfDragEventArgs = new dfDragEventArgs(this.activeControl, dfDragEventArgs.State, dfDragEventArgs.Data, ray, mousePosition);
						dfDragEventArgs.Target = control;
						this.activeControl.OnDragEnd(dfDragEventArgs);
					}
					else
					{
						dfDragDropState state = (control == null) ? dfDragDropState.CancelledNoTarget : dfDragDropState.Cancelled;
						dfDragEventArgs args = new dfDragEventArgs(this.activeControl, state, this.dragData, ray, mousePosition);
						this.activeControl.OnDragEnd(args);
					}
					this.dragState = dfDragDropState.None;
					this.lastDragControl = null;
					this.activeControl = null;
					this.lastClickTime = 0f;
					this.lastHoverTime = 0f;
					this.lastPosition = mousePosition;
					return;
				}
				if (control == this.activeControl)
				{
					return;
				}
				if (control != this.lastDragControl)
				{
					if (this.lastDragControl != null)
					{
						dfDragEventArgs args2 = new dfDragEventArgs(this.lastDragControl, this.dragState, this.dragData, ray, mousePosition);
						this.lastDragControl.OnDragLeave(args2);
					}
					if (control != null)
					{
						dfDragEventArgs args3 = new dfDragEventArgs(control, this.dragState, this.dragData, ray, mousePosition);
						control.OnDragEnter(args3);
					}
					this.lastDragControl = control;
					return;
				}
				if (control != null && this.mouseMoveDelta.magnitude > 1f)
				{
					dfDragEventArgs args4 = new dfDragEventArgs(control, this.dragState, this.dragData, ray, mousePosition);
					control.OnDragOver(args4);
				}
				return;
			}
			else
			{
				if (this.buttonsPressed != dfMouseButtons.None)
				{
					this.lastHoverTime = Time.realtimeSinceStartup + manager.hoverStartDelay;
					if (this.activeControl != null)
					{
						if (this.activeControl.transform.IsChildOf(manager.transform))
						{
							this.activeControl.OnMouseDown(new dfMouseEventArgs(this.activeControl, this.buttonsPressed, 0, ray, mousePosition, num));
						}
					}
					else if (control == null || control.transform.IsChildOf(manager.transform))
					{
						this.setActive(manager, control, mousePosition, ray);
						if (control != null)
						{
							dfGUIManager.SetFocus(control);
							control.OnMouseDown(new dfMouseEventArgs(control, this.buttonsPressed, 0, ray, mousePosition, num));
						}
						else if (!retainFocusSetting)
						{
							dfControl dfControl = dfGUIManager.ActiveControl;
							if (dfControl != null && dfControl.transform.IsChildOf(manager.transform))
							{
								dfControl.Unfocus();
							}
						}
					}
					if (this.buttonsReleased == dfMouseButtons.None)
					{
						return;
					}
				}
				if (this.buttonsReleased == dfMouseButtons.None)
				{
					if (this.activeControl != null && this.activeControl == control && this.mouseMoveDelta.magnitude == 0f && Time.realtimeSinceStartup - this.lastHoverTime > manager.hoverNotifactionFrequency)
					{
						this.activeControl.OnMouseHover(new dfMouseEventArgs(this.activeControl, this.buttonsDown, 0, ray, mousePosition, num));
						this.lastHoverTime = Time.realtimeSinceStartup;
					}
					if (this.buttonsDown == dfMouseButtons.None)
					{
						if (num != 0f && control != null)
						{
							this.setActive(manager, control, mousePosition, ray);
							control.OnMouseWheel(new dfMouseEventArgs(control, this.buttonsDown, 0, ray, mousePosition, num));
							return;
						}
						this.setActive(manager, control, mousePosition, ray);
					}
					else if (this.buttonsDown != dfMouseButtons.None && this.activeControl != null)
					{
						if (control != null)
						{
							int renderOrder = control.RenderOrder;
							int renderOrder2 = this.activeControl.RenderOrder;
						}
						if (this.mouseMoveDelta.magnitude >= 2f && (this.buttonsDown & (dfMouseButtons.Left | dfMouseButtons.Right)) != dfMouseButtons.None && this.dragState != dfDragDropState.Denied)
						{
							dfDragEventArgs dfDragEventArgs2 = new dfDragEventArgs(this.activeControl)
							{
								Position = mousePosition
							};
							this.activeControl.OnDragStart(dfDragEventArgs2);
							if (dfDragEventArgs2.State == dfDragDropState.Dragging)
							{
								this.dragState = dfDragDropState.Dragging;
								this.dragData = dfDragEventArgs2.Data;
								return;
							}
							this.dragState = dfDragDropState.Denied;
						}
					}
					if (this.activeControl != null && this.mouseMoveDelta.magnitude >= 1f)
					{
						dfMouseEventArgs args5 = new dfMouseEventArgs(this.activeControl, this.buttonsDown, 0, ray, mousePosition, num)
						{
							MoveDelta = this.mouseMoveDelta
						};
						this.activeControl.OnMouseMove(args5);
					}
					return;
				}
				this.lastHoverTime = Time.realtimeSinceStartup + manager.hoverStartDelay;
				if (this.activeControl == null)
				{
					this.setActive(manager, control, mousePosition, ray);
					return;
				}
				if (this.activeControl == control && this.buttonsDown == dfMouseButtons.None)
				{
					float d = this.activeControl.PixelsToUnits();
					Vector3 a = this.activeControlPosition / d;
					Vector3 b = this.activeControl.transform.position / d;
					if (Vector3.Distance(a, b) <= 1f)
					{
						if (Time.realtimeSinceStartup - this.lastClickTime < 0.25f)
						{
							this.lastClickTime = 0f;
							this.activeControl.OnDoubleClick(new dfMouseEventArgs(this.activeControl, this.buttonsReleased, 1, ray, mousePosition, num));
						}
						else
						{
							this.lastClickTime = Time.realtimeSinceStartup;
							this.activeControl.OnClick(new dfMouseEventArgs(this.activeControl, this.buttonsReleased, 1, ray, mousePosition, num));
						}
					}
				}
				this.activeControl.OnMouseUp(new dfMouseEventArgs(this.activeControl, this.buttonsReleased, 0, ray, mousePosition, num));
				if (this.buttonsDown == dfMouseButtons.None && this.activeControl != control)
				{
					this.setActive(manager, null, mousePosition, ray);
				}
				return;
			}
		}

		// Token: 0x06001C50 RID: 7248 RVA: 0x0007919C File Offset: 0x0007739C
		private static void getMouseButtonInfo(IInputAdapter adapter, ref dfMouseButtons buttonsDown, ref dfMouseButtons buttonsReleased, ref dfMouseButtons buttonsPressed)
		{
			for (int i = 0; i < 3; i++)
			{
				if (adapter.GetMouseButton(i))
				{
					buttonsDown |= (dfMouseButtons)(1 << i);
				}
				if (adapter.GetMouseButtonUp(i))
				{
					buttonsReleased |= (dfMouseButtons)(1 << i);
				}
				if (adapter.GetMouseButtonDown(i))
				{
					buttonsPressed |= (dfMouseButtons)(1 << i);
				}
			}
		}

		// Token: 0x06001C51 RID: 7249 RVA: 0x000791F4 File Offset: 0x000773F4
		private void setActive(dfInputManager manager, dfControl control, Vector2 position, Ray ray)
		{
			if (this.activeControl != null && this.activeControl != control)
			{
				this.activeControl.OnMouseLeave(new dfMouseEventArgs(this.activeControl)
				{
					Position = position,
					Ray = ray
				});
			}
			if (control != null && control != this.activeControl)
			{
				this.lastClickTime = 0f;
				this.lastHoverTime = Time.realtimeSinceStartup + manager.hoverStartDelay;
				control.OnMouseEnter(new dfMouseEventArgs(control)
				{
					Position = position,
					Ray = ray
				});
			}
			this.activeControl = control;
			this.activeControlPosition = ((control != null) ? control.transform.position : (Vector3.one * float.MinValue));
			this.lastPosition = position;
			this.dragState = dfDragDropState.None;
		}

		// Token: 0x04001612 RID: 5650
		private const string scrollAxisName = "Mouse ScrollWheel";

		// Token: 0x04001613 RID: 5651
		private const float DOUBLECLICK_TIME = 0.25f;

		// Token: 0x04001614 RID: 5652
		private const int DRAG_START_DELTA = 2;

		// Token: 0x04001615 RID: 5653
		private dfControl activeControl;

		// Token: 0x04001616 RID: 5654
		private Vector3 activeControlPosition;

		// Token: 0x04001617 RID: 5655
		private Vector2 lastPosition = Vector2.one * -2.1474836E+09f;

		// Token: 0x04001618 RID: 5656
		private Vector2 mouseMoveDelta = Vector2.zero;

		// Token: 0x04001619 RID: 5657
		private float lastClickTime;

		// Token: 0x0400161A RID: 5658
		private float lastHoverTime;

		// Token: 0x0400161B RID: 5659
		private dfDragDropState dragState;

		// Token: 0x0400161C RID: 5660
		private object dragData;

		// Token: 0x0400161D RID: 5661
		private dfControl lastDragControl;

		// Token: 0x0400161E RID: 5662
		private dfMouseButtons buttonsDown;

		// Token: 0x0400161F RID: 5663
		private dfMouseButtons buttonsReleased;

		// Token: 0x04001620 RID: 5664
		private dfMouseButtons buttonsPressed;
	}

	// Token: 0x02000364 RID: 868
	private class DefaultInput : IInputAdapter
	{
		// Token: 0x06001C53 RID: 7251 RVA: 0x000792F9 File Offset: 0x000774F9
		public bool GetKeyDown(KeyCode key)
		{
			return Input.GetKeyDown(key);
		}

		// Token: 0x06001C54 RID: 7252 RVA: 0x00079301 File Offset: 0x00077501
		public bool GetKeyUp(KeyCode key)
		{
			return Input.GetKeyUp(key);
		}

		// Token: 0x06001C55 RID: 7253 RVA: 0x00079309 File Offset: 0x00077509
		public float GetAxis(string axisName)
		{
			return Input.GetAxis(axisName);
		}

		// Token: 0x06001C56 RID: 7254 RVA: 0x00079311 File Offset: 0x00077511
		public Vector2 GetMousePosition()
		{
			return Input.mousePosition;
		}

		// Token: 0x06001C57 RID: 7255 RVA: 0x0007931D File Offset: 0x0007751D
		public bool GetMouseButton(int button)
		{
			return Input.GetMouseButton(button);
		}

		// Token: 0x06001C58 RID: 7256 RVA: 0x00079325 File Offset: 0x00077525
		public bool GetMouseButtonDown(int button)
		{
			return Input.GetMouseButtonDown(button);
		}

		// Token: 0x06001C59 RID: 7257 RVA: 0x0007932D File Offset: 0x0007752D
		public bool GetMouseButtonUp(int button)
		{
			return Input.GetMouseButtonUp(button);
		}
	}
}

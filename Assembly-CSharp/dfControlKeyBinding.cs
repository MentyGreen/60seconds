using System;
using UnityEngine;

// Token: 0x020000A1 RID: 161
[AddComponentMenu("Daikon Forge/Data Binding/Key Binding")]
[Serializable]
public class dfControlKeyBinding : MonoBehaviour, IDataBindingComponent
{
	// Token: 0x17000213 RID: 531
	// (get) Token: 0x0600095F RID: 2399 RVA: 0x00029ACA File Offset: 0x00027CCA
	// (set) Token: 0x06000960 RID: 2400 RVA: 0x00029AD2 File Offset: 0x00027CD2
	public dfControl Control
	{
		get
		{
			return this.control;
		}
		set
		{
			if (this.isBound)
			{
				this.Unbind();
			}
			this.control = value;
		}
	}

	// Token: 0x17000214 RID: 532
	// (get) Token: 0x06000961 RID: 2401 RVA: 0x00029AE9 File Offset: 0x00027CE9
	// (set) Token: 0x06000962 RID: 2402 RVA: 0x00029AF1 File Offset: 0x00027CF1
	public KeyCode KeyCode
	{
		get
		{
			return this.keyCode;
		}
		set
		{
			this.keyCode = value;
		}
	}

	// Token: 0x17000215 RID: 533
	// (get) Token: 0x06000963 RID: 2403 RVA: 0x00029AFA File Offset: 0x00027CFA
	// (set) Token: 0x06000964 RID: 2404 RVA: 0x00029B02 File Offset: 0x00027D02
	public bool AltPressed
	{
		get
		{
			return this.altPressed;
		}
		set
		{
			this.altPressed = value;
		}
	}

	// Token: 0x17000216 RID: 534
	// (get) Token: 0x06000965 RID: 2405 RVA: 0x00029B0B File Offset: 0x00027D0B
	// (set) Token: 0x06000966 RID: 2406 RVA: 0x00029B13 File Offset: 0x00027D13
	public bool ControlPressed
	{
		get
		{
			return this.controlPressed;
		}
		set
		{
			this.controlPressed = value;
		}
	}

	// Token: 0x17000217 RID: 535
	// (get) Token: 0x06000967 RID: 2407 RVA: 0x00029B1C File Offset: 0x00027D1C
	// (set) Token: 0x06000968 RID: 2408 RVA: 0x00029B24 File Offset: 0x00027D24
	public bool ShiftPressed
	{
		get
		{
			return this.shiftPressed;
		}
		set
		{
			this.shiftPressed = value;
		}
	}

	// Token: 0x17000218 RID: 536
	// (get) Token: 0x06000969 RID: 2409 RVA: 0x00029B2D File Offset: 0x00027D2D
	// (set) Token: 0x0600096A RID: 2410 RVA: 0x00029B35 File Offset: 0x00027D35
	public dfComponentMemberInfo Target
	{
		get
		{
			return this.target;
		}
		set
		{
			if (this.isBound)
			{
				this.Unbind();
			}
			this.target = value;
		}
	}

	// Token: 0x17000219 RID: 537
	// (get) Token: 0x0600096B RID: 2411 RVA: 0x00029B4C File Offset: 0x00027D4C
	public bool IsBound
	{
		get
		{
			return this.isBound;
		}
	}

	// Token: 0x0600096C RID: 2412 RVA: 0x00029B54 File Offset: 0x00027D54
	public void Awake()
	{
	}

	// Token: 0x0600096D RID: 2413 RVA: 0x00029B56 File Offset: 0x00027D56
	public void OnEnable()
	{
	}

	// Token: 0x0600096E RID: 2414 RVA: 0x00029B58 File Offset: 0x00027D58
	public void Start()
	{
		if (this.control != null && this.target.IsValid)
		{
			this.Bind();
		}
	}

	// Token: 0x0600096F RID: 2415 RVA: 0x00029B7B File Offset: 0x00027D7B
	public void Bind()
	{
		if (this.isBound)
		{
			this.Unbind();
		}
		if (this.control != null)
		{
			this.control.KeyDown += this.eventSource_KeyDown;
		}
		this.isBound = true;
	}

	// Token: 0x06000970 RID: 2416 RVA: 0x00029BB7 File Offset: 0x00027DB7
	public void Unbind()
	{
		if (this.control != null)
		{
			this.control.KeyDown -= this.eventSource_KeyDown;
		}
		this.isBound = false;
	}

	// Token: 0x06000971 RID: 2417 RVA: 0x00029BE8 File Offset: 0x00027DE8
	private void eventSource_KeyDown(dfControl sourceControl, dfKeyEventArgs args)
	{
		if (args.KeyCode != this.keyCode)
		{
			return;
		}
		if (args.Shift != this.shiftPressed || args.Control != this.controlPressed || args.Alt != this.altPressed)
		{
			return;
		}
		this.target.GetMethod().Invoke(this.target.Component, null);
	}

	// Token: 0x0400047B RID: 1147
	[SerializeField]
	protected dfControl control;

	// Token: 0x0400047C RID: 1148
	[SerializeField]
	protected KeyCode keyCode;

	// Token: 0x0400047D RID: 1149
	[SerializeField]
	protected bool shiftPressed;

	// Token: 0x0400047E RID: 1150
	[SerializeField]
	protected bool altPressed;

	// Token: 0x0400047F RID: 1151
	[SerializeField]
	protected bool controlPressed;

	// Token: 0x04000480 RID: 1152
	[SerializeField]
	protected dfComponentMemberInfo target;

	// Token: 0x04000481 RID: 1153
	private bool isBound;
}

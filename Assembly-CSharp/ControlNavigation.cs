using System;
using UnityEngine;

// Token: 0x020000AC RID: 172
[AddComponentMenu("Daikon Forge/Examples/General/Control Navigation")]
public class ControlNavigation : MonoBehaviour
{
	// Token: 0x06000A27 RID: 2599 RVA: 0x0002C500 File Offset: 0x0002A700
	private void OnMouseEnter(dfControl sender, dfMouseEventArgs args)
	{
		if (this.FocusOnMouseEnter)
		{
			dfControl component = base.GetComponent<dfControl>();
			if (component != null)
			{
				component.Focus();
			}
		}
	}

	// Token: 0x06000A28 RID: 2600 RVA: 0x0002C52B File Offset: 0x0002A72B
	private void OnClick(dfControl sender, dfMouseEventArgs args)
	{
		if (this.SelectOnClick != null)
		{
			this.SelectOnClick.Focus();
		}
	}

	// Token: 0x06000A29 RID: 2601 RVA: 0x0002C548 File Offset: 0x0002A748
	private void OnKeyDown(dfControl sender, dfKeyEventArgs args)
	{
		KeyCode keyCode = args.KeyCode;
		if (keyCode != KeyCode.Tab)
		{
			switch (keyCode)
			{
			case KeyCode.UpArrow:
				if (this.SelectOnUp != null)
				{
					this.SelectOnUp.Focus();
					args.Use();
					return;
				}
				break;
			case KeyCode.DownArrow:
				if (this.SelectOnDown != null)
				{
					this.SelectOnDown.Focus();
					args.Use();
				}
				break;
			case KeyCode.RightArrow:
				if (this.SelectOnRight != null)
				{
					this.SelectOnRight.Focus();
					args.Use();
					return;
				}
				break;
			case KeyCode.LeftArrow:
				if (this.SelectOnLeft != null)
				{
					this.SelectOnLeft.Focus();
					args.Use();
					return;
				}
				break;
			default:
				return;
			}
		}
		else if (args.Shift)
		{
			if (this.SelectOnShiftTab != null)
			{
				this.SelectOnShiftTab.Focus();
				args.Use();
				return;
			}
		}
		else if (this.SelectOnTab != null)
		{
			this.SelectOnTab.Focus();
			args.Use();
			return;
		}
	}

	// Token: 0x06000A2A RID: 2602 RVA: 0x0002C64B File Offset: 0x0002A84B
	private void Awake()
	{
	}

	// Token: 0x06000A2B RID: 2603 RVA: 0x0002C64D File Offset: 0x0002A84D
	private void OnEnable()
	{
	}

	// Token: 0x06000A2C RID: 2604 RVA: 0x0002C650 File Offset: 0x0002A850
	private void Start()
	{
		if (this.FocusOnStart)
		{
			dfControl component = base.GetComponent<dfControl>();
			if (component != null)
			{
				component.Focus();
			}
		}
	}

	// Token: 0x040004CA RID: 1226
	public bool FocusOnStart;

	// Token: 0x040004CB RID: 1227
	public bool FocusOnMouseEnter;

	// Token: 0x040004CC RID: 1228
	public dfControl SelectOnLeft;

	// Token: 0x040004CD RID: 1229
	public dfControl SelectOnRight;

	// Token: 0x040004CE RID: 1230
	public dfControl SelectOnUp;

	// Token: 0x040004CF RID: 1231
	public dfControl SelectOnDown;

	// Token: 0x040004D0 RID: 1232
	public dfControl SelectOnTab;

	// Token: 0x040004D1 RID: 1233
	public dfControl SelectOnShiftTab;

	// Token: 0x040004D2 RID: 1234
	public dfControl SelectOnClick;
}

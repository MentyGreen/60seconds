using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000010 RID: 16
[ExecuteInEditMode]
[AddComponentMenu("Daikon Forge/User Interface/Panel Addon/Flow Layout")]
public class dfPanelFlowLayout : MonoBehaviour
{
	// Token: 0x170000A4 RID: 164
	// (get) Token: 0x060002D1 RID: 721 RVA: 0x0000D7B2 File Offset: 0x0000B9B2
	// (set) Token: 0x060002D2 RID: 722 RVA: 0x0000D7BA File Offset: 0x0000B9BA
	public dfControlOrientation Direction
	{
		get
		{
			return this.flowDirection;
		}
		set
		{
			if (value != this.flowDirection)
			{
				this.flowDirection = value;
				this.PerformLayout();
			}
		}
	}

	// Token: 0x170000A5 RID: 165
	// (get) Token: 0x060002D3 RID: 723 RVA: 0x0000D7D2 File Offset: 0x0000B9D2
	// (set) Token: 0x060002D4 RID: 724 RVA: 0x0000D7DA File Offset: 0x0000B9DA
	public Vector2 ItemSpacing
	{
		get
		{
			return this.itemSpacing;
		}
		set
		{
			value = Vector2.Max(value, Vector2.zero);
			if (!object.Equals(value, this.itemSpacing))
			{
				this.itemSpacing = value;
				this.PerformLayout();
			}
		}
	}

	// Token: 0x170000A6 RID: 166
	// (get) Token: 0x060002D5 RID: 725 RVA: 0x0000D80E File Offset: 0x0000BA0E
	// (set) Token: 0x060002D6 RID: 726 RVA: 0x0000D829 File Offset: 0x0000BA29
	public RectOffset BorderPadding
	{
		get
		{
			if (this.borderPadding == null)
			{
				this.borderPadding = new RectOffset();
			}
			return this.borderPadding;
		}
		set
		{
			value = value.ConstrainPadding();
			if (!object.Equals(value, this.borderPadding))
			{
				this.borderPadding = value;
				this.PerformLayout();
			}
		}
	}

	// Token: 0x170000A7 RID: 167
	// (get) Token: 0x060002D7 RID: 727 RVA: 0x0000D84E File Offset: 0x0000BA4E
	// (set) Token: 0x060002D8 RID: 728 RVA: 0x0000D856 File Offset: 0x0000BA56
	public bool HideClippedControls
	{
		get
		{
			return this.hideClippedControls;
		}
		set
		{
			if (value != this.hideClippedControls)
			{
				this.hideClippedControls = value;
				this.PerformLayout();
			}
		}
	}

	// Token: 0x170000A8 RID: 168
	// (get) Token: 0x060002D9 RID: 729 RVA: 0x0000D86E File Offset: 0x0000BA6E
	// (set) Token: 0x060002DA RID: 730 RVA: 0x0000D876 File Offset: 0x0000BA76
	public int MaxLayoutSize
	{
		get
		{
			return this.maxLayoutSize;
		}
		set
		{
			if (value != this.maxLayoutSize)
			{
				this.maxLayoutSize = value;
				this.PerformLayout();
			}
		}
	}

	// Token: 0x170000A9 RID: 169
	// (get) Token: 0x060002DB RID: 731 RVA: 0x0000D88E File Offset: 0x0000BA8E
	public List<dfControl> ExcludedControls
	{
		get
		{
			return this.excludedControls;
		}
	}

	// Token: 0x060002DC RID: 732 RVA: 0x0000D898 File Offset: 0x0000BA98
	public void OnEnable()
	{
		this.panel = base.GetComponent<dfPanel>();
		if (this.panel == null)
		{
			Debug.LogError("The " + base.GetType().Name + " component requires a dfPanel component.", base.gameObject);
			base.enabled = false;
			return;
		}
		this.panel.SizeChanged += this.OnSizeChanged;
	}

	// Token: 0x060002DD RID: 733 RVA: 0x0000D903 File Offset: 0x0000BB03
	public void OnDisable()
	{
		if (this.panel != null)
		{
			this.panel.SizeChanged -= this.OnSizeChanged;
			this.panel = null;
		}
	}

	// Token: 0x060002DE RID: 734 RVA: 0x0000D931 File Offset: 0x0000BB31
	public void OnControlAdded(dfControl container, dfControl child)
	{
		child.ZOrderChanged += this.child_ZOrderChanged;
		child.SizeChanged += this.child_SizeChanged;
		this.PerformLayout();
	}

	// Token: 0x060002DF RID: 735 RVA: 0x0000D95D File Offset: 0x0000BB5D
	public void OnControlRemoved(dfControl container, dfControl child)
	{
		child.ZOrderChanged -= this.child_ZOrderChanged;
		child.SizeChanged -= this.child_SizeChanged;
		this.PerformLayout();
	}

	// Token: 0x060002E0 RID: 736 RVA: 0x0000D989 File Offset: 0x0000BB89
	public void OnSizeChanged(dfControl control, Vector2 value)
	{
		this.PerformLayout();
	}

	// Token: 0x060002E1 RID: 737 RVA: 0x0000D991 File Offset: 0x0000BB91
	private void child_SizeChanged(dfControl control, Vector2 value)
	{
		this.PerformLayout();
	}

	// Token: 0x060002E2 RID: 738 RVA: 0x0000D999 File Offset: 0x0000BB99
	private void child_ZOrderChanged(dfControl control, int value)
	{
		this.PerformLayout();
	}

	// Token: 0x060002E3 RID: 739 RVA: 0x0000D9A4 File Offset: 0x0000BBA4
	public void PerformLayout()
	{
		if (this.panel == null)
		{
			this.panel = base.GetComponent<dfPanel>();
		}
		Vector3 vector = new Vector3((float)this.borderPadding.left, (float)this.borderPadding.top);
		bool flag = true;
		float num = (this.flowDirection == dfControlOrientation.Horizontal && this.maxLayoutSize > 0) ? ((float)this.maxLayoutSize) : (this.panel.Width - (float)this.borderPadding.right);
		float num2 = (this.flowDirection == dfControlOrientation.Vertical && this.maxLayoutSize > 0) ? ((float)this.maxLayoutSize) : (this.panel.Height - (float)this.borderPadding.bottom);
		int num3 = 0;
		dfList<dfControl> controls = this.panel.Controls;
		int i = 0;
		while (i < controls.Count)
		{
			dfControl dfControl = controls[i];
			if (dfControl.enabled && dfControl.gameObject.activeSelf && !this.excludedControls.Contains(dfControl))
			{
				if (!flag)
				{
					if (this.flowDirection == dfControlOrientation.Horizontal)
					{
						vector.x += this.itemSpacing.x;
					}
					else
					{
						vector.y += this.itemSpacing.y;
					}
				}
				if (this.flowDirection == dfControlOrientation.Horizontal)
				{
					if (!flag && vector.x + dfControl.Width > num + 1E-45f)
					{
						vector.x = (float)this.borderPadding.left;
						vector.y += (float)num3;
						num3 = 0;
					}
				}
				else if (!flag && vector.y + dfControl.Height > num2 + 1E-45f)
				{
					vector.y = (float)this.borderPadding.top;
					vector.x += (float)num3;
					num3 = 0;
				}
				dfControl.RelativePosition = vector;
				if (this.flowDirection == dfControlOrientation.Horizontal)
				{
					vector.x += dfControl.Width;
					num3 = Mathf.Max(Mathf.CeilToInt(dfControl.Height + this.itemSpacing.y), num3);
				}
				else
				{
					vector.y += dfControl.Height;
					num3 = Mathf.Max(Mathf.CeilToInt(dfControl.Width + this.itemSpacing.x), num3);
				}
				dfControl.IsVisible = this.canShowControlUnclipped(dfControl);
			}
			i++;
			flag = false;
		}
	}

	// Token: 0x060002E4 RID: 740 RVA: 0x0000DC08 File Offset: 0x0000BE08
	private bool canShowControlUnclipped(dfControl control)
	{
		if (!this.hideClippedControls)
		{
			return true;
		}
		Vector3 relativePosition = control.RelativePosition;
		return relativePosition.x + control.Width < this.panel.Width - (float)this.borderPadding.right && relativePosition.y + control.Height < this.panel.Height - (float)this.borderPadding.bottom;
	}

	// Token: 0x040000FE RID: 254
	[SerializeField]
	protected RectOffset borderPadding = new RectOffset();

	// Token: 0x040000FF RID: 255
	[SerializeField]
	protected Vector2 itemSpacing;

	// Token: 0x04000100 RID: 256
	[SerializeField]
	protected dfControlOrientation flowDirection;

	// Token: 0x04000101 RID: 257
	[SerializeField]
	protected bool hideClippedControls;

	// Token: 0x04000102 RID: 258
	[SerializeField]
	protected int maxLayoutSize;

	// Token: 0x04000103 RID: 259
	[SerializeField]
	protected List<dfControl> excludedControls = new List<dfControl>();

	// Token: 0x04000104 RID: 260
	private dfPanel panel;
}

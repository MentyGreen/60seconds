using System;
using UnityEngine;

// Token: 0x02000019 RID: 25
[dfCategory("Basic Controls")]
[dfTooltip("Used in conjunction with the dfTabContainer class to implement tabbed containers. This control maintains the tabs that are displayed for the user to select, and the dfTabContainer class manages the display of the tab pages themselves.")]
[dfHelp("http://www.daikonforge.com/docs/df-gui/classdf_tab_container.html")]
[ExecuteInEditMode]
[AddComponentMenu("Daikon Forge/User Interface/Containers/Tab Control/Tab Page Container")]
[Serializable]
public class dfTabContainer : dfControl
{
	// Token: 0x14000035 RID: 53
	// (add) Token: 0x06000409 RID: 1033 RVA: 0x00014914 File Offset: 0x00012B14
	// (remove) Token: 0x0600040A RID: 1034 RVA: 0x0001494C File Offset: 0x00012B4C
	public event PropertyChangedEventHandler<int> SelectedIndexChanged;

	// Token: 0x170000F1 RID: 241
	// (get) Token: 0x0600040B RID: 1035 RVA: 0x00014984 File Offset: 0x00012B84
	// (set) Token: 0x0600040C RID: 1036 RVA: 0x000149C5 File Offset: 0x00012BC5
	public dfAtlas Atlas
	{
		get
		{
			if (this.atlas == null)
			{
				dfGUIManager manager = base.GetManager();
				if (manager != null)
				{
					return this.atlas = manager.DefaultAtlas;
				}
			}
			return this.atlas;
		}
		set
		{
			if (!dfAtlas.Equals(value, this.atlas))
			{
				this.atlas = value;
				this.Invalidate();
			}
		}
	}

	// Token: 0x170000F2 RID: 242
	// (get) Token: 0x0600040D RID: 1037 RVA: 0x000149E2 File Offset: 0x00012BE2
	// (set) Token: 0x0600040E RID: 1038 RVA: 0x000149EA File Offset: 0x00012BEA
	public string BackgroundSprite
	{
		get
		{
			return this.backgroundSprite;
		}
		set
		{
			if (value != this.backgroundSprite)
			{
				this.backgroundSprite = value;
				this.Invalidate();
			}
		}
	}

	// Token: 0x170000F3 RID: 243
	// (get) Token: 0x0600040F RID: 1039 RVA: 0x00014A07 File Offset: 0x00012C07
	// (set) Token: 0x06000410 RID: 1040 RVA: 0x00014A22 File Offset: 0x00012C22
	public RectOffset Padding
	{
		get
		{
			if (this.padding == null)
			{
				this.padding = new RectOffset();
			}
			return this.padding;
		}
		set
		{
			value = value.ConstrainPadding();
			if (!object.Equals(value, this.padding))
			{
				this.padding = value;
				this.arrangeTabPages();
			}
		}
	}

	// Token: 0x170000F4 RID: 244
	// (get) Token: 0x06000411 RID: 1041 RVA: 0x00014A47 File Offset: 0x00012C47
	// (set) Token: 0x06000412 RID: 1042 RVA: 0x00014A4F File Offset: 0x00012C4F
	public int SelectedIndex
	{
		get
		{
			return this.selectedIndex;
		}
		set
		{
			if (value != this.selectedIndex)
			{
				this.selectPageByIndex(value);
			}
		}
	}

	// Token: 0x06000413 RID: 1043 RVA: 0x00014A64 File Offset: 0x00012C64
	public dfControl AddTabPage()
	{
		dfPanel dfPanel = (from i in this.controls
		where i is dfPanel
		select i).FirstOrDefault() as dfPanel;
		string name = "Tab Page " + (this.controls.Count + 1).ToString();
		dfPanel dfPanel2 = base.AddControl<dfPanel>();
		dfPanel2.name = name;
		dfPanel2.Atlas = this.Atlas;
		dfPanel2.Anchor = dfAnchorStyle.All;
		dfPanel2.ClipChildren = true;
		if (dfPanel != null)
		{
			dfPanel2.Atlas = dfPanel.Atlas;
			dfPanel2.BackgroundSprite = dfPanel.BackgroundSprite;
		}
		this.arrangeTabPages();
		this.Invalidate();
		return dfPanel2;
	}

	// Token: 0x06000414 RID: 1044 RVA: 0x00014B1D File Offset: 0x00012D1D
	public override void OnEnable()
	{
		base.OnEnable();
		if (this.size.sqrMagnitude < 1E-45f)
		{
			base.Size = new Vector2(256f, 256f);
		}
	}

	// Token: 0x06000415 RID: 1045 RVA: 0x00014B4C File Offset: 0x00012D4C
	protected internal override void OnControlAdded(dfControl child)
	{
		base.OnControlAdded(child);
		this.attachEvents(child);
		this.arrangeTabPages();
	}

	// Token: 0x06000416 RID: 1046 RVA: 0x00014B62 File Offset: 0x00012D62
	protected internal override void OnControlRemoved(dfControl child)
	{
		base.OnControlRemoved(child);
		this.detachEvents(child);
		this.arrangeTabPages();
	}

	// Token: 0x06000417 RID: 1047 RVA: 0x00014B78 File Offset: 0x00012D78
	protected internal virtual void OnSelectedIndexChanged(int Index)
	{
		base.SignalHierarchy("OnSelectedIndexChanged", new object[]
		{
			this,
			Index
		});
		if (this.SelectedIndexChanged != null)
		{
			this.SelectedIndexChanged(this, Index);
		}
	}

	// Token: 0x06000418 RID: 1048 RVA: 0x00014BB0 File Offset: 0x00012DB0
	protected override void OnRebuildRenderData()
	{
		if (this.Atlas == null || string.IsNullOrEmpty(this.backgroundSprite))
		{
			return;
		}
		dfAtlas.ItemInfo itemInfo = this.Atlas[this.backgroundSprite];
		if (itemInfo == null)
		{
			return;
		}
		this.renderData.Material = this.Atlas.Material;
		Color32 color = base.ApplyOpacity(base.IsEnabled ? this.color : this.disabledColor);
		dfSprite.RenderOptions options = new dfSprite.RenderOptions
		{
			atlas = this.atlas,
			color = color,
			fillAmount = 1f,
			flip = dfSpriteFlip.None,
			offset = this.pivot.TransformToUpperLeft(base.Size),
			pixelsToUnits = base.PixelsToUnits(),
			size = base.Size,
			spriteInfo = itemInfo
		};
		if (itemInfo.border.horizontal == 0 && itemInfo.border.vertical == 0)
		{
			dfSprite.renderSprite(this.renderData, options);
			return;
		}
		dfSlicedSprite.renderSprite(this.renderData, options);
	}

	// Token: 0x06000419 RID: 1049 RVA: 0x00014CC8 File Offset: 0x00012EC8
	private void selectPageByIndex(int value)
	{
		value = Mathf.Max(Mathf.Min(value, this.controls.Count - 1), -1);
		if (value == this.selectedIndex)
		{
			return;
		}
		this.selectedIndex = value;
		for (int i = 0; i < this.controls.Count; i++)
		{
			dfControl dfControl = this.controls[i];
			if (!(dfControl == null))
			{
				dfControl.IsVisible = (i == value);
			}
		}
		this.arrangeTabPages();
		this.Invalidate();
		this.OnSelectedIndexChanged(value);
	}

	// Token: 0x0600041A RID: 1050 RVA: 0x00014D4C File Offset: 0x00012F4C
	private void arrangeTabPages()
	{
		if (this.padding == null)
		{
			this.padding = new RectOffset(0, 0, 0, 0);
		}
		Vector3 relativePosition = new Vector3((float)this.padding.left, (float)this.padding.top);
		Vector2 size = new Vector2(this.size.x - (float)this.padding.horizontal, this.size.y - (float)this.padding.vertical);
		for (int i = 0; i < this.controls.Count; i++)
		{
			dfPanel dfPanel = this.controls[i] as dfPanel;
			if (dfPanel != null)
			{
				dfPanel.Size = size;
				dfPanel.RelativePosition = relativePosition;
			}
		}
	}

	// Token: 0x0600041B RID: 1051 RVA: 0x00014E05 File Offset: 0x00013005
	private void attachEvents(dfControl control)
	{
		control.IsVisibleChanged += this.control_IsVisibleChanged;
		control.PositionChanged += this.childControlInvalidated;
		control.SizeChanged += this.childControlInvalidated;
	}

	// Token: 0x0600041C RID: 1052 RVA: 0x00014E3D File Offset: 0x0001303D
	private void detachEvents(dfControl control)
	{
		control.IsVisibleChanged -= this.control_IsVisibleChanged;
		control.PositionChanged -= this.childControlInvalidated;
		control.SizeChanged -= this.childControlInvalidated;
	}

	// Token: 0x0600041D RID: 1053 RVA: 0x00014E75 File Offset: 0x00013075
	private void control_IsVisibleChanged(dfControl control, bool value)
	{
		this.onChildControlInvalidatedLayout();
	}

	// Token: 0x0600041E RID: 1054 RVA: 0x00014E7D File Offset: 0x0001307D
	private void childControlInvalidated(dfControl control, Vector2 value)
	{
		this.onChildControlInvalidatedLayout();
	}

	// Token: 0x0600041F RID: 1055 RVA: 0x00014E85 File Offset: 0x00013085
	private void onChildControlInvalidatedLayout()
	{
		if (base.IsLayoutSuspended)
		{
			return;
		}
		this.arrangeTabPages();
		this.Invalidate();
	}

	// Token: 0x04000163 RID: 355
	[SerializeField]
	protected dfAtlas atlas;

	// Token: 0x04000164 RID: 356
	[SerializeField]
	protected string backgroundSprite;

	// Token: 0x04000165 RID: 357
	[SerializeField]
	protected RectOffset padding = new RectOffset();

	// Token: 0x04000166 RID: 358
	[SerializeField]
	protected int selectedIndex;
}

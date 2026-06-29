using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Token: 0x02000014 RID: 20
[dfCategory("Basic Controls")]
[dfTooltip("Implements a scrollable control container")]
[dfHelp("http://www.daikonforge.com/docs/df-gui/classdf_scroll_panel.html")]
[ExecuteInEditMode]
[AddComponentMenu("Daikon Forge/User Interface/Containers/Scrollable Panel")]
[Serializable]
public class dfScrollPanel : dfControl
{
	// Token: 0x14000031 RID: 49
	// (add) Token: 0x06000320 RID: 800 RVA: 0x0000F2C4 File Offset: 0x0000D4C4
	// (remove) Token: 0x06000321 RID: 801 RVA: 0x0000F2FC File Offset: 0x0000D4FC
	public event PropertyChangedEventHandler<Vector2> ScrollPositionChanged;

	// Token: 0x170000B8 RID: 184
	// (get) Token: 0x06000322 RID: 802 RVA: 0x0000F331 File Offset: 0x0000D531
	// (set) Token: 0x06000323 RID: 803 RVA: 0x0000F339 File Offset: 0x0000D539
	public bool UseScrollMomentum
	{
		get
		{
			return this.useScrollMomentum;
		}
		set
		{
			this.useScrollMomentum = value;
			this.scrollMomentum = Vector2.zero;
		}
	}

	// Token: 0x170000B9 RID: 185
	// (get) Token: 0x06000324 RID: 804 RVA: 0x0000F34D File Offset: 0x0000D54D
	// (set) Token: 0x06000325 RID: 805 RVA: 0x0000F355 File Offset: 0x0000D555
	public bool ScrollWithArrowKeys
	{
		get
		{
			return this.scrollWithArrowKeys;
		}
		set
		{
			this.scrollWithArrowKeys = value;
		}
	}

	// Token: 0x170000BA RID: 186
	// (get) Token: 0x06000326 RID: 806 RVA: 0x0000F360 File Offset: 0x0000D560
	// (set) Token: 0x06000327 RID: 807 RVA: 0x0000F3A1 File Offset: 0x0000D5A1
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

	// Token: 0x170000BB RID: 187
	// (get) Token: 0x06000328 RID: 808 RVA: 0x0000F3BE File Offset: 0x0000D5BE
	// (set) Token: 0x06000329 RID: 809 RVA: 0x0000F3C6 File Offset: 0x0000D5C6
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

	// Token: 0x170000BC RID: 188
	// (get) Token: 0x0600032A RID: 810 RVA: 0x0000F3E3 File Offset: 0x0000D5E3
	// (set) Token: 0x0600032B RID: 811 RVA: 0x0000F3EB File Offset: 0x0000D5EB
	public Color32 BackgroundColor
	{
		get
		{
			return this.backgroundColor;
		}
		set
		{
			if (!object.Equals(value, this.backgroundColor))
			{
				this.backgroundColor = value;
				this.Invalidate();
			}
		}
	}

	// Token: 0x170000BD RID: 189
	// (get) Token: 0x0600032C RID: 812 RVA: 0x0000F412 File Offset: 0x0000D612
	// (set) Token: 0x0600032D RID: 813 RVA: 0x0000F41A File Offset: 0x0000D61A
	public bool AutoReset
	{
		get
		{
			return this.autoReset;
		}
		set
		{
			if (value != this.autoReset)
			{
				this.autoReset = value;
				this.Reset();
			}
		}
	}

	// Token: 0x170000BE RID: 190
	// (get) Token: 0x0600032E RID: 814 RVA: 0x0000F432 File Offset: 0x0000D632
	// (set) Token: 0x0600032F RID: 815 RVA: 0x0000F44D File Offset: 0x0000D64D
	public RectOffset ScrollPadding
	{
		get
		{
			if (this.scrollPadding == null)
			{
				this.scrollPadding = new RectOffset();
			}
			return this.scrollPadding;
		}
		set
		{
			value = value.ConstrainPadding();
			if (!object.Equals(value, this.scrollPadding))
			{
				this.scrollPadding = value;
				if (this.AutoReset || this.AutoLayout)
				{
					this.Reset();
				}
			}
		}
	}

	// Token: 0x170000BF RID: 191
	// (get) Token: 0x06000330 RID: 816 RVA: 0x0000F482 File Offset: 0x0000D682
	// (set) Token: 0x06000331 RID: 817 RVA: 0x0000F48A File Offset: 0x0000D68A
	public bool AutoLayout
	{
		get
		{
			return this.autoLayout;
		}
		set
		{
			if (value != this.autoLayout)
			{
				this.autoLayout = value;
				if (this.AutoReset || this.AutoLayout)
				{
					this.Reset();
				}
			}
		}
	}

	// Token: 0x170000C0 RID: 192
	// (get) Token: 0x06000332 RID: 818 RVA: 0x0000F4B2 File Offset: 0x0000D6B2
	// (set) Token: 0x06000333 RID: 819 RVA: 0x0000F4BA File Offset: 0x0000D6BA
	public bool WrapLayout
	{
		get
		{
			return this.wrapLayout;
		}
		set
		{
			if (value != this.wrapLayout)
			{
				this.wrapLayout = value;
				this.Reset();
			}
		}
	}

	// Token: 0x170000C1 RID: 193
	// (get) Token: 0x06000334 RID: 820 RVA: 0x0000F4D2 File Offset: 0x0000D6D2
	// (set) Token: 0x06000335 RID: 821 RVA: 0x0000F4DA File Offset: 0x0000D6DA
	public dfScrollPanel.LayoutDirection FlowDirection
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
				this.Reset();
			}
		}
	}

	// Token: 0x170000C2 RID: 194
	// (get) Token: 0x06000336 RID: 822 RVA: 0x0000F4F2 File Offset: 0x0000D6F2
	// (set) Token: 0x06000337 RID: 823 RVA: 0x0000F50D File Offset: 0x0000D70D
	public RectOffset FlowPadding
	{
		get
		{
			if (this.flowPadding == null)
			{
				this.flowPadding = new RectOffset();
			}
			return this.flowPadding;
		}
		set
		{
			value = value.ConstrainPadding();
			if (!object.Equals(value, this.flowPadding))
			{
				this.flowPadding = value;
				this.Reset();
			}
		}
	}

	// Token: 0x170000C3 RID: 195
	// (get) Token: 0x06000338 RID: 824 RVA: 0x0000F532 File Offset: 0x0000D732
	// (set) Token: 0x06000339 RID: 825 RVA: 0x0000F53C File Offset: 0x0000D73C
	public Vector2 ScrollPosition
	{
		get
		{
			return this.scrollPosition;
		}
		set
		{
			Vector2 a = this.calculateViewSize();
			Vector2 b = new Vector2(this.size.x - (float)this.scrollPadding.horizontal, this.size.y - (float)this.scrollPadding.vertical);
			value = Vector2.Min(a - b, value);
			value = Vector2.Max(Vector2.zero, value);
			value = value.RoundToInt();
			if ((value - this.scrollPosition).sqrMagnitude > 1E-45f)
			{
				Vector2 v = value - this.scrollPosition;
				this.scrollPosition = value;
				this.scrollChildControls(v);
				this.updateScrollbars();
			}
			this.OnScrollPositionChanged();
		}
	}

	// Token: 0x170000C4 RID: 196
	// (get) Token: 0x0600033A RID: 826 RVA: 0x0000F5F0 File Offset: 0x0000D7F0
	// (set) Token: 0x0600033B RID: 827 RVA: 0x0000F5F8 File Offset: 0x0000D7F8
	public int ScrollWheelAmount
	{
		get
		{
			return this.scrollWheelAmount;
		}
		set
		{
			this.scrollWheelAmount = value;
		}
	}

	// Token: 0x170000C5 RID: 197
	// (get) Token: 0x0600033C RID: 828 RVA: 0x0000F601 File Offset: 0x0000D801
	// (set) Token: 0x0600033D RID: 829 RVA: 0x0000F609 File Offset: 0x0000D809
	public dfScrollbar HorzScrollbar
	{
		get
		{
			return this.horzScroll;
		}
		set
		{
			this.horzScroll = value;
			this.updateScrollbars();
		}
	}

	// Token: 0x170000C6 RID: 198
	// (get) Token: 0x0600033E RID: 830 RVA: 0x0000F618 File Offset: 0x0000D818
	// (set) Token: 0x0600033F RID: 831 RVA: 0x0000F620 File Offset: 0x0000D820
	public dfScrollbar VertScrollbar
	{
		get
		{
			return this.vertScroll;
		}
		set
		{
			this.vertScroll = value;
			this.updateScrollbars();
		}
	}

	// Token: 0x170000C7 RID: 199
	// (get) Token: 0x06000340 RID: 832 RVA: 0x0000F62F File Offset: 0x0000D82F
	// (set) Token: 0x06000341 RID: 833 RVA: 0x0000F637 File Offset: 0x0000D837
	public dfControlOrientation WheelScrollDirection
	{
		get
		{
			return this.wheelDirection;
		}
		set
		{
			this.wheelDirection = value;
		}
	}

	// Token: 0x170000C8 RID: 200
	// (get) Token: 0x06000342 RID: 834 RVA: 0x0000F640 File Offset: 0x0000D840
	// (set) Token: 0x06000343 RID: 835 RVA: 0x0000F648 File Offset: 0x0000D848
	public bool UseVirtualScrolling
	{
		get
		{
			return this.useVirtualScrolling;
		}
		set
		{
			this.useVirtualScrolling = value;
			if (!value)
			{
				this.VirtualScrollingTile = null;
			}
		}
	}

	// Token: 0x170000C9 RID: 201
	// (get) Token: 0x06000344 RID: 836 RVA: 0x0000F65B File Offset: 0x0000D85B
	// (set) Token: 0x06000345 RID: 837 RVA: 0x0000F663 File Offset: 0x0000D863
	public bool AutoFitVirtualTiles
	{
		get
		{
			return this.autoFitVirtualTiles;
		}
		set
		{
			this.autoFitVirtualTiles = value;
		}
	}

	// Token: 0x170000CA RID: 202
	// (get) Token: 0x06000346 RID: 838 RVA: 0x0000F66C File Offset: 0x0000D86C
	// (set) Token: 0x06000347 RID: 839 RVA: 0x0000F67E File Offset: 0x0000D87E
	public dfControl VirtualScrollingTile
	{
		get
		{
			if (!this.useVirtualScrolling)
			{
				return null;
			}
			return this.virtualScrollingTile;
		}
		set
		{
			this.virtualScrollingTile = (this.useVirtualScrolling ? value : null);
		}
	}

	// Token: 0x06000348 RID: 840 RVA: 0x0000F692 File Offset: 0x0000D892
	protected internal override RectOffset GetClipPadding()
	{
		return this.scrollPadding ?? dfRectOffsetExtensions.Empty;
	}

	// Token: 0x06000349 RID: 841 RVA: 0x0000F6A4 File Offset: 0x0000D8A4
	protected internal override Plane[] GetClippingPlanes()
	{
		if (!base.ClipChildren)
		{
			return null;
		}
		Vector3[] corners = base.GetCorners();
		Vector3 vector = base.transform.TransformDirection(Vector3.right);
		Vector3 vector2 = base.transform.TransformDirection(Vector3.left);
		Vector3 vector3 = base.transform.TransformDirection(Vector3.up);
		Vector3 vector4 = base.transform.TransformDirection(Vector3.down);
		float d = base.PixelsToUnits();
		RectOffset rectOffset = this.ScrollPadding;
		corners[0] += vector * (float)rectOffset.left * d + vector4 * (float)rectOffset.top * d;
		corners[1] += vector2 * (float)rectOffset.right * d + vector4 * (float)rectOffset.top * d;
		corners[2] += vector * (float)rectOffset.left * d + vector3 * (float)rectOffset.bottom * d;
		return new Plane[]
		{
			new Plane(vector, corners[0]),
			new Plane(vector2, corners[1]),
			new Plane(vector3, corners[2]),
			new Plane(vector4, corners[0])
		};
	}

	// Token: 0x170000CB RID: 203
	// (get) Token: 0x0600034A RID: 842 RVA: 0x0000F842 File Offset: 0x0000DA42
	public override bool CanFocus
	{
		get
		{
			return (base.IsEnabled && base.IsVisible) || base.CanFocus;
		}
	}

	// Token: 0x0600034B RID: 843 RVA: 0x0000F85C File Offset: 0x0000DA5C
	public override void OnDestroy()
	{
		if (this.horzScroll != null)
		{
			this.horzScroll.ValueChanged -= this.horzScroll_ValueChanged;
		}
		if (this.vertScroll != null)
		{
			this.vertScroll.ValueChanged -= this.vertScroll_ValueChanged;
		}
		this.horzScroll = null;
		this.vertScroll = null;
	}

	// Token: 0x0600034C RID: 844 RVA: 0x0000F8C4 File Offset: 0x0000DAC4
	public override void Update()
	{
		base.Update();
		if (this.useScrollMomentum && !this.isMouseDown && this.scrollMomentum.magnitude > 0.25f)
		{
			this.ScrollPosition += this.scrollMomentum;
			this.scrollMomentum *= 0.95f - Time.deltaTime;
		}
		if (this.isControlInvalidated && this.autoLayout && base.IsVisible)
		{
			this.AutoArrange();
			this.updateScrollbars();
		}
	}

	// Token: 0x0600034D RID: 845 RVA: 0x0000F950 File Offset: 0x0000DB50
	public override void LateUpdate()
	{
		base.LateUpdate();
		this.initialize();
		if (this.resetNeeded && base.IsVisible)
		{
			this.resetNeeded = false;
			if (this.autoReset || this.autoLayout)
			{
				this.Reset();
			}
		}
	}

	// Token: 0x0600034E RID: 846 RVA: 0x0000F98C File Offset: 0x0000DB8C
	public override void OnEnable()
	{
		base.OnEnable();
		if (this.size == Vector2.zero)
		{
			base.SuspendLayout();
			Camera camera = base.GetCamera();
			base.Size = new Vector3((float)(camera.pixelWidth / 2), (float)(camera.pixelHeight / 2));
			base.ResumeLayout();
		}
		if (this.autoLayout)
		{
			this.AutoArrange();
		}
		this.updateScrollbars();
	}

	// Token: 0x0600034F RID: 847 RVA: 0x0000F9FA File Offset: 0x0000DBFA
	protected internal override void OnIsVisibleChanged()
	{
		base.OnIsVisibleChanged();
		if (base.IsVisible && (this.autoReset || this.autoLayout))
		{
			this.Reset();
			this.updateScrollbars();
		}
	}

	// Token: 0x06000350 RID: 848 RVA: 0x0000FA28 File Offset: 0x0000DC28
	protected internal override void OnSizeChanged()
	{
		base.OnSizeChanged();
		if (this.autoReset || this.autoLayout)
		{
			this.Reset();
			return;
		}
		Vector2 vector = this.calculateMinChildPosition();
		if (vector.x > (float)this.scrollPadding.left || vector.y > (float)this.scrollPadding.top)
		{
			vector -= new Vector2((float)this.scrollPadding.left, (float)this.scrollPadding.top);
			vector = Vector2.Max(vector, Vector2.zero);
			this.scrollChildControls(vector);
		}
		this.updateScrollbars();
	}

	// Token: 0x06000351 RID: 849 RVA: 0x0000FAC3 File Offset: 0x0000DCC3
	protected internal override void OnResolutionChanged(Vector2 previousResolution, Vector2 currentResolution)
	{
		base.OnResolutionChanged(previousResolution, currentResolution);
		this.resetNeeded = (this.AutoLayout || this.AutoReset);
	}

	// Token: 0x06000352 RID: 850 RVA: 0x0000FAE4 File Offset: 0x0000DCE4
	protected internal override void OnGotFocus(dfFocusEventArgs args)
	{
		if (args.Source != this)
		{
			this.ScrollIntoView(args.Source);
		}
		base.OnGotFocus(args);
	}

	// Token: 0x06000353 RID: 851 RVA: 0x0000FB08 File Offset: 0x0000DD08
	protected internal override void OnKeyDown(dfKeyEventArgs args)
	{
		if (!this.scrollWithArrowKeys || args.Used)
		{
			base.OnKeyDown(args);
			return;
		}
		float num = (this.horzScroll != null) ? this.horzScroll.IncrementAmount : 1f;
		float num2 = (this.vertScroll != null) ? this.vertScroll.IncrementAmount : 1f;
		if (args.KeyCode == KeyCode.LeftArrow)
		{
			this.ScrollPosition += new Vector2(-num, 0f);
			args.Use();
		}
		else if (args.KeyCode == KeyCode.RightArrow)
		{
			this.ScrollPosition += new Vector2(num, 0f);
			args.Use();
		}
		else if (args.KeyCode == KeyCode.UpArrow)
		{
			this.ScrollPosition += new Vector2(0f, -num2);
			args.Use();
		}
		else if (args.KeyCode == KeyCode.DownArrow)
		{
			this.ScrollPosition += new Vector2(0f, num2);
			args.Use();
		}
		base.OnKeyDown(args);
	}

	// Token: 0x06000354 RID: 852 RVA: 0x0000FC3D File Offset: 0x0000DE3D
	protected internal override void OnMouseEnter(dfMouseEventArgs args)
	{
		base.OnMouseEnter(args);
		this.touchStartPosition = args.Position;
	}

	// Token: 0x06000355 RID: 853 RVA: 0x0000FC52 File Offset: 0x0000DE52
	protected internal override void OnMouseDown(dfMouseEventArgs args)
	{
		base.OnMouseDown(args);
		this.scrollMomentum = Vector2.zero;
		this.touchStartPosition = args.Position;
		this.isMouseDown = this.IsInteractive;
	}

	// Token: 0x06000356 RID: 854 RVA: 0x0000FC7E File Offset: 0x0000DE7E
	internal override void OnDragStart(dfDragEventArgs args)
	{
		base.OnDragStart(args);
		this.scrollMomentum = Vector2.zero;
		if (args.Used)
		{
			this.isMouseDown = false;
		}
	}

	// Token: 0x06000357 RID: 855 RVA: 0x0000FCA1 File Offset: 0x0000DEA1
	protected internal override void OnMouseUp(dfMouseEventArgs args)
	{
		base.OnMouseUp(args);
		this.isMouseDown = false;
	}

	// Token: 0x06000358 RID: 856 RVA: 0x0000FCB4 File Offset: 0x0000DEB4
	protected internal override void OnMouseMove(dfMouseEventArgs args)
	{
		if ((args is dfTouchEventArgs || this.isMouseDown) && !args.Used && (args.Position - this.touchStartPosition).magnitude > 5f)
		{
			Vector2 vector = args.MoveDelta.Scale(-1f, 1f);
			dfGUIManager manager = base.GetManager();
			Vector2 screenSize = manager.GetScreenSize();
			Camera renderCamera = manager.RenderCamera;
			vector.x = screenSize.x * (vector.x / (float)renderCamera.pixelWidth);
			vector.y = screenSize.y * (vector.y / (float)renderCamera.pixelHeight);
			this.ScrollPosition += vector;
			this.scrollMomentum = (this.scrollMomentum + vector) * 0.5f;
			args.Use();
		}
		base.OnMouseMove(args);
	}

	// Token: 0x06000359 RID: 857 RVA: 0x0000FDA0 File Offset: 0x0000DFA0
	protected internal override void OnMouseWheel(dfMouseEventArgs args)
	{
		try
		{
			if (!args.Used)
			{
				float num = (this.wheelDirection == dfControlOrientation.Horizontal) ? ((this.horzScroll != null) ? this.horzScroll.IncrementAmount : ((float)this.scrollWheelAmount)) : ((this.vertScroll != null) ? this.vertScroll.IncrementAmount : ((float)this.scrollWheelAmount));
				if (this.wheelDirection == dfControlOrientation.Horizontal)
				{
					this.ScrollPosition = new Vector2(this.scrollPosition.x - num * args.WheelDelta, this.scrollPosition.y);
					this.scrollMomentum = new Vector2(-num * args.WheelDelta, 0f);
				}
				else
				{
					this.ScrollPosition = new Vector2(this.scrollPosition.x, this.scrollPosition.y - num * args.WheelDelta);
					this.scrollMomentum = new Vector2(0f, -num * args.WheelDelta);
				}
				args.Use();
				base.Signal("OnMouseWheel", this, args);
			}
		}
		finally
		{
			base.OnMouseWheel(args);
		}
	}

	// Token: 0x0600035A RID: 858 RVA: 0x0000FED4 File Offset: 0x0000E0D4
	protected internal override void OnControlAdded(dfControl child)
	{
		base.OnControlAdded(child);
		this.attachEvents(child);
		if (this.autoLayout)
		{
			this.AutoArrange();
		}
	}

	// Token: 0x0600035B RID: 859 RVA: 0x0000FEF2 File Offset: 0x0000E0F2
	protected internal override void OnControlRemoved(dfControl child)
	{
		base.OnControlRemoved(child);
		if (child != null)
		{
			this.detachEvents(child);
		}
		if (this.autoLayout)
		{
			this.AutoArrange();
			return;
		}
		this.updateScrollbars();
	}

	// Token: 0x0600035C RID: 860 RVA: 0x0000FF20 File Offset: 0x0000E120
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
		Color32 color = base.ApplyOpacity(this.BackgroundColor);
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

	// Token: 0x0600035D RID: 861 RVA: 0x00010028 File Offset: 0x0000E228
	protected internal void OnScrollPositionChanged()
	{
		this.Invalidate();
		base.SignalHierarchy("OnScrollPositionChanged", new object[]
		{
			this,
			this.ScrollPosition
		});
		if (this.ScrollPositionChanged != null)
		{
			this.ScrollPositionChanged(this, this.ScrollPosition);
		}
	}

	// Token: 0x0600035E RID: 862 RVA: 0x0001007C File Offset: 0x0000E27C
	public void FitToContents()
	{
		if (this.controls.Count == 0)
		{
			return;
		}
		Vector2 vector = Vector2.zero;
		for (int i = 0; i < this.controls.Count; i++)
		{
			dfControl dfControl = this.controls[i];
			Vector2 rhs = dfControl.RelativePosition + dfControl.Size;
			vector = Vector2.Max(vector, rhs);
		}
		base.Size = vector + new Vector2((float)this.scrollPadding.right, (float)this.scrollPadding.bottom);
	}

	// Token: 0x0600035F RID: 863 RVA: 0x00010108 File Offset: 0x0000E308
	public void CenterChildControls()
	{
		if (this.controls.Count == 0)
		{
			return;
		}
		Vector2 vector = Vector2.one * float.MaxValue;
		Vector2 vector2 = Vector2.one * float.MinValue;
		for (int i = 0; i < this.controls.Count; i++)
		{
			dfControl dfControl = this.controls[i];
			Vector2 vector3 = dfControl.RelativePosition;
			Vector2 rhs = vector3 + dfControl.Size;
			vector = Vector2.Min(vector, vector3);
			vector2 = Vector2.Max(vector2, rhs);
		}
		Vector2 b = vector2 - vector;
		Vector2 b2 = (base.Size - b) * 0.5f;
		for (int j = 0; j < this.controls.Count; j++)
		{
			dfControl dfControl2 = this.controls[j];
			dfControl2.RelativePosition = dfControl2.RelativePosition - vector + b2;
		}
	}

	// Token: 0x06000360 RID: 864 RVA: 0x00010203 File Offset: 0x0000E403
	public void ScrollToTop()
	{
		this.scrollMomentum = Vector2.zero;
		this.ScrollPosition = new Vector2(this.scrollPosition.x, 0f);
	}

	// Token: 0x06000361 RID: 865 RVA: 0x0001022B File Offset: 0x0000E42B
	public void ScrollToBottom()
	{
		this.scrollMomentum = Vector2.zero;
		this.ScrollPosition = new Vector2(this.scrollPosition.x, 2.1474836E+09f);
	}

	// Token: 0x06000362 RID: 866 RVA: 0x00010253 File Offset: 0x0000E453
	public void ScrollToLeft()
	{
		this.scrollMomentum = Vector2.zero;
		this.ScrollPosition = new Vector2(0f, this.scrollPosition.y);
	}

	// Token: 0x06000363 RID: 867 RVA: 0x0001027B File Offset: 0x0000E47B
	public void ScrollToRight()
	{
		this.scrollMomentum = Vector2.zero;
		this.ScrollPosition = new Vector2(2.1474836E+09f, this.scrollPosition.y);
	}

	// Token: 0x06000364 RID: 868 RVA: 0x000102A4 File Offset: 0x0000E4A4
	public void ScrollIntoView(dfControl control)
	{
		this.scrollMomentum = Vector2.zero;
		Rect rect = new Rect(this.scrollPosition.x + (float)this.scrollPadding.left, this.scrollPosition.y + (float)this.scrollPadding.top, this.size.x - (float)this.scrollPadding.horizontal, this.size.y - (float)this.scrollPadding.vertical).RoundToInt();
		Vector3 vector = control.RelativePosition;
		Vector2 size = control.Size;
		while (!this.controls.Contains(control))
		{
			control = control.Parent;
			vector += control.RelativePosition;
		}
		Rect other = new Rect(this.scrollPosition.x + vector.x, this.scrollPosition.y + vector.y, size.x, size.y).RoundToInt();
		if (rect.Contains(other))
		{
			return;
		}
		Vector2 vector2 = this.scrollPosition;
		if (other.xMin < rect.xMin)
		{
			vector2.x = other.xMin - (float)this.scrollPadding.left;
		}
		else if (other.xMax > rect.xMax)
		{
			vector2.x = other.xMax - Mathf.Max(this.size.x, size.x) + (float)this.scrollPadding.horizontal;
		}
		if (other.y < rect.y)
		{
			vector2.y = other.yMin - (float)this.scrollPadding.top;
		}
		else if (other.yMax > rect.yMax)
		{
			vector2.y = other.yMax - Mathf.Max(this.size.y, size.y) + (float)this.scrollPadding.vertical;
		}
		this.ScrollPosition = vector2;
		this.scrollMomentum = Vector2.zero;
	}

	// Token: 0x06000365 RID: 869 RVA: 0x00010498 File Offset: 0x0000E698
	public void Reset()
	{
		try
		{
			base.SuspendLayout();
			if (this.autoLayout)
			{
				Vector2 vector = this.ScrollPosition;
				this.ScrollPosition = Vector2.zero;
				this.AutoArrange();
				this.ScrollPosition = vector;
			}
			else
			{
				this.scrollPadding = this.ScrollPadding.ConstrainPadding();
				Vector3 vector2 = this.calculateMinChildPosition();
				vector2 -= new Vector3((float)this.scrollPadding.left, (float)this.scrollPadding.top);
				for (int i = 0; i < this.controls.Count; i++)
				{
					this.controls[i].RelativePosition -= vector2;
				}
				this.scrollPosition = Vector2.zero;
			}
			this.Invalidate();
			this.updateScrollbars();
		}
		finally
		{
			base.ResumeLayout();
		}
	}

	// Token: 0x06000366 RID: 870 RVA: 0x00010578 File Offset: 0x0000E778
	private void Virtualize<T>(IList<T> backingList, int startIndex)
	{
		if (!this.useVirtualScrolling)
		{
			Debug.LogError("Virtual scrolling not enabled for this dfScrollPanel: " + base.name);
			return;
		}
		if (this.virtualScrollingTile == null)
		{
			Debug.LogError("Virtual scrolling cannot be started without assigning VirtualScrollingTile: " + base.name);
			return;
		}
		int count = backingList.Count;
		dfVirtualScrollData<T> dfVirtualScrollData = this.GetVirtualScrollData<T>() ?? this.initVirtualScrollData<T>(backingList);
		bool flag = this.isVerticalFlow();
		RectOffset rectOffset = dfVirtualScrollData.ItemPadding = new RectOffset(this.FlowPadding.left, this.FlowPadding.right, this.FlowPadding.top, this.FlowPadding.bottom);
		int num = flag ? rectOffset.vertical : rectOffset.horizontal;
		int num2 = flag ? rectOffset.top : rectOffset.left;
		float num3 = flag ? base.Height : base.Width;
		this.AutoLayout = false;
		this.AutoReset = false;
		IDFVirtualScrollingTile idfvirtualScrollingTile;
		if ((idfvirtualScrollingTile = dfVirtualScrollData.DummyTop) == null)
		{
			idfvirtualScrollingTile = (dfVirtualScrollData.DummyTop = this.initTile(rectOffset));
		}
		IDFVirtualScrollingTile idfvirtualScrollingTile2 = idfvirtualScrollingTile;
		dfPanel dfPanel = idfvirtualScrollingTile2.GetDfPanel();
		float num4 = flag ? idfvirtualScrollingTile2.GetDfPanel().Height : idfvirtualScrollingTile2.GetDfPanel().Width;
		dfPanel.IsEnabled = false;
		dfPanel.Opacity = 0f;
		dfPanel.gameObject.hideFlags = HideFlags.HideInHierarchy;
		dfScrollbar dfScrollbar;
		if ((dfScrollbar = this.VertScrollbar) || (dfScrollbar = this.HorzScrollbar))
		{
			IDFVirtualScrollingTile idfvirtualScrollingTile3;
			if ((idfvirtualScrollingTile3 = dfVirtualScrollData.DummyBottom) == null)
			{
				idfvirtualScrollingTile3 = (dfVirtualScrollData.DummyBottom = this.initTile(rectOffset));
			}
			dfPanel dfPanel2 = idfvirtualScrollingTile3.GetDfPanel();
			float num5 = (flag ? dfPanel.RelativePosition.y : dfPanel.RelativePosition.x) + ((float)(backingList.Count - 1) * (num4 + (float)num) + (float)num2);
			dfPanel2.RelativePosition = (flag ? new Vector3(dfPanel.RelativePosition.x, num5) : new Vector3(num5, dfPanel.RelativePosition.y));
			dfPanel2.IsEnabled = dfPanel.IsEnabled;
			dfPanel2.gameObject.hideFlags = dfPanel.hideFlags;
			dfPanel2.Opacity = dfPanel.Opacity;
			if (startIndex == 0 && dfScrollbar.MaxValue != 0f)
			{
				startIndex = Mathf.RoundToInt(dfScrollbar.Value / dfScrollbar.MaxValue * (float)(backingList.Count - 1));
			}
			dfScrollbar.Value = (float)startIndex * (num4 + (float)num);
		}
		float num6 = num3 / (num4 + (float)num);
		int num7 = Mathf.RoundToInt(num6);
		int num8 = ((float)num7 > num6) ? (num7 + 1) : (num7 + 2);
		float num9 = (float)num2;
		float num10 = (float)startIndex;
		int num11 = 0;
		while (num11 < num8 && num11 < backingList.Count && startIndex <= backingList.Count)
		{
			try
			{
				IDFVirtualScrollingTile idfvirtualScrollingTile4 = (dfVirtualScrollData.IsInitialized && dfVirtualScrollData.Tiles.Count >= num11 + 1) ? dfVirtualScrollData.Tiles[num11] : this.initTile(rectOffset);
				dfControl dfPanel3 = idfvirtualScrollingTile4.GetDfPanel();
				float num12 = num9;
				dfPanel3.RelativePosition = (flag ? new Vector2((float)rectOffset.left, num12) : new Vector2(num12, (float)rectOffset.top));
				num9 = num12 + num4 + (float)num;
				if (!dfVirtualScrollData.Tiles.Contains(idfvirtualScrollingTile4))
				{
					dfVirtualScrollData.Tiles.Add(idfvirtualScrollingTile4);
				}
				idfvirtualScrollingTile4.VirtualScrollItemIndex = startIndex;
				idfvirtualScrollingTile4.OnScrollPanelItemVirtualize(backingList[startIndex]);
				startIndex++;
			}
			catch
			{
				foreach (IDFVirtualScrollingTile idfvirtualScrollingTile5 in dfVirtualScrollData.Tiles)
				{
					int num13 = idfvirtualScrollingTile5.VirtualScrollItemIndex - 1;
					idfvirtualScrollingTile5.VirtualScrollItemIndex = num13;
					idfvirtualScrollingTile5.OnScrollPanelItemVirtualize(backingList[num13]);
				}
			}
			num11++;
		}
		if (num10 != 0f && this.ScrollPositionChanged != null)
		{
			this.ScrollPositionChanged -= this.virtualScrollPositionChanged<T>;
		}
		dfVirtualScrollData.IsInitialized = true;
		this.ScrollPositionChanged += this.virtualScrollPositionChanged<T>;
	}

	// Token: 0x06000367 RID: 871 RVA: 0x00010998 File Offset: 0x0000EB98
	public void Virtualize<T>(IList<T> backingList, dfPanel tile)
	{
		if (!tile.GetComponents<MonoBehaviour>().FirstOrDefault((MonoBehaviour t) => t is IDFVirtualScrollingTile))
		{
			Debug.LogError("The tile you've chosen does not implement IDFVirtualScrollingTile!");
			return;
		}
		this.UseVirtualScrolling = true;
		this.VirtualScrollingTile = tile;
		this.Virtualize<T>(backingList, 0);
	}

	// Token: 0x06000368 RID: 872 RVA: 0x000109F7 File Offset: 0x0000EBF7
	public void Virtualize<T>(IList<T> backingList)
	{
		this.Virtualize<T>(backingList, 0);
	}

	// Token: 0x06000369 RID: 873 RVA: 0x00010A04 File Offset: 0x0000EC04
	public void ResetVirtualScrollingData()
	{
		this.virtualScrollData = null;
		foreach (dfControl dfControl in this.controls.ToArray())
		{
			base.RemoveControl(dfControl);
			Object.Destroy(dfControl.gameObject);
		}
		this.ScrollPosition = Vector2.zero;
	}

	// Token: 0x0600036A RID: 874 RVA: 0x00010A53 File Offset: 0x0000EC53
	public dfVirtualScrollData<T> GetVirtualScrollData<T>()
	{
		return (dfVirtualScrollData<T>)this.virtualScrollData;
	}

	// Token: 0x0600036B RID: 875 RVA: 0x00010A60 File Offset: 0x0000EC60
	[HideInInspector]
	private void AutoArrange()
	{
		base.SuspendLayout();
		try
		{
			this.scrollPadding = this.ScrollPadding.ConstrainPadding();
			this.flowPadding = this.FlowPadding.ConstrainPadding();
			float num = (float)this.scrollPadding.left + (float)this.flowPadding.left - this.scrollPosition.x;
			float num2 = (float)this.scrollPadding.top + (float)this.flowPadding.top - this.scrollPosition.y;
			float num3 = 0f;
			float num4 = 0f;
			for (int i = 0; i < this.controls.Count; i++)
			{
				dfControl dfControl = this.controls[i];
				if (dfControl.GetIsVisibleRaw() && dfControl.enabled && dfControl.gameObject.activeSelf && !(dfControl == this.horzScroll) && !(dfControl == this.vertScroll))
				{
					if (this.wrapLayout)
					{
						if (this.flowDirection == dfScrollPanel.LayoutDirection.Horizontal)
						{
							if (num + dfControl.Width >= this.size.x - (float)this.scrollPadding.right)
							{
								num = (float)this.scrollPadding.left + (float)this.flowPadding.left;
								num2 += num4;
								num4 = 0f;
							}
						}
						else if (num2 + dfControl.Height + (float)this.flowPadding.vertical >= this.size.y - (float)this.scrollPadding.bottom)
						{
							num2 = (float)this.scrollPadding.top + (float)this.flowPadding.top;
							num += num3;
							num3 = 0f;
						}
					}
					Vector2 v = new Vector2(num, num2);
					dfControl.RelativePosition = v;
					float num5 = dfControl.Width + (float)this.flowPadding.horizontal;
					float num6 = dfControl.Height + (float)this.flowPadding.vertical;
					num3 = Mathf.Max(num5, num3);
					num4 = Mathf.Max(num6, num4);
					if (this.flowDirection == dfScrollPanel.LayoutDirection.Horizontal)
					{
						num += num5;
					}
					else
					{
						num2 += num6;
					}
				}
			}
			this.updateScrollbars();
		}
		finally
		{
			base.ResumeLayout();
		}
	}

	// Token: 0x0600036C RID: 876 RVA: 0x00010CB0 File Offset: 0x0000EEB0
	[HideInInspector]
	private void initialize()
	{
		if (this.initialized)
		{
			return;
		}
		this.initialized = true;
		if (Application.isPlaying)
		{
			if (this.horzScroll != null)
			{
				this.horzScroll.ValueChanged += this.horzScroll_ValueChanged;
			}
			if (this.vertScroll != null)
			{
				this.vertScroll.ValueChanged += this.vertScroll_ValueChanged;
			}
		}
		if (this.resetNeeded || this.autoLayout || this.autoReset)
		{
			this.Reset();
		}
		this.Invalidate();
		this.ScrollPosition = Vector2.zero;
		this.updateScrollbars();
	}

	// Token: 0x0600036D RID: 877 RVA: 0x00010D53 File Offset: 0x0000EF53
	private void vertScroll_ValueChanged(dfControl control, float value)
	{
		this.ScrollPosition = new Vector2(this.scrollPosition.x, value);
	}

	// Token: 0x0600036E RID: 878 RVA: 0x00010D6C File Offset: 0x0000EF6C
	private void horzScroll_ValueChanged(dfControl control, float value)
	{
		this.ScrollPosition = new Vector2(value, this.ScrollPosition.y);
	}

	// Token: 0x0600036F RID: 879 RVA: 0x00010D88 File Offset: 0x0000EF88
	private void scrollChildControls(Vector3 delta)
	{
		try
		{
			this.scrolling = true;
			delta = delta.Scale(1f, -1f, 1f);
			for (int i = 0; i < this.controls.Count; i++)
			{
				dfControl dfControl = this.controls[i];
				dfControl.Position = (dfControl.Position - delta).RoundToInt();
			}
		}
		finally
		{
			this.scrolling = false;
		}
	}

	// Token: 0x06000370 RID: 880 RVA: 0x00010E08 File Offset: 0x0000F008
	private Vector2 calculateMinChildPosition()
	{
		float num = float.MaxValue;
		float num2 = float.MaxValue;
		for (int i = 0; i < this.controls.Count; i++)
		{
			dfControl dfControl = this.controls[i];
			if (dfControl.enabled && dfControl.gameObject.activeSelf)
			{
				Vector3 vector = dfControl.RelativePosition.FloorToInt();
				num = Mathf.Min(num, vector.x);
				num2 = Mathf.Min(num2, vector.y);
			}
		}
		return new Vector2(num, num2);
	}

	// Token: 0x06000371 RID: 881 RVA: 0x00010E8C File Offset: 0x0000F08C
	private Vector2 calculateViewSize()
	{
		Vector2 b = new Vector2((float)this.scrollPadding.horizontal, (float)this.scrollPadding.vertical).RoundToInt();
		Vector2 vector = base.Size.RoundToInt() - b;
		if (!base.IsVisible || this.controls.Count == 0)
		{
			return vector;
		}
		Vector2 vector2 = Vector2.one * float.MaxValue;
		Vector2 vector3 = Vector2.one * float.MinValue;
		for (int i = 0; i < this.controls.Count; i++)
		{
			dfControl dfControl = this.controls[i];
			if (!Application.isPlaying || dfControl.IsVisible)
			{
				Vector2 vector4 = dfControl.RelativePosition.CeilToInt();
				Vector2 lhs = vector4 + dfControl.Size.CeilToInt();
				vector2 = Vector2.Min(vector4, vector2);
				vector3 = Vector2.Max(lhs, vector3);
			}
		}
		Vector2 b2 = Vector2.Max(Vector2.zero, vector2 - new Vector2((float)this.scrollPadding.left, (float)this.scrollPadding.top));
		vector3 = Vector2.Max(vector3 + b2, vector);
		return vector3 - vector2 + b2;
	}

	// Token: 0x06000372 RID: 882 RVA: 0x00010FC0 File Offset: 0x0000F1C0
	[HideInInspector]
	private void updateScrollbars()
	{
		Vector2 vector = this.calculateViewSize();
		Vector2 vector2 = base.Size - new Vector2((float)this.scrollPadding.horizontal, (float)this.scrollPadding.vertical);
		if (this.horzScroll != null)
		{
			this.horzScroll.MinValue = 0f;
			this.horzScroll.MaxValue = vector.x;
			this.horzScroll.ScrollSize = vector2.x;
			this.horzScroll.Value = Mathf.Max(0f, this.scrollPosition.x);
		}
		if (this.vertScroll != null)
		{
			this.vertScroll.MinValue = 0f;
			this.vertScroll.MaxValue = vector.y;
			this.vertScroll.ScrollSize = vector2.y;
			this.vertScroll.Value = Mathf.Max(0f, this.scrollPosition.y);
		}
	}

	// Token: 0x06000373 RID: 883 RVA: 0x000110C0 File Offset: 0x0000F2C0
	private void virtualScrollPositionChanged<T>(dfControl control, Vector2 value)
	{
		dfVirtualScrollData<T> dfVirtualScrollData = this.GetVirtualScrollData<T>();
		if (dfVirtualScrollData == null)
		{
			return;
		}
		IList<T> backingList = dfVirtualScrollData.BackingList;
		RectOffset itemPadding = dfVirtualScrollData.ItemPadding;
		List<IDFVirtualScrollingTile> tiles = dfVirtualScrollData.Tiles;
		bool flag = this.isVerticalFlow();
		float num = flag ? (value.y - dfVirtualScrollData.LastScrollPosition.y) : (value.x - dfVirtualScrollData.LastScrollPosition.x);
		dfVirtualScrollData.LastScrollPosition = value;
		if (Mathf.Abs(num) > base.Height && (this.VertScrollbar || this.HorzScrollbar))
		{
			int startIndex = Mathf.RoundToInt((flag ? (value.y / this.VertScrollbar.MaxValue) : (value.x / this.HorzScrollbar.MaxValue)) * (float)(backingList.Count - 1));
			this.Virtualize<T>(backingList, startIndex);
			return;
		}
		foreach (IDFVirtualScrollingTile idfvirtualScrollingTile in tiles)
		{
			int num2 = 0;
			float num3 = 0f;
			bool flag2 = false;
			dfPanel dfPanel = idfvirtualScrollingTile.GetDfPanel();
			float num4 = flag ? dfPanel.RelativePosition.y : dfPanel.RelativePosition.x;
			float num5 = flag ? dfPanel.Height : dfPanel.Width;
			float num6 = flag ? base.Height : base.Width;
			if (num > 0f)
			{
				if (num4 + num5 > 0f)
				{
					continue;
				}
				dfVirtualScrollData.GetNewLimits(flag, true, out num2, out num3);
				if (num2 >= backingList.Count)
				{
					continue;
				}
				flag2 = true;
				dfPanel.RelativePosition = (flag ? new Vector3(dfPanel.RelativePosition.x, num3 + num5 + (float)itemPadding.vertical) : new Vector3(num3 + num5 + (float)itemPadding.horizontal, dfPanel.RelativePosition.y));
			}
			else if (num < 0f)
			{
				if (num4 < num6)
				{
					continue;
				}
				dfVirtualScrollData.GetNewLimits(flag, false, out num2, out num3);
				if (num2 < 0)
				{
					continue;
				}
				flag2 = true;
				dfPanel.RelativePosition = (flag ? new Vector3(dfPanel.RelativePosition.x, num3 - (num5 + (float)itemPadding.vertical)) : new Vector3(num3 - (num5 + (float)itemPadding.horizontal), dfPanel.RelativePosition.y));
			}
			if (flag2)
			{
				idfvirtualScrollingTile.VirtualScrollItemIndex = num2;
				idfvirtualScrollingTile.OnScrollPanelItemVirtualize(backingList[num2]);
			}
		}
	}

	// Token: 0x06000374 RID: 884 RVA: 0x00011354 File Offset: 0x0000F554
	private dfVirtualScrollData<T> initVirtualScrollData<T>(IList<T> backingList)
	{
		dfVirtualScrollData<T> result = new dfVirtualScrollData<T>
		{
			BackingList = backingList
		};
		this.virtualScrollData = result;
		return result;
	}

	// Token: 0x06000375 RID: 885 RVA: 0x00011378 File Offset: 0x0000F578
	private IDFVirtualScrollingTile initTile(RectOffset padding)
	{
		IDFVirtualScrollingTile idfvirtualScrollingTile = (IDFVirtualScrollingTile)Object.Instantiate<MonoBehaviour>(this.virtualScrollingTile.GetComponents<MonoBehaviour>().FirstOrDefault((MonoBehaviour p) => p is IDFVirtualScrollingTile));
		dfPanel dfPanel = idfvirtualScrollingTile.GetDfPanel();
		bool flag = this.isVerticalFlow();
		base.AddControl(dfPanel);
		if (this.AutoFitVirtualTiles)
		{
			if (flag)
			{
				dfPanel.Width = base.Width - (float)padding.horizontal;
			}
			else
			{
				dfPanel.Height = base.Height - (float)padding.vertical;
			}
		}
		dfPanel.RelativePosition = new Vector3((float)padding.left, (float)padding.top);
		return idfvirtualScrollingTile;
	}

	// Token: 0x06000376 RID: 886 RVA: 0x00011421 File Offset: 0x0000F621
	private bool isVerticalFlow()
	{
		return this.FlowDirection == dfScrollPanel.LayoutDirection.Vertical;
	}

	// Token: 0x06000377 RID: 887 RVA: 0x0001142C File Offset: 0x0000F62C
	private void attachEvents(dfControl control)
	{
		control.IsVisibleChanged += this.childIsVisibleChanged;
		control.PositionChanged += this.childControlInvalidated;
		control.SizeChanged += this.childControlInvalidated;
		control.ZOrderChanged += this.childOrderChanged;
	}

	// Token: 0x06000378 RID: 888 RVA: 0x00011484 File Offset: 0x0000F684
	private void detachEvents(dfControl control)
	{
		control.IsVisibleChanged -= this.childIsVisibleChanged;
		control.PositionChanged -= this.childControlInvalidated;
		control.SizeChanged -= this.childControlInvalidated;
		control.ZOrderChanged -= this.childOrderChanged;
	}

	// Token: 0x06000379 RID: 889 RVA: 0x000114D9 File Offset: 0x0000F6D9
	private void childOrderChanged(dfControl control, int value)
	{
		this.onChildControlInvalidatedLayout();
	}

	// Token: 0x0600037A RID: 890 RVA: 0x000114E1 File Offset: 0x0000F6E1
	private void childIsVisibleChanged(dfControl control, bool value)
	{
		this.onChildControlInvalidatedLayout();
	}

	// Token: 0x0600037B RID: 891 RVA: 0x000114E9 File Offset: 0x0000F6E9
	private void childControlInvalidated(dfControl control, Vector2 value)
	{
		this.onChildControlInvalidatedLayout();
	}

	// Token: 0x0600037C RID: 892 RVA: 0x000114F1 File Offset: 0x0000F6F1
	[HideInInspector]
	private void onChildControlInvalidatedLayout()
	{
		if (this.scrolling || base.IsLayoutSuspended)
		{
			return;
		}
		if (this.autoLayout)
		{
			this.AutoArrange();
		}
		this.updateScrollbars();
		this.Invalidate();
	}

	// Token: 0x0400011B RID: 283
	[SerializeField]
	protected dfAtlas atlas;

	// Token: 0x0400011C RID: 284
	[SerializeField]
	protected string backgroundSprite;

	// Token: 0x0400011D RID: 285
	[SerializeField]
	protected Color32 backgroundColor = UnityEngine.Color.white;

	// Token: 0x0400011E RID: 286
	[SerializeField]
	protected bool autoReset = true;

	// Token: 0x0400011F RID: 287
	[SerializeField]
	protected bool autoLayout;

	// Token: 0x04000120 RID: 288
	[SerializeField]
	protected RectOffset scrollPadding = new RectOffset();

	// Token: 0x04000121 RID: 289
	[SerializeField]
	protected RectOffset flowPadding = new RectOffset();

	// Token: 0x04000122 RID: 290
	[SerializeField]
	protected dfScrollPanel.LayoutDirection flowDirection;

	// Token: 0x04000123 RID: 291
	[SerializeField]
	protected bool wrapLayout;

	// Token: 0x04000124 RID: 292
	[SerializeField]
	protected Vector2 scrollPosition = Vector2.zero;

	// Token: 0x04000125 RID: 293
	[SerializeField]
	protected int scrollWheelAmount = 10;

	// Token: 0x04000126 RID: 294
	[SerializeField]
	protected dfScrollbar horzScroll;

	// Token: 0x04000127 RID: 295
	[SerializeField]
	protected dfScrollbar vertScroll;

	// Token: 0x04000128 RID: 296
	[SerializeField]
	protected dfControlOrientation wheelDirection;

	// Token: 0x04000129 RID: 297
	[SerializeField]
	protected bool scrollWithArrowKeys;

	// Token: 0x0400012A RID: 298
	[SerializeField]
	protected bool useScrollMomentum;

	// Token: 0x0400012B RID: 299
	[SerializeField]
	protected bool useVirtualScrolling;

	// Token: 0x0400012C RID: 300
	[SerializeField]
	protected bool autoFitVirtualTiles = true;

	// Token: 0x0400012D RID: 301
	[SerializeField]
	protected dfControl virtualScrollingTile;

	// Token: 0x0400012E RID: 302
	private bool initialized;

	// Token: 0x0400012F RID: 303
	private bool resetNeeded;

	// Token: 0x04000130 RID: 304
	private bool scrolling;

	// Token: 0x04000131 RID: 305
	private bool isMouseDown;

	// Token: 0x04000132 RID: 306
	private Vector2 touchStartPosition = Vector2.zero;

	// Token: 0x04000133 RID: 307
	private Vector2 scrollMomentum = Vector2.zero;

	// Token: 0x04000134 RID: 308
	private object virtualScrollData;

	// Token: 0x0200035A RID: 858
	public enum LayoutDirection
	{
		// Token: 0x040015F3 RID: 5619
		Horizontal,
		// Token: 0x040015F4 RID: 5620
		Vertical
	}
}

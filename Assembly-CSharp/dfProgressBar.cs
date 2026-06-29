using System;
using UnityEngine;

// Token: 0x02000011 RID: 17
[dfCategory("Basic Controls")]
[dfTooltip("Implements a progress bar that can be used to display the progress from a start value toward an end value, such as the amount of work completed or a player's progress toward some goal, etc.")]
[dfHelp("http://www.daikonforge.com/docs/df-gui/classdf_progress_bar.html")]
[ExecuteInEditMode]
[AddComponentMenu("Daikon Forge/User Interface/Progress Bar")]
[Serializable]
public class dfProgressBar : dfControl
{
	// Token: 0x14000030 RID: 48
	// (add) Token: 0x060002E6 RID: 742 RVA: 0x0000DC98 File Offset: 0x0000BE98
	// (remove) Token: 0x060002E7 RID: 743 RVA: 0x0000DCD0 File Offset: 0x0000BED0
	public event PropertyChangedEventHandler<float> ValueChanged;

	// Token: 0x170000AA RID: 170
	// (get) Token: 0x060002E8 RID: 744 RVA: 0x0000DD08 File Offset: 0x0000BF08
	// (set) Token: 0x060002E9 RID: 745 RVA: 0x0000DD49 File Offset: 0x0000BF49
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

	// Token: 0x170000AB RID: 171
	// (get) Token: 0x060002EA RID: 746 RVA: 0x0000DD66 File Offset: 0x0000BF66
	// (set) Token: 0x060002EB RID: 747 RVA: 0x0000DD6E File Offset: 0x0000BF6E
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
				this.setDefaultSize(value);
				this.Invalidate();
			}
		}
	}

	// Token: 0x170000AC RID: 172
	// (get) Token: 0x060002EC RID: 748 RVA: 0x0000DD92 File Offset: 0x0000BF92
	// (set) Token: 0x060002ED RID: 749 RVA: 0x0000DD9A File Offset: 0x0000BF9A
	public string ProgressSprite
	{
		get
		{
			return this.progressSprite;
		}
		set
		{
			if (value != this.progressSprite)
			{
				this.progressSprite = value;
				this.Invalidate();
			}
		}
	}

	// Token: 0x170000AD RID: 173
	// (get) Token: 0x060002EE RID: 750 RVA: 0x0000DDB7 File Offset: 0x0000BFB7
	// (set) Token: 0x060002EF RID: 751 RVA: 0x0000DDBF File Offset: 0x0000BFBF
	public Color32 ProgressColor
	{
		get
		{
			return this.progressColor;
		}
		set
		{
			if (!object.Equals(value, this.progressColor))
			{
				this.progressColor = value;
				this.Invalidate();
			}
		}
	}

	// Token: 0x170000AE RID: 174
	// (get) Token: 0x060002F0 RID: 752 RVA: 0x0000DDE6 File Offset: 0x0000BFE6
	// (set) Token: 0x060002F1 RID: 753 RVA: 0x0000DDEE File Offset: 0x0000BFEE
	public float MinValue
	{
		get
		{
			return this.minValue;
		}
		set
		{
			if (value != this.minValue)
			{
				this.minValue = value;
				if (this.rawValue < value)
				{
					this.Value = value;
				}
				this.Invalidate();
			}
		}
	}

	// Token: 0x170000AF RID: 175
	// (get) Token: 0x060002F2 RID: 754 RVA: 0x0000DE16 File Offset: 0x0000C016
	// (set) Token: 0x060002F3 RID: 755 RVA: 0x0000DE1E File Offset: 0x0000C01E
	public float MaxValue
	{
		get
		{
			return this.maxValue;
		}
		set
		{
			if (value != this.maxValue)
			{
				this.maxValue = value;
				if (this.rawValue > value)
				{
					this.Value = value;
				}
				this.Invalidate();
			}
		}
	}

	// Token: 0x170000B0 RID: 176
	// (get) Token: 0x060002F4 RID: 756 RVA: 0x0000DE46 File Offset: 0x0000C046
	// (set) Token: 0x060002F5 RID: 757 RVA: 0x0000DE4E File Offset: 0x0000C04E
	public float Value
	{
		get
		{
			return this.rawValue;
		}
		set
		{
			value = Mathf.Max(this.minValue, Mathf.Min(this.maxValue, value));
			if (!Mathf.Approximately(value, this.rawValue))
			{
				this.rawValue = value;
				this.OnValueChanged();
			}
		}
	}

	// Token: 0x170000B1 RID: 177
	// (get) Token: 0x060002F6 RID: 758 RVA: 0x0000DE84 File Offset: 0x0000C084
	// (set) Token: 0x060002F7 RID: 759 RVA: 0x0000DE8C File Offset: 0x0000C08C
	public dfProgressFillMode FillMode
	{
		get
		{
			return this.fillMode;
		}
		set
		{
			if (value != this.fillMode)
			{
				this.fillMode = value;
				this.Invalidate();
			}
		}
	}

	// Token: 0x170000B2 RID: 178
	// (get) Token: 0x060002F8 RID: 760 RVA: 0x0000DEA4 File Offset: 0x0000C0A4
	// (set) Token: 0x060002F9 RID: 761 RVA: 0x0000DEBF File Offset: 0x0000C0BF
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
			if (!object.Equals(value, this.padding))
			{
				this.padding = value;
				this.Invalidate();
			}
		}
	}

	// Token: 0x170000B3 RID: 179
	// (get) Token: 0x060002FA RID: 762 RVA: 0x0000DEDC File Offset: 0x0000C0DC
	// (set) Token: 0x060002FB RID: 763 RVA: 0x0000DEE4 File Offset: 0x0000C0E4
	public bool ActAsSlider
	{
		get
		{
			return this.actAsSlider;
		}
		set
		{
			this.actAsSlider = value;
		}
	}

	// Token: 0x060002FC RID: 764 RVA: 0x0000DEF0 File Offset: 0x0000C0F0
	protected internal override void OnMouseWheel(dfMouseEventArgs args)
	{
		try
		{
			if (this.actAsSlider)
			{
				float num = (this.maxValue - this.minValue) * 0.1f;
				this.Value += num * (float)Mathf.RoundToInt(-args.WheelDelta);
				args.Use();
			}
		}
		finally
		{
			base.OnMouseWheel(args);
		}
	}

	// Token: 0x060002FD RID: 765 RVA: 0x0000DF58 File Offset: 0x0000C158
	protected internal override void OnMouseMove(dfMouseEventArgs args)
	{
		try
		{
			if (this.actAsSlider)
			{
				if (args.Buttons.IsSet(dfMouseButtons.Left))
				{
					this.Value = this.getValueFromMouseEvent(args);
					args.Use();
				}
			}
		}
		finally
		{
			base.OnMouseMove(args);
		}
	}

	// Token: 0x060002FE RID: 766 RVA: 0x0000DFAC File Offset: 0x0000C1AC
	protected internal override void OnMouseDown(dfMouseEventArgs args)
	{
		try
		{
			if (this.actAsSlider)
			{
				if (args.Buttons.IsSet(dfMouseButtons.Left))
				{
					base.Focus();
					this.Value = this.getValueFromMouseEvent(args);
					args.Use();
				}
			}
		}
		finally
		{
			base.OnMouseDown(args);
		}
	}

	// Token: 0x060002FF RID: 767 RVA: 0x0000E008 File Offset: 0x0000C208
	protected internal override void OnKeyDown(dfKeyEventArgs args)
	{
		try
		{
			if (this.actAsSlider)
			{
				float num = (this.maxValue - this.minValue) * 0.1f;
				if (args.KeyCode == KeyCode.LeftArrow)
				{
					this.Value -= num;
					args.Use();
				}
				else if (args.KeyCode == KeyCode.RightArrow)
				{
					this.Value += num;
					args.Use();
				}
			}
		}
		finally
		{
			base.OnKeyDown(args);
		}
	}

	// Token: 0x06000300 RID: 768 RVA: 0x0000E094 File Offset: 0x0000C294
	protected internal virtual void OnValueChanged()
	{
		this.Invalidate();
		base.SignalHierarchy("OnValueChanged", new object[]
		{
			this,
			this.Value
		});
		if (this.ValueChanged != null)
		{
			this.ValueChanged(this, this.Value);
		}
	}

	// Token: 0x06000301 RID: 769 RVA: 0x0000E0E5 File Offset: 0x0000C2E5
	protected override void OnRebuildRenderData()
	{
		if (this.Atlas == null)
		{
			return;
		}
		this.renderData.Material = this.Atlas.Material;
		this.renderBackground();
		this.renderProgressFill();
	}

	// Token: 0x06000302 RID: 770 RVA: 0x0000E118 File Offset: 0x0000C318
	private void renderProgressFill()
	{
		if (this.Atlas == null)
		{
			return;
		}
		dfAtlas.ItemInfo itemInfo = this.Atlas[this.progressSprite];
		if (itemInfo == null)
		{
			return;
		}
		Vector3 b = new Vector3((float)this.padding.left, (float)(-(float)this.padding.top));
		Vector2 vector = new Vector2(this.size.x - (float)this.padding.horizontal, this.size.y - (float)this.padding.vertical);
		float fillAmount = 1f;
		float num = this.maxValue - this.minValue;
		float num2 = (this.rawValue - this.minValue) / num;
		dfProgressFillMode dfProgressFillMode = this.fillMode;
		if (dfProgressFillMode == dfProgressFillMode.Stretch)
		{
			float num3 = vector.x * num2;
			float num4 = (float)itemInfo.border.horizontal;
		}
		if (dfProgressFillMode == dfProgressFillMode.Fill)
		{
			fillAmount = num2;
		}
		else
		{
			vector.x = Mathf.Max((float)itemInfo.border.horizontal, vector.x * num2);
		}
		Color32 color = base.ApplyOpacity(base.IsEnabled ? this.ProgressColor : base.DisabledColor);
		dfSprite.RenderOptions options = new dfSprite.RenderOptions
		{
			atlas = this.atlas,
			color = color,
			fillAmount = fillAmount,
			flip = dfSpriteFlip.None,
			offset = this.pivot.TransformToUpperLeft(base.Size) + b,
			pixelsToUnits = base.PixelsToUnits(),
			size = vector,
			spriteInfo = itemInfo
		};
		if (itemInfo.border.horizontal == 0 && itemInfo.border.vertical == 0)
		{
			dfSprite.renderSprite(this.renderData, options);
			return;
		}
		dfSlicedSprite.renderSprite(this.renderData, options);
	}

	// Token: 0x06000303 RID: 771 RVA: 0x0000E2D8 File Offset: 0x0000C4D8
	private void renderBackground()
	{
		if (this.Atlas == null)
		{
			return;
		}
		dfAtlas.ItemInfo itemInfo = this.Atlas[this.backgroundSprite];
		if (itemInfo == null)
		{
			return;
		}
		Color32 color = base.ApplyOpacity(base.IsEnabled ? base.Color : base.DisabledColor);
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

	// Token: 0x06000304 RID: 772 RVA: 0x0000E3D0 File Offset: 0x0000C5D0
	private float getValueFromMouseEvent(dfMouseEventArgs args)
	{
		Vector3[] endPoints = this.getEndPoints(true);
		Vector3 vector = endPoints[0];
		Vector3 vector2 = endPoints[1];
		Plane plane = new Plane(base.transform.TransformDirection(Vector3.back), vector);
		Ray ray = args.Ray;
		float d = 0f;
		if (!plane.Raycast(ray, out d))
		{
			return this.rawValue;
		}
		Vector3 test = ray.origin + ray.direction * d;
		float num = (dfProgressBar.closestPoint(vector, vector2, test, true) - vector).magnitude / (vector2 - vector).magnitude;
		return this.minValue + (this.maxValue - this.minValue) * num;
	}

	// Token: 0x06000305 RID: 773 RVA: 0x0000E48B File Offset: 0x0000C68B
	private Vector3[] getEndPoints()
	{
		return this.getEndPoints(false);
	}

	// Token: 0x06000306 RID: 774 RVA: 0x0000E494 File Offset: 0x0000C694
	private Vector3[] getEndPoints(bool convertToWorld)
	{
		Vector3 vector = this.pivot.TransformToUpperLeft(base.Size);
		Vector3 vector2 = new Vector3(vector.x + (float)this.padding.left, vector.y - this.size.y * 0.5f);
		Vector3 vector3 = vector2 + new Vector3(this.size.x - (float)this.padding.right, 0f);
		if (convertToWorld)
		{
			float d = base.PixelsToUnits();
			Matrix4x4 localToWorldMatrix = base.transform.localToWorldMatrix;
			vector2 = localToWorldMatrix.MultiplyPoint(vector2 * d);
			vector3 = localToWorldMatrix.MultiplyPoint(vector3 * d);
		}
		return new Vector3[]
		{
			vector2,
			vector3
		};
	}

	// Token: 0x06000307 RID: 775 RVA: 0x0000E55C File Offset: 0x0000C75C
	private static Vector3 closestPoint(Vector3 start, Vector3 end, Vector3 test, bool clamp)
	{
		Vector3 rhs = test - start;
		Vector3 vector = (end - start).normalized;
		float magnitude = (end - start).magnitude;
		float num = Vector3.Dot(vector, rhs);
		if (clamp)
		{
			if (num < 0f)
			{
				return start;
			}
			if (num > magnitude)
			{
				return end;
			}
		}
		vector *= num;
		return start + vector;
	}

	// Token: 0x06000308 RID: 776 RVA: 0x0000E5C0 File Offset: 0x0000C7C0
	private void setDefaultSize(string spriteName)
	{
		if (this.Atlas == null)
		{
			return;
		}
		dfAtlas.ItemInfo itemInfo = this.Atlas[spriteName];
		if (this.size == Vector2.zero && itemInfo != null)
		{
			base.Size = itemInfo.sizeInPixels;
		}
	}

	// Token: 0x04000106 RID: 262
	[SerializeField]
	protected dfAtlas atlas;

	// Token: 0x04000107 RID: 263
	[SerializeField]
	protected string backgroundSprite;

	// Token: 0x04000108 RID: 264
	[SerializeField]
	protected string progressSprite;

	// Token: 0x04000109 RID: 265
	[SerializeField]
	protected Color32 progressColor = UnityEngine.Color.white;

	// Token: 0x0400010A RID: 266
	[SerializeField]
	protected float rawValue = 0.25f;

	// Token: 0x0400010B RID: 267
	[SerializeField]
	protected float minValue;

	// Token: 0x0400010C RID: 268
	[SerializeField]
	protected float maxValue = 1f;

	// Token: 0x0400010D RID: 269
	[SerializeField]
	protected dfProgressFillMode fillMode;

	// Token: 0x0400010E RID: 270
	[SerializeField]
	protected RectOffset padding = new RectOffset();

	// Token: 0x0400010F RID: 271
	[SerializeField]
	protected bool actAsSlider;
}

using System;
using UnityEngine;

// Token: 0x02000017 RID: 23
[dfCategory("Basic Controls")]
[dfTooltip("Allows the user to select any value from a specified range by moving an indicator along a horizontal or vertical track")]
[dfHelp("http://www.daikonforge.com/docs/df-gui/classdf_slider.html")]
[ExecuteInEditMode]
[AddComponentMenu("Daikon Forge/User Interface/Slider")]
[Serializable]
public class dfSlider : dfControl
{
	// Token: 0x14000033 RID: 51
	// (add) Token: 0x060003BE RID: 958 RVA: 0x000133BC File Offset: 0x000115BC
	// (remove) Token: 0x060003BF RID: 959 RVA: 0x000133F4 File Offset: 0x000115F4
	public event PropertyChangedEventHandler<float> ValueChanged;

	// Token: 0x170000DB RID: 219
	// (get) Token: 0x060003C0 RID: 960 RVA: 0x0001342C File Offset: 0x0001162C
	// (set) Token: 0x060003C1 RID: 961 RVA: 0x0001346D File Offset: 0x0001166D
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

	// Token: 0x170000DC RID: 220
	// (get) Token: 0x060003C2 RID: 962 RVA: 0x0001348A File Offset: 0x0001168A
	// (set) Token: 0x060003C3 RID: 963 RVA: 0x00013492 File Offset: 0x00011692
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

	// Token: 0x170000DD RID: 221
	// (get) Token: 0x060003C4 RID: 964 RVA: 0x000134AF File Offset: 0x000116AF
	// (set) Token: 0x060003C5 RID: 965 RVA: 0x000134B7 File Offset: 0x000116B7
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
				this.updateValueIndicators(this.rawValue);
				this.Invalidate();
			}
		}
	}

	// Token: 0x170000DE RID: 222
	// (get) Token: 0x060003C6 RID: 966 RVA: 0x000134EB File Offset: 0x000116EB
	// (set) Token: 0x060003C7 RID: 967 RVA: 0x000134F3 File Offset: 0x000116F3
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
				this.updateValueIndicators(this.rawValue);
				this.Invalidate();
			}
		}
	}

	// Token: 0x170000DF RID: 223
	// (get) Token: 0x060003C8 RID: 968 RVA: 0x00013527 File Offset: 0x00011727
	// (set) Token: 0x060003C9 RID: 969 RVA: 0x0001352F File Offset: 0x0001172F
	public float StepSize
	{
		get
		{
			return this.stepSize;
		}
		set
		{
			value = Mathf.Max(0f, value);
			if (value != this.stepSize)
			{
				this.stepSize = value;
				this.Value = this.rawValue.Quantize(value);
				this.Invalidate();
			}
		}
	}

	// Token: 0x170000E0 RID: 224
	// (get) Token: 0x060003CA RID: 970 RVA: 0x00013566 File Offset: 0x00011766
	// (set) Token: 0x060003CB RID: 971 RVA: 0x0001356E File Offset: 0x0001176E
	public float ScrollSize
	{
		get
		{
			return this.scrollSize;
		}
		set
		{
			value = Mathf.Max(0f, value);
			if (value != this.scrollSize)
			{
				this.scrollSize = value;
				this.Invalidate();
			}
		}
	}

	// Token: 0x170000E1 RID: 225
	// (get) Token: 0x060003CC RID: 972 RVA: 0x00013593 File Offset: 0x00011793
	// (set) Token: 0x060003CD RID: 973 RVA: 0x0001359B File Offset: 0x0001179B
	public dfControlOrientation Orientation
	{
		get
		{
			return this.orientation;
		}
		set
		{
			if (value != this.orientation)
			{
				this.orientation = value;
				this.Invalidate();
				this.updateValueIndicators(this.rawValue);
			}
		}
	}

	// Token: 0x170000E2 RID: 226
	// (get) Token: 0x060003CE RID: 974 RVA: 0x000135BF File Offset: 0x000117BF
	// (set) Token: 0x060003CF RID: 975 RVA: 0x000135C8 File Offset: 0x000117C8
	public float Value
	{
		get
		{
			return this.rawValue;
		}
		set
		{
			value = Mathf.Max(this.minValue, Mathf.Min(this.maxValue, value.RoundToNearest(this.stepSize)));
			if (!Mathf.Approximately(value, this.rawValue))
			{
				this.rawValue = value;
				this.OnValueChanged();
			}
		}
	}

	// Token: 0x170000E3 RID: 227
	// (get) Token: 0x060003D0 RID: 976 RVA: 0x00013614 File Offset: 0x00011814
	// (set) Token: 0x060003D1 RID: 977 RVA: 0x0001361C File Offset: 0x0001181C
	public dfControl Thumb
	{
		get
		{
			return this.thumb;
		}
		set
		{
			if (value != this.thumb)
			{
				this.thumb = value;
				this.Invalidate();
				this.updateValueIndicators(this.rawValue);
			}
		}
	}

	// Token: 0x170000E4 RID: 228
	// (get) Token: 0x060003D2 RID: 978 RVA: 0x00013645 File Offset: 0x00011845
	// (set) Token: 0x060003D3 RID: 979 RVA: 0x0001364D File Offset: 0x0001184D
	public dfControl Progress
	{
		get
		{
			return this.fillIndicator;
		}
		set
		{
			if (value != this.fillIndicator)
			{
				this.fillIndicator = value;
				this.Invalidate();
				this.updateValueIndicators(this.rawValue);
			}
		}
	}

	// Token: 0x170000E5 RID: 229
	// (get) Token: 0x060003D4 RID: 980 RVA: 0x00013676 File Offset: 0x00011876
	// (set) Token: 0x060003D5 RID: 981 RVA: 0x0001367E File Offset: 0x0001187E
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

	// Token: 0x170000E6 RID: 230
	// (get) Token: 0x060003D6 RID: 982 RVA: 0x00013696 File Offset: 0x00011896
	// (set) Token: 0x060003D7 RID: 983 RVA: 0x000136B1 File Offset: 0x000118B1
	public RectOffset FillPadding
	{
		get
		{
			if (this.fillPadding == null)
			{
				this.fillPadding = new RectOffset();
			}
			return this.fillPadding;
		}
		set
		{
			if (!object.Equals(value, this.fillPadding))
			{
				this.fillPadding = value;
				this.updateValueIndicators(this.rawValue);
				this.Invalidate();
			}
		}
	}

	// Token: 0x170000E7 RID: 231
	// (get) Token: 0x060003D8 RID: 984 RVA: 0x000136DA File Offset: 0x000118DA
	// (set) Token: 0x060003D9 RID: 985 RVA: 0x000136E2 File Offset: 0x000118E2
	public Vector2 ThumbOffset
	{
		get
		{
			return this.thumbOffset;
		}
		set
		{
			if (Vector2.Distance(value, this.thumbOffset) > 1E-45f)
			{
				this.thumbOffset = value;
				this.updateValueIndicators(this.rawValue);
			}
		}
	}

	// Token: 0x170000E8 RID: 232
	// (get) Token: 0x060003DA RID: 986 RVA: 0x0001370A File Offset: 0x0001190A
	// (set) Token: 0x060003DB RID: 987 RVA: 0x00013712 File Offset: 0x00011912
	public bool RightToLeft
	{
		get
		{
			return this.rightToLeft;
		}
		set
		{
			if (value != this.rightToLeft)
			{
				this.rightToLeft = value;
				this.updateValueIndicators(this.rawValue);
			}
		}
	}

	// Token: 0x060003DC RID: 988 RVA: 0x00013730 File Offset: 0x00011930
	protected internal override void OnKeyDown(dfKeyEventArgs args)
	{
		if (args.Used)
		{
			return;
		}
		if (this.Orientation == dfControlOrientation.Horizontal)
		{
			if (args.KeyCode == KeyCode.LeftArrow)
			{
				this.Value -= (this.rightToLeft ? (-this.scrollSize) : this.scrollSize);
				args.Use();
				return;
			}
			if (args.KeyCode == KeyCode.RightArrow)
			{
				this.Value += (this.rightToLeft ? (-this.scrollSize) : this.scrollSize);
				args.Use();
				return;
			}
		}
		else
		{
			if (args.KeyCode == KeyCode.UpArrow)
			{
				this.Value += this.ScrollSize;
				args.Use();
				return;
			}
			if (args.KeyCode == KeyCode.DownArrow)
			{
				this.Value -= this.ScrollSize;
				args.Use();
				return;
			}
		}
		base.OnKeyDown(args);
	}

	// Token: 0x060003DD RID: 989 RVA: 0x00013813 File Offset: 0x00011A13
	public override void Start()
	{
		base.Start();
		this.updateValueIndicators(this.rawValue);
	}

	// Token: 0x060003DE RID: 990 RVA: 0x00013827 File Offset: 0x00011A27
	public override void OnEnable()
	{
		if (this.size.magnitude < 1E-45f)
		{
			this.size = new Vector2(100f, 25f);
		}
		base.OnEnable();
		this.updateValueIndicators(this.rawValue);
	}

	// Token: 0x060003DF RID: 991 RVA: 0x00013864 File Offset: 0x00011A64
	protected internal override void OnMouseWheel(dfMouseEventArgs args)
	{
		int num = (this.orientation == dfControlOrientation.Horizontal) ? -1 : 1;
		this.Value += this.scrollSize * args.WheelDelta * (float)num;
		args.Use();
		base.Signal("OnMouseWheel", args);
		base.raiseMouseWheelEvent(args);
	}

	// Token: 0x060003E0 RID: 992 RVA: 0x000138B5 File Offset: 0x00011AB5
	protected internal override void OnMouseMove(dfMouseEventArgs args)
	{
		if (!args.Buttons.IsSet(dfMouseButtons.Left))
		{
			base.OnMouseMove(args);
			return;
		}
		this.Value = this.getValueFromMouseEvent(args);
		args.Use();
		base.Signal("OnMouseMove", this, args);
		base.raiseMouseMoveEvent(args);
	}

	// Token: 0x060003E1 RID: 993 RVA: 0x000138F8 File Offset: 0x00011AF8
	protected internal override void OnMouseDown(dfMouseEventArgs args)
	{
		if (!args.Buttons.IsSet(dfMouseButtons.Left))
		{
			base.OnMouseMove(args);
			return;
		}
		base.Focus();
		this.Value = this.getValueFromMouseEvent(args);
		args.Use();
		base.Signal("OnMouseDown", this, args);
		base.raiseMouseDownEvent(args);
	}

	// Token: 0x060003E2 RID: 994 RVA: 0x00013949 File Offset: 0x00011B49
	protected internal override void OnSizeChanged()
	{
		base.OnSizeChanged();
		this.updateValueIndicators(this.rawValue);
	}

	// Token: 0x060003E3 RID: 995 RVA: 0x00013960 File Offset: 0x00011B60
	protected internal virtual void OnValueChanged()
	{
		this.Invalidate();
		this.updateValueIndicators(this.rawValue);
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

	// Token: 0x170000E9 RID: 233
	// (get) Token: 0x060003E4 RID: 996 RVA: 0x000139BD File Offset: 0x00011BBD
	public override bool CanFocus
	{
		get
		{
			return (base.IsEnabled && base.IsVisible) || base.CanFocus;
		}
	}

	// Token: 0x060003E5 RID: 997 RVA: 0x000139D7 File Offset: 0x00011BD7
	protected override void OnRebuildRenderData()
	{
		if (this.Atlas == null)
		{
			return;
		}
		this.renderData.Material = this.Atlas.Material;
		this.renderBackground();
	}

	// Token: 0x060003E6 RID: 998 RVA: 0x00013A04 File Offset: 0x00011C04
	protected internal virtual void renderBackground()
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

	// Token: 0x060003E7 RID: 999 RVA: 0x00013AFC File Offset: 0x00011CFC
	private void updateValueIndicators(float rawValue)
	{
		if (Mathf.Approximately(this.MinValue, this.MaxValue))
		{
			if (Application.isEditor)
			{
				Debug.LogWarning("Slider Min and Max values cannot be the same", this);
			}
			if (this.thumb != null)
			{
				this.thumb.IsVisible = false;
			}
			if (this.fillIndicator != null)
			{
				this.fillIndicator.IsVisible = false;
			}
			return;
		}
		if (this.thumb != null)
		{
			this.thumb.IsVisible = true;
		}
		if (this.fillIndicator != null)
		{
			this.fillIndicator.IsVisible = true;
		}
		if (this.thumb != null)
		{
			Vector3[] endPoints = this.getEndPoints(true);
			Vector3 vector = endPoints[1] - endPoints[0];
			float num = this.maxValue - this.minValue;
			float d = (rawValue - this.minValue) / num * vector.magnitude;
			Vector3 b = this.thumbOffset * base.PixelsToUnits();
			Vector3 position = endPoints[0] + vector.normalized * d + b;
			if (this.orientation == dfControlOrientation.Vertical || this.rightToLeft)
			{
				position = endPoints[1] + -vector.normalized * d + b;
			}
			this.thumb.Pivot = dfPivotPoint.MiddleCenter;
			this.thumb.transform.position = position;
		}
		if (this.fillIndicator == null)
		{
			return;
		}
		RectOffset rectOffset = this.FillPadding;
		float num2 = (rawValue - this.minValue) / (this.maxValue - this.minValue);
		Vector3 relativePosition = new Vector3((float)rectOffset.left, (float)rectOffset.top);
		Vector2 vector2 = this.size - new Vector2((float)rectOffset.horizontal, (float)rectOffset.vertical);
		dfSprite dfSprite = this.fillIndicator as dfSprite;
		if (dfSprite != null && this.fillMode == dfProgressFillMode.Fill)
		{
			dfSprite.FillAmount = num2;
			dfSprite.FillDirection = ((this.orientation == dfControlOrientation.Horizontal) ? dfFillDirection.Horizontal : dfFillDirection.Vertical);
			dfSprite.InvertFill = (this.rightToLeft || this.orientation == dfControlOrientation.Vertical);
		}
		else if (this.orientation == dfControlOrientation.Horizontal)
		{
			vector2.x = base.Width * num2 - (float)rectOffset.horizontal;
		}
		else
		{
			vector2.y = base.Height * num2 - (float)rectOffset.vertical;
			relativePosition.y = base.Height - vector2.y;
		}
		this.fillIndicator.Size = vector2;
		this.fillIndicator.RelativePosition = relativePosition;
	}

	// Token: 0x060003E8 RID: 1000 RVA: 0x00013DA0 File Offset: 0x00011FA0
	private float getValueFromMouseEvent(dfMouseEventArgs args)
	{
		Vector3[] endPoints = this.getEndPoints(true);
		Vector3 vector = endPoints[0];
		Vector3 vector2 = endPoints[1];
		if (this.orientation == dfControlOrientation.Vertical || this.rightToLeft)
		{
			vector = endPoints[1];
			vector2 = endPoints[0];
		}
		Plane plane = new Plane(base.transform.TransformDirection(Vector3.back), vector);
		Ray ray = args.Ray;
		float distance = 0f;
		if (!plane.Raycast(ray, out distance))
		{
			return this.rawValue;
		}
		Vector3 point = ray.GetPoint(distance);
		float num = (dfSlider.closestPoint(vector, vector2, point, true) - vector).magnitude / (vector2 - vector).magnitude;
		return this.minValue + (this.maxValue - this.minValue) * num;
	}

	// Token: 0x060003E9 RID: 1001 RVA: 0x00013E6F File Offset: 0x0001206F
	private Vector3[] getEndPoints()
	{
		return this.getEndPoints(false);
	}

	// Token: 0x060003EA RID: 1002 RVA: 0x00013E78 File Offset: 0x00012078
	private Vector3[] getEndPoints(bool convertToWorld)
	{
		Vector3 vector = this.pivot.TransformToUpperLeft(base.Size);
		Vector3 vector2 = new Vector3(vector.x, vector.y - this.size.y * 0.5f);
		Vector3 vector3 = vector2 + new Vector3(this.size.x, 0f);
		if (this.orientation == dfControlOrientation.Vertical)
		{
			vector2 = new Vector3(vector.x + this.size.x * 0.5f, vector.y);
			vector3 = vector2 - new Vector3(0f, this.size.y);
		}
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

	// Token: 0x060003EB RID: 1003 RVA: 0x00013F70 File Offset: 0x00012170
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

	// Token: 0x0400014C RID: 332
	[SerializeField]
	protected dfAtlas atlas;

	// Token: 0x0400014D RID: 333
	[SerializeField]
	protected string backgroundSprite;

	// Token: 0x0400014E RID: 334
	[SerializeField]
	protected dfControlOrientation orientation;

	// Token: 0x0400014F RID: 335
	[SerializeField]
	protected float rawValue = 10f;

	// Token: 0x04000150 RID: 336
	[SerializeField]
	protected float minValue;

	// Token: 0x04000151 RID: 337
	[SerializeField]
	protected float maxValue = 100f;

	// Token: 0x04000152 RID: 338
	[SerializeField]
	protected float stepSize = 1f;

	// Token: 0x04000153 RID: 339
	[SerializeField]
	protected float scrollSize = 1f;

	// Token: 0x04000154 RID: 340
	[SerializeField]
	protected dfControl thumb;

	// Token: 0x04000155 RID: 341
	[SerializeField]
	protected dfControl fillIndicator;

	// Token: 0x04000156 RID: 342
	[SerializeField]
	protected dfProgressFillMode fillMode = dfProgressFillMode.Fill;

	// Token: 0x04000157 RID: 343
	[SerializeField]
	protected RectOffset fillPadding = new RectOffset();

	// Token: 0x04000158 RID: 344
	[SerializeField]
	protected Vector2 thumbOffset = Vector2.zero;

	// Token: 0x04000159 RID: 345
	[SerializeField]
	protected bool rightToLeft;
}

using System;
using UnityEngine;

// Token: 0x02000015 RID: 21
[dfCategory("Basic Controls")]
[dfTooltip("Implements a common Scrollbar control")]
[dfHelp("http://www.daikonforge.com/docs/df-gui/classdf_scrollbar.html")]
[ExecuteInEditMode]
[AddComponentMenu("Daikon Forge/User Interface/Scrollbar")]
[Serializable]
public class dfScrollbar : dfControl
{
	// Token: 0x14000032 RID: 50
	// (add) Token: 0x0600037E RID: 894 RVA: 0x00011590 File Offset: 0x0000F790
	// (remove) Token: 0x0600037F RID: 895 RVA: 0x000115C8 File Offset: 0x0000F7C8
	public event PropertyChangedEventHandler<float> ValueChanged;

	// Token: 0x170000CC RID: 204
	// (get) Token: 0x06000380 RID: 896 RVA: 0x00011600 File Offset: 0x0000F800
	// (set) Token: 0x06000381 RID: 897 RVA: 0x00011641 File Offset: 0x0000F841
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

	// Token: 0x170000CD RID: 205
	// (get) Token: 0x06000382 RID: 898 RVA: 0x0001165E File Offset: 0x0000F85E
	// (set) Token: 0x06000383 RID: 899 RVA: 0x00011666 File Offset: 0x0000F866
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
				this.Value = this.Value;
				this.Invalidate();
				this.doAutoHide();
			}
		}
	}

	// Token: 0x170000CE RID: 206
	// (get) Token: 0x06000384 RID: 900 RVA: 0x00011690 File Offset: 0x0000F890
	// (set) Token: 0x06000385 RID: 901 RVA: 0x00011698 File Offset: 0x0000F898
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
				this.Value = this.Value;
				this.Invalidate();
				this.doAutoHide();
			}
		}
	}

	// Token: 0x170000CF RID: 207
	// (get) Token: 0x06000386 RID: 902 RVA: 0x000116C2 File Offset: 0x0000F8C2
	// (set) Token: 0x06000387 RID: 903 RVA: 0x000116CA File Offset: 0x0000F8CA
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
				this.Value = this.Value;
				this.Invalidate();
			}
		}
	}

	// Token: 0x170000D0 RID: 208
	// (get) Token: 0x06000388 RID: 904 RVA: 0x000116FB File Offset: 0x0000F8FB
	// (set) Token: 0x06000389 RID: 905 RVA: 0x00011703 File Offset: 0x0000F903
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
				this.Value = this.Value;
				this.Invalidate();
				this.doAutoHide();
			}
		}
	}

	// Token: 0x170000D1 RID: 209
	// (get) Token: 0x0600038A RID: 906 RVA: 0x0001173A File Offset: 0x0000F93A
	// (set) Token: 0x0600038B RID: 907 RVA: 0x00011742 File Offset: 0x0000F942
	public float IncrementAmount
	{
		get
		{
			return this.increment;
		}
		set
		{
			value = Mathf.Max(0f, value);
			if (!Mathf.Approximately(value, this.increment))
			{
				this.increment = value;
			}
		}
	}

	// Token: 0x170000D2 RID: 210
	// (get) Token: 0x0600038C RID: 908 RVA: 0x00011766 File Offset: 0x0000F966
	// (set) Token: 0x0600038D RID: 909 RVA: 0x0001176E File Offset: 0x0000F96E
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
			}
		}
	}

	// Token: 0x170000D3 RID: 211
	// (get) Token: 0x0600038E RID: 910 RVA: 0x00011786 File Offset: 0x0000F986
	// (set) Token: 0x0600038F RID: 911 RVA: 0x0001178E File Offset: 0x0000F98E
	public float Value
	{
		get
		{
			return this.rawValue;
		}
		set
		{
			value = this.adjustValue(value);
			if (!Mathf.Approximately(value, this.rawValue))
			{
				this.rawValue = value;
				this.OnValueChanged();
			}
			this.updateThumb(this.rawValue);
			this.doAutoHide();
		}
	}

	// Token: 0x170000D4 RID: 212
	// (get) Token: 0x06000390 RID: 912 RVA: 0x000117C6 File Offset: 0x0000F9C6
	// (set) Token: 0x06000391 RID: 913 RVA: 0x000117CE File Offset: 0x0000F9CE
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
			}
		}
	}

	// Token: 0x170000D5 RID: 213
	// (get) Token: 0x06000392 RID: 914 RVA: 0x000117EB File Offset: 0x0000F9EB
	// (set) Token: 0x06000393 RID: 915 RVA: 0x000117F3 File Offset: 0x0000F9F3
	public dfControl Track
	{
		get
		{
			return this.track;
		}
		set
		{
			if (value != this.track)
			{
				this.track = value;
				this.Invalidate();
			}
		}
	}

	// Token: 0x170000D6 RID: 214
	// (get) Token: 0x06000394 RID: 916 RVA: 0x00011810 File Offset: 0x0000FA10
	// (set) Token: 0x06000395 RID: 917 RVA: 0x00011818 File Offset: 0x0000FA18
	public dfControl IncButton
	{
		get
		{
			return this.incButton;
		}
		set
		{
			if (value != this.incButton)
			{
				this.incButton = value;
				this.Invalidate();
			}
		}
	}

	// Token: 0x170000D7 RID: 215
	// (get) Token: 0x06000396 RID: 918 RVA: 0x00011835 File Offset: 0x0000FA35
	// (set) Token: 0x06000397 RID: 919 RVA: 0x0001183D File Offset: 0x0000FA3D
	public dfControl DecButton
	{
		get
		{
			return this.decButton;
		}
		set
		{
			if (value != this.decButton)
			{
				this.decButton = value;
				this.Invalidate();
			}
		}
	}

	// Token: 0x170000D8 RID: 216
	// (get) Token: 0x06000398 RID: 920 RVA: 0x0001185A File Offset: 0x0000FA5A
	// (set) Token: 0x06000399 RID: 921 RVA: 0x00011878 File Offset: 0x0000FA78
	public RectOffset ThumbPadding
	{
		get
		{
			if (this.thumbPadding == null)
			{
				this.thumbPadding = new RectOffset();
			}
			return this.thumbPadding;
		}
		set
		{
			if (this.orientation == dfControlOrientation.Horizontal)
			{
				value.top = (value.bottom = 0);
			}
			else
			{
				value.left = (value.right = 0);
			}
			if (!object.Equals(value, this.thumbPadding))
			{
				this.thumbPadding = value;
				this.updateThumb(this.rawValue);
			}
		}
	}

	// Token: 0x170000D9 RID: 217
	// (get) Token: 0x0600039A RID: 922 RVA: 0x000118D0 File Offset: 0x0000FAD0
	// (set) Token: 0x0600039B RID: 923 RVA: 0x000118D8 File Offset: 0x0000FAD8
	public bool AutoHide
	{
		get
		{
			return this.autoHide;
		}
		set
		{
			if (value != this.autoHide)
			{
				this.autoHide = value;
				this.Invalidate();
				this.doAutoHide();
			}
		}
	}

	// Token: 0x0600039C RID: 924 RVA: 0x000118F8 File Offset: 0x0000FAF8
	public override Vector2 CalculateMinimumSize()
	{
		Vector2[] array = new Vector2[3];
		if (this.decButton != null)
		{
			array[0] = this.decButton.CalculateMinimumSize();
		}
		if (this.incButton != null)
		{
			array[1] = this.incButton.CalculateMinimumSize();
		}
		if (this.thumb != null)
		{
			array[2] = this.thumb.CalculateMinimumSize();
		}
		Vector2 zero = Vector2.zero;
		if (this.orientation == dfControlOrientation.Horizontal)
		{
			zero.x = array[0].x + array[1].x + array[2].x;
			zero.y = Mathf.Max(new float[]
			{
				array[0].y,
				array[1].y,
				array[2].y
			});
		}
		else
		{
			zero.x = Mathf.Max(new float[]
			{
				array[0].x,
				array[1].x,
				array[2].x
			});
			zero.y = array[0].y + array[1].y + array[2].y;
		}
		return Vector2.Max(zero, base.CalculateMinimumSize());
	}

	// Token: 0x170000DA RID: 218
	// (get) Token: 0x0600039D RID: 925 RVA: 0x00011A60 File Offset: 0x0000FC60
	public override bool CanFocus
	{
		get
		{
			return (base.IsEnabled && base.IsVisible) || base.CanFocus;
		}
	}

	// Token: 0x0600039E RID: 926 RVA: 0x00011A7A File Offset: 0x0000FC7A
	protected override void OnRebuildRenderData()
	{
		this.updateThumb(this.rawValue);
		base.OnRebuildRenderData();
	}

	// Token: 0x0600039F RID: 927 RVA: 0x00011A8E File Offset: 0x0000FC8E
	public override void Start()
	{
		base.Start();
		this.attachEvents();
	}

	// Token: 0x060003A0 RID: 928 RVA: 0x00011A9C File Offset: 0x0000FC9C
	public override void OnDisable()
	{
		base.OnDisable();
		this.detachEvents();
	}

	// Token: 0x060003A1 RID: 929 RVA: 0x00011AAA File Offset: 0x0000FCAA
	public override void OnDestroy()
	{
		base.OnDestroy();
		this.detachEvents();
	}

	// Token: 0x060003A2 RID: 930 RVA: 0x00011AB8 File Offset: 0x0000FCB8
	private void attachEvents()
	{
		if (!Application.isPlaying)
		{
			return;
		}
		if (this.IncButton != null)
		{
			this.IncButton.MouseDown += this.incrementPressed;
			this.IncButton.MouseHover += this.incrementPressed;
		}
		if (this.DecButton != null)
		{
			this.DecButton.MouseDown += this.decrementPressed;
			this.DecButton.MouseHover += this.decrementPressed;
		}
	}

	// Token: 0x060003A3 RID: 931 RVA: 0x00011B48 File Offset: 0x0000FD48
	private void detachEvents()
	{
		if (!Application.isPlaying)
		{
			return;
		}
		if (this.IncButton != null)
		{
			this.IncButton.MouseDown -= this.incrementPressed;
			this.IncButton.MouseHover -= this.incrementPressed;
		}
		if (this.DecButton != null)
		{
			this.DecButton.MouseDown -= this.decrementPressed;
			this.DecButton.MouseHover -= this.decrementPressed;
		}
	}

	// Token: 0x060003A4 RID: 932 RVA: 0x00011BD8 File Offset: 0x0000FDD8
	protected internal override void OnKeyDown(dfKeyEventArgs args)
	{
		if (this.Orientation == dfControlOrientation.Horizontal)
		{
			if (args.KeyCode == KeyCode.LeftArrow)
			{
				this.Value -= this.IncrementAmount;
				args.Use();
				return;
			}
			if (args.KeyCode == KeyCode.RightArrow)
			{
				this.Value += this.IncrementAmount;
				args.Use();
				return;
			}
		}
		else
		{
			if (args.KeyCode == KeyCode.UpArrow)
			{
				this.Value -= this.IncrementAmount;
				args.Use();
				return;
			}
			if (args.KeyCode == KeyCode.DownArrow)
			{
				this.Value += this.IncrementAmount;
				args.Use();
				return;
			}
		}
		base.OnKeyDown(args);
	}

	// Token: 0x060003A5 RID: 933 RVA: 0x00011C90 File Offset: 0x0000FE90
	protected internal override void OnMouseWheel(dfMouseEventArgs args)
	{
		this.Value += this.IncrementAmount * -args.WheelDelta;
		args.Use();
		base.Signal("OnMouseWheel", this, args);
	}

	// Token: 0x060003A6 RID: 934 RVA: 0x00011CC4 File Offset: 0x0000FEC4
	protected internal override void OnMouseHover(dfMouseEventArgs args)
	{
		if (args.Source == this.incButton || args.Source == this.decButton || args.Source == this.thumb)
		{
			return;
		}
		if (args.Source != this.track || !args.Buttons.IsSet(dfMouseButtons.Left))
		{
			base.OnMouseHover(args);
			return;
		}
		this.updateFromTrackClick(args);
		args.Use();
		base.Signal("OnMouseHover", this, args);
	}

	// Token: 0x060003A7 RID: 935 RVA: 0x00011D54 File Offset: 0x0000FF54
	protected internal override void OnMouseMove(dfMouseEventArgs args)
	{
		if (args.Source == this.incButton || args.Source == this.decButton)
		{
			return;
		}
		if ((args.Source != this.track && args.Source != this.thumb) || !args.Buttons.IsSet(dfMouseButtons.Left))
		{
			base.OnMouseMove(args);
			return;
		}
		this.Value = Mathf.Max(this.minValue, this.getValueFromMouseEvent(args) - this.scrollSize * 0.5f);
		args.Use();
		base.Signal("OnMouseMove", this, args);
	}

	// Token: 0x060003A8 RID: 936 RVA: 0x00011E00 File Offset: 0x00010000
	protected internal override void OnMouseDown(dfMouseEventArgs args)
	{
		if (args.Buttons.IsSet(dfMouseButtons.Left))
		{
			base.Focus();
		}
		if (args.Source == this.incButton || args.Source == this.decButton)
		{
			return;
		}
		if ((args.Source != this.track && args.Source != this.thumb) || !args.Buttons.IsSet(dfMouseButtons.Left))
		{
			base.OnMouseDown(args);
			return;
		}
		if (args.Source == this.thumb)
		{
			RaycastHit raycastHit;
			this.thumb.GetComponent<Collider>().Raycast(args.Ray, out raycastHit, 1000f);
			Vector3 a = this.thumb.transform.position + this.thumb.Pivot.TransformToCenter(this.thumb.Size * base.PixelsToUnits());
			this.thumbMouseOffset = a - raycastHit.point;
		}
		else
		{
			this.updateFromTrackClick(args);
		}
		args.Use();
		base.Signal("OnMouseDown", this, args);
	}

	// Token: 0x060003A9 RID: 937 RVA: 0x00011F24 File Offset: 0x00010124
	protected internal virtual void OnValueChanged()
	{
		this.doAutoHide();
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

	// Token: 0x060003AA RID: 938 RVA: 0x00011F7B File Offset: 0x0001017B
	protected internal override void OnSizeChanged()
	{
		base.OnSizeChanged();
		this.updateThumb(this.rawValue);
	}

	// Token: 0x060003AB RID: 939 RVA: 0x00011F8F File Offset: 0x0001018F
	private void doAutoHide()
	{
		if (!this.autoHide || !Application.isPlaying)
		{
			return;
		}
		if (Mathf.CeilToInt(this.ScrollSize) >= Mathf.CeilToInt(this.maxValue - this.minValue))
		{
			base.Hide();
			return;
		}
		base.Show();
	}

	// Token: 0x060003AC RID: 940 RVA: 0x00011FCD File Offset: 0x000101CD
	private void incrementPressed(dfControl sender, dfMouseEventArgs args)
	{
		if (args.Buttons.IsSet(dfMouseButtons.Left))
		{
			this.Value += this.IncrementAmount;
			args.Use();
		}
	}

	// Token: 0x060003AD RID: 941 RVA: 0x00011FF6 File Offset: 0x000101F6
	private void decrementPressed(dfControl sender, dfMouseEventArgs args)
	{
		if (args.Buttons.IsSet(dfMouseButtons.Left))
		{
			this.Value -= this.IncrementAmount;
			args.Use();
		}
	}

	// Token: 0x060003AE RID: 942 RVA: 0x00012020 File Offset: 0x00010220
	private void updateFromTrackClick(dfMouseEventArgs args)
	{
		float valueFromMouseEvent = this.getValueFromMouseEvent(args);
		if (valueFromMouseEvent > this.rawValue + this.scrollSize)
		{
			this.Value += this.scrollSize;
			return;
		}
		if (valueFromMouseEvent < this.rawValue)
		{
			this.Value -= this.scrollSize;
		}
	}

	// Token: 0x060003AF RID: 943 RVA: 0x00012078 File Offset: 0x00010278
	private float adjustValue(float value)
	{
		return Mathf.Max(Mathf.Min(Mathf.Max(Mathf.Max(this.maxValue - this.minValue, 0f) - this.scrollSize, 0f) + this.minValue, value), this.minValue).Quantize(this.stepSize);
	}

	// Token: 0x060003B0 RID: 944 RVA: 0x000120D0 File Offset: 0x000102D0
	private void updateThumb(float rawValue)
	{
		if (this.controls.Count == 0 || this.thumb == null || this.track == null || !base.IsVisible)
		{
			return;
		}
		float num = this.maxValue - this.minValue;
		if (num <= 0f || num <= this.scrollSize)
		{
			this.thumb.IsVisible = false;
			return;
		}
		this.thumb.IsVisible = true;
		float num2 = (this.orientation == dfControlOrientation.Horizontal) ? this.track.Width : this.track.Height;
		float num3 = (this.orientation == dfControlOrientation.Horizontal) ? Mathf.Max(this.scrollSize / num * num2, this.thumb.MinimumSize.x) : Mathf.Max(this.scrollSize / num * num2, this.thumb.MinimumSize.y);
		Vector2 size = (this.orientation == dfControlOrientation.Horizontal) ? new Vector2(num3, this.thumb.Height) : new Vector2(this.thumb.Width, num3);
		if (this.Orientation == dfControlOrientation.Horizontal)
		{
			size.x -= (float)this.thumbPadding.horizontal;
		}
		else
		{
			size.y -= (float)this.thumbPadding.vertical;
		}
		this.thumb.Size = size;
		float d = (rawValue - this.minValue) / (num - this.scrollSize) * (num2 - num3);
		Vector3 a = (this.orientation == dfControlOrientation.Horizontal) ? Vector3.right : Vector3.up;
		Vector3 b = (this.Orientation == dfControlOrientation.Horizontal) ? new Vector3(0f, (this.track.Height - this.thumb.Height) * 0.5f) : new Vector3((this.track.Width - this.thumb.Width) * 0.5f, 0f);
		if (this.Orientation == dfControlOrientation.Horizontal)
		{
			b.x = (float)this.thumbPadding.left;
		}
		else
		{
			b.y = (float)this.thumbPadding.top;
		}
		if (this.thumb.Parent == this)
		{
			this.thumb.RelativePosition = this.track.RelativePosition + b + a * d;
			return;
		}
		this.thumb.RelativePosition = a * d + b;
	}

	// Token: 0x060003B1 RID: 945 RVA: 0x00012334 File Offset: 0x00010534
	private float getValueFromMouseEvent(dfMouseEventArgs args)
	{
		Vector3[] corners = this.track.GetCorners();
		Vector3 vector = corners[0];
		Vector3 vector2 = corners[(this.orientation == dfControlOrientation.Horizontal) ? 1 : 2];
		Plane plane = new Plane(base.transform.TransformDirection(Vector3.back), vector);
		Ray ray = args.Ray;
		float d = 0f;
		if (!plane.Raycast(ray, out d))
		{
			return this.rawValue;
		}
		Vector3 vector3 = ray.origin + ray.direction * d;
		if (args.Source == this.thumb)
		{
			vector3 += this.thumbMouseOffset;
		}
		float num = (dfScrollbar.closestPoint(vector, vector2, vector3, true) - vector).magnitude / (vector2 - vector).magnitude;
		return this.minValue + (this.maxValue - this.minValue) * num;
	}

	// Token: 0x060003B2 RID: 946 RVA: 0x00012420 File Offset: 0x00010620
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

	// Token: 0x04000136 RID: 310
	[SerializeField]
	protected dfAtlas atlas;

	// Token: 0x04000137 RID: 311
	[SerializeField]
	protected dfControlOrientation orientation;

	// Token: 0x04000138 RID: 312
	[SerializeField]
	protected float rawValue = 1f;

	// Token: 0x04000139 RID: 313
	[SerializeField]
	protected float minValue;

	// Token: 0x0400013A RID: 314
	[SerializeField]
	protected float maxValue = 100f;

	// Token: 0x0400013B RID: 315
	[SerializeField]
	protected float stepSize = 1f;

	// Token: 0x0400013C RID: 316
	[SerializeField]
	protected float scrollSize = 1f;

	// Token: 0x0400013D RID: 317
	[SerializeField]
	protected float increment = 1f;

	// Token: 0x0400013E RID: 318
	[SerializeField]
	protected dfControl thumb;

	// Token: 0x0400013F RID: 319
	[SerializeField]
	protected dfControl track;

	// Token: 0x04000140 RID: 320
	[SerializeField]
	protected dfControl incButton;

	// Token: 0x04000141 RID: 321
	[SerializeField]
	protected dfControl decButton;

	// Token: 0x04000142 RID: 322
	[SerializeField]
	protected RectOffset thumbPadding = new RectOffset();

	// Token: 0x04000143 RID: 323
	[SerializeField]
	protected bool autoHide;

	// Token: 0x04000144 RID: 324
	private Vector3 thumbMouseOffset = Vector3.zero;
}

using System;
using UnityEngine;

// Token: 0x02000021 RID: 33
[AddComponentMenu("Daikon Forge/Input/Gestures/Double Tap")]
public class dfDoubleTapGesture : dfGestureBase
{
	// Token: 0x1400003D RID: 61
	// (add) Token: 0x06000500 RID: 1280 RVA: 0x000194E0 File Offset: 0x000176E0
	// (remove) Token: 0x06000501 RID: 1281 RVA: 0x00019518 File Offset: 0x00017718
	public event dfGestureEventHandler<dfDoubleTapGesture> DoubleTapGesture;

	// Token: 0x17000127 RID: 295
	// (get) Token: 0x06000502 RID: 1282 RVA: 0x0001954D File Offset: 0x0001774D
	// (set) Token: 0x06000503 RID: 1283 RVA: 0x00019555 File Offset: 0x00017755
	public float Timeout
	{
		get
		{
			return this.timeout;
		}
		set
		{
			this.timeout = value;
		}
	}

	// Token: 0x17000128 RID: 296
	// (get) Token: 0x06000504 RID: 1284 RVA: 0x0001955E File Offset: 0x0001775E
	// (set) Token: 0x06000505 RID: 1285 RVA: 0x00019566 File Offset: 0x00017766
	public float MaximumDistance
	{
		get
		{
			return this.maxDistance;
		}
		set
		{
			this.maxDistance = value;
		}
	}

	// Token: 0x06000506 RID: 1286 RVA: 0x0001956F File Offset: 0x0001776F
	protected void Start()
	{
	}

	// Token: 0x06000507 RID: 1287 RVA: 0x00019574 File Offset: 0x00017774
	public void OnMouseDown(dfControl source, dfMouseEventArgs args)
	{
		if (base.State == dfGestureState.Possible && Time.realtimeSinceStartup - base.StartTime <= this.timeout && Vector2.Distance(args.Position, base.StartPosition) <= this.maxDistance)
		{
			base.StartPosition = (base.CurrentPosition = args.Position);
			base.State = dfGestureState.Began;
			if (this.DoubleTapGesture != null)
			{
				this.DoubleTapGesture(this);
			}
			base.gameObject.Signal("OnDoubleTapGesture", new object[]
			{
				this
			});
			this.endGesture();
			return;
		}
		base.StartPosition = (base.CurrentPosition = args.Position);
		base.State = dfGestureState.Possible;
		base.StartTime = Time.realtimeSinceStartup;
	}

	// Token: 0x06000508 RID: 1288 RVA: 0x00019630 File Offset: 0x00017830
	public void OnMouseLeave()
	{
		this.endGesture();
	}

	// Token: 0x06000509 RID: 1289 RVA: 0x00019638 File Offset: 0x00017838
	public void OnMultiTouchEnd()
	{
		this.endGesture();
	}

	// Token: 0x0600050A RID: 1290 RVA: 0x00019640 File Offset: 0x00017840
	public void OnMultiTouch()
	{
		this.endGesture();
	}

	// Token: 0x0600050B RID: 1291 RVA: 0x00019648 File Offset: 0x00017848
	private void endGesture()
	{
		if (base.State == dfGestureState.Began || base.State == dfGestureState.Changed)
		{
			base.State = dfGestureState.Ended;
			return;
		}
		if (base.State == dfGestureState.Possible)
		{
			base.State = dfGestureState.Cancelled;
			return;
		}
		base.State = dfGestureState.None;
	}

	// Token: 0x040001B4 RID: 436
	[SerializeField]
	private float timeout = 0.5f;

	// Token: 0x040001B5 RID: 437
	[SerializeField]
	private float maxDistance = 35f;
}

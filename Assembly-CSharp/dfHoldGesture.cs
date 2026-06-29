using System;
using UnityEngine;

// Token: 0x02000026 RID: 38
[AddComponentMenu("Daikon Forge/Input/Gestures/Hold")]
public class dfHoldGesture : dfGestureBase
{
	// Token: 0x1400003F RID: 63
	// (add) Token: 0x0600052C RID: 1324 RVA: 0x0001994C File Offset: 0x00017B4C
	// (remove) Token: 0x0600052D RID: 1325 RVA: 0x00019984 File Offset: 0x00017B84
	public event dfGestureEventHandler<dfHoldGesture> HoldGestureStart;

	// Token: 0x14000040 RID: 64
	// (add) Token: 0x0600052E RID: 1326 RVA: 0x000199BC File Offset: 0x00017BBC
	// (remove) Token: 0x0600052F RID: 1327 RVA: 0x000199F4 File Offset: 0x00017BF4
	public event dfGestureEventHandler<dfHoldGesture> HoldGestureEnd;

	// Token: 0x17000131 RID: 305
	// (get) Token: 0x06000530 RID: 1328 RVA: 0x00019A29 File Offset: 0x00017C29
	// (set) Token: 0x06000531 RID: 1329 RVA: 0x00019A31 File Offset: 0x00017C31
	public float MinimumTime
	{
		get
		{
			return this.minTime;
		}
		set
		{
			this.minTime = value;
		}
	}

	// Token: 0x17000132 RID: 306
	// (get) Token: 0x06000532 RID: 1330 RVA: 0x00019A3A File Offset: 0x00017C3A
	// (set) Token: 0x06000533 RID: 1331 RVA: 0x00019A42 File Offset: 0x00017C42
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

	// Token: 0x17000133 RID: 307
	// (get) Token: 0x06000534 RID: 1332 RVA: 0x00019A4B File Offset: 0x00017C4B
	public float HoldLength
	{
		get
		{
			if (base.State == dfGestureState.Began)
			{
				return Time.realtimeSinceStartup - base.StartTime;
			}
			return 0f;
		}
	}

	// Token: 0x06000535 RID: 1333 RVA: 0x00019A68 File Offset: 0x00017C68
	protected void Start()
	{
	}

	// Token: 0x06000536 RID: 1334 RVA: 0x00019A6C File Offset: 0x00017C6C
	protected void Update()
	{
		if (base.State == dfGestureState.Possible && Time.realtimeSinceStartup - base.StartTime >= this.minTime)
		{
			base.State = dfGestureState.Began;
			if (this.HoldGestureStart != null)
			{
				this.HoldGestureStart(this);
			}
			base.gameObject.Signal("OnHoldGestureStart", new object[]
			{
				this
			});
		}
	}

	// Token: 0x06000537 RID: 1335 RVA: 0x00019ACC File Offset: 0x00017CCC
	public void OnMouseDown(dfControl source, dfMouseEventArgs args)
	{
		base.State = dfGestureState.Possible;
		base.StartPosition = (base.CurrentPosition = args.Position);
		base.StartTime = Time.realtimeSinceStartup;
	}

	// Token: 0x06000538 RID: 1336 RVA: 0x00019B00 File Offset: 0x00017D00
	public void OnMouseMove(dfControl source, dfMouseEventArgs args)
	{
		if (base.State != dfGestureState.Possible && base.State != dfGestureState.Began)
		{
			return;
		}
		base.CurrentPosition = args.Position;
		if (Vector2.Distance(args.Position, base.StartPosition) > this.maxDistance)
		{
			if (base.State == dfGestureState.Possible)
			{
				base.State = dfGestureState.Failed;
				return;
			}
			if (base.State == dfGestureState.Began)
			{
				base.State = dfGestureState.Cancelled;
				if (this.HoldGestureEnd != null)
				{
					this.HoldGestureEnd(this);
				}
				base.gameObject.Signal("OnHoldGestureEnd", new object[]
				{
					this
				});
			}
		}
	}

	// Token: 0x06000539 RID: 1337 RVA: 0x00019B98 File Offset: 0x00017D98
	public void OnMouseUp(dfControl source, dfMouseEventArgs args)
	{
		if (base.State == dfGestureState.Began)
		{
			base.CurrentPosition = args.Position;
			base.State = dfGestureState.Ended;
			if (this.HoldGestureEnd != null)
			{
				this.HoldGestureEnd(this);
			}
			base.gameObject.Signal("OnHoldGestureEnd", new object[]
			{
				this
			});
		}
		base.State = dfGestureState.None;
	}

	// Token: 0x0600053A RID: 1338 RVA: 0x00019BF8 File Offset: 0x00017DF8
	public void OnMultiTouch(dfControl source, dfTouchEventArgs args)
	{
		if (base.State == dfGestureState.Began)
		{
			base.State = dfGestureState.Cancelled;
			if (this.HoldGestureEnd != null)
			{
				this.HoldGestureEnd(this);
			}
			base.gameObject.Signal("OnHoldGestureEnd", new object[]
			{
				this
			});
			return;
		}
		base.State = dfGestureState.Failed;
	}

	// Token: 0x040001CA RID: 458
	[SerializeField]
	private float minTime = 0.75f;

	// Token: 0x040001CB RID: 459
	[SerializeField]
	private float maxDistance = 25f;
}

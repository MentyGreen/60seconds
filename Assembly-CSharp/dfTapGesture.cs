using System;
using UnityEngine;

// Token: 0x0200002A RID: 42
[AddComponentMenu("Daikon Forge/Input/Gestures/Tap")]
public class dfTapGesture : dfGestureBase
{
	// Token: 0x1400004A RID: 74
	// (add) Token: 0x0600056F RID: 1391 RVA: 0x0001AA94 File Offset: 0x00018C94
	// (remove) Token: 0x06000570 RID: 1392 RVA: 0x0001AACC File Offset: 0x00018CCC
	public event dfGestureEventHandler<dfTapGesture> TapGesture;

	// Token: 0x17000138 RID: 312
	// (get) Token: 0x06000571 RID: 1393 RVA: 0x0001AB01 File Offset: 0x00018D01
	// (set) Token: 0x06000572 RID: 1394 RVA: 0x0001AB09 File Offset: 0x00018D09
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

	// Token: 0x17000139 RID: 313
	// (get) Token: 0x06000573 RID: 1395 RVA: 0x0001AB12 File Offset: 0x00018D12
	// (set) Token: 0x06000574 RID: 1396 RVA: 0x0001AB1A File Offset: 0x00018D1A
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

	// Token: 0x06000575 RID: 1397 RVA: 0x0001AB23 File Offset: 0x00018D23
	protected void Start()
	{
	}

	// Token: 0x06000576 RID: 1398 RVA: 0x0001AB28 File Offset: 0x00018D28
	public void OnMouseDown(dfControl source, dfMouseEventArgs args)
	{
		base.StartPosition = (base.CurrentPosition = args.Position);
		base.State = dfGestureState.Possible;
		base.StartTime = Time.realtimeSinceStartup;
	}

	// Token: 0x06000577 RID: 1399 RVA: 0x0001AB5C File Offset: 0x00018D5C
	public void OnMouseMove(dfControl source, dfMouseEventArgs args)
	{
		if (base.State == dfGestureState.Possible || base.State == dfGestureState.Began)
		{
			base.CurrentPosition = args.Position;
			if (Vector2.Distance(args.Position, base.StartPosition) > this.maxDistance)
			{
				base.State = dfGestureState.Failed;
			}
		}
	}

	// Token: 0x06000578 RID: 1400 RVA: 0x0001AB9C File Offset: 0x00018D9C
	public void OnMouseUp(dfControl source, dfMouseEventArgs args)
	{
		if (base.State != dfGestureState.Possible)
		{
			base.State = dfGestureState.None;
			return;
		}
		if (Time.realtimeSinceStartup - base.StartTime <= this.timeout)
		{
			base.CurrentPosition = args.Position;
			base.State = dfGestureState.Ended;
			if (this.TapGesture != null)
			{
				this.TapGesture(this);
			}
			base.gameObject.Signal("OnTapGesture", new object[]
			{
				this
			});
			return;
		}
		base.State = dfGestureState.Failed;
	}

	// Token: 0x06000579 RID: 1401 RVA: 0x0001AC18 File Offset: 0x00018E18
	public void OnMultiTouch(dfControl source, dfTouchEventArgs args)
	{
		base.State = dfGestureState.Failed;
	}

	// Token: 0x040001DE RID: 478
	[SerializeField]
	private float timeout = 0.25f;

	// Token: 0x040001DF RID: 479
	[SerializeField]
	private float maxDistance = 25f;
}

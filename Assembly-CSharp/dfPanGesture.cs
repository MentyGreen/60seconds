using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000027 RID: 39
[AddComponentMenu("Daikon Forge/Input/Gestures/Pan")]
public class dfPanGesture : dfGestureBase
{
	// Token: 0x14000041 RID: 65
	// (add) Token: 0x0600053C RID: 1340 RVA: 0x00019C6C File Offset: 0x00017E6C
	// (remove) Token: 0x0600053D RID: 1341 RVA: 0x00019CA4 File Offset: 0x00017EA4
	public event dfGestureEventHandler<dfPanGesture> PanGestureStart;

	// Token: 0x14000042 RID: 66
	// (add) Token: 0x0600053E RID: 1342 RVA: 0x00019CDC File Offset: 0x00017EDC
	// (remove) Token: 0x0600053F RID: 1343 RVA: 0x00019D14 File Offset: 0x00017F14
	public event dfGestureEventHandler<dfPanGesture> PanGestureMove;

	// Token: 0x14000043 RID: 67
	// (add) Token: 0x06000540 RID: 1344 RVA: 0x00019D4C File Offset: 0x00017F4C
	// (remove) Token: 0x06000541 RID: 1345 RVA: 0x00019D84 File Offset: 0x00017F84
	public event dfGestureEventHandler<dfPanGesture> PanGestureEnd;

	// Token: 0x17000134 RID: 308
	// (get) Token: 0x06000542 RID: 1346 RVA: 0x00019DB9 File Offset: 0x00017FB9
	// (set) Token: 0x06000543 RID: 1347 RVA: 0x00019DC1 File Offset: 0x00017FC1
	public float MinimumDistance
	{
		get
		{
			return this.minDistance;
		}
		set
		{
			this.minDistance = value;
		}
	}

	// Token: 0x17000135 RID: 309
	// (get) Token: 0x06000544 RID: 1348 RVA: 0x00019DCA File Offset: 0x00017FCA
	// (set) Token: 0x06000545 RID: 1349 RVA: 0x00019DD2 File Offset: 0x00017FD2
	public Vector2 Delta { get; protected set; }

	// Token: 0x06000546 RID: 1350 RVA: 0x00019DDB File Offset: 0x00017FDB
	protected void Start()
	{
	}

	// Token: 0x06000547 RID: 1351 RVA: 0x00019DE0 File Offset: 0x00017FE0
	public void OnMouseDown(dfControl source, dfMouseEventArgs args)
	{
		base.StartPosition = (base.CurrentPosition = args.Position);
		base.State = dfGestureState.Possible;
		base.StartTime = Time.realtimeSinceStartup;
		this.Delta = Vector2.zero;
	}

	// Token: 0x06000548 RID: 1352 RVA: 0x00019E20 File Offset: 0x00018020
	public void OnMouseMove(dfControl source, dfMouseEventArgs args)
	{
		if (base.State == dfGestureState.Possible)
		{
			if (Vector2.Distance(args.Position, base.StartPosition) >= this.minDistance)
			{
				base.State = dfGestureState.Began;
				base.CurrentPosition = args.Position;
				this.Delta = args.Position - base.StartPosition;
				if (this.PanGestureStart != null)
				{
					this.PanGestureStart(this);
				}
				base.gameObject.Signal("OnPanGestureStart", new object[]
				{
					this
				});
				return;
			}
		}
		else if (base.State == dfGestureState.Began || base.State == dfGestureState.Changed)
		{
			base.State = dfGestureState.Changed;
			this.Delta = args.Position - base.CurrentPosition;
			base.CurrentPosition = args.Position;
			if (this.PanGestureMove != null)
			{
				this.PanGestureMove(this);
			}
			base.gameObject.Signal("OnPanGestureMove", new object[]
			{
				this
			});
		}
	}

	// Token: 0x06000549 RID: 1353 RVA: 0x00019F17 File Offset: 0x00018117
	public void OnMouseUp(dfControl source, dfMouseEventArgs args)
	{
		this.endPanGesture();
	}

	// Token: 0x0600054A RID: 1354 RVA: 0x00019F1F File Offset: 0x0001811F
	public void OnMultiTouchEnd()
	{
		this.endPanGesture();
		this.multiTouchMode = false;
	}

	// Token: 0x0600054B RID: 1355 RVA: 0x00019F30 File Offset: 0x00018130
	public void OnMultiTouch(dfControl source, dfTouchEventArgs args)
	{
		Vector2 center = this.getCenter(args.Touches);
		if (!this.multiTouchMode)
		{
			this.endPanGesture();
			this.multiTouchMode = true;
			base.State = dfGestureState.Possible;
			base.StartPosition = center;
			return;
		}
		if (base.State == dfGestureState.Possible)
		{
			if (Vector2.Distance(center, base.StartPosition) >= this.minDistance)
			{
				base.State = dfGestureState.Began;
				base.CurrentPosition = center;
				this.Delta = base.CurrentPosition - base.StartPosition;
				if (this.PanGestureStart != null)
				{
					this.PanGestureStart(this);
				}
				base.gameObject.Signal("OnPanGestureStart", new object[]
				{
					this
				});
				return;
			}
		}
		else if (base.State == dfGestureState.Began || base.State == dfGestureState.Changed)
		{
			base.State = dfGestureState.Changed;
			this.Delta = center - base.CurrentPosition;
			base.CurrentPosition = center;
			if (this.PanGestureMove != null)
			{
				this.PanGestureMove(this);
			}
			base.gameObject.Signal("OnPanGestureMove", new object[]
			{
				this
			});
		}
	}

	// Token: 0x0600054C RID: 1356 RVA: 0x0001A044 File Offset: 0x00018244
	private Vector2 getCenter(List<dfTouchInfo> list)
	{
		Vector2 a = Vector2.zero;
		for (int i = 0; i < list.Count; i++)
		{
			a += list[i].position;
		}
		return a / (float)list.Count;
	}

	// Token: 0x0600054D RID: 1357 RVA: 0x0001A08C File Offset: 0x0001828C
	private void endPanGesture()
	{
		this.Delta = Vector2.zero;
		base.StartPosition = Vector2.one * float.MinValue;
		if (base.State == dfGestureState.Began || base.State == dfGestureState.Changed)
		{
			base.State = dfGestureState.Ended;
			if (this.PanGestureEnd != null)
			{
				this.PanGestureEnd(this);
			}
			base.gameObject.Signal("OnPanGestureEnd", new object[]
			{
				this
			});
			return;
		}
		if (base.State == dfGestureState.Possible)
		{
			base.State = dfGestureState.Cancelled;
		}
	}

	// Token: 0x040001CF RID: 463
	[SerializeField]
	protected float minDistance = 25f;

	// Token: 0x040001D0 RID: 464
	private bool multiTouchMode;
}

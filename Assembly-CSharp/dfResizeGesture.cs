using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000028 RID: 40
[AddComponentMenu("Daikon Forge/Input/Gestures/Resize")]
public class dfResizeGesture : dfGestureBase
{
	// Token: 0x14000044 RID: 68
	// (add) Token: 0x0600054F RID: 1359 RVA: 0x0001A128 File Offset: 0x00018328
	// (remove) Token: 0x06000550 RID: 1360 RVA: 0x0001A160 File Offset: 0x00018360
	public event dfGestureEventHandler<dfResizeGesture> ResizeGestureStart;

	// Token: 0x14000045 RID: 69
	// (add) Token: 0x06000551 RID: 1361 RVA: 0x0001A198 File Offset: 0x00018398
	// (remove) Token: 0x06000552 RID: 1362 RVA: 0x0001A1D0 File Offset: 0x000183D0
	public event dfGestureEventHandler<dfResizeGesture> ResizeGestureUpdate;

	// Token: 0x14000046 RID: 70
	// (add) Token: 0x06000553 RID: 1363 RVA: 0x0001A208 File Offset: 0x00018408
	// (remove) Token: 0x06000554 RID: 1364 RVA: 0x0001A240 File Offset: 0x00018440
	public event dfGestureEventHandler<dfResizeGesture> ResizeGestureEnd;

	// Token: 0x17000136 RID: 310
	// (get) Token: 0x06000555 RID: 1365 RVA: 0x0001A275 File Offset: 0x00018475
	// (set) Token: 0x06000556 RID: 1366 RVA: 0x0001A27D File Offset: 0x0001847D
	public float SizeDelta { get; protected set; }

	// Token: 0x06000557 RID: 1367 RVA: 0x0001A286 File Offset: 0x00018486
	protected void Start()
	{
	}

	// Token: 0x06000558 RID: 1368 RVA: 0x0001A288 File Offset: 0x00018488
	public void OnMultiTouchEnd()
	{
		this.endGesture();
	}

	// Token: 0x06000559 RID: 1369 RVA: 0x0001A290 File Offset: 0x00018490
	public void OnMultiTouch(dfControl sender, dfTouchEventArgs args)
	{
		List<dfTouchInfo> touches = args.Touches;
		if (base.State == dfGestureState.None || base.State == dfGestureState.Cancelled || base.State == dfGestureState.Ended)
		{
			base.State = dfGestureState.Possible;
			return;
		}
		if (base.State == dfGestureState.Possible)
		{
			if (this.isResizeMovement(args.Touches))
			{
				base.State = dfGestureState.Began;
				base.StartPosition = (base.CurrentPosition = this.getCenter(touches));
				this.lastDistance = Vector2.Distance(touches[0].position, touches[1].position);
				this.SizeDelta = 0f;
				if (this.ResizeGestureStart != null)
				{
					this.ResizeGestureStart(this);
				}
				base.gameObject.Signal("OnResizeGestureStart", new object[]
				{
					this
				});
				return;
			}
		}
		else if ((base.State == dfGestureState.Began || base.State == dfGestureState.Changed) && this.isResizeMovement(touches))
		{
			base.State = dfGestureState.Changed;
			base.CurrentPosition = this.getCenter(touches);
			float num = Vector2.Distance(touches[0].position, touches[1].position);
			this.SizeDelta = num - this.lastDistance;
			this.lastDistance = num;
			if (this.ResizeGestureUpdate != null)
			{
				this.ResizeGestureUpdate(this);
			}
			base.gameObject.Signal("OnResizeGestureUpdate", new object[]
			{
				this
			});
		}
	}

	// Token: 0x0600055A RID: 1370 RVA: 0x0001A400 File Offset: 0x00018600
	private Vector2 getCenter(List<dfTouchInfo> list)
	{
		Vector2 a = Vector2.zero;
		for (int i = 0; i < list.Count; i++)
		{
			a += list[i].position;
		}
		return a / (float)list.Count;
	}

	// Token: 0x0600055B RID: 1371 RVA: 0x0001A448 File Offset: 0x00018648
	private bool isResizeMovement(List<dfTouchInfo> list)
	{
		if (list.Count < 2)
		{
			return false;
		}
		dfTouchInfo dfTouchInfo = list[0];
		Vector2 normalized = (dfTouchInfo.deltaPosition * (Time.deltaTime / dfTouchInfo.deltaTime)).normalized;
		dfTouchInfo dfTouchInfo2 = list[1];
		Vector2 normalized2 = (dfTouchInfo2.deltaPosition * (Time.deltaTime / dfTouchInfo2.deltaTime)).normalized;
		float f = Vector2.Dot(normalized, (dfTouchInfo.position - dfTouchInfo2.position).normalized);
		float f2 = Vector2.Dot(normalized2, (dfTouchInfo2.position - dfTouchInfo.position).normalized);
		return Mathf.Abs(f) >= 0.21460181f || Mathf.Abs(f2) >= 0.21460181f;
	}

	// Token: 0x0600055C RID: 1372 RVA: 0x0001A518 File Offset: 0x00018718
	private void endGesture()
	{
		if (base.State == dfGestureState.Began || base.State == dfGestureState.Changed)
		{
			if (base.State == dfGestureState.Began)
			{
				base.State = dfGestureState.Cancelled;
			}
			else
			{
				base.State = dfGestureState.Ended;
			}
			this.lastDistance = (this.SizeDelta = 0f);
			if (this.ResizeGestureEnd != null)
			{
				this.ResizeGestureEnd(this);
			}
			base.gameObject.Signal("OnResizeGestureEnd", new object[]
			{
				this
			});
			return;
		}
		base.State = dfGestureState.None;
	}

	// Token: 0x040001D6 RID: 470
	private float lastDistance;
}

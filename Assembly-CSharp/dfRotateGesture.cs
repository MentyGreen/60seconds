using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000029 RID: 41
[AddComponentMenu("Daikon Forge/Input/Gestures/Rotate")]
public class dfRotateGesture : dfGestureBase
{
	// Token: 0x14000047 RID: 71
	// (add) Token: 0x0600055E RID: 1374 RVA: 0x0001A5A4 File Offset: 0x000187A4
	// (remove) Token: 0x0600055F RID: 1375 RVA: 0x0001A5DC File Offset: 0x000187DC
	public event dfGestureEventHandler<dfRotateGesture> RotateGestureStart;

	// Token: 0x14000048 RID: 72
	// (add) Token: 0x06000560 RID: 1376 RVA: 0x0001A614 File Offset: 0x00018814
	// (remove) Token: 0x06000561 RID: 1377 RVA: 0x0001A64C File Offset: 0x0001884C
	public event dfGestureEventHandler<dfRotateGesture> RotateGestureUpdate;

	// Token: 0x14000049 RID: 73
	// (add) Token: 0x06000562 RID: 1378 RVA: 0x0001A684 File Offset: 0x00018884
	// (remove) Token: 0x06000563 RID: 1379 RVA: 0x0001A6BC File Offset: 0x000188BC
	public event dfGestureEventHandler<dfRotateGesture> RotateGestureEnd;

	// Token: 0x17000137 RID: 311
	// (get) Token: 0x06000564 RID: 1380 RVA: 0x0001A6F1 File Offset: 0x000188F1
	// (set) Token: 0x06000565 RID: 1381 RVA: 0x0001A6F9 File Offset: 0x000188F9
	public float AngleDelta { get; protected set; }

	// Token: 0x06000566 RID: 1382 RVA: 0x0001A702 File Offset: 0x00018902
	protected void Start()
	{
	}

	// Token: 0x06000567 RID: 1383 RVA: 0x0001A704 File Offset: 0x00018904
	public void OnMultiTouchEnd()
	{
		this.endGesture();
	}

	// Token: 0x06000568 RID: 1384 RVA: 0x0001A70C File Offset: 0x0001890C
	public void OnMultiTouch(dfControl sender, dfTouchEventArgs args)
	{
		List<dfTouchInfo> touches = args.Touches;
		if (base.State == dfGestureState.None || base.State == dfGestureState.Cancelled || base.State == dfGestureState.Ended)
		{
			base.State = dfGestureState.Possible;
			this.accumulatedDelta = 0f;
			return;
		}
		if (base.State == dfGestureState.Possible)
		{
			if (this.isRotateMovement(args.Touches))
			{
				float num = this.getAngleDelta(touches) + this.accumulatedDelta;
				if (Mathf.Abs(num) < this.thresholdAngle)
				{
					this.accumulatedDelta = num;
					return;
				}
				base.State = dfGestureState.Began;
				base.StartPosition = (base.CurrentPosition = this.getCenter(touches));
				this.AngleDelta = num;
				if (this.RotateGestureStart != null)
				{
					this.RotateGestureStart(this);
				}
				base.gameObject.Signal("OnRotateGestureStart", new object[]
				{
					this
				});
				return;
			}
		}
		else if (base.State == dfGestureState.Began || base.State == dfGestureState.Changed)
		{
			float angleDelta = this.getAngleDelta(touches);
			if (Mathf.Abs(angleDelta) <= 1E-45f || Mathf.Abs(angleDelta) > 22.5f)
			{
				return;
			}
			base.State = dfGestureState.Changed;
			this.AngleDelta = angleDelta;
			base.CurrentPosition = this.getCenter(touches);
			if (this.RotateGestureUpdate != null)
			{
				this.RotateGestureUpdate(this);
			}
			base.gameObject.Signal("OnRotateGestureUpdate", new object[]
			{
				this
			});
		}
	}

	// Token: 0x06000569 RID: 1385 RVA: 0x0001A864 File Offset: 0x00018A64
	private float getAngleDelta(List<dfTouchInfo> touches)
	{
		if (touches.Count < 2)
		{
			return 0f;
		}
		dfTouchInfo dfTouchInfo = touches[0];
		dfTouchInfo dfTouchInfo2 = touches[1];
		if (Vector2.Distance(dfTouchInfo.deltaPosition, dfTouchInfo2.deltaPosition) <= 1E-45f)
		{
			return 0f;
		}
		Vector2 b = dfTouchInfo.deltaPosition * (Time.deltaTime / dfTouchInfo.deltaTime);
		Vector2 b2 = dfTouchInfo2.deltaPosition * (Time.deltaTime / dfTouchInfo2.deltaTime);
		Vector2 vector = dfTouchInfo.position - b - (dfTouchInfo2.position - b2);
		Vector2 vector2 = dfTouchInfo.position - dfTouchInfo2.position;
		float num = this.deltaAngle(vector.normalized, vector2.normalized);
		if (float.IsNaN(num))
		{
			return 0f;
		}
		if (dfTouchInfo.phase == TouchPhase.Stationary || dfTouchInfo2.phase == TouchPhase.Stationary)
		{
			num *= 0.5f;
		}
		return num;
	}

	// Token: 0x0600056A RID: 1386 RVA: 0x0001A960 File Offset: 0x00018B60
	private float deltaAngle(Vector2 start, Vector2 end)
	{
		float y = start.x * end.y - start.y * end.x;
		return 57.29578f * Mathf.Atan2(y, Vector2.Dot(start, end));
	}

	// Token: 0x0600056B RID: 1387 RVA: 0x0001A99C File Offset: 0x00018B9C
	private Vector2 getCenter(List<dfTouchInfo> list)
	{
		Vector2 a = Vector2.zero;
		for (int i = 0; i < list.Count; i++)
		{
			a += list[i].position;
		}
		return a / (float)list.Count;
	}

	// Token: 0x0600056C RID: 1388 RVA: 0x0001A9E3 File Offset: 0x00018BE3
	private bool isRotateMovement(List<dfTouchInfo> list)
	{
		return Mathf.Abs(this.getAngleDelta(list)) >= 0.1f;
	}

	// Token: 0x0600056D RID: 1389 RVA: 0x0001A9FC File Offset: 0x00018BFC
	private void endGesture()
	{
		this.AngleDelta = 0f;
		this.accumulatedDelta = 0f;
		if (base.State == dfGestureState.Began || base.State == dfGestureState.Changed)
		{
			base.State = dfGestureState.Ended;
			if (this.RotateGestureEnd != null)
			{
				this.RotateGestureEnd(this);
			}
			base.gameObject.Signal("OnRotateGestureEnd", new object[]
			{
				this
			});
			return;
		}
		if (base.State == dfGestureState.Possible)
		{
			base.State = dfGestureState.Cancelled;
			return;
		}
		base.State = dfGestureState.None;
	}

	// Token: 0x040001DA RID: 474
	[SerializeField]
	protected float thresholdAngle = 10f;

	// Token: 0x040001DC RID: 476
	private float accumulatedDelta;
}

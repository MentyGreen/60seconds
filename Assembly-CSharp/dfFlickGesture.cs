using System;
using UnityEngine;

// Token: 0x02000022 RID: 34
[AddComponentMenu("Daikon Forge/Input/Gestures/Flick")]
public class dfFlickGesture : dfGestureBase
{
	// Token: 0x1400003E RID: 62
	// (add) Token: 0x0600050D RID: 1293 RVA: 0x0001969C File Offset: 0x0001789C
	// (remove) Token: 0x0600050E RID: 1294 RVA: 0x000196D4 File Offset: 0x000178D4
	public event dfGestureEventHandler<dfFlickGesture> FlickGesture;

	// Token: 0x17000129 RID: 297
	// (get) Token: 0x0600050F RID: 1295 RVA: 0x00019709 File Offset: 0x00017909
	// (set) Token: 0x06000510 RID: 1296 RVA: 0x00019711 File Offset: 0x00017911
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

	// Token: 0x1700012A RID: 298
	// (get) Token: 0x06000511 RID: 1297 RVA: 0x0001971A File Offset: 0x0001791A
	// (set) Token: 0x06000512 RID: 1298 RVA: 0x00019722 File Offset: 0x00017922
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

	// Token: 0x1700012B RID: 299
	// (get) Token: 0x06000513 RID: 1299 RVA: 0x0001972B File Offset: 0x0001792B
	// (set) Token: 0x06000514 RID: 1300 RVA: 0x00019733 File Offset: 0x00017933
	public float DeltaTime { get; protected set; }

	// Token: 0x06000515 RID: 1301 RVA: 0x0001973C File Offset: 0x0001793C
	protected void Start()
	{
	}

	// Token: 0x06000516 RID: 1302 RVA: 0x00019740 File Offset: 0x00017940
	public void OnMouseDown(dfControl source, dfMouseEventArgs args)
	{
		base.StartPosition = (base.CurrentPosition = args.Position);
		base.State = dfGestureState.Possible;
		base.StartTime = Time.realtimeSinceStartup;
		this.hoverTime = Time.realtimeSinceStartup;
	}

	// Token: 0x06000517 RID: 1303 RVA: 0x00019780 File Offset: 0x00017980
	public void OnMouseHover(dfControl source, dfMouseEventArgs args)
	{
		if (base.State == dfGestureState.Possible && Time.realtimeSinceStartup - this.hoverTime >= this.timeout)
		{
			base.StartPosition = (base.CurrentPosition = args.Position);
			base.StartTime = Time.realtimeSinceStartup;
		}
	}

	// Token: 0x06000518 RID: 1304 RVA: 0x000197CA File Offset: 0x000179CA
	public void OnMouseMove(dfControl source, dfMouseEventArgs args)
	{
		this.hoverTime = Time.realtimeSinceStartup;
		if (base.State == dfGestureState.Possible || base.State == dfGestureState.Began)
		{
			base.State = dfGestureState.Began;
			base.CurrentPosition = args.Position;
		}
	}

	// Token: 0x06000519 RID: 1305 RVA: 0x000197FC File Offset: 0x000179FC
	public void OnMouseUp(dfControl source, dfMouseEventArgs args)
	{
		if (base.State == dfGestureState.Began)
		{
			base.CurrentPosition = args.Position;
			if (Time.realtimeSinceStartup - base.StartTime <= this.timeout)
			{
				if (Vector2.Distance(base.CurrentPosition, base.StartPosition) >= this.minDistance)
				{
					base.State = dfGestureState.Ended;
					this.DeltaTime = Time.realtimeSinceStartup - base.StartTime;
					if (this.FlickGesture != null)
					{
						this.FlickGesture(this);
					}
					base.gameObject.Signal("OnFlickGesture", new object[]
					{
						this
					});
					return;
				}
				base.State = dfGestureState.Failed;
				return;
			}
			else
			{
				base.State = dfGestureState.Failed;
			}
		}
	}

	// Token: 0x0600051A RID: 1306 RVA: 0x000198A6 File Offset: 0x00017AA6
	public void OnMultiTouchEnd()
	{
		this.endGesture();
	}

	// Token: 0x0600051B RID: 1307 RVA: 0x000198AE File Offset: 0x00017AAE
	public void OnMultiTouch()
	{
		this.endGesture();
	}

	// Token: 0x0600051C RID: 1308 RVA: 0x000198B6 File Offset: 0x00017AB6
	private void endGesture()
	{
		base.State = dfGestureState.None;
	}

	// Token: 0x040001B7 RID: 439
	[SerializeField]
	private float timeout = 0.25f;

	// Token: 0x040001B8 RID: 440
	[SerializeField]
	private float minDistance = 25f;

	// Token: 0x040001BA RID: 442
	private float hoverTime;
}

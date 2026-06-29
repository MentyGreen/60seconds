using System;
using UnityEngine;

// Token: 0x02000033 RID: 51
internal class dfTouchTrackingInfo
{
	// Token: 0x1700015A RID: 346
	// (get) Token: 0x060005CF RID: 1487 RVA: 0x0001BB5E File Offset: 0x00019D5E
	// (set) Token: 0x060005D0 RID: 1488 RVA: 0x0001BB66 File Offset: 0x00019D66
	public int FingerID { get; set; }

	// Token: 0x1700015B RID: 347
	// (get) Token: 0x060005D1 RID: 1489 RVA: 0x0001BB6F File Offset: 0x00019D6F
	// (set) Token: 0x060005D2 RID: 1490 RVA: 0x0001BB77 File Offset: 0x00019D77
	public TouchPhase Phase
	{
		get
		{
			return this.phase;
		}
		set
		{
			this.IsActive = true;
			this.phase = value;
			if (value == TouchPhase.Stationary)
			{
				this.deltaTime = float.Epsilon;
				this.deltaPosition = Vector2.zero;
				this.lastUpdateTime = Time.realtimeSinceStartup;
			}
		}
	}

	// Token: 0x1700015C RID: 348
	// (get) Token: 0x060005D3 RID: 1491 RVA: 0x0001BBAC File Offset: 0x00019DAC
	// (set) Token: 0x060005D4 RID: 1492 RVA: 0x0001BBB4 File Offset: 0x00019DB4
	public Vector2 Position
	{
		get
		{
			return this.position;
		}
		set
		{
			this.IsActive = true;
			if (this.Phase == TouchPhase.Began)
			{
				this.deltaPosition = Vector2.zero;
			}
			else
			{
				this.deltaPosition = value - this.position;
			}
			this.position = value;
			float realtimeSinceStartup = Time.realtimeSinceStartup;
			this.deltaTime = realtimeSinceStartup - this.lastUpdateTime;
			this.lastUpdateTime = realtimeSinceStartup;
		}
	}

	// Token: 0x060005D5 RID: 1493 RVA: 0x0001BC11 File Offset: 0x00019E11
	public static implicit operator dfTouchInfo(dfTouchTrackingInfo info)
	{
		return new dfTouchInfo(info.FingerID, info.phase, (info.phase == TouchPhase.Began) ? 1 : 0, info.position, info.deltaPosition, info.deltaTime);
	}

	// Token: 0x04000207 RID: 519
	private TouchPhase phase;

	// Token: 0x04000208 RID: 520
	private Vector2 position = Vector2.one * float.MinValue;

	// Token: 0x04000209 RID: 521
	private Vector2 deltaPosition = Vector2.zero;

	// Token: 0x0400020A RID: 522
	private float deltaTime;

	// Token: 0x0400020B RID: 523
	private float lastUpdateTime = Time.realtimeSinceStartup;

	// Token: 0x0400020C RID: 524
	public bool IsActive;
}

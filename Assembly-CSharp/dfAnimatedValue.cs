using System;
using UnityEngine;

// Token: 0x020000C5 RID: 197
public abstract class dfAnimatedValue<T> where T : struct
{
	// Token: 0x06000A84 RID: 2692 RVA: 0x0002DB4C File Offset: 0x0002BD4C
	protected internal dfAnimatedValue(T StartValue, T EndValue, float Time) : this()
	{
		this.startValue = StartValue;
		this.endValue = EndValue;
		this.animLength = Time;
	}

	// Token: 0x06000A85 RID: 2693 RVA: 0x0002DB69 File Offset: 0x0002BD69
	protected internal dfAnimatedValue()
	{
		this.startTime = Time.realtimeSinceStartup;
		this.easingFunction = dfEasingFunctions.GetFunction(this.easingType);
	}

	// Token: 0x17000230 RID: 560
	// (get) Token: 0x06000A86 RID: 2694 RVA: 0x0002DB98 File Offset: 0x0002BD98
	public bool IsDone
	{
		get
		{
			return this.isDone;
		}
	}

	// Token: 0x17000231 RID: 561
	// (get) Token: 0x06000A87 RID: 2695 RVA: 0x0002DBA0 File Offset: 0x0002BDA0
	// (set) Token: 0x06000A88 RID: 2696 RVA: 0x0002DBA8 File Offset: 0x0002BDA8
	public float Length
	{
		get
		{
			return this.animLength;
		}
		set
		{
			this.animLength = value;
			this.startTime = Time.realtimeSinceStartup;
			this.isDone = false;
		}
	}

	// Token: 0x17000232 RID: 562
	// (get) Token: 0x06000A89 RID: 2697 RVA: 0x0002DBC3 File Offset: 0x0002BDC3
	// (set) Token: 0x06000A8A RID: 2698 RVA: 0x0002DBCB File Offset: 0x0002BDCB
	public T StartValue
	{
		get
		{
			return this.startValue;
		}
		set
		{
			this.startValue = value;
			this.startTime = Time.realtimeSinceStartup;
			this.isDone = false;
		}
	}

	// Token: 0x17000233 RID: 563
	// (get) Token: 0x06000A8B RID: 2699 RVA: 0x0002DBE6 File Offset: 0x0002BDE6
	// (set) Token: 0x06000A8C RID: 2700 RVA: 0x0002DBEE File Offset: 0x0002BDEE
	public T EndValue
	{
		get
		{
			return this.endValue;
		}
		set
		{
			this.endValue = value;
			this.startTime = Time.realtimeSinceStartup;
			this.isDone = false;
		}
	}

	// Token: 0x17000234 RID: 564
	// (get) Token: 0x06000A8D RID: 2701 RVA: 0x0002DC0C File Offset: 0x0002BE0C
	public T Value
	{
		get
		{
			float num = Time.realtimeSinceStartup - this.startTime;
			if (num >= this.animLength)
			{
				this.isDone = true;
				return this.endValue;
			}
			float time = Mathf.Clamp01(num / this.animLength);
			time = this.easingFunction(0f, 1f, time);
			return this.Lerp(this.startValue, this.endValue, time);
		}
	}

	// Token: 0x17000235 RID: 565
	// (get) Token: 0x06000A8E RID: 2702 RVA: 0x0002DC75 File Offset: 0x0002BE75
	// (set) Token: 0x06000A8F RID: 2703 RVA: 0x0002DC7D File Offset: 0x0002BE7D
	public dfEasingType EasingType
	{
		get
		{
			return this.easingType;
		}
		set
		{
			this.easingType = value;
			this.easingFunction = dfEasingFunctions.GetFunction(this.easingType);
		}
	}

	// Token: 0x06000A90 RID: 2704
	protected abstract T Lerp(T start, T end, float time);

	// Token: 0x06000A91 RID: 2705 RVA: 0x0002DC97 File Offset: 0x0002BE97
	public static implicit operator T(dfAnimatedValue<T> animated)
	{
		return animated.Value;
	}

	// Token: 0x0400050A RID: 1290
	private T startValue;

	// Token: 0x0400050B RID: 1291
	private T endValue;

	// Token: 0x0400050C RID: 1292
	private float animLength = 1f;

	// Token: 0x0400050D RID: 1293
	private float startTime;

	// Token: 0x0400050E RID: 1294
	private bool isDone;

	// Token: 0x0400050F RID: 1295
	private dfEasingType easingType;

	// Token: 0x04000510 RID: 1296
	private dfEasingFunctions.EasingFunction easingFunction;
}

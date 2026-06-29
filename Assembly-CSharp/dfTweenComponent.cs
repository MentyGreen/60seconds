using System;
using System.Text;
using UnityEngine;

// Token: 0x020000CA RID: 202
[Serializable]
public abstract class dfTweenComponent<T> : dfTweenComponentBase where T : struct
{
	// Token: 0x14000057 RID: 87
	// (add) Token: 0x06000AB7 RID: 2743 RVA: 0x0002E67C File Offset: 0x0002C87C
	// (remove) Token: 0x06000AB8 RID: 2744 RVA: 0x0002E6B4 File Offset: 0x0002C8B4
	public event TweenNotification TweenStarted;

	// Token: 0x14000058 RID: 88
	// (add) Token: 0x06000AB9 RID: 2745 RVA: 0x0002E6EC File Offset: 0x0002C8EC
	// (remove) Token: 0x06000ABA RID: 2746 RVA: 0x0002E724 File Offset: 0x0002C924
	public event TweenNotification TweenStopped;

	// Token: 0x14000059 RID: 89
	// (add) Token: 0x06000ABB RID: 2747 RVA: 0x0002E75C File Offset: 0x0002C95C
	// (remove) Token: 0x06000ABC RID: 2748 RVA: 0x0002E794 File Offset: 0x0002C994
	public event TweenNotification TweenPaused;

	// Token: 0x1400005A RID: 90
	// (add) Token: 0x06000ABD RID: 2749 RVA: 0x0002E7CC File Offset: 0x0002C9CC
	// (remove) Token: 0x06000ABE RID: 2750 RVA: 0x0002E804 File Offset: 0x0002CA04
	public event TweenNotification TweenResumed;

	// Token: 0x1400005B RID: 91
	// (add) Token: 0x06000ABF RID: 2751 RVA: 0x0002E83C File Offset: 0x0002CA3C
	// (remove) Token: 0x06000AC0 RID: 2752 RVA: 0x0002E874 File Offset: 0x0002CA74
	public event TweenNotification TweenReset;

	// Token: 0x1400005C RID: 92
	// (add) Token: 0x06000AC1 RID: 2753 RVA: 0x0002E8AC File Offset: 0x0002CAAC
	// (remove) Token: 0x06000AC2 RID: 2754 RVA: 0x0002E8E4 File Offset: 0x0002CAE4
	public event TweenNotification TweenCompleted;

	// Token: 0x17000236 RID: 566
	// (get) Token: 0x06000AC3 RID: 2755 RVA: 0x0002E919 File Offset: 0x0002CB19
	// (set) Token: 0x06000AC4 RID: 2756 RVA: 0x0002E921 File Offset: 0x0002CB21
	public T StartValue
	{
		get
		{
			return this.startValue;
		}
		set
		{
			this.startValue = value;
			if (this.state != dfTweenState.Stopped)
			{
				this.Stop();
				this.Play();
			}
		}
	}

	// Token: 0x17000237 RID: 567
	// (get) Token: 0x06000AC5 RID: 2757 RVA: 0x0002E93E File Offset: 0x0002CB3E
	// (set) Token: 0x06000AC6 RID: 2758 RVA: 0x0002E946 File Offset: 0x0002CB46
	public T EndValue
	{
		get
		{
			return this.endValue;
		}
		set
		{
			this.endValue = value;
			if (this.state != dfTweenState.Stopped)
			{
				this.Stop();
				this.Play();
			}
		}
	}

	// Token: 0x17000238 RID: 568
	// (get) Token: 0x06000AC7 RID: 2759 RVA: 0x0002E963 File Offset: 0x0002CB63
	public dfTweenState State
	{
		get
		{
			return this.state;
		}
	}

	// Token: 0x06000AC8 RID: 2760 RVA: 0x0002E96B File Offset: 0x0002CB6B
	public static dfTweenComponent<T> Create(Component target, string propertyName, T startValue, T endValue, float length)
	{
		return dfTweenComponent<T>.Create(target, propertyName, startValue, endValue, length, dfEasingType.Linear);
	}

	// Token: 0x06000AC9 RID: 2761 RVA: 0x0002E97C File Offset: 0x0002CB7C
	public static dfTweenComponent<T> Create(Component target, string propertyName, T startValue, T endValue, float length, dfEasingType func)
	{
		if (target == null || target.gameObject == null)
		{
			throw new ArgumentNullException("target");
		}
		if (string.IsNullOrEmpty(propertyName))
		{
			throw new ArgumentNullException("propertyName");
		}
		dfTweenComponent<T> dfTweenComponent = (dfTweenComponent<T>)target.gameObject.AddComponent(typeof(T));
		dfTweenComponent.autoRun = false;
		dfTweenComponent.target = new dfComponentMemberInfo
		{
			Component = target,
			MemberName = propertyName
		};
		dfTweenComponent.startValue = startValue;
		dfTweenComponent.endValue = endValue;
		dfTweenComponent.length = length;
		dfTweenComponent.easingType = func;
		return dfTweenComponent;
	}

	// Token: 0x06000ACA RID: 2762 RVA: 0x0002EA18 File Offset: 0x0002CC18
	public override void Play()
	{
		if (this.state != dfTweenState.Stopped)
		{
			this.Stop();
		}
		if (!base.enabled || !base.gameObject.activeSelf || !base.gameObject.activeInHierarchy)
		{
			return;
		}
		if (this.target == null)
		{
			throw new NullReferenceException("Tween target is NULL");
		}
		if (!this.target.IsValid)
		{
			string str = "Invalid property binding configuration on ";
			string path = this.getPath(base.gameObject.transform);
			string str2 = " - ";
			dfComponentMemberInfo target = this.target;
			throw new InvalidOperationException(str + path + str2 + ((target != null) ? target.ToString() : null));
		}
		this.boundProperty = this.target.GetProperty();
		this.easingFunction = dfEasingFunctions.GetFunction(this.easingType);
		this.onStarted();
		this.actualStartValue = this.startValue;
		this.actualEndValue = this.endValue;
		if (this.syncStartWhenRun)
		{
			this.actualStartValue = (T)((object)this.boundProperty.Value);
		}
		else if (this.startValueIsOffset)
		{
			this.actualStartValue = this.offset(this.startValue, (T)((object)this.boundProperty.Value));
		}
		if (this.syncEndWhenRun)
		{
			this.actualEndValue = (T)((object)this.boundProperty.Value);
		}
		else if (this.endValueIsOffset)
		{
			this.actualEndValue = this.offset(this.endValue, (T)((object)this.boundProperty.Value));
		}
		this.boundProperty.Value = this.actualStartValue;
		this.startTime = Time.realtimeSinceStartup;
		this.state = dfTweenState.Started;
	}

	// Token: 0x06000ACB RID: 2763 RVA: 0x0002EBAC File Offset: 0x0002CDAC
	public override void Stop()
	{
		if (this.state == dfTweenState.Stopped)
		{
			return;
		}
		if (this.skipToEndOnStop)
		{
			this.boundProperty.Value = this.actualEndValue;
		}
		this.state = dfTweenState.Stopped;
		this.onStopped();
		this.easingFunction = null;
		this.boundProperty = null;
	}

	// Token: 0x06000ACC RID: 2764 RVA: 0x0002EBFB File Offset: 0x0002CDFB
	public override void Reset()
	{
		if (this.boundProperty != null)
		{
			this.boundProperty.Value = this.actualStartValue;
		}
		this.state = dfTweenState.Stopped;
		this.onReset();
		this.easingFunction = null;
		this.boundProperty = null;
	}

	// Token: 0x06000ACD RID: 2765 RVA: 0x0002EC36 File Offset: 0x0002CE36
	public void Pause()
	{
		base.IsPaused = true;
	}

	// Token: 0x06000ACE RID: 2766 RVA: 0x0002EC3F File Offset: 0x0002CE3F
	public void Resume()
	{
		base.IsPaused = false;
	}

	// Token: 0x06000ACF RID: 2767 RVA: 0x0002EC48 File Offset: 0x0002CE48
	public void Update()
	{
		if (this.state == dfTweenState.Stopped || this.state == dfTweenState.Paused)
		{
			return;
		}
		if (this.state == dfTweenState.Started)
		{
			if (this.startTime + base.StartDelay >= Time.realtimeSinceStartup)
			{
				return;
			}
			this.state = dfTweenState.Playing;
			this.startTime = Time.realtimeSinceStartup;
			this.pingPongDirection = 0f;
		}
		float num = Mathf.Min(Time.realtimeSinceStartup - this.startTime, this.length);
		if (num < this.length)
		{
			float time = this.easingFunction(0f, 1f, Mathf.Abs(this.pingPongDirection - num / this.length));
			if (this.animCurve != null)
			{
				time = this.animCurve.Evaluate(time);
			}
			this.boundProperty.Value = this.evaluate(this.actualStartValue, this.actualEndValue, time);
			return;
		}
		if (this.loopType == dfTweenLoopType.Once)
		{
			this.boundProperty.Value = this.actualEndValue;
			this.Stop();
			this.onCompleted();
			return;
		}
		if (this.loopType == dfTweenLoopType.Loop)
		{
			this.startTime = Time.realtimeSinceStartup;
			return;
		}
		if (this.loopType != dfTweenLoopType.PingPong)
		{
			throw new NotImplementedException();
		}
		this.startTime = Time.realtimeSinceStartup;
		if (this.pingPongDirection == 0f)
		{
			this.pingPongDirection = 1f;
			return;
		}
		this.pingPongDirection = 0f;
	}

	// Token: 0x06000AD0 RID: 2768
	public abstract T evaluate(T startValue, T endValue, float time);

	// Token: 0x06000AD1 RID: 2769
	public abstract T offset(T value, T offset);

	// Token: 0x06000AD2 RID: 2770 RVA: 0x0002EDA4 File Offset: 0x0002CFA4
	public override string ToString()
	{
		if (base.Target != null && base.Target.IsValid)
		{
			string name = this.target.Component.name;
			return string.Format("{0} ({1}.{2})", this.TweenName, name, this.target.MemberName);
		}
		return this.TweenName;
	}

	// Token: 0x06000AD3 RID: 2771 RVA: 0x0002EDFC File Offset: 0x0002CFFC
	private string getPath(Transform obj)
	{
		StringBuilder stringBuilder = new StringBuilder();
		while (obj != null)
		{
			if (stringBuilder.Length > 0)
			{
				stringBuilder.Insert(0, "\\");
				stringBuilder.Insert(0, obj.name);
			}
			else
			{
				stringBuilder.Append(obj.name);
			}
			obj = obj.parent;
		}
		return stringBuilder.ToString();
	}

	// Token: 0x06000AD4 RID: 2772 RVA: 0x0002EE5B File Offset: 0x0002D05B
	protected internal static float Lerp(float startValue, float endValue, float time)
	{
		return startValue + (endValue - startValue) * time;
	}

	// Token: 0x06000AD5 RID: 2773 RVA: 0x0002EE64 File Offset: 0x0002D064
	protected internal override void onPaused()
	{
		base.SendMessage("TweenPaused", this, SendMessageOptions.DontRequireReceiver);
		if (this.TweenPaused != null)
		{
			this.TweenPaused(this);
		}
	}

	// Token: 0x06000AD6 RID: 2774 RVA: 0x0002EE87 File Offset: 0x0002D087
	protected internal override void onResumed()
	{
		base.SendMessage("TweenResumed", this, SendMessageOptions.DontRequireReceiver);
		if (this.TweenResumed != null)
		{
			this.TweenResumed(this);
		}
	}

	// Token: 0x06000AD7 RID: 2775 RVA: 0x0002EEAA File Offset: 0x0002D0AA
	protected internal override void onStarted()
	{
		base.SendMessage("TweenStarted", this, SendMessageOptions.DontRequireReceiver);
		if (this.TweenStarted != null)
		{
			this.TweenStarted(this);
		}
	}

	// Token: 0x06000AD8 RID: 2776 RVA: 0x0002EECD File Offset: 0x0002D0CD
	protected internal override void onStopped()
	{
		base.SendMessage("TweenStopped", this, SendMessageOptions.DontRequireReceiver);
		if (this.TweenStopped != null)
		{
			this.TweenStopped(this);
		}
	}

	// Token: 0x06000AD9 RID: 2777 RVA: 0x0002EEF0 File Offset: 0x0002D0F0
	protected internal override void onReset()
	{
		base.SendMessage("TweenReset", this, SendMessageOptions.DontRequireReceiver);
		if (this.TweenReset != null)
		{
			this.TweenReset(this);
		}
	}

	// Token: 0x06000ADA RID: 2778 RVA: 0x0002EF13 File Offset: 0x0002D113
	protected internal override void onCompleted()
	{
		base.SendMessage("TweenCompleted", this, SendMessageOptions.DontRequireReceiver);
		if (this.TweenCompleted != null)
		{
			this.TweenCompleted(this);
		}
	}

	// Token: 0x04000533 RID: 1331
	[SerializeField]
	protected T startValue;

	// Token: 0x04000534 RID: 1332
	[SerializeField]
	protected T endValue;

	// Token: 0x04000535 RID: 1333
	[SerializeField]
	protected dfPlayDirection direction;

	// Token: 0x04000536 RID: 1334
	private T actualStartValue;

	// Token: 0x04000537 RID: 1335
	private T actualEndValue;

	// Token: 0x04000538 RID: 1336
	private float startTime;

	// Token: 0x04000539 RID: 1337
	private float pingPongDirection;
}

using System;
using UnityEngine;

// Token: 0x020000CB RID: 203
[Serializable]
public abstract class dfTweenComponentBase : dfTweenPlayableBase
{
	// Token: 0x17000239 RID: 569
	// (get) Token: 0x06000ADC RID: 2780 RVA: 0x0002EF3E File Offset: 0x0002D13E
	// (set) Token: 0x06000ADD RID: 2781 RVA: 0x0002EF5A File Offset: 0x0002D15A
	public override string TweenName
	{
		get
		{
			if (this.tweenName == null)
			{
				this.tweenName = base.ToString();
			}
			return this.tweenName;
		}
		set
		{
			this.tweenName = value;
		}
	}

	// Token: 0x1700023A RID: 570
	// (get) Token: 0x06000ADE RID: 2782 RVA: 0x0002EF63 File Offset: 0x0002D163
	// (set) Token: 0x06000ADF RID: 2783 RVA: 0x0002EF6B File Offset: 0x0002D16B
	public dfComponentMemberInfo Target
	{
		get
		{
			return this.target;
		}
		set
		{
			this.target = value;
		}
	}

	// Token: 0x1700023B RID: 571
	// (get) Token: 0x06000AE0 RID: 2784 RVA: 0x0002EF74 File Offset: 0x0002D174
	// (set) Token: 0x06000AE1 RID: 2785 RVA: 0x0002EF7C File Offset: 0x0002D17C
	public AnimationCurve AnimationCurve
	{
		get
		{
			return this.animCurve;
		}
		set
		{
			this.animCurve = value;
		}
	}

	// Token: 0x1700023C RID: 572
	// (get) Token: 0x06000AE2 RID: 2786 RVA: 0x0002EF85 File Offset: 0x0002D185
	// (set) Token: 0x06000AE3 RID: 2787 RVA: 0x0002EF8D File Offset: 0x0002D18D
	public float Length
	{
		get
		{
			return this.length;
		}
		set
		{
			this.length = Mathf.Max(0f, value);
		}
	}

	// Token: 0x1700023D RID: 573
	// (get) Token: 0x06000AE4 RID: 2788 RVA: 0x0002EFA0 File Offset: 0x0002D1A0
	// (set) Token: 0x06000AE5 RID: 2789 RVA: 0x0002EFA8 File Offset: 0x0002D1A8
	public float StartDelay
	{
		get
		{
			return this.delayBeforeStarting;
		}
		set
		{
			this.delayBeforeStarting = value;
		}
	}

	// Token: 0x1700023E RID: 574
	// (get) Token: 0x06000AE6 RID: 2790 RVA: 0x0002EFB1 File Offset: 0x0002D1B1
	// (set) Token: 0x06000AE7 RID: 2791 RVA: 0x0002EFB9 File Offset: 0x0002D1B9
	public dfEasingType Function
	{
		get
		{
			return this.easingType;
		}
		set
		{
			this.easingType = value;
			if (this.state != dfTweenState.Stopped)
			{
				this.Stop();
				this.Play();
			}
		}
	}

	// Token: 0x1700023F RID: 575
	// (get) Token: 0x06000AE8 RID: 2792 RVA: 0x0002EFD6 File Offset: 0x0002D1D6
	// (set) Token: 0x06000AE9 RID: 2793 RVA: 0x0002EFDE File Offset: 0x0002D1DE
	public dfTweenLoopType LoopType
	{
		get
		{
			return this.loopType;
		}
		set
		{
			this.loopType = value;
			if (this.state != dfTweenState.Stopped)
			{
				this.Stop();
				this.Play();
			}
		}
	}

	// Token: 0x17000240 RID: 576
	// (get) Token: 0x06000AEA RID: 2794 RVA: 0x0002EFFB File Offset: 0x0002D1FB
	// (set) Token: 0x06000AEB RID: 2795 RVA: 0x0002F003 File Offset: 0x0002D203
	public bool SyncStartValueWhenRun
	{
		get
		{
			return this.syncStartWhenRun;
		}
		set
		{
			this.syncStartWhenRun = value;
		}
	}

	// Token: 0x17000241 RID: 577
	// (get) Token: 0x06000AEC RID: 2796 RVA: 0x0002F00C File Offset: 0x0002D20C
	// (set) Token: 0x06000AED RID: 2797 RVA: 0x0002F014 File Offset: 0x0002D214
	public bool StartValueIsOffset
	{
		get
		{
			return this.startValueIsOffset;
		}
		set
		{
			this.startValueIsOffset = value;
		}
	}

	// Token: 0x17000242 RID: 578
	// (get) Token: 0x06000AEE RID: 2798 RVA: 0x0002F01D File Offset: 0x0002D21D
	// (set) Token: 0x06000AEF RID: 2799 RVA: 0x0002F025 File Offset: 0x0002D225
	public bool SyncEndValueWhenRun
	{
		get
		{
			return this.syncEndWhenRun;
		}
		set
		{
			this.syncEndWhenRun = value;
		}
	}

	// Token: 0x17000243 RID: 579
	// (get) Token: 0x06000AF0 RID: 2800 RVA: 0x0002F02E File Offset: 0x0002D22E
	// (set) Token: 0x06000AF1 RID: 2801 RVA: 0x0002F036 File Offset: 0x0002D236
	public bool EndValueIsOffset
	{
		get
		{
			return this.endValueIsOffset;
		}
		set
		{
			this.endValueIsOffset = value;
		}
	}

	// Token: 0x17000244 RID: 580
	// (get) Token: 0x06000AF2 RID: 2802 RVA: 0x0002F03F File Offset: 0x0002D23F
	// (set) Token: 0x06000AF3 RID: 2803 RVA: 0x0002F047 File Offset: 0x0002D247
	public bool AutoRun
	{
		get
		{
			return this.autoRun;
		}
		set
		{
			this.autoRun = value;
		}
	}

	// Token: 0x17000245 RID: 581
	// (get) Token: 0x06000AF4 RID: 2804 RVA: 0x0002F050 File Offset: 0x0002D250
	public override bool IsPlaying
	{
		get
		{
			return base.enabled && this.state > dfTweenState.Stopped;
		}
	}

	// Token: 0x17000246 RID: 582
	// (get) Token: 0x06000AF5 RID: 2805 RVA: 0x0002F065 File Offset: 0x0002D265
	// (set) Token: 0x06000AF6 RID: 2806 RVA: 0x0002F070 File Offset: 0x0002D270
	public bool IsPaused
	{
		get
		{
			return this.state == dfTweenState.Paused;
		}
		set
		{
			bool flag = this.state == dfTweenState.Paused;
			if (value != flag && this.state != dfTweenState.Stopped)
			{
				this.state = (value ? dfTweenState.Paused : dfTweenState.Playing);
				if (value)
				{
					this.onPaused();
					return;
				}
				this.onResumed();
			}
		}
	}

	// Token: 0x06000AF7 RID: 2807
	protected internal abstract void onPaused();

	// Token: 0x06000AF8 RID: 2808
	protected internal abstract void onResumed();

	// Token: 0x06000AF9 RID: 2809
	protected internal abstract void onStarted();

	// Token: 0x06000AFA RID: 2810
	protected internal abstract void onStopped();

	// Token: 0x06000AFB RID: 2811
	protected internal abstract void onReset();

	// Token: 0x06000AFC RID: 2812
	protected internal abstract void onCompleted();

	// Token: 0x06000AFD RID: 2813 RVA: 0x0002F0B0 File Offset: 0x0002D2B0
	public void Start()
	{
		if (this.autoRun && !this.wasAutoStarted)
		{
			this.wasAutoStarted = true;
			this.Play();
		}
	}

	// Token: 0x06000AFE RID: 2814 RVA: 0x0002F0CF File Offset: 0x0002D2CF
	public void OnDisable()
	{
		this.Stop();
		this.wasAutoStarted = false;
	}

	// Token: 0x06000AFF RID: 2815 RVA: 0x0002F0E0 File Offset: 0x0002D2E0
	public override string ToString()
	{
		if (this.Target != null && this.Target.IsValid)
		{
			string name = this.target.Component.name;
			return string.Format("{0} ({1}.{2})", this.TweenName, name, this.target.MemberName);
		}
		return this.TweenName;
	}

	// Token: 0x0400053A RID: 1338
	[SerializeField]
	protected string tweenName = "";

	// Token: 0x0400053B RID: 1339
	[SerializeField]
	protected dfComponentMemberInfo target;

	// Token: 0x0400053C RID: 1340
	[SerializeField]
	protected dfEasingType easingType;

	// Token: 0x0400053D RID: 1341
	[SerializeField]
	protected AnimationCurve animCurve = new AnimationCurve(new Keyframe[]
	{
		new Keyframe(0f, 0f, 0f, 1f),
		new Keyframe(1f, 1f, 1f, 0f)
	});

	// Token: 0x0400053E RID: 1342
	[SerializeField]
	protected float length = 1f;

	// Token: 0x0400053F RID: 1343
	[SerializeField]
	protected bool syncStartWhenRun;

	// Token: 0x04000540 RID: 1344
	[SerializeField]
	protected bool startValueIsOffset;

	// Token: 0x04000541 RID: 1345
	[SerializeField]
	protected bool syncEndWhenRun;

	// Token: 0x04000542 RID: 1346
	[SerializeField]
	protected bool endValueIsOffset;

	// Token: 0x04000543 RID: 1347
	[SerializeField]
	protected dfTweenLoopType loopType;

	// Token: 0x04000544 RID: 1348
	[SerializeField]
	protected bool autoRun;

	// Token: 0x04000545 RID: 1349
	[SerializeField]
	protected bool skipToEndOnStop;

	// Token: 0x04000546 RID: 1350
	[SerializeField]
	protected float delayBeforeStarting;

	// Token: 0x04000547 RID: 1351
	protected dfTweenState state;

	// Token: 0x04000548 RID: 1352
	protected dfEasingFunctions.EasingFunction easingFunction;

	// Token: 0x04000549 RID: 1353
	protected dfObservableProperty boundProperty;

	// Token: 0x0400054A RID: 1354
	protected bool wasAutoStarted;
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using UnityEngine;

// Token: 0x020000AA RID: 170
[AddComponentMenu("Daikon Forge/Tweens/Sprite Animator")]
[Serializable]
public class dfSpriteAnimation : dfTweenPlayableBase
{
	// Token: 0x14000051 RID: 81
	// (add) Token: 0x060009F0 RID: 2544 RVA: 0x0002BC6C File Offset: 0x00029E6C
	// (remove) Token: 0x060009F1 RID: 2545 RVA: 0x0002BCA4 File Offset: 0x00029EA4
	public event TweenNotification AnimationStarted;

	// Token: 0x14000052 RID: 82
	// (add) Token: 0x060009F2 RID: 2546 RVA: 0x0002BCDC File Offset: 0x00029EDC
	// (remove) Token: 0x060009F3 RID: 2547 RVA: 0x0002BD14 File Offset: 0x00029F14
	public event TweenNotification AnimationStopped;

	// Token: 0x14000053 RID: 83
	// (add) Token: 0x060009F4 RID: 2548 RVA: 0x0002BD4C File Offset: 0x00029F4C
	// (remove) Token: 0x060009F5 RID: 2549 RVA: 0x0002BD84 File Offset: 0x00029F84
	public event TweenNotification AnimationPaused;

	// Token: 0x14000054 RID: 84
	// (add) Token: 0x060009F6 RID: 2550 RVA: 0x0002BDBC File Offset: 0x00029FBC
	// (remove) Token: 0x060009F7 RID: 2551 RVA: 0x0002BDF4 File Offset: 0x00029FF4
	public event TweenNotification AnimationResumed;

	// Token: 0x14000055 RID: 85
	// (add) Token: 0x060009F8 RID: 2552 RVA: 0x0002BE2C File Offset: 0x0002A02C
	// (remove) Token: 0x060009F9 RID: 2553 RVA: 0x0002BE64 File Offset: 0x0002A064
	public event TweenNotification AnimationReset;

	// Token: 0x14000056 RID: 86
	// (add) Token: 0x060009FA RID: 2554 RVA: 0x0002BE9C File Offset: 0x0002A09C
	// (remove) Token: 0x060009FB RID: 2555 RVA: 0x0002BED4 File Offset: 0x0002A0D4
	public event TweenNotification AnimationCompleted;

	// Token: 0x17000226 RID: 550
	// (get) Token: 0x060009FC RID: 2556 RVA: 0x0002BF09 File Offset: 0x0002A109
	// (set) Token: 0x060009FD RID: 2557 RVA: 0x0002BF11 File Offset: 0x0002A111
	public dfAnimationClip Clip
	{
		get
		{
			return this.clip;
		}
		set
		{
			this.clip = value;
		}
	}

	// Token: 0x17000227 RID: 551
	// (get) Token: 0x060009FE RID: 2558 RVA: 0x0002BF1A File Offset: 0x0002A11A
	// (set) Token: 0x060009FF RID: 2559 RVA: 0x0002BF22 File Offset: 0x0002A122
	public dfComponentMemberInfo Target
	{
		get
		{
			return this.memberInfo;
		}
		set
		{
			this.memberInfo = value;
		}
	}

	// Token: 0x17000228 RID: 552
	// (get) Token: 0x06000A00 RID: 2560 RVA: 0x0002BF2B File Offset: 0x0002A12B
	// (set) Token: 0x06000A01 RID: 2561 RVA: 0x0002BF33 File Offset: 0x0002A133
	public bool AutoRun
	{
		get
		{
			return this.autoStart;
		}
		set
		{
			this.autoStart = value;
		}
	}

	// Token: 0x17000229 RID: 553
	// (get) Token: 0x06000A02 RID: 2562 RVA: 0x0002BF3C File Offset: 0x0002A13C
	// (set) Token: 0x06000A03 RID: 2563 RVA: 0x0002BF44 File Offset: 0x0002A144
	public float Length
	{
		get
		{
			return this.length;
		}
		set
		{
			this.length = Mathf.Max(value, 0.03f);
		}
	}

	// Token: 0x1700022A RID: 554
	// (get) Token: 0x06000A04 RID: 2564 RVA: 0x0002BF57 File Offset: 0x0002A157
	// (set) Token: 0x06000A05 RID: 2565 RVA: 0x0002BF5F File Offset: 0x0002A15F
	public dfTweenLoopType LoopType
	{
		get
		{
			return this.loopType;
		}
		set
		{
			this.loopType = value;
		}
	}

	// Token: 0x1700022B RID: 555
	// (get) Token: 0x06000A06 RID: 2566 RVA: 0x0002BF68 File Offset: 0x0002A168
	// (set) Token: 0x06000A07 RID: 2567 RVA: 0x0002BF70 File Offset: 0x0002A170
	public dfPlayDirection Direction
	{
		get
		{
			return this.playDirection;
		}
		set
		{
			this.playDirection = value;
			if (this.IsPlaying)
			{
				this.Play();
			}
		}
	}

	// Token: 0x1700022C RID: 556
	// (get) Token: 0x06000A08 RID: 2568 RVA: 0x0002BF87 File Offset: 0x0002A187
	// (set) Token: 0x06000A09 RID: 2569 RVA: 0x0002BF99 File Offset: 0x0002A199
	public bool IsPaused
	{
		get
		{
			return this.isRunning && this.isPaused;
		}
		set
		{
			if (value != this.IsPaused)
			{
				if (value)
				{
					this.Pause();
					return;
				}
				this.Resume();
			}
		}
	}

	// Token: 0x06000A0A RID: 2570 RVA: 0x0002BFB4 File Offset: 0x0002A1B4
	public void Awake()
	{
	}

	// Token: 0x06000A0B RID: 2571 RVA: 0x0002BFB6 File Offset: 0x0002A1B6
	public void Start()
	{
	}

	// Token: 0x06000A0C RID: 2572 RVA: 0x0002BFB8 File Offset: 0x0002A1B8
	public void LateUpdate()
	{
		if (this.AutoRun && !this.IsPlaying && !this.autoRunStarted)
		{
			this.autoRunStarted = true;
			this.Play();
		}
	}

	// Token: 0x06000A0D RID: 2573 RVA: 0x0002BFDF File Offset: 0x0002A1DF
	public void PlayForward()
	{
		this.playDirection = dfPlayDirection.Forward;
		this.Play();
	}

	// Token: 0x06000A0E RID: 2574 RVA: 0x0002BFEE File Offset: 0x0002A1EE
	public void PlayReverse()
	{
		this.playDirection = dfPlayDirection.Reverse;
		this.Play();
	}

	// Token: 0x06000A0F RID: 2575 RVA: 0x0002BFFD File Offset: 0x0002A1FD
	public void Pause()
	{
		if (this.isRunning)
		{
			this.isPaused = true;
			this.onPaused();
		}
	}

	// Token: 0x06000A10 RID: 2576 RVA: 0x0002C014 File Offset: 0x0002A214
	public void Resume()
	{
		if (this.isRunning && this.isPaused)
		{
			this.isPaused = false;
			this.onResumed();
		}
	}

	// Token: 0x1700022D RID: 557
	// (get) Token: 0x06000A11 RID: 2577 RVA: 0x0002C033 File Offset: 0x0002A233
	public override bool IsPlaying
	{
		get
		{
			return this.isRunning;
		}
	}

	// Token: 0x06000A12 RID: 2578 RVA: 0x0002C03C File Offset: 0x0002A23C
	public override void Play()
	{
		if (this.IsPlaying)
		{
			this.Stop();
		}
		if (!base.enabled || !base.gameObject.activeSelf || !base.gameObject.activeInHierarchy)
		{
			return;
		}
		if (this.memberInfo == null)
		{
			throw new NullReferenceException("Animation target is NULL");
		}
		if (!this.memberInfo.IsValid)
		{
			string str = "Invalid property binding configuration on ";
			string path = this.getPath(base.gameObject.transform);
			string str2 = " - ";
			dfObservableProperty dfObservableProperty = this.target;
			throw new InvalidOperationException(str + path + str2 + ((dfObservableProperty != null) ? dfObservableProperty.ToString() : null));
		}
		this.target = this.memberInfo.GetProperty();
		base.StartCoroutine(this.Execute());
	}

	// Token: 0x06000A13 RID: 2579 RVA: 0x0002C0F0 File Offset: 0x0002A2F0
	public override void Reset()
	{
		List<string> list = (this.clip != null) ? this.clip.Sprites : null;
		if (this.memberInfo.IsValid && list != null && list.Count > 0)
		{
			dfSpriteAnimation.SetProperty(this.memberInfo.Component, this.memberInfo.MemberName, list[0]);
		}
		if (!this.isRunning)
		{
			return;
		}
		base.StopAllCoroutines();
		this.isRunning = false;
		this.isPaused = false;
		this.onReset();
		this.target = null;
	}

	// Token: 0x06000A14 RID: 2580 RVA: 0x0002C180 File Offset: 0x0002A380
	public override void Stop()
	{
		if (!this.isRunning)
		{
			return;
		}
		List<string> list = (this.clip != null) ? this.clip.Sprites : null;
		if (this.skipToEndOnStop && list != null)
		{
			this.setFrame(Mathf.Max(list.Count - 1, 0));
		}
		base.StopAllCoroutines();
		this.isRunning = false;
		this.isPaused = false;
		this.onStopped();
		this.target = null;
	}

	// Token: 0x1700022E RID: 558
	// (get) Token: 0x06000A15 RID: 2581 RVA: 0x0002C1F3 File Offset: 0x0002A3F3
	// (set) Token: 0x06000A16 RID: 2582 RVA: 0x0002C1FB File Offset: 0x0002A3FB
	public override string TweenName
	{
		get
		{
			return this.animationName;
		}
		set
		{
			this.animationName = value;
		}
	}

	// Token: 0x06000A17 RID: 2583 RVA: 0x0002C204 File Offset: 0x0002A404
	protected void onPaused()
	{
		base.SendMessage("AnimationPaused", this, SendMessageOptions.DontRequireReceiver);
		if (this.AnimationPaused != null)
		{
			this.AnimationPaused(this);
		}
	}

	// Token: 0x06000A18 RID: 2584 RVA: 0x0002C227 File Offset: 0x0002A427
	protected void onResumed()
	{
		base.SendMessage("AnimationResumed", this, SendMessageOptions.DontRequireReceiver);
		if (this.AnimationResumed != null)
		{
			this.AnimationResumed(this);
		}
	}

	// Token: 0x06000A19 RID: 2585 RVA: 0x0002C24A File Offset: 0x0002A44A
	protected void onStarted()
	{
		base.SendMessage("AnimationStarted", this, SendMessageOptions.DontRequireReceiver);
		if (this.AnimationStarted != null)
		{
			this.AnimationStarted(this);
		}
	}

	// Token: 0x06000A1A RID: 2586 RVA: 0x0002C26D File Offset: 0x0002A46D
	protected void onStopped()
	{
		base.SendMessage("AnimationStopped", this, SendMessageOptions.DontRequireReceiver);
		if (this.AnimationStopped != null)
		{
			this.AnimationStopped(this);
		}
	}

	// Token: 0x06000A1B RID: 2587 RVA: 0x0002C290 File Offset: 0x0002A490
	protected void onReset()
	{
		base.SendMessage("AnimationReset", this, SendMessageOptions.DontRequireReceiver);
		if (this.AnimationReset != null)
		{
			this.AnimationReset(this);
		}
	}

	// Token: 0x06000A1C RID: 2588 RVA: 0x0002C2B3 File Offset: 0x0002A4B3
	protected void onCompleted()
	{
		base.SendMessage("AnimationCompleted", this, SendMessageOptions.DontRequireReceiver);
		if (this.AnimationCompleted != null)
		{
			this.AnimationCompleted(this);
		}
	}

	// Token: 0x06000A1D RID: 2589 RVA: 0x0002C2D8 File Offset: 0x0002A4D8
	internal static void SetProperty(object target, string property, object value)
	{
		if (target == null)
		{
			throw new NullReferenceException("Target is null");
		}
		MemberInfo[] member = target.GetType().GetMember(property, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
		if (member == null || member.Length == 0)
		{
			throw new IndexOutOfRangeException("Property not found: " + property);
		}
		MemberInfo memberInfo = member[0];
		if (memberInfo is FieldInfo)
		{
			((FieldInfo)memberInfo).SetValue(target, value);
			return;
		}
		if (memberInfo is PropertyInfo)
		{
			((PropertyInfo)memberInfo).SetValue(target, value, null);
			return;
		}
		throw new InvalidOperationException("Member type not supported: " + memberInfo.GetMemberType().ToString());
	}

	// Token: 0x06000A1E RID: 2590 RVA: 0x0002C36E File Offset: 0x0002A56E
	private IEnumerator Execute()
	{
		if (this.clip == null || this.clip.Sprites == null || this.clip.Sprites.Count == 0)
		{
			yield break;
		}
		this.isRunning = true;
		this.isPaused = false;
		this.onStarted();
		float startTime = Time.realtimeSinceStartup;
		int direction = (this.playDirection == dfPlayDirection.Forward) ? 1 : -1;
		int lastFrameIndex = (direction == 1) ? 0 : (this.clip.Sprites.Count - 1);
		this.setFrame(lastFrameIndex);
		for (;;)
		{
			yield return null;
			if (!this.IsPaused)
			{
				int num = this.clip.Sprites.Count - 1;
				float realtimeSinceStartup = Time.realtimeSinceStartup;
				float num2 = realtimeSinceStartup - startTime;
				int num3 = Mathf.RoundToInt(Mathf.Clamp01(num2 / this.length) * (float)num);
				if (num2 >= this.length)
				{
					switch (this.loopType)
					{
					case dfTweenLoopType.Once:
						goto IL_12D;
					case dfTweenLoopType.Loop:
						startTime = realtimeSinceStartup;
						num3 = 0;
						break;
					case dfTweenLoopType.PingPong:
						startTime = realtimeSinceStartup;
						direction *= -1;
						num3 = 0;
						break;
					}
				}
				if (direction == -1)
				{
					num3 = num - num3;
				}
				if (lastFrameIndex != num3)
				{
					lastFrameIndex = num3;
					this.setFrame(num3);
				}
			}
		}
		IL_12D:
		this.isRunning = false;
		this.onCompleted();
		yield break;
		yield break;
	}

	// Token: 0x06000A1F RID: 2591 RVA: 0x0002C380 File Offset: 0x0002A580
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

	// Token: 0x06000A20 RID: 2592 RVA: 0x0002C3E0 File Offset: 0x0002A5E0
	private void setFrame(int frameIndex)
	{
		List<string> sprites = this.clip.Sprites;
		if (sprites.Count == 0)
		{
			return;
		}
		frameIndex = Mathf.Max(0, Mathf.Min(frameIndex, sprites.Count - 1));
		if (this.target != null)
		{
			this.target.Value = sprites[frameIndex];
		}
	}

	// Token: 0x040004BC RID: 1212
	[SerializeField]
	private string animationName = "ANIMATION";

	// Token: 0x040004BD RID: 1213
	[SerializeField]
	private dfAnimationClip clip;

	// Token: 0x040004BE RID: 1214
	[SerializeField]
	private dfComponentMemberInfo memberInfo = new dfComponentMemberInfo();

	// Token: 0x040004BF RID: 1215
	[SerializeField]
	private dfTweenLoopType loopType = dfTweenLoopType.Loop;

	// Token: 0x040004C0 RID: 1216
	[SerializeField]
	private float length = 1f;

	// Token: 0x040004C1 RID: 1217
	[SerializeField]
	private bool autoStart;

	// Token: 0x040004C2 RID: 1218
	[SerializeField]
	private bool skipToEndOnStop;

	// Token: 0x040004C3 RID: 1219
	[SerializeField]
	private dfPlayDirection playDirection;

	// Token: 0x040004C4 RID: 1220
	private bool autoRunStarted;

	// Token: 0x040004C5 RID: 1221
	private bool isRunning;

	// Token: 0x040004C6 RID: 1222
	private bool isPaused;

	// Token: 0x040004C7 RID: 1223
	private dfObservableProperty target;
}

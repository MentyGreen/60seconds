using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Token: 0x020000D0 RID: 208
[AddComponentMenu("Daikon Forge/Tweens/Group")]
[Serializable]
public class dfTweenGroup : dfTweenPlayableBase
{
	// Token: 0x1400005D RID: 93
	// (add) Token: 0x06000B12 RID: 2834 RVA: 0x0002F4E8 File Offset: 0x0002D6E8
	// (remove) Token: 0x06000B13 RID: 2835 RVA: 0x0002F520 File Offset: 0x0002D720
	public event TweenNotification TweenStarted;

	// Token: 0x1400005E RID: 94
	// (add) Token: 0x06000B14 RID: 2836 RVA: 0x0002F558 File Offset: 0x0002D758
	// (remove) Token: 0x06000B15 RID: 2837 RVA: 0x0002F590 File Offset: 0x0002D790
	public event TweenNotification TweenStopped;

	// Token: 0x1400005F RID: 95
	// (add) Token: 0x06000B16 RID: 2838 RVA: 0x0002F5C8 File Offset: 0x0002D7C8
	// (remove) Token: 0x06000B17 RID: 2839 RVA: 0x0002F600 File Offset: 0x0002D800
	public event TweenNotification TweenReset;

	// Token: 0x14000060 RID: 96
	// (add) Token: 0x06000B18 RID: 2840 RVA: 0x0002F638 File Offset: 0x0002D838
	// (remove) Token: 0x06000B19 RID: 2841 RVA: 0x0002F670 File Offset: 0x0002D870
	public event TweenNotification TweenCompleted;

	// Token: 0x17000247 RID: 583
	// (get) Token: 0x06000B1A RID: 2842 RVA: 0x0002F6A5 File Offset: 0x0002D8A5
	// (set) Token: 0x06000B1B RID: 2843 RVA: 0x0002F6AD File Offset: 0x0002D8AD
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

	// Token: 0x17000248 RID: 584
	// (get) Token: 0x06000B1C RID: 2844 RVA: 0x0002F6B6 File Offset: 0x0002D8B6
	// (set) Token: 0x06000B1D RID: 2845 RVA: 0x0002F6BE File Offset: 0x0002D8BE
	public bool AutoStart
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

	// Token: 0x17000249 RID: 585
	// (get) Token: 0x06000B1E RID: 2846 RVA: 0x0002F6C7 File Offset: 0x0002D8C7
	// (set) Token: 0x06000B1F RID: 2847 RVA: 0x0002F6CF File Offset: 0x0002D8CF
	public override string TweenName
	{
		get
		{
			return this.groupName;
		}
		set
		{
			this.groupName = value;
		}
	}

	// Token: 0x1700024A RID: 586
	// (get) Token: 0x06000B20 RID: 2848 RVA: 0x0002F6D8 File Offset: 0x0002D8D8
	public override bool IsPlaying
	{
		get
		{
			for (int i = 0; i < this.Tweens.Count; i++)
			{
				if (!(this.Tweens[i] == null) && this.Tweens[i].enabled && this.Tweens[i].IsPlaying)
				{
					return true;
				}
			}
			return false;
		}
	}

	// Token: 0x06000B21 RID: 2849 RVA: 0x0002F738 File Offset: 0x0002D938
	public void Start()
	{
		if (this.AutoStart && !this.IsPlaying)
		{
			this.Play();
		}
	}

	// Token: 0x06000B22 RID: 2850 RVA: 0x0002F750 File Offset: 0x0002D950
	public void EnableTween(string TweenName)
	{
		for (int i = 0; i < this.Tweens.Count; i++)
		{
			if (!(this.Tweens[i] == null) && this.Tweens[i].TweenName == TweenName)
			{
				this.Tweens[i].enabled = true;
				return;
			}
		}
	}

	// Token: 0x06000B23 RID: 2851 RVA: 0x0002F7B4 File Offset: 0x0002D9B4
	public void DisableTween(string TweenName)
	{
		for (int i = 0; i < this.Tweens.Count; i++)
		{
			if (!(this.Tweens[i] == null) && this.Tweens[i].name == TweenName)
			{
				this.Tweens[i].enabled = false;
				return;
			}
		}
	}

	// Token: 0x06000B24 RID: 2852 RVA: 0x0002F817 File Offset: 0x0002DA17
	public override void Play()
	{
		if (this.IsPlaying)
		{
			this.Stop();
		}
		this.onStarted();
		if (this.Mode == dfTweenGroup.TweenGroupMode.Concurrent)
		{
			base.StartCoroutine(this.runConcurrent());
			return;
		}
		base.StartCoroutine(this.runSequence());
	}

	// Token: 0x06000B25 RID: 2853 RVA: 0x0002F850 File Offset: 0x0002DA50
	public override void Stop()
	{
		if (!this.IsPlaying)
		{
			return;
		}
		base.StopAllCoroutines();
		for (int i = 0; i < this.Tweens.Count; i++)
		{
			if (!(this.Tweens[i] == null))
			{
				this.Tweens[i].Stop();
			}
		}
		this.onStopped();
	}

	// Token: 0x06000B26 RID: 2854 RVA: 0x0002F8B0 File Offset: 0x0002DAB0
	public override void Reset()
	{
		if (!this.IsPlaying)
		{
			return;
		}
		base.StopAllCoroutines();
		for (int i = 0; i < this.Tweens.Count; i++)
		{
			if (!(this.Tweens[i] == null))
			{
				this.Tweens[i].Reset();
			}
		}
		this.onReset();
	}

	// Token: 0x06000B27 RID: 2855 RVA: 0x0002F90D File Offset: 0x0002DB0D
	[HideInInspector]
	private IEnumerator runSequence()
	{
		if (this.delayBeforeStarting > 0f)
		{
			float timeout = Time.realtimeSinceStartup + this.delayBeforeStarting;
			while (Time.realtimeSinceStartup < timeout)
			{
				yield return null;
			}
		}
		for (int i = 0; i < this.Tweens.Count; i++)
		{
			if (!(this.Tweens[i] == null) && this.Tweens[i].enabled)
			{
				dfTweenPlayableBase tween = this.Tweens[i];
				tween.Play();
				while (tween.IsPlaying)
				{
					yield return null;
				}
				tween = null;
			}
		}
		this.onCompleted();
		yield break;
	}

	// Token: 0x06000B28 RID: 2856 RVA: 0x0002F91C File Offset: 0x0002DB1C
	[HideInInspector]
	private IEnumerator runConcurrent()
	{
		if (this.delayBeforeStarting > 0f)
		{
			float timeout = Time.realtimeSinceStartup + this.delayBeforeStarting;
			while (Time.realtimeSinceStartup < timeout)
			{
				yield return null;
			}
		}
		for (int i = 0; i < this.Tweens.Count; i++)
		{
			if (!(this.Tweens[i] == null) && this.Tweens[i].enabled)
			{
				this.Tweens[i].Play();
			}
		}
		do
		{
			yield return null;
		}
		while (this.Tweens.Any((dfTweenPlayableBase tween) => tween != null && tween.IsPlaying));
		this.onCompleted();
		yield break;
	}

	// Token: 0x06000B29 RID: 2857 RVA: 0x0002F92B File Offset: 0x0002DB2B
	protected internal void onStarted()
	{
		base.SendMessage("TweenStarted", this, SendMessageOptions.DontRequireReceiver);
		if (this.TweenStarted != null)
		{
			this.TweenStarted(this);
		}
	}

	// Token: 0x06000B2A RID: 2858 RVA: 0x0002F94E File Offset: 0x0002DB4E
	protected internal void onStopped()
	{
		base.SendMessage("TweenStopped", this, SendMessageOptions.DontRequireReceiver);
		if (this.TweenStopped != null)
		{
			this.TweenStopped(this);
		}
	}

	// Token: 0x06000B2B RID: 2859 RVA: 0x0002F971 File Offset: 0x0002DB71
	protected internal void onReset()
	{
		base.SendMessage("TweenReset", this, SendMessageOptions.DontRequireReceiver);
		if (this.TweenReset != null)
		{
			this.TweenReset(this);
		}
	}

	// Token: 0x06000B2C RID: 2860 RVA: 0x0002F994 File Offset: 0x0002DB94
	protected internal void onCompleted()
	{
		base.SendMessage("TweenCompleted", this, SendMessageOptions.DontRequireReceiver);
		if (this.TweenCompleted != null)
		{
			this.TweenCompleted(this);
		}
	}

	// Token: 0x0400055C RID: 1372
	[SerializeField]
	protected string groupName = "";

	// Token: 0x0400055D RID: 1373
	[SerializeField]
	protected bool autoStart;

	// Token: 0x0400055E RID: 1374
	[SerializeField]
	protected float delayBeforeStarting;

	// Token: 0x0400055F RID: 1375
	public List<dfTweenPlayableBase> Tweens = new List<dfTweenPlayableBase>();

	// Token: 0x04000560 RID: 1376
	public dfTweenGroup.TweenGroupMode Mode;

	// Token: 0x0200038A RID: 906
	public enum TweenGroupMode
	{
		// Token: 0x0400169A RID: 5786
		Concurrent,
		// Token: 0x0400169B RID: 5787
		Sequence
	}
}

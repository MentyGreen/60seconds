using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x020000FD RID: 253
public class Loading : MonoBehaviour
{
	// Token: 0x14000062 RID: 98
	// (add) Token: 0x06000C4B RID: 3147 RVA: 0x00036094 File Offset: 0x00034294
	// (remove) Token: 0x06000C4C RID: 3148 RVA: 0x000360CC File Offset: 0x000342CC
	public event Loading.ReportLoading OnLoadReady;

	// Token: 0x14000063 RID: 99
	// (add) Token: 0x06000C4D RID: 3149 RVA: 0x00036104 File Offset: 0x00034304
	// (remove) Token: 0x06000C4E RID: 3150 RVA: 0x0003613C File Offset: 0x0003433C
	public event Loading.ReportLoading OnLoadDone;

	// Token: 0x06000C4F RID: 3151 RVA: 0x00036171 File Offset: 0x00034371
	private void Awake()
	{
		Loading.Loader = this;
		Object.DontDestroyOnLoad(base.gameObject);
	}

	// Token: 0x06000C50 RID: 3152 RVA: 0x00036184 File Offset: 0x00034384
	private void Start()
	{
	}

	// Token: 0x06000C51 RID: 3153 RVA: 0x00036186 File Offset: 0x00034386
	private void Update()
	{
	}

	// Token: 0x06000C52 RID: 3154 RVA: 0x00036188 File Offset: 0x00034388
	public void GoToLoading(bool skipLoadingLevel = false)
	{
		bool flag = !string.IsNullOrEmpty(this._nextLevelName);
		bool flag2 = !skipLoadingLevel && !string.IsNullOrEmpty(this._loadingLevelName);
		if (flag && flag2)
		{
			base.StartCoroutine(this.LoadLoadingLevel());
			return;
		}
		if (flag)
		{
			SceneManager.LoadScene(this._nextLevelName);
		}
	}

	// Token: 0x06000C53 RID: 3155 RVA: 0x000361DA File Offset: 0x000343DA
	protected IEnumerator LoadLoadingLevel()
	{
		SceneManager.LoadScene(this._loadingLevelName);
		yield return new WaitForSeconds(this._loadingDelay);
		this.StartLoadingNextLevel();
		yield break;
	}

	// Token: 0x06000C54 RID: 3156 RVA: 0x000361E9 File Offset: 0x000343E9
	public void StartLoadingNextLevel()
	{
		base.StartCoroutine(this.LoadNextLevel());
	}

	// Token: 0x06000C55 RID: 3157 RVA: 0x000361F8 File Offset: 0x000343F8
	protected IEnumerator LoadNextLevel()
	{
		this._loadingProcess = SceneManager.LoadSceneAsync(this._nextLevelName);
		this._loadingProcess.allowSceneActivation = this._autoLoad;
		while (!this._loadingProcess.isDone)
		{
			yield return new WaitForSeconds(this._loadingProgressTimeout);
			if (!this._autoLoad)
			{
				if (this._ready)
				{
					this._loadingProcess.allowSceneActivation = true;
				}
				else if (!this._signalledReady && this._loadingProcess.progress >= 0.9f)
				{
					this._signalledReady = true;
					if (this._waitForFadeOut)
					{
						if (this.OnLoadDone != null)
						{
							this.OnLoadDone(this._nextLevelName, 1f, true);
						}
						SoundManager.Instance.CrossOut(1.5f, SoundManager.Instance.MusicSource);
						yield return new WaitForSeconds(1.25f);
						this.SignalReady();
					}
					else
					{
						this.InvokeWaitingForSignal(this._loadingProcess.progress, true);
					}
				}
			}
		}
		if (this._autoLoad || !this._waitForFadeOut)
		{
			if (this.OnLoadDone != null)
			{
				this.OnLoadDone(this._nextLevelName, 1f, true);
			}
			yield return new WaitForSeconds(0.75f);
		}
		this._ready = false;
		this._signalledReady = false;
		this._loadingProcess = null;
		yield break;
	}

	// Token: 0x06000C56 RID: 3158 RVA: 0x00036208 File Offset: 0x00034408
	protected void InvokeWaitingForSignal(float progress, bool complete)
	{
		if (this.OnLoadReady != null)
		{
			this.OnLoadReady(this._nextLevelName, progress, complete);
		}
		GameObject.FindGameObjectWithTag("LoadingProcess").GetComponent<dfControl>().IsVisible = false;
		GameObject gameObject = GameObject.FindGameObjectWithTag("LoadingReady");
		gameObject.GetComponent<dfControl>().IsVisible = true;
		dfEventBinding component = gameObject.GetComponent<dfEventBinding>();
		component.DataTarget.Component = this;
		component.DataTarget.MemberName = "SignalReady";
		component.Bind();
	}

	// Token: 0x06000C57 RID: 3159 RVA: 0x00036281 File Offset: 0x00034481
	public void SignalReady()
	{
		this._ready = true;
	}

	// Token: 0x06000C58 RID: 3160 RVA: 0x0003628A File Offset: 0x0003448A
	public void CompleteLoadingNextLevel()
	{
		if (this._loadingProcess != null)
		{
			this._loadingProcess.allowSceneActivation = true;
		}
	}

	// Token: 0x17000266 RID: 614
	// (get) Token: 0x06000C59 RID: 3161 RVA: 0x000362A0 File Offset: 0x000344A0
	// (set) Token: 0x06000C5A RID: 3162 RVA: 0x000362A8 File Offset: 0x000344A8
	public string NextLevelName
	{
		get
		{
			return this._nextLevelName;
		}
		set
		{
			if (value == "main_menu" && Random.Range(0, 2) > 0)
			{
				this._nextLevelName = "main_menu_astro";
				return;
			}
			this._nextLevelName = value;
		}
	}

	// Token: 0x17000267 RID: 615
	// (get) Token: 0x06000C5B RID: 3163 RVA: 0x000362D4 File Offset: 0x000344D4
	// (set) Token: 0x06000C5C RID: 3164 RVA: 0x000362DC File Offset: 0x000344DC
	public string LoadingLevelName
	{
		get
		{
			return this._loadingLevelName;
		}
		set
		{
			this._loadingLevelName = string.Empty;
			for (int i = 0; i < this._loadLevels.Length; i++)
			{
				if (this._loadLevels[i] == value)
				{
					this._loadingLevelName = value;
					return;
				}
			}
		}
	}

	// Token: 0x17000268 RID: 616
	// (get) Token: 0x06000C5D RID: 3165 RVA: 0x0003631F File Offset: 0x0003451F
	// (set) Token: 0x06000C5E RID: 3166 RVA: 0x00036327 File Offset: 0x00034527
	public bool AutoLoad
	{
		get
		{
			return this._autoLoad;
		}
		set
		{
			this._autoLoad = value;
		}
	}

	// Token: 0x04000695 RID: 1685
	public static Loading Loader;

	// Token: 0x04000696 RID: 1686
	[SerializeField]
	protected string[] _loadLevels;

	// Token: 0x04000697 RID: 1687
	[SerializeField]
	protected string _nextLevelName = string.Empty;

	// Token: 0x04000698 RID: 1688
	[SerializeField]
	protected string _loadingLevelName = "loading";

	// Token: 0x04000699 RID: 1689
	[SerializeField]
	protected bool _autoLoad = true;

	// Token: 0x0400069A RID: 1690
	[SerializeField]
	protected bool _waitForFadeOut = true;

	// Token: 0x0400069B RID: 1691
	[SerializeField]
	protected float _loadingDelay = 1f;

	// Token: 0x0400069C RID: 1692
	[SerializeField]
	protected float _loadingProgressTimeout = 0.25f;

	// Token: 0x0400069D RID: 1693
	protected AsyncOperation _loadingProcess;

	// Token: 0x0400069E RID: 1694
	protected bool _ready;

	// Token: 0x0400069F RID: 1695
	protected bool _signalledReady;

	// Token: 0x040006A0 RID: 1696
	protected int _contentIndex;

	// Token: 0x020003A8 RID: 936
	// (Invoke) Token: 0x06001DA9 RID: 7593
	public delegate void ReportLoading(string levelName, float progress, bool complete);
}

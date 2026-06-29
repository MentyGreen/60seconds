using System;
using UnityEngine;

// Token: 0x02000100 RID: 256
public class SimpleIntro : MonoBehaviour
{
	// Token: 0x06000C65 RID: 3173 RVA: 0x0003646D File Offset: 0x0003466D
	private void Awake()
	{
	}

	// Token: 0x06000C66 RID: 3174 RVA: 0x00036470 File Offset: 0x00034670
	private void Start()
	{
		Settings.Data.ShowCursor = false;
		Settings.Data.LockCursor = CursorLockMode.Locked;
		if (Settings.Data.SkipIntro)
		{
			this.End();
			return;
		}
		if (this._introAnimation != null)
		{
			this._introAnimation.Play();
		}
	}

	// Token: 0x06000C67 RID: 3175 RVA: 0x000364C0 File Offset: 0x000346C0
	private void Update()
	{
		if (this._canSkip)
		{
			if (this._skip)
			{
				if (this._skipInvokeTime <= Time.time)
				{
					this.End();
					return;
				}
			}
			else
			{
				if (!this._skipPress)
				{
					this._skip = true;
					this._skipInvokeTime = Time.time + this._skipTimeout;
					return;
				}
				if (Input.anyKey)
				{
					this.End();
					return;
				}
			}
		}
		else
		{
			this._skip = false;
		}
	}

	// Token: 0x06000C68 RID: 3176 RVA: 0x00036528 File Offset: 0x00034728
	public void End()
	{
		Loading.Loader.NextLevelName = "main_menu";
		Loading.Loader.GoToLoading(false);
	}

	// Token: 0x040006A2 RID: 1698
	private bool _canProgress;

	// Token: 0x040006A3 RID: 1699
	private bool _canSkip = true;

	// Token: 0x040006A4 RID: 1700
	private bool _skip;

	// Token: 0x040006A5 RID: 1701
	private float _skipInvokeTime;

	// Token: 0x040006A6 RID: 1702
	private bool _skipPress = true;

	// Token: 0x040006A7 RID: 1703
	[SerializeField]
	private float _skipTimeout = 0.05f;

	// Token: 0x040006A8 RID: 1704
	[SerializeField]
	private dfTweenPlayableBase _introAnimation;
}

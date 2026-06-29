using System;
using System.Collections;
using UnityEngine;

// Token: 0x020000FC RID: 252
public class Intro : MonoBehaviour
{
	// Token: 0x06000C45 RID: 3141 RVA: 0x00035F64 File Offset: 0x00034164
	private void Awake()
	{
	}

	// Token: 0x06000C46 RID: 3142 RVA: 0x00035F66 File Offset: 0x00034166
	private void Start()
	{
		Settings.Data.ShowCursor = false;
		Settings.Data.LockCursor = CursorLockMode.Locked;
		if (Settings.Data.SkipIntro)
		{
			this.End(true);
			return;
		}
		base.StartCoroutine(this.IntroProgress());
	}

	// Token: 0x06000C47 RID: 3143 RVA: 0x00035F9F File Offset: 0x0003419F
	private IEnumerator IntroProgress()
	{
		yield return new WaitForSeconds(0.25f);
		this._video = Object.FindObjectOfType<PlayVideo>();
		if (this._video != null)
		{
			this._video.UpdateScaling();
			this._video.Play();
		}
		yield break;
	}

	// Token: 0x06000C48 RID: 3144 RVA: 0x00035FB0 File Offset: 0x000341B0
	private void Update()
	{
		if (this._video != null && !this._video.IsPlaying())
		{
			this.End(true);
		}
		if (this._canSkip)
		{
			if (this._skip)
			{
				if (this._skipInvokeTime <= Time.time)
				{
					this.End(true);
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
					this.End(true);
					return;
				}
			}
		}
		else
		{
			this._skip = false;
		}
	}

	// Token: 0x06000C49 RID: 3145 RVA: 0x0003603C File Offset: 0x0003423C
	public void End(bool menu)
	{
		if (menu)
		{
			PlayVideo playVideo = Object.FindObjectOfType<PlayVideo>();
			if (playVideo != null && playVideo.IsPlaying())
			{
				playVideo.Stop();
			}
			Application.LoadLevel("main_menu");
		}
	}

	// Token: 0x0400068C RID: 1676
	private bool _canProgress;

	// Token: 0x0400068D RID: 1677
	private bool _canSkip = true;

	// Token: 0x0400068E RID: 1678
	private bool _skip;

	// Token: 0x0400068F RID: 1679
	private float _skipInvokeTime;

	// Token: 0x04000690 RID: 1680
	private bool _skipPress = true;

	// Token: 0x04000691 RID: 1681
	[SerializeField]
	private float _skipTimeout = 0.05f;

	// Token: 0x04000692 RID: 1682
	private PlayVideo _video;
}

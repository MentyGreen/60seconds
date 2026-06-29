using System;
using UnityEngine;

// Token: 0x02000134 RID: 308
[RequireComponent(typeof(AudioSource))]
public class PlayVideo : MonoBehaviour
{
	// Token: 0x06000F18 RID: 3864 RVA: 0x0003E917 File Offset: 0x0003CB17
	private void Awake()
	{
		this._originalOrtoSize = Camera.main.orthographicSize;
	}

	// Token: 0x06000F19 RID: 3865 RVA: 0x0003E929 File Offset: 0x0003CB29
	private void Start()
	{
	}

	// Token: 0x06000F1A RID: 3866 RVA: 0x0003E92B File Offset: 0x0003CB2B
	public void UpdateScaling()
	{
	}

	// Token: 0x06000F1B RID: 3867 RVA: 0x0003E92D File Offset: 0x0003CB2D
	public void Play()
	{
	}

	// Token: 0x06000F1C RID: 3868 RVA: 0x0003E92F File Offset: 0x0003CB2F
	public void Pause()
	{
	}

	// Token: 0x06000F1D RID: 3869 RVA: 0x0003E931 File Offset: 0x0003CB31
	public void Stop()
	{
	}

	// Token: 0x06000F1E RID: 3870 RVA: 0x0003E933 File Offset: 0x0003CB33
	public bool IsPlaying()
	{
		return false;
	}

	// Token: 0x0400091C RID: 2332
	private float _originalOrtoSize = 1f;
}

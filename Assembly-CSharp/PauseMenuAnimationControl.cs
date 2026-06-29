using System;
using UnityEngine;

// Token: 0x0200014A RID: 330
public class PauseMenuAnimationControl : MonoBehaviour
{
	// Token: 0x06000FAD RID: 4013 RVA: 0x000411BB File Offset: 0x0003F3BB
	public void SetTimeScaleToZero()
	{
		Time.timeScale = 0f;
	}

	// Token: 0x06000FAE RID: 4014 RVA: 0x000411C7 File Offset: 0x0003F3C7
	public void DisablePauseMenuObject()
	{
		this._pauseMenuObject.SetActive(false);
	}

	// Token: 0x040009AA RID: 2474
	[SerializeField]
	private GameObject _pauseMenuObject;
}

using System;
using UnityEngine;

// Token: 0x0200011E RID: 286
public class ScavengeGUISetup : MonoBehaviour
{
	// Token: 0x06000E27 RID: 3623 RVA: 0x0003AE49 File Offset: 0x00039049
	private void Awake()
	{
		if (!this._processed)
		{
			this.Process();
		}
	}

	// Token: 0x06000E28 RID: 3624 RVA: 0x0003AE5C File Offset: 0x0003905C
	public void Process()
	{
		this._processed = true;
		ResolutionHandler resolutionHandler = Object.FindObjectOfType<ResolutionHandler>();
		GlobalTools.GetController<GameFlow>();
		bool flag = ResolutionHandler.Is169(resolutionHandler.SelectedAspectRatio.AspectRatio);
		bool flag2 = ResolutionHandler.Is1610(resolutionHandler.SelectedAspectRatio.AspectRatio);
		bool flag3 = ResolutionHandler.Is43(resolutionHandler.SelectedAspectRatio.AspectRatio);
		if (!flag && !flag2 && !flag3)
		{
			flag = true;
		}
		if (flag3 && resolutionHandler.SelectedAspectRatio.AspectRatio == 1.25f)
		{
			for (int i = 0; i < this._43objects.Length; i++)
			{
				this._43objects[i].GetComponent<dfSprite>().Width /= 1.25f;
			}
		}
		ScavengeGUISetup.ActivateObjects(this._nativeObjects, flag);
		ScavengeGUISetup.ActivateTweens(this._nativeTweens, flag);
		ScavengeGUISetup.ActivateObjects(this._1610objects, flag2);
		ScavengeGUISetup.ActivateTweens(this._1610tweens, flag2);
		ScavengeGUISetup.ActivateObjects(this._43objects, flag3);
		ScavengeGUISetup.ActivateTweens(this._43tweens, flag3);
		Object.Destroy(this);
	}

	// Token: 0x06000E29 RID: 3625 RVA: 0x0003AF64 File Offset: 0x00039164
	private static void ActivateObjects(GameObject[] objects = null, bool activate = false)
	{
		if (objects != null)
		{
			for (int i = 0; i < objects.Length; i++)
			{
				objects[i].SetActive(activate);
			}
		}
	}

	// Token: 0x06000E2A RID: 3626 RVA: 0x0003AF8C File Offset: 0x0003918C
	private static void ActivateTweens(dfTweenPlayableBase[] tweens = null, bool activate = false)
	{
		if (tweens != null)
		{
			for (int i = 0; i < tweens.Length; i++)
			{
				tweens[i].enabled = activate;
			}
		}
	}

	// Token: 0x04000866 RID: 2150
	[SerializeField]
	private GameObject[] _43objects;

	// Token: 0x04000867 RID: 2151
	[SerializeField]
	private dfTweenPlayableBase[] _43tweens;

	// Token: 0x04000868 RID: 2152
	[SerializeField]
	private GameObject[] _nativeObjects;

	// Token: 0x04000869 RID: 2153
	[SerializeField]
	private dfTweenPlayableBase[] _nativeTweens;

	// Token: 0x0400086A RID: 2154
	[SerializeField]
	private GameObject[] _1610objects;

	// Token: 0x0400086B RID: 2155
	[SerializeField]
	private dfTweenPlayableBase[] _1610tweens;

	// Token: 0x0400086C RID: 2156
	private bool _processed;
}

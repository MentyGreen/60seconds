using System;
using RG.Parsecs.EventEditor;
using RG.Remaster.Survival;
using UnityEngine;

// Token: 0x02000158 RID: 344
public class DisableSkinIfNotUnlocked : MonoBehaviour
{
	// Token: 0x06000FF4 RID: 4084 RVA: 0x00041DB2 File Offset: 0x0003FFB2
	private void OnDestroy()
	{
		if (this._isSkinUnlockedVariable != null && !this._isSkinUnlockedVariable.Value)
		{
			this._skinController.CurrentSkinIndex.Value = 0;
		}
	}

	// Token: 0x040009E3 RID: 2531
	[SerializeField]
	private GlobalBoolVariable _isSkinUnlockedVariable;

	// Token: 0x040009E4 RID: 2532
	[SerializeField]
	private SkinController _skinController;
}

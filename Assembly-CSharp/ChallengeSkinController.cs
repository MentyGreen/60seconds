using System;
using RG.Parsecs.EventEditor;
using RG.Remaster.Survival;
using UnityEngine;

// Token: 0x02000159 RID: 345
public class ChallengeSkinController : MonoBehaviour
{
	// Token: 0x06000FF6 RID: 4086 RVA: 0x00041DE8 File Offset: 0x0003FFE8
	private void Start()
	{
		if (this._isChallenge01Active != null && this._isChallenge01Active.Value)
		{
			this._skinController.ForceSkinUse(this._forcedSkinId);
		}
	}

	// Token: 0x06000FF7 RID: 4087 RVA: 0x00041E16 File Offset: 0x00040016
	private void OnDestroy()
	{
		if (this._isSkinUnlockedVariable != null && !this._isSkinUnlockedVariable.Value)
		{
			this._skinController.CurrentSkinIndex.Value = 0;
		}
	}

	// Token: 0x040009E5 RID: 2533
	[SerializeField]
	private GlobalBoolVariable _isSkinUnlockedVariable;

	// Token: 0x040009E6 RID: 2534
	[SerializeField]
	private GlobalBoolVariable _isChallenge01Active;

	// Token: 0x040009E7 RID: 2535
	[SerializeField]
	private SkinId _forcedSkinId;

	// Token: 0x040009E8 RID: 2536
	[SerializeField]
	private SkinController _skinController;
}

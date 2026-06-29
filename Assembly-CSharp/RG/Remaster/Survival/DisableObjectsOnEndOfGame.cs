using System;
using System.Collections.Generic;
using RG.Parsecs.EventEditor;
using RG.Parsecs.Survival;
using UnityEngine;

namespace RG.Remaster.Survival
{
	// Token: 0x0200022A RID: 554
	public class DisableObjectsOnEndOfGame : MonoBehaviour
	{
		// Token: 0x0600156D RID: 5485 RVA: 0x0005EA70 File Offset: 0x0005CC70
		private void Awake()
		{
			if (this._disableObjectsOnEndgame && this._objectsToDisableOnEndgameList.Count != 0)
			{
				if (this._isScavengeOnly != null && this._isScavengeOnly.Value)
				{
					this.DeactivateObjectsOnEndgame(this._objectsToDisableOnEndgameList);
					return;
				}
				this._eodListenerList.RegisterOnEndOfDay(new EndOfDayListenerList.OnEndOfDay(this.OnEndOfDay), "PrepareSystem", 999, this, false);
			}
		}

		// Token: 0x0600156E RID: 5486 RVA: 0x0005EADD File Offset: 0x0005CCDD
		private void OnDestroy()
		{
			this._eodListenerList.UnregisterOnEndOfDay(new EndOfDayListenerList.OnEndOfDay(this.OnEndOfDay), "PrepareSystem");
		}

		// Token: 0x0600156F RID: 5487 RVA: 0x0005EAFC File Offset: 0x0005CCFC
		private void DeactivateObjectsOnEndgame(List<GameObject> objects)
		{
			if (objects == null)
			{
				return;
			}
			for (int i = 0; i < objects.Count; i++)
			{
				objects[i].SetActive(false);
			}
		}

		// Token: 0x06001570 RID: 5488 RVA: 0x0005EB2C File Offset: 0x0005CD2C
		private void OnEndOfDay()
		{
			if ((this._endGameData != null && this._endGameData.RuntimeData.ShouldEndGame) || (this._isAbsence != null && this._isAbsence.Value) || (this._isHatchVisible != null && this._isHatchVisible.Value))
			{
				this.DeactivateObjectsOnEndgame(this._objectsToDisableOnEndgameList);
			}
		}

		// Token: 0x04000E53 RID: 3667
		[SerializeField]
		private bool _disableObjectsOnEndgame;

		// Token: 0x04000E54 RID: 3668
		[SerializeField]
		private List<GameObject> _objectsToDisableOnEndgameList;

		// Token: 0x04000E55 RID: 3669
		[SerializeField]
		private EndOfDayListenerList _eodListenerList;

		// Token: 0x04000E56 RID: 3670
		[SerializeField]
		private EndGameData _endGameData;

		// Token: 0x04000E57 RID: 3671
		[SerializeField]
		private GlobalBoolVariable _isAbsence;

		// Token: 0x04000E58 RID: 3672
		[SerializeField]
		private GlobalBoolVariable _isHatchVisible;

		// Token: 0x04000E59 RID: 3673
		[SerializeField]
		private GlobalBoolVariable _isScavengeOnly;
	}
}

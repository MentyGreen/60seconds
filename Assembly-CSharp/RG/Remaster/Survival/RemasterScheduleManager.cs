using System;
using RG.Parsecs.EventEditor;
using RG.Parsecs.Survival;
using UnityEngine;

namespace RG.Remaster.Survival
{
	// Token: 0x0200022F RID: 559
	public class RemasterScheduleManager : SchedulerManager
	{
		// Token: 0x06001587 RID: 5511 RVA: 0x0005F310 File Offset: 0x0005D510
		private new void Start()
		{
			this._saveFirstDay = (this._isGameShouldBeSave.Value && !this._endGameData.RuntimeData.ShouldEndGame && this._allowSurvivalInteraction.Value);
			this._metagameFunction.Execute(null);
			if (this._isContinueAvailable != null && this._saveFirstDay)
			{
				this._isContinueAvailable.Value = true;
			}
			base.Start();
		}

		// Token: 0x04000E6F RID: 3695
		[SerializeField]
		private GlobalBoolVariable _isGameShouldBeSave;

		// Token: 0x04000E70 RID: 3696
		[SerializeField]
		private GlobalBoolVariable _allowSurvivalInteraction;

		// Token: 0x04000E71 RID: 3697
		[SerializeField]
		private EndGameData _endGameData;

		// Token: 0x04000E72 RID: 3698
		[SerializeField]
		private GlobalBoolVariable _isContinueAvailable;

		// Token: 0x04000E73 RID: 3699
		[SerializeField]
		private NodeFunction _metagameFunction;
	}
}

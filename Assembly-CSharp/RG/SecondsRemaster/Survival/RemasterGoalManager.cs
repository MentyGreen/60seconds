using System;
using RG.Parsecs.Common;
using RG.Parsecs.EventEditor;
using UnityEngine;

namespace RG.SecondsRemaster.Survival
{
	// Token: 0x02000336 RID: 822
	public class RemasterGoalManager : GoalManager
	{
		// Token: 0x06001B7D RID: 7037 RVA: 0x0007674E File Offset: 0x0007494E
		protected override void StartGlowing()
		{
			if (this._goalTabVisibleVariable != null)
			{
				this._goalTabVisibleVariable.Value = true;
			}
			if (this._goalTabAttentionVariable != null)
			{
				this._goalTabAttentionVariable.Value = true;
			}
			this._shouldGlow = true;
		}

		// Token: 0x0400153C RID: 5436
		[SerializeField]
		private GlobalBoolVariable _goalTabAttentionVariable;

		// Token: 0x0400153D RID: 5437
		[SerializeField]
		private GlobalBoolVariable _goalTabVisibleVariable;
	}
}

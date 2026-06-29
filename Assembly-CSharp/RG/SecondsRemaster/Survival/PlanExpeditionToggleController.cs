using System;
using RG.Parsecs.Common;
using RG.Parsecs.EventEditor;
using UnityEngine;

namespace RG.SecondsRemaster.Survival
{
	// Token: 0x02000323 RID: 803
	public class PlanExpeditionToggleController : ToggleController
	{
		// Token: 0x06001B0C RID: 6924 RVA: 0x000749FD File Offset: 0x00072BFD
		public override void OnToggleValueChangedAction(bool toggleValue)
		{
			this._shouldDisplaySendExpeditionPage.SetValue(toggleValue);
			this._ouClickedSoundPlayer.PlaySound();
		}

		// Token: 0x040014C8 RID: 5320
		[SerializeField]
		private GlobalBoolVariable _shouldDisplaySendExpeditionPage;

		// Token: 0x040014C9 RID: 5321
		[SerializeField]
		private OnUIClickedSoundPlayer _ouClickedSoundPlayer;
	}
}

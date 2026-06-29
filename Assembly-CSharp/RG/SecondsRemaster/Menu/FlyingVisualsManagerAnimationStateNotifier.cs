using System;
using UnityEngine;

namespace RG.SecondsRemaster.Menu
{
	// Token: 0x02000286 RID: 646
	public class FlyingVisualsManagerAnimationStateNotifier : MonoBehaviour
	{
		// Token: 0x060017C4 RID: 6084 RVA: 0x000682F6 File Offset: 0x000664F6
		public void NotifyAnimationEnded()
		{
			this._manager.IsAnimationPlaying = false;
		}

		// Token: 0x04001161 RID: 4449
		[SerializeField]
		private FlyingVisualsManager _manager;
	}
}

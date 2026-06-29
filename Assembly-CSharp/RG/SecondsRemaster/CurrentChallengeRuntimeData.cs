using System;
using UnityEngine;

namespace RG.SecondsRemaster
{
	// Token: 0x02000243 RID: 579
	[Serializable]
	public class CurrentChallengeRuntimeData
	{
		// Token: 0x17000438 RID: 1080
		// (get) Token: 0x06001605 RID: 5637 RVA: 0x00060CEB File Offset: 0x0005EEEB
		// (set) Token: 0x06001606 RID: 5638 RVA: 0x00060CF3 File Offset: 0x0005EEF3
		public Challenge Challenge
		{
			get
			{
				return this._challenge;
			}
			set
			{
				this._challenge = value;
			}
		}

		// Token: 0x04000EC3 RID: 3779
		[SerializeField]
		private Challenge _challenge;
	}
}

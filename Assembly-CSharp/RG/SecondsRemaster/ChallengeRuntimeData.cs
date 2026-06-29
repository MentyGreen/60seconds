using System;
using UnityEngine;

namespace RG.SecondsRemaster
{
	// Token: 0x02000240 RID: 576
	[Serializable]
	public class ChallengeRuntimeData
	{
		// Token: 0x17000433 RID: 1075
		// (get) Token: 0x060015F4 RID: 5620 RVA: 0x00060BC0 File Offset: 0x0005EDC0
		// (set) Token: 0x060015F5 RID: 5621 RVA: 0x00060BC8 File Offset: 0x0005EDC8
		public float Time
		{
			get
			{
				return this._time;
			}
			set
			{
				this._time = value;
			}
		}

		// Token: 0x17000434 RID: 1076
		// (get) Token: 0x060015F6 RID: 5622 RVA: 0x00060BD1 File Offset: 0x0005EDD1
		// (set) Token: 0x060015F7 RID: 5623 RVA: 0x00060BD9 File Offset: 0x0005EDD9
		public string UnlockDate
		{
			get
			{
				return this._unlockDate;
			}
			set
			{
				this._unlockDate = value;
			}
		}

		// Token: 0x04000EBE RID: 3774
		[SerializeField]
		private float _time;

		// Token: 0x04000EBF RID: 3775
		[SerializeField]
		private string _unlockDate;
	}
}

using System;
using UnityEngine;

namespace RG.SecondsRemaster.Core
{
	// Token: 0x0200024E RID: 590
	[Serializable]
	public class SecondsRemediumStaticData
	{
		// Token: 0x17000444 RID: 1092
		// (get) Token: 0x0600162F RID: 5679 RVA: 0x00061062 File Offset: 0x0005F262
		public bool IsDamaged
		{
			get
			{
				return this._isDamaged;
			}
		}

		// Token: 0x04000EDB RID: 3803
		[SerializeField]
		private bool _isDamaged;
	}
}

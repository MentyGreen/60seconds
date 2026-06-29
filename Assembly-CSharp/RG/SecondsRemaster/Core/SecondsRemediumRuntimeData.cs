using System;
using UnityEngine;

namespace RG.SecondsRemaster.Core
{
	// Token: 0x0200024F RID: 591
	[Serializable]
	public class SecondsRemediumRuntimeData
	{
		// Token: 0x17000445 RID: 1093
		// (get) Token: 0x06001631 RID: 5681 RVA: 0x00061072 File Offset: 0x0005F272
		// (set) Token: 0x06001632 RID: 5682 RVA: 0x0006107A File Offset: 0x0005F27A
		public bool IsDamaged
		{
			get
			{
				return this._isDamaged;
			}
			set
			{
				this._isDamaged = value;
			}
		}

		// Token: 0x04000EDC RID: 3804
		[SerializeField]
		private bool _isDamaged;
	}
}

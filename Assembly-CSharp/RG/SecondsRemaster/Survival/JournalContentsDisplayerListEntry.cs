using System;
using UnityEngine;

namespace RG.SecondsRemaster.Survival
{
	// Token: 0x020002EA RID: 746
	[Serializable]
	public class JournalContentsDisplayerListEntry
	{
		// Token: 0x170004AF RID: 1199
		// (get) Token: 0x060019D2 RID: 6610 RVA: 0x0006FF2A File Offset: 0x0006E12A
		public EJournalContentType Type
		{
			get
			{
				return this._type;
			}
		}

		// Token: 0x170004B0 RID: 1200
		// (get) Token: 0x060019D3 RID: 6611 RVA: 0x0006FF32 File Offset: 0x0006E132
		public JournalContentDisplayer Displayer
		{
			get
			{
				return this._displayer;
			}
		}

		// Token: 0x040013BB RID: 5051
		[SerializeField]
		private EJournalContentType _type;

		// Token: 0x040013BC RID: 5052
		[SerializeField]
		private JournalContentDisplayer _displayer;
	}
}

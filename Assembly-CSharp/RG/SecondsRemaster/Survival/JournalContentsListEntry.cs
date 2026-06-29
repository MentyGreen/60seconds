using System;
using System.Collections.Generic;
using UnityEngine;

namespace RG.SecondsRemaster.Survival
{
	// Token: 0x020002E4 RID: 740
	[Serializable]
	public abstract class JournalContentsListEntry<TJournalContent, TContentDisplayer> where TJournalContent : JournalContent where TContentDisplayer : JournalContentDisplayer<TJournalContent>
	{
		// Token: 0x170004AB RID: 1195
		// (get) Token: 0x060019C0 RID: 6592 RVA: 0x0006FB80 File Offset: 0x0006DD80
		public List<TJournalContent> JournalContents
		{
			get
			{
				return this._journalContents;
			}
		}

		// Token: 0x060019C1 RID: 6593 RVA: 0x0006FB88 File Offset: 0x0006DD88
		public void SetContentData(TJournalContent data, TContentDisplayer displayer)
		{
			displayer.SetContentData(data);
		}

		// Token: 0x060019C2 RID: 6594 RVA: 0x0006FB9B File Offset: 0x0006DD9B
		public void AddContentToList(TJournalContent journalContent)
		{
			if (this._journalContents == null)
			{
				this._journalContents = new List<TJournalContent>();
			}
			this._journalContents.Add(journalContent);
		}

		// Token: 0x060019C3 RID: 6595 RVA: 0x0006FBBC File Offset: 0x0006DDBC
		public void ClearJournalContents()
		{
			if (this._journalContents == null)
			{
				this._journalContents = new List<TJournalContent>();
				return;
			}
			this._journalContents.Clear();
		}

		// Token: 0x040013AD RID: 5037
		[SerializeField]
		private List<TJournalContent> _journalContents = new List<TJournalContent>();
	}
}

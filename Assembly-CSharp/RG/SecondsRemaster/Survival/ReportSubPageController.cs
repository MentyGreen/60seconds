using System;
using UnityEngine;

namespace RG.SecondsRemaster.Survival
{
	// Token: 0x02000331 RID: 817
	public class ReportSubPageController : SubPageController
	{
		// Token: 0x06001B5D RID: 7005 RVA: 0x0007603C File Offset: 0x0007423C
		public void SetDoodlesHolder(GameObject doodlesHolder)
		{
			this._doodlesHolder = doodlesHolder;
		}

		// Token: 0x06001B5E RID: 7006 RVA: 0x00076045 File Offset: 0x00074245
		public override void Show()
		{
			base.Show();
			if (this._doodlesHolder != null)
			{
				this._doodlesHolder.SetActive(true);
			}
		}

		// Token: 0x06001B5F RID: 7007 RVA: 0x00076067 File Offset: 0x00074267
		public override void Hide()
		{
			base.Hide();
			if (this._doodlesHolder != null)
			{
				this._doodlesHolder.SetActive(false);
			}
		}

		// Token: 0x06001B60 RID: 7008 RVA: 0x00076089 File Offset: 0x00074289
		public override bool CanBeDisplayed()
		{
			return true;
		}

		// Token: 0x06001B61 RID: 7009 RVA: 0x0007608C File Offset: 0x0007428C
		private void FixLinkedTextsInDisplayer()
		{
			foreach (TextJournalContentDisplayer textJournalContentDisplayer in base.GetComponentsInChildren<TextJournalContentDisplayer>())
			{
				if (textJournalContentDisplayer != null)
				{
					textJournalContentDisplayer.TryToFixLinkedText();
				}
			}
		}

		// Token: 0x04001527 RID: 5415
		[SerializeField]
		private GameObject _doodlesHolder;
	}
}

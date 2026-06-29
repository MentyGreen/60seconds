using System;
using RG.Parsecs.EventEditor;
using UnityEngine;

namespace RG.SecondsRemaster.Survival
{
	// Token: 0x0200032E RID: 814
	public class RationPageController : PageController
	{
		// Token: 0x06001B48 RID: 6984 RVA: 0x00075AC8 File Offset: 0x00073CC8
		public override void SetPageData(bool visible)
		{
			if (visible && this._attentionVariable != null && this._attentionVariable.Value)
			{
				this._attentionVariable.Value = false;
			}
			for (int i = 0; i < this._heads.Length; i++)
			{
				this._heads[i].UpdateMedkitScratch();
			}
			if (!base.CanRefreshPageToday())
			{
				return;
			}
			for (int j = 0; j < this._heads.Length; j++)
			{
				this._heads[j].ResetHead();
				this._heads[j].UpdateHeadVisibility();
				this._heads[j].SetScratchVisibility();
			}
			for (int k = 0; k < this._rationAllControllers.Length; k++)
			{
				this._rationAllControllers[k].UpdateFill();
			}
			base.SetPageNotRefreshableToday();
		}

		// Token: 0x06001B49 RID: 6985 RVA: 0x00075B88 File Offset: 0x00073D88
		public override void InitializePage()
		{
			base.InitializePage();
			if (base.IsEnabled())
			{
				this._attentionVariable.Value = true;
			}
		}

		// Token: 0x06001B4A RID: 6986 RVA: 0x00075BA4 File Offset: 0x00073DA4
		private void OnEnable()
		{
			for (int i = 0; i < this._rationAllControllers.Length; i++)
			{
				this._rationAllControllers[i].UpdateFill();
			}
		}

		// Token: 0x04001510 RID: 5392
		[SerializeField]
		private JournalRationingHeadController[] _heads;

		// Token: 0x04001511 RID: 5393
		[SerializeField]
		private RationAllController[] _rationAllControllers;

		// Token: 0x04001512 RID: 5394
		[SerializeField]
		private GlobalBoolVariable _attentionVariable;
	}
}

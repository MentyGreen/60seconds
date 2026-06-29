using System;
using UnityEngine;

namespace RG.SecondsRemaster.Survival
{
	// Token: 0x0200031B RID: 795
	public class SubPageController : PageController
	{
		// Token: 0x170004D9 RID: 1241
		// (get) Token: 0x06001AEC RID: 6892 RVA: 0x000744A0 File Offset: 0x000726A0
		// (set) Token: 0x06001AED RID: 6893 RVA: 0x000744A8 File Offset: 0x000726A8
		public override PageController ParentPage
		{
			get
			{
				return this._parentPage;
			}
			set
			{
				this._parentPage = value;
			}
		}

		// Token: 0x06001AEE RID: 6894 RVA: 0x000744B1 File Offset: 0x000726B1
		public override bool IsSubpage()
		{
			return true;
		}

		// Token: 0x06001AEF RID: 6895 RVA: 0x000744B4 File Offset: 0x000726B4
		public override void Show()
		{
			if (this.ParentPage != null)
			{
				this.ParentPage.Show();
			}
			base.Show();
		}

		// Token: 0x06001AF0 RID: 6896 RVA: 0x000744D5 File Offset: 0x000726D5
		public override void Hide()
		{
			if (this.ParentPage != null)
			{
				this.ParentPage.Hide();
			}
			base.Hide();
		}

		// Token: 0x040014AA RID: 5290
		[SerializeField]
		private PageController _parentPage;
	}
}

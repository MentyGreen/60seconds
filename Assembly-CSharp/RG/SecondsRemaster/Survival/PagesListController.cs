using System;
using System.Collections.Generic;
using UnityEngine;

namespace RG.SecondsRemaster.Survival
{
	// Token: 0x0200031A RID: 794
	public class PagesListController : MonoBehaviour
	{
		// Token: 0x170004D8 RID: 1240
		// (get) Token: 0x06001AE9 RID: 6889 RVA: 0x0007444A File Offset: 0x0007264A
		public List<PageController> Pages
		{
			get
			{
				return this._pages;
			}
		}

		// Token: 0x06001AEA RID: 6890 RVA: 0x00074454 File Offset: 0x00072654
		public void ClearPages()
		{
			for (int i = 0; i < this._pages.Count; i++)
			{
				Object.Destroy(this._pages[i].gameObject);
			}
			this._pages.Clear();
		}

		// Token: 0x040014A9 RID: 5289
		[SerializeField]
		private List<PageController> _pages;
	}
}

using System;
using System.Collections.Generic;
using RG.Core.Base;
using UnityEngine;

namespace RG.SecondsRemaster.Survival
{
	// Token: 0x020002E9 RID: 745
	[CreateAssetMenu(menuName = "60 Seconds Remaster!/Events Renderer/New Journal Contents Displayers List", fileName = "Journal_Contents_Displayers")]
	public class JournalContentsDisplayerList : RGScriptableObject
	{
		// Token: 0x060019D0 RID: 6608 RVA: 0x0006FED8 File Offset: 0x0006E0D8
		public JournalContentDisplayer GetContentDisplayer(EJournalContentType contentType)
		{
			for (int i = 0; i < this._contentsDisplayers.Count; i++)
			{
				if (this._contentsDisplayers[i].Type == contentType)
				{
					return this._contentsDisplayers[i].Displayer;
				}
			}
			return null;
		}

		// Token: 0x040013BA RID: 5050
		[SerializeField]
		private List<JournalContentsDisplayerListEntry> _contentsDisplayers;
	}
}

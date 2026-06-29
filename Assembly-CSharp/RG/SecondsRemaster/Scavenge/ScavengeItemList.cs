using System;
using System.Collections.Generic;
using RG.Core.Base;
using UnityEngine;

namespace RG.SecondsRemaster.Scavenge
{
	// Token: 0x020002CA RID: 714
	[CreateAssetMenu(menuName = "60 Seconds Remaster!/Scavenge/Scavenge Item List")]
	public class ScavengeItemList : RGScriptableObject
	{
		// Token: 0x17000496 RID: 1174
		// (get) Token: 0x06001930 RID: 6448 RVA: 0x0006DB59 File Offset: 0x0006BD59
		public List<ScavengeItem> Items
		{
			get
			{
				return this._items;
			}
		}

		// Token: 0x040012F8 RID: 4856
		[SerializeField]
		private List<ScavengeItem> _items;
	}
}

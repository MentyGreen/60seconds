using System;
using UnityEngine;
using UnityEngine.UI;

namespace RG.SecondsRemaster.Menu
{
	// Token: 0x02000293 RID: 659
	[Serializable]
	public class IconList
	{
		// Token: 0x1700047C RID: 1148
		// (get) Token: 0x06001824 RID: 6180 RVA: 0x00069D3C File Offset: 0x00067F3C
		public Image[] Icons
		{
			get
			{
				return this._icons;
			}
		}

		// Token: 0x040011CA RID: 4554
		[SerializeField]
		private Image[] _icons;
	}
}

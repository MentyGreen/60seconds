using System;
using System.Collections.Generic;
using RG.Core.Base;
using UnityEngine;

namespace RG.Remaster.Survival
{
	// Token: 0x0200022D RID: 557
	[CreateAssetMenu(menuName = "60 Seconds Remaster!/Characters/HatDataList", fileName = "New HatDataList")]
	[Serializable]
	public class HatDataList : RGScriptableObject
	{
		// Token: 0x17000415 RID: 1045
		// (get) Token: 0x06001584 RID: 5508 RVA: 0x0005F2D5 File Offset: 0x0005D4D5
		public List<HatData> HatData
		{
			get
			{
				return this._hatData;
			}
		}

		// Token: 0x04000E6A RID: 3690
		[SerializeField]
		private List<HatData> _hatData = new List<HatData>();
	}
}

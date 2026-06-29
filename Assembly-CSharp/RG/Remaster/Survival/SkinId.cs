using System;
using RG.Core.Base;
using UnityEngine;

namespace RG.Remaster.Survival
{
	// Token: 0x02000239 RID: 569
	[CreateAssetMenu(menuName = "60 Seconds Remaster!/Skin Id", fileName = "New SkinId")]
	[Serializable]
	public class SkinId : RGScriptableObject
	{
		// Token: 0x17000420 RID: 1056
		// (get) Token: 0x060015C3 RID: 5571 RVA: 0x00060765 File Offset: 0x0005E965
		public string Id
		{
			get
			{
				return this.Guid;
			}
		}
	}
}

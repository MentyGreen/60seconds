using System;
using System.Collections.Generic;
using RG.Core.Base;
using UnityEngine;

namespace RG.Remaster.Survival
{
	// Token: 0x02000237 RID: 567
	[CreateAssetMenu(menuName = "60 Seconds Remaster!/SkinDataList", fileName = "New SkinDataList")]
	[Serializable]
	public class SkinDataList : RGScriptableObject
	{
		// Token: 0x1700041F RID: 1055
		// (get) Token: 0x060015BF RID: 5567 RVA: 0x0006073F File Offset: 0x0005E93F
		public List<SkinData> SkinData
		{
			get
			{
				return this._skinData;
			}
		}

		// Token: 0x060015C0 RID: 5568 RVA: 0x00060747 File Offset: 0x0005E947
		public bool IsValid()
		{
			return true;
		}

		// Token: 0x04000EA5 RID: 3749
		[SerializeField]
		private List<SkinData> _skinData = new List<SkinData>();
	}
}

using System;
using System.Collections.Generic;
using RG.Core.Base;
using UnityEngine;

namespace RG.SecondsRemaster
{
	// Token: 0x02000246 RID: 582
	[CreateAssetMenu(menuName = "60 Seconds Remaster!/New Difficulty Level List", fileName = "New Difficulty Level List")]
	public class DifficultyLevelList : RGScriptableObject
	{
		// Token: 0x1700043E RID: 1086
		// (get) Token: 0x0600160F RID: 5647 RVA: 0x00060D3C File Offset: 0x0005EF3C
		public List<DifficultyLevel> Levels
		{
			get
			{
				return this._levels;
			}
		}

		// Token: 0x04000ECA RID: 3786
		[SerializeField]
		private List<DifficultyLevel> _levels;
	}
}

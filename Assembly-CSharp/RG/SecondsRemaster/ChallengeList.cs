using System;
using System.Collections.Generic;
using RG.Core.Base;
using UnityEngine;

namespace RG.SecondsRemaster
{
	// Token: 0x02000241 RID: 577
	[CreateAssetMenu(menuName = "60 Seconds Remaster!/Challenges/New Challenge List", fileName = "New Challenge List")]
	public class ChallengeList : RGScriptableObject
	{
		// Token: 0x17000435 RID: 1077
		// (get) Token: 0x060015F9 RID: 5625 RVA: 0x00060BEA File Offset: 0x0005EDEA
		public List<Challenge> Challenges
		{
			get
			{
				return this._challenges;
			}
		}

		// Token: 0x04000EC0 RID: 3776
		[SerializeField]
		private List<Challenge> _challenges;
	}
}

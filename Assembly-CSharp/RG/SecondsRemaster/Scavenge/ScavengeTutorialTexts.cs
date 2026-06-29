using System;
using System.Collections.Generic;
using RG.Core.Base;
using UnityEngine;

namespace RG.SecondsRemaster.Scavenge
{
	// Token: 0x020002CC RID: 716
	[CreateAssetMenu(fileName = "New Scavenge Tutorial Texts", menuName = "60 Seconds Remaster!/Scavenge/New Scavenge Tutorial Texts")]
	public class ScavengeTutorialTexts : RGScriptableObject
	{
		// Token: 0x06001936 RID: 6454 RVA: 0x0006DBA0 File Offset: 0x0006BDA0
		public ScavengeTutorialState GetTexts(ScavengeTutorialDriver.ETutorialStage state)
		{
			for (int i = 0; i < this._states.Count; i++)
			{
				if (this._states[i].State == state)
				{
					return this._states[i];
				}
			}
			return null;
		}

		// Token: 0x040012FB RID: 4859
		[SerializeField]
		private List<ScavengeTutorialState> _states;
	}
}

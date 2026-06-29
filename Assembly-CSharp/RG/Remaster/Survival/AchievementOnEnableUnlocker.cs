using System;
using RG.Parsecs.Common;
using UnityEngine;

namespace RG.Remaster.Survival
{
	// Token: 0x0200023B RID: 571
	public class AchievementOnEnableUnlocker : MonoBehaviour
	{
		// Token: 0x060015CD RID: 5581 RVA: 0x000608E2 File Offset: 0x0005EAE2
		private void OnEnable()
		{
			if (!AchievementsSystem.IsAchievementUnlocked(this._achievement))
			{
				AchievementsSystem.UnlockAchievement(this._achievement);
			}
		}

		// Token: 0x04000EAB RID: 3755
		[SerializeField]
		private Achievement _achievement;
	}
}

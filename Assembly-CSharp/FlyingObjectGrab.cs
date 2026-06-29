using System;
using FMODUnity;
using RG.Parsecs.Common;
using RG.VirtualInput;
using UnityEngine;

// Token: 0x02000153 RID: 339
public class FlyingObjectGrab : MonoBehaviour
{
	// Token: 0x06000FDF RID: 4063 RVA: 0x00041AB4 File Offset: 0x0003FCB4
	private void OnMouseDown()
	{
		if (!Singleton<VirtualInputManager>.Instance.IsPointerOverGameObject())
		{
			if (this._achievementToBeUnlocked != null && !AchievementsSystem.IsAchievementUnlocked(this._achievementToBeUnlocked))
			{
				AchievementsSystem.UnlockAchievement(this._achievementToBeUnlocked);
			}
			AudioManager.PlaySound(this._grabSound, 1f, 1f, 0f);
			base.gameObject.SetActive(false);
		}
	}

	// Token: 0x040009D3 RID: 2515
	[EventRef]
	[SerializeField]
	private string _grabSound;

	// Token: 0x040009D4 RID: 2516
	[SerializeField]
	private Achievement _achievementToBeUnlocked;
}

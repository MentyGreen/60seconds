using System;
using UnityEngine;
using UnityEngine.UI;

namespace RG.SecondsRemaster.Menu
{
	// Token: 0x02000294 RID: 660
	public class ChallengeSlotController : MonoBehaviour
	{
		// Token: 0x1700047D RID: 1149
		// (get) Token: 0x06001826 RID: 6182 RVA: 0x00069D4C File Offset: 0x00067F4C
		// (set) Token: 0x06001827 RID: 6183 RVA: 0x00069D54 File Offset: 0x00067F54
		public Challenge Challenge { get; private set; }

		// Token: 0x06001828 RID: 6184 RVA: 0x00069D60 File Offset: 0x00067F60
		public void SetChallengeData(Challenge challenge)
		{
			this.Challenge = challenge;
			for (int i = 0; i < this._missionImages.Length; i++)
			{
				this._missionImages[i].sprite = challenge.ChallengeGraphic;
			}
			this._challengeCompleted.SetActive(challenge.IsUnlocked);
		}

		// Token: 0x040011CB RID: 4555
		[SerializeField]
		private Image[] _missionImages;

		// Token: 0x040011CC RID: 4556
		[SerializeField]
		private GameObject _challengeCompleted;
	}
}

using System;
using FMODUnity;
using RG.Parsecs.Common;
using UnityEngine;

namespace RG.SecondsRemaster.Survival
{
	// Token: 0x02000338 RID: 824
	public class SurvivalMusicController : MonoBehaviour
	{
		// Token: 0x06001B84 RID: 7044 RVA: 0x000768EB File Offset: 0x00074AEB
		private void Start()
		{
			AudioManager.Instance.PlayMusicFadeOut(this._musicEvent, 1f, 1f, 0.5f);
		}

		// Token: 0x04001545 RID: 5445
		[EventRef]
		[SerializeField]
		private string _musicEvent;
	}
}

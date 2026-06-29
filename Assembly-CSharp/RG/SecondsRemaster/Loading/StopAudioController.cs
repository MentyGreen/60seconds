using System;
using RG.Parsecs.Common;
using UnityEngine;

namespace RG.SecondsRemaster.Loading
{
	// Token: 0x020002C1 RID: 705
	public class StopAudioController : MonoBehaviour
	{
		// Token: 0x060018F1 RID: 6385 RVA: 0x0006D070 File Offset: 0x0006B270
		private void Start()
		{
			AudioManager.Instance.StopPlayingMusicFadeOut();
			AudioManager.Instance.StopPlayingSfxFadeOut();
			AudioManager.Instance.StopPlayingUiFadeOut();
		}
	}
}

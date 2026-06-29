using System;
using RG.Parsecs.Common;
using RG.VirtualInput;
using UnityEngine;

namespace RG.Remaster.Scavenge
{
	// Token: 0x0200023E RID: 574
	public class RestartPanelController : MonoBehaviour
	{
		// Token: 0x060015D8 RID: 5592 RVA: 0x00060A1C File Offset: 0x0005EC1C
		public void Start()
		{
			Singleton<VirtualInputManager>.Instance.VisualManager.MouseCursorVisible = true;
		}

		// Token: 0x060015D9 RID: 5593 RVA: 0x00060A2E File Offset: 0x0005EC2E
		public void Restart()
		{
			AudioManager.Instance.StopPlayingMusicFadeOut();
			AudioManager.Instance.StopPlayingSfxFadeOut();
			ResetGame.RestartLevel();
		}

		// Token: 0x060015DA RID: 5594 RVA: 0x00060A49 File Offset: 0x0005EC49
		public void Exit()
		{
			AudioManager.Instance.StopPlayingMusicFadeOut();
			AudioManager.Instance.StopPlayingSfxFadeOut();
			Singleton<GameManager>.Instance.ResetGame();
		}
	}
}

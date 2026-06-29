using System;
using RG.Parsecs.Common;
using Steamworks;
using UnityEngine;

// Token: 0x02000151 RID: 337
public class DemoSurvivalBlockedPopupController : MonoBehaviour
{
	// Token: 0x06000FD5 RID: 4053 RVA: 0x0004193F File Offset: 0x0003FB3F
	public void OnEnable()
	{
		this._survivalPopupAnimator.SetTrigger("ShowPopup");
	}

	// Token: 0x06000FD6 RID: 4054 RVA: 0x00041951 File Offset: 0x0003FB51
	public void ClickedGoBackToMenu()
	{
		AudioManager.Instance.StopPlayingMusicFadeOut();
		AudioManager.Instance.StopPlayingSfxFadeOut();
		Singleton<GameManager>.Instance.ResetGame();
	}

	// Token: 0x06000FD7 RID: 4055 RVA: 0x00041971 File Offset: 0x0003FB71
	public void ClickedGoToStore()
	{
		if (SteamManager.Initialized && SteamAPI.IsSteamRunning())
		{
			SteamFriends.ActivateGameOverlayToStore(new AppId_t(DemoManager.REATOMIZED_FULL_VERSION_APP_ID), EOverlayToStoreFlag.k_EOverlayToStoreFlag_None);
			return;
		}
		Application.OpenURL("https://store.steampowered.com/app/1012880/60_Seconds_Reatomized/");
	}

	// Token: 0x040009CC RID: 2508
	private const string SHOW_POPUP_TRIGGER_NAME = "ShowPopup";

	// Token: 0x040009CD RID: 2509
	[SerializeField]
	private Animator _survivalPopupAnimator;
}

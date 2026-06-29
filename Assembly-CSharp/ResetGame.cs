using System;
using RG.Parsecs.Common;

// Token: 0x020000FE RID: 254
public class ResetGame
{
	// Token: 0x06000C61 RID: 3169 RVA: 0x00036380 File Offset: 0x00034580
	public static void RestartLevel()
	{
		if (GameSessionData.Instance.Setup.IsScavengeGame())
		{
			if (GameSessionData.Instance.Setup.IsChallengeGame() || GameSessionData.Instance.Setup.IsTutorialGame())
			{
				ResetGame.GameLevel = GameSessionData.Instance.Setup.ForcedLevelStem;
			}
			else if (DemoManager.IS_DEMO_VERSION)
			{
				ResetGame.GameLevel = "level_scavenge_11";
			}
			else
			{
				ResetGame.GameLevel = GameSessionData.Instance.Setup.GetRandomScavengeLevelName();
			}
			if (ResetGame.GameLevel != null)
			{
				ResetGame.DoRestart();
				return;
			}
		}
		else
		{
			ResetGame.GameLevel = GameSessionData.Instance.Setup.ForcedLevelStem;
		}
	}

	// Token: 0x06000C62 RID: 3170 RVA: 0x0003641E File Offset: 0x0003461E
	private static void DoRestart()
	{
		AudioManager.Instance.StopPlayingSfxFadeOut();
		if (ScavengeDataLogger.Instance != null)
		{
			ScavengeDataLogger.Instance.EndLog(false, false, false);
		}
		Singleton<GameManager>.Instance.ScavangeSceneName = ResetGame.GameLevel;
		Singleton<GameManager>.Instance.StartScavenge();
	}

	// Token: 0x040006A1 RID: 1697
	private static string GameLevel;
}

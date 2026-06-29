using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Token: 0x0200010E RID: 270
[Serializable]
public class GameSetup : ScriptableObject
{
	// Token: 0x06000D20 RID: 3360 RVA: 0x000373F4 File Offset: 0x000355F4
	public GameSetup()
	{
		this.LevelItems = this.levelItems;
		this.CollectItems = this.collectItems;
	}

	// Token: 0x06000D21 RID: 3361 RVA: 0x000374C8 File Offset: 0x000356C8
	public static GameSetup[] LoadGameSetup(EGameType gameType)
	{
		switch (gameType)
		{
		case EGameType.TUTORIAL:
			return Resources.LoadAll<GameSetup>("Setups/Tutorial");
		case EGameType.FULL:
			return Resources.LoadAll<GameSetup>("Setups/Full");
		case EGameType.SCAVENGE:
			return Resources.LoadAll<GameSetup>("Setups/Scavenge");
		case EGameType.SURVIVAL:
			return Resources.LoadAll<GameSetup>("Setups/Survival");
		case EGameType.CHALLENGE_SCAVENGE:
			return Resources.LoadAll<GameSetup>("DLC/Setups/Challenge/Scavenge");
		case EGameType.CHALLENGE_SURVIVAL:
			return Resources.LoadAll<GameSetup>("DLC/Setups/Challenge/Survival");
		case EGameType.SCENARIO:
			return Resources.LoadAll<GameSetup>("DLC/Setups/Scenario");
		default:
			return null;
		}
	}

	// Token: 0x06000D22 RID: 3362 RVA: 0x00037549 File Offset: 0x00035749
	public bool IsScavengeGame()
	{
		return this._gameType == EGameType.SCAVENGE || this._gameType == EGameType.FULL || this._gameType == EGameType.CHALLENGE_SCAVENGE || this.IsTutorialGame();
	}

	// Token: 0x06000D23 RID: 3363 RVA: 0x0003756E File Offset: 0x0003576E
	public bool IsTutorialGame()
	{
		return this._gameType == EGameType.TUTORIAL;
	}

	// Token: 0x06000D24 RID: 3364 RVA: 0x00037579 File Offset: 0x00035779
	public bool IsSurvivalGame()
	{
		return this._gameType == EGameType.FULL || this._gameType == EGameType.SURVIVAL || this._gameType == EGameType.CHALLENGE_SURVIVAL || this._gameType == EGameType.SCENARIO;
	}

	// Token: 0x06000D25 RID: 3365 RVA: 0x000375A1 File Offset: 0x000357A1
	public bool IsChallengeGame()
	{
		return this._gameType == EGameType.CHALLENGE_SCAVENGE || this._gameType == EGameType.CHALLENGE_SURVIVAL;
	}

	// Token: 0x06000D26 RID: 3366 RVA: 0x000375B8 File Offset: 0x000357B8
	public void SetItemsForCharacter(ECharacter character)
	{
		this.LevelItems = (from x in this.levelItems
		where x.Character != character
		select x).ToList<ScavengeItemController>();
		this.CollectItems = (from x in this.collectItems
		where x.Character != character
		select x).ToList<ScavengeItemController>();
	}

	// Token: 0x06000D27 RID: 3367 RVA: 0x00037618 File Offset: 0x00035818
	public string GetRandomScavengeLevelName()
	{
		if (!string.IsNullOrEmpty(this._forcedLevelStem) && this._forcedLevelMin < this._forcedLevelMax)
		{
			int num = Random.Range(this._forcedLevelMin, this._forcedLevelMax + 1);
			return this._forcedLevelStem + num.ToString();
		}
		return null;
	}

	// Token: 0x06000D28 RID: 3368 RVA: 0x00037668 File Offset: 0x00035868
	public bool AreSpecificItemsToBeCollected()
	{
		return this._collectItems.Count > 0;
	}

	// Token: 0x06000D29 RID: 3369 RVA: 0x00037678 File Offset: 0x00035878
	public bool IsThereExtraInfo()
	{
		return this._setupExtraInfo.Count > 0;
	}

	// Token: 0x06000D2A RID: 3370 RVA: 0x00037688 File Offset: 0x00035888
	public bool DoesUnlockAchievement()
	{
		return !string.IsNullOrEmpty(this._achievementId);
	}

	// Token: 0x06000D2B RID: 3371 RVA: 0x00037698 File Offset: 0x00035898
	public bool DoesUnlockRewards()
	{
		return !string.IsNullOrEmpty(this._rewardId);
	}

	// Token: 0x170002BF RID: 703
	// (get) Token: 0x06000D2C RID: 3372 RVA: 0x000376A8 File Offset: 0x000358A8
	// (set) Token: 0x06000D2D RID: 3373 RVA: 0x000376B0 File Offset: 0x000358B0
	public SLevelData LevelData
	{
		get
		{
			return this._levelData;
		}
		set
		{
			this._levelData = value;
		}
	}

	// Token: 0x170002C0 RID: 704
	// (get) Token: 0x06000D2E RID: 3374 RVA: 0x000376B9 File Offset: 0x000358B9
	// (set) Token: 0x06000D2F RID: 3375 RVA: 0x000376C1 File Offset: 0x000358C1
	public List<ScavengeItemController> LevelItems { get; private set; }

	// Token: 0x170002C1 RID: 705
	// (get) Token: 0x06000D30 RID: 3376 RVA: 0x000376CA File Offset: 0x000358CA
	// (set) Token: 0x06000D31 RID: 3377 RVA: 0x000376D2 File Offset: 0x000358D2
	public List<ScavengeItemController> CollectItems { get; private set; }

	// Token: 0x170002C2 RID: 706
	// (get) Token: 0x06000D32 RID: 3378 RVA: 0x000376DB File Offset: 0x000358DB
	public List<string> SetupExtraInfo
	{
		get
		{
			return this._setupExtraInfo;
		}
	}

	// Token: 0x170002C3 RID: 707
	// (get) Token: 0x06000D33 RID: 3379 RVA: 0x000376E3 File Offset: 0x000358E3
	// (set) Token: 0x06000D34 RID: 3380 RVA: 0x000376EB File Offset: 0x000358EB
	public EGameType GameType
	{
		get
		{
			return this._gameType;
		}
		set
		{
			this._gameType = value;
		}
	}

	// Token: 0x170002C4 RID: 708
	// (get) Token: 0x06000D35 RID: 3381 RVA: 0x000376F4 File Offset: 0x000358F4
	// (set) Token: 0x06000D36 RID: 3382 RVA: 0x000376FC File Offset: 0x000358FC
	public int GameTime
	{
		get
		{
			return this._gameTime;
		}
		set
		{
			this._gameTime = value;
		}
	}

	// Token: 0x170002C5 RID: 709
	// (get) Token: 0x06000D37 RID: 3383 RVA: 0x00037705 File Offset: 0x00035905
	// (set) Token: 0x06000D38 RID: 3384 RVA: 0x0003770D File Offset: 0x0003590D
	public int PrepareTime
	{
		get
		{
			return this._prepareTime;
		}
		set
		{
			this._prepareTime = value;
		}
	}

	// Token: 0x170002C6 RID: 710
	// (get) Token: 0x06000D39 RID: 3385 RVA: 0x00037716 File Offset: 0x00035916
	// (set) Token: 0x06000D3A RID: 3386 RVA: 0x0003771E File Offset: 0x0003591E
	public string SetupName
	{
		get
		{
			return this._setupName;
		}
		set
		{
			this._setupName = value;
		}
	}

	// Token: 0x170002C7 RID: 711
	// (get) Token: 0x06000D3B RID: 3387 RVA: 0x00037727 File Offset: 0x00035927
	// (set) Token: 0x06000D3C RID: 3388 RVA: 0x0003772F File Offset: 0x0003592F
	public string SetupIcon
	{
		get
		{
			return this._setupIcon;
		}
		set
		{
			this._setupIcon = value;
		}
	}

	// Token: 0x170002C8 RID: 712
	// (get) Token: 0x06000D3D RID: 3389 RVA: 0x00037738 File Offset: 0x00035938
	// (set) Token: 0x06000D3E RID: 3390 RVA: 0x00037740 File Offset: 0x00035940
	public string SetupDescription
	{
		get
		{
			return this._setupDescription;
		}
		set
		{
			this._setupDescription = value;
		}
	}

	// Token: 0x170002C9 RID: 713
	// (get) Token: 0x06000D3F RID: 3391 RVA: 0x00037749 File Offset: 0x00035949
	// (set) Token: 0x06000D40 RID: 3392 RVA: 0x00037751 File Offset: 0x00035951
	public int ComfortZoneTimeout
	{
		get
		{
			return this._comfortZoneTimeout;
		}
		set
		{
			this._comfortZoneTimeout = value;
		}
	}

	// Token: 0x170002CA RID: 714
	// (get) Token: 0x06000D41 RID: 3393 RVA: 0x0003775A File Offset: 0x0003595A
	// (set) Token: 0x06000D42 RID: 3394 RVA: 0x00037762 File Offset: 0x00035962
	public int CautionZoneTimeout
	{
		get
		{
			return this._cautionZoneTimeout;
		}
		set
		{
			this._cautionZoneTimeout = value;
		}
	}

	// Token: 0x170002CB RID: 715
	// (get) Token: 0x06000D43 RID: 3395 RVA: 0x0003776B File Offset: 0x0003596B
	public SDifficultyCollection Difficulties
	{
		get
		{
			return this._difficulties;
		}
	}

	// Token: 0x170002CC RID: 716
	// (get) Token: 0x06000D44 RID: 3396 RVA: 0x00037773 File Offset: 0x00035973
	public string ForcedLevelStem
	{
		get
		{
			return this._forcedLevelStem;
		}
	}

	// Token: 0x170002CD RID: 717
	// (get) Token: 0x06000D45 RID: 3397 RVA: 0x0003777B File Offset: 0x0003597B
	public int ForcedLevelMax
	{
		get
		{
			return this._forcedLevelMax;
		}
	}

	// Token: 0x170002CE RID: 718
	// (get) Token: 0x06000D46 RID: 3398 RVA: 0x00037783 File Offset: 0x00035983
	public int ForcedLevelMin
	{
		get
		{
			return this._forcedLevelMin;
		}
	}

	// Token: 0x170002CF RID: 719
	// (get) Token: 0x06000D47 RID: 3399 RVA: 0x0003778B File Offset: 0x0003598B
	public string RewardId
	{
		get
		{
			return this._rewardId;
		}
	}

	// Token: 0x170002D0 RID: 720
	// (get) Token: 0x06000D48 RID: 3400 RVA: 0x00037793 File Offset: 0x00035993
	public string AchievementId
	{
		get
		{
			return this._achievementId;
		}
	}

	// Token: 0x170002D1 RID: 721
	// (get) Token: 0x06000D49 RID: 3401 RVA: 0x0003779B File Offset: 0x0003599B
	public bool Enabled
	{
		get
		{
			return this._enabled;
		}
	}

	// Token: 0x170002D2 RID: 722
	// (get) Token: 0x06000D4A RID: 3402 RVA: 0x000377A3 File Offset: 0x000359A3
	public int Version
	{
		get
		{
			return this._version;
		}
	}

	// Token: 0x04000723 RID: 1827
	[SerializeField]
	private bool _enabled = true;

	// Token: 0x04000724 RID: 1828
	[SerializeField]
	private int _version;

	// Token: 0x04000725 RID: 1829
	[SerializeField]
	private string _setupName = string.Empty;

	// Token: 0x04000726 RID: 1830
	[SerializeField]
	private string _setupDescription = string.Empty;

	// Token: 0x04000727 RID: 1831
	[SerializeField]
	private string _setupIcon = string.Empty;

	// Token: 0x04000728 RID: 1832
	[SerializeField]
	private string _rewardId = string.Empty;

	// Token: 0x04000729 RID: 1833
	[SerializeField]
	private string _achievementId = string.Empty;

	// Token: 0x0400072A RID: 1834
	[SerializeField]
	private EGameType _gameType;

	// Token: 0x0400072B RID: 1835
	[SerializeField]
	private int _prepareTime;

	// Token: 0x0400072C RID: 1836
	[SerializeField]
	private int _gameTime = 60;

	// Token: 0x0400072D RID: 1837
	[SerializeField]
	private int _comfortZoneTimeout = 20;

	// Token: 0x0400072E RID: 1838
	[SerializeField]
	private int _cautionZoneTimeout = 40;

	// Token: 0x0400072F RID: 1839
	[SerializeField]
	private string _forcedLevelStem = string.Empty;

	// Token: 0x04000730 RID: 1840
	[SerializeField]
	private int _forcedLevelMin = 1;

	// Token: 0x04000731 RID: 1841
	[SerializeField]
	private int _forcedLevelMax = 1;

	// Token: 0x04000732 RID: 1842
	[SerializeField]
	private SLevelData _levelData;

	// Token: 0x04000733 RID: 1843
	[SerializeField]
	private List<GameObject> _levelItems = new List<GameObject>();

	// Token: 0x04000734 RID: 1844
	[SerializeField]
	private List<ScavengeItemController> levelItems = new List<ScavengeItemController>();

	// Token: 0x04000735 RID: 1845
	[SerializeField]
	private List<GameObject> _collectItems = new List<GameObject>();

	// Token: 0x04000736 RID: 1846
	[SerializeField]
	private List<ScavengeItemController> collectItems = new List<ScavengeItemController>();

	// Token: 0x04000737 RID: 1847
	[SerializeField]
	private SDifficultyCollection _difficulties;

	// Token: 0x04000738 RID: 1848
	[SerializeField]
	private List<string> _setupExtraInfo = new List<string>();
}

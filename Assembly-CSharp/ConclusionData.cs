using System;
using System.Collections.Generic;

// Token: 0x02000108 RID: 264
public class ConclusionData
{
	// Token: 0x06000C7B RID: 3195 RVA: 0x00036750 File Offset: 0x00034950
	public void FillFinalConclusionData(bool controlCharsAlive, bool goalFailed, bool goalAchieved, bool absenceFail, EGameResolution resolution, int timesDefendedBunker)
	{
		this._controlCharsAlive = controlCharsAlive;
		this._goalFailed = goalFailed;
		this._goalAchieved = goalAchieved;
		this._absenceFail = absenceFail;
		this._resolution = resolution;
		this._timesDefendedBunker = timesDefendedBunker;
	}

	// Token: 0x06000C7C RID: 3196 RVA: 0x00036780 File Offset: 0x00034980
	public void ClearConclusionData()
	{
		this._survivedChars.Clear();
		this._controlCharsAlive = false;
		this._goalFailed = false;
		this._goalAchieved = false;
		this._resolution = EGameResolution.NONE;
		this._absenceFail = false;
		this._timesDefendedBunker = 0;
		this._waterConsumed = 0f;
		this._foodConsumed = 0f;
		this._daysSurvived = 0;
		this._expeditionCount = 0;
		this._successfulExpeditions = 0;
		this._yesCount = 0;
		this._noCount = 0;
		this._itemsBroughtBack = 0;
		this._customConclusionText = string.Empty;
		this._customConclusionAudio = string.Empty;
		this._parameters.Clear();
	}

	// Token: 0x06000C7D RID: 3197 RVA: 0x00036823 File Offset: 0x00034A23
	public void AddSurvivingChar(string characterName)
	{
		if (!this._survivedChars.Contains(characterName))
		{
			this._survivedChars.Add(characterName);
		}
	}

	// Token: 0x06000C7E RID: 3198 RVA: 0x0003683F File Offset: 0x00034A3F
	public void AddParameter(string param)
	{
		if (!this._parameters.Contains(param))
		{
			this._parameters.Add(param);
		}
	}

	// Token: 0x06000C7F RID: 3199 RVA: 0x0003685B File Offset: 0x00034A5B
	public bool HasSurvived(string characterName)
	{
		return this._survivedChars.Contains(characterName);
	}

	// Token: 0x1700026F RID: 623
	// (get) Token: 0x06000C80 RID: 3200 RVA: 0x00036869 File Offset: 0x00034A69
	public bool ControlCharsAlive
	{
		get
		{
			return this._controlCharsAlive;
		}
	}

	// Token: 0x17000270 RID: 624
	// (get) Token: 0x06000C81 RID: 3201 RVA: 0x00036871 File Offset: 0x00034A71
	public bool GoalFailed
	{
		get
		{
			return this._goalFailed;
		}
	}

	// Token: 0x17000271 RID: 625
	// (get) Token: 0x06000C82 RID: 3202 RVA: 0x00036879 File Offset: 0x00034A79
	public bool GoalAchieved
	{
		get
		{
			return this._goalAchieved;
		}
	}

	// Token: 0x17000272 RID: 626
	// (get) Token: 0x06000C83 RID: 3203 RVA: 0x00036881 File Offset: 0x00034A81
	public bool AbsenceFail
	{
		get
		{
			return this._absenceFail;
		}
	}

	// Token: 0x17000273 RID: 627
	// (get) Token: 0x06000C84 RID: 3204 RVA: 0x00036889 File Offset: 0x00034A89
	public EGameResolution Resolution
	{
		get
		{
			return this._resolution;
		}
	}

	// Token: 0x17000274 RID: 628
	// (get) Token: 0x06000C85 RID: 3205 RVA: 0x00036891 File Offset: 0x00034A91
	public List<string> SurvivedChars
	{
		get
		{
			return this._survivedChars;
		}
	}

	// Token: 0x06000C86 RID: 3206 RVA: 0x00036899 File Offset: 0x00034A99
	public void AddWaterConsumed(float value = 1f)
	{
		this.WaterConsumed += value;
	}

	// Token: 0x06000C87 RID: 3207 RVA: 0x000368A9 File Offset: 0x00034AA9
	public void AddSoupConsumed(float value = 1f)
	{
		this.FoodConsumed += value;
	}

	// Token: 0x06000C88 RID: 3208 RVA: 0x000368B9 File Offset: 0x00034AB9
	public void AddDaysSurvived(int value = 1)
	{
		this.DaysSurvived += value;
	}

	// Token: 0x06000C89 RID: 3209 RVA: 0x000368C9 File Offset: 0x00034AC9
	public void AddExpeditionCount(int value = 1)
	{
		this.ExpeditionCount += value;
	}

	// Token: 0x06000C8A RID: 3210 RVA: 0x000368D9 File Offset: 0x00034AD9
	public void AddSuccessfulExpeditions(int value = 1)
	{
		this.SuccessfulExpeditions += value;
	}

	// Token: 0x06000C8B RID: 3211 RVA: 0x000368E9 File Offset: 0x00034AE9
	public void AddYesCount(int value = 1)
	{
		this.YesCount += value;
	}

	// Token: 0x06000C8C RID: 3212 RVA: 0x000368F9 File Offset: 0x00034AF9
	public void AddNoCount(int value = 1)
	{
		this.NoCount += value;
	}

	// Token: 0x06000C8D RID: 3213 RVA: 0x00036909 File Offset: 0x00034B09
	public void AddItemsBroughtBack(int value = 1)
	{
		this.ItemsBroughtBack += value;
	}

	// Token: 0x17000275 RID: 629
	// (get) Token: 0x06000C8E RID: 3214 RVA: 0x00036919 File Offset: 0x00034B19
	// (set) Token: 0x06000C8F RID: 3215 RVA: 0x00036921 File Offset: 0x00034B21
	public float WaterConsumed
	{
		get
		{
			return this._waterConsumed;
		}
		private set
		{
			this._waterConsumed = value;
		}
	}

	// Token: 0x17000276 RID: 630
	// (get) Token: 0x06000C90 RID: 3216 RVA: 0x0003692A File Offset: 0x00034B2A
	// (set) Token: 0x06000C91 RID: 3217 RVA: 0x00036932 File Offset: 0x00034B32
	public float FoodConsumed
	{
		get
		{
			return this._foodConsumed;
		}
		private set
		{
			this._foodConsumed = value;
		}
	}

	// Token: 0x17000277 RID: 631
	// (get) Token: 0x06000C92 RID: 3218 RVA: 0x0003693B File Offset: 0x00034B3B
	// (set) Token: 0x06000C93 RID: 3219 RVA: 0x00036943 File Offset: 0x00034B43
	public int DaysSurvived
	{
		get
		{
			return this._daysSurvived;
		}
		set
		{
			this._daysSurvived = value;
		}
	}

	// Token: 0x17000278 RID: 632
	// (get) Token: 0x06000C94 RID: 3220 RVA: 0x0003694C File Offset: 0x00034B4C
	public int TimesDefendedBunker
	{
		get
		{
			return this._timesDefendedBunker;
		}
	}

	// Token: 0x17000279 RID: 633
	// (get) Token: 0x06000C95 RID: 3221 RVA: 0x00036954 File Offset: 0x00034B54
	// (set) Token: 0x06000C96 RID: 3222 RVA: 0x0003695C File Offset: 0x00034B5C
	public int ExpeditionCount
	{
		get
		{
			return this._expeditionCount;
		}
		private set
		{
			this._expeditionCount = value;
		}
	}

	// Token: 0x1700027A RID: 634
	// (get) Token: 0x06000C97 RID: 3223 RVA: 0x00036965 File Offset: 0x00034B65
	// (set) Token: 0x06000C98 RID: 3224 RVA: 0x0003696D File Offset: 0x00034B6D
	public int SuccessfulExpeditions
	{
		get
		{
			return this._successfulExpeditions;
		}
		private set
		{
			this._successfulExpeditions = value;
		}
	}

	// Token: 0x1700027B RID: 635
	// (get) Token: 0x06000C99 RID: 3225 RVA: 0x00036976 File Offset: 0x00034B76
	// (set) Token: 0x06000C9A RID: 3226 RVA: 0x0003697E File Offset: 0x00034B7E
	public int YesCount
	{
		get
		{
			return this._yesCount;
		}
		private set
		{
			this._yesCount = value;
		}
	}

	// Token: 0x1700027C RID: 636
	// (get) Token: 0x06000C9B RID: 3227 RVA: 0x00036987 File Offset: 0x00034B87
	// (set) Token: 0x06000C9C RID: 3228 RVA: 0x0003698F File Offset: 0x00034B8F
	public int NoCount
	{
		get
		{
			return this._noCount;
		}
		private set
		{
			this._noCount = value;
		}
	}

	// Token: 0x1700027D RID: 637
	// (get) Token: 0x06000C9D RID: 3229 RVA: 0x00036998 File Offset: 0x00034B98
	// (set) Token: 0x06000C9E RID: 3230 RVA: 0x000369A0 File Offset: 0x00034BA0
	public int ItemsBroughtBack
	{
		get
		{
			return this._itemsBroughtBack;
		}
		private set
		{
			this._itemsBroughtBack = value;
		}
	}

	// Token: 0x1700027E RID: 638
	// (get) Token: 0x06000C9F RID: 3231 RVA: 0x000369A9 File Offset: 0x00034BA9
	// (set) Token: 0x06000CA0 RID: 3232 RVA: 0x000369B1 File Offset: 0x00034BB1
	public string CustomConclusionText
	{
		get
		{
			return this._customConclusionText;
		}
		set
		{
			this._customConclusionText = value;
		}
	}

	// Token: 0x1700027F RID: 639
	// (get) Token: 0x06000CA1 RID: 3233 RVA: 0x000369BA File Offset: 0x00034BBA
	// (set) Token: 0x06000CA2 RID: 3234 RVA: 0x000369C2 File Offset: 0x00034BC2
	public string CustomConclusionAudio
	{
		get
		{
			return this._customConclusionAudio;
		}
		set
		{
			this._customConclusionAudio = value;
		}
	}

	// Token: 0x17000280 RID: 640
	// (get) Token: 0x06000CA3 RID: 3235 RVA: 0x000369CB File Offset: 0x00034BCB
	public List<string> Parameters
	{
		get
		{
			return this._parameters;
		}
	}

	// Token: 0x040006D3 RID: 1747
	private List<string> _survivedChars = new List<string>();

	// Token: 0x040006D4 RID: 1748
	private List<string> _parameters = new List<string>();

	// Token: 0x040006D5 RID: 1749
	private bool _controlCharsAlive;

	// Token: 0x040006D6 RID: 1750
	private bool _goalFailed;

	// Token: 0x040006D7 RID: 1751
	private bool _goalAchieved;

	// Token: 0x040006D8 RID: 1752
	private EGameResolution _resolution;

	// Token: 0x040006D9 RID: 1753
	private bool _absenceFail;

	// Token: 0x040006DA RID: 1754
	private int _timesDefendedBunker;

	// Token: 0x040006DB RID: 1755
	private float _waterConsumed;

	// Token: 0x040006DC RID: 1756
	private float _foodConsumed;

	// Token: 0x040006DD RID: 1757
	private int _daysSurvived;

	// Token: 0x040006DE RID: 1758
	private int _expeditionCount;

	// Token: 0x040006DF RID: 1759
	private int _successfulExpeditions;

	// Token: 0x040006E0 RID: 1760
	private int _yesCount;

	// Token: 0x040006E1 RID: 1761
	private int _noCount;

	// Token: 0x040006E2 RID: 1762
	private int _itemsBroughtBack;

	// Token: 0x040006E3 RID: 1763
	private string _customConclusionText = string.Empty;

	// Token: 0x040006E4 RID: 1764
	private string _customConclusionAudio = string.Empty;
}

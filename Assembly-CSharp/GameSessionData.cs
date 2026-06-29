using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using RG.SecondsRemaster;
using UnityEngine;

// Token: 0x02000109 RID: 265
public class GameSessionData : MonoBehaviour
{
	// Token: 0x06000CA5 RID: 3237 RVA: 0x00036A08 File Offset: 0x00034C08
	private void Awake()
	{
		this._saveFilename = Application.persistentDataPath + "/LastGame.sav";
		this._conclusionData = new ConclusionData();
		if (GameSessionData.Instance != null)
		{
			Object.Destroy(base.gameObject);
		}
		else
		{
			GameSessionData.Instance = this;
			Object.DontDestroyOnLoad(this);
		}
		this.ValidateSaveFile(true, "/LastGame.sav");
	}

	// Token: 0x06000CA6 RID: 3238 RVA: 0x00036A68 File Offset: 0x00034C68
	private void Start()
	{
	}

	// Token: 0x06000CA7 RID: 3239 RVA: 0x00036A6C File Offset: 0x00034C6C
	public bool DeleteSavefile()
	{
		FileInfo fileInfo = new FileInfo(this._saveFilename);
		if (fileInfo.Exists)
		{
			fileInfo.Delete();
			return true;
		}
		return false;
	}

	// Token: 0x06000CA8 RID: 3240 RVA: 0x00036A98 File Offset: 0x00034C98
	public bool ValidateSaveFile(bool deleteOnError = true, string filename = "/LastGame.sav")
	{
		bool flag = true;
		if (!string.IsNullOrEmpty(filename))
		{
			string path = Application.persistentDataPath + filename;
			if (File.Exists(path))
			{
				BinaryFormatter binaryFormatter = new BinaryFormatter();
				FileStream fileStream = File.Open(path, FileMode.Open);
				int num = 0;
				try
				{
					num = (int)binaryFormatter.Deserialize(fileStream);
				}
				catch
				{
					flag = false;
				}
				finally
				{
					fileStream.Close();
				}
				if (num != Settings.SaveVersion)
				{
					flag = false;
				}
				if (!flag && deleteOnError)
				{
					this.DeleteSavefile();
				}
			}
		}
		return flag;
	}

	// Token: 0x06000CA9 RID: 3241 RVA: 0x00036B28 File Offset: 0x00034D28
	public void SetScavengeData(int prepareTime = 0, int gameTime = 0)
	{
		this._customGameTime = (prepareTime > 0 || gameTime > 0);
		this._prepareTime = prepareTime;
		this._gameTime = gameTime;
	}

	// Token: 0x06000CAA RID: 3242 RVA: 0x00036B4C File Offset: 0x00034D4C
	public int GetCollectedItemCount(string name)
	{
		int num = 0;
		for (int i = 0; i < this._collectedItems.Length; i++)
		{
			if (this._collectedItems[i] == name)
			{
				num++;
			}
		}
		return num;
	}

	// Token: 0x06000CAB RID: 3243 RVA: 0x00036B84 File Offset: 0x00034D84
	public GameObject GetPlayerTemplate()
	{
		if (this._forcedPlayerTemplate != null)
		{
			return this._forcedPlayerTemplate;
		}
		if (this._setup)
		{
			ECharacter character = this._character;
			if (character == ECharacter.DAD)
			{
				return this._setup.LevelData.PlayerTemplate;
			}
			if (character == ECharacter.MOM)
			{
				return this._setup.LevelData.DoloresTemplate;
			}
		}
		if (!(this._setup == null))
		{
			return this._setup.LevelData.PlayerTemplate;
		}
		return null;
	}

	// Token: 0x06000CAC RID: 3244 RVA: 0x00036C0F File Offset: 0x00034E0F
	public int GetGameTime()
	{
		if (this._customGameTime)
		{
			return this._gameTime;
		}
		return this._setup.GameTime;
	}

	// Token: 0x06000CAD RID: 3245 RVA: 0x00036C2B File Offset: 0x00034E2B
	public int GetPrepareTime()
	{
		if (this._customGameTime)
		{
			return this._prepareTime;
		}
		return this._setup.PrepareTime;
	}

	// Token: 0x06000CAE RID: 3246 RVA: 0x00036C47 File Offset: 0x00034E47
	public int GetComfortZoneTimeout()
	{
		if (this._customGameTime)
		{
			return this._comfortZoneTimeout;
		}
		return this._setup.ComfortZoneTimeout;
	}

	// Token: 0x06000CAF RID: 3247 RVA: 0x00036C63 File Offset: 0x00034E63
	public int GetCautionZoneTimeout()
	{
		if (this._customGameTime)
		{
			return this._cautionZoneTimeout;
		}
		return this._setup.CautionZoneTimeout;
	}

	// Token: 0x06000CB0 RID: 3248 RVA: 0x00036C80 File Offset: 0x00034E80
	public SDifficultyData GetCurrentDifficulty()
	{
		switch (this._difficulty)
		{
		case EGameDifficulty.EASY:
			return this._setup.Difficulties.Easy;
		case EGameDifficulty.NORMAL:
			return this._setup.Difficulties.Normal;
		case EGameDifficulty.HARD:
			return this._setup.Difficulties.Hard;
		default:
			return default(SDifficultyData);
		}
	}

	// Token: 0x17000281 RID: 641
	// (get) Token: 0x06000CB1 RID: 3249 RVA: 0x00036CEF File Offset: 0x00034EEF
	// (set) Token: 0x06000CB2 RID: 3250 RVA: 0x00036CF7 File Offset: 0x00034EF7
	public bool CustomGameTime
	{
		get
		{
			return this._customGameTime;
		}
		set
		{
			this._customGameTime = value;
		}
	}

	// Token: 0x17000282 RID: 642
	// (get) Token: 0x06000CB3 RID: 3251 RVA: 0x00036D00 File Offset: 0x00034F00
	// (set) Token: 0x06000CB4 RID: 3252 RVA: 0x00036D08 File Offset: 0x00034F08
	public EGameDifficulty Difficulty
	{
		get
		{
			return this._difficulty;
		}
		set
		{
			this._difficulty = value;
		}
	}

	// Token: 0x17000283 RID: 643
	// (get) Token: 0x06000CB5 RID: 3253 RVA: 0x00036D11 File Offset: 0x00034F11
	// (set) Token: 0x06000CB6 RID: 3254 RVA: 0x00036D19 File Offset: 0x00034F19
	public ECharacter Character
	{
		get
		{
			return this._character;
		}
		set
		{
			this._character = value;
		}
	}

	// Token: 0x17000284 RID: 644
	// (get) Token: 0x06000CB7 RID: 3255 RVA: 0x00036D22 File Offset: 0x00034F22
	// (set) Token: 0x06000CB8 RID: 3256 RVA: 0x00036D2A File Offset: 0x00034F2A
	public GameObject ForcedPlayerTemplate
	{
		get
		{
			return this._forcedPlayerTemplate;
		}
		set
		{
			this._forcedPlayerTemplate = value;
		}
	}

	// Token: 0x17000285 RID: 645
	// (get) Token: 0x06000CB9 RID: 3257 RVA: 0x00036D33 File Offset: 0x00034F33
	public int TotalCollectedItems
	{
		get
		{
			return this._collectedItems.Length;
		}
	}

	// Token: 0x17000286 RID: 646
	// (get) Token: 0x06000CBA RID: 3258 RVA: 0x00036D3D File Offset: 0x00034F3D
	// (set) Token: 0x06000CBB RID: 3259 RVA: 0x00036D45 File Offset: 0x00034F45
	public bool Alive
	{
		get
		{
			return this._alive;
		}
		set
		{
			this._alive = value;
		}
	}

	// Token: 0x17000287 RID: 647
	// (get) Token: 0x06000CBC RID: 3260 RVA: 0x00036D4E File Offset: 0x00034F4E
	public List<string> DamagedItems
	{
		get
		{
			return this._damagedItems;
		}
	}

	// Token: 0x17000288 RID: 648
	// (get) Token: 0x06000CBD RID: 3261 RVA: 0x00036D56 File Offset: 0x00034F56
	// (set) Token: 0x06000CBE RID: 3262 RVA: 0x00036D5E File Offset: 0x00034F5E
	public List<string> RemainingItems
	{
		get
		{
			return this._remainingItems;
		}
		set
		{
			this._remainingItems = value;
		}
	}

	// Token: 0x17000289 RID: 649
	// (get) Token: 0x06000CBF RID: 3263 RVA: 0x00036D67 File Offset: 0x00034F67
	// (set) Token: 0x06000CC0 RID: 3264 RVA: 0x00036D6F File Offset: 0x00034F6F
	public List<string> ReportedItems
	{
		get
		{
			return this._reportedItems;
		}
		set
		{
			this._reportedItems = value;
		}
	}

	// Token: 0x1700028A RID: 650
	// (get) Token: 0x06000CC1 RID: 3265 RVA: 0x00036D78 File Offset: 0x00034F78
	// (set) Token: 0x06000CC2 RID: 3266 RVA: 0x00036D80 File Offset: 0x00034F80
	public bool GotToBunker
	{
		get
		{
			return this._gotToBunker;
		}
		set
		{
			this._gotToBunker = value;
		}
	}

	// Token: 0x1700028B RID: 651
	// (get) Token: 0x06000CC3 RID: 3267 RVA: 0x00036D89 File Offset: 0x00034F89
	// (set) Token: 0x06000CC4 RID: 3268 RVA: 0x00036D91 File Offset: 0x00034F91
	public string Surname
	{
		get
		{
			return this._surname;
		}
		set
		{
			this._surname = value;
		}
	}

	// Token: 0x1700028C RID: 652
	// (get) Token: 0x06000CC5 RID: 3269 RVA: 0x00036D9A File Offset: 0x00034F9A
	// (set) Token: 0x06000CC6 RID: 3270 RVA: 0x00036DA2 File Offset: 0x00034FA2
	public string[] CollectedItems
	{
		get
		{
			return this._collectedItems;
		}
		set
		{
			this._collectedItems = value;
		}
	}

	// Token: 0x1700028D RID: 653
	// (get) Token: 0x06000CC7 RID: 3271 RVA: 0x00036DAB File Offset: 0x00034FAB
	// (set) Token: 0x06000CC8 RID: 3272 RVA: 0x00036DB3 File Offset: 0x00034FB3
	public int DaysSurvived
	{
		get
		{
			return this._daysSurvived;
		}
		set
		{
			this._daysSurvived = value;
			GameSessionData.Instance.Conclusion.DaysSurvived = value;
		}
	}

	// Token: 0x1700028E RID: 654
	// (get) Token: 0x06000CC9 RID: 3273 RVA: 0x00036DCC File Offset: 0x00034FCC
	public string DaysSurvivedStr
	{
		get
		{
			return this._daysSurvived.ToString();
		}
	}

	// Token: 0x1700028F RID: 655
	// (get) Token: 0x06000CCA RID: 3274 RVA: 0x00036DD9 File Offset: 0x00034FD9
	// (set) Token: 0x06000CCB RID: 3275 RVA: 0x00036DE1 File Offset: 0x00034FE1
	public string MajorCharName
	{
		get
		{
			return this._majorCharName;
		}
		set
		{
			this._majorCharName = value;
		}
	}

	// Token: 0x17000290 RID: 656
	// (get) Token: 0x06000CCC RID: 3276 RVA: 0x00036DEA File Offset: 0x00034FEA
	// (set) Token: 0x06000CCD RID: 3277 RVA: 0x00036DF2 File Offset: 0x00034FF2
	public string MinorChar1Name
	{
		get
		{
			return this._minorChar1Name;
		}
		set
		{
			this._minorChar1Name = value;
		}
	}

	// Token: 0x17000291 RID: 657
	// (get) Token: 0x06000CCE RID: 3278 RVA: 0x00036DFB File Offset: 0x00034FFB
	// (set) Token: 0x06000CCF RID: 3279 RVA: 0x00036E03 File Offset: 0x00035003
	public string MinorChar2Name
	{
		get
		{
			return this._minorChar2Name;
		}
		set
		{
			this._minorChar2Name = value;
		}
	}

	// Token: 0x17000292 RID: 658
	// (get) Token: 0x06000CD0 RID: 3280 RVA: 0x00036E0C File Offset: 0x0003500C
	// (set) Token: 0x06000CD1 RID: 3281 RVA: 0x00036E14 File Offset: 0x00035014
	public string MinorChar3Name
	{
		get
		{
			return this._minorChar3Name;
		}
		set
		{
			this._minorChar3Name = value;
		}
	}

	// Token: 0x17000293 RID: 659
	// (get) Token: 0x06000CD2 RID: 3282 RVA: 0x00036E1D File Offset: 0x0003501D
	// (set) Token: 0x06000CD3 RID: 3283 RVA: 0x00036E25 File Offset: 0x00035025
	public GameSetup Setup
	{
		get
		{
			return this._setup;
		}
		set
		{
			this._setup = value;
		}
	}

	// Token: 0x17000294 RID: 660
	// (get) Token: 0x06000CD4 RID: 3284 RVA: 0x00036E2E File Offset: 0x0003502E
	// (set) Token: 0x06000CD5 RID: 3285 RVA: 0x00036E36 File Offset: 0x00035036
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

	// Token: 0x17000295 RID: 661
	// (get) Token: 0x06000CD6 RID: 3286 RVA: 0x00036E3F File Offset: 0x0003503F
	// (set) Token: 0x06000CD7 RID: 3287 RVA: 0x00036E47 File Offset: 0x00035047
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

	// Token: 0x17000296 RID: 662
	// (get) Token: 0x06000CD8 RID: 3288 RVA: 0x00036E50 File Offset: 0x00035050
	// (set) Token: 0x06000CD9 RID: 3289 RVA: 0x00036E58 File Offset: 0x00035058
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

	// Token: 0x17000297 RID: 663
	// (get) Token: 0x06000CDA RID: 3290 RVA: 0x00036E61 File Offset: 0x00035061
	// (set) Token: 0x06000CDB RID: 3291 RVA: 0x00036E69 File Offset: 0x00035069
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

	// Token: 0x17000298 RID: 664
	// (get) Token: 0x06000CDC RID: 3292 RVA: 0x00036E72 File Offset: 0x00035072
	// (set) Token: 0x06000CDD RID: 3293 RVA: 0x00036E7A File Offset: 0x0003507A
	public bool ToLoad
	{
		get
		{
			return this._toLoad;
		}
		set
		{
			this._toLoad = value;
			this._loaded = true;
		}
	}

	// Token: 0x17000299 RID: 665
	// (get) Token: 0x06000CDE RID: 3294 RVA: 0x00036E8A File Offset: 0x0003508A
	// (set) Token: 0x06000CDF RID: 3295 RVA: 0x00036E92 File Offset: 0x00035092
	public bool Loaded
	{
		get
		{
			return this._loaded;
		}
		set
		{
			this._loaded = value;
		}
	}

	// Token: 0x1700029A RID: 666
	// (get) Token: 0x06000CE0 RID: 3296 RVA: 0x00036E9B File Offset: 0x0003509B
	// (set) Token: 0x06000CE1 RID: 3297 RVA: 0x00036EA3 File Offset: 0x000350A3
	public string CustomEventsPath
	{
		get
		{
			return this._customEventsPath;
		}
		set
		{
			this._customEventsPath = value;
		}
	}

	// Token: 0x1700029B RID: 667
	// (get) Token: 0x06000CE2 RID: 3298 RVA: 0x00036EAC File Offset: 0x000350AC
	public ConclusionData Conclusion
	{
		get
		{
			return this._conclusionData;
		}
	}

	// Token: 0x1700029C RID: 668
	// (get) Token: 0x06000CE3 RID: 3299 RVA: 0x00036EB4 File Offset: 0x000350B4
	public List<string> SuitcaseStock
	{
		get
		{
			return this._suitcaseStock;
		}
	}

	// Token: 0x1700029D RID: 669
	// (get) Token: 0x06000CE4 RID: 3300 RVA: 0x00036EBC File Offset: 0x000350BC
	public List<string> ShelterStock
	{
		get
		{
			return this._shelterStock;
		}
	}

	// Token: 0x1700029E RID: 670
	// (get) Token: 0x06000CE5 RID: 3301 RVA: 0x00036EC4 File Offset: 0x000350C4
	public int ShelterStockFood
	{
		get
		{
			return this._shelterStockFood;
		}
	}

	// Token: 0x1700029F RID: 671
	// (get) Token: 0x06000CE6 RID: 3302 RVA: 0x00036ECC File Offset: 0x000350CC
	public int ShelterStockWater
	{
		get
		{
			return this._shelterStockWater;
		}
	}

	// Token: 0x170002A0 RID: 672
	// (get) Token: 0x06000CE7 RID: 3303 RVA: 0x00036ED4 File Offset: 0x000350D4
	// (set) Token: 0x06000CE8 RID: 3304 RVA: 0x00036EDC File Offset: 0x000350DC
	public ECurrentGameStage CurrentGameStage
	{
		get
		{
			return this._currentGameStage;
		}
		set
		{
			this._currentGameStage = value;
		}
	}

	// Token: 0x170002A1 RID: 673
	// (get) Token: 0x06000CE9 RID: 3305 RVA: 0x00036EE5 File Offset: 0x000350E5
	// (set) Token: 0x06000CEA RID: 3306 RVA: 0x00036EED File Offset: 0x000350ED
	public float ScavengeFinishedTime
	{
		get
		{
			return this._scavengeFinishedTime;
		}
		set
		{
			this._scavengeFinishedTime = value;
		}
	}

	// Token: 0x170002A2 RID: 674
	// (get) Token: 0x06000CEB RID: 3307 RVA: 0x00036EF6 File Offset: 0x000350F6
	// (set) Token: 0x06000CEC RID: 3308 RVA: 0x00036EFE File Offset: 0x000350FE
	public Challenge CurrentChallenge
	{
		get
		{
			return this._currentChallenge;
		}
		set
		{
			this._currentChallenge = value;
		}
	}

	// Token: 0x040006E5 RID: 1765
	public static GameSessionData Instance;

	// Token: 0x040006E6 RID: 1766
	private bool _toLoad;

	// Token: 0x040006E7 RID: 1767
	private bool _loaded;

	// Token: 0x040006E8 RID: 1768
	private ECurrentGameStage _currentGameStage;

	// Token: 0x040006E9 RID: 1769
	[SerializeField]
	private GameSetup _setup;

	// Token: 0x040006EA RID: 1770
	[SerializeField]
	private EGameDifficulty _difficulty;

	// Token: 0x040006EB RID: 1771
	[SerializeField]
	private ECharacter _character = ECharacter.DAD;

	// Token: 0x040006EC RID: 1772
	[SerializeField]
	private GameObject _forcedPlayerTemplate;

	// Token: 0x040006ED RID: 1773
	[SerializeField]
	private int _prepareTime;

	// Token: 0x040006EE RID: 1774
	[SerializeField]
	private int _gameTime = 60;

	// Token: 0x040006EF RID: 1775
	[SerializeField]
	private int _comfortZoneTimeout = 20;

	// Token: 0x040006F0 RID: 1776
	[SerializeField]
	private int _cautionZoneTimeout = 40;

	// Token: 0x040006F1 RID: 1777
	[SerializeField]
	private bool _customGameTime;

	// Token: 0x040006F2 RID: 1778
	[SerializeField]
	private Challenge _currentChallenge;

	// Token: 0x040006F3 RID: 1779
	[SerializeField]
	private bool _gotToBunker;

	// Token: 0x040006F4 RID: 1780
	[SerializeField]
	private bool _alive;

	// Token: 0x040006F5 RID: 1781
	private int _daysSurvived = 1;

	// Token: 0x040006F6 RID: 1782
	private float _scavengeFinishedTime;

	// Token: 0x040006F7 RID: 1783
	[SerializeField]
	private string[] _collectedItems;

	// Token: 0x040006F8 RID: 1784
	private List<string> _remainingItems = new List<string>();

	// Token: 0x040006F9 RID: 1785
	private List<string> _reportedItems = new List<string>();

	// Token: 0x040006FA RID: 1786
	private List<string> _damagedItems = new List<string>();

	// Token: 0x040006FB RID: 1787
	private List<string> _shelterStock = new List<string>();

	// Token: 0x040006FC RID: 1788
	private List<string> _suitcaseStock = new List<string>();

	// Token: 0x040006FD RID: 1789
	private int _shelterStockFood;

	// Token: 0x040006FE RID: 1790
	private int _shelterStockWater;

	// Token: 0x040006FF RID: 1791
	[SerializeField]
	private string _surname = string.Empty;

	// Token: 0x04000700 RID: 1792
	[SerializeField]
	private string _majorCharName = string.Empty;

	// Token: 0x04000701 RID: 1793
	[SerializeField]
	private string _minorChar1Name = string.Empty;

	// Token: 0x04000702 RID: 1794
	[SerializeField]
	private string _minorChar2Name = string.Empty;

	// Token: 0x04000703 RID: 1795
	[SerializeField]
	private string _minorChar3Name = string.Empty;

	// Token: 0x04000704 RID: 1796
	[SerializeField]
	private string _customEventsPath = string.Empty;

	// Token: 0x04000705 RID: 1797
	private string _saveFilename;

	// Token: 0x04000706 RID: 1798
	private ConclusionData _conclusionData;
}

using System;
using System.Collections.Generic;
using System.IO;
using Steamworks;
using UnityEngine;

// Token: 0x02000114 RID: 276
[Serializable]
public class Profile
{
	// Token: 0x06000D63 RID: 3427 RVA: 0x00037A88 File Offset: 0x00035C88
	public void Initialize()
	{
		SteamUserStats.RequestCurrentStats();
		this.LoadUnlocks();
	}

	// Token: 0x06000D64 RID: 3428 RVA: 0x00037A98 File Offset: 0x00035C98
	public void ClearSnapshots()
	{
		DirectoryInfo directoryInfo = new DirectoryInfo(Application.persistentDataPath);
		if (directoryInfo.Exists)
		{
			FileInfo[] files = directoryInfo.GetFiles(".png");
			if (files != null)
			{
				for (int i = files.Length - 1; i >= 0; i--)
				{
					files[i].Delete();
				}
			}
		}
	}

	// Token: 0x06000D65 RID: 3429 RVA: 0x00037ADF File Offset: 0x00035CDF
	public void TakeSnapshot(string filename)
	{
		this.TakeScreenshot(Application.persistentDataPath + "/" + filename);
	}

	// Token: 0x06000D66 RID: 3430 RVA: 0x00037AF8 File Offset: 0x00035CF8
	public void TakeScreenshot(string path, string format)
	{
		DateTime now = DateTime.Now;
		this.TakeScreenshot(string.Concat(new string[]
		{
			path,
			"/",
			now.Year.ToString(),
			now.Month.ToString(),
			now.Day.ToString(),
			now.Hour.ToString(),
			now.Minute.ToString(),
			now.Second.ToString(),
			format
		}));
	}

	// Token: 0x06000D67 RID: 3431 RVA: 0x00037B99 File Offset: 0x00035D99
	public void TakeScreenshot(string fullPath)
	{
		ScreenCapture.CaptureScreenshot(fullPath);
	}

	// Token: 0x06000D68 RID: 3432 RVA: 0x00037BA4 File Offset: 0x00035DA4
	public void AddRecord(string name, object val)
	{
		if (val is int)
		{
			int num = 0;
			if (SteamUserStats.GetStat(name, out num))
			{
				SteamUserStats.SetStat(name, num + (int)val);
			}
		}
		else if (val is float)
		{
			float num2 = 0f;
			if (SteamUserStats.GetStat(name, out num2))
			{
				SteamUserStats.SetStat(name, num2 + (float)val);
			}
		}
		SteamUserStats.StoreStats();
	}

	// Token: 0x06000D69 RID: 3433 RVA: 0x00037C04 File Offset: 0x00035E04
	public void SetRecordBitfield(string name, int val)
	{
		int bitfieldValue = GlobalTools.GetBitfieldValue(val);
		SteamUserStats.SetStat(name, bitfieldValue);
	}

	// Token: 0x06000D6A RID: 3434 RVA: 0x00037C20 File Offset: 0x00035E20
	public void AddRecordBitfield(string name, int val)
	{
		int bitfieldValue = GlobalTools.GetBitfieldValue(val);
		int num = 0;
		if (SteamUserStats.GetStat(name, out num) && !GlobalTools.TestBitfield(val, num))
		{
			num += bitfieldValue;
			SteamUserStats.SetStat(name, num);
			SteamUserStats.StoreStats();
		}
	}

	// Token: 0x06000D6B RID: 3435 RVA: 0x00037C5C File Offset: 0x00035E5C
	public bool TestRecordBitfieldValue(string name, int val)
	{
		int num = -1;
		SteamUserStats.GetStat(name, out num);
		return num >= 0 && GlobalTools.TestBitfieldValue(val, num);
	}

	// Token: 0x06000D6C RID: 3436 RVA: 0x00037C84 File Offset: 0x00035E84
	public bool TestRecordBitfield(string name, int val)
	{
		int num = -1;
		SteamUserStats.GetStat(name, out num);
		return num >= 0 && GlobalTools.TestBitfield(val, num);
	}

	// Token: 0x06000D6D RID: 3437 RVA: 0x00037CAC File Offset: 0x00035EAC
	public void IncrementRecord(string name, float val, bool forceLocal = false)
	{
		float num = this.GetRecord<float>(name, forceLocal) + val;
		this.SetRecord(name, num, forceLocal);
	}

	// Token: 0x06000D6E RID: 3438 RVA: 0x00037CD4 File Offset: 0x00035ED4
	public void SetRecord(string name, object val, bool forceLocal = false)
	{
		bool flag = false;
		if (!forceLocal)
		{
			if (val is int)
			{
				SteamUserStats.SetStat(name, (int)val);
			}
			else if (val is float)
			{
				SteamUserStats.SetStat(name, (float)val);
			}
			SteamUserStats.StoreStats();
			return;
		}
		if (val is int)
		{
			int val2 = (int)val;
			global::PlayerPrefs.SetInt(name, val2);
			flag = true;
		}
		else if (val is float)
		{
			float val3 = (float)val;
			global::PlayerPrefs.SetFloat(name, val3);
			flag = true;
		}
		else if (val is string)
		{
			global::PlayerPrefs.SetString(name, val.ToString());
			flag = true;
		}
		if (flag)
		{
			global::PlayerPrefs.Save();
		}
	}

	// Token: 0x06000D6F RID: 3439 RVA: 0x00037D6B File Offset: 0x00035F6B
	public void Reset()
	{
		SteamUserStats.ResetAllStats(true);
	}

	// Token: 0x06000D70 RID: 3440 RVA: 0x00037D74 File Offset: 0x00035F74
	public T GetRecord<T>(string name, bool forceLocal = false)
	{
		return (T)((object)this.GetRecordObject<T>(name, forceLocal));
	}

	// Token: 0x06000D71 RID: 3441 RVA: 0x00037D84 File Offset: 0x00035F84
	private object GetRecordObject<T>(string name, bool forceLocal = false)
	{
		if (!forceLocal)
		{
			if (typeof(T) == typeof(int))
			{
				int num = 0;
				if (SteamUserStats.GetStat(name, out num))
				{
					return num;
				}
			}
			else if (typeof(T) == typeof(float))
			{
				float num2 = 0f;
				if (SteamUserStats.GetStat(name, out num2))
				{
					return num2;
				}
			}
		}
		if (typeof(T) == typeof(int))
		{
			return global::PlayerPrefs.GetInt(name, 0);
		}
		if (typeof(T) == typeof(float))
		{
			return global::PlayerPrefs.GetFloat(name, 0f);
		}
		if (typeof(T) == typeof(string))
		{
			return global::PlayerPrefs.GetString(name, string.Empty);
		}
		return default(T);
	}

	// Token: 0x06000D72 RID: 3442 RVA: 0x00037E80 File Offset: 0x00036080
	public void UnlockAchievements(bool unlock)
	{
		for (int i = 0; i < this._achievements.Length; i++)
		{
			if (unlock)
			{
				this.UnlockAchievement(this._achievements[i].Text);
			}
			else
			{
				this.LockAchievement(this._achievements[i].Text);
			}
		}
	}

	// Token: 0x06000D73 RID: 3443 RVA: 0x00037ED6 File Offset: 0x000360D6
	public void LockAchievement(string name)
	{
		SteamUserStats.ClearAchievement(name);
		SteamUserStats.StoreStats();
	}

	// Token: 0x06000D74 RID: 3444 RVA: 0x00037EE5 File Offset: 0x000360E5
	public void UnlockAchievement(string name)
	{
		if (!this.IsAchievementUnlocked(name))
		{
			SteamUserStats.SetAchievement(name);
			SteamUserStats.StoreStats();
		}
	}

	// Token: 0x06000D75 RID: 3445 RVA: 0x00037F00 File Offset: 0x00036100
	public int ProgressAchievement(string achName, string recordName, int progress, int maxProgress, bool indicate = false, bool forceProgress = false)
	{
		int record = this.GetRecord<int>(recordName, false);
		uint nCurProgress = (uint)progress;
		if (this.IsAchievementUnlocked(achName))
		{
			return record;
		}
		if (forceProgress)
		{
			this.SetRecord(recordName, progress, false);
		}
		else
		{
			this.AddRecord(recordName, progress);
		}
		int record2 = this.GetRecord<int>(recordName, false);
		if (record2 >= maxProgress)
		{
			indicate = true;
			this.UnlockAchievement(achName);
			nCurProgress = (uint)maxProgress;
		}
		else
		{
			int num = maxProgress / 2;
			if (record < num && record2 >= num)
			{
				indicate = true;
				nCurProgress = (uint)record2;
			}
		}
		if (indicate)
		{
			SteamUserStats.IndicateAchievementProgress(achName, nCurProgress, (uint)maxProgress);
			SteamUserStats.StoreStats();
		}
		return record2;
	}

	// Token: 0x06000D76 RID: 3446 RVA: 0x00037F88 File Offset: 0x00036188
	public bool IsAchievementUnlocked(string name)
	{
		bool result = false;
		SteamUserStats.GetAchievement(name, out result);
		return result;
	}

	// Token: 0x06000D77 RID: 3447 RVA: 0x00037FA4 File Offset: 0x000361A4
	public TextEntry GetAchievementData(string name)
	{
		if (this._achievements != null)
		{
			for (int i = 0; i < this._achievements.Length; i++)
			{
				if (this._achievements[i].Text == name)
				{
					return this._achievements[i];
				}
			}
		}
		return null;
	}

	// Token: 0x06000D78 RID: 3448 RVA: 0x00037FFC File Offset: 0x000361FC
	public bool GetAchievementData(int index, bool localize, out string achCode, out string achName, out string achDescr, out string achIcon, out string achProgressMax)
	{
		achCode = string.Empty;
		achName = string.Empty;
		achDescr = string.Empty;
		achIcon = string.Empty;
		achProgressMax = string.Empty;
		if (this._achievements != null && index >= 0 && index < this._achievements.Length)
		{
			TextEntry textEntry = this._achievements[index];
			if (textEntry != null)
			{
				achCode = textEntry.Text;
				achName = (localize ? Settings.Data.LocalizationManager.GetValue(textEntry.GetParam(0)) : textEntry.GetParam(0));
				achDescr = (localize ? Settings.Data.LocalizationManager.GetValue(textEntry.GetParam(1)) : textEntry.GetParam(1));
				achIcon = textEntry.GetParam(2);
				achProgressMax = textEntry.GetParam(3);
				return true;
			}
		}
		return false;
	}

	// Token: 0x06000D79 RID: 3449 RVA: 0x000380D0 File Offset: 0x000362D0
	public bool GetAchievementData(string name, bool localize, out string achName, out string achDescr, out string achIcon)
	{
		TextEntry achievementData = this.GetAchievementData(name);
		if (achievementData != null)
		{
			achName = (localize ? Settings.Data.LocalizationManager.GetValue(achievementData.GetParam(0)) : achievementData.GetParam(0));
			achDescr = (localize ? Settings.Data.LocalizationManager.GetValue(achievementData.GetParam(1)) : achievementData.GetParam(1));
			achIcon = achievementData.GetParam(2);
			return true;
		}
		achName = string.Empty;
		achDescr = string.Empty;
		achIcon = string.Empty;
		return false;
	}

	// Token: 0x06000D7A RID: 3450 RVA: 0x00038158 File Offset: 0x00036358
	public string[] GetUnlockedAchievements()
	{
		if (this._achievements != null)
		{
			List<string> list = new List<string>(this._achievements.Length);
			for (int i = 0; i < this._achievements.Length; i++)
			{
				TextEntry textEntry = this._achievements[i];
				bool flag = false;
				SteamUserStats.GetAchievement(textEntry.Text, out flag);
				if (flag)
				{
					list.Add(textEntry.Text);
				}
			}
		}
		return null;
	}

	// Token: 0x170002DD RID: 733
	// (get) Token: 0x06000D7B RID: 3451 RVA: 0x000381C7 File Offset: 0x000363C7
	public TextCollection Achievements
	{
		get
		{
			return this._achievements;
		}
	}

	// Token: 0x06000D7C RID: 3452 RVA: 0x000381D0 File Offset: 0x000363D0
	public static void TryToUnlockDifficultyAchievements(EGameDifficulty difficulty, EGameType gameType)
	{
		string name = string.Empty;
		switch (difficulty)
		{
		case EGameDifficulty.EASY:
			name = "EasyModesDone";
			break;
		case EGameDifficulty.NORMAL:
			name = "NormalModesDone";
			break;
		case EGameDifficulty.HARD:
			name = "HardModesDone";
			break;
		}
		Settings.Data.PlayerProfile.AddRecordBitfield(name, (int)gameType);
		int num = GlobalTools.GetBitfieldValue(2) + GlobalTools.GetBitfieldValue(3);
		GlobalTools.GetBitfieldValue(4);
		bool flag = true;
		for (int i = 0; i < 3; i++)
		{
			flag &= Settings.Data.PlayerProfile.TestRecordBitfield(name, i);
		}
		if (flag)
		{
			switch (difficulty)
			{
			case EGameDifficulty.EASY:
				Settings.Data.PlayerProfile.UnlockAchievement("ACH_EASY");
				return;
			case EGameDifficulty.NORMAL:
				Settings.Data.PlayerProfile.UnlockAchievement("ACH_NORMAL");
				return;
			case EGameDifficulty.HARD:
				Settings.Data.PlayerProfile.UnlockAchievement("ACH_HARD");
				break;
			default:
				return;
			}
		}
	}

	// Token: 0x06000D7D RID: 3453 RVA: 0x000382B0 File Offset: 0x000364B0
	private void LoadUnlocks()
	{
		GameUnlock[] array = Resources.LoadAll<GameUnlock>("DLC/Unlocks");
		if (array != null && array.Length != 0)
		{
			this._unlocks.AddRange(array);
		}
		for (int i = 0; i < this._unlocks.Count; i++)
		{
			this._unlocks[i].Unlock(this.IsUnlockAvailable(this._unlocks[i].Id));
		}
	}

	// Token: 0x06000D7E RID: 3454 RVA: 0x00038319 File Offset: 0x00036519
	public bool IsUnlockAvailable(string name)
	{
		return this.GetRecord<int>(name, true) == int.MaxValue;
	}

	// Token: 0x06000D7F RID: 3455 RVA: 0x0003832C File Offset: 0x0003652C
	public void SetUnlockAvailable(string id, bool available)
	{
		for (int i = 0; i < this._unlocks.Count; i++)
		{
			if (this._unlocks[i].Id == id)
			{
				this._unlocks[i].Unlock(available);
				this.SetRecord(id, available ? int.MaxValue : 0, true);
			}
		}
	}

	// Token: 0x06000D80 RID: 3456 RVA: 0x00038394 File Offset: 0x00036594
	public GameUnlock GetUnlock(string id)
	{
		return this._unlocks.Find((GameUnlock x) => x.Id == id);
	}

	// Token: 0x06000D81 RID: 3457 RVA: 0x000383C8 File Offset: 0x000365C8
	public bool GetUnlocks(string id, out GameUnlock[] unlocks)
	{
		List<GameUnlock> list = new List<GameUnlock>();
		for (int i = 0; i < this._unlocks.Count; i++)
		{
			if (this._unlocks[i].Id == id)
			{
				list.Add(this._unlocks[i]);
			}
		}
		unlocks = list.ToArray();
		return unlocks != null && unlocks.Length != 0;
	}

	// Token: 0x06000D82 RID: 3458 RVA: 0x00038430 File Offset: 0x00036630
	public bool GetUnlocks(EGameUnlockType unlockType, string target, out GameUnlock[] unlocks)
	{
		List<GameUnlock> list = new List<GameUnlock>();
		for (int i = 0; i < this._unlocks.Count; i++)
		{
			if (this._unlocks[i].Type == unlockType && this._unlocks[i].IsTarget(target))
			{
				list.Add(this._unlocks[i]);
			}
		}
		unlocks = list.ToArray();
		return unlocks != null && unlocks.Length != 0;
	}

	// Token: 0x06000D83 RID: 3459 RVA: 0x000384A8 File Offset: 0x000366A8
	private static string GetCurrentHatRecordName(string character)
	{
		string result = string.Empty;
		if (character != null)
		{
			if (!(character == "dad"))
			{
				if (!(character == "mom"))
				{
					if (!(character == "daughter"))
					{
						if (!(character == "son"))
						{
							if (character == "dog")
							{
								result = "DogHat";
							}
						}
						else
						{
							result = "SonHat";
						}
					}
					else
					{
						result = "DaughterHat";
					}
				}
				else
				{
					result = "MomHat";
				}
			}
			else
			{
				result = "DadHat";
			}
		}
		return result;
	}

	// Token: 0x06000D84 RID: 3460 RVA: 0x00038528 File Offset: 0x00036728
	public void SetCurrentHat(string character, string hatId)
	{
		this.SetRecord(Profile.GetCurrentHatRecordName(character), string.IsNullOrEmpty(hatId) ? string.Empty : hatId, true);
	}

	// Token: 0x06000D85 RID: 3461 RVA: 0x00038548 File Offset: 0x00036748
	public string GetCurrentHat(string character)
	{
		string record = this.GetRecord<string>(Profile.GetCurrentHatRecordName(character), true);
		if (!string.IsNullOrEmpty(record) && this.IsUnlockAvailable(record))
		{
			return record;
		}
		return null;
	}

	// Token: 0x06000D86 RID: 3462 RVA: 0x00038577 File Offset: 0x00036777
	public bool WasChallengeCompleted(string id)
	{
		return this.GetRecord<int>(this.GetChallengeDoneRecordId(id), true) > 0;
	}

	// Token: 0x06000D87 RID: 3463 RVA: 0x0003858C File Offset: 0x0003678C
	public bool GetChallengeCompletionData(string id, out float time, out DateTime date)
	{
		bool result = this.WasChallengeCompleted(id);
		time = this.GetRecord<float>(this.GetChallengeDoneTimeRecordId(id), true);
		string text = this.GetRecord<int>(this.GetChallengeDoneDateRecordId(id), true).ToString();
		text = text.Insert(4, "/");
		text = text.Insert(7, "/");
		date = DateTime.Parse(text);
		return result;
	}

	// Token: 0x06000D88 RID: 3464 RVA: 0x000385F0 File Offset: 0x000367F0
	public void CompleteSurvivalChallenge(string id, int days, DateTime date)
	{
		string challengeDoneRecordId = this.GetChallengeDoneRecordId(id);
		int record = this.GetRecord<int>(challengeDoneRecordId, false);
		this.SetRecord(challengeDoneRecordId, record + 1, true);
		int num = (record > 0) ? this.GetRecord<int>(this.GetChallengeDoneTimeRecordId(id), false) : 0;
		if (record == 0 || days < num)
		{
			this.SetRecord(this.GetChallengeDoneTimeRecordId(id), days, true);
			string value = date.ToString("yyyyMMdd");
			this.SetRecord(this.GetChallengeDoneDateRecordId(id), Convert.ToInt32(value), true);
		}
	}

	// Token: 0x06000D89 RID: 3465 RVA: 0x0003867C File Offset: 0x0003687C
	public void CompleteScavengeChallenge(string id, float time, DateTime date)
	{
		string challengeDoneRecordId = this.GetChallengeDoneRecordId(id);
		int record = this.GetRecord<int>(challengeDoneRecordId, false);
		this.SetRecord(challengeDoneRecordId, record + 1, true);
		float num = (record > 0) ? this.GetRecord<float>(this.GetChallengeDoneTimeRecordId(id), false) : 999f;
		if (record == 0 || time < num)
		{
			this.SetRecord(this.GetChallengeDoneTimeRecordId(id), time, true);
			string value = date.ToString("yyyyMMdd");
			this.SetRecord(this.GetChallengeDoneDateRecordId(id), Convert.ToInt32(value), true);
		}
	}

	// Token: 0x06000D8A RID: 3466 RVA: 0x0003870A File Offset: 0x0003690A
	private string GetChallengeDoneRecordId(string challengeId)
	{
		return challengeId + "Done";
	}

	// Token: 0x06000D8B RID: 3467 RVA: 0x00038717 File Offset: 0x00036917
	private string GetChallengeDoneTimeRecordId(string challengeId)
	{
		return challengeId + "DoneTime";
	}

	// Token: 0x06000D8C RID: 3468 RVA: 0x00038724 File Offset: 0x00036924
	private string GetChallengeDoneDateRecordId(string challengeId)
	{
		return challengeId + "DoneDate";
	}

	// Token: 0x04000753 RID: 1875
	public const int UNLOCKED_VAL = 2147483647;

	// Token: 0x04000754 RID: 1876
	public const int LOCKED_VAL = 0;

	// Token: 0x04000755 RID: 1877
	[SerializeField]
	private TextCollection _achievements;

	// Token: 0x04000756 RID: 1878
	private List<GameUnlock> _unlocks = new List<GameUnlock>();
}

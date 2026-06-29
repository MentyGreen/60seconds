using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using DunGen;
using Steamworks;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x02000139 RID: 313
public class ScavengeDataLogger : MonoBehaviour
{
	// Token: 0x06000F3F RID: 3903 RVA: 0x0003EED3 File Offset: 0x0003D0D3
	private void Awake()
	{
		ScavengeDataLogger.Instance = this;
		if (!Settings.Data.DataLogging)
		{
			Object.Destroy(this);
		}
	}

	// Token: 0x06000F40 RID: 3904 RVA: 0x0003EEED File Offset: 0x0003D0ED
	private void Start()
	{
		this.StartLog();
	}

	// Token: 0x06000F41 RID: 3905 RVA: 0x0003EEF8 File Offset: 0x0003D0F8
	public void Save(bool gameFinished, bool cloud, bool async)
	{
		try
		{
			DirectoryInfo directoryInfo = new DirectoryInfo(Settings.Data.DataFilepath);
			if (!directoryInfo.Exists)
			{
				directoryInfo.Create();
			}
			string text = this.SaveFile(Settings.Data.DataFilepath, gameFinished);
			if (!string.IsNullOrEmpty(text))
			{
				if (cloud)
				{
					base.StartCoroutine(this.SendToFirebase(text));
				}
				else if (async)
				{
					base.StartCoroutine(this.SendToWWWAsync(text));
				}
				else
				{
					this.SendToWWW(text);
				}
			}
		}
		catch (Exception)
		{
		}
		Object.Destroy(this, 10f);
	}

	// Token: 0x06000F42 RID: 3906 RVA: 0x0003EF8C File Offset: 0x0003D18C
	public string SaveFile(string filepath, bool gameFinished)
	{
		string text = string.Concat(new string[]
		{
			filepath,
			DateTime.Now.Year.ToString(),
			DateTime.Now.Month.ToString(),
			DateTime.Now.Day.ToString(),
			DateTime.Now.Hour.ToString(),
			DateTime.Now.Minute.ToString(),
			DateTime.Now.Second.ToString(),
			this._login,
			".txt"
		});
		using (StreamWriter streamWriter = new StreamWriter(text))
		{
			streamWriter.Write(this.GenerateLog(gameFinished));
		}
		return text;
	}

	// Token: 0x06000F43 RID: 3907 RVA: 0x0003F07C File Offset: 0x0003D27C
	private IEnumerator SendToFirebase(string filepath)
	{
		filepath.Substring(filepath.LastIndexOf('/'));
		WWW www = new WWW("hosting6112512.az.pl/get_time.php");
		yield return www;
		string text = www.text;
		yield break;
	}

	// Token: 0x06000F44 RID: 3908 RVA: 0x0003F08C File Offset: 0x0003D28C
	private void SendToWWW(string filepath)
	{
		WWWForm wwwform = new WWWForm();
		wwwform.AddField("action", "log upload");
		wwwform.AddField("file", "file");
		byte[] contents = File.ReadAllBytes(filepath);
		wwwform.AddBinaryData("file", contents, Path.GetFileName(filepath), "text/plain");
		if (new WWW("hosting6112512.az.pl/dataupload.php", wwwform).error == null)
		{
			File.Delete(filepath);
		}
	}

	// Token: 0x06000F45 RID: 3909 RVA: 0x0003F0F5 File Offset: 0x0003D2F5
	private IEnumerator SendToWWWAsync(string filepath)
	{
		WWWForm wwwform = new WWWForm();
		wwwform.AddField("action", "log upload");
		wwwform.AddField("file", "file");
		byte[] contents = File.ReadAllBytes(filepath);
		wwwform.AddBinaryData("file", contents, Path.GetFileName(filepath), "text/plain");
		WWW w = new WWW("hosting6112512.az.pl/dataupload.php", wwwform);
		yield return w;
		if (w.error == null)
		{
			File.Delete(filepath);
		}
		yield break;
	}

	// Token: 0x06000F46 RID: 3910 RVA: 0x0003F104 File Offset: 0x0003D304
	public void SendToFtp(string folderName, string fileName, string user, string pass)
	{
		try
		{
			string fileName2 = Path.GetFileName(fileName);
			FtpWebRequest ftpWebRequest = WebRequest.Create(new Uri(string.Format("ftp://{0}/{1}/{2}", "ftp.hosting6112512.az.pl", folderName, fileName2))) as FtpWebRequest;
			ftpWebRequest.Method = "STOR";
			ftpWebRequest.UseBinary = true;
			ftpWebRequest.UsePassive = true;
			ftpWebRequest.KeepAlive = true;
			ftpWebRequest.Credentials = new NetworkCredential(user, pass);
			ftpWebRequest.ConnectionGroupName = "group";
			using (FileStream fileStream = File.OpenRead(fileName))
			{
				byte[] array = new byte[fileStream.Length];
				fileStream.Read(array, 0, array.Length);
				fileStream.Close();
				Stream requestStream = ftpWebRequest.GetRequestStream();
				requestStream.Write(array, 0, array.Length);
				requestStream.Flush();
				requestStream.Close();
			}
		}
		catch (Exception)
		{
		}
	}

	// Token: 0x06000F47 RID: 3911 RVA: 0x0003F1E4 File Offset: 0x0003D3E4
	public void StartLog()
	{
		this._levelName = SceneManager.GetActiveScene().name;
		this._forcedHouse = GlobalTools.GetController<GameFlow>().ForcedHouse;
		this.LogIdData();
		this.LogSetupData();
		base.StartCoroutine(this.LogPlayerData(this._playerMovementFrequency));
	}

	// Token: 0x06000F48 RID: 3912 RVA: 0x0003F233 File Offset: 0x0003D433
	public void EndLog(bool gameFinished, bool cloud, bool async)
	{
		this._scavengeFinishedTime = GameSessionData.Instance.ScavengeFinishedTime;
		this._terminate = true;
		this.Save(gameFinished, cloud, async);
	}

	// Token: 0x06000F49 RID: 3913 RVA: 0x0003F258 File Offset: 0x0003D458
	private string GenerateLog(bool gameFinished)
	{
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.Append(ScavengeDataLogger._version);
		stringBuilder.Append(';');
		stringBuilder.Append(DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString());
		stringBuilder.Append(';');
		stringBuilder.Append(this._login);
		stringBuilder.Append(';');
		stringBuilder.Append(this._levelName);
		stringBuilder.Append(';');
		stringBuilder.Append(gameFinished);
		stringBuilder.Append(';');
		stringBuilder.Append(this._playtime.ToString());
		stringBuilder.Append(';');
		stringBuilder.Append(this._forcedHouse ? 1 : 0);
		stringBuilder.Append(';');
		stringBuilder.Append(this._gameType);
		stringBuilder.Append(';');
		stringBuilder.Append(this._prepTime);
		stringBuilder.Append(';');
		stringBuilder.Append(this._runTime);
		stringBuilder.Append(';');
		stringBuilder.Append(this._scavengeFinishedTime);
		stringBuilder.Append(';');
		stringBuilder.Append(this._shelterPosition.ToString());
		stringBuilder.Append(';');
		stringBuilder.Append(this._endTime);
		stringBuilder.Append(';');
		stringBuilder.Append("ROOMS");
		stringBuilder.Append(';');
		stringBuilder.Append((this._roomDataLog == null) ? 0 : this._roomDataLog.Length);
		stringBuilder.Append(';');
		if (this._roomDataLog != null)
		{
			for (int i = 0; i < this._roomDataLog.Length; i++)
			{
				stringBuilder.Append(this._roomDataLog[i].GetString(';'));
				stringBuilder.Append(';');
			}
		}
		stringBuilder.Append("ITEMS");
		stringBuilder.Append(';');
		stringBuilder.Append((this._itemDataLog == null) ? 0 : this._itemDataLog.Length);
		stringBuilder.Append(';');
		if (this._itemDataLog != null)
		{
			for (int j = 0; j < this._itemDataLog.Length; j++)
			{
				stringBuilder.Append(this._itemDataLog[j].GetString(';'));
				stringBuilder.Append(';');
			}
		}
		stringBuilder.Append("MOVEMENT");
		stringBuilder.Append(';');
		stringBuilder.Append(this._startTime);
		stringBuilder.Append(';');
		stringBuilder.Append(this._playerDataLog.Count);
		stringBuilder.Append(';');
		for (int k = 0; k < this._playerDataLog.Count; k++)
		{
			stringBuilder.Append(this._playerDataLog[k].GetString(';'));
			stringBuilder.Append(';');
		}
		stringBuilder.Append("COLLECTED");
		stringBuilder.Append(';');
		stringBuilder.Append(this._itemCollectedDataLog.Count);
		stringBuilder.Append(';');
		for (int l = 0; l < this._itemCollectedDataLog.Count; l++)
		{
			stringBuilder.Append(this._itemCollectedDataLog[l].GetString(';'));
			stringBuilder.Append(';');
		}
		stringBuilder.Append("DROPS");
		stringBuilder.Append(';');
		stringBuilder.Append(this._dropDataLog.Count);
		stringBuilder.Append(';');
		for (int m = 0; m < this._dropDataLog.Count; m++)
		{
			stringBuilder.Append(this._dropDataLog[m].ToString());
			stringBuilder.Append(';');
		}
		stringBuilder.Append("COLLISIONS");
		stringBuilder.Append(';');
		stringBuilder.Append(this._collisionDataLog.Count);
		stringBuilder.Append(';');
		for (int n = 0; n < this._collisionDataLog.Count; n++)
		{
			stringBuilder.Append(this._collisionDataLog[n].GetString(';'));
			stringBuilder.Append(';');
		}
		return stringBuilder.ToString();
	}

	// Token: 0x06000F4A RID: 3914 RVA: 0x0003F6AF File Offset: 0x0003D8AF
	public void LogItemPickupData(string id, float time, Vector3 pos, Quaternion rot)
	{
		this._itemCollectedDataLog.Add(new SObjectDataLog(time, pos, rot, id));
	}

	// Token: 0x06000F4B RID: 3915 RVA: 0x0003F6C6 File Offset: 0x0003D8C6
	public void LogDropData(float time)
	{
		this._dropDataLog.Add(time);
	}

	// Token: 0x06000F4C RID: 3916 RVA: 0x0003F6D4 File Offset: 0x0003D8D4
	public void LogCollisionData(string id, float time, Vector3 pos, Quaternion rot)
	{
		this._collisionDataLog.Add(new SObjectDataLog(time, pos, rot, id));
	}

	// Token: 0x06000F4D RID: 3917 RVA: 0x0003F6EC File Offset: 0x0003D8EC
	public void LogIdData()
	{
		this._login = string.Empty;
		this._login = SteamUser.GetSteamID().m_SteamID.ToString();
		this._playtime = Settings.Data.PlayerProfile.GetRecord<float>("Playtime", false) + Time.time;
	}

	// Token: 0x06000F4E RID: 3918 RVA: 0x0003F740 File Offset: 0x0003D940
	public void LogSetupData()
	{
		this._gameType = GameSessionData.Instance.Setup.GameType;
		this._prepTime = GameSessionData.Instance.GetCurrentDifficulty().PrepareTime;
		this._runTime = GameSessionData.Instance.GetCurrentDifficulty().ScavengeTime;
		this._startTime = Time.time;
		Tile[] array = Object.FindObjectsOfType<Tile>();
		if (array != null)
		{
			this._roomDataLog = new SObjectDataLog[array.Length];
			for (int i = 0; i < array.Length; i++)
			{
				Transform transform = array[i].gameObject.transform;
				this._roomDataLog[i] = new SObjectDataLog(Time.time, transform.position, transform.rotation, array[i].gameObject.name);
			}
		}
		ScavengeItemController[] array2 = Object.FindObjectsOfType<ScavengeItemController>();
		if (array2 != null)
		{
			this._itemDataLog = new SObjectDataLog[array2.Length];
			for (int j = 0; j < array2.Length; j++)
			{
				Transform transform2 = array2[j].gameObject.transform;
				this._itemDataLog[j] = new SObjectDataLog(Time.time, transform2.position, transform2.rotation, array2[j].SurvivalName);
			}
		}
		Shelter shelter = GlobalTools.GetShelter();
		if (shelter != null)
		{
			this._shelterPosition = shelter.gameObject.transform.position;
		}
	}

	// Token: 0x06000F4F RID: 3919 RVA: 0x0003F895 File Offset: 0x0003DA95
	public IEnumerator LogPlayerData(float frequency)
	{
		GameObject player = GlobalTools.GetPlayer();
		Transform playerTransform = null;
		if (player == null)
		{
			this._terminate = true;
		}
		else
		{
			playerTransform = player.transform;
		}
		while (!this._terminate)
		{
			this._playerDataLog.Add(new SBaseDataLog(Time.time, playerTransform.position, playerTransform.rotation));
			yield return new WaitForSeconds(frequency);
		}
		this._endTime = Time.time;
		yield break;
	}

	// Token: 0x1700033A RID: 826
	// (get) Token: 0x06000F50 RID: 3920 RVA: 0x0003F8AB File Offset: 0x0003DAAB
	// (set) Token: 0x06000F51 RID: 3921 RVA: 0x0003F8B3 File Offset: 0x0003DAB3
	public bool Terminate
	{
		get
		{
			return this._terminate;
		}
		set
		{
			this._terminate = value;
		}
	}

	// Token: 0x04000932 RID: 2354
	public static ScavengeDataLogger Instance = null;

	// Token: 0x04000933 RID: 2355
	[SerializeField]
	private float _playerMovementFrequency = 0.1f;

	// Token: 0x04000934 RID: 2356
	private bool _terminate;

	// Token: 0x04000935 RID: 2357
	private string _login = string.Empty;

	// Token: 0x04000936 RID: 2358
	private float _playtime;

	// Token: 0x04000937 RID: 2359
	private EGameType _gameType;

	// Token: 0x04000938 RID: 2360
	private int _runTime;

	// Token: 0x04000939 RID: 2361
	private int _prepTime;

	// Token: 0x0400093A RID: 2362
	private float _startTime;

	// Token: 0x0400093B RID: 2363
	private float _endTime;

	// Token: 0x0400093C RID: 2364
	private Vector3 _shelterPosition = Vector3.zero;

	// Token: 0x0400093D RID: 2365
	private string _levelName;

	// Token: 0x0400093E RID: 2366
	private bool _forcedHouse;

	// Token: 0x0400093F RID: 2367
	private float _scavengeFinishedTime;

	// Token: 0x04000940 RID: 2368
	private List<SObjectDataLog> _itemCollectedDataLog = new List<SObjectDataLog>(40);

	// Token: 0x04000941 RID: 2369
	private List<SObjectDataLog> _collisionDataLog = new List<SObjectDataLog>();

	// Token: 0x04000942 RID: 2370
	private List<float> _dropDataLog = new List<float>();

	// Token: 0x04000943 RID: 2371
	private List<SBaseDataLog> _playerDataLog = new List<SBaseDataLog>(600);

	// Token: 0x04000944 RID: 2372
	private SObjectDataLog[] _itemDataLog;

	// Token: 0x04000945 RID: 2373
	private SObjectDataLog[] _roomDataLog;

	// Token: 0x04000946 RID: 2374
	private static string _version = "0004";
}

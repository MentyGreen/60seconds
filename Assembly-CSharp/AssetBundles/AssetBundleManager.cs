using System;
using System.Collections.Generic;
using UnityEngine;

namespace AssetBundles
{
	// Token: 0x0200020C RID: 524
	public class AssetBundleManager : MonoBehaviour
	{
		// Token: 0x170003DA RID: 986
		// (get) Token: 0x0600147D RID: 5245 RVA: 0x0005B2A5 File Offset: 0x000594A5
		// (set) Token: 0x0600147E RID: 5246 RVA: 0x0005B2AC File Offset: 0x000594AC
		public static AssetBundleManager.LogMode logMode
		{
			get
			{
				return AssetBundleManager.m_LogMode;
			}
			set
			{
				AssetBundleManager.m_LogMode = value;
			}
		}

		// Token: 0x170003DB RID: 987
		// (get) Token: 0x0600147F RID: 5247 RVA: 0x0005B2B4 File Offset: 0x000594B4
		// (set) Token: 0x06001480 RID: 5248 RVA: 0x0005B2BB File Offset: 0x000594BB
		public static string BaseDownloadingURL
		{
			get
			{
				return AssetBundleManager.m_BaseDownloadingURL;
			}
			set
			{
				AssetBundleManager.m_BaseDownloadingURL = value;
			}
		}

		// Token: 0x170003DC RID: 988
		// (get) Token: 0x06001481 RID: 5249 RVA: 0x0005B2C3 File Offset: 0x000594C3
		// (set) Token: 0x06001482 RID: 5250 RVA: 0x0005B2CA File Offset: 0x000594CA
		public static string[] ActiveVariants
		{
			get
			{
				return AssetBundleManager.m_ActiveVariants;
			}
			set
			{
				AssetBundleManager.m_ActiveVariants = value;
			}
		}

		// Token: 0x170003DD RID: 989
		// (set) Token: 0x06001483 RID: 5251 RVA: 0x0005B2D2 File Offset: 0x000594D2
		public static AssetBundleManifest AssetBundleManifestObject
		{
			set
			{
				AssetBundleManager.m_AssetBundleManifest = value;
			}
		}

		// Token: 0x06001484 RID: 5252 RVA: 0x0005B2DA File Offset: 0x000594DA
		private static void Log(AssetBundleManager.LogType logType, string text)
		{
			if (logType == AssetBundleManager.LogType.Error)
			{
				Debug.LogError("[AssetBundleManager] " + text);
				return;
			}
			if (AssetBundleManager.m_LogMode == AssetBundleManager.LogMode.All)
			{
				Debug.Log("[AssetBundleManager] " + text);
			}
		}

		// Token: 0x06001485 RID: 5253 RVA: 0x0005B308 File Offset: 0x00059508
		private static string GetStreamingAssetsPath()
		{
			if (Application.isEditor)
			{
				return "file://" + Environment.CurrentDirectory.Replace("\\", "/");
			}
			if (Application.isMobilePlatform || Application.isConsolePlatform)
			{
				return Application.streamingAssetsPath;
			}
			return "file://" + Application.streamingAssetsPath;
		}

		// Token: 0x06001486 RID: 5254 RVA: 0x0005B35E File Offset: 0x0005955E
		public static void SetSourceAssetBundleDirectory(string relativePath)
		{
			AssetBundleManager.BaseDownloadingURL = AssetBundleManager.GetStreamingAssetsPath() + relativePath;
		}

		// Token: 0x06001487 RID: 5255 RVA: 0x0005B370 File Offset: 0x00059570
		public static void SetSourceAssetBundleURL(string absolutePath)
		{
			AssetBundleManager.BaseDownloadingURL = absolutePath + Utility.GetPlatformName() + "/";
		}

		// Token: 0x06001488 RID: 5256 RVA: 0x0005B388 File Offset: 0x00059588
		public static void SetDevelopmentAssetBundleServer()
		{
			TextAsset textAsset = Resources.Load("AssetBundleServerURL") as TextAsset;
			string text = (textAsset != null) ? textAsset.text.Trim() : null;
			if (text == null || text.Length == 0)
			{
				Debug.LogError("Development Server URL could not be found.");
				return;
			}
			AssetBundleManager.SetSourceAssetBundleURL(text);
		}

		// Token: 0x06001489 RID: 5257 RVA: 0x0005B3DC File Offset: 0x000595DC
		public static LoadedAssetBundle GetLoadedAssetBundle(string assetBundleName, out string error)
		{
			if (AssetBundleManager.m_DownloadingErrors.TryGetValue(assetBundleName, out error))
			{
				return null;
			}
			LoadedAssetBundle loadedAssetBundle = null;
			AssetBundleManager.m_LoadedAssetBundles.TryGetValue(assetBundleName, out loadedAssetBundle);
			if (loadedAssetBundle == null)
			{
				return null;
			}
			string[] array = null;
			if (!AssetBundleManager.m_Dependencies.TryGetValue(assetBundleName, out array))
			{
				return loadedAssetBundle;
			}
			foreach (string key in array)
			{
				if (AssetBundleManager.m_DownloadingErrors.TryGetValue(assetBundleName, out error))
				{
					return loadedAssetBundle;
				}
				LoadedAssetBundle loadedAssetBundle2;
				AssetBundleManager.m_LoadedAssetBundles.TryGetValue(key, out loadedAssetBundle2);
				if (loadedAssetBundle2 == null)
				{
					return null;
				}
			}
			return loadedAssetBundle;
		}

		// Token: 0x0600148A RID: 5258 RVA: 0x0005B45C File Offset: 0x0005965C
		public static AssetBundleLoadManifestOperation Initialize()
		{
			return AssetBundleManager.Initialize(Utility.GetPlatformName());
		}

		// Token: 0x0600148B RID: 5259 RVA: 0x0005B468 File Offset: 0x00059668
		public static AssetBundleLoadManifestOperation Initialize(string manifestAssetBundleName)
		{
			Object.DontDestroyOnLoad(new GameObject("AssetBundleManager", new Type[]
			{
				typeof(AssetBundleManager)
			}));
			AssetBundleManager.LoadAssetBundle(manifestAssetBundleName, true);
			AssetBundleLoadManifestOperation assetBundleLoadManifestOperation = new AssetBundleLoadManifestOperation(manifestAssetBundleName, "AssetBundleManifest", typeof(AssetBundleManifest));
			AssetBundleManager.m_InProgressOperations.Add(assetBundleLoadManifestOperation);
			return assetBundleLoadManifestOperation;
		}

		// Token: 0x0600148C RID: 5260 RVA: 0x0005B4C0 File Offset: 0x000596C0
		protected static void LoadAssetBundle(string assetBundleName, bool isLoadingAssetBundleManifest = false)
		{
			AssetBundleManager.Log(AssetBundleManager.LogType.Info, "Loading Asset Bundle " + (isLoadingAssetBundleManifest ? "Manifest: " : ": ") + assetBundleName);
			if (!isLoadingAssetBundleManifest && AssetBundleManager.m_AssetBundleManifest == null)
			{
				Debug.LogError("Please initialize AssetBundleManifest by calling AssetBundleManager.Initialize()");
				return;
			}
			if (!AssetBundleManager.LoadAssetBundleInternal(assetBundleName, isLoadingAssetBundleManifest) && !isLoadingAssetBundleManifest)
			{
				AssetBundleManager.LoadDependencies(assetBundleName);
			}
		}

		// Token: 0x0600148D RID: 5261 RVA: 0x0005B51C File Offset: 0x0005971C
		protected static string RemapVariantName(string assetBundleName)
		{
			string[] allAssetBundlesWithVariant = AssetBundleManager.m_AssetBundleManifest.GetAllAssetBundlesWithVariant();
			string[] array = assetBundleName.Split(new char[]
			{
				'.'
			});
			int num = int.MaxValue;
			int num2 = -1;
			for (int i = 0; i < allAssetBundlesWithVariant.Length; i++)
			{
				string[] array2 = allAssetBundlesWithVariant[i].Split(new char[]
				{
					'.'
				});
				if (!(array2[0] != array[0]))
				{
					int num3 = Array.IndexOf<string>(AssetBundleManager.m_ActiveVariants, array2[1]);
					if (num3 == -1)
					{
						num3 = 2147483646;
					}
					if (num3 < num)
					{
						num = num3;
						num2 = i;
					}
				}
			}
			if (num == 2147483646)
			{
				Debug.LogWarning("Ambigious asset bundle variant chosen because there was no matching active variant: " + allAssetBundlesWithVariant[num2]);
			}
			if (num2 != -1)
			{
				return allAssetBundlesWithVariant[num2];
			}
			return assetBundleName;
		}

		// Token: 0x0600148E RID: 5262 RVA: 0x0005B5D0 File Offset: 0x000597D0
		protected static bool LoadAssetBundleInternal(string assetBundleName, bool isLoadingAssetBundleManifest)
		{
			LoadedAssetBundle loadedAssetBundle = null;
			AssetBundleManager.m_LoadedAssetBundles.TryGetValue(assetBundleName, out loadedAssetBundle);
			if (loadedAssetBundle != null)
			{
				loadedAssetBundle.m_ReferencedCount++;
				return true;
			}
			if (AssetBundleManager.m_DownloadingWWWs.ContainsKey(assetBundleName))
			{
				return true;
			}
			string url = AssetBundleManager.m_BaseDownloadingURL + assetBundleName;
			WWW value;
			if (isLoadingAssetBundleManifest)
			{
				value = new WWW(url);
			}
			else
			{
				value = WWW.LoadFromCacheOrDownload(url, AssetBundleManager.m_AssetBundleManifest.GetAssetBundleHash(assetBundleName), 0U);
			}
			AssetBundleManager.m_DownloadingWWWs.Add(assetBundleName, value);
			return false;
		}

		// Token: 0x0600148F RID: 5263 RVA: 0x0005B64C File Offset: 0x0005984C
		protected static void LoadDependencies(string assetBundleName)
		{
			if (AssetBundleManager.m_AssetBundleManifest == null)
			{
				Debug.LogError("Please initialize AssetBundleManifest by calling AssetBundleManager.Initialize()");
				return;
			}
			string[] allDependencies = AssetBundleManager.m_AssetBundleManifest.GetAllDependencies(assetBundleName);
			if (allDependencies.Length == 0)
			{
				return;
			}
			for (int i = 0; i < allDependencies.Length; i++)
			{
				allDependencies[i] = AssetBundleManager.RemapVariantName(allDependencies[i]);
			}
			AssetBundleManager.m_Dependencies.Add(assetBundleName, allDependencies);
			for (int j = 0; j < allDependencies.Length; j++)
			{
				AssetBundleManager.LoadAssetBundleInternal(allDependencies[j], false);
			}
		}

		// Token: 0x06001490 RID: 5264 RVA: 0x0005B6BF File Offset: 0x000598BF
		public static void UnloadAssetBundle(string assetBundleName)
		{
			AssetBundleManager.UnloadAssetBundleInternal(assetBundleName);
			AssetBundleManager.UnloadDependencies(assetBundleName);
		}

		// Token: 0x06001491 RID: 5265 RVA: 0x0005B6D0 File Offset: 0x000598D0
		protected static void UnloadDependencies(string assetBundleName)
		{
			string[] array = null;
			if (!AssetBundleManager.m_Dependencies.TryGetValue(assetBundleName, out array))
			{
				return;
			}
			string[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				AssetBundleManager.UnloadAssetBundleInternal(array2[i]);
			}
			AssetBundleManager.m_Dependencies.Remove(assetBundleName);
		}

		// Token: 0x06001492 RID: 5266 RVA: 0x0005B714 File Offset: 0x00059914
		protected static void UnloadAssetBundleInternal(string assetBundleName)
		{
			string text;
			LoadedAssetBundle loadedAssetBundle = AssetBundleManager.GetLoadedAssetBundle(assetBundleName, out text);
			if (loadedAssetBundle == null)
			{
				return;
			}
			LoadedAssetBundle loadedAssetBundle2 = loadedAssetBundle;
			int num = loadedAssetBundle2.m_ReferencedCount - 1;
			loadedAssetBundle2.m_ReferencedCount = num;
			if (num == 0)
			{
				loadedAssetBundle.m_AssetBundle.Unload(false);
				AssetBundleManager.m_LoadedAssetBundles.Remove(assetBundleName);
				AssetBundleManager.Log(AssetBundleManager.LogType.Info, assetBundleName + " has been unloaded successfully");
			}
		}

		// Token: 0x06001493 RID: 5267 RVA: 0x0005B76C File Offset: 0x0005996C
		private void Update()
		{
			List<string> list = new List<string>();
			foreach (KeyValuePair<string, WWW> keyValuePair in AssetBundleManager.m_DownloadingWWWs)
			{
				WWW value = keyValuePair.Value;
				if (value.error != null)
				{
					AssetBundleManager.m_DownloadingErrors.Add(keyValuePair.Key, string.Format("Failed downloading bundle {0} from {1}: {2}", keyValuePair.Key, value.url, value.error));
					list.Add(keyValuePair.Key);
				}
				else if (value.isDone)
				{
					if (value.assetBundle == null)
					{
						AssetBundleManager.m_DownloadingErrors.Add(keyValuePair.Key, string.Format("{0} is not a valid asset bundle.", keyValuePair.Key));
						list.Add(keyValuePair.Key);
					}
					else
					{
						AssetBundleManager.m_LoadedAssetBundles.Add(keyValuePair.Key, new LoadedAssetBundle(value.assetBundle));
						list.Add(keyValuePair.Key);
					}
				}
			}
			foreach (string key in list)
			{
				WWW www = AssetBundleManager.m_DownloadingWWWs[key];
				AssetBundleManager.m_DownloadingWWWs.Remove(key);
				www.Dispose();
			}
			int i = 0;
			while (i < AssetBundleManager.m_InProgressOperations.Count)
			{
				if (!AssetBundleManager.m_InProgressOperations[i].Update())
				{
					AssetBundleManager.m_InProgressOperations.RemoveAt(i);
				}
				else
				{
					i++;
				}
			}
		}

		// Token: 0x06001494 RID: 5268 RVA: 0x0005B910 File Offset: 0x00059B10
		public static AssetBundleLoadAssetOperation LoadAssetAsync(string assetBundleName, string assetName, Type type)
		{
			AssetBundleManager.Log(AssetBundleManager.LogType.Info, string.Concat(new string[]
			{
				"Loading ",
				assetName,
				" from ",
				assetBundleName,
				" bundle"
			}));
			assetBundleName = AssetBundleManager.RemapVariantName(assetBundleName);
			AssetBundleManager.LoadAssetBundle(assetBundleName, false);
			AssetBundleLoadAssetOperation assetBundleLoadAssetOperation = new AssetBundleLoadAssetOperationFull(assetBundleName, assetName, type);
			AssetBundleManager.m_InProgressOperations.Add(assetBundleLoadAssetOperation);
			return assetBundleLoadAssetOperation;
		}

		// Token: 0x06001495 RID: 5269 RVA: 0x0005B974 File Offset: 0x00059B74
		public static AssetBundleLoadOperation LoadLevelAsync(string assetBundleName, string levelName, bool isAdditive)
		{
			AssetBundleManager.Log(AssetBundleManager.LogType.Info, string.Concat(new string[]
			{
				"Loading ",
				levelName,
				" from ",
				assetBundleName,
				" bundle"
			}));
			assetBundleName = AssetBundleManager.RemapVariantName(assetBundleName);
			AssetBundleManager.LoadAssetBundle(assetBundleName, false);
			AssetBundleLoadOperation assetBundleLoadOperation = new AssetBundleLoadLevelOperation(assetBundleName, levelName, isAdditive);
			AssetBundleManager.m_InProgressOperations.Add(assetBundleLoadOperation);
			return assetBundleLoadOperation;
		}

		// Token: 0x04000D87 RID: 3463
		private static AssetBundleManager.LogMode m_LogMode = AssetBundleManager.LogMode.All;

		// Token: 0x04000D88 RID: 3464
		private static string m_BaseDownloadingURL = "";

		// Token: 0x04000D89 RID: 3465
		private static string[] m_ActiveVariants = new string[0];

		// Token: 0x04000D8A RID: 3466
		private static AssetBundleManifest m_AssetBundleManifest = null;

		// Token: 0x04000D8B RID: 3467
		private static Dictionary<string, LoadedAssetBundle> m_LoadedAssetBundles = new Dictionary<string, LoadedAssetBundle>();

		// Token: 0x04000D8C RID: 3468
		private static Dictionary<string, WWW> m_DownloadingWWWs = new Dictionary<string, WWW>();

		// Token: 0x04000D8D RID: 3469
		private static Dictionary<string, string> m_DownloadingErrors = new Dictionary<string, string>();

		// Token: 0x04000D8E RID: 3470
		private static List<AssetBundleLoadOperation> m_InProgressOperations = new List<AssetBundleLoadOperation>();

		// Token: 0x04000D8F RID: 3471
		private static Dictionary<string, string[]> m_Dependencies = new Dictionary<string, string[]>();

		// Token: 0x0200040F RID: 1039
		public enum LogMode
		{
			// Token: 0x0400188F RID: 6287
			All,
			// Token: 0x04001890 RID: 6288
			JustErrors
		}

		// Token: 0x02000410 RID: 1040
		public enum LogType
		{
			// Token: 0x04001892 RID: 6290
			Info,
			// Token: 0x04001893 RID: 6291
			Warning,
			// Token: 0x04001894 RID: 6292
			Error
		}
	}
}

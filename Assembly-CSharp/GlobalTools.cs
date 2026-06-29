using System;
using UnityEngine;

// Token: 0x02000133 RID: 307
public class GlobalTools
{
	// Token: 0x06000F03 RID: 3843 RVA: 0x0003E688 File Offset: 0x0003C888
	public static void DebugLog(object log)
	{
		if (Debug.isDebugBuild)
		{
			Debug.Log(log);
		}
	}

	// Token: 0x06000F04 RID: 3844 RVA: 0x0003E697 File Offset: 0x0003C897
	public static void DebugLogError(object error)
	{
		if (Debug.isDebugBuild)
		{
			Debug.LogError(error);
		}
	}

	// Token: 0x06000F05 RID: 3845 RVA: 0x0003E6A6 File Offset: 0x0003C8A6
	public static PlayerInventory GetPlayerInventory()
	{
		if (GlobalTools._inventoryCache == null)
		{
			GlobalTools._inventoryCache = Object.FindObjectOfType<PlayerInventory>();
		}
		return GlobalTools._inventoryCache;
	}

	// Token: 0x06000F06 RID: 3846 RVA: 0x0003E6C4 File Offset: 0x0003C8C4
	public static ThirdPersonController GetThirdPersonController()
	{
		if (GlobalTools._thirdPersonController == null)
		{
			GlobalTools._thirdPersonController = Object.FindObjectOfType<ThirdPersonController>();
		}
		return GlobalTools._thirdPersonController;
	}

	// Token: 0x06000F07 RID: 3847 RVA: 0x0003E6E2 File Offset: 0x0003C8E2
	public static PlayerInteraction GetPlayerInteraction()
	{
		if (GlobalTools._interactionCache == null)
		{
			GlobalTools._interactionCache = Object.FindObjectOfType<PlayerInteraction>();
		}
		return GlobalTools._interactionCache;
	}

	// Token: 0x06000F08 RID: 3848 RVA: 0x0003E700 File Offset: 0x0003C900
	public static GameObject GetPlayer()
	{
		GameObject gameObject = GameObject.FindGameObjectWithTag("Player");
		if (gameObject == null)
		{
			GlobalTools.GetController<GameFlow>().SpawnPlayer(GameSessionData.Instance.GetPlayerTemplate());
			gameObject = GameObject.FindGameObjectWithTag("Player");
		}
		return gameObject;
	}

	// Token: 0x06000F09 RID: 3849 RVA: 0x0003E744 File Offset: 0x0003C944
	public static Shelter GetShelter()
	{
		if (GlobalTools._shelterCache == null)
		{
			GameObject gameObject = GameObject.FindGameObjectWithTag("Exit");
			if (gameObject != null)
			{
				GlobalTools._shelterCache = gameObject.GetComponent<Shelter>();
			}
		}
		return GlobalTools._shelterCache;
	}

	// Token: 0x06000F0A RID: 3850 RVA: 0x0003E782 File Offset: 0x0003C982
	public static ShelterInventory GetShelterInventory()
	{
		if (GlobalTools._shelterInventoryCache == null)
		{
			GlobalTools._shelterInventoryCache = Object.FindObjectOfType<ShelterInventory>();
		}
		return GlobalTools._shelterInventoryCache;
	}

	// Token: 0x06000F0B RID: 3851 RVA: 0x0003E7A0 File Offset: 0x0003C9A0
	public static void HandleErrorMenu()
	{
		if (GameObject.Find("ErrorReportMenu") == null)
		{
			GlobalTools.OpenErrorMenu();
			return;
		}
		GlobalTools.CloseErrorMenu();
	}

	// Token: 0x06000F0C RID: 3852 RVA: 0x0003E7C0 File Offset: 0x0003C9C0
	public static void OpenErrorMenu()
	{
		GameObject gameObject = GameObject.Find("ErrorReportMenu");
		if (gameObject == null)
		{
			GameObject gameObject2 = GameObject.FindGameObjectWithTag("DebugPlaceholder");
			if (gameObject2 != null)
			{
				gameObject = (Object.Instantiate(Resources.Load("ErrorReportMenu")) as GameObject);
				if (gameObject != null)
				{
					gameObject2.GetComponent<dfControl>().IsInteractive = true;
					gameObject.name = "ErrorReportMenu";
					gameObject.transform.parent = gameObject2.transform;
				}
			}
		}
	}

	// Token: 0x06000F0D RID: 3853 RVA: 0x0003E83C File Offset: 0x0003CA3C
	public static void CloseErrorMenu()
	{
		GameObject gameObject = GameObject.Find("ErrorReportMenu");
		if (gameObject != null)
		{
			gameObject.transform.parent.GetComponent<dfControl>().IsInteractive = false;
			Object.Destroy(gameObject);
		}
	}

	// Token: 0x06000F0E RID: 3854 RVA: 0x0003E879 File Offset: 0x0003CA79
	public static T GetController<T>() where T : Component
	{
		return GameObject.FindGameObjectWithTag("GameController").GetComponent<T>();
	}

	// Token: 0x06000F0F RID: 3855 RVA: 0x0003E88A File Offset: 0x0003CA8A
	public static AudioSource PlaySound(AudioEntry sound, bool loop = false, float delay = 0f, float volume = 3.4028235E+38f, float pitch = 3.4028235E+38f, Transform location = null)
	{
		return null;
	}

	// Token: 0x06000F10 RID: 3856 RVA: 0x0003E88D File Offset: 0x0003CA8D
	public static AudioSource PlaySound(AudioClip sound, bool loop = false, float delay = 0f, float volume = 3.4028235E+38f, float pitch = 3.4028235E+38f, Transform location = null)
	{
		return null;
	}

	// Token: 0x06000F11 RID: 3857 RVA: 0x0003E890 File Offset: 0x0003CA90
	public static AudioSource PlaySound(string soundName, bool loop = false, float delay = 0f, float volume = 3.4028235E+38f, float pitch = 3.4028235E+38f, Transform location = null)
	{
		return null;
	}

	// Token: 0x06000F12 RID: 3858 RVA: 0x0003E893 File Offset: 0x0003CA93
	public static void StopSound(AudioSource sound, bool crossOut = false, float crossTime = 1f)
	{
	}

	// Token: 0x06000F13 RID: 3859 RVA: 0x0003E895 File Offset: 0x0003CA95
	public static int GetBitfieldValue(int val)
	{
		return (int)Mathf.Pow(10f, (float)val);
	}

	// Token: 0x06000F14 RID: 3860 RVA: 0x0003E8A4 File Offset: 0x0003CAA4
	public static bool TestBitfieldValue(int v1, int v2)
	{
		return v1 == v2;
	}

	// Token: 0x06000F15 RID: 3861 RVA: 0x0003E8AC File Offset: 0x0003CAAC
	public static bool TestBitfield(int testedDigit, int val)
	{
		int i = 1;
		int num = 1;
		while (i < val)
		{
			num++;
			i *= 10;
		}
		if (i != val)
		{
			num--;
		}
		int num2 = num - testedDigit;
		int num3 = 0;
		int num4 = val;
		int num5 = 0;
		while (num4 != 0 && num5 < num2)
		{
			num3 = num4 % 10;
			num4 /= 10;
			num5++;
		}
		return (num4 != 0 || num5 >= num2) && num3 > 0;
	}

	// Token: 0x04000916 RID: 2326
	private static PlayerInventory _inventoryCache;

	// Token: 0x04000917 RID: 2327
	private static ThirdPersonController _thirdPersonController;

	// Token: 0x04000918 RID: 2328
	private static PlayerInteraction _interactionCache;

	// Token: 0x04000919 RID: 2329
	private static GameObject _playerCache;

	// Token: 0x0400091A RID: 2330
	private static Shelter _shelterCache;

	// Token: 0x0400091B RID: 2331
	private static ShelterInventory _shelterInventoryCache;
}

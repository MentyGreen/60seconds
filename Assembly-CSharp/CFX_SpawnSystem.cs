using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000E8 RID: 232
public class CFX_SpawnSystem : MonoBehaviour
{
	// Token: 0x06000B9C RID: 2972 RVA: 0x00032A0C File Offset: 0x00030C0C
	public static GameObject GetNextObject(GameObject sourceObj, bool activateObject = true)
	{
		int instanceID = sourceObj.GetInstanceID();
		if (!CFX_SpawnSystem.instance.poolCursors.ContainsKey(instanceID))
		{
			Debug.LogError(string.Concat(new string[]
			{
				"[CFX_SpawnSystem.GetNextPoolObject()] Object hasn't been preloaded: ",
				sourceObj.name,
				" (ID:",
				instanceID.ToString(),
				")"
			}));
			return null;
		}
		int index = CFX_SpawnSystem.instance.poolCursors[instanceID];
		Dictionary<int, int> dictionary = CFX_SpawnSystem.instance.poolCursors;
		int key = instanceID;
		int num = dictionary[key];
		dictionary[key] = num + 1;
		if (CFX_SpawnSystem.instance.poolCursors[instanceID] >= CFX_SpawnSystem.instance.instantiatedObjects[instanceID].Count)
		{
			CFX_SpawnSystem.instance.poolCursors[instanceID] = 0;
		}
		GameObject gameObject = CFX_SpawnSystem.instance.instantiatedObjects[instanceID][index];
		if (activateObject)
		{
			gameObject.SetActive(true);
		}
		return gameObject;
	}

	// Token: 0x06000B9D RID: 2973 RVA: 0x00032AF9 File Offset: 0x00030CF9
	public static void PreloadObject(GameObject sourceObj, int poolSize = 1)
	{
		CFX_SpawnSystem.instance.addObjectToPool(sourceObj, poolSize);
	}

	// Token: 0x06000B9E RID: 2974 RVA: 0x00032B07 File Offset: 0x00030D07
	public static void UnloadObjects(GameObject sourceObj)
	{
		CFX_SpawnSystem.instance.removeObjectsFromPool(sourceObj);
	}

	// Token: 0x1700024F RID: 591
	// (get) Token: 0x06000B9F RID: 2975 RVA: 0x00032B14 File Offset: 0x00030D14
	public static bool AllObjectsLoaded
	{
		get
		{
			return CFX_SpawnSystem.instance.allObjectsLoaded;
		}
	}

	// Token: 0x06000BA0 RID: 2976 RVA: 0x00032B20 File Offset: 0x00030D20
	private void addObjectToPool(GameObject sourceObject, int number)
	{
		int instanceID = sourceObject.GetInstanceID();
		if (!this.instantiatedObjects.ContainsKey(instanceID))
		{
			this.instantiatedObjects.Add(instanceID, new List<GameObject>());
			this.poolCursors.Add(instanceID, 0);
		}
		for (int i = 0; i < number; i++)
		{
			GameObject gameObject = Object.Instantiate<GameObject>(sourceObject);
			gameObject.SetActive(false);
			CFX_AutoDestructShuriken[] componentsInChildren = gameObject.GetComponentsInChildren<CFX_AutoDestructShuriken>(true);
			for (int j = 0; j < componentsInChildren.Length; j++)
			{
				componentsInChildren[j].OnlyDeactivate = true;
			}
			CFX_LightIntensityFade[] componentsInChildren2 = gameObject.GetComponentsInChildren<CFX_LightIntensityFade>(true);
			for (int j = 0; j < componentsInChildren2.Length; j++)
			{
				componentsInChildren2[j].autodestruct = false;
			}
			this.instantiatedObjects[instanceID].Add(gameObject);
			if (this.hideObjectsInHierarchy)
			{
				gameObject.hideFlags = HideFlags.HideInHierarchy;
			}
		}
	}

	// Token: 0x06000BA1 RID: 2977 RVA: 0x00032BEC File Offset: 0x00030DEC
	private void removeObjectsFromPool(GameObject sourceObject)
	{
		int instanceID = sourceObject.GetInstanceID();
		if (!this.instantiatedObjects.ContainsKey(instanceID))
		{
			Debug.LogWarning(string.Concat(new string[]
			{
				"[CFX_SpawnSystem.removeObjectsFromPool()] There aren't any preloaded object for: ",
				sourceObject.name,
				" (ID:",
				instanceID.ToString(),
				")"
			}));
			return;
		}
		for (int i = this.instantiatedObjects[instanceID].Count - 1; i >= 0; i--)
		{
			Object obj = this.instantiatedObjects[instanceID][i];
			this.instantiatedObjects[instanceID].RemoveAt(i);
			Object.Destroy(obj);
		}
		this.instantiatedObjects.Remove(instanceID);
		this.poolCursors.Remove(instanceID);
	}

	// Token: 0x06000BA2 RID: 2978 RVA: 0x00032CAB File Offset: 0x00030EAB
	private void Awake()
	{
		if (CFX_SpawnSystem.instance != null)
		{
			Debug.LogWarning("CFX_SpawnSystem: There should only be one instance of CFX_SpawnSystem per Scene!");
		}
		CFX_SpawnSystem.instance = this;
	}

	// Token: 0x06000BA3 RID: 2979 RVA: 0x00032CCC File Offset: 0x00030ECC
	private void Start()
	{
		this.allObjectsLoaded = false;
		for (int i = 0; i < this.objectsToPreload.Length; i++)
		{
			CFX_SpawnSystem.PreloadObject(this.objectsToPreload[i], this.objectsToPreloadTimes[i]);
		}
		this.allObjectsLoaded = true;
	}

	// Token: 0x040005F9 RID: 1529
	private static CFX_SpawnSystem instance;

	// Token: 0x040005FA RID: 1530
	public GameObject[] objectsToPreload = new GameObject[0];

	// Token: 0x040005FB RID: 1531
	public int[] objectsToPreloadTimes = new int[0];

	// Token: 0x040005FC RID: 1532
	public bool hideObjectsInHierarchy;

	// Token: 0x040005FD RID: 1533
	private bool allObjectsLoaded;

	// Token: 0x040005FE RID: 1534
	private Dictionary<int, List<GameObject>> instantiatedObjects = new Dictionary<int, List<GameObject>>();

	// Token: 0x040005FF RID: 1535
	private Dictionary<int, int> poolCursors = new Dictionary<int, int>();
}

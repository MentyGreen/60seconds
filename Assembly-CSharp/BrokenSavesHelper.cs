using System;
using RG.Core.SaveSystem;
using RG.Parsecs.EventEditor;
using UnityEngine;

// Token: 0x0200015A RID: 346
public class BrokenSavesHelper : MonoBehaviour
{
	// Token: 0x06000FF9 RID: 4089 RVA: 0x00041E4C File Offset: 0x0004004C
	private void Start()
	{
		Object.DontDestroyOnLoad(base.gameObject);
	}

	// Token: 0x06000FFA RID: 4090 RVA: 0x00041E5C File Offset: 0x0004005C
	private void Update()
	{
		if (StorageDataManager.SURVIVAL_SAVE_CORRUPTED)
		{
			StorageDataManager.SURVIVAL_SAVE_CORRUPTED = false;
			Debug.Log("Survival save was corrupted and deleted, need to change continue available");
			this._continueAvailable.Value = false;
			StorageDataManager.TheInstance.Save("GlobalGameData", delegate()
			{
				Debug.Log("Saved Global Game Data");
			}, delegate()
			{
				Debug.Log("Failed to save Global Game Data");
			}, true, false);
		}
	}

	// Token: 0x040009E9 RID: 2537
	[SerializeField]
	private GlobalBoolVariable _continueAvailable;

	// Token: 0x040009EA RID: 2538
	private const string GlobalGameDataSaveName = "GlobalGameData";
}

using System;
using DunGen.Graph;
using UnityEngine;

// Token: 0x0200010A RID: 266
[Serializable]
public struct SLevelData
{
	// Token: 0x170002A3 RID: 675
	// (get) Token: 0x06000CEE RID: 3310 RVA: 0x00036FBA File Offset: 0x000351BA
	// (set) Token: 0x06000CEF RID: 3311 RVA: 0x00036FC2 File Offset: 0x000351C2
	public GameObject HousePrefab
	{
		get
		{
			return this._housePrefab;
		}
		set
		{
			this._housePrefab = value;
		}
	}

	// Token: 0x170002A4 RID: 676
	// (get) Token: 0x06000CF0 RID: 3312 RVA: 0x00036FCB File Offset: 0x000351CB
	// (set) Token: 0x06000CF1 RID: 3313 RVA: 0x00036FD3 File Offset: 0x000351D3
	public DungeonFlow FlowData
	{
		get
		{
			return this._flowData;
		}
		set
		{
			this._flowData = value;
		}
	}

	// Token: 0x170002A5 RID: 677
	// (get) Token: 0x06000CF2 RID: 3314 RVA: 0x00036FDC File Offset: 0x000351DC
	// (set) Token: 0x06000CF3 RID: 3315 RVA: 0x00036FE4 File Offset: 0x000351E4
	public GameObject PlayerTemplate
	{
		get
		{
			return this._playerTemplate;
		}
		set
		{
			this._playerTemplate = value;
		}
	}

	// Token: 0x170002A6 RID: 678
	// (get) Token: 0x06000CF4 RID: 3316 RVA: 0x00036FED File Offset: 0x000351ED
	// (set) Token: 0x06000CF5 RID: 3317 RVA: 0x00036FF5 File Offset: 0x000351F5
	public GameObject DoloresTemplate
	{
		get
		{
			return this._doloresTemplate;
		}
		set
		{
			this._doloresTemplate = value;
		}
	}

	// Token: 0x06000CF6 RID: 3318 RVA: 0x00036FFE File Offset: 0x000351FE
	public GameObject GetLevelPrefab(int index)
	{
		if (this._levelPrefabs != null && this._levelPrefabs.Length != 0 && this._levelPrefabs.Length > index)
		{
			return this._levelPrefabs[index];
		}
		return null;
	}

	// Token: 0x06000CF7 RID: 3319 RVA: 0x00037026 File Offset: 0x00035226
	public GameObject GetRandomLevelPrefab()
	{
		if (this._levelPrefabs != null && this._levelPrefabs.Length != 0)
		{
			return this._levelPrefabs[Random.Range(0, this._levelPrefabs.Length)];
		}
		return null;
	}

	// Token: 0x04000707 RID: 1799
	[SerializeField]
	private GameObject _housePrefab;

	// Token: 0x04000708 RID: 1800
	[SerializeField]
	private DungeonFlow _flowData;

	// Token: 0x04000709 RID: 1801
	[SerializeField]
	private GameObject _playerTemplate;

	// Token: 0x0400070A RID: 1802
	[SerializeField]
	private GameObject _doloresTemplate;

	// Token: 0x0400070B RID: 1803
	[SerializeField]
	private string _levelPrefabsPath;

	// Token: 0x0400070C RID: 1804
	[SerializeField]
	private GameObject[] _levelPrefabs;
}

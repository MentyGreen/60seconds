using System;
using UnityEngine;

// Token: 0x02000115 RID: 277
[Serializable]
public class QualitySetting
{
	// Token: 0x06000D8D RID: 3469 RVA: 0x00038734 File Offset: 0x00036934
	public void Set()
	{
		string[] names = QualitySettings.names;
		for (int i = 0; i < names.Length; i++)
		{
			if (names[i] == this._key)
			{
				QualitySettings.SetQualityLevel(i, true);
				return;
			}
		}
	}

	// Token: 0x170002DE RID: 734
	// (get) Token: 0x06000D8E RID: 3470 RVA: 0x0003876D File Offset: 0x0003696D
	public string Name
	{
		get
		{
			return this._name;
		}
	}

	// Token: 0x170002DF RID: 735
	// (get) Token: 0x06000D8F RID: 3471 RVA: 0x00038775 File Offset: 0x00036975
	public string Key
	{
		get
		{
			return this._key;
		}
	}

	// Token: 0x04000757 RID: 1879
	[SerializeField]
	private string _name = string.Empty;

	// Token: 0x04000758 RID: 1880
	[SerializeField]
	private string _key = string.Empty;
}

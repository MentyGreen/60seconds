using System;
using UnityEngine;

// Token: 0x02000138 RID: 312
public struct SObjectDataLog
{
	// Token: 0x06000F3D RID: 3901 RVA: 0x0003EE96 File Offset: 0x0003D096
	public SObjectDataLog(float time, Vector3 pos, Quaternion rot, string id)
	{
		this.BaseData = new SBaseDataLog(time, pos, rot);
		this.Id = id;
	}

	// Token: 0x06000F3E RID: 3902 RVA: 0x0003EEAE File Offset: 0x0003D0AE
	public string GetString(char sep)
	{
		return this.BaseData.GetString(sep) + sep.ToString() + this.Id.ToString();
	}

	// Token: 0x04000930 RID: 2352
	public string Id;

	// Token: 0x04000931 RID: 2353
	public SBaseDataLog BaseData;
}

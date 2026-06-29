using System;
using UnityEngine;

// Token: 0x020000F9 RID: 249
[Serializable]
public struct SCamParams
{
	// Token: 0x17000259 RID: 601
	// (get) Token: 0x06000C08 RID: 3080 RVA: 0x00034D9B File Offset: 0x00032F9B
	// (set) Token: 0x06000C09 RID: 3081 RVA: 0x00034DA3 File Offset: 0x00032FA3
	public Vector3 Relocation
	{
		get
		{
			return this._relocation;
		}
		set
		{
			this._relocation = value;
		}
	}

	// Token: 0x1700025A RID: 602
	// (get) Token: 0x06000C0A RID: 3082 RVA: 0x00034DAC File Offset: 0x00032FAC
	// (set) Token: 0x06000C0B RID: 3083 RVA: 0x00034DB4 File Offset: 0x00032FB4
	public float Time
	{
		get
		{
			return this._time;
		}
		set
		{
			this._time = value;
		}
	}

	// Token: 0x1700025B RID: 603
	// (get) Token: 0x06000C0C RID: 3084 RVA: 0x00034DBD File Offset: 0x00032FBD
	// (set) Token: 0x06000C0D RID: 3085 RVA: 0x00034DC5 File Offset: 0x00032FC5
	public string EaseType
	{
		get
		{
			return this._easeType;
		}
		set
		{
			this._easeType = value;
		}
	}

	// Token: 0x0400063D RID: 1597
	[SerializeField]
	private Vector3 _relocation;

	// Token: 0x0400063E RID: 1598
	[SerializeField]
	private float _time;

	// Token: 0x0400063F RID: 1599
	[SerializeField]
	private string _easeType;
}

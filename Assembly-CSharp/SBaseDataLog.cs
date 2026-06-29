using System;
using UnityEngine;

// Token: 0x02000137 RID: 311
public struct SBaseDataLog
{
	// Token: 0x06000F3B RID: 3899 RVA: 0x0003EE1A File Offset: 0x0003D01A
	public SBaseDataLog(float time, Vector3 pos, Quaternion rot)
	{
		this.Time = time;
		this.Position = pos;
		this.Rotation = rot;
	}

	// Token: 0x06000F3C RID: 3900 RVA: 0x0003EE34 File Offset: 0x0003D034
	public string GetString(char sep)
	{
		return string.Concat(new string[]
		{
			this.Time.ToString(),
			sep.ToString(),
			this.Position.ToString(),
			sep.ToString(),
			this.Rotation.ToString()
		});
	}

	// Token: 0x0400092D RID: 2349
	public float Time;

	// Token: 0x0400092E RID: 2350
	public Vector3 Position;

	// Token: 0x0400092F RID: 2351
	public Quaternion Rotation;
}

using System;

// Token: 0x0200010F RID: 271
[Serializable]
public struct SKeyValuePair
{
	// Token: 0x06000D4B RID: 3403 RVA: 0x000377AB File Offset: 0x000359AB
	public SKeyValuePair(string id, string val)
	{
		this.Id = id;
		this.Val = val;
	}

	// Token: 0x0400073B RID: 1851
	public string Id;

	// Token: 0x0400073C RID: 1852
	public string Val;
}

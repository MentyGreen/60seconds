using System;
using UnityEngine;

// Token: 0x0200010B RID: 267
[Serializable]
public struct SPairValue
{
	// Token: 0x170002A7 RID: 679
	// (get) Token: 0x06000CF8 RID: 3320 RVA: 0x00037050 File Offset: 0x00035250
	// (set) Token: 0x06000CF9 RID: 3321 RVA: 0x00037058 File Offset: 0x00035258
	public int Value1
	{
		get
		{
			return this._value1;
		}
		set
		{
			this._value1 = value;
		}
	}

	// Token: 0x170002A8 RID: 680
	// (get) Token: 0x06000CFA RID: 3322 RVA: 0x00037061 File Offset: 0x00035261
	// (set) Token: 0x06000CFB RID: 3323 RVA: 0x00037069 File Offset: 0x00035269
	public int Value2
	{
		get
		{
			return this._value2;
		}
		set
		{
			this._value2 = value;
		}
	}

	// Token: 0x170002A9 RID: 681
	// (get) Token: 0x06000CFC RID: 3324 RVA: 0x00037072 File Offset: 0x00035272
	public int Value
	{
		get
		{
			return Random.Range(this._value1, this._value2 + 1);
		}
	}

	// Token: 0x170002AA RID: 682
	// (get) Token: 0x06000CFD RID: 3325 RVA: 0x00037087 File Offset: 0x00035287
	public bool Available
	{
		get
		{
			return this._value1 > 0 || this._value2 > 0;
		}
	}

	// Token: 0x06000CFE RID: 3326 RVA: 0x0003709D File Offset: 0x0003529D
	public void GetValues(out string v1, out string sep, out string v2)
	{
		sep = string.Empty;
		v2 = string.Empty;
		v1 = this._value1.ToString();
		if (this._value1 != this._value2)
		{
			v2 = this._value2.ToString();
			sep = "-";
		}
	}

	// Token: 0x0400070D RID: 1805
	[SerializeField]
	private int _value1;

	// Token: 0x0400070E RID: 1806
	[SerializeField]
	private int _value2;
}

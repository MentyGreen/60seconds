using System;
using UnityEngine;

// Token: 0x0200011A RID: 282
[Serializable]
public class TextEntry
{
	// Token: 0x06000E12 RID: 3602 RVA: 0x0003A80E File Offset: 0x00038A0E
	public TextEntry()
	{
	}

	// Token: 0x06000E13 RID: 3603 RVA: 0x0003A834 File Offset: 0x00038A34
	public TextEntry(string txt, string p1 = null, string p2 = null, string p3 = null)
	{
		this._text = txt;
		int paramCount = ((p1 == null) ? 0 : 1) + ((p2 == null) ? 0 : 1) + ((p3 == null) ? 0 : 1);
		this._paramCount = paramCount;
		if (p1 != null)
		{
			this._params[0] = p1;
		}
		if (p2 != null)
		{
			this._params[1] = p2;
		}
		if (p3 != null)
		{
			this._params[2] = p3;
		}
	}

	// Token: 0x06000E14 RID: 3604 RVA: 0x0003A8B2 File Offset: 0x00038AB2
	public string GetParam(int index)
	{
		if (index >= 0 && index < this._params.Length)
		{
			return this._params[index];
		}
		return null;
	}

	// Token: 0x06000E15 RID: 3605 RVA: 0x0003A8D0 File Offset: 0x00038AD0
	public bool TestParam(string param)
	{
		int num = 0;
		while (num < this._params.Length && !string.IsNullOrEmpty(this._params[num]))
		{
			if (this._params[num] == param)
			{
				return true;
			}
			num++;
		}
		return false;
	}

	// Token: 0x17000309 RID: 777
	// (get) Token: 0x06000E16 RID: 3606 RVA: 0x0003A912 File Offset: 0x00038B12
	public string Text
	{
		get
		{
			return this._text;
		}
	}

	// Token: 0x1700030A RID: 778
	// (get) Token: 0x06000E17 RID: 3607 RVA: 0x0003A91A File Offset: 0x00038B1A
	// (set) Token: 0x06000E18 RID: 3608 RVA: 0x0003A924 File Offset: 0x00038B24
	public int ParamCount
	{
		get
		{
			return this._paramCount;
		}
		set
		{
			if (value != this._paramCount && value >= 0)
			{
				string[] array = new string[value];
				this._paramCount = value;
				int num = 0;
				while (num < this._params.Length && num < value)
				{
					array[num] = this._params[num];
					num++;
				}
				this._params = array;
			}
		}
	}

	// Token: 0x0400085B RID: 2139
	private const int DEF_PARAM_LIMIT = 3;

	// Token: 0x0400085C RID: 2140
	[SerializeField]
	private string[] _params = new string[3];

	// Token: 0x0400085D RID: 2141
	[SerializeField]
	private string _text = string.Empty;

	// Token: 0x0400085E RID: 2142
	[SerializeField]
	private int _paramCount = 3;
}

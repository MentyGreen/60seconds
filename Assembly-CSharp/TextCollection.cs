using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200011B RID: 283
[Serializable]
public class TextCollection : ScriptableObject
{
	// Token: 0x06000E1A RID: 3610 RVA: 0x0003A994 File Offset: 0x00038B94
	public string GetParametrisedEntry(List<string> modParameters, string singleParam = null, string[] constParams = null, bool localized = true)
	{
		this._approvedTexts.Clear();
		int num = 0;
		bool flag = !string.IsNullOrEmpty(singleParam);
		if (this._texts != null && (modParameters != null || flag || constParams != null))
		{
			ICollection<string> collection = null;
			if (!flag)
			{
				if (modParameters == null)
				{
					collection = constParams;
					num = constParams.Length;
				}
				else
				{
					collection = modParameters;
					num = modParameters.Count;
				}
			}
			for (int i = 0; i < this._texts.Count; i++)
			{
				bool flag2 = true;
				if (collection != null)
				{
					if (this._texts[i].ParamCount == num)
					{
						using (IEnumerator<string> enumerator = collection.GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								string text = enumerator.Current;
								if (string.IsNullOrEmpty(text))
								{
									break;
								}
								if (!this._texts[i].TestParam(text))
								{
									flag2 = false;
									break;
								}
							}
							goto IL_E9;
						}
					}
					flag2 = false;
				}
				else if (this._texts[i].ParamCount != 1 || !this._texts[i].TestParam(singleParam))
				{
					flag2 = false;
				}
				IL_E9:
				if (flag2)
				{
					this._approvedTexts.Add(this._texts[i]);
				}
			}
		}
		if (this._approvedTexts.Count <= 0)
		{
			return string.Empty;
		}
		string text2 = this._approvedTexts[Random.Range(0, this._approvedTexts.Count)].Text;
		this._approvedTexts.Clear();
		if (localized)
		{
			return Settings.Data.LocalizationManager.GetValue(text2);
		}
		return text2;
	}

	// Token: 0x1700030B RID: 779
	// (get) Token: 0x06000E1B RID: 3611 RVA: 0x0003AB24 File Offset: 0x00038D24
	public int Length
	{
		get
		{
			return this._texts.Count;
		}
	}

	// Token: 0x06000E1C RID: 3612 RVA: 0x0003AB31 File Offset: 0x00038D31
	public TextEntry GetRandomText()
	{
		if (this._texts.Count > 0)
		{
			return this._texts[Random.Range(0, this._texts.Count)];
		}
		return null;
	}

	// Token: 0x1700030C RID: 780
	public TextEntry this[int key]
	{
		get
		{
			if (key < 0 || key >= this._texts.Count)
			{
				return null;
			}
			return this._texts[key];
		}
	}

	// Token: 0x0400085F RID: 2143
	[SerializeField]
	private List<TextEntry> _texts = new List<TextEntry>();

	// Token: 0x04000860 RID: 2144
	private List<TextEntry> _approvedTexts = new List<TextEntry>();
}

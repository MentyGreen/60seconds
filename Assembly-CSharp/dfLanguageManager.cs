using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

// Token: 0x02000068 RID: 104
public class dfLanguageManager : MonoBehaviour
{
	// Token: 0x170001CD RID: 461
	// (get) Token: 0x06000763 RID: 1891 RVA: 0x00020440 File Offset: 0x0001E640
	public dfLanguageCode CurrentLanguage
	{
		get
		{
			return this.currentLanguage;
		}
	}

	// Token: 0x170001CE RID: 462
	// (get) Token: 0x06000764 RID: 1892 RVA: 0x00020448 File Offset: 0x0001E648
	// (set) Token: 0x06000765 RID: 1893 RVA: 0x00020450 File Offset: 0x0001E650
	public TextAsset DataFile
	{
		get
		{
			return this.dataFile;
		}
		set
		{
			if (value != this.dataFile)
			{
				this.dataFile = value;
				this.LoadLanguage(this.currentLanguage);
			}
		}
	}

	// Token: 0x06000766 RID: 1894 RVA: 0x00020474 File Offset: 0x0001E674
	public void Start()
	{
		dfLanguageCode language = this.currentLanguage;
		if (this.currentLanguage == dfLanguageCode.None)
		{
			language = this.SystemLanguageToLanguageCode(Application.systemLanguage);
		}
		this.LoadLanguage(language);
	}

	// Token: 0x06000767 RID: 1895 RVA: 0x000204A4 File Offset: 0x0001E6A4
	public void LoadLanguage(dfLanguageCode language)
	{
		this.currentLanguage = language;
		this.strings.Clear();
		if (this.dataFile != null)
		{
			this.parseDataFile();
		}
		dfControl[] componentsInChildren = base.GetComponentsInChildren<dfControl>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			componentsInChildren[i].Localize();
		}
	}

	// Token: 0x06000768 RID: 1896 RVA: 0x000204F4 File Offset: 0x0001E6F4
	public string GetValue(string key)
	{
		string empty = string.Empty;
		if (this.strings.TryGetValue(key, out empty))
		{
			return empty;
		}
		return key;
	}

	// Token: 0x06000769 RID: 1897 RVA: 0x0002051C File Offset: 0x0001E71C
	private void parseDataFile()
	{
		string text = this.dataFile.text.Replace("\r\n", "\n").Trim();
		List<string> list = new List<string>();
		int i = this.parseLine(text, list, 0);
		int num = list.IndexOf(this.currentLanguage.ToString());
		if (num < 0)
		{
			return;
		}
		List<string> list2 = new List<string>();
		while (i < text.Length)
		{
			i = this.parseLine(text, list2, i);
			if (list2.Count != 0)
			{
				string key = list2[0];
				string value = (num < list2.Count) ? list2[num] : "";
				this.strings[key] = value;
			}
		}
	}

	// Token: 0x0600076A RID: 1898 RVA: 0x000205D0 File Offset: 0x0001E7D0
	private int parseLine(string data, List<string> values, int index)
	{
		values.Clear();
		bool flag = false;
		StringBuilder stringBuilder = new StringBuilder(256);
		while (index < data.Length)
		{
			char c = data[index];
			if (c == '"')
			{
				if (!flag)
				{
					flag = true;
				}
				else if (index + 1 < data.Length && data[index + 1] == c)
				{
					index++;
					stringBuilder.Append(c);
				}
				else
				{
					flag = false;
				}
			}
			else if (c == ',')
			{
				if (flag)
				{
					stringBuilder.Append(c);
				}
				else
				{
					values.Add(stringBuilder.ToString());
					stringBuilder.Length = 0;
				}
			}
			else if (c == '\n')
			{
				if (!flag)
				{
					index++;
					break;
				}
				stringBuilder.Append(c);
			}
			else
			{
				stringBuilder.Append(c);
			}
			index++;
		}
		if (stringBuilder.Length > 0)
		{
			values.Add(stringBuilder.ToString());
		}
		return index;
	}

	// Token: 0x0600076B RID: 1899 RVA: 0x000206A4 File Offset: 0x0001E8A4
	private dfLanguageCode SystemLanguageToLanguageCode(SystemLanguage language)
	{
		switch (language)
		{
		case SystemLanguage.Afrikaans:
			return dfLanguageCode.AF;
		case SystemLanguage.Arabic:
			return dfLanguageCode.AR;
		case SystemLanguage.Basque:
			return dfLanguageCode.EU;
		case SystemLanguage.Belarusian:
			return dfLanguageCode.BE;
		case SystemLanguage.Bulgarian:
			return dfLanguageCode.BG;
		case SystemLanguage.Catalan:
			return dfLanguageCode.CA;
		case SystemLanguage.Chinese:
			return dfLanguageCode.ZH;
		case SystemLanguage.Czech:
			return dfLanguageCode.CS;
		case SystemLanguage.Danish:
			return dfLanguageCode.DA;
		case SystemLanguage.Dutch:
			return dfLanguageCode.NL;
		case SystemLanguage.English:
			return dfLanguageCode.EN;
		case SystemLanguage.Estonian:
			return dfLanguageCode.ES;
		case SystemLanguage.Faroese:
			return dfLanguageCode.FO;
		case SystemLanguage.Finnish:
			return dfLanguageCode.FI;
		case SystemLanguage.French:
			return dfLanguageCode.FR;
		case SystemLanguage.German:
			return dfLanguageCode.DE;
		case SystemLanguage.Greek:
			return dfLanguageCode.EL;
		case SystemLanguage.Hebrew:
			return dfLanguageCode.HE;
		case SystemLanguage.Hungarian:
			return dfLanguageCode.HU;
		case SystemLanguage.Icelandic:
			return dfLanguageCode.IS;
		case SystemLanguage.Indonesian:
			return dfLanguageCode.ID;
		case SystemLanguage.Italian:
			return dfLanguageCode.IT;
		case SystemLanguage.Japanese:
			return dfLanguageCode.JA;
		case SystemLanguage.Korean:
			return dfLanguageCode.KO;
		case SystemLanguage.Latvian:
			return dfLanguageCode.LV;
		case SystemLanguage.Lithuanian:
			return dfLanguageCode.LT;
		case SystemLanguage.Norwegian:
			return dfLanguageCode.NO;
		case SystemLanguage.Polish:
			return dfLanguageCode.PL;
		case SystemLanguage.Portuguese:
			return dfLanguageCode.PT;
		case SystemLanguage.Romanian:
			return dfLanguageCode.RO;
		case SystemLanguage.Russian:
			return dfLanguageCode.RU;
		case SystemLanguage.SerboCroatian:
			return dfLanguageCode.SH;
		case SystemLanguage.Slovak:
			return dfLanguageCode.SK;
		case SystemLanguage.Slovenian:
			return dfLanguageCode.SL;
		case SystemLanguage.Spanish:
			return dfLanguageCode.ES;
		case SystemLanguage.Swedish:
			return dfLanguageCode.SV;
		case SystemLanguage.Thai:
			return dfLanguageCode.TH;
		case SystemLanguage.Turkish:
			return dfLanguageCode.TR;
		case SystemLanguage.Ukrainian:
			return dfLanguageCode.UK;
		case SystemLanguage.Vietnamese:
			return dfLanguageCode.VI;
		case SystemLanguage.Unknown:
			return dfLanguageCode.EN;
		}
		throw new ArgumentException("Unknown system language: " + language.ToString());
	}

	// Token: 0x04000393 RID: 915
	[SerializeField]
	private dfLanguageCode currentLanguage;

	// Token: 0x04000394 RID: 916
	[SerializeField]
	private TextAsset dataFile;

	// Token: 0x04000395 RID: 917
	private Dictionary<string, string> strings = new Dictionary<string, string>();
}

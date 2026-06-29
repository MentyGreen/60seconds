using System;
using UnityEngine;

// Token: 0x02000112 RID: 274
[Serializable]
public class Localization
{
	// Token: 0x170002DB RID: 731
	// (get) Token: 0x06000D5D RID: 3421 RVA: 0x0003797D File Offset: 0x00035B7D
	public bool IsAvailable
	{
		get
		{
			return this._localLanguageManager != null && this._bookAsset != null;
		}
	}

	// Token: 0x06000D5E RID: 3422 RVA: 0x0003799C File Offset: 0x00035B9C
	public void Bind(string langCode)
	{
		dfLanguageManager dfLanguageManager = Object.FindObjectOfType<dfLanguageManager>();
		if (dfLanguageManager == null)
		{
			dfGUIManager dfGUIManager = Object.FindObjectOfType<dfGUIManager>();
			if (dfGUIManager != null)
			{
				dfLanguageManager = dfGUIManager.gameObject.AddComponent<dfLanguageManager>();
			}
		}
		if (dfLanguageManager != null)
		{
			this._localLanguageManager = dfLanguageManager;
			dfLanguageManager.DataFile = this._bookAsset;
			this.SetLanguage(langCode);
		}
	}

	// Token: 0x06000D5F RID: 3423 RVA: 0x000379F8 File Offset: 0x00035BF8
	public void SetLanguage(string code)
	{
		if (this._localLanguageManager != null)
		{
			dfLanguageCode language = (dfLanguageCode)Enum.Parse(typeof(dfLanguageCode), code, true);
			this._localLanguageManager.LoadLanguage(language);
		}
	}

	// Token: 0x06000D60 RID: 3424 RVA: 0x00037A38 File Offset: 0x00035C38
	public string GetValue(string key)
	{
		if (!(this._localLanguageManager != null))
		{
			return key;
		}
		string value = this._localLanguageManager.GetValue(key);
		if (string.IsNullOrEmpty(value))
		{
			return key;
		}
		return value;
	}

	// Token: 0x170002DC RID: 732
	// (get) Token: 0x06000D61 RID: 3425 RVA: 0x00037A6D File Offset: 0x00035C6D
	public TextAsset Book
	{
		get
		{
			return this._bookAsset;
		}
	}

	// Token: 0x0400074B RID: 1867
	[SerializeField]
	protected TextAsset _bookAsset;

	// Token: 0x0400074C RID: 1868
	protected dfLanguageManager _localLanguageManager;
}

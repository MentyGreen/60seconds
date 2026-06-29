using System;
using I2.Loc;
using UnityEngine;

// Token: 0x0200013C RID: 316
public class WordwrapLanguageSetup : MonoBehaviour
{
	// Token: 0x06000F6A RID: 3946 RVA: 0x0003FFB3 File Offset: 0x0003E1B3
	public static void StaticAsianWordWrap()
	{
	}

	// Token: 0x06000F6B RID: 3947 RVA: 0x0003FFB5 File Offset: 0x0003E1B5
	public void AsianWordWrap()
	{
		WordwrapLanguageSetup.StaticAsianWordWrap();
	}

	// Token: 0x06000F6C RID: 3948 RVA: 0x0003FFBC File Offset: 0x0003E1BC
	public static bool IsAsianLanguage()
	{
		return LocalizationManager.CurrentLanguageCode == "zh-CN" || LocalizationManager.CurrentLanguageCode == "ja" || LocalizationManager.CurrentLanguageCode == "ko";
	}
}

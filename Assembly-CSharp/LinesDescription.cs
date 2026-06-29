using System;
using I2.Loc;
using RG.Core.Base;
using UnityEngine;

// Token: 0x0200014E RID: 334
[CreateAssetMenu(menuName = "60 Seconds Remaster!/Lines Description")]
public class LinesDescription : RGScriptableObject
{
	// Token: 0x06000FC6 RID: 4038 RVA: 0x000416D0 File Offset: 0x0003F8D0
	public LinesDescription.LineDescription GetLinesDescriptionForCurrentLanguage()
	{
		string currentLanguage = LocalizationManager.CurrentLanguage;
		if (string.IsNullOrEmpty(this._currentLanguage) || !this._currentLanguage.Equals(currentLanguage))
		{
			this._currentLanguage = currentLanguage;
			bool flag = false;
			for (int i = 0; i < this._linesDescriptions.Length; i++)
			{
				if (this._linesDescriptions[i].Language.Equals(this._currentLanguage))
				{
					this._currentLineDescription = this._linesDescriptions[i];
					flag = true;
				}
			}
			if (!flag)
			{
				this._currentLineDescription = this._fallbackLineDescription;
			}
		}
		return this._currentLineDescription;
	}

	// Token: 0x06000FC7 RID: 4039 RVA: 0x00041761 File Offset: 0x0003F961
	private void OnEnable()
	{
		this._currentLanguage = null;
	}

	// Token: 0x040009C0 RID: 2496
	[SerializeField]
	private LinesDescription.LineDescription[] _linesDescriptions;

	// Token: 0x040009C1 RID: 2497
	[SerializeField]
	private LinesDescription.LineDescription _fallbackLineDescription;

	// Token: 0x040009C2 RID: 2498
	[SerializeField]
	private string _currentLanguage;

	// Token: 0x040009C3 RID: 2499
	private LinesDescription.LineDescription _currentLineDescription;

	// Token: 0x020003CF RID: 975
	[Serializable]
	public struct LineDescription
	{
		// Token: 0x040017C3 RID: 6083
		public int LineHeight;

		// Token: 0x040017C4 RID: 6084
		public int FirstLineHeight;

		// Token: 0x040017C5 RID: 6085
		public float LineSpacing;

		// Token: 0x040017C6 RID: 6086
		public float TooltipLayoutSpacing;

		// Token: 0x040017C7 RID: 6087
		public float AchievementTooltipLayoutSpacing;

		// Token: 0x040017C8 RID: 6088
		public float TextMargin;

		// Token: 0x040017C9 RID: 6089
		public string Language;

		// Token: 0x040017CA RID: 6090
		public float ActionPageTextSize;

		// Token: 0x040017CB RID: 6091
		public float ReportPageTextSize;
	}
}

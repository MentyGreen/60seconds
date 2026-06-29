using System;
using UnityEngine;
using UnityEngine.UI;

namespace RG.SecondsRemaster.Menu
{
	// Token: 0x020002BB RID: 699
	public class SetAchievementTooltipLayoutSpacing : MonoBehaviour
	{
		// Token: 0x060018CA RID: 6346 RVA: 0x0006CA3B File Offset: 0x0006AC3B
		private void OnEnable()
		{
			if (this._layoutGroup != null)
			{
				this._layoutGroup.spacing = this._linesDescription.GetLinesDescriptionForCurrentLanguage().AchievementTooltipLayoutSpacing;
			}
		}

		// Token: 0x04001298 RID: 4760
		[SerializeField]
		private LinesDescription _linesDescription;

		// Token: 0x04001299 RID: 4761
		[SerializeField]
		private VerticalLayoutGroup _layoutGroup;
	}
}

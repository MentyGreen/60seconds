using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000154 RID: 340
[RequireComponent(typeof(VerticalLayoutGroup))]
public class SetTooltipLayoutSpacing : MonoBehaviour
{
	// Token: 0x06000FE1 RID: 4065 RVA: 0x00041B21 File Offset: 0x0003FD21
	private void OnEnable()
	{
		if (this._layoutGroup == null)
		{
			this._layoutGroup = base.GetComponent<VerticalLayoutGroup>();
		}
		this._layoutGroup.spacing = this._linesDescription.GetLinesDescriptionForCurrentLanguage().TooltipLayoutSpacing;
	}

	// Token: 0x040009D5 RID: 2517
	[SerializeField]
	private LinesDescription _linesDescription;

	// Token: 0x040009D6 RID: 2518
	private VerticalLayoutGroup _layoutGroup;
}

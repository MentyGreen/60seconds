using System;
using I2.Loc;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RG.SecondsRemaster.Menu
{
	// Token: 0x02000298 RID: 664
	public class RuleController : MonoBehaviour
	{
		// Token: 0x06001834 RID: 6196 RVA: 0x00069E1C File Offset: 0x0006801C
		public void SetRule(LocalizedString title, Sprite icon)
		{
			this._ruleTitle.text = title;
			this._ruleIcon.sprite = icon;
			this._ruleRect.sizeDelta = new Vector2(this._ruleRect.sizeDelta.x, Mathf.Max(60f, this._ruleTitle.preferredHeight));
		}

		// Token: 0x040011D6 RID: 4566
		[SerializeField]
		private TextMeshProUGUI _ruleTitle;

		// Token: 0x040011D7 RID: 4567
		[SerializeField]
		private Image _ruleIcon;

		// Token: 0x040011D8 RID: 4568
		[SerializeField]
		private RectTransform _ruleRect;
	}
}

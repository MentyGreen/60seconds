using System;
using RG.Parsecs.EventEditor;
using UnityEngine;
using UnityEngine.UI;

namespace RG.SecondsRemaster.Menu
{
	// Token: 0x020002B3 RID: 691
	public class PostApoTooltipController : MonoBehaviour
	{
		// Token: 0x060018AF RID: 6319 RVA: 0x0006C6FD File Offset: 0x0006A8FD
		private void OnEnable()
		{
			this._tooltipBackgroundImage.sprite = (this._isContinueAvailableVariable.Value ? this._postApoSprite : this._normalSprite);
		}

		// Token: 0x04001276 RID: 4726
		[SerializeField]
		private Sprite _normalSprite;

		// Token: 0x04001277 RID: 4727
		[SerializeField]
		private Sprite _postApoSprite;

		// Token: 0x04001278 RID: 4728
		[SerializeField]
		private Image _tooltipBackgroundImage;

		// Token: 0x04001279 RID: 4729
		[SerializeField]
		private GlobalBoolVariable _isContinueAvailableVariable;
	}
}

using System;
using UnityEngine;
using UnityEngine.UI;

namespace RG.SecondsRemaster.Survival
{
	// Token: 0x0200033F RID: 831
	public class JournalTabTooltipSizeFitter : MonoBehaviour
	{
		// Token: 0x06001BB1 RID: 7089 RVA: 0x00076F06 File Offset: 0x00075106
		private void OnEnable()
		{
			this._isRefreshing = true;
			this._canvasGroup.alpha = 0f;
		}

		// Token: 0x06001BB2 RID: 7090 RVA: 0x00076F20 File Offset: 0x00075120
		private void Update()
		{
			if (this._isRefreshing)
			{
				Canvas.ForceUpdateCanvases();
				if (this._rectTransform.sizeDelta.x > this._preferredWidth)
				{
					this._layoutElement.preferredWidth = this._preferredWidth;
				}
				else
				{
					this._layoutElement.preferredWidth = -1f;
				}
				this._isRefreshing = false;
				this._canvasGroup.alpha = 1f;
				Canvas.ForceUpdateCanvases();
			}
		}

		// Token: 0x04001579 RID: 5497
		[SerializeField]
		private float _preferredWidth;

		// Token: 0x0400157A RID: 5498
		[SerializeField]
		private LayoutElement _layoutElement;

		// Token: 0x0400157B RID: 5499
		[SerializeField]
		private RectTransform _rectTransform;

		// Token: 0x0400157C RID: 5500
		[SerializeField]
		private CanvasGroup _canvasGroup;

		// Token: 0x0400157D RID: 5501
		private bool _isRefreshing = true;

		// Token: 0x0400157E RID: 5502
		private const float PREFERRED_WIDTH_DISABLED_VALUE = -1f;
	}
}

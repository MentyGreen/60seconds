using System;
using UnityEngine;

namespace RG.SecondsRemaster.Menu
{
	// Token: 0x020002B7 RID: 695
	public class HideTooltipOnFirstFrame : MonoBehaviour
	{
		// Token: 0x060018BE RID: 6334 RVA: 0x0006C8DB File Offset: 0x0006AADB
		private void OnEnable()
		{
			this._isRefreshing = true;
			this._canvasGroup.alpha = 0f;
		}

		// Token: 0x060018BF RID: 6335 RVA: 0x0006C8F4 File Offset: 0x0006AAF4
		private void Update()
		{
			if (this._isRefreshing)
			{
				Canvas.ForceUpdateCanvases();
				this._canvasGroup.alpha = 1f;
				this._isRefreshing = false;
			}
		}

		// Token: 0x0400128C RID: 4748
		[SerializeField]
		private CanvasGroup _canvasGroup;

		// Token: 0x0400128D RID: 4749
		private bool _isRefreshing;
	}
}

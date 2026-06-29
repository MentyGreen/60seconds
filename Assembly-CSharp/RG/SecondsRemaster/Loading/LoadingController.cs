using System;
using I2.Loc;
using RG.Parsecs.Loading;
using TMPro;
using UnityEngine;

namespace RG.SecondsRemaster.Loading
{
	// Token: 0x020002BF RID: 703
	public class LoadingController : PosterController
	{
		// Token: 0x060018EB RID: 6379 RVA: 0x0006CFE8 File Offset: 0x0006B1E8
		public void SetLoadingScreen(LoadingScreen screen)
		{
			base.SetPoster(screen);
			if (this._middleText != null && !string.IsNullOrEmpty(screen.MiddleText.mTerm))
			{
				this._middleText.text = screen.MiddleText;
				this._middleText.gameObject.GetComponent<Localize>().OnLocalize(false);
			}
		}

		// Token: 0x040012B4 RID: 4788
		[SerializeField]
		private TextMeshProUGUI _middleText;
	}
}

using System;
using TMPro;
using UnityEngine;

namespace RG.SecondsRemaster.Scavenge
{
	// Token: 0x020002CE RID: 718
	public class SergeantSpeechController : MonoBehaviour
	{
		// Token: 0x06001939 RID: 6457 RVA: 0x0006DBF8 File Offset: 0x0006BDF8
		public void ShowText(string text)
		{
			if (!this._textShow.gameObject.activeInHierarchy)
			{
				this._textShow.text = text;
				this._animator.SetTrigger("Show");
				return;
			}
			this._textHide.text = this._textShow.text;
			this._animator.SetTrigger("Switch");
			this._textShow.text = text;
		}

		// Token: 0x0600193A RID: 6458 RVA: 0x0006DC66 File Offset: 0x0006BE66
		public void HideText()
		{
			this._textHide.text = this._textShow.text;
			this._animator.SetTrigger("Hide");
		}

		// Token: 0x04001301 RID: 4865
		[SerializeField]
		private TextMeshProUGUI _textShow;

		// Token: 0x04001302 RID: 4866
		[SerializeField]
		private TextMeshProUGUI _textHide;

		// Token: 0x04001303 RID: 4867
		[SerializeField]
		private Animator _animator;

		// Token: 0x04001304 RID: 4868
		private const string SHOW_TEXT_PARAM_NAME = "Show";

		// Token: 0x04001305 RID: 4869
		private const string HIDE_TEXT_PARAM_NAME = "Hide";

		// Token: 0x04001306 RID: 4870
		private const string SWITCH_TEXT_PARAM_NAME = "Switch";
	}
}

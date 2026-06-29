using System;
using UnityEngine;
using UnityEngine.UI;

namespace RG.SecondsRemaster.Survival
{
	// Token: 0x0200031E RID: 798
	public class CharacterScratchController : MonoBehaviour
	{
		// Token: 0x06001AF8 RID: 6904 RVA: 0x000745C6 File Offset: 0x000727C6
		public void SetScratch(bool characterAvaialable)
		{
			this._scratch.SetActive(!characterAvaialable);
			this._icon.color = (characterAvaialable ? Color.white : new Color(1f, 1f, 1f, 1f));
		}

		// Token: 0x040014B0 RID: 5296
		[SerializeField]
		private GameObject _scratch;

		// Token: 0x040014B1 RID: 5297
		[SerializeField]
		private Image _icon;
	}
}

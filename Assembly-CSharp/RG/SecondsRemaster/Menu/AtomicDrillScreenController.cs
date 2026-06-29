using System;
using UnityEngine;

namespace RG.SecondsRemaster.Menu
{
	// Token: 0x0200028E RID: 654
	public class AtomicDrillScreenController : MonoBehaviour
	{
		// Token: 0x060017F6 RID: 6134 RVA: 0x000690B5 File Offset: 0x000672B5
		public void Awake()
		{
			this._alreadyTriggered = false;
		}

		// Token: 0x060017F7 RID: 6135 RVA: 0x000690BE File Offset: 0x000672BE
		private void OnEnable()
		{
			if (this._remasterMenuManager == null)
			{
				this._remasterMenuManager = Object.FindObjectOfType<RemasterMenuManager>();
			}
		}

		// Token: 0x060017F8 RID: 6136 RVA: 0x000690D9 File Offset: 0x000672D9
		public void StartTutorial()
		{
			if (!this._alreadyTriggered)
			{
				this._alreadyTriggered = true;
				this._remasterMenuManager.StartTutorial();
			}
		}

		// Token: 0x0400119D RID: 4509
		[SerializeField]
		private RemasterMenuManager _remasterMenuManager;

		// Token: 0x0400119E RID: 4510
		private bool _alreadyTriggered;
	}
}

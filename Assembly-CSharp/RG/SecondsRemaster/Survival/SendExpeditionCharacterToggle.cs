using System;
using RG.Parsecs.Common;
using RG.Parsecs.Survival;
using UnityEngine;

namespace RG.SecondsRemaster.Survival
{
	// Token: 0x02000326 RID: 806
	public class SendExpeditionCharacterToggle : ToggleController
	{
		// Token: 0x06001B20 RID: 6944 RVA: 0x00074FD6 File Offset: 0x000731D6
		public override void OnToggleValueChangedAction(bool toggleValue)
		{
			this._sendExpeditionPageController.SetExpedtionCharacter(toggleValue ? this._character : null);
			this._onUiClickedSoundPlayer.PlaySound();
			this._remasterItemsSlotsController.SetHandsInteractable();
		}

		// Token: 0x06001B21 RID: 6945 RVA: 0x00075005 File Offset: 0x00073205
		public void SetToggleInteractable(bool interactable)
		{
			base.Toggle.interactable = interactable;
		}

		// Token: 0x170004E0 RID: 1248
		// (get) Token: 0x06001B22 RID: 6946 RVA: 0x00075013 File Offset: 0x00073213
		public Character Character
		{
			get
			{
				return this._character;
			}
		}

		// Token: 0x040014E0 RID: 5344
		[SerializeField]
		private Character _character;

		// Token: 0x040014E1 RID: 5345
		[SerializeField]
		private SendExpeditionPageController _sendExpeditionPageController;

		// Token: 0x040014E2 RID: 5346
		[SerializeField]
		private OnUIClickedSoundPlayer _onUiClickedSoundPlayer;

		// Token: 0x040014E3 RID: 5347
		[SerializeField]
		private RemasterItemsSlotsController _remasterItemsSlotsController;
	}
}

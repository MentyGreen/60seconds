using System;
using RG.Parsecs.Survival;
using UnityEngine;
using UnityEngine.Events;

namespace RG.SecondsRemaster.Survival
{
	// Token: 0x0200031D RID: 797
	public class CharacterAvailabilityController : MonoBehaviour
	{
		// Token: 0x06001AF5 RID: 6901 RVA: 0x0007451C File Offset: 0x0007271C
		private void Start()
		{
			if (this._refreshOnStart)
			{
				this.RefreshCharacterAvailability();
			}
		}

		// Token: 0x06001AF6 RID: 6902 RVA: 0x0007452C File Offset: 0x0007272C
		public void RefreshCharacterAvailability()
		{
			bool flag = CharacterManager.Instance.GetCharacterList().CharactersInGame.Contains(this._character) && this._character.RuntimeData.IsAlive() && this._character.RuntimeData.IsDrawnOnShip() && !this._character.RuntimeData.IsOnExpedition();
			if (this._checkExpeditionSendPossibility)
			{
				flag = (flag && !this._character.RuntimeData.HasStatusPreventingGoingOnExpeditions());
			}
			this._onCharacterAvailabilityChange.Invoke(flag);
		}

		// Token: 0x040014AC RID: 5292
		[SerializeField]
		private Character _character;

		// Token: 0x040014AD RID: 5293
		[SerializeField]
		private bool _refreshOnStart;

		// Token: 0x040014AE RID: 5294
		[SerializeField]
		private bool _checkExpeditionSendPossibility;

		// Token: 0x040014AF RID: 5295
		[SerializeField]
		private CharacterAvailabilityController.OnRefreshCharacterAvailability _onCharacterAvailabilityChange;

		// Token: 0x02000436 RID: 1078
		[Serializable]
		public class OnRefreshCharacterAvailability : UnityEvent<bool>
		{
		}
	}
}

using System;
using RG.Parsecs.Common;
using RG.Parsecs.Survival;
using UnityEngine;

namespace RG.SecondsRemaster.Survival
{
	// Token: 0x0200032F RID: 815
	public class RationingToggle : ToggleController
	{
		// Token: 0x06001B4C RID: 6988 RVA: 0x00075BD9 File Offset: 0x00073DD9
		public void SetCharacterIndex(int characterIndex)
		{
			this._characterIndex = characterIndex;
		}

		// Token: 0x06001B4D RID: 6989 RVA: 0x00075BE2 File Offset: 0x00073DE2
		public void SetScratchVisibility()
		{
			if (this._scratch != null)
			{
				this._scratch.SetActive(!this._item.IsLockable() || !this._item.BaseRuntimeData.IsAvailable);
			}
		}

		// Token: 0x06001B4E RID: 6990 RVA: 0x00075C20 File Offset: 0x00073E20
		private bool IsItemRationedForAnyCharacter()
		{
			if (this._scratch == null || this._characterList == null || !(this._item is Remedium))
			{
				return false;
			}
			bool flag = false;
			for (int i = 0; i < this._characterList.CharactersInGame.Count; i++)
			{
				Character character = this._characterList.CharactersInGame[i];
				flag |= this._rationingManager.IsRemediumRationedToCharacter(character, (Remedium)this._item);
			}
			return flag;
		}

		// Token: 0x06001B4F RID: 6991 RVA: 0x00075CA4 File Offset: 0x00073EA4
		public void UpdateRemediumScratchVisibility()
		{
			if (this._scratch == null)
			{
				return;
			}
			bool flag = false;
			if (this._item.BaseRuntimeData.IsAvailable)
			{
				if (this._item.IsLockable())
				{
					flag = true;
				}
				else if (this.IsItemRationedForAnyCharacter())
				{
					flag = true;
				}
			}
			this._scratch.SetActive(!flag);
			base.Toggle.interactable = flag;
		}

		// Token: 0x06001B50 RID: 6992 RVA: 0x00075D0C File Offset: 0x00073F0C
		public override void OnToggleValueChangedAction(bool toggleValue)
		{
			if (this._rationingManager.IsItemAvailableForRationing(this._item) || this._rationingData.IsItemRationedForCharacter(this._characterIndex, this._item.BaseStaticData.ItemId))
			{
				this._rationingManager.CharacterRationed(this._characterIndex, this._item);
			}
			else
			{
				base.SetToggleWithoutInvokingValueChange(false);
			}
			this._onUiClickedSoundPlayer.PlaySound();
		}

		// Token: 0x04001513 RID: 5395
		[SerializeField]
		private RationingManager _rationingManager;

		// Token: 0x04001514 RID: 5396
		[SerializeField]
		private RationingData _rationingData;

		// Token: 0x04001515 RID: 5397
		[SerializeField]
		private IItem _item;

		// Token: 0x04001516 RID: 5398
		[SerializeField]
		private OnUIClickedSoundPlayer _onUiClickedSoundPlayer;

		// Token: 0x04001517 RID: 5399
		[SerializeField]
		private GameObject _scratch;

		// Token: 0x04001518 RID: 5400
		[SerializeField]
		private CharacterList _characterList;

		// Token: 0x04001519 RID: 5401
		private int _characterIndex;
	}
}

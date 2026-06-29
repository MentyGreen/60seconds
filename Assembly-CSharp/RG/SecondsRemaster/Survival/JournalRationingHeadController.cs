using System;
using System.Collections.Generic;
using RG.Parsecs.Survival;
using RG.SecondsRemaster.Core;
using UnityEngine;
using UnityEngine.UI;

namespace RG.SecondsRemaster.Survival
{
	// Token: 0x0200032C RID: 812
	public class JournalRationingHeadController : MonoBehaviour
	{
		// Token: 0x06001B3B RID: 6971 RVA: 0x0007568C File Offset: 0x0007388C
		private void SetCharacterIndex()
		{
			if (this._characters == null)
			{
				this._characters = CharacterManager.Instance.GetCharacterList().CharactersInGame;
			}
			this._characterIndex = (this._characters.Contains(this._character) ? this._characters.IndexOf(this._character) : -1);
			this._soupToggle.SetCharacterIndex(this._characterIndex);
			this._medkitToggle.SetCharacterIndex(this._characterIndex);
			this._waterToggle.SetCharacterIndex(this._characterIndex);
		}

		// Token: 0x06001B3C RID: 6972 RVA: 0x00075716 File Offset: 0x00073916
		public void SetScratchVisibility()
		{
			this._soupToggle.SetScratchVisibility();
			this._medkitToggle.UpdateRemediumScratchVisibility();
			this._waterToggle.SetScratchVisibility();
		}

		// Token: 0x06001B3D RID: 6973 RVA: 0x00075739 File Offset: 0x00073939
		public void UpdateMedkitScratch()
		{
			this._medkitToggle.UpdateRemediumScratchVisibility();
		}

		// Token: 0x06001B3E RID: 6974 RVA: 0x00075748 File Offset: 0x00073948
		public void UpdateHeadVisibility()
		{
			this.SetCharacterIndex();
			this._headButton.interactable = (this._characterIndex != -1 && this._character.RuntimeData.IsAlive() && this._character.RuntimeData.IsDrawnOnShip());
			this._scratch.SetActive(!this._headButton.interactable);
			this._medkitHolder.SetActive(this._headButton.interactable && this._rationingManager.CanItemBeRationedToCharacter(this._medkit, this._characterIndex) && this._rationingManager.IsStatusNeededToRationItemToCharacter(this._medkit, this._characterIndex));
			this._medkitHolder.GetComponentInChildren<Toggle>().interactable = (this._medkit.RuntimeData.IsAvailable && !this._medkit.SecondsRemediumRuntimeData.IsDamaged);
			this._waterHolder.SetActive(this._headButton.interactable);
			this._soupHolder.SetActive(this._headButton.interactable);
		}

		// Token: 0x06001B3F RID: 6975 RVA: 0x0007585C File Offset: 0x00073A5C
		public void RationWaterAndSoup()
		{
			bool isOn = true;
			if (this._soupToggle.Toggle.isOn == this._waterToggle.Toggle.isOn)
			{
				isOn = !this._soupToggle.Toggle.isOn;
			}
			this._soupToggle.Toggle.isOn = isOn;
			this._waterToggle.Toggle.isOn = isOn;
		}

		// Token: 0x06001B40 RID: 6976 RVA: 0x000758C3 File Offset: 0x00073AC3
		public void ResetHead()
		{
			this._soupToggle.SetToggleWithoutInvokingValueChange(false);
			this._waterToggle.SetToggleWithoutInvokingValueChange(false);
			this._medkitToggle.SetToggleWithoutInvokingValueChange(false);
		}

		// Token: 0x040014FB RID: 5371
		[SerializeField]
		private RationingManager _rationingManager;

		// Token: 0x040014FC RID: 5372
		[SerializeField]
		private Selectable _headButton;

		// Token: 0x040014FD RID: 5373
		[SerializeField]
		private GameObject _scratch;

		// Token: 0x040014FE RID: 5374
		[SerializeField]
		private GameObject _medkitHolder;

		// Token: 0x040014FF RID: 5375
		[SerializeField]
		private GameObject _waterHolder;

		// Token: 0x04001500 RID: 5376
		[SerializeField]
		private GameObject _soupHolder;

		// Token: 0x04001501 RID: 5377
		[SerializeField]
		private RationingToggle _waterToggle;

		// Token: 0x04001502 RID: 5378
		[SerializeField]
		private RationingToggle _soupToggle;

		// Token: 0x04001503 RID: 5379
		[SerializeField]
		private RationingToggle _medkitToggle;

		// Token: 0x04001504 RID: 5380
		[SerializeField]
		private SecondsCharacter _character;

		// Token: 0x04001505 RID: 5381
		[SerializeField]
		private SecondsRemedium _medkit;

		// Token: 0x04001506 RID: 5382
		private int _characterIndex;

		// Token: 0x04001507 RID: 5383
		private List<Character> _characters;

		// Token: 0x04001508 RID: 5384
		private const int NO_CHARACTER_INDEX = -1;
	}
}

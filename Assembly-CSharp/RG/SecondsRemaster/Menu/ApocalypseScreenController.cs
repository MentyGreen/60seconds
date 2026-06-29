using System;
using RG.Parsecs.Survival;
using UnityEngine;

namespace RG.SecondsRemaster.Menu
{
	// Token: 0x0200028D RID: 653
	public class ApocalypseScreenController : MonoBehaviour
	{
		// Token: 0x060017ED RID: 6125 RVA: 0x00068F97 File Offset: 0x00067197
		public void Awake()
		{
			this._alreadyTriggered = false;
		}

		// Token: 0x060017EE RID: 6126 RVA: 0x00068FA0 File Offset: 0x000671A0
		private void OnEnable()
		{
			if (this._remasterMenuManager == null)
			{
				this._remasterMenuManager = Object.FindObjectOfType<RemasterMenuManager>();
			}
			this.SetAllTogglesDisabled();
			this.EnableRandomCharacter();
			this.SetDefaultDifficulty();
		}

		// Token: 0x060017EF RID: 6127 RVA: 0x00068FD0 File Offset: 0x000671D0
		private void SetAllTogglesDisabled()
		{
			for (int i = 0; i < this._characterToggles.Length; i++)
			{
				this._characterToggles[i].SetToggleWithoutInvokingValueChange(false);
			}
			for (int j = 0; j < this._difficultyToggles.Length; j++)
			{
				this._difficultyToggles[j].SetToggleWithoutInvokingValueChange(false);
			}
		}

		// Token: 0x060017F0 RID: 6128 RVA: 0x00069020 File Offset: 0x00067220
		private void EnableRandomCharacter()
		{
			int num = Random.Range(0, this._characterToggles.Length);
			this._characterToggles[num].Toggle.isOn = true;
			this.SetCharacter(this._characterToggles[num].Character);
		}

		// Token: 0x060017F1 RID: 6129 RVA: 0x00069062 File Offset: 0x00067262
		private void SetDefaultDifficulty()
		{
			this._defaultDifficultyToggle.Toggle.isOn = true;
		}

		// Token: 0x060017F2 RID: 6130 RVA: 0x00069075 File Offset: 0x00067275
		public void SetCharacter(Character character)
		{
			this._remasterMenuManager.CurrentCharacter = character;
		}

		// Token: 0x060017F3 RID: 6131 RVA: 0x00069083 File Offset: 0x00067283
		public void SetDifficulty(DifficultyLevel difficultyLevel)
		{
			this._remasterMenuManager.CurrentDifficultyLevel = difficultyLevel;
		}

		// Token: 0x060017F4 RID: 6132 RVA: 0x00069091 File Offset: 0x00067291
		public void StartApocalypse()
		{
			if (!this._alreadyTriggered)
			{
				this._alreadyTriggered = true;
				this._remasterMenuManager.StartFullGame();
			}
		}

		// Token: 0x04001198 RID: 4504
		[SerializeField]
		private CharacterEventToggleController[] _characterToggles;

		// Token: 0x04001199 RID: 4505
		[SerializeField]
		private UnityEventToggleController[] _difficultyToggles;

		// Token: 0x0400119A RID: 4506
		[SerializeField]
		private UnityEventToggleController _defaultDifficultyToggle;

		// Token: 0x0400119B RID: 4507
		[SerializeField]
		private RemasterMenuManager _remasterMenuManager;

		// Token: 0x0400119C RID: 4508
		private bool _alreadyTriggered;
	}
}

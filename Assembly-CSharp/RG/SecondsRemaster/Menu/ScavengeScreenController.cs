using System;
using RG.Parsecs.Survival;
using UnityEngine;

namespace RG.SecondsRemaster.Menu
{
	// Token: 0x02000299 RID: 665
	public class ScavengeScreenController : MonoBehaviour
	{
		// Token: 0x06001836 RID: 6198 RVA: 0x00069E83 File Offset: 0x00068083
		public void Awake()
		{
			this._alreadyTriggered = false;
		}

		// Token: 0x06001837 RID: 6199 RVA: 0x00069E8C File Offset: 0x0006808C
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

		// Token: 0x06001838 RID: 6200 RVA: 0x00069EBC File Offset: 0x000680BC
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

		// Token: 0x06001839 RID: 6201 RVA: 0x00069F0C File Offset: 0x0006810C
		private void EnableRandomCharacter()
		{
			int num = Random.Range(0, this._characterToggles.Length);
			this._characterToggles[num].Toggle.isOn = true;
			this.SetCharacter(this._characterToggles[num].Character);
		}

		// Token: 0x0600183A RID: 6202 RVA: 0x00069F4E File Offset: 0x0006814E
		private void SetDefaultDifficulty()
		{
			this._defaultDifficultyToggle.Toggle.isOn = true;
		}

		// Token: 0x0600183B RID: 6203 RVA: 0x00069F61 File Offset: 0x00068161
		public void SetCharacter(Character character)
		{
			this._remasterMenuManager.CurrentCharacter = character;
		}

		// Token: 0x0600183C RID: 6204 RVA: 0x00069F6F File Offset: 0x0006816F
		public void SetDifficulty(DifficultyLevel difficultyLevel)
		{
			this._remasterMenuManager.CurrentDifficultyLevel = difficultyLevel;
		}

		// Token: 0x0600183D RID: 6205 RVA: 0x00069F7D File Offset: 0x0006817D
		public void StartScavenge()
		{
			if (!this._alreadyTriggered)
			{
				this._alreadyTriggered = true;
				this._remasterMenuManager.StartScavenge();
			}
		}

		// Token: 0x040011D9 RID: 4569
		[SerializeField]
		private CharacterEventToggleController[] _characterToggles;

		// Token: 0x040011DA RID: 4570
		[SerializeField]
		private UnityEventToggleController[] _difficultyToggles;

		// Token: 0x040011DB RID: 4571
		[SerializeField]
		private UnityEventToggleController _defaultDifficultyToggle;

		// Token: 0x040011DC RID: 4572
		[SerializeField]
		private RemasterMenuManager _remasterMenuManager;

		// Token: 0x040011DD RID: 4573
		private bool _alreadyTriggered;
	}
}

using System;
using UnityEngine;

namespace RG.SecondsRemaster.Menu
{
	// Token: 0x0200029B RID: 667
	public class SurvivalScreenController : MonoBehaviour
	{
		// Token: 0x06001844 RID: 6212 RVA: 0x0006A092 File Offset: 0x00068292
		public void Awake()
		{
			this._alreadyTriggered = false;
		}

		// Token: 0x06001845 RID: 6213 RVA: 0x0006A09B File Offset: 0x0006829B
		private void OnEnable()
		{
			if (this._remasterMenuManager == null)
			{
				this._remasterMenuManager = Object.FindObjectOfType<RemasterMenuManager>();
			}
			this.SetAllTogglesDisabled();
			this.SetDefaultDifficulty();
		}

		// Token: 0x06001846 RID: 6214 RVA: 0x0006A0C4 File Offset: 0x000682C4
		private void SetAllTogglesDisabled()
		{
			for (int i = 0; i < this._difficultyToggles.Length; i++)
			{
				this._difficultyToggles[i].SetToggleWithoutInvokingValueChange(false);
			}
		}

		// Token: 0x06001847 RID: 6215 RVA: 0x0006A0F2 File Offset: 0x000682F2
		private void SetDefaultDifficulty()
		{
			this._defaultDifficultyToggle.Toggle.isOn = true;
		}

		// Token: 0x06001848 RID: 6216 RVA: 0x0006A105 File Offset: 0x00068305
		public void SetDifficulty(DifficultyLevel difficultyLevel)
		{
			this._remasterMenuManager.CurrentDifficultyLevel = difficultyLevel;
		}

		// Token: 0x06001849 RID: 6217 RVA: 0x0006A113 File Offset: 0x00068313
		public void StartSurvival()
		{
			if (!this._alreadyTriggered)
			{
				this._alreadyTriggered = true;
				this._remasterMenuManager.StartSurvival();
			}
		}

		// Token: 0x040011E3 RID: 4579
		[SerializeField]
		private UnityEventToggleController[] _difficultyToggles;

		// Token: 0x040011E4 RID: 4580
		[SerializeField]
		private UnityEventToggleController _defaultDifficultyToggle;

		// Token: 0x040011E5 RID: 4581
		[SerializeField]
		private RemasterMenuManager _remasterMenuManager;

		// Token: 0x040011E6 RID: 4582
		private bool _alreadyTriggered;
	}
}

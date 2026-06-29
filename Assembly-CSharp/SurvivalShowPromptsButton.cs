using System;
using Rewired;
using RG.Parsecs.Survival;
using UnityEngine;

// Token: 0x02000157 RID: 343
public class SurvivalShowPromptsButton : MonoBehaviour
{
	// Token: 0x06000FF1 RID: 4081 RVA: 0x00041CF8 File Offset: 0x0003FEF8
	public void Awake()
	{
		this._player = ReInput.players.GetPlayer(0);
	}

	// Token: 0x06000FF2 RID: 4082 RVA: 0x00041D0C File Offset: 0x0003FF0C
	private void Update()
	{
		if (this._player != null && this._player.controllers.GetLastActiveController() != this._lastController)
		{
			this._lastController = this._player.controllers.GetLastActiveController();
		}
		if (this._lastController != null)
		{
			this._showPromptsRect.gameObject.SetActive(this._lastController is Joystick && this._survivalData.CurrentDay <= 3 && !this._endGameData.RuntimeData.ShouldEndGame);
			return;
		}
		this._showPromptsRect.gameObject.SetActive(false);
	}

	// Token: 0x040009DE RID: 2526
	[SerializeField]
	private RectTransform _showPromptsRect;

	// Token: 0x040009DF RID: 2527
	[SerializeField]
	[Tooltip("Reference to Survival Data scriptable object.")]
	private SurvivalData _survivalData;

	// Token: 0x040009E0 RID: 2528
	[SerializeField]
	private EndGameData _endGameData;

	// Token: 0x040009E1 RID: 2529
	private Player _player;

	// Token: 0x040009E2 RID: 2530
	private Controller _lastController;
}

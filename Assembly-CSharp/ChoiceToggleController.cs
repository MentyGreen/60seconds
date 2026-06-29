using System;
using RG.Parsecs.Common;
using RG.Parsecs.Survival;
using UnityEngine;

// Token: 0x0200014D RID: 333
public class ChoiceToggleController : ToggleController
{
	// Token: 0x06000FC2 RID: 4034 RVA: 0x0004167F File Offset: 0x0003F87F
	private void OnEnable()
	{
		this.RefreshToggle();
	}

	// Token: 0x06000FC3 RID: 4035 RVA: 0x00041687 File Offset: 0x0003F887
	public override void OnToggleValueChangedAction(bool toggleValue)
	{
		this._choiceCardsController.SetCurrentChoice(toggleValue ? this._choiceCardController : null);
		this._onUiClickedSoundPlayer.PlaySound();
	}

	// Token: 0x06000FC4 RID: 4036 RVA: 0x000416AB File Offset: 0x0003F8AB
	public void RefreshToggle()
	{
		if (!base.Toggle.isOn)
		{
			this._choiceCardController.RefreshCard();
		}
	}

	// Token: 0x040009BD RID: 2493
	[SerializeField]
	private ChoiceCardController _choiceCardController;

	// Token: 0x040009BE RID: 2494
	[SerializeField]
	private ChoiceCardsController _choiceCardsController;

	// Token: 0x040009BF RID: 2495
	[SerializeField]
	private OnUIClickedSoundPlayer _onUiClickedSoundPlayer;
}

using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000155 RID: 341
public class SurvivalPrompt : MonoBehaviour
{
	// Token: 0x06000FE3 RID: 4067 RVA: 0x00041B60 File Offset: 0x0003FD60
	public void OnEnable()
	{
		SurvivalPromptsController.Instance.RegisterPrompt(this);
	}

	// Token: 0x06000FE4 RID: 4068 RVA: 0x00041B6D File Offset: 0x0003FD6D
	public void OnDisable()
	{
		SurvivalPromptsController.Instance.UnregisterPrompt(this);
	}

	// Token: 0x06000FE5 RID: 4069 RVA: 0x00041B7A File Offset: 0x0003FD7A
	public void Show()
	{
		if (this._checkButtonInteractable && this._buttonToCheck != null && !this._buttonToCheck.interactable)
		{
			return;
		}
		this._promptCanvasGroup.alpha = 1f;
	}

	// Token: 0x06000FE6 RID: 4070 RVA: 0x00041BB0 File Offset: 0x0003FDB0
	public void Hide()
	{
		this._promptCanvasGroup.alpha = 0f;
	}

	// Token: 0x040009D7 RID: 2519
	[SerializeField]
	private CanvasGroup _promptCanvasGroup;

	// Token: 0x040009D8 RID: 2520
	[SerializeField]
	private bool _checkButtonInteractable;

	// Token: 0x040009D9 RID: 2521
	[SerializeField]
	private Button _buttonToCheck;
}

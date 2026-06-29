using System;
using Rewired;
using RG.Parsecs.Common;
using RG.VirtualInput;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x0200014F RID: 335
public class HandHintController : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler
{
	// Token: 0x06000FC9 RID: 4041 RVA: 0x00041772 File Offset: 0x0003F972
	public void Awake()
	{
		this._player = ReInput.players.GetPlayer(0);
	}

	// Token: 0x06000FCA RID: 4042 RVA: 0x00041785 File Offset: 0x0003F985
	public void Update()
	{
		if (this._isShowed && (!HandHintController.ShouldShowHint || this._player.GetButtonDown(41)))
		{
			this.HideHint();
		}
	}

	// Token: 0x06000FCB RID: 4043 RVA: 0x000417AC File Offset: 0x0003F9AC
	public void TryToShowHint()
	{
		if (HandHintController.ShouldShowHint && this._button.interactable && (Singleton<VirtualInputManager>.Instance.IsPointerMode() || Singleton<VirtualInputManager>.Instance.IsSelectablesMode()) && this._hintRect)
		{
			this._hintRect.gameObject.SetActive(true);
			this._isShowed = true;
		}
	}

	// Token: 0x06000FCC RID: 4044 RVA: 0x0004180A File Offset: 0x0003FA0A
	public void HideHint()
	{
		if (this._hintRect)
		{
			this._hintRect.gameObject.SetActive(false);
			this._isShowed = false;
		}
	}

	// Token: 0x06000FCD RID: 4045 RVA: 0x00041831 File Offset: 0x0003FA31
	public void OnPointerEnter(PointerEventData eventData)
	{
		if (Singleton<VirtualInputManager>.Instance.IsPointerMode())
		{
			this.TryToShowHint();
		}
	}

	// Token: 0x06000FCE RID: 4046 RVA: 0x00041845 File Offset: 0x0003FA45
	public void OnPointerExit(PointerEventData eventData)
	{
		this.HideHint();
	}

	// Token: 0x040009C4 RID: 2500
	public static bool ShouldShowHint = true;

	// Token: 0x040009C5 RID: 2501
	[SerializeField]
	private RectTransform _hintRect;

	// Token: 0x040009C6 RID: 2502
	[SerializeField]
	private Button _button;

	// Token: 0x040009C7 RID: 2503
	private bool _isShowed;

	// Token: 0x040009C8 RID: 2504
	private Player _player;
}

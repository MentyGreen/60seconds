using System;
using RG.Parsecs.Survival;
using RG.VirtualInput;
using UnityEngine;

// Token: 0x0200014C RID: 332
public class CatVirtualButtonHelper : MonoBehaviour
{
	// Token: 0x06000FBE RID: 4030 RVA: 0x00041605 File Offset: 0x0003F805
	public void Setup()
	{
		if (this._catVisualGameObject.activeInHierarchy)
		{
			this._virtualButton.Selectable.interactable = true;
			return;
		}
		this._virtualButton.Selectable.interactable = false;
	}

	// Token: 0x06000FBF RID: 4031 RVA: 0x00041637 File Offset: 0x0003F837
	private void Awake()
	{
		this._endOfDayListener.RegisterOnEndOfDay(new EndOfDayListenerList.OnEndOfDay(this.Setup), "Visuals", 10, this, true);
	}

	// Token: 0x06000FC0 RID: 4032 RVA: 0x00041659 File Offset: 0x0003F859
	private void OnDestroy()
	{
		this._endOfDayListener.UnregisterOnEndOfDay(new EndOfDayListenerList.OnEndOfDay(this.Setup), "Visuals");
	}

	// Token: 0x040009BA RID: 2490
	[SerializeField]
	private VirtualInputButton _virtualButton;

	// Token: 0x040009BB RID: 2491
	[SerializeField]
	private EndOfDayListenerList _endOfDayListener;

	// Token: 0x040009BC RID: 2492
	[SerializeField]
	private GameObject _catVisualGameObject;
}

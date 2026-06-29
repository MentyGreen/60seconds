using System;
using System.Collections.Generic;
using RG.Parsecs.Survival;
using RG.VirtualInput;
using UnityEngine;

// Token: 0x02000150 RID: 336
public class PancakeVirtualButtonHelper : MonoBehaviour
{
	// Token: 0x06000FD1 RID: 4049 RVA: 0x00041860 File Offset: 0x0003FA60
	public void Setup()
	{
		foreach (GameObject gameObject in this._pancakeSkins)
		{
			if (gameObject.activeInHierarchy)
			{
				this._virtualButton.Selectable.interactable = true;
				this._virtualButton.SetPositionTransform(gameObject.transform);
				return;
			}
		}
		this._virtualButton.Selectable.interactable = false;
	}

	// Token: 0x06000FD2 RID: 4050 RVA: 0x000418EC File Offset: 0x0003FAEC
	private void Awake()
	{
		this._endOfDayListener.RegisterOnEndOfDay(new EndOfDayListenerList.OnEndOfDay(this.Setup), "Visuals", 10, this, true);
	}

	// Token: 0x06000FD3 RID: 4051 RVA: 0x0004190E File Offset: 0x0003FB0E
	private void OnDestroy()
	{
		this._endOfDayListener.UnregisterOnEndOfDay(new EndOfDayListenerList.OnEndOfDay(this.Setup), "Visuals");
	}

	// Token: 0x040009C9 RID: 2505
	[SerializeField]
	private VirtualInputButton _virtualButton;

	// Token: 0x040009CA RID: 2506
	[SerializeField]
	private List<GameObject> _pancakeSkins = new List<GameObject>();

	// Token: 0x040009CB RID: 2507
	[SerializeField]
	private EndOfDayListenerList _endOfDayListener;
}

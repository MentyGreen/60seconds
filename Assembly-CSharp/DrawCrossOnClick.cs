using System;
using RG.Parsecs.Common;
using RG.Parsecs.EventEditor;
using RG.Parsecs.Survival;
using RG.SecondsRemaster.Core;
using RG.VirtualInput;
using UnityEngine;
using UnityEngine.EventSystems;

// Token: 0x02000152 RID: 338
public class DrawCrossOnClick : MonoBehaviour
{
	// Token: 0x06000FD9 RID: 4057 RVA: 0x000419A4 File Offset: 0x0003FBA4
	private void Awake()
	{
		this._camera = Camera.main;
		this._endOfDay.RegisterOnEndOfDay(new EndOfDayListenerList.OnEndOfDay(this.OnEndOfDay), "Reset", 1, this, false);
	}

	// Token: 0x06000FDA RID: 4058 RVA: 0x000419D0 File Offset: 0x0003FBD0
	private void OnDestroy()
	{
		this._endOfDay.UnregisterOnEndOfDay(new EndOfDayListenerList.OnEndOfDay(this.OnEndOfDay), "Reset");
	}

	// Token: 0x06000FDB RID: 4059 RVA: 0x000419EE File Offset: 0x0003FBEE
	private void OnDisable()
	{
		this._cross.gameObject.SetActive(false);
	}

	// Token: 0x06000FDC RID: 4060 RVA: 0x00041A01 File Offset: 0x0003FC01
	private void OnEndOfDay()
	{
		if (this._map.IsDamaged() || !this._map.RuntimeData.IsAvailable)
		{
			this._secretBonusVariable.Value = false;
			this._cross.gameObject.SetActive(false);
		}
	}

	// Token: 0x06000FDD RID: 4061 RVA: 0x00041A40 File Offset: 0x0003FC40
	private void OnMouseDown()
	{
		if (!EventSystem.current.IsPointerOverGameObject())
		{
			Vector3 position = this._camera.ScreenToWorldPoint(Singleton<VirtualInputManager>.Instance.GetMousePosition());
			position.z = 0f;
			this._cross.transform.position = position;
			this._cross.gameObject.SetActive(true);
			this._secretBonusVariable.Value = true;
		}
	}

	// Token: 0x040009CE RID: 2510
	[SerializeField]
	private GameObject _cross;

	// Token: 0x040009CF RID: 2511
	private Camera _camera;

	// Token: 0x040009D0 RID: 2512
	[SerializeField]
	private GlobalBoolVariable _secretBonusVariable;

	// Token: 0x040009D1 RID: 2513
	[SerializeField]
	private EndOfDayListenerList _endOfDay;

	// Token: 0x040009D2 RID: 2514
	[SerializeField]
	private SecondsItem _map;
}

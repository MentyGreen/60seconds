using System;
using RG.Parsecs.EventEditor;
using UnityEngine;

// Token: 0x02000147 RID: 327
public class PanelStyleController : MonoBehaviour
{
	// Token: 0x06000FA5 RID: 4005 RVA: 0x000410F4 File Offset: 0x0003F2F4
	private void Start()
	{
		if (this._isContinueAvailable != null && this._isContinueAvailable.Value)
		{
			this._normalPanel.SetActive(true);
			this._postapoPanel.SetActive(true);
		}
		if (this._isContinueAvailable != null && !this._isContinueAvailable.Value)
		{
			this._normalPanel.SetActive(true);
			this._postapoPanel.SetActive(false);
		}
	}

	// Token: 0x06000FA6 RID: 4006 RVA: 0x00041167 File Offset: 0x0003F367
	private void Update()
	{
	}

	// Token: 0x040009A5 RID: 2469
	[SerializeField]
	private GlobalBoolVariable _isContinueAvailable;

	// Token: 0x040009A6 RID: 2470
	[Tooltip("GameObject of given panel (Probably the root object that has 'normal' sprite component)")]
	[SerializeField]
	private GameObject _normalPanel;

	// Token: 0x040009A7 RID: 2471
	[Tooltip("GameObject of postapo overlay for 'normalPanel'")]
	[SerializeField]
	private GameObject _postapoPanel;
}

using System;
using System.Collections.Generic;
using RG.Parsecs.EventEditor;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000141 RID: 321
public class DisableToggleOnDemo : MonoBehaviour
{
	// Token: 0x06000F94 RID: 3988 RVA: 0x00040D80 File Offset: 0x0003EF80
	private void Start()
	{
		if (this._isDemoVariable != null && this._toggleToBlockOnDemo != null)
		{
			this._toggleToBlockOnDemo.interactable = !this._isDemoVariable.Value;
			foreach (GameObject gameObject in this._additionalObjectsToActivateOnDemo)
			{
				gameObject.SetActive(this._isDemoVariable.Value);
			}
		}
	}

	// Token: 0x04000999 RID: 2457
	[SerializeField]
	private GlobalBoolVariable _isDemoVariable;

	// Token: 0x0400099A RID: 2458
	[SerializeField]
	private Toggle _toggleToBlockOnDemo;

	// Token: 0x0400099B RID: 2459
	[SerializeField]
	private List<GameObject> _additionalObjectsToActivateOnDemo = new List<GameObject>();
}

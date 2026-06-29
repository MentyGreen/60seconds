using System;
using System.Collections.Generic;
using RG.Parsecs.EventEditor;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200013F RID: 319
public class DisableButtonOnDemo : MonoBehaviour
{
	// Token: 0x06000F90 RID: 3984 RVA: 0x00040CAC File Offset: 0x0003EEAC
	private void Start()
	{
		if (this._isDemoVariable != null && this._buttonToBlockOnDemo != null)
		{
			this._buttonToBlockOnDemo.interactable = !this._isDemoVariable.Value;
			foreach (GameObject gameObject in this._additionalObjectsToActivateOnDemo)
			{
				gameObject.SetActive(this._isDemoVariable.Value);
			}
		}
	}

	// Token: 0x04000995 RID: 2453
	[SerializeField]
	private GlobalBoolVariable _isDemoVariable;

	// Token: 0x04000996 RID: 2454
	[SerializeField]
	private Button _buttonToBlockOnDemo;

	// Token: 0x04000997 RID: 2455
	[SerializeField]
	private List<GameObject> _additionalObjectsToActivateOnDemo = new List<GameObject>();
}

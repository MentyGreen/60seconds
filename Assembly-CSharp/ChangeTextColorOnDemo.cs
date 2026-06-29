using System;
using RG.Parsecs.EventEditor;
using TMPro;
using UnityEngine;

// Token: 0x0200013E RID: 318
public class ChangeTextColorOnDemo : MonoBehaviour
{
	// Token: 0x06000F8E RID: 3982 RVA: 0x00040C54 File Offset: 0x0003EE54
	private void Start()
	{
		if (this._isDemoVariable != null && this._textToChangeColorOnDemo != null)
		{
			this._textToChangeColorOnDemo.color = (this._isDemoVariable.Value ? this._demoColor : this._fullVersionColor);
		}
	}

	// Token: 0x04000991 RID: 2449
	[SerializeField]
	private GlobalBoolVariable _isDemoVariable;

	// Token: 0x04000992 RID: 2450
	[SerializeField]
	private TextMeshProUGUI _textToChangeColorOnDemo;

	// Token: 0x04000993 RID: 2451
	[SerializeField]
	private Color _demoColor;

	// Token: 0x04000994 RID: 2452
	[SerializeField]
	private Color _fullVersionColor;
}

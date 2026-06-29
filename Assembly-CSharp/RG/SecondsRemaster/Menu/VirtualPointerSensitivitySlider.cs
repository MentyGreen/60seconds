using System;
using RG.Parsecs.EventEditor;
using UnityEngine;
using UnityEngine.UI;

namespace RG.SecondsRemaster.Menu
{
	// Token: 0x020002B2 RID: 690
	public class VirtualPointerSensitivitySlider : MonoBehaviour
	{
		// Token: 0x060018AC RID: 6316 RVA: 0x0006C6CF File Offset: 0x0006A8CF
		private void OnEnable()
		{
			this._slider.value = this._pointerSensitivity.Value;
		}

		// Token: 0x060018AD RID: 6317 RVA: 0x0006C6E7 File Offset: 0x0006A8E7
		public void ChangeSensitivity(float value)
		{
			this._pointerSensitivity.Value = value;
		}

		// Token: 0x04001274 RID: 4724
		[SerializeField]
		private Slider _slider;

		// Token: 0x04001275 RID: 4725
		[SerializeField]
		private GlobalFloatVariable _pointerSensitivity;
	}
}

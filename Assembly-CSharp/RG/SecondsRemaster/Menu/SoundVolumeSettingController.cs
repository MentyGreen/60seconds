using System;
using RG.Parsecs.Common;
using RG.Parsecs.EventEditor;
using UnityEngine;
using UnityEngine.UI;

namespace RG.SecondsRemaster.Menu
{
	// Token: 0x020002B1 RID: 689
	public class SoundVolumeSettingController : MonoBehaviour
	{
		// Token: 0x060018A9 RID: 6313 RVA: 0x0006C5F4 File Offset: 0x0006A7F4
		private void OnEnable()
		{
			if (this._soundVolume.Value < this._slider.minValue)
			{
				this._slider.value = this._slider.maxValue;
				this._soundVolume.Value = this._slider.maxValue;
				return;
			}
			this._slider.value = this._soundVolume.Value;
		}

		// Token: 0x060018AA RID: 6314 RVA: 0x0006C65C File Offset: 0x0006A85C
		public void ChangeVolume(float value)
		{
			this._soundVolume.Value = value;
			if (!this._applyInstantly)
			{
				return;
			}
			AudioManager.Instance.SetSfxVolume(this._soundVolume.Value * this._masterVolume.Value);
			AudioManager.Instance.SetUiVolume(this._soundVolume.Value * this._masterVolume.Value);
		}

		// Token: 0x04001270 RID: 4720
		[SerializeField]
		private Slider _slider;

		// Token: 0x04001271 RID: 4721
		[SerializeField]
		private GlobalFloatVariable _masterVolume;

		// Token: 0x04001272 RID: 4722
		[SerializeField]
		private GlobalFloatVariable _soundVolume;

		// Token: 0x04001273 RID: 4723
		[SerializeField]
		private bool _applyInstantly = true;
	}
}

using System;
using RG.Parsecs.Common;
using RG.Parsecs.EventEditor;
using UnityEngine;
using UnityEngine.UI;

namespace RG.SecondsRemaster.Menu
{
	// Token: 0x020002A9 RID: 681
	public class MasterVolumeSettingController : MonoBehaviour
	{
		// Token: 0x06001881 RID: 6273 RVA: 0x0006B2DC File Offset: 0x000694DC
		private void OnEnable()
		{
			if (this._masterVolume.Value < this._slider.minValue)
			{
				this._slider.value = this._slider.maxValue;
				this._masterVolume.Value = this._slider.maxValue;
				return;
			}
			this._slider.value = this._masterVolume.Value;
		}

		// Token: 0x06001882 RID: 6274 RVA: 0x0006B344 File Offset: 0x00069544
		public void ChangeVolume(float value)
		{
			this._masterVolume.Value = value;
			if (!this._applyInstantly)
			{
				return;
			}
			AudioManager.Instance.SetMusicVolume(this._musicVolume.Value * this._masterVolume.Value);
			AudioManager.Instance.SetSfxVolume(this._soundVolume.Value * this._masterVolume.Value);
			AudioManager.Instance.SetUiVolume(this._soundVolume.Value * this._masterVolume.Value);
		}

		// Token: 0x04001234 RID: 4660
		[SerializeField]
		private Slider _slider;

		// Token: 0x04001235 RID: 4661
		[SerializeField]
		private GlobalFloatVariable _masterVolume;

		// Token: 0x04001236 RID: 4662
		[SerializeField]
		private GlobalFloatVariable _soundVolume;

		// Token: 0x04001237 RID: 4663
		[SerializeField]
		private GlobalFloatVariable _musicVolume;

		// Token: 0x04001238 RID: 4664
		[SerializeField]
		private bool _applyInstantly = true;
	}
}

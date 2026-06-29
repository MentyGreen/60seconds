using System;
using RG.Parsecs.Common;
using RG.Parsecs.EventEditor;
using UnityEngine;
using UnityEngine.UI;

namespace RG.SecondsRemaster.Menu
{
	// Token: 0x020002AA RID: 682
	public class MusicVolumeSettingController : MonoBehaviour
	{
		// Token: 0x06001884 RID: 6276 RVA: 0x0006B3D8 File Offset: 0x000695D8
		private void OnEnable()
		{
			if (this._musicVolume.Value < this._slider.minValue)
			{
				this._slider.value = this._slider.maxValue;
				this._musicVolume.Value = this._slider.maxValue;
				return;
			}
			this._slider.value = this._musicVolume.Value;
		}

		// Token: 0x06001885 RID: 6277 RVA: 0x0006B440 File Offset: 0x00069640
		public void ChangeVolume(float value)
		{
			this._musicVolume.Value = value;
			if (!this._applyInstantly)
			{
				return;
			}
			AudioManager.Instance.SetMusicVolume(this._musicVolume.Value * this._masterVolume.Value);
		}

		// Token: 0x04001239 RID: 4665
		[SerializeField]
		private Slider _slider;

		// Token: 0x0400123A RID: 4666
		[SerializeField]
		private GlobalFloatVariable _masterVolume;

		// Token: 0x0400123B RID: 4667
		[SerializeField]
		private GlobalFloatVariable _musicVolume;

		// Token: 0x0400123C RID: 4668
		[SerializeField]
		private bool _applyInstantly = true;
	}
}

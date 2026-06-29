using System;
using RG.Parsecs.Common;
using RG.Parsecs.EventEditor;
using RG.VirtualInput;
using UnityEngine;
using UnityEngine.PostProcessing;
using UnityEngine.UI;

namespace RG.SecondsRemaster.Menu
{
	// Token: 0x020002A7 RID: 679
	public class GammaSettingController : MonoBehaviour
	{
		// Token: 0x0600187A RID: 6266 RVA: 0x0006AFC0 File Offset: 0x000691C0
		private void OnEnable()
		{
			this._blockChangeGammaInvoke = true;
			if (this._gammaValue.Value < this._slider.minValue)
			{
				this._slider.value = 0f;
				this._gammaValue.Value = 0f;
			}
			else
			{
				this._slider.value = this._gammaValue.Value;
			}
			this._blockChangeGammaInvoke = false;
		}

		// Token: 0x0600187B RID: 6267 RVA: 0x0006B02C File Offset: 0x0006922C
		public void ChangeGamma(float value)
		{
			if (this._blockChangeGammaInvoke)
			{
				return;
			}
			if (!Singleton<VirtualInputManager>.Instance.IsSelectablesMode() && Mathf.Abs(value) < 0.1f)
			{
				this._blockChangeGammaInvoke = true;
				this._slider.value = 0f;
				this._blockChangeGammaInvoke = false;
			}
			this._gammaValue.Value = value;
			if (!this._applyInstantly)
			{
				return;
			}
			for (int i = 0; i < this._postProcessingProfiles.Length; i++)
			{
				ColorGradingModel.Settings settings = this._postProcessingProfiles[i].colorGrading.settings;
				settings.basic.postExposure = this._gammaValue.Value;
				this._postProcessingProfiles[i].colorGrading.settings = settings;
			}
		}

		// Token: 0x04001227 RID: 4647
		[SerializeField]
		private Slider _slider;

		// Token: 0x04001228 RID: 4648
		[SerializeField]
		private GlobalFloatVariable _gammaValue;

		// Token: 0x04001229 RID: 4649
		[SerializeField]
		private PostProcessingProfile[] _postProcessingProfiles;

		// Token: 0x0400122A RID: 4650
		[SerializeField]
		private bool _applyInstantly = true;

		// Token: 0x0400122B RID: 4651
		private bool _blockChangeGammaInvoke;

		// Token: 0x0400122C RID: 4652
		private const float EPSILON_FOR_DEFAULTING_TO_MIDDLE = 0.1f;
	}
}

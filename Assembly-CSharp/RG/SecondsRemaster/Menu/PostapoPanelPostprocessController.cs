using System;
using System.Collections;
using Cinemachine.PostFX;
using RG.Parsecs.EventEditor;
using UnityEngine;
using UnityEngine.PostProcessing;

namespace RG.SecondsRemaster.Menu
{
	// Token: 0x020002B4 RID: 692
	public class PostapoPanelPostprocessController : MonoBehaviour
	{
		// Token: 0x060018B1 RID: 6321 RVA: 0x0006C730 File Offset: 0x0006A930
		private void OnEnable()
		{
			this._time = Time.time;
			if (this._isContinueAvailable != null && this._isContinueAvailable.Value)
			{
				base.StartCoroutine(this.ShiftColorGradingToPanelProfile(this._currentPostProcessingProfile.colorGrading.settings));
			}
			if (this._isContinueAvailable != null && !this._isContinueAvailable.Value)
			{
				this.SetNormalColorTones();
			}
		}

		// Token: 0x060018B2 RID: 6322 RVA: 0x0006C7A1 File Offset: 0x0006A9A1
		public void SetNormalColorTones()
		{
			this._currentPostProcessingProfile.colorGrading.settings = this._mainMenuProfile.colorGrading.settings;
		}

		// Token: 0x060018B3 RID: 6323 RVA: 0x0006C7C3 File Offset: 0x0006A9C3
		private IEnumerator ShiftColorGradingToPanelProfile(ColorGradingModel.Settings colorGradingSettings)
		{
			while (Time.time - this._time < 0.5f)
			{
				float t = (Time.time - this._time) * 1f / 0.5f;
				colorGradingSettings.basic.tint = Mathf.Lerp(this._mainMenuProfile.colorGrading.settings.basic.tint, this._postapoMainMenuPanelProfile.colorGrading.settings.basic.tint, t);
				colorGradingSettings.basic.temperature = Mathf.Lerp(this._mainMenuProfile.colorGrading.settings.basic.temperature, this._postapoMainMenuPanelProfile.colorGrading.settings.basic.temperature, t);
				colorGradingSettings.colorWheels.linear.gain.a = Mathf.Lerp(this._mainMenuProfile.colorGrading.settings.colorWheels.linear.gain.a, this._postapoMainMenuPanelProfile.colorGrading.settings.colorWheels.linear.gain.a, t);
				colorGradingSettings.colorWheels.linear.gain.r = Mathf.Lerp(this._mainMenuProfile.colorGrading.settings.colorWheels.linear.gain.r, this._postapoMainMenuPanelProfile.colorGrading.settings.colorWheels.linear.gain.r, t);
				colorGradingSettings.colorWheels.linear.gain.g = Mathf.Lerp(this._mainMenuProfile.colorGrading.settings.colorWheels.linear.gain.g, this._postapoMainMenuPanelProfile.colorGrading.settings.colorWheels.linear.gain.g, t);
				colorGradingSettings.colorWheels.linear.gain.b = Mathf.Lerp(this._mainMenuProfile.colorGrading.settings.colorWheels.linear.gain.b, this._postapoMainMenuPanelProfile.colorGrading.settings.colorWheels.linear.gain.b, t);
				this._currentPostProcessingProfile.colorGrading.settings = colorGradingSettings;
				yield return null;
			}
			colorGradingSettings.curves = this._postapoMainMenuPanelProfile.colorGrading.settings.curves;
			yield break;
		}

		// Token: 0x060018B4 RID: 6324 RVA: 0x0006C7D9 File Offset: 0x0006A9D9
		private void OnDisable()
		{
			this.SetNormalColorTones();
		}

		// Token: 0x060018B5 RID: 6325 RVA: 0x0006C7E1 File Offset: 0x0006A9E1
		private void OnApplicationQuit()
		{
			this.SetNormalColorTones();
		}

		// Token: 0x0400127A RID: 4730
		[SerializeField]
		private CinemachinePostFX _cameraPostFXComponent;

		// Token: 0x0400127B RID: 4731
		[SerializeField]
		private PostProcessingProfile _currentPostProcessingProfile;

		// Token: 0x0400127C RID: 4732
		[SerializeField]
		private GlobalBoolVariable _isContinueAvailable;

		// Token: 0x0400127D RID: 4733
		[SerializeField]
		private PostProcessingProfile _postapoMainMenuPanelProfile;

		// Token: 0x0400127E RID: 4734
		[SerializeField]
		private PostProcessingProfile _mainMenuProfile;

		// Token: 0x0400127F RID: 4735
		private float _time;

		// Token: 0x04001280 RID: 4736
		private const float SPEED = 1f;

		// Token: 0x04001281 RID: 4737
		private const float TRANSITION_DURATION = 0.5f;
	}
}

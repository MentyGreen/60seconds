using System;
using System.Collections;
using FMODUnity;
using RG.Parsecs.Common;
using RG.Parsecs.Menu;
using UnityEngine;

namespace RG.SecondsRemaster.Survival
{
	// Token: 0x0200033C RID: 828
	public class FlickerLights : MonoBehaviour
	{
		// Token: 0x06001B9F RID: 7071 RVA: 0x00076B77 File Offset: 0x00074D77
		private void Awake()
		{
			this._flickeringDurationForSeconds = new WaitForSeconds(this._disableTime);
		}

		// Token: 0x06001BA0 RID: 7072 RVA: 0x00076B8C File Offset: 0x00074D8C
		public void StartFlicking()
		{
			if (this._graphicLightOn == null || this._inProgress || this.AreFlashesDisabled())
			{
				return;
			}
			if (base.gameObject.activeInHierarchy)
			{
				this._inProgress = true;
				base.StartCoroutine(this.Flicker(Random.Range(this._minNumberOfFlickers, this._maxNumberOfFlickers)));
			}
		}

		// Token: 0x06001BA1 RID: 7073 RVA: 0x00076BEA File Offset: 0x00074DEA
		private bool AreFlashesDisabled()
		{
			return this._settings.runtimeData.HasKey("DisableFlashes") && Convert.ToBoolean(this._settings.runtimeData.GetString("DisableFlashes"));
		}

		// Token: 0x06001BA2 RID: 7074 RVA: 0x00076C1F File Offset: 0x00074E1F
		private IEnumerator Flicker(int count = 2)
		{
			int num;
			for (int i = 0; i < count; i = num + 1)
			{
				this._graphicLightOff.SetActive(true);
				this._graphicLightOn.SetActive(false);
				base.StartCoroutine(this.PlayFlickerSound());
				this._lightsObscurerAnimator.SetBool("ShowObscurer", true);
				yield return this._flickeringDurationForSeconds;
				this._graphicLightOn.SetActive(true);
				this._graphicLightOff.SetActive(false);
				this._lightsObscurerAnimator.SetBool("ShowObscurer", false);
				yield return new WaitForSeconds(Random.Range(this._minDelay, this._maxDelay));
				num = i;
			}
			this._inProgress = false;
			yield break;
		}

		// Token: 0x06001BA3 RID: 7075 RVA: 0x00076C35 File Offset: 0x00074E35
		private IEnumerator PlayFlickerSound()
		{
			AudioManager.PlaySound(this._flickerSound, 1f, 1f, 0f);
			yield return null;
			yield break;
		}

		// Token: 0x0400155A RID: 5466
		[EventRef]
		[SerializeField]
		private string _flickerSound;

		// Token: 0x0400155B RID: 5467
		[SerializeField]
		private GameObject _graphicLightOn;

		// Token: 0x0400155C RID: 5468
		[SerializeField]
		private GameObject _graphicLightOff;

		// Token: 0x0400155D RID: 5469
		[SerializeField]
		private SettingsSO _settings;

		// Token: 0x0400155E RID: 5470
		[SerializeField]
		private Animator _lightsObscurerAnimator;

		// Token: 0x0400155F RID: 5471
		[Space(25f)]
		[Tooltip("Minimum time between flickers")]
		[Range(0.001f, float.PositiveInfinity)]
		[SerializeField]
		private float _minDelay = 0.001f;

		// Token: 0x04001560 RID: 5472
		[Tooltip("Maximum time between flickers")]
		[Range(0.001f, float.PositiveInfinity)]
		[SerializeField]
		private float _maxDelay = 0.1f;

		// Token: 0x04001561 RID: 5473
		[Tooltip("How long lights are disable between flickering.")]
		[Range(0.01f, 1f)]
		[SerializeField]
		private float _disableTime = 0.15f;

		// Token: 0x04001562 RID: 5474
		[Tooltip("Minimum amount of times lights will flicker when clicked")]
		[SerializeField]
		private int _minNumberOfFlickers = 1;

		// Token: 0x04001563 RID: 5475
		[Tooltip("Maximum amount of times lights will flicker when clicked")]
		[SerializeField]
		private int _maxNumberOfFlickers = 5;

		// Token: 0x04001564 RID: 5476
		private WaitForSeconds _flickeringDurationForSeconds;

		// Token: 0x04001565 RID: 5477
		private bool _inProgress;

		// Token: 0x04001566 RID: 5478
		private const string OBSCURER_ANIMATOR_BOOL = "ShowObscurer";
	}
}

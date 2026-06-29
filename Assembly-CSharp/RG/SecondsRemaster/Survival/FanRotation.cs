using System;
using System.Collections;
using FMOD.Studio;
using FMODUnity;
using RG.Parsecs.Common;
using UnityEngine;

namespace RG.SecondsRemaster.Survival
{
	// Token: 0x0200033B RID: 827
	public class FanRotation : MonoBehaviour
	{
		// Token: 0x06001B94 RID: 7060 RVA: 0x00076A5E File Offset: 0x00074C5E
		private void Start()
		{
			this.PlaySound();
		}

		// Token: 0x06001B95 RID: 7061 RVA: 0x00076A66 File Offset: 0x00074C66
		private void OnMouseDown()
		{
		}

		// Token: 0x06001B96 RID: 7062 RVA: 0x00076A68 File Offset: 0x00074C68
		private void OnMouseUp()
		{
		}

		// Token: 0x06001B97 RID: 7063 RVA: 0x00076A6A File Offset: 0x00074C6A
		private void OnMouseOver()
		{
		}

		// Token: 0x06001B98 RID: 7064 RVA: 0x00076A6C File Offset: 0x00074C6C
		private void OnMouseEnter()
		{
		}

		// Token: 0x06001B99 RID: 7065 RVA: 0x00076A6E File Offset: 0x00074C6E
		private void OnMouseExit()
		{
		}

		// Token: 0x06001B9A RID: 7066 RVA: 0x00076A70 File Offset: 0x00074C70
		private void OnMouseUpAsButton()
		{
			if (this._animationInProgress)
			{
				return;
			}
			this._animationTime = 0f;
			this._animationInProgress = true;
			if (Mathf.Approximately(this._animator.GetFloat("SpeedRotation"), 0f))
			{
				this.PlaySound();
				base.StartCoroutine(this.ChangeRotation(false));
				return;
			}
			this.StopSound();
			base.StartCoroutine(this.ChangeRotation(true));
		}

		// Token: 0x06001B9B RID: 7067 RVA: 0x00076ADD File Offset: 0x00074CDD
		private void PlaySound()
		{
			this._eventInstance = AudioManager.PlaySoundAndReturnInstance(this._fanEvent, 1f, 1f, 0f);
		}

		// Token: 0x06001B9C RID: 7068 RVA: 0x00076AFF File Offset: 0x00074CFF
		private void StopSound()
		{
			this._eventInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
		}

		// Token: 0x06001B9D RID: 7069 RVA: 0x00076B0E File Offset: 0x00074D0E
		private IEnumerator ChangeRotation(bool stop = false)
		{
			float min = (float)(stop ? 1 : 0);
			float max = (float)(stop ? 0 : 1);
			for (;;)
			{
				this._animationTime += Time.deltaTime * this._lerpSpeed;
				float value = Mathf.Lerp(min, max, this._lerpCurve.Evaluate(this._animationTime));
				this._animator.SetFloat("SpeedRotation", value);
				if (Mathf.Approximately(this._animator.GetFloat("SpeedRotation"), max))
				{
					break;
				}
				yield return this._endOfFrame;
			}
			this._animator.SetFloat("SpeedRotation", max);
			yield return new WaitForSeconds(this._delayBetweenOperations);
			this._animationInProgress = false;
			yield break;
		}

		// Token: 0x04001550 RID: 5456
		[SerializeField]
		private Animator _animator;

		// Token: 0x04001551 RID: 5457
		[SerializeField]
		private AnimationCurve _lerpCurve = AnimationCurve.Linear(0f, 0f, 1f, 0f);

		// Token: 0x04001552 RID: 5458
		[SerializeField]
		private float _lerpSpeed = 1f;

		// Token: 0x04001553 RID: 5459
		[Tooltip("Delay after we can start/stop fan again (in seconds)")]
		[SerializeField]
		private float _delayBetweenOperations = 10f;

		// Token: 0x04001554 RID: 5460
		private readonly WaitForEndOfFrame _endOfFrame = new WaitForEndOfFrame();

		// Token: 0x04001555 RID: 5461
		private const string ROTATION_SPPED_CHANGE = "SpeedRotation";

		// Token: 0x04001556 RID: 5462
		private bool _animationInProgress;

		// Token: 0x04001557 RID: 5463
		private float _animationTime;

		// Token: 0x04001558 RID: 5464
		[EventRef]
		[SerializeField]
		private string _fanEvent;

		// Token: 0x04001559 RID: 5465
		private EventInstance _eventInstance;
	}
}

using System;
using FMODUnity;
using RG.Parsecs.Common;
using UnityEngine;
using UnityEngine.UI;

namespace RG.SecondsRemaster.Scavenge
{
	// Token: 0x020002C7 RID: 711
	public class ClockController : MonoBehaviour
	{
		// Token: 0x06001912 RID: 6418 RVA: 0x0006D682 File Offset: 0x0006B882
		private void Awake()
		{
			this._uiImages = base.GetComponentsInChildren<Image>();
		}

		// Token: 0x06001913 RID: 6419 RVA: 0x0006D690 File Offset: 0x0006B890
		public void Initialize(float scavengeTime, float alarmTime, float watchPulseTime)
		{
			this._scavengeTime = scavengeTime;
			this._watchPulseTime = watchPulseTime;
			this._alarmTime = alarmTime;
			this._watchPulsing = false;
			this._watchAnimator.enabled = true;
		}

		// Token: 0x06001914 RID: 6420 RVA: 0x0006D6BC File Offset: 0x0006B8BC
		public void UpdateHandPosition(float currentTimeLeft)
		{
			AudioManager.PlaySound(this._tickSoundName, 1f, 1f, 0f);
			if (currentTimeLeft - this._scavengeTime <= 0f)
			{
				if (!this._redFill.gameObject.activeSelf)
				{
					this._redFill.gameObject.SetActive(true);
				}
				if (!this._hands.gameObject.activeSelf)
				{
					this._hands.gameObject.SetActive(true);
				}
				this._redFill.fillAmount = 1f - currentTimeLeft / this._scavengeTime;
			}
			if (!this._alarm.activeSelf && currentTimeLeft <= this._alarmTime)
			{
				this._alarm.SetActive(true);
			}
			if (!this._watchPulsing && currentTimeLeft <= this._watchPulseTime)
			{
				this.PulseClock(true);
			}
			float z = -(360f * (1f - currentTimeLeft / this._scavengeTime));
			this._watchHand.localRotation = Quaternion.Euler(0f, 0f, z);
			if (currentTimeLeft <= 0f)
			{
				this.PulseClock(false);
				this._alarm.SetActive(false);
				iTween.ShakePosition(base.gameObject, new Vector3(this._shakeAmount.x, this._shakeAmount.y, this._shakeAmount.z), this._shakeLength);
				AudioManager.PlaySound(this._watchRingSoundName, 1f, 1f, 0f);
			}
		}

		// Token: 0x06001915 RID: 6421 RVA: 0x0006D82D File Offset: 0x0006BA2D
		public void ResetRedFill()
		{
			this._redFill.fillAmount = 0f;
		}

		// Token: 0x06001916 RID: 6422 RVA: 0x0006D83F File Offset: 0x0006BA3F
		public void PulseClock(bool pulse)
		{
			this._watchPulsing = pulse;
			this._watchAnimator.SetBool("Pulse", pulse);
		}

		// Token: 0x06001917 RID: 6423 RVA: 0x0006D85C File Offset: 0x0006BA5C
		public void HideClock()
		{
			for (int i = 0; i < this._uiImages.Length; i++)
			{
				this._uiImages[i].color = Color.clear;
			}
		}

		// Token: 0x040012D7 RID: 4823
		[SerializeField]
		private RectTransform _watchHand;

		// Token: 0x040012D8 RID: 4824
		[SerializeField]
		private Animator _watchAnimator;

		// Token: 0x040012D9 RID: 4825
		[SerializeField]
		private Image _redFill;

		// Token: 0x040012DA RID: 4826
		[SerializeField]
		private GameObject _alarm;

		// Token: 0x040012DB RID: 4827
		[SerializeField]
		private GameObject _hands;

		// Token: 0x040012DC RID: 4828
		[SerializeField]
		private Vector3 _shakeAmount = new Vector3(10f, 1f, 0f);

		// Token: 0x040012DD RID: 4829
		[SerializeField]
		private float _shakeLength = 5f;

		// Token: 0x040012DE RID: 4830
		[EventRef]
		[SerializeField]
		private string _tickSoundName;

		// Token: 0x040012DF RID: 4831
		[EventRef]
		[SerializeField]
		private string _watchRingSoundName;

		// Token: 0x040012E0 RID: 4832
		private Image[] _uiImages;

		// Token: 0x040012E1 RID: 4833
		private float _alarmTime = 20f;

		// Token: 0x040012E2 RID: 4834
		private float _watchPulseTime = 40f;

		// Token: 0x040012E3 RID: 4835
		private float _scavengeTime = 60f;

		// Token: 0x040012E4 RID: 4836
		private bool _watchPulsing;

		// Token: 0x040012E5 RID: 4837
		private const string PULSE_PARAMETER_NAME = "Pulse";
	}
}

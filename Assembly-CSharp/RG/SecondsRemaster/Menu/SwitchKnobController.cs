using System;
using System.Collections;
using UnityEngine;

namespace RG.SecondsRemaster.Menu
{
	// Token: 0x0200029C RID: 668
	public class SwitchKnobController : MonoBehaviour
	{
		// Token: 0x0600184B RID: 6219 RVA: 0x0006A138 File Offset: 0x00068338
		public void SetRandomRotation()
		{
			if (this._rotationOngoing)
			{
				return;
			}
			this._staticNoiseAnimator.SetTrigger("Show");
			this._rotationOngoing = true;
			int num = (Random.Range(0, 2) > 0) ? -1 : 1;
			float totalRotation = Random.Range(this._minimumRotation, this._maximumRotation);
			base.StartCoroutine(this.Rotate(totalRotation, (float)num));
		}

		// Token: 0x0600184C RID: 6220 RVA: 0x0006A196 File Offset: 0x00068396
		private IEnumerator Rotate(float totalRotation, float direction)
		{
			float amountRotated = 0f;
			float rotationAmount = totalRotation * this._smoothness * Time.deltaTime;
			while (amountRotated + this._accuracy <= totalRotation)
			{
				this._knobTransform.Rotate(0f, 0f, direction * rotationAmount);
				amountRotated += rotationAmount;
				yield return new WaitForEndOfFrame();
			}
			this._rotationOngoing = false;
			yield break;
		}

		// Token: 0x040011E7 RID: 4583
		[SerializeField]
		private RectTransform _knobTransform;

		// Token: 0x040011E8 RID: 4584
		[SerializeField]
		private float _smoothness;

		// Token: 0x040011E9 RID: 4585
		[SerializeField]
		private float _accuracy;

		// Token: 0x040011EA RID: 4586
		[SerializeField]
		[Range(0f, 360f)]
		private float _minimumRotation;

		// Token: 0x040011EB RID: 4587
		[SerializeField]
		[Range(0f, 360f)]
		private float _maximumRotation;

		// Token: 0x040011EC RID: 4588
		[SerializeField]
		private Animator _staticNoiseAnimator;

		// Token: 0x040011ED RID: 4589
		private bool _rotationOngoing;

		// Token: 0x040011EE RID: 4590
		private const int LEFT = -1;

		// Token: 0x040011EF RID: 4591
		private const int RIGHT = 1;

		// Token: 0x040011F0 RID: 4592
		private const string SHOW_NOISE_PARAM_NAME = "Show";
	}
}

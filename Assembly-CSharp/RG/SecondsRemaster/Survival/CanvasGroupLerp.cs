using System;
using System.Collections;
using UnityEngine;

namespace RG.SecondsRemaster.Survival
{
	// Token: 0x02000334 RID: 820
	[RequireComponent(typeof(CanvasGroup))]
	public class CanvasGroupLerp : MonoBehaviour
	{
		// Token: 0x06001B75 RID: 7029 RVA: 0x000764A1 File Offset: 0x000746A1
		private void Awake()
		{
			this._canvasGroup = base.GetComponent<CanvasGroup>();
		}

		// Token: 0x06001B76 RID: 7030 RVA: 0x000764AF File Offset: 0x000746AF
		public IEnumerator HideCanvasGroup()
		{
			while (this._canvasGroup.alpha > this._lerpEpsilon)
			{
				this._canvasGroup.alpha = Mathf.Lerp(this._canvasGroup.alpha, 0f, this._lerpSpeed * Time.deltaTime);
				yield return new WaitForEndOfFrame();
			}
			this._canvasGroup.alpha = 0f;
			yield return null;
			yield break;
		}

		// Token: 0x06001B77 RID: 7031 RVA: 0x000764BE File Offset: 0x000746BE
		public IEnumerator ShowCanvasGroup()
		{
			while (this._canvasGroup.alpha < 1f - this._lerpEpsilon)
			{
				this._canvasGroup.alpha = Mathf.Lerp(this._canvasGroup.alpha, 1f, this._lerpSpeed * Time.deltaTime);
				yield return new WaitForEndOfFrame();
			}
			this._canvasGroup.alpha = 1f;
			yield return null;
			yield break;
		}

		// Token: 0x04001536 RID: 5430
		private const float ALPHA_VISIBLE_VALUE = 1f;

		// Token: 0x04001537 RID: 5431
		private const float ALPHA_INVISIBLE_VALUE = 0f;

		// Token: 0x04001538 RID: 5432
		[SerializeField]
		[Tooltip("How fast will transition occur?")]
		private float _lerpSpeed = 5f;

		// Token: 0x04001539 RID: 5433
		[SerializeField]
		[Tooltip("Lerping will slow down when it get's to marginal [0 and 1] values. This value will determine how fast it should be interrupted (e.g: 0.1f means that after reaching 0.9 of transparency it'll set transparency to 1)")]
		private float _lerpEpsilon = 0.1f;

		// Token: 0x0400153A RID: 5434
		private CanvasGroup _canvasGroup;
	}
}

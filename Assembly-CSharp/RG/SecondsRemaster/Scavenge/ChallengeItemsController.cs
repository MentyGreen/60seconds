using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RG.SecondsRemaster.Scavenge
{
	// Token: 0x020002C6 RID: 710
	public class ChallengeItemsController : MonoBehaviour
	{
		// Token: 0x0600190A RID: 6410 RVA: 0x0006D444 File Offset: 0x0006B644
		private void Start()
		{
			if (GameSessionData.Instance.CurrentChallenge == null)
			{
				this._iconHolder.gameObject.SetActive(false);
				return;
			}
			this._items = GameSessionData.Instance.CurrentChallenge.Collectables;
			this.SpawnChallengeItems();
		}

		// Token: 0x0600190B RID: 6411 RVA: 0x0006D490 File Offset: 0x0006B690
		private void SpawnChallengeItems()
		{
			if (this._items == null)
			{
				return;
			}
			this._icons = new List<ChallengeItemController>(this._items.Count);
			for (int i = 0; i < this._items.Count; i++)
			{
				ScavengeItem icon = this._items[i];
				RectTransform rectTransform = (RectTransform)Object.Instantiate<Transform>(this._iconRectTransformPrefab, this._iconHolder);
				ChallengeItemController componentInChildren = rectTransform.GetComponentInChildren<ChallengeItemController>();
				componentInChildren.SetIcon(icon);
				componentInChildren.SetRect(rectTransform);
				this._icons.Add(componentInChildren);
			}
		}

		// Token: 0x0600190C RID: 6412 RVA: 0x0006D518 File Offset: 0x0006B718
		public void HideChallengeUI()
		{
			for (int i = 0; i < this._icons.Count; i++)
			{
				this._icons[i].gameObject.SetActive(false);
			}
		}

		// Token: 0x0600190D RID: 6413 RVA: 0x0006D554 File Offset: 0x0006B754
		public void DisableScavengeItem(ScavengeItem item)
		{
			if (this._icons == null)
			{
				return;
			}
			for (int i = 0; i < this._icons.Count; i++)
			{
				ChallengeItemController challengeItemController = this._icons[i];
				if (challengeItemController.Item == item && challengeItemController.Enabled)
				{
					challengeItemController.DisableIcon();
					base.StartCoroutine(this.ShrinkIconRect(challengeItemController.ParentRectTransform));
					break;
				}
			}
			this.ChangeIconRow();
		}

		// Token: 0x0600190E RID: 6414 RVA: 0x0006D5C4 File Offset: 0x0006B7C4
		private void ChangeIconRow()
		{
			for (int i = this._icons.Count - 1; i >= 0; i--)
			{
				ChallengeItemController challengeItemController = this._icons[i];
				if (challengeItemController.ParentRectTransform.anchoredPosition.y >= -125f)
				{
					break;
				}
				if (challengeItemController.ParentRectTransform.anchoredPosition.x < 300f)
				{
					challengeItemController.ChangeIconRow();
					base.StartCoroutine(this.PulseIconRectWidth(challengeItemController.ParentRectTransform));
					return;
				}
			}
		}

		// Token: 0x0600190F RID: 6415 RVA: 0x0006D63E File Offset: 0x0006B83E
		private IEnumerator ShrinkIconRect(RectTransform iconRect)
		{
			float currentTime = 0f;
			float elementPreferredWidth = iconRect.GetComponent<LayoutElement>().preferredWidth;
			float timeOfTravelMultiplier = 4.5f;
			yield return this._pulseWaitForSeconds;
			while (elementPreferredWidth > 0f)
			{
				elementPreferredWidth = (iconRect.GetComponent<LayoutElement>().preferredWidth = Mathf.Lerp(elementPreferredWidth, 0f, currentTime));
				currentTime += Time.deltaTime * timeOfTravelMultiplier;
				yield return new WaitForEndOfFrame();
			}
			iconRect.gameObject.SetActive(false);
			yield break;
		}

		// Token: 0x06001910 RID: 6416 RVA: 0x0006D654 File Offset: 0x0006B854
		private IEnumerator PulseIconRectWidth(RectTransform iconRect)
		{
			float currentTime = 0f;
			float elementPreferredWidth = iconRect.GetComponent<LayoutElement>().preferredWidth;
			float timeOfTravelMultiplier = 5f;
			yield return this._pulseWaitForSeconds;
			while (elementPreferredWidth > 1f)
			{
				elementPreferredWidth = (iconRect.GetComponent<LayoutElement>().preferredWidth = Mathf.Lerp(elementPreferredWidth, 1f, currentTime));
				currentTime += Time.deltaTime * timeOfTravelMultiplier;
				yield return new WaitForEndOfFrame();
			}
			currentTime = 0f;
			while (elementPreferredWidth < 200f)
			{
				elementPreferredWidth = (iconRect.GetComponent<LayoutElement>().preferredWidth = Mathf.Lerp(elementPreferredWidth, 200f, currentTime));
				currentTime += Time.deltaTime * timeOfTravelMultiplier;
				yield return new WaitForEndOfFrame();
			}
			yield break;
		}

		// Token: 0x040012CB RID: 4811
		[SerializeField]
		private Transform _iconHolder;

		// Token: 0x040012CC RID: 4812
		[SerializeField]
		private Transform _iconRectTransformPrefab;

		// Token: 0x040012CD RID: 4813
		private const float TIME_OF_TRAVEL_MULTIPLIER = 4.5f;

		// Token: 0x040012CE RID: 4814
		private const float TIME_OF_SWITCHING_ROWS_MULTIPLIER = 5f;

		// Token: 0x040012CF RID: 4815
		private const float TIME_OF_ICON_ANIMATION = 0.5f;

		// Token: 0x040012D0 RID: 4816
		private const float FIRST_ROW_HEIGHT = -125f;

		// Token: 0x040012D1 RID: 4817
		private const float FIRST_COLUMN_WIDTH = 300f;

		// Token: 0x040012D2 RID: 4818
		private const float DEFAULT_PREFERRED_WIDTH = 200f;

		// Token: 0x040012D3 RID: 4819
		private const float MIN_PREFERRED_WIDTH_FOR_PULSE = 1f;

		// Token: 0x040012D4 RID: 4820
		private readonly WaitForSeconds _pulseWaitForSeconds = new WaitForSeconds(0.5f);

		// Token: 0x040012D5 RID: 4821
		private List<ScavengeItem> _items;

		// Token: 0x040012D6 RID: 4822
		private List<ChallengeItemController> _icons;
	}
}

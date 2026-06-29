using System;
using System.Collections;
using UnityEngine;

namespace RG.SecondsRemaster.Survival
{
	// Token: 0x0200033D RID: 829
	public class FlightManager : MonoBehaviour
	{
		// Token: 0x06001BA5 RID: 7077 RVA: 0x00076C7B File Offset: 0x00074E7B
		private void Awake()
		{
			this.SetFlyingObject();
		}

		// Token: 0x06001BA6 RID: 7078 RVA: 0x00076C84 File Offset: 0x00074E84
		public void SetFlyingObject()
		{
			DateTime now = DateTime.Now;
			if (now.Day == 31 && now.Month == 10)
			{
				this._flyingObject = this._bat;
				return;
			}
			this._flyingObject = this._fly;
		}

		// Token: 0x06001BA7 RID: 7079 RVA: 0x00076CC6 File Offset: 0x00074EC6
		private IEnumerator FlyingAnimation()
		{
			this.SetStartingPosition();
			this.SelectPath();
			this.AnimationLaunch();
			yield return null;
			yield break;
		}

		// Token: 0x06001BA8 RID: 7080 RVA: 0x00076CD5 File Offset: 0x00074ED5
		public void StartAnimation()
		{
			base.StartCoroutine(this.FlyingAnimation());
		}

		// Token: 0x06001BA9 RID: 7081 RVA: 0x00076CE4 File Offset: 0x00074EE4
		private void SelectPath()
		{
			this._flightDirection = ((Random.Range(0, 2) > 0) ? FlightManager.EFlightDirection.RIGHT : FlightManager.EFlightDirection.LEFT);
			if (this._flightDirection == FlightManager.EFlightDirection.RIGHT)
			{
				this._flyingObject.GetComponent<SpriteRenderer>().flipX = true;
				this._currentPath = "Right" + Random.Range(1, 4).ToString();
			}
			else
			{
				this._flyingObject.GetComponent<SpriteRenderer>().flipX = false;
				this._currentPath = "Left" + Random.Range(1, 4).ToString();
			}
			if (this._currentPath != null)
			{
				this._flyingObject.gameObject.SetActive(true);
			}
		}

		// Token: 0x06001BAA RID: 7082 RVA: 0x00076D8C File Offset: 0x00074F8C
		private void AnimationLaunch()
		{
			iTween.MoveTo(this._flyingObject, iTween.Hash(new object[]
			{
				"path",
				iTweenPath.GetPath(this._currentPath),
				"time",
				this._flightTime,
				"oncomplete",
				"OnPathCompleted",
				"oncompletetarget",
				base.gameObject
			}));
		}

		// Token: 0x06001BAB RID: 7083 RVA: 0x00076DFC File Offset: 0x00074FFC
		private void OnPathCompleted()
		{
			this._flyingObject.SetActive(false);
		}

		// Token: 0x06001BAC RID: 7084 RVA: 0x00076E0C File Offset: 0x0007500C
		private void SetStartingPosition()
		{
			if (this._flightDirection == FlightManager.EFlightDirection.RIGHT)
			{
				this._flyingObject.transform.position = new Vector3(-100f, 0f, 0f);
				return;
			}
			this._flyingObject.transform.position = new Vector3(100f, 0f, 0f);
		}

		// Token: 0x04001567 RID: 5479
		[SerializeField]
		private GameObject _fly;

		// Token: 0x04001568 RID: 5480
		[SerializeField]
		private GameObject _bat;

		// Token: 0x04001569 RID: 5481
		[SerializeField]
		private float _flightTime = 7f;

		// Token: 0x0400156A RID: 5482
		[NonSerialized]
		private FlightManager.EFlightDirection _flightDirection;

		// Token: 0x0400156B RID: 5483
		[NonSerialized]
		private GameObject _flyingObject;

		// Token: 0x0400156C RID: 5484
		[NonSerialized]
		private string _currentPath;

		// Token: 0x0400156D RID: 5485
		private const string PATH_RIGHT = "Right";

		// Token: 0x0400156E RID: 5486
		private const string PATH_LEFT = "Left";

		// Token: 0x0400156F RID: 5487
		private const string I_TWEEN_ARG_PATH = "path";

		// Token: 0x04001570 RID: 5488
		private const string I_TWEEN_ARG_TIME = "time";

		// Token: 0x04001571 RID: 5489
		private const string I_TWEEN_ARG_ON_COMPLETE = "oncomplete";

		// Token: 0x04001572 RID: 5490
		private const string I_TWEEN_ARG_ON_COMPLETE_TARGET = "oncompletetarget";

		// Token: 0x04001573 RID: 5491
		private const int DAY_OF_HALLOWEEN = 31;

		// Token: 0x04001574 RID: 5492
		private const int MONTH_OF_HALLOWEEN = 10;

		// Token: 0x0200043D RID: 1085
		private enum EFlightDirection
		{
			// Token: 0x04001939 RID: 6457
			LEFT,
			// Token: 0x0400193A RID: 6458
			RIGHT
		}
	}
}

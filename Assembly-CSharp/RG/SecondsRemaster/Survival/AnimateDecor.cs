using System;
using System.Collections;
using RG.Parsecs.Survival;
using UnityEngine;

namespace RG.SecondsRemaster.Survival
{
	// Token: 0x0200033A RID: 826
	public class AnimateDecor : MonoBehaviour
	{
		// Token: 0x06001B8E RID: 7054 RVA: 0x00076988 File Offset: 0x00074B88
		private void Start()
		{
			base.StartCoroutine(this.RunAnimations());
			this._endOfDay.RegisterOnEndOfDay(new EndOfDayListenerList.OnEndOfDay(this.DisableAnimations), "EndGame", 3, this, false);
			this._endOfDay.RegisterOnDayStarted(new EndOfDayListenerList.OnDayStarted(this.EnableAnimations), "ChangeFinished", 1);
		}

		// Token: 0x06001B8F RID: 7055 RVA: 0x000769DE File Offset: 0x00074BDE
		private void OnDestroy()
		{
			this._endOfDay.UnregisterOnEndOfDay(new EndOfDayListenerList.OnEndOfDay(this.DisableAnimations), "EndGame");
			this._endOfDay.UnregisterOnDayStarted(new EndOfDayListenerList.OnDayStarted(this.EnableAnimations), "ChangeFinished");
		}

		// Token: 0x06001B90 RID: 7056 RVA: 0x00076A18 File Offset: 0x00074C18
		private void DisableAnimations()
		{
			this._animationEnabled = false;
		}

		// Token: 0x06001B91 RID: 7057 RVA: 0x00076A21 File Offset: 0x00074C21
		private void EnableAnimations()
		{
			this._animationEnabled = true;
		}

		// Token: 0x06001B92 RID: 7058 RVA: 0x00076A2A File Offset: 0x00074C2A
		private IEnumerator RunAnimations()
		{
			while (!this._endGameData.RuntimeData.ShouldEndGame)
			{
				yield return new WaitForSeconds(Random.Range(this._minTime, this._maxTIme));
				if (this._animationEnabled)
				{
					if (Random.Range(0, 100) < 30)
					{
						if (this._flightManager.gameObject.activeInHierarchy)
						{
							this._flightManager.StartAnimation();
						}
					}
					else
					{
						this._flickerLights.StartFlicking();
					}
				}
			}
			yield break;
		}

		// Token: 0x04001549 RID: 5449
		[SerializeField]
		private FlightManager _flightManager;

		// Token: 0x0400154A RID: 5450
		[SerializeField]
		private FlickerLights _flickerLights;

		// Token: 0x0400154B RID: 5451
		[SerializeField]
		private EndGameData _endGameData;

		// Token: 0x0400154C RID: 5452
		[Range(1f, 3.4028235E+38f)]
		[SerializeField]
		private float _minTime = 5f;

		// Token: 0x0400154D RID: 5453
		[Range(1f, 3.4028235E+38f)]
		[SerializeField]
		private float _maxTIme = 25f;

		// Token: 0x0400154E RID: 5454
		[SerializeField]
		private EndOfDayListenerList _endOfDay;

		// Token: 0x0400154F RID: 5455
		private bool _animationEnabled = true;
	}
}

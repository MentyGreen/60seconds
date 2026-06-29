using System;
using System.Collections.Generic;
using RG.Parsecs.EventEditor;
using UnityEngine;

namespace RG.SecondsRemaster.Menu
{
	// Token: 0x02000285 RID: 645
	public class FlyingVisualsManager : MonoBehaviour
	{
		// Token: 0x060017BF RID: 6079 RVA: 0x00068108 File Offset: 0x00066308
		private void Start()
		{
			float time = Random.Range(this._minTimeBeforeFirstFlier, this._maxTimeBeforeFirstFlier);
			base.Invoke("EnableFliers", time);
		}

		// Token: 0x060017C0 RID: 6080 RVA: 0x00068134 File Offset: 0x00066334
		private void Update()
		{
			if (this._fliersShouldBeDisplayed)
			{
				if (!this.IsAnimationPlaying)
				{
					if (Time.time > this._lastFlierDisplayTime + this._currentFlierCooldown)
					{
						this._lastFlierDisplayTime = Time.time;
						this._currentFlierCooldown = Random.Range(this._minTimeBeforeSubsequentFliers, this._maxTimeBeforeSubsequentFliers);
						int index = Random.Range(0, this._flyingObjects.Count);
						if (this._canFlierBeFlipped[index])
						{
							if ((double)Random.value > 0.5)
							{
								this._flyingObjects[index].transform.parent.transform.localScale = this._invertedScale;
								this._isObjectInverted.Value = true;
							}
							else
							{
								this._flyingObjects[index].transform.parent.transform.localScale = this._regularScale;
								this._isObjectInverted.Value = false;
							}
						}
						this.IsAnimationPlaying = true;
						this._flyingObjects[index].SetTrigger(FlyingVisualsManager._triggerHash);
						return;
					}
				}
				else
				{
					this._lastFlierDisplayTime = Time.time;
				}
			}
		}

		// Token: 0x060017C1 RID: 6081 RVA: 0x00068250 File Offset: 0x00066450
		private void EnableFliers()
		{
			this._fliersShouldBeDisplayed = true;
		}

		// Token: 0x04001153 RID: 4435
		public bool IsAnimationPlaying;

		// Token: 0x04001154 RID: 4436
		private static readonly int _triggerHash = Animator.StringToHash("Show");

		// Token: 0x04001155 RID: 4437
		private readonly Vector3 _regularScale = new Vector3(1f, 1f, 1f);

		// Token: 0x04001156 RID: 4438
		private readonly Vector3 _invertedScale = new Vector3(-1f, 1f, 1f);

		// Token: 0x04001157 RID: 4439
		[SerializeField]
		private float _minTimeBeforeFirstFlier = 20f;

		// Token: 0x04001158 RID: 4440
		[SerializeField]
		private float _maxTimeBeforeFirstFlier = 40f;

		// Token: 0x04001159 RID: 4441
		[SerializeField]
		private float _minTimeBeforeSubsequentFliers = 12f;

		// Token: 0x0400115A RID: 4442
		[SerializeField]
		private float _maxTimeBeforeSubsequentFliers = 30f;

		// Token: 0x0400115B RID: 4443
		[SerializeField]
		private List<Animator> _flyingObjects = new List<Animator>();

		// Token: 0x0400115C RID: 4444
		[SerializeField]
		private List<bool> _canFlierBeFlipped = new List<bool>();

		// Token: 0x0400115D RID: 4445
		private bool _fliersShouldBeDisplayed;

		// Token: 0x0400115E RID: 4446
		private float _lastFlierDisplayTime;

		// Token: 0x0400115F RID: 4447
		private float _currentFlierCooldown;

		// Token: 0x04001160 RID: 4448
		[SerializeField]
		private GlobalBoolVariable _isObjectInverted;
	}
}

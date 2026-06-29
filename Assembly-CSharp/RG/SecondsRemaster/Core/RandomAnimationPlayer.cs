using System;
using System.Collections.Generic;
using UnityEngine;

namespace RG.SecondsRemaster.Core
{
	// Token: 0x02000252 RID: 594
	public class RandomAnimationPlayer : MonoBehaviour
	{
		// Token: 0x06001635 RID: 5685 RVA: 0x00061093 File Offset: 0x0005F293
		private void Start()
		{
			this.ValidateSetup();
			this._lastTriggerTime = Time.time;
			this._triggerDelay = Random.Range(this._minCooldown, this._maxCooldown);
		}

		// Token: 0x06001636 RID: 5686 RVA: 0x000610C0 File Offset: 0x0005F2C0
		private void ValidateSetup()
		{
			if (this._animator == null)
			{
				Debug.LogError("Animator not set up properly in " + base.gameObject.name);
			}
			if (this._animationTriggerNames == null || this._animationTriggerNames.Count < 1 || this._animationTriggerNames.Contains(null))
			{
				Debug.LogError("Animation Trigger Names not set up properly in " + base.gameObject.name);
			}
			if (this._minCooldown < 0f || this._maxCooldown < 0f || this._maxCooldown < this._minCooldown)
			{
				Debug.LogError("Cooldown times not set up properly in " + base.gameObject.name);
			}
		}

		// Token: 0x06001637 RID: 5687 RVA: 0x00061175 File Offset: 0x0005F375
		private void Update()
		{
			if (Time.time > this._lastTriggerTime + this._triggerDelay)
			{
				this.FireAnimationTrigger();
				this._lastTriggerTime = Time.time;
				this._triggerDelay = Random.Range(this._minCooldown, this._maxCooldown);
			}
		}

		// Token: 0x06001638 RID: 5688 RVA: 0x000611B4 File Offset: 0x0005F3B4
		private void FireAnimationTrigger()
		{
			int index = (this._animationTriggerNames.Count > 1) ? Random.Range(0, this._animationTriggerNames.Count) : 0;
			this._animator.SetTrigger(this._animationTriggerNames[index]);
		}

		// Token: 0x04000EF2 RID: 3826
		[SerializeField]
		private Animator _animator;

		// Token: 0x04000EF3 RID: 3827
		[SerializeField]
		private List<string> _animationTriggerNames;

		// Token: 0x04000EF4 RID: 3828
		[SerializeField]
		private float _minCooldown = 10f;

		// Token: 0x04000EF5 RID: 3829
		[SerializeField]
		private float _maxCooldown = 60f;

		// Token: 0x04000EF6 RID: 3830
		private float _lastTriggerTime;

		// Token: 0x04000EF7 RID: 3831
		private float _triggerDelay;
	}
}

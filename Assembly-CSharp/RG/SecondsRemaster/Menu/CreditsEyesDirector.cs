using System;
using System.Collections.Generic;
using UnityEngine;

namespace RG.SecondsRemaster.Menu
{
	// Token: 0x0200028C RID: 652
	public class CreditsEyesDirector : MonoBehaviour
	{
		// Token: 0x060017E7 RID: 6119 RVA: 0x00068E00 File Offset: 0x00067000
		private void OnEnable()
		{
			if (this._currentlyPlayingControllers == null)
			{
				this._currentlyPlayingControllers = new CreditsEyesController[this._eyesGroups.Count];
			}
			for (int i = 0; i < this._eyesGroups.Count; i++)
			{
				this._eyesGroups[i].Initialize();
			}
			this.AssignEyesTypeToGroups();
			this.StartInitialAnimations();
			this._lastTriggerTime = Time.time;
		}

		// Token: 0x060017E8 RID: 6120 RVA: 0x00068E6C File Offset: 0x0006706C
		private void Update()
		{
			if (Time.time >= this._lastTriggerTime + this._refreshInterval)
			{
				int num = this.CheckIfAnimationHasFinishedAndReturnIndex();
				if (num != -1)
				{
					CreditsEyesController creditsEyesController = this._currentlyPlayingControllers[num].ParentGroup.StartAnimationAtRandomPointAndReturnInstance();
					if (creditsEyesController != null)
					{
						this._currentlyPlayingControllers[num] = creditsEyesController;
					}
				}
				this._lastTriggerTime = Time.time;
			}
		}

		// Token: 0x060017E9 RID: 6121 RVA: 0x00068EC8 File Offset: 0x000670C8
		private void StartInitialAnimations()
		{
			for (int i = 0; i < this._eyesGroups.Count; i++)
			{
				CreditsEyesController creditsEyesController = this._eyesGroups[i].StartAnimationAtRandomPointAndReturnInstance();
				if (creditsEyesController != null)
				{
					this._currentlyPlayingControllers[i] = creditsEyesController;
				}
			}
		}

		// Token: 0x060017EA RID: 6122 RVA: 0x00068F10 File Offset: 0x00067110
		private int CheckIfAnimationHasFinishedAndReturnIndex()
		{
			for (int i = 0; i < this._currentlyPlayingControllers.Length; i++)
			{
				if (this._currentlyPlayingControllers[i].IsFree)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x060017EB RID: 6123 RVA: 0x00068F44 File Offset: 0x00067144
		private void AssignEyesTypeToGroups()
		{
			bool flag = Random.Range(0, 2) > 0;
			for (int i = 0; i < this._eyesGroups.Count; i++)
			{
				this._eyesGroups[i].GroupEyesType = (flag ? EEyesType.TED : EEyesType.DOLORES);
				flag = !flag;
			}
		}

		// Token: 0x04001194 RID: 4500
		[SerializeField]
		private List<CreditsEyeGroup> _eyesGroups;

		// Token: 0x04001195 RID: 4501
		[SerializeField]
		private float _refreshInterval;

		// Token: 0x04001196 RID: 4502
		private float _lastTriggerTime;

		// Token: 0x04001197 RID: 4503
		private CreditsEyesController[] _currentlyPlayingControllers;
	}
}

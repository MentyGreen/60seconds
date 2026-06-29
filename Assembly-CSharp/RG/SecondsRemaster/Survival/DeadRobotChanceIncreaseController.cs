using System;
using RG.Parsecs.EventEditor;
using UnityEngine;

namespace RG.SecondsRemaster.Survival
{
	// Token: 0x020002DB RID: 731
	public class DeadRobotChanceIncreaseController : MonoBehaviour
	{
		// Token: 0x06001996 RID: 6550 RVA: 0x0006F56C File Offset: 0x0006D76C
		private void Start()
		{
			this._deadRobotChanceIncrease.Value = (DateTime.Today.DayOfWeek == DayOfWeek.Thursday);
		}

		// Token: 0x04001393 RID: 5011
		[SerializeField]
		private GlobalBoolVariable _deadRobotChanceIncrease;
	}
}

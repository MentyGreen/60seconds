using System;
using RG.Parsecs.Common;
using UnityEngine;

namespace RG.SecondsRemaster.Menu
{
	// Token: 0x0200029F RID: 671
	public class UnityEventToggleController : ToggleController
	{
		// Token: 0x06001851 RID: 6225 RVA: 0x0006A20A File Offset: 0x0006840A
		public override void OnToggleValueChangedAction(bool toggleValue)
		{
			this._onValueChange.Invoke(toggleValue);
		}

		// Token: 0x040011F2 RID: 4594
		[SerializeField]
		private OnValueChange _onValueChange;
	}
}

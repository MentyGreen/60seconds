using System;
using I2.Loc;
using RG.Parsecs.Common;
using UnityEngine;
using UnityEngine.UI;

namespace RG.SecondsRemaster.Survival
{
	// Token: 0x02000340 RID: 832
	public class JournalTooltipContent : TooltipContent
	{
		// Token: 0x06001BB4 RID: 7092 RVA: 0x00076FA0 File Offset: 0x000751A0
		public virtual LocalizedString Name()
		{
			if (this._tab != null && !this._tab.interactable && !string.IsNullOrEmpty(this._inactiveTab))
			{
				return this._inactiveTab;
			}
			return this._name;
		}

		// Token: 0x06001BB5 RID: 7093 RVA: 0x00076FDC File Offset: 0x000751DC
		public override bool IsValid()
		{
			return this._name != null && !string.IsNullOrEmpty(this._name);
		}

		// Token: 0x0400157F RID: 5503
		[SerializeField]
		private Button _tab;

		// Token: 0x04001580 RID: 5504
		[SerializeField]
		private LocalizedString _inactiveTab;

		// Token: 0x04001581 RID: 5505
		[SerializeField]
		private LocalizedString _name;
	}
}

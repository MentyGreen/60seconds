using System;
using I2.Loc;
using RG.Parsecs.Common;
using RG.Parsecs.EventEditor;
using UnityEngine;

namespace RG.SecondsRemaster.Menu
{
	// Token: 0x020002B8 RID: 696
	public class NewGameTooltipContent : TooltipContent
	{
		// Token: 0x060018C1 RID: 6337 RVA: 0x0006C924 File Offset: 0x0006AB24
		public override bool IsValid()
		{
			return this.TextTerm != null && !this.TextTerm.IsNullOrEmpty() && this._isContinueAvailable != null && this._isContinueAvailable.Value;
		}

		// Token: 0x17000483 RID: 1155
		// (get) Token: 0x060018C2 RID: 6338 RVA: 0x0006C969 File Offset: 0x0006AB69
		public LocalizedString TextTerm
		{
			get
			{
				return this._textTerm;
			}
		}

		// Token: 0x0400128E RID: 4750
		[SerializeField]
		private LocalizedString _textTerm;

		// Token: 0x0400128F RID: 4751
		[SerializeField]
		private GlobalBoolVariable _isContinueAvailable;
	}
}

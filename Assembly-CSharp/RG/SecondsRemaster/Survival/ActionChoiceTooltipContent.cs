using System;
using I2.Loc;
using RG.Parsecs.Common;
using RG.Parsecs.Survival;
using UnityEngine;

namespace RG.SecondsRemaster.Survival
{
	// Token: 0x02000339 RID: 825
	public class ActionChoiceTooltipContent : TooltipContent
	{
		// Token: 0x170004E4 RID: 1252
		// (get) Token: 0x06001B86 RID: 7046 RVA: 0x00076914 File Offset: 0x00074B14
		public Character Character
		{
			get
			{
				return this._character;
			}
		}

		// Token: 0x170004E5 RID: 1253
		// (get) Token: 0x06001B87 RID: 7047 RVA: 0x0007691C File Offset: 0x00074B1C
		public IItem Item
		{
			get
			{
				return this._item;
			}
		}

		// Token: 0x170004E6 RID: 1254
		// (get) Token: 0x06001B88 RID: 7048 RVA: 0x00076924 File Offset: 0x00074B24
		public LocalizedString Term
		{
			get
			{
				return this._term;
			}
		}

		// Token: 0x06001B89 RID: 7049 RVA: 0x0007692C File Offset: 0x00074B2C
		public override bool IsValid()
		{
			return true;
		}

		// Token: 0x06001B8A RID: 7050 RVA: 0x0007692F File Offset: 0x00074B2F
		public void SetCharacterContent(Character character)
		{
			this._character = character;
			this._item = null;
			this._term = null;
		}

		// Token: 0x06001B8B RID: 7051 RVA: 0x0007694B File Offset: 0x00074B4B
		public void SetItemContent(IItem item)
		{
			this._item = item;
			this._character = null;
			this._term = null;
		}

		// Token: 0x06001B8C RID: 7052 RVA: 0x00076967 File Offset: 0x00074B67
		public void SetTermContent(LocalizedString term)
		{
			this._term = term;
			this._character = null;
			this._item = null;
		}

		// Token: 0x04001546 RID: 5446
		[SerializeField]
		private Character _character;

		// Token: 0x04001547 RID: 5447
		[SerializeField]
		private IItem _item;

		// Token: 0x04001548 RID: 5448
		[SerializeField]
		private LocalizedString _term;
	}
}

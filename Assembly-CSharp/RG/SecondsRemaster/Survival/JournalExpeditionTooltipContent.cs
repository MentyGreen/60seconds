using System;
using I2.Loc;
using RG.Parsecs.Survival;
using UnityEngine;

namespace RG.SecondsRemaster.Survival
{
	// Token: 0x0200033E RID: 830
	public class JournalExpeditionTooltipContent : JournalTooltipContent
	{
		// Token: 0x06001BAE RID: 7086 RVA: 0x00076E80 File Offset: 0x00075080
		public override LocalizedString Name()
		{
			if (this._defaultCharacterList.GetCharactersWithStatus(this._onExpeditionStatus.Guid).Count > 0)
			{
				return this._expeditionOngoing;
			}
			if (this._expeditionData.RuntimeData.IsOngoing)
			{
				return this._expeditionOngoing;
			}
			return base.Name();
		}

		// Token: 0x06001BAF RID: 7087 RVA: 0x00076ED1 File Offset: 0x000750D1
		public override bool IsValid()
		{
			return base.IsValid() && this._expeditionData != null && !string.IsNullOrEmpty(this._expeditionOngoing);
		}

		// Token: 0x04001575 RID: 5493
		[SerializeField]
		private ExpeditionData _expeditionData;

		// Token: 0x04001576 RID: 5494
		[SerializeField]
		private LocalizedString _expeditionOngoing;

		// Token: 0x04001577 RID: 5495
		[SerializeField]
		private CharacterList _defaultCharacterList;

		// Token: 0x04001578 RID: 5496
		[SerializeField]
		private CharacterStatus _onExpeditionStatus;
	}
}

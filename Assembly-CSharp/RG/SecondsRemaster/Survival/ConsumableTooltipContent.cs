using System;
using I2.Loc;
using RG.Parsecs.Common;
using RG.SecondsRemaster.Core;
using UnityEngine;

namespace RG.SecondsRemaster.Survival
{
	// Token: 0x02000346 RID: 838
	public class ConsumableTooltipContent : TooltipContent
	{
		// Token: 0x170004E7 RID: 1255
		// (get) Token: 0x06001BCD RID: 7117 RVA: 0x00077406 File Offset: 0x00075606
		public SecondsConsumableRemedium Consumable
		{
			get
			{
				return this._consumable;
			}
		}

		// Token: 0x170004E8 RID: 1256
		// (get) Token: 0x06001BCE RID: 7118 RVA: 0x0007740E File Offset: 0x0007560E
		public LocalizedString GeneralInfo
		{
			get
			{
				return this._generalInfo;
			}
		}

		// Token: 0x170004E9 RID: 1257
		// (get) Token: 0x06001BCF RID: 7119 RVA: 0x00077416 File Offset: 0x00075616
		public LocalizedString ContainerName
		{
			get
			{
				return this._containerName;
			}
		}

		// Token: 0x06001BD0 RID: 7120 RVA: 0x0007741E File Offset: 0x0007561E
		public override bool IsValid()
		{
			return !string.IsNullOrEmpty(this._generalInfo) && !string.IsNullOrEmpty(this._containerName) && !(this._consumable == null);
		}

		// Token: 0x04001592 RID: 5522
		[SerializeField]
		private LocalizedString _generalInfo;

		// Token: 0x04001593 RID: 5523
		[SerializeField]
		private LocalizedString _containerName;

		// Token: 0x04001594 RID: 5524
		[SerializeField]
		private SecondsConsumableRemedium _consumable;
	}
}

using System;
using I2.Loc;
using RG.Parsecs.Common;
using UnityEngine;

namespace RG.SecondsRemaster.Menu
{
	// Token: 0x020002B5 RID: 693
	public class DifficultyTooltipContent : TooltipContent
	{
		// Token: 0x17000482 RID: 1154
		// (get) Token: 0x060018B7 RID: 6327 RVA: 0x0006C7F1 File Offset: 0x0006A9F1
		public LocalizedString[] DifficultyLevelTexts
		{
			get
			{
				return this._difficultyLevelTexts;
			}
		}

		// Token: 0x060018B8 RID: 6328 RVA: 0x0006C7F9 File Offset: 0x0006A9F9
		public override bool IsValid()
		{
			return this._difficultyLevelTexts != null;
		}

		// Token: 0x04001282 RID: 4738
		[SerializeField]
		[Tooltip("Difficulty params, the number of elements must match the number of headers in Content Handler!")]
		private LocalizedString[] _difficultyLevelTexts;
	}
}

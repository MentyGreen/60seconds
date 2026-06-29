using System;
using RG.Parsecs.Common;
using RG.SecondsRemaster.Survival;
using TMPro;
using UnityEngine;

namespace RG.SecondsRemaster
{
	// Token: 0x02000249 RID: 585
	public class JournalTooltipContentHandler : TooltipContentHandler
	{
		// Token: 0x06001612 RID: 5650 RVA: 0x00060D54 File Offset: 0x0005EF54
		public override void HandleContent(TooltipContent content)
		{
			if (content is JournalTooltipContent)
			{
				this.HandleVisitorTooltipContent((JournalTooltipContent)content);
			}
		}

		// Token: 0x06001613 RID: 5651 RVA: 0x00060D6C File Offset: 0x0005EF6C
		private void HandleVisitorTooltipContent(JournalTooltipContent content)
		{
			if (this._text != null)
			{
				if (string.IsNullOrEmpty(content.Name()))
				{
					this._text.text = string.Empty;
					return;
				}
				this._text.text = content.Name();
			}
		}

		// Token: 0x04000ED3 RID: 3795
		[SerializeField]
		private TextMeshProUGUI _text;
	}
}

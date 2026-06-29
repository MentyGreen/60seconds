using System;
using RG.Parsecs.Common;
using TMPro;
using UnityEngine;

namespace RG.SecondsRemaster.Survival
{
	// Token: 0x02000343 RID: 835
	public class RemasterActionChoiceTooltipContentHandler : TooltipContentHandler
	{
		// Token: 0x06001BC5 RID: 7109 RVA: 0x00077238 File Offset: 0x00075438
		public override void HandleContent(TooltipContent content)
		{
			ActionChoiceTooltipContent actionChoiceTooltipContent = content as ActionChoiceTooltipContent;
			if (actionChoiceTooltipContent != null)
			{
				this.HandleTooltipContent(actionChoiceTooltipContent);
			}
		}

		// Token: 0x06001BC6 RID: 7110 RVA: 0x0007725C File Offset: 0x0007545C
		private void HandleTooltipContent(ActionChoiceTooltipContent content)
		{
			if (content.Character != null)
			{
				this._text.text = content.Character.StaticData.Name;
				return;
			}
			if (content.Item != null)
			{
				this._text.text = content.Item.BaseStaticData.Name;
				return;
			}
			if (!string.IsNullOrEmpty(content.Term) && content.Term != null)
			{
				this._text.text = content.Term;
			}
		}

		// Token: 0x04001591 RID: 5521
		[Tooltip("Text handler of tooltip.")]
		[SerializeField]
		private TextMeshProUGUI _text;
	}
}

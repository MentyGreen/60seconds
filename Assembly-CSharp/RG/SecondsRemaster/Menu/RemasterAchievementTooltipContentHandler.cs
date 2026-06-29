using System;
using RG.Parsecs.Common;
using TMPro;
using UnityEngine;

namespace RG.SecondsRemaster.Menu
{
	// Token: 0x02000288 RID: 648
	public class RemasterAchievementTooltipContentHandler : TooltipContentHandler
	{
		// Token: 0x060017DC RID: 6108 RVA: 0x00068BA4 File Offset: 0x00066DA4
		public override void HandleContent(TooltipContent content)
		{
			RemasterAchievementTooltipContent remasterAchievementTooltipContent = content as RemasterAchievementTooltipContent;
			if (remasterAchievementTooltipContent != null)
			{
				this._titleText.text = string.Format("<color=#{1}>{0}</color>", remasterAchievementTooltipContent.Achievement.title, this._titleColorHex);
				this._descriptionText.text = string.Format("<color=#{1}>{0}</color>", remasterAchievementTooltipContent.Achievement.description, this._descriptionColorHex);
			}
		}

		// Token: 0x060017DD RID: 6109 RVA: 0x00068C17 File Offset: 0x00066E17
		private void Awake()
		{
			this._titleColorHex = ColorUtility.ToHtmlStringRGB(this._titleColor);
			this._descriptionColorHex = ColorUtility.ToHtmlStringRGB(this._descriptionColor);
		}

		// Token: 0x04001179 RID: 4473
		[SerializeField]
		private TextMeshProUGUI _titleText;

		// Token: 0x0400117A RID: 4474
		[SerializeField]
		private TextMeshProUGUI _descriptionText;

		// Token: 0x0400117B RID: 4475
		[SerializeField]
		private Color _titleColor;

		// Token: 0x0400117C RID: 4476
		[SerializeField]
		private Color _descriptionColor;

		// Token: 0x0400117D RID: 4477
		private string _titleColorHex;

		// Token: 0x0400117E RID: 4478
		private string _descriptionColorHex;

		// Token: 0x0400117F RID: 4479
		private const string TEXT_FORMAT = "<color=#{1}>{0}</color>";
	}
}

using System;
using RG.Parsecs.Survival;
using UnityEngine;
using UnityEngine.UI;

namespace RG.SecondsRemaster.Survival
{
	// Token: 0x020002F8 RID: 760
	public class SpriteJournalContentDisplayer : JournalContentDisplayer<SpriteJournalContent>
	{
		// Token: 0x170004BC RID: 1212
		// (get) Token: 0x06001A09 RID: 6665 RVA: 0x00071130 File Offset: 0x0006F330
		public override int LinesAmount
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x06001A0A RID: 6666 RVA: 0x00071134 File Offset: 0x0006F334
		public override void SetContentData(JournalContent content)
		{
			if (content.Type != EJournalContentType.SPRITE)
			{
				return;
			}
			SpriteJournalContent spriteJournalContent = (SpriteJournalContent)content;
			this._image.sprite = spriteJournalContent.Sprite;
			switch (spriteJournalContent.Align)
			{
			case EventContentData.ESpriteAlign.LEFT:
				this._layoutGroup.childAlignment = TextAnchor.UpperLeft;
				return;
			case EventContentData.ESpriteAlign.CENTER:
				this._layoutGroup.childAlignment = TextAnchor.UpperCenter;
				return;
			case EventContentData.ESpriteAlign.RIGHT:
				this._layoutGroup.childAlignment = TextAnchor.UpperRight;
				return;
			default:
				return;
			}
		}

		// Token: 0x040013F1 RID: 5105
		[SerializeField]
		private Image _image;

		// Token: 0x040013F2 RID: 5106
		[SerializeField]
		private HorizontalLayoutGroup _layoutGroup;
	}
}

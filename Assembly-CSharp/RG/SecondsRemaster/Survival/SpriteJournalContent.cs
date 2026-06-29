using System;
using RG.Core.SaveSystem;
using RG.Parsecs.Common;
using RG.Parsecs.Survival;
using UnityEngine;

namespace RG.SecondsRemaster.Survival
{
	// Token: 0x020002F6 RID: 758
	[Serializable]
	public sealed class SpriteJournalContent : JournalContent
	{
		// Token: 0x170004BA RID: 1210
		// (get) Token: 0x06001A02 RID: 6658 RVA: 0x00070FDC File Offset: 0x0006F1DC
		public Sprite Sprite
		{
			get
			{
				return this._sprite;
			}
		}

		// Token: 0x170004BB RID: 1211
		// (get) Token: 0x06001A03 RID: 6659 RVA: 0x00070FE4 File Offset: 0x0006F1E4
		public EventContentData.ESpriteAlign Align
		{
			get
			{
				return this._align;
			}
		}

		// Token: 0x06001A04 RID: 6660 RVA: 0x00070FEC File Offset: 0x0006F1EC
		public SpriteJournalContent(Sprite sprite, EventContentData.ESpriteAlign align, int displayPriority) : base(displayPriority)
		{
			this.type = EJournalContentType.SPRITE;
			this._sprite = sprite;
			this._align = align;
		}

		// Token: 0x06001A05 RID: 6661 RVA: 0x0007100A File Offset: 0x0006F20A
		public SpriteJournalContent(string serializedData, SaveEvent saveEvent) : base(saveEvent)
		{
			this.Deserialize(serializedData, saveEvent);
		}

		// Token: 0x06001A06 RID: 6662 RVA: 0x0007101C File Offset: 0x0006F21C
		public override string Serialize()
		{
			return JsonUtility.ToJson(new SpriteJournalContentWrapper
			{
				DisplayOrder = this.displayOrder,
				DisplayPriority = this.displayPriority,
				GroupId = ((this.groupId != null) ? this.groupId.Guid : string.Empty),
				Type = this.type,
				Sprite = ((this._sprite != null) ? this._sprite.name : string.Empty),
				Align = this._align
			});
		}

		// Token: 0x06001A07 RID: 6663 RVA: 0x000710B0 File Offset: 0x0006F2B0
		public override void Deserialize(string data, SaveEvent saveEvent)
		{
			SpriteJournalContentWrapper spriteJournalContentWrapper = JsonUtility.FromJson<SpriteJournalContentWrapper>(data);
			base.DeserializeBaseWrapper(spriteJournalContentWrapper, saveEvent);
			if (!string.IsNullOrEmpty(spriteJournalContentWrapper.Sprite))
			{
				Sprite sprite = ContentManager.GetSprite(spriteJournalContentWrapper.Sprite);
				if (sprite == null)
				{
					Debug.LogError(string.Format("Cannot find sprite {0} in asset bundle", spriteJournalContentWrapper.Sprite));
				}
				else
				{
					this._sprite = (string.IsNullOrEmpty(spriteJournalContentWrapper.Sprite) ? null : sprite);
				}
			}
			this._align = spriteJournalContentWrapper.Align;
		}

		// Token: 0x040013ED RID: 5101
		[SerializeField]
		private Sprite _sprite;

		// Token: 0x040013EE RID: 5102
		[SerializeField]
		private EventContentData.ESpriteAlign _align;
	}
}

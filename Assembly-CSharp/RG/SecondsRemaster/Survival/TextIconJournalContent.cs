using System;
using I2.Loc;
using RG.Core.SaveSystem;
using RG.Parsecs.Survival;
using UnityEngine;

namespace RG.SecondsRemaster.Survival
{
	// Token: 0x02000305 RID: 773
	[Serializable]
	public sealed class TextIconJournalContent : JournalContent
	{
		// Token: 0x170004CA RID: 1226
		// (get) Token: 0x06001A47 RID: 6727 RVA: 0x00071F4A File Offset: 0x0007014A
		public EventContentData.ETextIconContentType IconType
		{
			get
			{
				return this._iconType;
			}
		}

		// Token: 0x170004CB RID: 1227
		// (get) Token: 0x06001A48 RID: 6728 RVA: 0x00071F52 File Offset: 0x00070152
		public LocalizedString IconTerm
		{
			get
			{
				return this._iconTerm;
			}
		}

		// Token: 0x170004CC RID: 1228
		// (get) Token: 0x06001A49 RID: 6729 RVA: 0x00071F5A File Offset: 0x0007015A
		public int Amount
		{
			get
			{
				return this._amount;
			}
		}

		// Token: 0x06001A4A RID: 6730 RVA: 0x00071F62 File Offset: 0x00070162
		public TextIconJournalContent(LocalizedString iconTerm, int amount, EventContentData.ETextIconContentType iconType, int displayPriority) : base(displayPriority)
		{
			this.type = EJournalContentType.TEXT_ICON;
			this._iconType = iconType;
			this._iconTerm = iconTerm;
			this._amount = amount;
		}

		// Token: 0x06001A4B RID: 6731 RVA: 0x00071F88 File Offset: 0x00070188
		public TextIconJournalContent(string serializedData, SaveEvent saveEvent) : base(saveEvent)
		{
			this.Deserialize(serializedData, saveEvent);
		}

		// Token: 0x06001A4C RID: 6732 RVA: 0x00071F9C File Offset: 0x0007019C
		public override string Serialize()
		{
			return JsonUtility.ToJson(new TextIconJournalContentWrapper
			{
				DisplayOrder = this.displayOrder,
				DisplayPriority = this.displayPriority,
				GroupId = ((this.groupId != null) ? this.groupId.Guid : string.Empty),
				Type = this.type,
				Sign = this._sign,
				IconTerm = this._iconTerm,
				IconType = this._iconType,
				Amount = this._amount
			});
		}

		// Token: 0x06001A4D RID: 6733 RVA: 0x00072030 File Offset: 0x00070230
		public override void Deserialize(string data, SaveEvent saveEvent)
		{
			TextIconJournalContentWrapper textIconJournalContentWrapper = JsonUtility.FromJson<TextIconJournalContentWrapper>(data);
			base.DeserializeBaseWrapper(textIconJournalContentWrapper, saveEvent);
			this._sign = textIconJournalContentWrapper.Sign;
			this._iconTerm = textIconJournalContentWrapper.IconTerm;
			this._iconType = textIconJournalContentWrapper.IconType;
			this._amount = textIconJournalContentWrapper.Amount;
		}

		// Token: 0x04001419 RID: 5145
		[SerializeField]
		private string _sign;

		// Token: 0x0400141A RID: 5146
		[SerializeField]
		private LocalizedString _iconTerm;

		// Token: 0x0400141B RID: 5147
		[SerializeField]
		private EventContentData.ETextIconContentType _iconType;

		// Token: 0x0400141C RID: 5148
		[SerializeField]
		private int _amount;
	}
}

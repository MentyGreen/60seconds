using System;
using System.Collections.Generic;
using RG.Core.SaveSystem;
using RG.Parsecs.EventEditor;
using UnityEngine;

namespace RG.SecondsRemaster.Survival
{
	// Token: 0x020002FA RID: 762
	[Serializable]
	public sealed class SpriteChoiceJournalContent : JournalContent
	{
		// Token: 0x06001A0D RID: 6669 RVA: 0x000711B3 File Offset: 0x0006F3B3
		public SpriteChoiceJournalContent(List<BaseActionCondition> actionConditions) : base(int.MinValue)
		{
			this.type = EJournalContentType.CUSTOM_CHOICE;
			this._actionConditions = actionConditions;
		}

		// Token: 0x170004BD RID: 1213
		// (get) Token: 0x06001A0E RID: 6670 RVA: 0x000711CE File Offset: 0x0006F3CE
		public List<BaseActionCondition> ActionConditions
		{
			get
			{
				return this._actionConditions;
			}
		}

		// Token: 0x06001A0F RID: 6671 RVA: 0x000711D6 File Offset: 0x0006F3D6
		public SpriteChoiceJournalContent(string serializedData, SaveEvent saveEvent) : base(saveEvent)
		{
			this.Deserialize(serializedData, saveEvent);
		}

		// Token: 0x06001A10 RID: 6672 RVA: 0x000711E8 File Offset: 0x0006F3E8
		public override string Serialize()
		{
			SpriteChoiceContentWrapper spriteChoiceContentWrapper = new SpriteChoiceContentWrapper
			{
				DisplayOrder = this.displayOrder,
				DisplayPriority = this.displayPriority,
				GroupId = ((this.groupId != null) ? this.groupId.Guid : string.Empty),
				Type = this.type,
				ActionConditions = new List<string>()
			};
			if (this._actionConditions != null)
			{
				for (int i = 0; i < this._actionConditions.Count; i++)
				{
					if (this._actionConditions[i] != null)
					{
						spriteChoiceContentWrapper.ActionConditions.Add(this._actionConditions[i].Guid);
					}
				}
			}
			return JsonUtility.ToJson(spriteChoiceContentWrapper);
		}

		// Token: 0x06001A11 RID: 6673 RVA: 0x000712A4 File Offset: 0x0006F4A4
		public override void Deserialize(string data, SaveEvent saveEvent)
		{
			SpriteChoiceContentWrapper spriteChoiceContentWrapper = JsonUtility.FromJson<SpriteChoiceContentWrapper>(data);
			base.DeserializeBaseWrapper(spriteChoiceContentWrapper, saveEvent);
			this._actionConditions = new List<BaseActionCondition>();
			for (int i = 0; i < spriteChoiceContentWrapper.ActionConditions.Count; i++)
			{
				if (!string.IsNullOrEmpty(spriteChoiceContentWrapper.ActionConditions[i]))
				{
					this._actionConditions.Add((BaseActionCondition)saveEvent.GetReferenceObjectByID(spriteChoiceContentWrapper.ActionConditions[i]));
				}
			}
		}

		// Token: 0x040013F3 RID: 5107
		[SerializeField]
		private List<BaseActionCondition> _actionConditions;
	}
}

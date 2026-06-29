using System;
using RG.Core.SaveSystem;
using UnityEngine;

namespace RG.SecondsRemaster.Survival
{
	// Token: 0x020002E0 RID: 736
	[Serializable]
	public abstract class JournalContent
	{
		// Token: 0x170004A3 RID: 1187
		// (get) Token: 0x060019AB RID: 6571 RVA: 0x0006F980 File Offset: 0x0006DB80
		public int DisplayPriority
		{
			get
			{
				return this.displayPriority;
			}
		}

		// Token: 0x170004A4 RID: 1188
		// (get) Token: 0x060019AC RID: 6572 RVA: 0x0006F988 File Offset: 0x0006DB88
		// (set) Token: 0x060019AD RID: 6573 RVA: 0x0006F990 File Offset: 0x0006DB90
		public int DisplayOrder
		{
			get
			{
				return this.displayOrder;
			}
			set
			{
				this.displayOrder = value;
			}
		}

		// Token: 0x170004A5 RID: 1189
		// (get) Token: 0x060019AE RID: 6574 RVA: 0x0006F999 File Offset: 0x0006DB99
		public EJournalContentType Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x170004A6 RID: 1190
		// (get) Token: 0x060019AF RID: 6575 RVA: 0x0006F9A1 File Offset: 0x0006DBA1
		// (set) Token: 0x060019B0 RID: 6576 RVA: 0x0006F9A9 File Offset: 0x0006DBA9
		public JournalContentDisplayer Displayer
		{
			get
			{
				return this._displayer;
			}
			set
			{
				this._displayer = value;
			}
		}

		// Token: 0x170004A7 RID: 1191
		// (get) Token: 0x060019B1 RID: 6577 RVA: 0x0006F9B2 File Offset: 0x0006DBB2
		// (set) Token: 0x060019B2 RID: 6578 RVA: 0x0006F9BA File Offset: 0x0006DBBA
		public TextJournalGroupId GroupId
		{
			get
			{
				return this.groupId;
			}
			set
			{
				this.groupId = value;
			}
		}

		// Token: 0x060019B3 RID: 6579 RVA: 0x0006F9C3 File Offset: 0x0006DBC3
		protected JournalContent(SaveEvent saveEvent)
		{
			if (saveEvent == null)
			{
				Debug.LogError("This constructor can be used only for deserialization from SaveEvent");
			}
		}

		// Token: 0x060019B4 RID: 6580 RVA: 0x0006F9DE File Offset: 0x0006DBDE
		protected JournalContent(int displayPriority)
		{
			this.displayPriority = displayPriority;
			this.displayPriority = Mathf.Clamp(this.displayPriority, int.MinValue, int.MaxValue);
		}

		// Token: 0x060019B5 RID: 6581 RVA: 0x0006FA08 File Offset: 0x0006DC08
		public virtual string Serialize()
		{
			return JsonUtility.ToJson(new JournalContentWrapper
			{
				DisplayOrder = this.displayOrder,
				DisplayPriority = this.displayPriority,
				GroupId = ((this.groupId != null) ? this.groupId.Guid : string.Empty),
				Type = this.type
			});
		}

		// Token: 0x060019B6 RID: 6582 RVA: 0x0006FA6C File Offset: 0x0006DC6C
		public virtual void Deserialize(string data, SaveEvent saveEvent)
		{
			JournalContentWrapper wrapper = JsonUtility.FromJson<JournalContentWrapper>(data);
			this.DeserializeBaseWrapper(wrapper, saveEvent);
		}

		// Token: 0x060019B7 RID: 6583 RVA: 0x0006FA88 File Offset: 0x0006DC88
		protected void DeserializeBaseWrapper(JournalContentWrapper wrapper, SaveEvent saveEvent)
		{
			this.displayOrder = wrapper.DisplayOrder;
			this.displayPriority = wrapper.DisplayPriority;
			this.groupId = ((!string.IsNullOrEmpty(wrapper.GroupId)) ? ((TextJournalGroupId)saveEvent.GetReferenceObjectByID(wrapper.GroupId)) : null);
			this.type = wrapper.Type;
		}

		// Token: 0x0400139E RID: 5022
		public const int MIN_DISPLAY_PRIORITY = -2147483648;

		// Token: 0x0400139F RID: 5023
		public const int MAX_DISPLAY_PRIORITY = 2147483647;

		// Token: 0x040013A0 RID: 5024
		[SerializeField]
		protected EJournalContentType type;

		// Token: 0x040013A1 RID: 5025
		[SerializeField]
		[Range(-2.1474836E+09f, 2.1474836E+09f)]
		protected int displayPriority;

		// Token: 0x040013A2 RID: 5026
		[SerializeField]
		protected int displayOrder;

		// Token: 0x040013A3 RID: 5027
		[SerializeField]
		protected TextJournalGroupId groupId;

		// Token: 0x040013A4 RID: 5028
		private JournalContentDisplayer _displayer;
	}
}

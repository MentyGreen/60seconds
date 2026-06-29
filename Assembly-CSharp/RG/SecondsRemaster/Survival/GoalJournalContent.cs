using System;
using I2.Loc;
using RG.Core.SaveSystem;
using RG.Parsecs.GoalEditor;
using UnityEngine;

namespace RG.SecondsRemaster.Survival
{
	// Token: 0x020002EE RID: 750
	[Serializable]
	public sealed class GoalJournalContent : JournalContent
	{
		// Token: 0x060019E6 RID: 6630 RVA: 0x00070A87 File Offset: 0x0006EC87
		public GoalJournalContent(string serializedData, SaveEvent saveEvent) : base(saveEvent)
		{
			this.Deserialize(serializedData, saveEvent);
		}

		// Token: 0x060019E7 RID: 6631 RVA: 0x00070A98 File Offset: 0x0006EC98
		public GoalJournalContent(Goal goal, bool isFailed, bool isAchieved, int displayPriority) : base(displayPriority)
		{
			this.type = EJournalContentType.GOAL;
			this._term = goal.Name;
			this._isAchieved = isAchieved;
			this._isFailed = isFailed;
		}

		// Token: 0x170004B2 RID: 1202
		// (get) Token: 0x060019E8 RID: 6632 RVA: 0x00070AC3 File Offset: 0x0006ECC3
		public LocalizedString Term
		{
			get
			{
				return this._term;
			}
		}

		// Token: 0x170004B3 RID: 1203
		// (get) Token: 0x060019E9 RID: 6633 RVA: 0x00070ACB File Offset: 0x0006ECCB
		public bool IsAchieved
		{
			get
			{
				return this._isAchieved;
			}
		}

		// Token: 0x170004B4 RID: 1204
		// (get) Token: 0x060019EA RID: 6634 RVA: 0x00070AD3 File Offset: 0x0006ECD3
		public bool IsFailed
		{
			get
			{
				return this._isFailed;
			}
		}

		// Token: 0x170004B5 RID: 1205
		// (get) Token: 0x060019EB RID: 6635 RVA: 0x00070ADB File Offset: 0x0006ECDB
		// (set) Token: 0x060019EC RID: 6636 RVA: 0x00070AE3 File Offset: 0x0006ECE3
		public int CheckmarkIndex
		{
			get
			{
				return this._checkmarkIndex;
			}
			set
			{
				this._checkmarkIndex = value;
			}
		}

		// Token: 0x060019ED RID: 6637 RVA: 0x00070AEC File Offset: 0x0006ECEC
		public override string Serialize()
		{
			return JsonUtility.ToJson(new GoalJournalContentWrapper
			{
				DisplayOrder = this.displayOrder,
				DisplayPriority = this.displayPriority,
				GroupId = ((this.groupId != null) ? this.groupId.Guid : string.Empty),
				Type = this.type,
				Term = this._term,
				IsAchieved = this._isAchieved,
				IsFailed = this._isFailed,
				CheckmarkIndex = this._checkmarkIndex
			});
		}

		// Token: 0x060019EE RID: 6638 RVA: 0x00070B80 File Offset: 0x0006ED80
		public override void Deserialize(string data, SaveEvent saveEvent)
		{
			GoalJournalContentWrapper goalJournalContentWrapper = JsonUtility.FromJson<GoalJournalContentWrapper>(data);
			base.DeserializeBaseWrapper(goalJournalContentWrapper, saveEvent);
			this._term = goalJournalContentWrapper.Term;
			this._isAchieved = goalJournalContentWrapper.IsAchieved;
			this._isFailed = goalJournalContentWrapper.IsFailed;
			this._checkmarkIndex = goalJournalContentWrapper.CheckmarkIndex;
		}

		// Token: 0x040013D9 RID: 5081
		[SerializeField]
		private LocalizedString _term;

		// Token: 0x040013DA RID: 5082
		[SerializeField]
		private bool _isAchieved;

		// Token: 0x040013DB RID: 5083
		[SerializeField]
		private bool _isFailed;

		// Token: 0x040013DC RID: 5084
		[SerializeField]
		private int _checkmarkIndex;
	}
}

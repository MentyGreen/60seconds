using System;
using System.Collections.Generic;
using RG.Core.Base;
using RG.Core.SaveSystem;
using UnityEngine;

namespace RG.SecondsRemaster.Survival
{
	// Token: 0x020002EB RID: 747
	[CreateAssetMenu(menuName = "60 Seconds Remaster!/Events Renderer/New Journal Contents List", fileName = "Journal_Contents_List")]
	public class JournalContentsList : RGScriptableObject, ISaveable
	{
		// Token: 0x060019D5 RID: 6613 RVA: 0x0006FF42 File Offset: 0x0006E142
		private void OnEnable()
		{
			this.Register();
		}

		// Token: 0x060019D6 RID: 6614 RVA: 0x0006FF4A File Offset: 0x0006E14A
		private void OnDestroy()
		{
			this.Unregister();
		}

		// Token: 0x060019D7 RID: 6615 RVA: 0x0006FF54 File Offset: 0x0006E154
		public void ClearJournalContentsList()
		{
			this._currentDisplayOrder = 0;
			if (this._textJournalContents == null)
			{
				this._textJournalContents = new TextJournalContentsListEntry();
			}
			if (this._textIconJournalContents == null)
			{
				this._textIconJournalContents = new TextIconJournalContentsListEntry();
			}
			if (this._spriteIconJournalContents == null)
			{
				this._spriteIconJournalContents = new SpriteJournalContentsListEntry();
			}
			if (this._yesNoChoiceJournalContents == null)
			{
				this._yesNoChoiceJournalContents = new YesNoChoiceJournalContentsListEntry();
			}
			if (this._itemChoiceJournalContents == null)
			{
				this._itemChoiceJournalContents = new ItemChoiceJournalContentsListEntry();
			}
			if (this._characterChoiceJournalContents == null)
			{
				this._characterChoiceJournalContents = new CharacterChoiceJournalContentsListEntry();
			}
			if (this._spriteChoiceJournalContents == null)
			{
				this._spriteChoiceJournalContents = new SpriteChoiceJournalContentsListEntry();
			}
			if (this._goalJournalContents == null)
			{
				this._goalJournalContents = new GoalJournalContentsListEntry();
			}
			this._textJournalContents.ClearJournalContents();
			this._textIconJournalContents.ClearJournalContents();
			this._spriteIconJournalContents.ClearJournalContents();
			this._yesNoChoiceJournalContents.ClearJournalContents();
			this._itemChoiceJournalContents.ClearJournalContents();
			this._characterChoiceJournalContents.ClearJournalContents();
			this._spriteChoiceJournalContents.ClearJournalContents();
			this._goalJournalContents.ClearJournalContents();
		}

		// Token: 0x060019D8 RID: 6616 RVA: 0x00070058 File Offset: 0x0006E258
		public List<JournalContent> GetSortedJournalContents()
		{
			List<JournalContent> list = this.AggregateAllContentsToOneList();
			this.SetGroupsData(list);
			this.SortJournalContents(list);
			if (list.Count > 0 && list[0].Type != EJournalContentType.TEXT)
			{
				for (int i = 1; i < list.Count; i++)
				{
					if (list[i].Type == EJournalContentType.TEXT)
					{
						JournalContent item = list[i];
						list.RemoveAt(i);
						list.Insert(0, item);
						break;
					}
				}
			}
			return list;
		}

		// Token: 0x060019D9 RID: 6617 RVA: 0x000700CC File Offset: 0x0006E2CC
		private List<JournalContent> AggregateAllContentsToOneList()
		{
			List<JournalContent> list = new List<JournalContent>();
			for (int i = 0; i < this._textJournalContents.JournalContents.Count; i++)
			{
				list.Add(this._textJournalContents.JournalContents[i]);
			}
			for (int j = 0; j < this._textIconJournalContents.JournalContents.Count; j++)
			{
				list.Add(this._textIconJournalContents.JournalContents[j]);
			}
			for (int k = 0; k < this._spriteIconJournalContents.JournalContents.Count; k++)
			{
				list.Add(this._spriteIconJournalContents.JournalContents[k]);
			}
			for (int l = 0; l < this._yesNoChoiceJournalContents.JournalContents.Count; l++)
			{
				list.Add(this._yesNoChoiceJournalContents.JournalContents[l]);
			}
			for (int m = 0; m < this._itemChoiceJournalContents.JournalContents.Count; m++)
			{
				list.Add(this._itemChoiceJournalContents.JournalContents[m]);
			}
			for (int n = 0; n < this._characterChoiceJournalContents.JournalContents.Count; n++)
			{
				list.Add(this._characterChoiceJournalContents.JournalContents[n]);
			}
			for (int num = 0; num < this._spriteChoiceJournalContents.JournalContents.Count; num++)
			{
				list.Add(this._spriteChoiceJournalContents.JournalContents[num]);
			}
			for (int num2 = 0; num2 < this._goalJournalContents.JournalContents.Count; num2++)
			{
				list.Add(this._goalJournalContents.JournalContents[num2]);
			}
			return list;
		}

		// Token: 0x060019DA RID: 6618 RVA: 0x0007028C File Offset: 0x0006E48C
		private void SetGroupsData(List<JournalContent> contents)
		{
			if (this._groupsData == null)
			{
				this._groupsData = new Dictionary<TextJournalGroupId, GroupData>();
			}
			else
			{
				this._groupsData.Clear();
			}
			for (int i = 0; i < contents.Count; i++)
			{
				JournalContent journalContent = contents[i];
				if (!(journalContent.GroupId == null))
				{
					if (!this._groupsData.ContainsKey(journalContent.GroupId))
					{
						this._groupsData.Add(journalContent.GroupId, new GroupData(journalContent.DisplayOrder, journalContent.DisplayPriority));
					}
					else
					{
						GroupData groupData = this._groupsData[journalContent.GroupId];
						groupData.Priority = ((journalContent.DisplayPriority > groupData.Priority) ? journalContent.DisplayPriority : groupData.Priority);
						groupData.Order = ((journalContent.DisplayOrder < groupData.Order) ? journalContent.DisplayOrder : groupData.Order);
					}
				}
			}
		}

		// Token: 0x060019DB RID: 6619 RVA: 0x00070376 File Offset: 0x0006E576
		private GroupData GetGroupsData(TextJournalGroupId groupId)
		{
			if (this._groupsData.ContainsKey(groupId))
			{
				return this._groupsData[groupId];
			}
			return null;
		}

		// Token: 0x060019DC RID: 6620 RVA: 0x00070394 File Offset: 0x0006E594
		private void SortJournalContents(List<JournalContent> journalContents)
		{
			journalContents.Sort(new Comparison<JournalContent>(this.ContentsPriorityComparer));
		}

		// Token: 0x060019DD RID: 6621 RVA: 0x000703A8 File Offset: 0x0006E5A8
		private int ContentsPriorityComparer(JournalContent x, JournalContent y)
		{
			if (x.GroupId != null && y.GroupId != null && x.GroupId == y.GroupId)
			{
				if (y.DisplayPriority == x.DisplayPriority)
				{
					return x.DisplayOrder.CompareTo(y.DisplayOrder);
				}
				return y.DisplayPriority.CompareTo(x.DisplayPriority);
			}
			else
			{
				int num = (x.GroupId != null) ? this.GetGroupsData(x.GroupId).Priority : x.DisplayPriority;
				int num2 = (y.GroupId != null) ? this.GetGroupsData(y.GroupId).Priority : y.DisplayPriority;
				if (num2 == num)
				{
					int num3 = (x.GroupId != null) ? this.GetGroupsData(x.GroupId).Order : x.DisplayOrder;
					int value = (y.GroupId != null) ? this.GetGroupsData(y.GroupId).Order : y.DisplayOrder;
					return num3.CompareTo(value);
				}
				return num2.CompareTo(num);
			}
		}

		// Token: 0x060019DE RID: 6622 RVA: 0x000704D4 File Offset: 0x0006E6D4
		public void AddJournalContent(JournalContent journalContent)
		{
			journalContent.DisplayOrder = this._currentDisplayOrder;
			this._currentDisplayOrder++;
			switch (journalContent.Type)
			{
			case EJournalContentType.TEXT:
				this._textJournalContents.AddContentToList((TextJournalContent)journalContent);
				return;
			case EJournalContentType.TEXT_ICON:
				this._textIconJournalContents.AddContentToList((TextIconJournalContent)journalContent);
				return;
			case EJournalContentType.SPRITE:
				this._spriteIconJournalContents.AddContentToList((SpriteJournalContent)journalContent);
				return;
			case EJournalContentType.YESNO_CHOICE:
				this._yesNoChoiceJournalContents.AddContentToList((YesNoChoiceJournalContent)journalContent);
				return;
			case EJournalContentType.ITEM_CHOICE:
				this._itemChoiceJournalContents.AddContentToList((ItemChoiceJournalContent)journalContent);
				return;
			case EJournalContentType.CHARACTER_CHOICE:
				this._characterChoiceJournalContents.AddContentToList((CharacterChoiceJournalContent)journalContent);
				return;
			case EJournalContentType.CUSTOM_CHOICE:
				this._spriteChoiceJournalContents.AddContentToList((SpriteChoiceJournalContent)journalContent);
				return;
			case EJournalContentType.GOAL:
				this._goalJournalContents.AddContentToList((GoalJournalContent)journalContent);
				return;
			default:
				Debug.LogWarning("JournalContentsList: Unknown content.");
				return;
			}
		}

		// Token: 0x060019DF RID: 6623 RVA: 0x000705C8 File Offset: 0x0006E7C8
		public string Serialize()
		{
			JournalContentListWrapper journalContentListWrapper = default(JournalContentListWrapper);
			journalContentListWrapper.TextJournalContents = new List<string>();
			journalContentListWrapper.TextIconJournalContents = new List<string>();
			journalContentListWrapper.SpriteIconJournalContents = new List<string>();
			journalContentListWrapper.YesNoChoiceJournalContents = new List<string>();
			journalContentListWrapper.ItemChoiceJournalContents = new List<string>();
			journalContentListWrapper.CharacterChoiceJournalContents = new List<string>();
			journalContentListWrapper.SpriteChoiceJournalContents = new List<string>();
			journalContentListWrapper.GoalJournalContents = new List<string>();
			for (int i = 0; i < this._textJournalContents.JournalContents.Count; i++)
			{
				journalContentListWrapper.TextJournalContents.Add(this._textJournalContents.JournalContents[i].Serialize());
			}
			for (int j = 0; j < this._textIconJournalContents.JournalContents.Count; j++)
			{
				journalContentListWrapper.TextIconJournalContents.Add(this._textIconJournalContents.JournalContents[j].Serialize());
			}
			for (int k = 0; k < this._spriteIconJournalContents.JournalContents.Count; k++)
			{
				journalContentListWrapper.SpriteIconJournalContents.Add(this._spriteIconJournalContents.JournalContents[k].Serialize());
			}
			for (int l = 0; l < this._yesNoChoiceJournalContents.JournalContents.Count; l++)
			{
				journalContentListWrapper.YesNoChoiceJournalContents.Add(this._yesNoChoiceJournalContents.JournalContents[l].Serialize());
			}
			for (int m = 0; m < this._itemChoiceJournalContents.JournalContents.Count; m++)
			{
				journalContentListWrapper.ItemChoiceJournalContents.Add(this._itemChoiceJournalContents.JournalContents[m].Serialize());
			}
			for (int n = 0; n < this._characterChoiceJournalContents.JournalContents.Count; n++)
			{
				journalContentListWrapper.CharacterChoiceJournalContents.Add(this._characterChoiceJournalContents.JournalContents[n].Serialize());
			}
			for (int num = 0; num < this._spriteChoiceJournalContents.JournalContents.Count; num++)
			{
				journalContentListWrapper.SpriteChoiceJournalContents.Add(this._spriteChoiceJournalContents.JournalContents[num].Serialize());
			}
			for (int num2 = 0; num2 < this._goalJournalContents.JournalContents.Count; num2++)
			{
				journalContentListWrapper.GoalJournalContents.Add(this._goalJournalContents.JournalContents[num2].Serialize());
			}
			return JsonUtility.ToJson(journalContentListWrapper);
		}

		// Token: 0x060019E0 RID: 6624 RVA: 0x00070844 File Offset: 0x0006EA44
		public void Deserialize(string jsonData)
		{
			JournalContentListWrapper journalContentListWrapper = JsonUtility.FromJson<JournalContentListWrapper>(jsonData);
			this.ClearJournalContentsList();
			for (int i = 0; i < journalContentListWrapper.TextJournalContents.Count; i++)
			{
				this._textJournalContents.AddContentToList(new TextJournalContent(journalContentListWrapper.TextJournalContents[i], this._saveEvent));
			}
			for (int j = 0; j < journalContentListWrapper.TextIconJournalContents.Count; j++)
			{
				this._textIconJournalContents.AddContentToList(new TextIconJournalContent(journalContentListWrapper.TextIconJournalContents[j], this._saveEvent));
			}
			for (int k = 0; k < journalContentListWrapper.SpriteIconJournalContents.Count; k++)
			{
				this._spriteIconJournalContents.AddContentToList(new SpriteJournalContent(journalContentListWrapper.SpriteIconJournalContents[k], this._saveEvent));
			}
			for (int l = 0; l < journalContentListWrapper.YesNoChoiceJournalContents.Count; l++)
			{
				this._yesNoChoiceJournalContents.AddContentToList(new YesNoChoiceJournalContent(journalContentListWrapper.YesNoChoiceJournalContents[l], this._saveEvent));
			}
			for (int m = 0; m < journalContentListWrapper.ItemChoiceJournalContents.Count; m++)
			{
				this._itemChoiceJournalContents.AddContentToList(new ItemChoiceJournalContent(journalContentListWrapper.ItemChoiceJournalContents[m], this._saveEvent));
			}
			for (int n = 0; n < journalContentListWrapper.CharacterChoiceJournalContents.Count; n++)
			{
				this._characterChoiceJournalContents.AddContentToList(new CharacterChoiceJournalContent(journalContentListWrapper.CharacterChoiceJournalContents[n], this._saveEvent));
			}
			for (int num = 0; num < journalContentListWrapper.SpriteChoiceJournalContents.Count; num++)
			{
				this._spriteChoiceJournalContents.AddContentToList(new SpriteChoiceJournalContent(journalContentListWrapper.SpriteChoiceJournalContents[num], this._saveEvent));
			}
			for (int num2 = 0; num2 < journalContentListWrapper.GoalJournalContents.Count; num2++)
			{
				this._goalJournalContents.AddContentToList(new GoalJournalContent(journalContentListWrapper.GoalJournalContents[num2], this._saveEvent));
			}
		}

		// Token: 0x060019E1 RID: 6625 RVA: 0x00070A37 File Offset: 0x0006EC37
		public void Register()
		{
			if (this._saveEvent != null)
			{
				this._saveEvent.RegisterListener(this);
			}
		}

		// Token: 0x060019E2 RID: 6626 RVA: 0x00070A53 File Offset: 0x0006EC53
		public void Unregister()
		{
			if (this._saveEvent != null)
			{
				this._saveEvent.UnregisterListener(this);
			}
		}

		// Token: 0x170004B1 RID: 1201
		// (get) Token: 0x060019E3 RID: 6627 RVA: 0x00070A6F File Offset: 0x0006EC6F
		public string ID
		{
			get
			{
				return this.Guid;
			}
		}

		// Token: 0x060019E4 RID: 6628 RVA: 0x00070A77 File Offset: 0x0006EC77
		public void ResetData()
		{
			this.ClearJournalContentsList();
		}

		// Token: 0x040013BD RID: 5053
		[SerializeField]
		private SaveEvent _saveEvent;

		// Token: 0x040013BE RID: 5054
		[SerializeField]
		private TextJournalContentsListEntry _textJournalContents;

		// Token: 0x040013BF RID: 5055
		[SerializeField]
		private TextIconJournalContentsListEntry _textIconJournalContents;

		// Token: 0x040013C0 RID: 5056
		[SerializeField]
		private SpriteJournalContentsListEntry _spriteIconJournalContents;

		// Token: 0x040013C1 RID: 5057
		[SerializeField]
		private YesNoChoiceJournalContentsListEntry _yesNoChoiceJournalContents;

		// Token: 0x040013C2 RID: 5058
		[SerializeField]
		private ItemChoiceJournalContentsListEntry _itemChoiceJournalContents;

		// Token: 0x040013C3 RID: 5059
		[SerializeField]
		private CharacterChoiceJournalContentsListEntry _characterChoiceJournalContents;

		// Token: 0x040013C4 RID: 5060
		[SerializeField]
		private SpriteChoiceJournalContentsListEntry _spriteChoiceJournalContents;

		// Token: 0x040013C5 RID: 5061
		[SerializeField]
		private GoalJournalContentsListEntry _goalJournalContents;

		// Token: 0x040013C6 RID: 5062
		private Dictionary<TextJournalGroupId, GroupData> _groupsData;

		// Token: 0x040013C7 RID: 5063
		private int _currentDisplayOrder;
	}
}

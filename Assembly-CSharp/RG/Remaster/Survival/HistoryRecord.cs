using System;
using System.Collections.Generic;
using I2.Loc;
using RG.Core.Base;
using RG.Core.SaveSystem;
using UnityEngine;

namespace RG.Remaster.Survival
{
	// Token: 0x02000231 RID: 561
	[CreateAssetMenu(fileName = "HistoryRecord", menuName = "60 Seconds Remaster!/New HistoryRecord")]
	[Serializable]
	public class HistoryRecord : RGScriptableObject, ISaveable
	{
		// Token: 0x17000416 RID: 1046
		// (get) Token: 0x0600158B RID: 5515 RVA: 0x0005F526 File Offset: 0x0005D726
		public List<LocalizedString> Entries
		{
			get
			{
				return this._entries;
			}
		}

		// Token: 0x17000417 RID: 1047
		// (get) Token: 0x0600158C RID: 5516 RVA: 0x0005F52E File Offset: 0x0005D72E
		public List<int> Days
		{
			get
			{
				return this._days;
			}
		}

		// Token: 0x17000418 RID: 1048
		// (get) Token: 0x0600158D RID: 5517 RVA: 0x0005F536 File Offset: 0x0005D736
		public LocalizedString DayTerm
		{
			get
			{
				return this._dayTerm;
			}
		}

		// Token: 0x17000419 RID: 1049
		// (get) Token: 0x0600158E RID: 5518 RVA: 0x0005F53E File Offset: 0x0005D73E
		public LocalizedString DayTitleSeparatorTerm
		{
			get
			{
				return this._dayTitleSeparatorTerm;
			}
		}

		// Token: 0x1700041A RID: 1050
		// (get) Token: 0x0600158F RID: 5519 RVA: 0x0005F546 File Offset: 0x0005D746
		public string ID
		{
			get
			{
				return this.Guid;
			}
		}

		// Token: 0x06001590 RID: 5520 RVA: 0x0005F54E File Offset: 0x0005D74E
		private void OnEnable()
		{
			this._saveEvent.RegisterListener(this);
		}

		// Token: 0x06001591 RID: 5521 RVA: 0x0005F55C File Offset: 0x0005D75C
		public string Serialize()
		{
			HistoryRecord.HistoryRecordWrapper historyRecordWrapper = new HistoryRecord.HistoryRecordWrapper
			{
				Entries = new List<LocalizedString>(),
				Days = new List<int>()
			};
			for (int i = 0; i < this._entries.Count; i++)
			{
				historyRecordWrapper.Entries.Add(this._entries[i]);
			}
			for (int j = 0; j < this._days.Count; j++)
			{
				historyRecordWrapper.Days.Add(this._days[j]);
			}
			return JsonUtility.ToJson(historyRecordWrapper);
		}

		// Token: 0x06001592 RID: 5522 RVA: 0x0005F5F0 File Offset: 0x0005D7F0
		public void Deserialize(string jsonData)
		{
			HistoryRecord.HistoryRecordWrapper historyRecordWrapper = JsonUtility.FromJson<HistoryRecord.HistoryRecordWrapper>(jsonData);
			this.Reset();
			for (int i = 0; i < historyRecordWrapper.Entries.Count; i++)
			{
				if (historyRecordWrapper.Entries[i] != null && !string.IsNullOrEmpty(historyRecordWrapper.Entries[i]))
				{
					this._entries.Add(historyRecordWrapper.Entries[i]);
				}
				else
				{
					Debug.LogWarningFormat("Current HistoryRecord entry is null or empty - index {0}", new object[]
					{
						i
					});
				}
			}
			for (int j = 0; j < historyRecordWrapper.Days.Count; j++)
			{
				this.Days.Add(historyRecordWrapper.Days[j]);
			}
		}

		// Token: 0x06001593 RID: 5523 RVA: 0x0005F6AA File Offset: 0x0005D8AA
		public void DefaultData()
		{
		}

		// Token: 0x06001594 RID: 5524 RVA: 0x0005F6AC File Offset: 0x0005D8AC
		public void Register()
		{
			this._saveEvent.RegisterListener(this);
		}

		// Token: 0x06001595 RID: 5525 RVA: 0x0005F6BA File Offset: 0x0005D8BA
		public void Unregister()
		{
			this._saveEvent.UnregisterListener(this);
		}

		// Token: 0x06001596 RID: 5526 RVA: 0x0005F6C8 File Offset: 0x0005D8C8
		public void ResetData()
		{
			this.Reset();
		}

		// Token: 0x06001597 RID: 5527 RVA: 0x0005F6D0 File Offset: 0x0005D8D0
		public void Reset()
		{
			this._entries.Clear();
			this._days.Clear();
		}

		// Token: 0x06001598 RID: 5528 RVA: 0x0005F6E8 File Offset: 0x0005D8E8
		public void AddEntry(LocalizedString entry, int day)
		{
			this._entries.Add(entry);
			this._days.Add(day);
			if (this._entries.Count > 1 && this._days.Count > 1 && this._days[this._days.Count - 1] < this._days[this._days.Count - 2])
			{
				this._days.Reverse(this._days.Count - 2, 2);
				this._entries.Reverse(this._entries.Count - 2, 2);
			}
		}

		// Token: 0x06001599 RID: 5529 RVA: 0x0005F790 File Offset: 0x0005D990
		public bool HasValidSetup()
		{
			return this._entries != null && this._days != null && this._dayTerm != null && !string.IsNullOrEmpty(this._dayTerm) && this._dayTitleSeparatorTerm != null && !string.IsNullOrEmpty(this._dayTitleSeparatorTerm);
		}

		// Token: 0x04000E80 RID: 3712
		[SerializeField]
		private List<LocalizedString> _entries = new List<LocalizedString>();

		// Token: 0x04000E81 RID: 3713
		[SerializeField]
		private List<int> _days = new List<int>();

		// Token: 0x04000E82 RID: 3714
		[SerializeField]
		private LocalizedString _dayTerm;

		// Token: 0x04000E83 RID: 3715
		[SerializeField]
		private LocalizedString _dayTitleSeparatorTerm;

		// Token: 0x04000E84 RID: 3716
		[Tooltip("Event to which data will be save and from which be loaded")]
		[SerializeField]
		private SaveEvent _saveEvent;

		// Token: 0x0200041B RID: 1051
		internal struct HistoryRecordWrapper
		{
			// Token: 0x040018B2 RID: 6322
			public List<LocalizedString> Entries;

			// Token: 0x040018B3 RID: 6323
			public List<int> Days;
		}
	}
}

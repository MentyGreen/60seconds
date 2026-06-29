using System;
using System.Text;
using I2.Loc;
using RG.Core.Base;
using RG.Parsecs.Survival;
using UnityEngine;

namespace RG.Remaster.Survival
{
	// Token: 0x02000234 RID: 564
	public class SimpleHistoryManager : RGMonoBehaviour
	{
		// Token: 0x1700041B RID: 1051
		// (get) Token: 0x060015A4 RID: 5540 RVA: 0x0006003B File Offset: 0x0005E23B
		public static SimpleHistoryManager Instance
		{
			get
			{
				if (SimpleHistoryManager._instance == null)
				{
					SimpleHistoryManager.FindInstanceInScene();
				}
				return SimpleHistoryManager._instance;
			}
		}

		// Token: 0x060015A5 RID: 5541 RVA: 0x00060054 File Offset: 0x0005E254
		private static void FindInstanceInScene()
		{
			SimpleHistoryManager._instance = Object.FindObjectOfType<SimpleHistoryManager>();
			if (SimpleHistoryManager._instance == null)
			{
				Debug.LogError("No SimpleHistoryManager in scene!");
			}
		}

		// Token: 0x060015A6 RID: 5542 RVA: 0x00060078 File Offset: 0x0005E278
		protected override void CustomAwake()
		{
			if (SimpleHistoryManager._instance == null)
			{
				SimpleHistoryManager._instance = this;
				return;
			}
			if (SimpleHistoryManager._instance != this)
			{
				Debug.LogWarning("Duplicate instance of SimpleHistoryManager detected! Removing " + base.gameObject.name);
				Object.Destroy(this);
			}
		}

		// Token: 0x060015A7 RID: 5543 RVA: 0x000600C8 File Offset: 0x0005E2C8
		public string RenderHistoryToString()
		{
			string empty = string.Empty;
			if (!(this._historyRecord != null))
			{
				Debug.LogError("No HistoryRecord set up in SimpleHistoryManager!");
				return empty;
			}
			if (this._historyRecord.HasValidSetup())
			{
				StringBuilder stringBuilder = new StringBuilder();
				for (int i = 0; i < this._historyRecord.Entries.Count; i++)
				{
					stringBuilder.Append(string.Format(this._historyRecord.DayTerm, this._historyRecord.Days[i]));
					stringBuilder.Append(this._historyRecord.DayTitleSeparatorTerm);
					stringBuilder.Append(this._historyRecord.Entries[i]);
					stringBuilder.AppendLine();
				}
				return stringBuilder.ToString();
			}
			Debug.LogError("HistoryRecord used in SimpleHistoryManager has invalid setup!");
			return empty;
		}

		// Token: 0x060015A8 RID: 5544 RVA: 0x000601B0 File Offset: 0x0005E3B0
		public void AddEntry(LocalizedString entry, int day, bool useCurrentDay)
		{
			if (string.IsNullOrEmpty(entry))
			{
				Debug.LogWarning("Empty entry term - was not added to HistoryRecord.");
				return;
			}
			if (!useCurrentDay)
			{
				this._historyRecord.AddEntry(entry, day);
				return;
			}
			if (this._survivalData != null)
			{
				this._historyRecord.AddEntry(entry, this._survivalData.DisplayDay);
				return;
			}
			Debug.LogWarning("No SurvivalData set in SimpleHistoryManager, no entry added.");
		}

		// Token: 0x04000E95 RID: 3733
		private const string DAY_NUMBER_SEPARATOR = " ";

		// Token: 0x04000E96 RID: 3734
		private static SimpleHistoryManager _instance;

		// Token: 0x04000E97 RID: 3735
		[SerializeField]
		private HistoryRecord _historyRecord;

		// Token: 0x04000E98 RID: 3736
		[SerializeField]
		private SurvivalData _survivalData;
	}
}

using System;
using RG.Core.Base;
using RG.Core.SaveSystem;
using UnityEngine;

namespace RG.SecondsRemaster.Survival
{
	// Token: 0x02000303 RID: 771
	[CreateAssetMenu(menuName = "60 Seconds Remaster!/New GroupID")]
	public class TextJournalGroupId : RGScriptableObject, ISaveable
	{
		// Token: 0x06001A3F RID: 6719 RVA: 0x00071F01 File Offset: 0x00070101
		public string Serialize()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001A40 RID: 6720 RVA: 0x00071F08 File Offset: 0x00070108
		public void Deserialize(string jsonData)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001A41 RID: 6721 RVA: 0x00071F0F File Offset: 0x0007010F
		public void Register()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001A42 RID: 6722 RVA: 0x00071F16 File Offset: 0x00070116
		public void Unregister()
		{
			throw new NotImplementedException();
		}

		// Token: 0x170004C9 RID: 1225
		// (get) Token: 0x06001A43 RID: 6723 RVA: 0x00071F1D File Offset: 0x0007011D
		public string ID
		{
			get
			{
				return this.Guid;
			}
		}

		// Token: 0x06001A44 RID: 6724 RVA: 0x00071F25 File Offset: 0x00070125
		public void ResetData()
		{
			throw new NotImplementedException();
		}

		// Token: 0x04001416 RID: 5142
		[SerializeField]
		private SaveEvent _saveEvent;
	}
}

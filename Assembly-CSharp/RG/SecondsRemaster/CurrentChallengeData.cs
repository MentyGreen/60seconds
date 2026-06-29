using System;
using RG.Core.Base;
using RG.Core.SaveSystem;
using UnityEngine;

namespace RG.SecondsRemaster
{
	// Token: 0x02000242 RID: 578
	[CreateAssetMenu(menuName = "60 Seconds Remaster!/Challenges/New Current Challenge Data", fileName = "New Current Challenge Data")]
	public class CurrentChallengeData : RGScriptableObject, ISaveable
	{
		// Token: 0x060015FB RID: 5627 RVA: 0x00060BFA File Offset: 0x0005EDFA
		private void OnEnable()
		{
			this.Register();
		}

		// Token: 0x060015FC RID: 5628 RVA: 0x00060C02 File Offset: 0x0005EE02
		private void OnDisable()
		{
			this.Unregister();
		}

		// Token: 0x060015FD RID: 5629 RVA: 0x00060C0A File Offset: 0x0005EE0A
		public string Serialize()
		{
			return JsonUtility.ToJson(new CurrentChallengeRuntimeDataWrapper
			{
				Challenge = (this._runtimeData.Challenge ? this._runtimeData.Challenge.Guid : string.Empty)
			});
		}

		// Token: 0x060015FE RID: 5630 RVA: 0x00060C48 File Offset: 0x0005EE48
		public void Deserialize(string jsonData)
		{
			CurrentChallengeRuntimeDataWrapper currentChallengeRuntimeDataWrapper = JsonUtility.FromJson<CurrentChallengeRuntimeDataWrapper>(jsonData);
			this._runtimeData.Challenge = ((!string.IsNullOrEmpty(currentChallengeRuntimeDataWrapper.Challenge)) ? (this._saveEvent.GetReferenceObjectByID(currentChallengeRuntimeDataWrapper.Challenge) as Challenge) : null);
		}

		// Token: 0x060015FF RID: 5631 RVA: 0x00060C8D File Offset: 0x0005EE8D
		public void Register()
		{
			if (this._saveEvent != null)
			{
				this._saveEvent.RegisterListener(this);
			}
		}

		// Token: 0x06001600 RID: 5632 RVA: 0x00060CA9 File Offset: 0x0005EEA9
		public void Unregister()
		{
			if (this._saveEvent != null)
			{
				this._saveEvent.UnregisterListener(this);
			}
		}

		// Token: 0x17000436 RID: 1078
		// (get) Token: 0x06001601 RID: 5633 RVA: 0x00060CC5 File Offset: 0x0005EEC5
		public string ID
		{
			get
			{
				return this.Guid;
			}
		}

		// Token: 0x17000437 RID: 1079
		// (get) Token: 0x06001602 RID: 5634 RVA: 0x00060CCD File Offset: 0x0005EECD
		public CurrentChallengeRuntimeData RuntimeData
		{
			get
			{
				return this._runtimeData;
			}
		}

		// Token: 0x06001603 RID: 5635 RVA: 0x00060CD5 File Offset: 0x0005EED5
		public void ResetData()
		{
			this._runtimeData.Challenge = null;
		}

		// Token: 0x04000EC1 RID: 3777
		[SerializeField]
		private SaveEvent _saveEvent;

		// Token: 0x04000EC2 RID: 3778
		[SerializeField]
		private CurrentChallengeRuntimeData _runtimeData;
	}
}

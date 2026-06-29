using System;
using Steamworks;
using UnityEngine;

namespace RG.Remaster.Common
{
	// Token: 0x0200021C RID: 540
	[Serializable]
	public class RichPresenceManager
	{
		// Token: 0x0600151D RID: 5405 RVA: 0x0005DD89 File Offset: 0x0005BF89
		public void Initialize()
		{
			this.SetRichPresenceStatus(this._defaultRichPresenceStatus);
		}

		// Token: 0x0600151E RID: 5406 RVA: 0x0005DD97 File Offset: 0x0005BF97
		private RichPresence GetRichPresence(ERichPresenceStatus statusId)
		{
			if (this._richPresenceData != null)
			{
				return this._richPresenceData.GetRichPresence(statusId);
			}
			return null;
		}

		// Token: 0x0600151F RID: 5407 RVA: 0x0005DDB5 File Offset: 0x0005BFB5
		public void SetParametrizedRichPresence(ERichPresenceStatus statusId, ERichPresenceParameter paramId, string paramValue)
		{
			this.SetRichPresenceParameter(statusId, paramId, paramValue);
			this.SetRichPresenceStatus(statusId);
		}

		// Token: 0x06001520 RID: 5408 RVA: 0x0005DDC8 File Offset: 0x0005BFC8
		public void SetRichPresenceParameter(ERichPresenceStatus statusId, ERichPresenceParameter paramId, string paramValue)
		{
			RichPresence richPresence = this.GetRichPresence(statusId);
			if (SteamManager.Initialized && SteamAPI.IsSteamRunning() && !string.IsNullOrEmpty(paramValue))
			{
				string parametrizedRichPresence = richPresence.GetParametrizedRichPresence(paramId);
				if (!string.IsNullOrEmpty(parametrizedRichPresence))
				{
					SteamFriends.SetRichPresence(parametrizedRichPresence, paramValue);
				}
			}
		}

		// Token: 0x06001521 RID: 5409 RVA: 0x0005DE0C File Offset: 0x0005C00C
		public void SetRichPresenceStatus(ERichPresenceStatus statusId)
		{
			RichPresence richPresence = this.GetRichPresence(statusId);
			if (richPresence != null)
			{
				this._currentRichPresenceStatus = statusId;
				if (SteamManager.Initialized && SteamAPI.IsSteamRunning())
				{
					SteamFriends.SetRichPresence("steam_display", richPresence.Content);
					SteamFriends.SetRichPresence("status", richPresence.Content);
				}
			}
		}

		// Token: 0x17000402 RID: 1026
		// (get) Token: 0x06001522 RID: 5410 RVA: 0x0005DE5B File Offset: 0x0005C05B
		public ERichPresenceStatus CurrentRichPresenceStatus
		{
			get
			{
				return this._currentRichPresenceStatus;
			}
		}

		// Token: 0x04000DFE RID: 3582
		private const string STEAM_DISPLAY_STR = "steam_display";

		// Token: 0x04000DFF RID: 3583
		private const string STEAM_STATUS_STR = "status";

		// Token: 0x04000E00 RID: 3584
		[SerializeField]
		private ERichPresenceStatus _defaultRichPresenceStatus;

		// Token: 0x04000E01 RID: 3585
		[SerializeField]
		private RichPresenceData _richPresenceData;

		// Token: 0x04000E02 RID: 3586
		private ERichPresenceStatus _currentRichPresenceStatus;
	}
}

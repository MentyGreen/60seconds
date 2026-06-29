using System;
using RG.Core.Base;
using UnityEngine;

namespace RG.Remaster.Common
{
	// Token: 0x0200021B RID: 539
	[CreateAssetMenu(menuName = "60 Seconds Remaster!/Platform/New Rich Presence Data", fileName = "New Rich Presence Data")]
	[Serializable]
	public class RichPresenceData : RGScriptableObject
	{
		// Token: 0x06001519 RID: 5401 RVA: 0x0005DCFC File Offset: 0x0005BEFC
		public RichPresence GetRichPresence(ERichPresenceStatus statusId)
		{
			if (this._richPresences != null)
			{
				for (int i = 0; i < this._richPresences.Length; i++)
				{
					if (this._richPresences[i].StatusId == statusId)
					{
						return this._richPresences[i];
					}
				}
			}
			return null;
		}

		// Token: 0x0600151A RID: 5402 RVA: 0x0005DD40 File Offset: 0x0005BF40
		public string GetRichPresenceContent(ERichPresenceStatus statusId)
		{
			RichPresence richPresence = this.GetRichPresence(statusId);
			if (richPresence != null)
			{
				return richPresence.Content;
			}
			return null;
		}

		// Token: 0x0600151B RID: 5403 RVA: 0x0005DD60 File Offset: 0x0005BF60
		public string GetParametrizedRichPresence(ERichPresenceStatus statusId, ERichPresenceParameter paramId)
		{
			RichPresence richPresence = this.GetRichPresence(statusId);
			if (richPresence != null)
			{
				return richPresence.GetParametrizedRichPresence(paramId);
			}
			return null;
		}

		// Token: 0x04000DFD RID: 3581
		[SerializeField]
		private RichPresence[] _richPresences;
	}
}

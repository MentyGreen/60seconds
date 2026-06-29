using System;
using RG.Parsecs.Common;
using UnityEngine;

namespace RG.Remaster.Common
{
	// Token: 0x02000216 RID: 534
	public class PlatformManager : Singleton<PlatformManager>
	{
		// Token: 0x06001512 RID: 5394 RVA: 0x0005DC63 File Offset: 0x0005BE63
		private void Start()
		{
			if (this._richPresenceManager != null)
			{
				this._richPresenceManager.Initialize();
			}
		}

		// Token: 0x170003FF RID: 1023
		// (get) Token: 0x06001513 RID: 5395 RVA: 0x0005DC78 File Offset: 0x0005BE78
		public RichPresenceManager RichPresenceManager
		{
			get
			{
				return this._richPresenceManager;
			}
		}

		// Token: 0x04000DEE RID: 3566
		[SerializeField]
		private RichPresenceManager _richPresenceManager;
	}
}

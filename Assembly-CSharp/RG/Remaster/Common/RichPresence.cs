using System;
using UnityEngine;

namespace RG.Remaster.Common
{
	// Token: 0x02000219 RID: 537
	[Serializable]
	public class RichPresence
	{
		// Token: 0x06001515 RID: 5397 RVA: 0x0005DC88 File Offset: 0x0005BE88
		public string GetParametrizedRichPresence(ERichPresenceParameter paramId)
		{
			if (this._parameters != null)
			{
				for (int i = 0; i < this._parameters.Length; i++)
				{
					if (this._parameters[i].Id == paramId)
					{
						return this._parameters[i].Content;
					}
				}
			}
			return null;
		}

		// Token: 0x17000400 RID: 1024
		// (get) Token: 0x06001516 RID: 5398 RVA: 0x0005DCD7 File Offset: 0x0005BED7
		public ERichPresenceStatus StatusId
		{
			get
			{
				return this._statusId;
			}
		}

		// Token: 0x17000401 RID: 1025
		// (get) Token: 0x06001517 RID: 5399 RVA: 0x0005DCDF File Offset: 0x0005BEDF
		public string Content
		{
			get
			{
				return this._content;
			}
		}

		// Token: 0x04000DF8 RID: 3576
		[SerializeField]
		private ERichPresenceStatus _statusId;

		// Token: 0x04000DF9 RID: 3577
		[SerializeField]
		private string _content = string.Empty;

		// Token: 0x04000DFA RID: 3578
		[SerializeField]
		private SParametrizedRichPresence[] _parameters;
	}
}

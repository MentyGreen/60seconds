using System;
using UnityEngine;

namespace RG.Remaster.Survival
{
	// Token: 0x02000235 RID: 565
	public class Skin : MonoBehaviour
	{
		// Token: 0x1700041C RID: 1052
		// (get) Token: 0x060015AA RID: 5546 RVA: 0x0006021E File Offset: 0x0005E41E
		public SkinId Id
		{
			get
			{
				return this._id;
			}
		}

		// Token: 0x04000E99 RID: 3737
		[SerializeField]
		private SkinId _id;
	}
}

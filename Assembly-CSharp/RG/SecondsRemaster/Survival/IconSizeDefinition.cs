using System;
using RG.Core.Base;
using UnityEngine;

namespace RG.SecondsRemaster.Survival
{
	// Token: 0x02000348 RID: 840
	[CreateAssetMenu(menuName = "60 Seconds Remaster!/Icon Size Definition")]
	public class IconSizeDefinition : RGScriptableObject
	{
		// Token: 0x170004EA RID: 1258
		// (get) Token: 0x06001BD5 RID: 7125 RVA: 0x0007751B File Offset: 0x0007571B
		public Vector2 Size
		{
			get
			{
				return this._size;
			}
		}

		// Token: 0x0400159A RID: 5530
		[SerializeField]
		private Vector2 _size;
	}
}

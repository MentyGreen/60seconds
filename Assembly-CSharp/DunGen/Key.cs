using System;
using UnityEngine;

namespace DunGen
{
	// Token: 0x020001E8 RID: 488
	[Serializable]
	public sealed class Key
	{
		// Token: 0x170003AE RID: 942
		// (get) Token: 0x060013CB RID: 5067 RVA: 0x00058F9C File Offset: 0x0005719C
		// (set) Token: 0x060013CC RID: 5068 RVA: 0x00058FA4 File Offset: 0x000571A4
		public int ID
		{
			get
			{
				return this.id;
			}
			set
			{
				this.id = value;
			}
		}

		// Token: 0x170003AF RID: 943
		// (get) Token: 0x060013CD RID: 5069 RVA: 0x00058FAD File Offset: 0x000571AD
		// (set) Token: 0x060013CE RID: 5070 RVA: 0x00058FB5 File Offset: 0x000571B5
		public string Name
		{
			get
			{
				return this.name;
			}
			internal set
			{
				this.name = value;
			}
		}

		// Token: 0x060013CF RID: 5071 RVA: 0x00058FBE File Offset: 0x000571BE
		internal Key(int id)
		{
			this.id = id;
		}

		// Token: 0x04000D0D RID: 3341
		public GameObject Prefab;

		// Token: 0x04000D0E RID: 3342
		public Color Colour;

		// Token: 0x04000D0F RID: 3343
		[SerializeField]
		private int id;

		// Token: 0x04000D10 RID: 3344
		[SerializeField]
		private string name;
	}
}

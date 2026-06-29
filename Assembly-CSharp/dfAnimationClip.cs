using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000034 RID: 52
[AddComponentMenu("Daikon Forge/User Interface/Animation Clip")]
[Serializable]
public class dfAnimationClip : MonoBehaviour
{
	// Token: 0x1700015D RID: 349
	// (get) Token: 0x060005D7 RID: 1495 RVA: 0x0001BC75 File Offset: 0x00019E75
	// (set) Token: 0x060005D8 RID: 1496 RVA: 0x0001BC7D File Offset: 0x00019E7D
	public dfAtlas Atlas
	{
		get
		{
			return this.atlas;
		}
		set
		{
			this.atlas = value;
		}
	}

	// Token: 0x1700015E RID: 350
	// (get) Token: 0x060005D9 RID: 1497 RVA: 0x0001BC86 File Offset: 0x00019E86
	public List<string> Sprites
	{
		get
		{
			return this.sprites;
		}
	}

	// Token: 0x0400020E RID: 526
	[SerializeField]
	private dfAtlas atlas;

	// Token: 0x0400020F RID: 527
	[SerializeField]
	private List<string> sprites = new List<string>();
}

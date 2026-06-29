using System;
using UnityEngine;

// Token: 0x020000E0 RID: 224
public class CFX_Demo_RandomDir : MonoBehaviour
{
	// Token: 0x06000B86 RID: 2950 RVA: 0x00032648 File Offset: 0x00030848
	private void Start()
	{
		base.transform.eulerAngles = new Vector3(Random.Range(this.min.x, this.max.x), Random.Range(this.min.y, this.max.y), Random.Range(this.min.z, this.max.z));
	}

	// Token: 0x040005E4 RID: 1508
	public Vector3 min = new Vector3(0f, 0f, 0f);

	// Token: 0x040005E5 RID: 1509
	public Vector3 max = new Vector3(0f, 360f, 0f);
}

using System;
using UnityEngine;

// Token: 0x020000E5 RID: 229
public class CFX_AutodestructWhenNoChildren : MonoBehaviour
{
	// Token: 0x06000B93 RID: 2963 RVA: 0x0003288C File Offset: 0x00030A8C
	private void Update()
	{
		if (base.transform.childCount == 0)
		{
			Object.Destroy(base.gameObject);
		}
	}
}

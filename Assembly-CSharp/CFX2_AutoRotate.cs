using System;
using UnityEngine;

// Token: 0x020000E9 RID: 233
public class CFX2_AutoRotate : MonoBehaviour
{
	// Token: 0x06000BA5 RID: 2981 RVA: 0x00032D45 File Offset: 0x00030F45
	private void Update()
	{
		base.transform.Rotate(this.speed * Time.deltaTime);
	}

	// Token: 0x04000600 RID: 1536
	public Vector3 speed = new Vector3(0f, 40f, 0f);
}

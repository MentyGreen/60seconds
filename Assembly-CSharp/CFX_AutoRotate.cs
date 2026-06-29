using System;
using UnityEngine;

// Token: 0x020000E4 RID: 228
public class CFX_AutoRotate : MonoBehaviour
{
	// Token: 0x06000B91 RID: 2961 RVA: 0x0003285A File Offset: 0x00030A5A
	private void Update()
	{
		base.transform.Rotate(this.rotation * Time.deltaTime, this.space);
	}

	// Token: 0x040005EF RID: 1519
	public Vector3 rotation;

	// Token: 0x040005F0 RID: 1520
	public Space space = Space.Self;
}

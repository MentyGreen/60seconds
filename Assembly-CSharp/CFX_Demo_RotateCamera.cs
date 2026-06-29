using System;
using UnityEngine;

// Token: 0x020000E2 RID: 226
public class CFX_Demo_RotateCamera : MonoBehaviour
{
	// Token: 0x06000B8B RID: 2955 RVA: 0x000327EA File Offset: 0x000309EA
	private void Update()
	{
		if (CFX_Demo_RotateCamera.rotating)
		{
			base.transform.RotateAround(this.rotationCenter.position, Vector3.up, this.speed * Time.deltaTime);
		}
	}

	// Token: 0x040005EB RID: 1515
	public static bool rotating = true;

	// Token: 0x040005EC RID: 1516
	public float speed = 30f;

	// Token: 0x040005ED RID: 1517
	public Transform rotationCenter;
}

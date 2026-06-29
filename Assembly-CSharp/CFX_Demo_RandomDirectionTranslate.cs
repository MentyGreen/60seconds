using System;
using UnityEngine;

// Token: 0x020000E1 RID: 225
public class CFX_Demo_RandomDirectionTranslate : MonoBehaviour
{
	// Token: 0x06000B88 RID: 2952 RVA: 0x000326F4 File Offset: 0x000308F4
	private void Start()
	{
		this.dir = new Vector3(Random.Range(0f, 360f), Random.Range(0f, 360f), Random.Range(0f, 360f)).normalized;
		this.dir.Scale(this.axis);
		this.dir += this.baseDir;
	}

	// Token: 0x06000B89 RID: 2953 RVA: 0x0003276C File Offset: 0x0003096C
	private void Update()
	{
		base.transform.Translate(this.dir * this.speed * Time.deltaTime);
		if (this.gravity)
		{
			base.transform.Translate(Physics.gravity * Time.deltaTime);
		}
	}

	// Token: 0x040005E6 RID: 1510
	public float speed = 30f;

	// Token: 0x040005E7 RID: 1511
	public Vector3 baseDir = Vector3.zero;

	// Token: 0x040005E8 RID: 1512
	public Vector3 axis = Vector3.forward;

	// Token: 0x040005E9 RID: 1513
	public bool gravity;

	// Token: 0x040005EA RID: 1514
	private Vector3 dir;
}

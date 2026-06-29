using System;
using UnityEngine;

// Token: 0x0200013A RID: 314
[AddComponentMenu("Rendering/SetRenderQueue")]
public class SetRenderQueue : MonoBehaviour
{
	// Token: 0x06000F54 RID: 3924 RVA: 0x0003F938 File Offset: 0x0003DB38
	protected void Awake()
	{
		Material[] materials = base.GetComponent<Renderer>().materials;
		int num = 0;
		while (num < materials.Length && num < this.m_queues.Length)
		{
			materials[num].renderQueue = this.m_queues[num];
			num++;
		}
	}

	// Token: 0x04000947 RID: 2375
	[SerializeField]
	protected int[] m_queues = new int[]
	{
		3000
	};
}

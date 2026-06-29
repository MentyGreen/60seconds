using System;
using UnityEngine;

// Token: 0x0200011D RID: 285
public class Resizer : MonoBehaviour
{
	// Token: 0x06000E24 RID: 3620 RVA: 0x0003AD38 File Offset: 0x00038F38
	private void Start()
	{
		ResolutionHandler resolutionHandler = Object.FindObjectOfType<ResolutionHandler>();
		if (resolutionHandler != null)
		{
			if (!this._disableWidth && !this._disableHeight && !this._disableDepth)
			{
				base.transform.localScale *= resolutionHandler.ResizeRatio;
			}
			else
			{
				float x = this._disableWidth ? base.transform.localScale.x : (base.transform.localScale.x * resolutionHandler.ResizeRatio);
				float y = this._disableHeight ? base.transform.localScale.y : (base.transform.localScale.y * resolutionHandler.ResizeRatio);
				float z = this._disableDepth ? base.transform.localScale.z : (base.transform.localScale.z * resolutionHandler.ResizeRatio);
				base.transform.localScale = new Vector3(x, y, z);
			}
		}
		Object.Destroy(this);
	}

	// Token: 0x06000E25 RID: 3621 RVA: 0x0003AE3F File Offset: 0x0003903F
	private void Update()
	{
	}

	// Token: 0x04000863 RID: 2147
	[SerializeField]
	private bool _disableWidth;

	// Token: 0x04000864 RID: 2148
	[SerializeField]
	private bool _disableHeight;

	// Token: 0x04000865 RID: 2149
	[SerializeField]
	private bool _disableDepth;
}

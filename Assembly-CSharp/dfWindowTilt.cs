using System;
using UnityEngine;

// Token: 0x020000BC RID: 188
[AddComponentMenu("Daikon Forge/Examples/General/Window Tilt")]
public class dfWindowTilt : MonoBehaviour
{
	// Token: 0x06000A69 RID: 2665 RVA: 0x0002D98E File Offset: 0x0002BB8E
	private void Start()
	{
		this.control = base.GetComponent<dfControl>();
		if (this.control == null)
		{
			base.enabled = false;
		}
	}

	// Token: 0x06000A6A RID: 2666 RVA: 0x0002D9B4 File Offset: 0x0002BBB4
	private void Update()
	{
		Camera camera = this.control.GetCamera();
		Vector3 center = this.control.GetCenter();
		Vector3 vector = camera.WorldToViewportPoint(center);
		this.control.transform.localRotation = Quaternion.Euler(0f, (vector.x * 2f - 1f) * 20f, 0f);
	}

	// Token: 0x04000509 RID: 1289
	private dfControl control;
}

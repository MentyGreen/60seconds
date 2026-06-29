using System;
using UnityEngine;

// Token: 0x020000BB RID: 187
[AddComponentMenu("Daikon Forge/Examples/3D/Replace GUI Camera")]
public class dfReplaceGUICamera : MonoBehaviour
{
	// Token: 0x06000A67 RID: 2663 RVA: 0x0002D908 File Offset: 0x0002BB08
	public void OnEnable()
	{
		if (this.mainCamera == null)
		{
			this.mainCamera = Camera.main;
		}
		dfGUIManager component = base.GetComponent<dfGUIManager>();
		if (component == null)
		{
			Debug.LogError("This script should be attached to a dfGUIManager instance", this);
			base.enabled = false;
			return;
		}
		this.mainCamera.cullingMask |= 1 << base.gameObject.layer;
		component.OverrideCamera = true;
		component.RenderCamera = this.mainCamera;
	}

	// Token: 0x04000508 RID: 1288
	public Camera mainCamera;
}

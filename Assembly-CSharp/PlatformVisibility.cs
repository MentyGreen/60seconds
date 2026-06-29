using System;
using UnityEngine;

// Token: 0x020000AE RID: 174
[AddComponentMenu("Daikon Forge/Examples/General/Platform-based Visibility")]
public class PlatformVisibility : MonoBehaviour
{
	// Token: 0x06000A31 RID: 2609 RVA: 0x0002C808 File Offset: 0x0002AA08
	private void Start()
	{
		dfControl component = base.GetComponent<dfControl>();
		if (component == null)
		{
			return;
		}
		if (this.HideInEditor && Application.isEditor)
		{
			component.Hide();
		}
	}

	// Token: 0x040004D8 RID: 1240
	public bool HideOnWeb;

	// Token: 0x040004D9 RID: 1241
	public bool HideOnMobile;

	// Token: 0x040004DA RID: 1242
	public bool HideInEditor;
}

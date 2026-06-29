using System;
using UnityEngine;

// Token: 0x020000AF RID: 175
[AddComponentMenu("Daikon Forge/Examples/General/Pixel-Perfect Platform Settings")]
public class RuntimePixelPerfect : MonoBehaviour
{
	// Token: 0x06000A33 RID: 2611 RVA: 0x0002C848 File Offset: 0x0002AA48
	private void Awake()
	{
		dfGUIManager component = base.GetComponent<dfGUIManager>();
		if (component == null)
		{
			throw new MissingComponentException("dfGUIManager instance not found");
		}
		if (Application.isEditor)
		{
			component.PixelPerfectMode = this.PixelPerfectInEditor;
			return;
		}
		component.PixelPerfectMode = this.PixelPerfectAtRuntime;
	}

	// Token: 0x040004DB RID: 1243
	public bool PixelPerfectInEditor;

	// Token: 0x040004DC RID: 1244
	public bool PixelPerfectAtRuntime = true;
}

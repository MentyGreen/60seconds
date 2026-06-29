using System;
using System.Collections;
using UnityEngine;

// Token: 0x020000B0 RID: 176
[AddComponentMenu("Daikon Forge/Examples/General/Set Default Control")]
public class SetDefaultControl : MonoBehaviour
{
	// Token: 0x06000A35 RID: 2613 RVA: 0x0002C89F File Offset: 0x0002AA9F
	public void Awake()
	{
		this.thisControl = base.GetComponent<dfControl>();
	}

	// Token: 0x06000A36 RID: 2614 RVA: 0x0002C8AD File Offset: 0x0002AAAD
	public void OnEnable()
	{
		if (this.defaultControl != null && this.defaultControl.IsVisible)
		{
			this.defaultControl.Focus();
		}
	}

	// Token: 0x06000A37 RID: 2615 RVA: 0x0002C8D5 File Offset: 0x0002AAD5
	public IEnumerator OnIsVisibleChanged(dfControl control, bool value)
	{
		if (control == this.thisControl && value && this.defaultControl != null)
		{
			yield return new WaitForEndOfFrame();
			this.defaultControl.Focus();
		}
		yield break;
	}

	// Token: 0x040004DD RID: 1245
	public dfControl defaultControl;

	// Token: 0x040004DE RID: 1246
	private dfControl thisControl;
}

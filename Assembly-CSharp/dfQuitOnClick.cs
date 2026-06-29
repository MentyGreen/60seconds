using System;
using UnityEngine;

// Token: 0x020000B8 RID: 184
[AddComponentMenu("Daikon Forge/Examples/General/Quit On Click")]
public class dfQuitOnClick : MonoBehaviour
{
	// Token: 0x06000A59 RID: 2649 RVA: 0x0002D4C0 File Offset: 0x0002B6C0
	private void OnClick()
	{
		Application.Quit();
	}
}

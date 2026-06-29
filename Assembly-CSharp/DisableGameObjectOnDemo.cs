using System;
using RG.Parsecs.EventEditor;
using UnityEngine;

// Token: 0x02000140 RID: 320
public class DisableGameObjectOnDemo : MonoBehaviour
{
	// Token: 0x06000F92 RID: 3986 RVA: 0x00040D4F File Offset: 0x0003EF4F
	public void Awake()
	{
		if (this._isDemoVariable != null && this._isDemoVariable.Value)
		{
			base.gameObject.SetActive(false);
		}
	}

	// Token: 0x04000998 RID: 2456
	[SerializeField]
	private GlobalBoolVariable _isDemoVariable;
}

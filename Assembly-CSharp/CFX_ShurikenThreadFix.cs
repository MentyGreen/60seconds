using System;
using System.Collections;
using UnityEngine;

// Token: 0x020000E7 RID: 231
public class CFX_ShurikenThreadFix : MonoBehaviour
{
	// Token: 0x06000B99 RID: 2969 RVA: 0x000329B0 File Offset: 0x00030BB0
	private void OnEnable()
	{
		this.systems = base.GetComponentsInChildren<ParticleSystem>();
		ParticleSystem[] array = this.systems;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].enableEmission = false;
		}
		base.StartCoroutine("WaitFrame");
	}

	// Token: 0x06000B9A RID: 2970 RVA: 0x000329F3 File Offset: 0x00030BF3
	private IEnumerator WaitFrame()
	{
		yield return null;
		foreach (ParticleSystem particleSystem in this.systems)
		{
			particleSystem.enableEmission = true;
			particleSystem.Play(true);
		}
		yield break;
	}

	// Token: 0x040005F8 RID: 1528
	private ParticleSystem[] systems;
}

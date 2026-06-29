using System;
using System.Collections;
using UnityEngine;

// Token: 0x020000E3 RID: 227
[RequireComponent(typeof(ParticleSystem))]
public class CFX_AutoDestructShuriken : MonoBehaviour
{
	// Token: 0x06000B8E RID: 2958 RVA: 0x00032835 File Offset: 0x00030A35
	private void OnEnable()
	{
		base.StartCoroutine("CheckIfAlive");
	}

	// Token: 0x06000B8F RID: 2959 RVA: 0x00032843 File Offset: 0x00030A43
	private IEnumerator CheckIfAlive()
	{
		ParticleSystem ps = base.GetComponent<ParticleSystem>();
		while (ps != null)
		{
			yield return new WaitForSeconds(0.5f);
			if (!ps.IsAlive(true))
			{
				if (this.OnlyDeactivate)
				{
					base.gameObject.SetActive(false);
					break;
				}
				Object.Destroy(base.gameObject);
				break;
			}
		}
		yield break;
	}

	// Token: 0x040005EE RID: 1518
	public bool OnlyDeactivate;
}

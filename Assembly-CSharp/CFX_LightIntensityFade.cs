using System;
using UnityEngine;

// Token: 0x020000E6 RID: 230
[RequireComponent(typeof(Light))]
public class CFX_LightIntensityFade : MonoBehaviour
{
	// Token: 0x06000B95 RID: 2965 RVA: 0x000328AE File Offset: 0x00030AAE
	private void Start()
	{
		this.baseIntensity = base.GetComponent<Light>().intensity;
	}

	// Token: 0x06000B96 RID: 2966 RVA: 0x000328C1 File Offset: 0x00030AC1
	private void OnEnable()
	{
		this.p_lifetime = 0f;
		this.p_delay = this.delay;
		if (this.delay > 0f)
		{
			base.GetComponent<Light>().enabled = false;
		}
	}

	// Token: 0x06000B97 RID: 2967 RVA: 0x000328F4 File Offset: 0x00030AF4
	private void Update()
	{
		if (this.p_delay > 0f)
		{
			this.p_delay -= Time.deltaTime;
			if (this.p_delay <= 0f)
			{
				base.GetComponent<Light>().enabled = true;
			}
			return;
		}
		if (this.p_lifetime / this.duration < 1f)
		{
			base.GetComponent<Light>().intensity = Mathf.Lerp(this.baseIntensity, this.finalIntensity, this.p_lifetime / this.duration);
			this.p_lifetime += Time.deltaTime;
			return;
		}
		if (this.autodestruct)
		{
			Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x040005F1 RID: 1521
	public float duration = 1f;

	// Token: 0x040005F2 RID: 1522
	public float delay;

	// Token: 0x040005F3 RID: 1523
	public float finalIntensity;

	// Token: 0x040005F4 RID: 1524
	private float baseIntensity;

	// Token: 0x040005F5 RID: 1525
	public bool autodestruct;

	// Token: 0x040005F6 RID: 1526
	private float p_lifetime;

	// Token: 0x040005F7 RID: 1527
	private float p_delay;
}

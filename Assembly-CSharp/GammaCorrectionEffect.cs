using System;
using UnityEngine;
using UnityStandardAssets.ImageEffects;

// Token: 0x02000132 RID: 306
[ExecuteInEditMode]
[AddComponentMenu("Image Effects/Gamma Correction")]
public class GammaCorrectionEffect : ImageEffectBase
{
	// Token: 0x06000F01 RID: 3841 RVA: 0x0003E655 File Offset: 0x0003C855
	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		base.material.SetFloat("_Gamma", 1f / this.gamma);
		Graphics.Blit(source, destination, base.material);
	}

	// Token: 0x04000915 RID: 2325
	public float gamma;
}

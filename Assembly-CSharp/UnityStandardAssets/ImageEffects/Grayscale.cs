using System;
using UnityEngine;

namespace UnityStandardAssets.ImageEffects
{
	// Token: 0x02000163 RID: 355
	[ExecuteInEditMode]
	[AddComponentMenu("Image Effects/Color Adjustments/Grayscale")]
	public class Grayscale : ImageEffectBase
	{
		// Token: 0x06001020 RID: 4128 RVA: 0x0004329B File Offset: 0x0004149B
		private void OnRenderImage(RenderTexture source, RenderTexture destination)
		{
			base.material.SetTexture("_RampTex", this.textureRamp);
			base.material.SetFloat("_RampOffset", this.rampOffset);
			Graphics.Blit(source, destination, base.material);
		}

		// Token: 0x04000A3B RID: 2619
		public Texture textureRamp;

		// Token: 0x04000A3C RID: 2620
		public float rampOffset;
	}
}

using System;
using UnityEngine;

namespace UnityStandardAssets.ImageEffects
{
	// Token: 0x02000161 RID: 353
	[ExecuteInEditMode]
	[RequireComponent(typeof(Camera))]
	[AddComponentMenu("Image Effects/Color Adjustments/Contrast Enhance (Unsharp Mask)")]
	internal class ContrastEnhance : PostEffectsBase
	{
		// Token: 0x06001013 RID: 4115 RVA: 0x00042D0C File Offset: 0x00040F0C
		public override bool CheckResources()
		{
			base.CheckSupport(false);
			this.contrastCompositeMaterial = base.CheckShaderAndCreateMaterial(this.contrastCompositeShader, this.contrastCompositeMaterial);
			this.separableBlurMaterial = base.CheckShaderAndCreateMaterial(this.separableBlurShader, this.separableBlurMaterial);
			if (!this.isSupported)
			{
				base.ReportAutoDisable();
			}
			return this.isSupported;
		}

		// Token: 0x06001014 RID: 4116 RVA: 0x00042D68 File Offset: 0x00040F68
		private void OnRenderImage(RenderTexture source, RenderTexture destination)
		{
			if (!this.CheckResources())
			{
				Graphics.Blit(source, destination);
				return;
			}
			int width = source.width;
			int height = source.height;
			RenderTexture temporary = RenderTexture.GetTemporary(width / 2, height / 2, 0);
			Graphics.Blit(source, temporary);
			RenderTexture temporary2 = RenderTexture.GetTemporary(width / 4, height / 4, 0);
			Graphics.Blit(temporary, temporary2);
			RenderTexture.ReleaseTemporary(temporary);
			this.separableBlurMaterial.SetVector("offsets", new Vector4(0f, this.blurSpread * 1f / (float)temporary2.height, 0f, 0f));
			RenderTexture temporary3 = RenderTexture.GetTemporary(width / 4, height / 4, 0);
			Graphics.Blit(temporary2, temporary3, this.separableBlurMaterial);
			RenderTexture.ReleaseTemporary(temporary2);
			this.separableBlurMaterial.SetVector("offsets", new Vector4(this.blurSpread * 1f / (float)temporary2.width, 0f, 0f, 0f));
			temporary2 = RenderTexture.GetTemporary(width / 4, height / 4, 0);
			Graphics.Blit(temporary3, temporary2, this.separableBlurMaterial);
			RenderTexture.ReleaseTemporary(temporary3);
			this.contrastCompositeMaterial.SetTexture("_MainTexBlurred", temporary2);
			this.contrastCompositeMaterial.SetFloat("intensity", this.intensity);
			this.contrastCompositeMaterial.SetFloat("threshhold", this.threshold);
			Graphics.Blit(source, destination, this.contrastCompositeMaterial);
			RenderTexture.ReleaseTemporary(temporary2);
		}

		// Token: 0x04000A27 RID: 2599
		public float intensity = 0.5f;

		// Token: 0x04000A28 RID: 2600
		public float threshold;

		// Token: 0x04000A29 RID: 2601
		private Material separableBlurMaterial;

		// Token: 0x04000A2A RID: 2602
		private Material contrastCompositeMaterial;

		// Token: 0x04000A2B RID: 2603
		public float blurSpread = 1f;

		// Token: 0x04000A2C RID: 2604
		public Shader separableBlurShader;

		// Token: 0x04000A2D RID: 2605
		public Shader contrastCompositeShader;
	}
}

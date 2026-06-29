using System;
using UnityEngine;

namespace UnityStandardAssets.ImageEffects
{
	// Token: 0x0200016B RID: 363
	[ExecuteInEditMode]
	[RequireComponent(typeof(Camera))]
	[AddComponentMenu("Image Effects/Other/Screen Overlay")]
	public class ScreenOverlay : PostEffectsBase
	{
		// Token: 0x0600104E RID: 4174 RVA: 0x00044BA9 File Offset: 0x00042DA9
		public override bool CheckResources()
		{
			base.CheckSupport(false);
			this.overlayMaterial = base.CheckShaderAndCreateMaterial(this.overlayShader, this.overlayMaterial);
			if (!this.isSupported)
			{
				base.ReportAutoDisable();
			}
			return this.isSupported;
		}

		// Token: 0x0600104F RID: 4175 RVA: 0x00044BE0 File Offset: 0x00042DE0
		private void OnRenderImage(RenderTexture source, RenderTexture destination)
		{
			if (!this.CheckResources())
			{
				Graphics.Blit(source, destination);
				return;
			}
			Vector4 value = new Vector4(1f, 0f, 0f, 1f);
			this.overlayMaterial.SetVector("_UV_Transform", value);
			this.overlayMaterial.SetFloat("_Intensity", this.intensity);
			this.overlayMaterial.SetTexture("_Overlay", this.texture);
			Graphics.Blit(source, destination, this.overlayMaterial, (int)this.blendMode);
		}

		// Token: 0x04000A68 RID: 2664
		public ScreenOverlay.OverlayBlendMode blendMode = ScreenOverlay.OverlayBlendMode.Overlay;

		// Token: 0x04000A69 RID: 2665
		public float intensity = 1f;

		// Token: 0x04000A6A RID: 2666
		public Texture2D texture;

		// Token: 0x04000A6B RID: 2667
		public Shader overlayShader;

		// Token: 0x04000A6C RID: 2668
		private Material overlayMaterial;

		// Token: 0x020003D2 RID: 978
		public enum OverlayBlendMode
		{
			// Token: 0x040017D3 RID: 6099
			Additive,
			// Token: 0x040017D4 RID: 6100
			ScreenBlend,
			// Token: 0x040017D5 RID: 6101
			Multiply,
			// Token: 0x040017D6 RID: 6102
			Overlay,
			// Token: 0x040017D7 RID: 6103
			AlphaBlend
		}
	}
}

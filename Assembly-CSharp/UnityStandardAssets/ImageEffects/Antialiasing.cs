using System;
using UnityEngine;

namespace UnityStandardAssets.ImageEffects
{
	// Token: 0x0200015D RID: 349
	[ExecuteInEditMode]
	[RequireComponent(typeof(Camera))]
	[AddComponentMenu("Image Effects/Other/Antialiasing")]
	public class Antialiasing : PostEffectsBase
	{
		// Token: 0x06000FFE RID: 4094 RVA: 0x00041F28 File Offset: 0x00040128
		public Material CurrentAAMaterial()
		{
			Material result;
			switch (this.mode)
			{
			case AAMode.FXAA2:
				result = this.materialFXAAII;
				break;
			case AAMode.FXAA3Console:
				result = this.materialFXAAIII;
				break;
			case AAMode.FXAA1PresetA:
				result = this.materialFXAAPreset2;
				break;
			case AAMode.FXAA1PresetB:
				result = this.materialFXAAPreset3;
				break;
			case AAMode.NFAA:
				result = this.nfaa;
				break;
			case AAMode.SSAA:
				result = this.ssaa;
				break;
			case AAMode.DLAA:
				result = this.dlaa;
				break;
			default:
				result = null;
				break;
			}
			return result;
		}

		// Token: 0x06000FFF RID: 4095 RVA: 0x00041FA4 File Offset: 0x000401A4
		public override bool CheckResources()
		{
			base.CheckSupport(false);
			this.materialFXAAPreset2 = base.CreateMaterial(this.shaderFXAAPreset2, this.materialFXAAPreset2);
			this.materialFXAAPreset3 = base.CreateMaterial(this.shaderFXAAPreset3, this.materialFXAAPreset3);
			this.materialFXAAII = base.CreateMaterial(this.shaderFXAAII, this.materialFXAAII);
			this.materialFXAAIII = base.CreateMaterial(this.shaderFXAAIII, this.materialFXAAIII);
			this.nfaa = base.CreateMaterial(this.nfaaShader, this.nfaa);
			this.ssaa = base.CreateMaterial(this.ssaaShader, this.ssaa);
			this.dlaa = base.CreateMaterial(this.dlaaShader, this.dlaa);
			if (!this.ssaaShader.isSupported)
			{
				base.NotSupported();
				base.ReportAutoDisable();
			}
			return this.isSupported;
		}

		// Token: 0x06001000 RID: 4096 RVA: 0x00042080 File Offset: 0x00040280
		public void OnRenderImage(RenderTexture source, RenderTexture destination)
		{
			if (!this.CheckResources())
			{
				Graphics.Blit(source, destination);
				return;
			}
			if (this.mode == AAMode.FXAA3Console && this.materialFXAAIII != null)
			{
				this.materialFXAAIII.SetFloat("_EdgeThresholdMin", this.edgeThresholdMin);
				this.materialFXAAIII.SetFloat("_EdgeThreshold", this.edgeThreshold);
				this.materialFXAAIII.SetFloat("_EdgeSharpness", this.edgeSharpness);
				Graphics.Blit(source, destination, this.materialFXAAIII);
				return;
			}
			if (this.mode == AAMode.FXAA1PresetB && this.materialFXAAPreset3 != null)
			{
				Graphics.Blit(source, destination, this.materialFXAAPreset3);
				return;
			}
			if (this.mode == AAMode.FXAA1PresetA && this.materialFXAAPreset2 != null)
			{
				source.anisoLevel = 4;
				Graphics.Blit(source, destination, this.materialFXAAPreset2);
				source.anisoLevel = 0;
				return;
			}
			if (this.mode == AAMode.FXAA2 && this.materialFXAAII != null)
			{
				Graphics.Blit(source, destination, this.materialFXAAII);
				return;
			}
			if (this.mode == AAMode.SSAA && this.ssaa != null)
			{
				Graphics.Blit(source, destination, this.ssaa);
				return;
			}
			if (this.mode == AAMode.DLAA && this.dlaa != null)
			{
				source.anisoLevel = 0;
				RenderTexture temporary = RenderTexture.GetTemporary(source.width, source.height);
				Graphics.Blit(source, temporary, this.dlaa, 0);
				Graphics.Blit(temporary, destination, this.dlaa, this.dlaaSharp ? 2 : 1);
				RenderTexture.ReleaseTemporary(temporary);
				return;
			}
			if (this.mode == AAMode.NFAA && this.nfaa != null)
			{
				source.anisoLevel = 0;
				this.nfaa.SetFloat("_OffsetScale", this.offsetScale);
				this.nfaa.SetFloat("_BlurRadius", this.blurRadius);
				Graphics.Blit(source, destination, this.nfaa, this.showGeneratedNormals ? 1 : 0);
				return;
			}
			Graphics.Blit(source, destination);
		}

		// Token: 0x040009F4 RID: 2548
		public AAMode mode = AAMode.FXAA3Console;

		// Token: 0x040009F5 RID: 2549
		public bool showGeneratedNormals;

		// Token: 0x040009F6 RID: 2550
		public float offsetScale = 0.2f;

		// Token: 0x040009F7 RID: 2551
		public float blurRadius = 18f;

		// Token: 0x040009F8 RID: 2552
		public float edgeThresholdMin = 0.05f;

		// Token: 0x040009F9 RID: 2553
		public float edgeThreshold = 0.2f;

		// Token: 0x040009FA RID: 2554
		public float edgeSharpness = 4f;

		// Token: 0x040009FB RID: 2555
		public bool dlaaSharp;

		// Token: 0x040009FC RID: 2556
		public Shader ssaaShader;

		// Token: 0x040009FD RID: 2557
		private Material ssaa;

		// Token: 0x040009FE RID: 2558
		public Shader dlaaShader;

		// Token: 0x040009FF RID: 2559
		private Material dlaa;

		// Token: 0x04000A00 RID: 2560
		public Shader nfaaShader;

		// Token: 0x04000A01 RID: 2561
		private Material nfaa;

		// Token: 0x04000A02 RID: 2562
		public Shader shaderFXAAPreset2;

		// Token: 0x04000A03 RID: 2563
		private Material materialFXAAPreset2;

		// Token: 0x04000A04 RID: 2564
		public Shader shaderFXAAPreset3;

		// Token: 0x04000A05 RID: 2565
		private Material materialFXAAPreset3;

		// Token: 0x04000A06 RID: 2566
		public Shader shaderFXAAII;

		// Token: 0x04000A07 RID: 2567
		private Material materialFXAAII;

		// Token: 0x04000A08 RID: 2568
		public Shader shaderFXAAIII;

		// Token: 0x04000A09 RID: 2569
		private Material materialFXAAIII;
	}
}

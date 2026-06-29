using System;
using UnityEngine;

namespace UnityStandardAssets.ImageEffects
{
	// Token: 0x0200016F RID: 367
	[ExecuteInEditMode]
	[RequireComponent(typeof(Camera))]
	[AddComponentMenu("Image Effects/Color Adjustments/Tonemapping")]
	public class Tonemapping : PostEffectsBase
	{
		// Token: 0x0600105F RID: 4191 RVA: 0x000453D4 File Offset: 0x000435D4
		public override bool CheckResources()
		{
			base.CheckSupport(false, true);
			this.tonemapMaterial = base.CheckShaderAndCreateMaterial(this.tonemapper, this.tonemapMaterial);
			if (!this.curveTex && this.type == Tonemapping.TonemapperType.UserCurve)
			{
				this.curveTex = new Texture2D(256, 1, TextureFormat.ARGB32, false, true);
				this.curveTex.filterMode = FilterMode.Bilinear;
				this.curveTex.wrapMode = TextureWrapMode.Clamp;
				this.curveTex.hideFlags = HideFlags.DontSave;
			}
			if (!this.isSupported)
			{
				base.ReportAutoDisable();
			}
			return this.isSupported;
		}

		// Token: 0x06001060 RID: 4192 RVA: 0x00045468 File Offset: 0x00043668
		public float UpdateCurve()
		{
			float num = 1f;
			if (this.remapCurve.keys.Length < 1)
			{
				this.remapCurve = new AnimationCurve(new Keyframe[]
				{
					new Keyframe(0f, 0f),
					new Keyframe(2f, 1f)
				});
			}
			if (this.remapCurve != null)
			{
				if (this.remapCurve.length > 0)
				{
					num = this.remapCurve[this.remapCurve.length - 1].time;
				}
				for (float num2 = 0f; num2 <= 1f; num2 += 0.003921569f)
				{
					float num3 = this.remapCurve.Evaluate(num2 * 1f * num);
					this.curveTex.SetPixel((int)Mathf.Floor(num2 * 255f), 0, new Color(num3, num3, num3));
				}
				this.curveTex.Apply();
			}
			return 1f / num;
		}

		// Token: 0x06001061 RID: 4193 RVA: 0x00045564 File Offset: 0x00043764
		private void OnDisable()
		{
			if (this.rt)
			{
				Object.DestroyImmediate(this.rt);
				this.rt = null;
			}
			if (this.tonemapMaterial)
			{
				Object.DestroyImmediate(this.tonemapMaterial);
				this.tonemapMaterial = null;
			}
			if (this.curveTex)
			{
				Object.DestroyImmediate(this.curveTex);
				this.curveTex = null;
			}
		}

		// Token: 0x06001062 RID: 4194 RVA: 0x000455D0 File Offset: 0x000437D0
		private bool CreateInternalRenderTexture()
		{
			if (this.rt)
			{
				return false;
			}
			this.rtFormat = (SystemInfo.SupportsRenderTextureFormat(RenderTextureFormat.RGHalf) ? RenderTextureFormat.RGHalf : RenderTextureFormat.ARGBHalf);
			this.rt = new RenderTexture(1, 1, 0, this.rtFormat);
			this.rt.hideFlags = HideFlags.DontSave;
			return true;
		}

		// Token: 0x06001063 RID: 4195 RVA: 0x00045624 File Offset: 0x00043824
		[ImageEffectTransformsToLDR]
		private void OnRenderImage(RenderTexture source, RenderTexture destination)
		{
			if (!this.CheckResources())
			{
				Graphics.Blit(source, destination);
				return;
			}
			this.exposureAdjustment = ((this.exposureAdjustment < 0.001f) ? 0.001f : this.exposureAdjustment);
			if (this.type == Tonemapping.TonemapperType.UserCurve)
			{
				float value = this.UpdateCurve();
				this.tonemapMaterial.SetFloat("_RangeScale", value);
				this.tonemapMaterial.SetTexture("_Curve", this.curveTex);
				Graphics.Blit(source, destination, this.tonemapMaterial, 4);
				return;
			}
			if (this.type == Tonemapping.TonemapperType.SimpleReinhard)
			{
				this.tonemapMaterial.SetFloat("_ExposureAdjustment", this.exposureAdjustment);
				Graphics.Blit(source, destination, this.tonemapMaterial, 6);
				return;
			}
			if (this.type == Tonemapping.TonemapperType.Hable)
			{
				this.tonemapMaterial.SetFloat("_ExposureAdjustment", this.exposureAdjustment);
				Graphics.Blit(source, destination, this.tonemapMaterial, 5);
				return;
			}
			if (this.type == Tonemapping.TonemapperType.Photographic)
			{
				this.tonemapMaterial.SetFloat("_ExposureAdjustment", this.exposureAdjustment);
				Graphics.Blit(source, destination, this.tonemapMaterial, 8);
				return;
			}
			if (this.type == Tonemapping.TonemapperType.OptimizedHejiDawson)
			{
				this.tonemapMaterial.SetFloat("_ExposureAdjustment", 0.5f * this.exposureAdjustment);
				Graphics.Blit(source, destination, this.tonemapMaterial, 7);
				return;
			}
			bool flag = this.CreateInternalRenderTexture();
			RenderTexture temporary = RenderTexture.GetTemporary((int)this.adaptiveTextureSize, (int)this.adaptiveTextureSize, 0, this.rtFormat);
			Graphics.Blit(source, temporary);
			int num = (int)Mathf.Log((float)temporary.width * 1f, 2f);
			int num2 = 2;
			RenderTexture[] array = new RenderTexture[num];
			for (int i = 0; i < num; i++)
			{
				array[i] = RenderTexture.GetTemporary(temporary.width / num2, temporary.width / num2, 0, this.rtFormat);
				num2 *= 2;
			}
			RenderTexture source2 = array[num - 1];
			Graphics.Blit(temporary, array[0], this.tonemapMaterial, 1);
			if (this.type == Tonemapping.TonemapperType.AdaptiveReinhardAutoWhite)
			{
				for (int j = 0; j < num - 1; j++)
				{
					Graphics.Blit(array[j], array[j + 1], this.tonemapMaterial, 9);
					source2 = array[j + 1];
				}
			}
			else if (this.type == Tonemapping.TonemapperType.AdaptiveReinhard)
			{
				for (int k = 0; k < num - 1; k++)
				{
					Graphics.Blit(array[k], array[k + 1]);
					source2 = array[k + 1];
				}
			}
			this.adaptionSpeed = ((this.adaptionSpeed < 0.001f) ? 0.001f : this.adaptionSpeed);
			this.tonemapMaterial.SetFloat("_AdaptionSpeed", this.adaptionSpeed);
			this.rt.MarkRestoreExpected();
			Graphics.Blit(source2, this.rt, this.tonemapMaterial, flag ? 3 : 2);
			this.middleGrey = ((this.middleGrey < 0.001f) ? 0.001f : this.middleGrey);
			this.tonemapMaterial.SetVector("_HdrParams", new Vector4(this.middleGrey, this.middleGrey, this.middleGrey, this.white * this.white));
			this.tonemapMaterial.SetTexture("_SmallTex", this.rt);
			if (this.type == Tonemapping.TonemapperType.AdaptiveReinhard)
			{
				Graphics.Blit(source, destination, this.tonemapMaterial, 0);
			}
			else if (this.type == Tonemapping.TonemapperType.AdaptiveReinhardAutoWhite)
			{
				Graphics.Blit(source, destination, this.tonemapMaterial, 10);
			}
			else
			{
				Debug.LogError("No valid adaptive tonemapper type found!");
				Graphics.Blit(source, destination);
			}
			for (int l = 0; l < num; l++)
			{
				RenderTexture.ReleaseTemporary(array[l]);
			}
			RenderTexture.ReleaseTemporary(temporary);
		}

		// Token: 0x04000A80 RID: 2688
		public Tonemapping.TonemapperType type = Tonemapping.TonemapperType.Photographic;

		// Token: 0x04000A81 RID: 2689
		public Tonemapping.AdaptiveTexSize adaptiveTextureSize = Tonemapping.AdaptiveTexSize.Square256;

		// Token: 0x04000A82 RID: 2690
		public AnimationCurve remapCurve;

		// Token: 0x04000A83 RID: 2691
		private Texture2D curveTex;

		// Token: 0x04000A84 RID: 2692
		public float exposureAdjustment = 1.5f;

		// Token: 0x04000A85 RID: 2693
		public float middleGrey = 0.4f;

		// Token: 0x04000A86 RID: 2694
		public float white = 2f;

		// Token: 0x04000A87 RID: 2695
		public float adaptionSpeed = 1.5f;

		// Token: 0x04000A88 RID: 2696
		public Shader tonemapper;

		// Token: 0x04000A89 RID: 2697
		public bool validRenderTextureFormat = true;

		// Token: 0x04000A8A RID: 2698
		private Material tonemapMaterial;

		// Token: 0x04000A8B RID: 2699
		private RenderTexture rt;

		// Token: 0x04000A8C RID: 2700
		private RenderTextureFormat rtFormat = RenderTextureFormat.ARGBHalf;

		// Token: 0x020003D4 RID: 980
		public enum TonemapperType
		{
			// Token: 0x040017DD RID: 6109
			SimpleReinhard,
			// Token: 0x040017DE RID: 6110
			UserCurve,
			// Token: 0x040017DF RID: 6111
			Hable,
			// Token: 0x040017E0 RID: 6112
			Photographic,
			// Token: 0x040017E1 RID: 6113
			OptimizedHejiDawson,
			// Token: 0x040017E2 RID: 6114
			AdaptiveReinhard,
			// Token: 0x040017E3 RID: 6115
			AdaptiveReinhardAutoWhite
		}

		// Token: 0x020003D5 RID: 981
		public enum AdaptiveTexSize
		{
			// Token: 0x040017E5 RID: 6117
			Square16 = 16,
			// Token: 0x040017E6 RID: 6118
			Square32 = 32,
			// Token: 0x040017E7 RID: 6119
			Square64 = 64,
			// Token: 0x040017E8 RID: 6120
			Square128 = 128,
			// Token: 0x040017E9 RID: 6121
			Square256 = 256,
			// Token: 0x040017EA RID: 6122
			Square512 = 512,
			// Token: 0x040017EB RID: 6123
			Square1024 = 1024
		}
	}
}

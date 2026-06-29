using System;
using UnityEngine;

namespace UnityStandardAssets.ImageEffects
{
	// Token: 0x0200016D RID: 365
	[ExecuteInEditMode]
	[RequireComponent(typeof(Camera))]
	[AddComponentMenu("Image Effects/Rendering/Screen Space Ambient Occlusion")]
	public class ScreenSpaceAmbientOcclusion : MonoBehaviour
	{
		// Token: 0x06001055 RID: 4181 RVA: 0x00044F69 File Offset: 0x00043169
		private static Material CreateMaterial(Shader shader)
		{
			if (!shader)
			{
				return null;
			}
			return new Material(shader)
			{
				hideFlags = HideFlags.HideAndDontSave
			};
		}

		// Token: 0x06001056 RID: 4182 RVA: 0x00044F83 File Offset: 0x00043183
		private static void DestroyMaterial(Material mat)
		{
			if (mat)
			{
				Object.DestroyImmediate(mat);
				mat = null;
			}
		}

		// Token: 0x06001057 RID: 4183 RVA: 0x00044F96 File Offset: 0x00043196
		private void OnDisable()
		{
			ScreenSpaceAmbientOcclusion.DestroyMaterial(this.m_SSAOMaterial);
		}

		// Token: 0x06001058 RID: 4184 RVA: 0x00044FA4 File Offset: 0x000431A4
		private void Start()
		{
			if (!SystemInfo.supportsImageEffects || !SystemInfo.SupportsRenderTextureFormat(RenderTextureFormat.Depth))
			{
				this.m_Supported = false;
				base.enabled = false;
				return;
			}
			this.CreateMaterials();
			if (!this.m_SSAOMaterial || this.m_SSAOMaterial.passCount != 5)
			{
				this.m_Supported = false;
				base.enabled = false;
				return;
			}
			this.m_Supported = true;
		}

		// Token: 0x06001059 RID: 4185 RVA: 0x00045006 File Offset: 0x00043206
		private void OnEnable()
		{
			base.GetComponent<Camera>().depthTextureMode |= DepthTextureMode.DepthNormals;
		}

		// Token: 0x0600105A RID: 4186 RVA: 0x0004501C File Offset: 0x0004321C
		private void CreateMaterials()
		{
			if (!this.m_SSAOMaterial && this.m_SSAOShader.isSupported)
			{
				this.m_SSAOMaterial = ScreenSpaceAmbientOcclusion.CreateMaterial(this.m_SSAOShader);
				this.m_SSAOMaterial.SetTexture("_RandomTexture", this.m_RandomTexture);
			}
		}

		// Token: 0x0600105B RID: 4187 RVA: 0x0004506C File Offset: 0x0004326C
		[ImageEffectOpaque]
		private void OnRenderImage(RenderTexture source, RenderTexture destination)
		{
			if (!this.m_Supported || !this.m_SSAOShader.isSupported)
			{
				base.enabled = false;
				return;
			}
			this.CreateMaterials();
			this.m_Downsampling = Mathf.Clamp(this.m_Downsampling, 1, 6);
			this.m_Radius = Mathf.Clamp(this.m_Radius, 0.05f, 1f);
			this.m_MinZ = Mathf.Clamp(this.m_MinZ, 1E-05f, 0.5f);
			this.m_OcclusionIntensity = Mathf.Clamp(this.m_OcclusionIntensity, 0.5f, 4f);
			this.m_OcclusionAttenuation = Mathf.Clamp(this.m_OcclusionAttenuation, 0.2f, 2f);
			this.m_Blur = Mathf.Clamp(this.m_Blur, 0, 4);
			RenderTexture renderTexture = RenderTexture.GetTemporary(source.width / this.m_Downsampling, source.height / this.m_Downsampling, 0);
			float fieldOfView = base.GetComponent<Camera>().fieldOfView;
			float farClipPlane = base.GetComponent<Camera>().farClipPlane;
			float num = Mathf.Tan(fieldOfView * 0.017453292f * 0.5f) * farClipPlane;
			float x = num * base.GetComponent<Camera>().aspect;
			this.m_SSAOMaterial.SetVector("_FarCorner", new Vector3(x, num, farClipPlane));
			int num2;
			int num3;
			if (this.m_RandomTexture)
			{
				num2 = this.m_RandomTexture.width;
				num3 = this.m_RandomTexture.height;
			}
			else
			{
				num2 = 1;
				num3 = 1;
			}
			this.m_SSAOMaterial.SetVector("_NoiseScale", new Vector3((float)renderTexture.width / (float)num2, (float)renderTexture.height / (float)num3, 0f));
			this.m_SSAOMaterial.SetVector("_Params", new Vector4(this.m_Radius, this.m_MinZ, 1f / this.m_OcclusionAttenuation, this.m_OcclusionIntensity));
			bool flag = this.m_Blur > 0;
			Graphics.Blit(flag ? null : source, renderTexture, this.m_SSAOMaterial, (int)this.m_SampleCount);
			if (flag)
			{
				RenderTexture temporary = RenderTexture.GetTemporary(source.width, source.height, 0);
				this.m_SSAOMaterial.SetVector("_TexelOffsetScale", new Vector4((float)this.m_Blur / (float)source.width, 0f, 0f, 0f));
				this.m_SSAOMaterial.SetTexture("_SSAO", renderTexture);
				Graphics.Blit(null, temporary, this.m_SSAOMaterial, 3);
				RenderTexture.ReleaseTemporary(renderTexture);
				RenderTexture temporary2 = RenderTexture.GetTemporary(source.width, source.height, 0);
				this.m_SSAOMaterial.SetVector("_TexelOffsetScale", new Vector4(0f, (float)this.m_Blur / (float)source.height, 0f, 0f));
				this.m_SSAOMaterial.SetTexture("_SSAO", temporary);
				Graphics.Blit(source, temporary2, this.m_SSAOMaterial, 3);
				RenderTexture.ReleaseTemporary(temporary);
				renderTexture = temporary2;
			}
			this.m_SSAOMaterial.SetTexture("_SSAO", renderTexture);
			Graphics.Blit(source, destination, this.m_SSAOMaterial, 4);
			RenderTexture.ReleaseTemporary(renderTexture);
		}

		// Token: 0x04000A75 RID: 2677
		public float m_Radius = 0.4f;

		// Token: 0x04000A76 RID: 2678
		public ScreenSpaceAmbientOcclusion.SSAOSamples m_SampleCount = ScreenSpaceAmbientOcclusion.SSAOSamples.Medium;

		// Token: 0x04000A77 RID: 2679
		public float m_OcclusionIntensity = 1.5f;

		// Token: 0x04000A78 RID: 2680
		public int m_Blur = 2;

		// Token: 0x04000A79 RID: 2681
		public int m_Downsampling = 2;

		// Token: 0x04000A7A RID: 2682
		public float m_OcclusionAttenuation = 1f;

		// Token: 0x04000A7B RID: 2683
		public float m_MinZ = 0.01f;

		// Token: 0x04000A7C RID: 2684
		public Shader m_SSAOShader;

		// Token: 0x04000A7D RID: 2685
		private Material m_SSAOMaterial;

		// Token: 0x04000A7E RID: 2686
		public Texture2D m_RandomTexture;

		// Token: 0x04000A7F RID: 2687
		private bool m_Supported;

		// Token: 0x020003D3 RID: 979
		public enum SSAOSamples
		{
			// Token: 0x040017D9 RID: 6105
			Low,
			// Token: 0x040017DA RID: 6106
			Medium,
			// Token: 0x040017DB RID: 6107
			High
		}
	}
}

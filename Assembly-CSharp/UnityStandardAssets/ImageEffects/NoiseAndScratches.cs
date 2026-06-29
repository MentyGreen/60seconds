using System;
using UnityEngine;

namespace UnityStandardAssets.ImageEffects
{
	// Token: 0x02000167 RID: 359
	[ExecuteInEditMode]
	[RequireComponent(typeof(Camera))]
	[AddComponentMenu("Image Effects/Noise/Noise and Scratches")]
	public class NoiseAndScratches : MonoBehaviour
	{
		// Token: 0x0600102F RID: 4143 RVA: 0x00043AA4 File Offset: 0x00041CA4
		protected void Start()
		{
			if (!SystemInfo.supportsImageEffects)
			{
				base.enabled = false;
				return;
			}
			if (this.shaderRGB == null || this.shaderYUV == null)
			{
				Debug.Log("Noise shaders are not set up! Disabling noise effect.");
				base.enabled = false;
				return;
			}
			if (!this.shaderRGB.isSupported)
			{
				base.enabled = false;
				return;
			}
			if (!this.shaderYUV.isSupported)
			{
				this.rgbFallback = true;
			}
		}

		// Token: 0x17000346 RID: 838
		// (get) Token: 0x06001030 RID: 4144 RVA: 0x00043B18 File Offset: 0x00041D18
		protected Material material
		{
			get
			{
				if (this.m_MaterialRGB == null)
				{
					this.m_MaterialRGB = new Material(this.shaderRGB);
					this.m_MaterialRGB.hideFlags = HideFlags.HideAndDontSave;
				}
				if (this.m_MaterialYUV == null && !this.rgbFallback)
				{
					this.m_MaterialYUV = new Material(this.shaderYUV);
					this.m_MaterialYUV.hideFlags = HideFlags.HideAndDontSave;
				}
				if (this.rgbFallback || this.monochrome)
				{
					return this.m_MaterialRGB;
				}
				return this.m_MaterialYUV;
			}
		}

		// Token: 0x06001031 RID: 4145 RVA: 0x00043BA2 File Offset: 0x00041DA2
		protected void OnDisable()
		{
			if (this.m_MaterialRGB)
			{
				Object.DestroyImmediate(this.m_MaterialRGB);
			}
			if (this.m_MaterialYUV)
			{
				Object.DestroyImmediate(this.m_MaterialYUV);
			}
		}

		// Token: 0x06001032 RID: 4146 RVA: 0x00043BD4 File Offset: 0x00041DD4
		private void SanitizeParameters()
		{
			this.grainIntensityMin = Mathf.Clamp(this.grainIntensityMin, 0f, 5f);
			this.grainIntensityMax = Mathf.Clamp(this.grainIntensityMax, 0f, 5f);
			this.scratchIntensityMin = Mathf.Clamp(this.scratchIntensityMin, 0f, 5f);
			this.scratchIntensityMax = Mathf.Clamp(this.scratchIntensityMax, 0f, 5f);
			this.scratchFPS = Mathf.Clamp(this.scratchFPS, 1f, 30f);
			this.scratchJitter = Mathf.Clamp(this.scratchJitter, 0f, 1f);
			this.grainSize = Mathf.Clamp(this.grainSize, 0.1f, 50f);
		}

		// Token: 0x06001033 RID: 4147 RVA: 0x00043CA0 File Offset: 0x00041EA0
		private void OnRenderImage(RenderTexture source, RenderTexture destination)
		{
			this.SanitizeParameters();
			if (this.scratchTimeLeft <= 0f)
			{
				this.scratchTimeLeft = Random.value * 2f / this.scratchFPS;
				this.scratchX = Random.value;
				this.scratchY = Random.value;
			}
			this.scratchTimeLeft -= Time.deltaTime;
			Material material = this.material;
			material.SetTexture("_GrainTex", this.grainTexture);
			material.SetTexture("_ScratchTex", this.scratchTexture);
			float num = 1f / this.grainSize;
			material.SetVector("_GrainOffsetScale", new Vector4(Random.value, Random.value, (float)Screen.width / (float)this.grainTexture.width * num, (float)Screen.height / (float)this.grainTexture.height * num));
			material.SetVector("_ScratchOffsetScale", new Vector4(this.scratchX + Random.value * this.scratchJitter, this.scratchY + Random.value * this.scratchJitter, (float)Screen.width / (float)this.scratchTexture.width, (float)Screen.height / (float)this.scratchTexture.height));
			material.SetVector("_Intensity", new Vector4(Random.Range(this.grainIntensityMin, this.grainIntensityMax), Random.Range(this.scratchIntensityMin, this.scratchIntensityMax), 0f, 0f));
			Graphics.Blit(source, destination, material);
		}

		// Token: 0x04000A51 RID: 2641
		public bool monochrome = true;

		// Token: 0x04000A52 RID: 2642
		private bool rgbFallback;

		// Token: 0x04000A53 RID: 2643
		public float grainIntensityMin = 0.1f;

		// Token: 0x04000A54 RID: 2644
		public float grainIntensityMax = 0.2f;

		// Token: 0x04000A55 RID: 2645
		public float grainSize = 2f;

		// Token: 0x04000A56 RID: 2646
		public float scratchIntensityMin = 0.05f;

		// Token: 0x04000A57 RID: 2647
		public float scratchIntensityMax = 0.25f;

		// Token: 0x04000A58 RID: 2648
		public float scratchFPS = 10f;

		// Token: 0x04000A59 RID: 2649
		public float scratchJitter = 0.01f;

		// Token: 0x04000A5A RID: 2650
		public Texture grainTexture;

		// Token: 0x04000A5B RID: 2651
		public Texture scratchTexture;

		// Token: 0x04000A5C RID: 2652
		public Shader shaderRGB;

		// Token: 0x04000A5D RID: 2653
		public Shader shaderYUV;

		// Token: 0x04000A5E RID: 2654
		private Material m_MaterialRGB;

		// Token: 0x04000A5F RID: 2655
		private Material m_MaterialYUV;

		// Token: 0x04000A60 RID: 2656
		private float scratchTimeLeft;

		// Token: 0x04000A61 RID: 2657
		private float scratchX;

		// Token: 0x04000A62 RID: 2658
		private float scratchY;
	}
}

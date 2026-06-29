using System;
using UnityEngine;

namespace UnityStandardAssets.ImageEffects
{
	// Token: 0x02000162 RID: 354
	[ExecuteInEditMode]
	[AddComponentMenu("Image Effects/Color Adjustments/Contrast Stretch")]
	public class ContrastStretch : MonoBehaviour
	{
		// Token: 0x17000341 RID: 833
		// (get) Token: 0x06001016 RID: 4118 RVA: 0x00042EDD File Offset: 0x000410DD
		protected Material materialLum
		{
			get
			{
				if (this.m_materialLum == null)
				{
					this.m_materialLum = new Material(this.shaderLum);
					this.m_materialLum.hideFlags = HideFlags.HideAndDontSave;
				}
				return this.m_materialLum;
			}
		}

		// Token: 0x17000342 RID: 834
		// (get) Token: 0x06001017 RID: 4119 RVA: 0x00042F11 File Offset: 0x00041111
		protected Material materialReduce
		{
			get
			{
				if (this.m_materialReduce == null)
				{
					this.m_materialReduce = new Material(this.shaderReduce);
					this.m_materialReduce.hideFlags = HideFlags.HideAndDontSave;
				}
				return this.m_materialReduce;
			}
		}

		// Token: 0x17000343 RID: 835
		// (get) Token: 0x06001018 RID: 4120 RVA: 0x00042F45 File Offset: 0x00041145
		protected Material materialAdapt
		{
			get
			{
				if (this.m_materialAdapt == null)
				{
					this.m_materialAdapt = new Material(this.shaderAdapt);
					this.m_materialAdapt.hideFlags = HideFlags.HideAndDontSave;
				}
				return this.m_materialAdapt;
			}
		}

		// Token: 0x17000344 RID: 836
		// (get) Token: 0x06001019 RID: 4121 RVA: 0x00042F79 File Offset: 0x00041179
		protected Material materialApply
		{
			get
			{
				if (this.m_materialApply == null)
				{
					this.m_materialApply = new Material(this.shaderApply);
					this.m_materialApply.hideFlags = HideFlags.HideAndDontSave;
				}
				return this.m_materialApply;
			}
		}

		// Token: 0x0600101A RID: 4122 RVA: 0x00042FB0 File Offset: 0x000411B0
		private void Start()
		{
			if (!SystemInfo.supportsImageEffects)
			{
				base.enabled = false;
				return;
			}
			if (!this.shaderAdapt.isSupported || !this.shaderApply.isSupported || !this.shaderLum.isSupported || !this.shaderReduce.isSupported)
			{
				base.enabled = false;
				return;
			}
		}

		// Token: 0x0600101B RID: 4123 RVA: 0x00043008 File Offset: 0x00041208
		private void OnEnable()
		{
			for (int i = 0; i < 2; i++)
			{
				if (!this.adaptRenderTex[i])
				{
					this.adaptRenderTex[i] = new RenderTexture(1, 1, 0);
					this.adaptRenderTex[i].hideFlags = HideFlags.HideAndDontSave;
				}
			}
		}

		// Token: 0x0600101C RID: 4124 RVA: 0x00043050 File Offset: 0x00041250
		private void OnDisable()
		{
			for (int i = 0; i < 2; i++)
			{
				Object.DestroyImmediate(this.adaptRenderTex[i]);
				this.adaptRenderTex[i] = null;
			}
			if (this.m_materialLum)
			{
				Object.DestroyImmediate(this.m_materialLum);
			}
			if (this.m_materialReduce)
			{
				Object.DestroyImmediate(this.m_materialReduce);
			}
			if (this.m_materialAdapt)
			{
				Object.DestroyImmediate(this.m_materialAdapt);
			}
			if (this.m_materialApply)
			{
				Object.DestroyImmediate(this.m_materialApply);
			}
		}

		// Token: 0x0600101D RID: 4125 RVA: 0x000430E0 File Offset: 0x000412E0
		private void OnRenderImage(RenderTexture source, RenderTexture destination)
		{
			RenderTexture renderTexture = RenderTexture.GetTemporary(source.width / 1, source.height / 1);
			Graphics.Blit(source, renderTexture, this.materialLum);
			while (renderTexture.width > 1 || renderTexture.height > 1)
			{
				int num = renderTexture.width / 2;
				if (num < 1)
				{
					num = 1;
				}
				int num2 = renderTexture.height / 2;
				if (num2 < 1)
				{
					num2 = 1;
				}
				RenderTexture temporary = RenderTexture.GetTemporary(num, num2);
				Graphics.Blit(renderTexture, temporary, this.materialReduce);
				RenderTexture.ReleaseTemporary(renderTexture);
				renderTexture = temporary;
			}
			this.CalculateAdaptation(renderTexture);
			this.materialApply.SetTexture("_AdaptTex", this.adaptRenderTex[this.curAdaptIndex]);
			Graphics.Blit(source, destination, this.materialApply);
			RenderTexture.ReleaseTemporary(renderTexture);
		}

		// Token: 0x0600101E RID: 4126 RVA: 0x00043198 File Offset: 0x00041398
		private void CalculateAdaptation(Texture curTexture)
		{
			int num = this.curAdaptIndex;
			this.curAdaptIndex = (this.curAdaptIndex + 1) % 2;
			float num2 = 1f - Mathf.Pow(1f - this.adaptationSpeed, 30f * Time.deltaTime);
			num2 = Mathf.Clamp(num2, 0.01f, 1f);
			this.materialAdapt.SetTexture("_CurTex", curTexture);
			this.materialAdapt.SetVector("_AdaptParams", new Vector4(num2, this.limitMinimum, this.limitMaximum, 0f));
			Graphics.SetRenderTarget(this.adaptRenderTex[this.curAdaptIndex]);
			GL.Clear(false, true, Color.black);
			Graphics.Blit(this.adaptRenderTex[num], this.adaptRenderTex[this.curAdaptIndex], this.materialAdapt);
		}

		// Token: 0x04000A2E RID: 2606
		public float adaptationSpeed = 0.02f;

		// Token: 0x04000A2F RID: 2607
		public float limitMinimum = 0.2f;

		// Token: 0x04000A30 RID: 2608
		public float limitMaximum = 0.6f;

		// Token: 0x04000A31 RID: 2609
		private RenderTexture[] adaptRenderTex = new RenderTexture[2];

		// Token: 0x04000A32 RID: 2610
		private int curAdaptIndex;

		// Token: 0x04000A33 RID: 2611
		public Shader shaderLum;

		// Token: 0x04000A34 RID: 2612
		private Material m_materialLum;

		// Token: 0x04000A35 RID: 2613
		public Shader shaderReduce;

		// Token: 0x04000A36 RID: 2614
		private Material m_materialReduce;

		// Token: 0x04000A37 RID: 2615
		public Shader shaderAdapt;

		// Token: 0x04000A38 RID: 2616
		private Material m_materialAdapt;

		// Token: 0x04000A39 RID: 2617
		public Shader shaderApply;

		// Token: 0x04000A3A RID: 2618
		private Material m_materialApply;
	}
}

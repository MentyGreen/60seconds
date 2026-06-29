using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace HighlightingSystem
{
	// Token: 0x0200017B RID: 379
	[DisallowMultipleComponent]
	public class HighlighterRenderer : MonoBehaviour
	{
		// Token: 0x060010C0 RID: 4288 RVA: 0x00046ABD File Offset: 0x00044CBD
		private void OnEnable()
		{
			this.endOfFrame = base.StartCoroutine(this.EndOfFrame());
		}

		// Token: 0x060010C1 RID: 4289 RVA: 0x00046AD1 File Offset: 0x00044CD1
		private void OnDisable()
		{
			this.lastCamera = null;
			if (this.endOfFrame != null)
			{
				base.StopCoroutine(this.endOfFrame);
			}
		}

		// Token: 0x060010C2 RID: 4290 RVA: 0x00046AF0 File Offset: 0x00044CF0
		private void OnWillRenderObject()
		{
			Camera current = Camera.current;
			if (HighlightingBase.IsHighlightingCamera(current))
			{
				this.lastCamera = current;
			}
		}

		// Token: 0x060010C3 RID: 4291 RVA: 0x00046B12 File Offset: 0x00044D12
		private IEnumerator EndOfFrame()
		{
			for (;;)
			{
				yield return new WaitForEndOfFrame();
				this.lastCamera = null;
				if (!this.isAlive)
				{
					Object.Destroy(this);
				}
			}
			yield break;
		}

		// Token: 0x060010C4 RID: 4292 RVA: 0x00046B24 File Offset: 0x00044D24
		public void Initialize(Material sharedOpaqueMaterial, Shader transparentShader)
		{
			this.data = new List<HighlighterRenderer.Data>();
			this.r = base.GetComponent<Renderer>();
			base.hideFlags = (HideFlags.HideInInspector | HideFlags.DontSaveInEditor | HideFlags.NotEditable | HideFlags.DontSaveInBuild);
			Material[] sharedMaterials = this.r.sharedMaterials;
			if (sharedMaterials != null)
			{
				for (int i = 0; i < sharedMaterials.Length; i++)
				{
					Material material = sharedMaterials[i];
					if (!(material == null))
					{
						HighlighterRenderer.Data item = default(HighlighterRenderer.Data);
						string tag = material.GetTag(HighlighterRenderer.sRenderType, true, HighlighterRenderer.sOpaque);
						if (tag == HighlighterRenderer.sTransparent || tag == HighlighterRenderer.sTransparentCutout)
						{
							Material material2 = new Material(transparentShader);
							if (this.r is SpriteRenderer)
							{
								material2.SetInt(ShaderPropertyID._Cull, 0);
							}
							if (material.HasProperty(ShaderPropertyID._MainTex))
							{
								material2.SetTexture(ShaderPropertyID._MainTex, material.mainTexture);
								material2.SetTextureOffset(HighlighterRenderer.sMainTex, material.mainTextureOffset);
								material2.SetTextureScale(HighlighterRenderer.sMainTex, material.mainTextureScale);
							}
							int cutoff = ShaderPropertyID._Cutoff;
							material2.SetFloat(cutoff, material.HasProperty(cutoff) ? material.GetFloat(cutoff) : HighlighterRenderer.transparentCutoff);
							item.material = material2;
							item.transparent = true;
						}
						else
						{
							item.material = sharedOpaqueMaterial;
							item.transparent = false;
						}
						item.submeshIndex = i;
						this.data.Add(item);
					}
				}
			}
		}

		// Token: 0x060010C5 RID: 4293 RVA: 0x00046C88 File Offset: 0x00044E88
		public bool FillBuffer(CommandBuffer buffer)
		{
			if (this.r == null)
			{
				return false;
			}
			if (this.lastCamera == Camera.current)
			{
				int i = 0;
				int count = this.data.Count;
				while (i < count)
				{
					HighlighterRenderer.Data data = this.data[i];
					buffer.DrawRenderer(this.r, data.material, data.submeshIndex);
					i++;
				}
			}
			return true;
		}

		// Token: 0x060010C6 RID: 4294 RVA: 0x00046CF8 File Offset: 0x00044EF8
		public void SetColorForTransparent(Color clr)
		{
			int i = 0;
			int count = this.data.Count;
			while (i < count)
			{
				HighlighterRenderer.Data data = this.data[i];
				if (data.transparent)
				{
					data.material.SetColor(ShaderPropertyID._Color, clr);
				}
				i++;
			}
		}

		// Token: 0x060010C7 RID: 4295 RVA: 0x00046D44 File Offset: 0x00044F44
		public void SetZTestForTransparent(int zTest)
		{
			int i = 0;
			int count = this.data.Count;
			while (i < count)
			{
				HighlighterRenderer.Data data = this.data[i];
				if (data.transparent)
				{
					data.material.SetInt(ShaderPropertyID._ZTest, zTest);
				}
				i++;
			}
		}

		// Token: 0x060010C8 RID: 4296 RVA: 0x00046D90 File Offset: 0x00044F90
		public void SetStencilRefForTransparent(int stencilRef)
		{
			int i = 0;
			int count = this.data.Count;
			while (i < count)
			{
				HighlighterRenderer.Data data = this.data[i];
				if (data.transparent)
				{
					data.material.SetInt(ShaderPropertyID._StencilRef, stencilRef);
				}
				i++;
			}
		}

		// Token: 0x060010C9 RID: 4297 RVA: 0x00046DDB File Offset: 0x00044FDB
		public void SetState(bool alive)
		{
			this.isAlive = alive;
		}

		// Token: 0x04000AC8 RID: 2760
		private static float transparentCutoff = 0.5f;

		// Token: 0x04000AC9 RID: 2761
		private const HideFlags flags = HideFlags.HideInInspector | HideFlags.DontSaveInEditor | HideFlags.NotEditable | HideFlags.DontSaveInBuild;

		// Token: 0x04000ACA RID: 2762
		private const int cullOff = 0;

		// Token: 0x04000ACB RID: 2763
		private static readonly string sRenderType = "RenderType";

		// Token: 0x04000ACC RID: 2764
		private static readonly string sOpaque = "Opaque";

		// Token: 0x04000ACD RID: 2765
		private static readonly string sTransparent = "Transparent";

		// Token: 0x04000ACE RID: 2766
		private static readonly string sTransparentCutout = "TransparentCutout";

		// Token: 0x04000ACF RID: 2767
		private static readonly string sMainTex = "_MainTex";

		// Token: 0x04000AD0 RID: 2768
		private Renderer r;

		// Token: 0x04000AD1 RID: 2769
		private List<HighlighterRenderer.Data> data;

		// Token: 0x04000AD2 RID: 2770
		private Camera lastCamera;

		// Token: 0x04000AD3 RID: 2771
		private bool isAlive;

		// Token: 0x04000AD4 RID: 2772
		private Coroutine endOfFrame;

		// Token: 0x020003D7 RID: 983
		private struct Data
		{
			// Token: 0x040017F2 RID: 6130
			public Material material;

			// Token: 0x040017F3 RID: 6131
			public int submeshIndex;

			// Token: 0x040017F4 RID: 6132
			public bool transparent;
		}
	}
}

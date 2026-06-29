using System;
using System.Collections.Generic;
using UnityEngine;

namespace HighlightingSystem
{
	// Token: 0x02000179 RID: 377
	[RequireComponent(typeof(Camera))]
	public class HighlightingBlitter : MonoBehaviour
	{
		// Token: 0x060010BB RID: 4283 RVA: 0x000469DC File Offset: 0x00044BDC
		protected virtual void OnRenderImage(RenderTexture src, RenderTexture dst)
		{
			bool flag = true;
			for (int i = 0; i < this.renderers.Count; i++)
			{
				HighlightingBase highlightingBase = this.renderers[i];
				if (flag)
				{
					highlightingBase.Blit(src, dst);
				}
				else
				{
					highlightingBase.Blit(dst, src);
				}
				flag = !flag;
			}
			if (flag)
			{
				Graphics.Blit(src, dst);
			}
		}

		// Token: 0x060010BC RID: 4284 RVA: 0x00046A32 File Offset: 0x00044C32
		public virtual void Register(HighlightingBase renderer)
		{
			if (!this.renderers.Contains(renderer))
			{
				this.renderers.Add(renderer);
			}
			base.enabled = (this.renderers.Count > 0);
		}

		// Token: 0x060010BD RID: 4285 RVA: 0x00046A64 File Offset: 0x00044C64
		public virtual void Unregister(HighlightingBase renderer)
		{
			int num = this.renderers.IndexOf(renderer);
			if (num != -1)
			{
				this.renderers.RemoveAt(num);
			}
			base.enabled = (this.renderers.Count > 0);
		}

		// Token: 0x04000AC7 RID: 2759
		protected List<HighlightingBase> renderers = new List<HighlightingBase>();
	}
}

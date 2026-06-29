using System;
using UnityEngine;

namespace UnityStandardAssets.ImageEffects
{
	// Token: 0x02000164 RID: 356
	[RequireComponent(typeof(Camera))]
	[AddComponentMenu("")]
	public class ImageEffectBase : MonoBehaviour
	{
		// Token: 0x06001022 RID: 4130 RVA: 0x000432DE File Offset: 0x000414DE
		protected virtual void Start()
		{
			if (!SystemInfo.supportsImageEffects)
			{
				base.enabled = false;
				return;
			}
			if (!this.shader || !this.shader.isSupported)
			{
				base.enabled = false;
			}
		}

		// Token: 0x17000345 RID: 837
		// (get) Token: 0x06001023 RID: 4131 RVA: 0x00043310 File Offset: 0x00041510
		protected Material material
		{
			get
			{
				if (this.m_Material == null)
				{
					this.m_Material = new Material(this.shader);
					this.m_Material.hideFlags = HideFlags.HideAndDontSave;
				}
				return this.m_Material;
			}
		}

		// Token: 0x06001024 RID: 4132 RVA: 0x00043344 File Offset: 0x00041544
		protected virtual void OnDisable()
		{
			if (this.m_Material)
			{
				Object.DestroyImmediate(this.m_Material);
			}
		}

		// Token: 0x04000A3D RID: 2621
		public Shader shader;

		// Token: 0x04000A3E RID: 2622
		private Material m_Material;
	}
}

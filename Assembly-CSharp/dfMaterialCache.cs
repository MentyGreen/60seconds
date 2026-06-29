using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000072 RID: 114
internal class dfMaterialCache
{
	// Token: 0x0600080C RID: 2060 RVA: 0x00023760 File Offset: 0x00021960
	public static Material Lookup(Material BaseMaterial)
	{
		if (BaseMaterial == null)
		{
			Debug.LogError("Cache lookup on null material");
			return null;
		}
		dfMaterialCache.Cache cache = null;
		if (!dfMaterialCache.caches.TryGetValue(BaseMaterial, out cache))
		{
			cache = (dfMaterialCache.caches[BaseMaterial] = new dfMaterialCache.Cache(BaseMaterial));
		}
		return cache.Obtain();
	}

	// Token: 0x0600080D RID: 2061 RVA: 0x000237AE File Offset: 0x000219AE
	public static void Reset()
	{
		dfMaterialCache.Cache.ResetAll();
	}

	// Token: 0x0600080E RID: 2062 RVA: 0x000237B5 File Offset: 0x000219B5
	public static void Clear()
	{
		dfMaterialCache.Cache.ClearAll();
		dfMaterialCache.caches.Clear();
	}

	// Token: 0x040003D7 RID: 983
	private static Dictionary<Material, dfMaterialCache.Cache> caches = new Dictionary<Material, dfMaterialCache.Cache>();

	// Token: 0x02000379 RID: 889
	private class Cache
	{
		// Token: 0x06001CD6 RID: 7382 RVA: 0x0007C5B0 File Offset: 0x0007A7B0
		private Cache()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001CD7 RID: 7383 RVA: 0x0007C5CA File Offset: 0x0007A7CA
		public Cache(Material BaseMaterial)
		{
			this.baseMaterial = BaseMaterial;
			this.instances.Add(BaseMaterial);
			dfMaterialCache.Cache.cacheInstances.Add(this);
		}

		// Token: 0x06001CD8 RID: 7384 RVA: 0x0007C600 File Offset: 0x0007A800
		public static void ClearAll()
		{
			for (int i = 0; i < dfMaterialCache.Cache.cacheInstances.Count; i++)
			{
				dfMaterialCache.Cache.cacheInstances[i].Clear();
			}
			dfMaterialCache.Cache.cacheInstances.Clear();
		}

		// Token: 0x06001CD9 RID: 7385 RVA: 0x0007C63C File Offset: 0x0007A83C
		public static void ResetAll()
		{
			for (int i = 0; i < dfMaterialCache.Cache.cacheInstances.Count; i++)
			{
				dfMaterialCache.Cache.cacheInstances[i].Reset();
			}
		}

		// Token: 0x06001CDA RID: 7386 RVA: 0x0007C670 File Offset: 0x0007A870
		public Material Obtain()
		{
			if (this.currentIndex < this.instances.Count)
			{
				List<Material> list = this.instances;
				int num = this.currentIndex;
				this.currentIndex = num + 1;
				return list[num];
			}
			this.currentIndex++;
			Material material = new Material(this.baseMaterial)
			{
				hideFlags = (HideFlags.HideInInspector | HideFlags.DontSaveInEditor | HideFlags.DontSaveInBuild | HideFlags.DontUnloadUnusedAsset),
				name = string.Format("{0} (Copy {1})", this.baseMaterial.name, this.currentIndex)
			};
			this.instances.Add(material);
			return material;
		}

		// Token: 0x06001CDB RID: 7387 RVA: 0x0007C702 File Offset: 0x0007A902
		public void Reset()
		{
			this.currentIndex = 0;
		}

		// Token: 0x06001CDC RID: 7388 RVA: 0x0007C70C File Offset: 0x0007A90C
		public void Clear()
		{
			this.currentIndex = 0;
			for (int i = 1; i < this.instances.Count; i++)
			{
				Material material = this.instances[i];
				if (material != null)
				{
					if (Application.isPlaying)
					{
						Object.Destroy(material);
					}
					else
					{
						Object.DestroyImmediate(material);
					}
				}
			}
			this.instances.Clear();
		}

		// Token: 0x0400166D RID: 5741
		private static List<dfMaterialCache.Cache> cacheInstances = new List<dfMaterialCache.Cache>();

		// Token: 0x0400166E RID: 5742
		private Material baseMaterial;

		// Token: 0x0400166F RID: 5743
		private List<Material> instances = new List<Material>(10);

		// Token: 0x04001670 RID: 5744
		private int currentIndex;
	}
}

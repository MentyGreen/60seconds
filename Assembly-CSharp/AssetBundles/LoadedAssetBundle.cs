using System;
using UnityEngine;

namespace AssetBundles
{
	// Token: 0x0200020B RID: 523
	public class LoadedAssetBundle
	{
		// Token: 0x0600147C RID: 5244 RVA: 0x0005B28F File Offset: 0x0005948F
		public LoadedAssetBundle(AssetBundle assetBundle)
		{
			this.m_AssetBundle = assetBundle;
			this.m_ReferencedCount = 1;
		}

		// Token: 0x04000D85 RID: 3461
		public AssetBundle m_AssetBundle;

		// Token: 0x04000D86 RID: 3462
		public int m_ReferencedCount;
	}
}

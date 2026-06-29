using System;
using UnityEngine;

namespace AssetBundles
{
	// Token: 0x02000207 RID: 519
	public abstract class AssetBundleLoadAssetOperation : AssetBundleLoadOperation
	{
		// Token: 0x06001470 RID: 5232
		public abstract T GetAsset<T>() where T : Object;
	}
}

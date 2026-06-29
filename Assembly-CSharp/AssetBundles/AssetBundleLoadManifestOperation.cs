using System;
using UnityEngine;

namespace AssetBundles
{
	// Token: 0x0200020A RID: 522
	public class AssetBundleLoadManifestOperation : AssetBundleLoadAssetOperationFull
	{
		// Token: 0x0600147A RID: 5242 RVA: 0x0005B258 File Offset: 0x00059458
		public AssetBundleLoadManifestOperation(string bundleName, string assetName, Type type) : base(bundleName, assetName, type)
		{
		}

		// Token: 0x0600147B RID: 5243 RVA: 0x0005B263 File Offset: 0x00059463
		public override bool Update()
		{
			base.Update();
			if (this.m_Request != null && this.m_Request.isDone)
			{
				AssetBundleManager.AssetBundleManifestObject = this.GetAsset<AssetBundleManifest>();
				return false;
			}
			return true;
		}
	}
}

using System;
using UnityEngine;

namespace AssetBundles
{
	// Token: 0x02000209 RID: 521
	public class AssetBundleLoadAssetOperationFull : AssetBundleLoadAssetOperation
	{
		// Token: 0x06001476 RID: 5238 RVA: 0x0005B174 File Offset: 0x00059374
		public AssetBundleLoadAssetOperationFull(string bundleName, string assetName, Type type)
		{
			this.m_AssetBundleName = bundleName;
			this.m_AssetName = assetName;
			this.m_Type = type;
		}

		// Token: 0x06001477 RID: 5239 RVA: 0x0005B194 File Offset: 0x00059394
		public override T GetAsset<T>()
		{
			if (this.m_Request != null && this.m_Request.isDone)
			{
				return this.m_Request.asset as T;
			}
			return default(T);
		}

		// Token: 0x06001478 RID: 5240 RVA: 0x0005B1D8 File Offset: 0x000593D8
		public override bool Update()
		{
			if (this.m_Request != null)
			{
				return false;
			}
			LoadedAssetBundle loadedAssetBundle = AssetBundleManager.GetLoadedAssetBundle(this.m_AssetBundleName, out this.m_DownloadingError);
			if (loadedAssetBundle != null)
			{
				this.m_Request = loadedAssetBundle.m_AssetBundle.LoadAssetAsync(this.m_AssetName, this.m_Type);
				return false;
			}
			return true;
		}

		// Token: 0x06001479 RID: 5241 RVA: 0x0005B224 File Offset: 0x00059424
		public override bool IsDone()
		{
			if (this.m_Request == null && this.m_DownloadingError != null)
			{
				Debug.LogError(this.m_DownloadingError);
				return true;
			}
			return this.m_Request != null && this.m_Request.isDone;
		}

		// Token: 0x04000D80 RID: 3456
		protected string m_AssetBundleName;

		// Token: 0x04000D81 RID: 3457
		protected string m_AssetName;

		// Token: 0x04000D82 RID: 3458
		protected string m_DownloadingError;

		// Token: 0x04000D83 RID: 3459
		protected Type m_Type;

		// Token: 0x04000D84 RID: 3460
		protected AssetBundleRequest m_Request;
	}
}

using System;
using UnityEngine;

namespace AssetBundles
{
	// Token: 0x02000206 RID: 518
	public class AssetBundleLoadLevelOperation : AssetBundleLoadOperation
	{
		// Token: 0x0600146D RID: 5229 RVA: 0x0005B099 File Offset: 0x00059299
		public AssetBundleLoadLevelOperation(string assetbundleName, string levelName, bool isAdditive)
		{
			this.m_AssetBundleName = assetbundleName;
			this.m_LevelName = levelName;
			this.m_IsAdditive = isAdditive;
		}

		// Token: 0x0600146E RID: 5230 RVA: 0x0005B0B8 File Offset: 0x000592B8
		public override bool Update()
		{
			if (this.m_Request != null)
			{
				return false;
			}
			if (AssetBundleManager.GetLoadedAssetBundle(this.m_AssetBundleName, out this.m_DownloadingError) != null)
			{
				if (this.m_IsAdditive)
				{
					this.m_Request = Application.LoadLevelAdditiveAsync(this.m_LevelName);
				}
				else
				{
					this.m_Request = Application.LoadLevelAsync(this.m_LevelName);
				}
				return false;
			}
			return true;
		}

		// Token: 0x0600146F RID: 5231 RVA: 0x0005B111 File Offset: 0x00059311
		public override bool IsDone()
		{
			if (this.m_Request == null && this.m_DownloadingError != null)
			{
				Debug.LogError(this.m_DownloadingError);
				return true;
			}
			return this.m_Request != null && this.m_Request.isDone;
		}

		// Token: 0x04000D7A RID: 3450
		protected string m_AssetBundleName;

		// Token: 0x04000D7B RID: 3451
		protected string m_LevelName;

		// Token: 0x04000D7C RID: 3452
		protected bool m_IsAdditive;

		// Token: 0x04000D7D RID: 3453
		protected string m_DownloadingError;

		// Token: 0x04000D7E RID: 3454
		protected AsyncOperation m_Request;
	}
}

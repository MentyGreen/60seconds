using System;
using UnityEngine;

namespace AssetBundles
{
	// Token: 0x0200020D RID: 525
	public class Utility
	{
		// Token: 0x06001498 RID: 5272 RVA: 0x0005BA40 File Offset: 0x00059C40
		public static string GetPlatformName()
		{
			return Utility.GetPlatformForAssetBundles(Application.platform);
		}

		// Token: 0x06001499 RID: 5273 RVA: 0x0005BA4C File Offset: 0x00059C4C
		private static string GetPlatformForAssetBundles(RuntimePlatform platform)
		{
			if (platform <= RuntimePlatform.WindowsPlayer)
			{
				if (platform == RuntimePlatform.OSXPlayer)
				{
					return "OSX";
				}
				if (platform == RuntimePlatform.WindowsPlayer)
				{
					return "Windows";
				}
			}
			else
			{
				if (platform == RuntimePlatform.IPhonePlayer)
				{
					return "iOS";
				}
				if (platform == RuntimePlatform.Android)
				{
					return "Android";
				}
				if (platform == RuntimePlatform.WebGLPlayer)
				{
					return "WebGL";
				}
			}
			return null;
		}

		// Token: 0x04000D90 RID: 3472
		public const string AssetBundlesOutputPath = "AssetBundles";
	}
}

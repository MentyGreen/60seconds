using System;
using UnityEngine;

namespace RG_GameCamera.Utils
{
	// Token: 0x0200017F RID: 383
	internal class CameraInstance
	{
		// Token: 0x06001117 RID: 4375 RVA: 0x00047ECD File Offset: 0x000460CD
		public static GameObject GetCameraRoot()
		{
			if (!CameraInstance.cameraRoot)
			{
				CameraInstance.cameraRoot = GameObject.Find(CameraInstance.RootName);
				if (!CameraInstance.cameraRoot)
				{
					CameraInstance.cameraRoot = new GameObject(CameraInstance.RootName);
				}
			}
			return CameraInstance.cameraRoot;
		}

		// Token: 0x06001118 RID: 4376 RVA: 0x00047F0C File Offset: 0x0004610C
		public static T CreateInstance<T>(string name) where T : Component
		{
			GameObject gameObject = CameraInstance.GetCameraRoot();
			T componentInChildren = gameObject.GetComponentInChildren<T>();
			if (componentInChildren)
			{
				return componentInChildren;
			}
			return new GameObject(name)
			{
				transform = 
				{
					parent = gameObject.transform
				}
			}.AddComponent<T>();
		}

		// Token: 0x04000B16 RID: 2838
		public static string RootName = "GameCamera";

		// Token: 0x04000B17 RID: 2839
		private static GameObject cameraRoot;
	}
}

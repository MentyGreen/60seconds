using System;
using System.Diagnostics;
using UnityEngine;

namespace RG_GameCamera.Utils
{
	// Token: 0x02000181 RID: 385
	public static class Debug
	{
		// Token: 0x06001120 RID: 4384 RVA: 0x00048006 File Offset: 0x00046206
		[Conditional("UNITY_EDITOR")]
		public static void Assert(bool condition, string message = "")
		{
			if (!condition)
			{
				Debug.LogError("Assert! " + message);
				Debug.Break();
			}
		}

		// Token: 0x06001121 RID: 4385 RVA: 0x00048020 File Offset: 0x00046220
		[Conditional("UNITY_EDITOR")]
		public static void Log(string format, params object[] args)
		{
			Debug.Log(string.Format(format, args));
		}

		// Token: 0x06001122 RID: 4386 RVA: 0x0004802E File Offset: 0x0004622E
		[Conditional("UNITY_EDITOR")]
		public static void Log(object arg)
		{
			Debug.Log(arg.ToString());
		}

		// Token: 0x06001123 RID: 4387 RVA: 0x0004803C File Offset: 0x0004623C
		public static Vector3 GetCentroid(GameObject obj)
		{
			MeshRenderer[] componentsInChildren = obj.GetComponentsInChildren<MeshRenderer>();
			Vector3 a = Vector3.zero;
			if (componentsInChildren != null && componentsInChildren.Length != 0)
			{
				foreach (MeshRenderer meshRenderer in componentsInChildren)
				{
					a += meshRenderer.bounds.center;
				}
				return a / (float)componentsInChildren.Length;
			}
			SkinnedMeshRenderer componentInChildren = obj.GetComponentInChildren<SkinnedMeshRenderer>();
			if (componentInChildren)
			{
				return componentInChildren.bounds.center;
			}
			return obj.transform.position;
		}

		// Token: 0x06001124 RID: 4388 RVA: 0x000480C4 File Offset: 0x000462C4
		public static void SetVisible(GameObject obj, bool status, bool includeInactive)
		{
			if (obj)
			{
				MeshRenderer[] componentsInChildren = obj.GetComponentsInChildren<MeshRenderer>(includeInactive);
				for (int i = 0; i < componentsInChildren.Length; i++)
				{
					componentsInChildren[i].enabled = status;
				}
				SkinnedMeshRenderer[] componentsInChildren2 = obj.GetComponentsInChildren<SkinnedMeshRenderer>(includeInactive);
				for (int i = 0; i < componentsInChildren2.Length; i++)
				{
					componentsInChildren2[i].enabled = status;
				}
			}
		}

		// Token: 0x06001125 RID: 4389 RVA: 0x00048117 File Offset: 0x00046317
		public static void ClearLog()
		{
		}

		// Token: 0x06001126 RID: 4390 RVA: 0x00048119 File Offset: 0x00046319
		public static bool IsActive(GameObject obj)
		{
			return obj && obj.activeSelf;
		}

		// Token: 0x06001127 RID: 4391 RVA: 0x0004812B File Offset: 0x0004632B
		public static void SetActive(GameObject obj, bool status)
		{
			if (obj)
			{
				obj.SetActive(status);
			}
		}

		// Token: 0x06001128 RID: 4392 RVA: 0x0004813C File Offset: 0x0004633C
		public static void SetActiveRecursively(GameObject obj, bool status)
		{
			if (obj)
			{
				int childCount = obj.transform.childCount;
				for (int i = 0; i < childCount; i++)
				{
					Debug.SetActiveRecursively(obj.transform.GetChild(i).gameObject, status);
				}
				obj.SetActive(status);
			}
		}

		// Token: 0x06001129 RID: 4393 RVA: 0x00048188 File Offset: 0x00046388
		public static void EnableCollider(GameObject obj, bool status)
		{
			if (obj)
			{
				Collider[] componentsInChildren = obj.GetComponentsInChildren<Collider>();
				for (int i = 0; i < componentsInChildren.Length; i++)
				{
					componentsInChildren[i].enabled = status;
				}
			}
		}

		// Token: 0x0600112A RID: 4394 RVA: 0x000481BB File Offset: 0x000463BB
		public static void Destroy(Object obj, bool allowDestroyingAssets)
		{
			if (Application.isPlaying)
			{
				Object.Destroy(obj);
				return;
			}
			Object.DestroyImmediate(obj, allowDestroyingAssets);
		}
	}
}

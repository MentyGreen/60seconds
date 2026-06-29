using System;
using System.Collections.Generic;
using UnityEngine;

namespace DunGen
{
	// Token: 0x020001FA RID: 506
	public static class UnityUtil
	{
		// Token: 0x06001422 RID: 5154 RVA: 0x0005A09C File Offset: 0x0005829C
		public static string GetUniqueName(string name, IEnumerable<string> usedNames)
		{
			if (string.IsNullOrEmpty(name))
			{
				return UnityUtil.GetUniqueName("New", usedNames);
			}
			string str = name;
			int num = 0;
			bool flag = false;
			int num2 = name.LastIndexOf(' ');
			if (num2 > -1)
			{
				str = name.Substring(0, num2);
				flag = int.TryParse(name.Substring(num2 + 1), out num);
				num++;
			}
			using (IEnumerator<string> enumerator = usedNames.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current == name)
					{
						if (flag)
						{
							return UnityUtil.GetUniqueName(str + " " + num.ToString(), usedNames);
						}
						return UnityUtil.GetUniqueName(name + " 2", usedNames);
					}
				}
			}
			return name;
		}

		// Token: 0x06001423 RID: 5155 RVA: 0x0005A168 File Offset: 0x00058368
		public static Bounds CalculateObjectBounds(GameObject obj, bool includeInactive, bool ignoreSpriteRenderers)
		{
			Bounds result = default(Bounds);
			bool flag = false;
			foreach (Renderer renderer in obj.GetComponentsInChildren<Renderer>(includeInactive))
			{
				if (renderer is MeshRenderer || (renderer is SpriteRenderer && !ignoreSpriteRenderers))
				{
					if (flag)
					{
						result.Encapsulate(renderer.bounds);
					}
					else
					{
						result = renderer.bounds;
					}
					flag = true;
				}
			}
			foreach (Collider collider in obj.GetComponentsInChildren<Collider>(includeInactive))
			{
				if (flag)
				{
					result.Encapsulate(collider.bounds);
				}
				else
				{
					result = collider.bounds;
				}
				flag = true;
			}
			return result;
		}

		// Token: 0x06001424 RID: 5156 RVA: 0x0005A211 File Offset: 0x00058411
		public static IEnumerable<T> GetComponentsInParents<T>(GameObject obj, bool includeInactive = false) where T : Component
		{
			if (obj.activeSelf || includeInactive)
			{
				foreach (T t in obj.GetComponents<T>())
				{
					yield return t;
				}
				T[] array = null;
			}
			if (obj.transform.parent != null)
			{
				foreach (T t2 in UnityUtil.GetComponentsInParents<T>(obj.transform.parent.gameObject, includeInactive))
				{
					yield return t2;
				}
				IEnumerator<T> enumerator = null;
			}
			yield break;
			yield break;
		}

		// Token: 0x06001425 RID: 5157 RVA: 0x0005A228 File Offset: 0x00058428
		public static T GetComponentInParents<T>(GameObject obj, bool includeInactive = false) where T : Component
		{
			if (obj.activeSelf || includeInactive)
			{
				T[] components = obj.GetComponents<T>();
				int num = 0;
				if (num < components.Length)
				{
					return components[num];
				}
			}
			if (obj.transform.parent != null)
			{
				return UnityUtil.GetComponentInParents<T>(obj.transform.parent.gameObject, includeInactive);
			}
			return default(T);
		}
	}
}

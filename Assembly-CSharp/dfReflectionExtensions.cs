using System;
using System.Collections;
using System.Linq;
using System.Reflection;
using UnityEngine;

// Token: 0x0200007D RID: 125
public static class dfReflectionExtensions
{
	// Token: 0x06000839 RID: 2105 RVA: 0x00023FD1 File Offset: 0x000221D1
	public static MemberTypes GetMemberType(this MemberInfo member)
	{
		return member.MemberType;
	}

	// Token: 0x0600083A RID: 2106 RVA: 0x00023FD9 File Offset: 0x000221D9
	public static Type GetBaseType(this Type type)
	{
		return type.BaseType;
	}

	// Token: 0x0600083B RID: 2107 RVA: 0x00023FE1 File Offset: 0x000221E1
	public static Assembly GetAssembly(this Type type)
	{
		return type.Assembly;
	}

	// Token: 0x0600083C RID: 2108 RVA: 0x00023FE9 File Offset: 0x000221E9
	[HideInInspector]
	internal static bool SignalHierarchy(this GameObject target, string messageName, params object[] args)
	{
		while (target != null)
		{
			if (target.Signal(messageName, args))
			{
				return true;
			}
			if (target.transform.parent == null)
			{
				break;
			}
			target = target.transform.parent.gameObject;
		}
		return false;
	}

	// Token: 0x0600083D RID: 2109 RVA: 0x00024028 File Offset: 0x00022228
	[HideInInspector]
	internal static bool Signal(this GameObject target, string messageName, params object[] args)
	{
		Component[] components = target.GetComponents(typeof(MonoBehaviour));
		Type[] array = new Type[args.Length];
		for (int i = 0; i < array.Length; i++)
		{
			if (args[i] == null)
			{
				array[i] = typeof(object);
			}
			else
			{
				array[i] = args[i].GetType();
			}
		}
		bool result = false;
		foreach (Component component in components)
		{
			if (!(component == null) && !(component.GetType() == null) && (!(component is MonoBehaviour) || ((MonoBehaviour)component).enabled))
			{
				MethodInfo method = dfReflectionExtensions.getMethod(component.GetType(), messageName, array);
				if (method != null)
				{
					IEnumerator enumerator = method.Invoke(component, args) as IEnumerator;
					if (enumerator != null)
					{
						((MonoBehaviour)component).StartCoroutine(enumerator);
					}
					result = true;
				}
				else if (args.Length != 0)
				{
					MethodInfo method2 = dfReflectionExtensions.getMethod(component.GetType(), messageName, dfReflectionExtensions.EmptyTypes);
					if (method2 != null)
					{
						IEnumerator enumerator = method2.Invoke(component, null) as IEnumerator;
						if (enumerator != null)
						{
							((MonoBehaviour)component).StartCoroutine(enumerator);
						}
						result = true;
					}
				}
			}
		}
		return result;
	}

	// Token: 0x0600083E RID: 2110 RVA: 0x00024163 File Offset: 0x00022363
	private static MethodInfo getMethod(Type type, string name, Type[] paramTypes)
	{
		return type.GetMethod(name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, paramTypes, null);
	}

	// Token: 0x0600083F RID: 2111 RVA: 0x00024174 File Offset: 0x00022374
	private static bool matchesParameterTypes(MethodInfo method, Type[] types)
	{
		ParameterInfo[] parameters = method.GetParameters();
		if (parameters.Length != types.Length)
		{
			return false;
		}
		for (int i = 0; i < types.Length; i++)
		{
			if (!parameters[i].ParameterType.IsAssignableFrom(types[i]))
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x06000840 RID: 2112 RVA: 0x000241B8 File Offset: 0x000223B8
	internal static FieldInfo[] GetAllFields(this Type type)
	{
		if (type == null)
		{
			return new FieldInfo[0];
		}
		BindingFlags bindingAttr = BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
		return (from f in type.GetFields(bindingAttr).Concat(type.GetBaseType().GetAllFields())
		where !f.IsDefined(typeof(HideInInspector), true)
		select f).ToArray<FieldInfo>();
	}

	// Token: 0x040003DD RID: 989
	public static Type[] EmptyTypes = new Type[0];
}

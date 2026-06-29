using System;
using System.Linq;
using System.Reflection;
using UnityEngine;

// Token: 0x020000A0 RID: 160
[Serializable]
public class dfComponentMemberInfo
{
	// Token: 0x17000212 RID: 530
	// (get) Token: 0x06000959 RID: 2393 RVA: 0x0002988C File Offset: 0x00027A8C
	public bool IsValid
	{
		get
		{
			return this.Component != null && !string.IsNullOrEmpty(this.MemberName) && !(this.Component.GetType().GetMember(this.MemberName).FirstOrDefault<MemberInfo>() == null);
		}
	}

	// Token: 0x0600095A RID: 2394 RVA: 0x000298E4 File Offset: 0x00027AE4
	public Type GetMemberType()
	{
		Type type = this.Component.GetType();
		MemberInfo memberInfo = type.GetMember(this.MemberName).FirstOrDefault<MemberInfo>();
		if (memberInfo == null)
		{
			throw new MissingMemberException("Member not found: " + type.Name + "." + this.MemberName);
		}
		if (memberInfo is FieldInfo)
		{
			return ((FieldInfo)memberInfo).FieldType;
		}
		if (memberInfo is PropertyInfo)
		{
			return ((PropertyInfo)memberInfo).PropertyType;
		}
		if (memberInfo is MethodInfo)
		{
			return ((MethodInfo)memberInfo).ReturnType;
		}
		if (memberInfo is EventInfo)
		{
			return ((EventInfo)memberInfo).EventHandlerType;
		}
		throw new InvalidCastException("Invalid member type: " + memberInfo.GetMemberType().ToString());
	}

	// Token: 0x0600095B RID: 2395 RVA: 0x000299AC File Offset: 0x00027BAC
	public MethodInfo GetMethod()
	{
		return this.Component.GetType().GetMember(this.MemberName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic).FirstOrDefault<MemberInfo>() as MethodInfo;
	}

	// Token: 0x0600095C RID: 2396 RVA: 0x000299D0 File Offset: 0x00027BD0
	public dfObservableProperty GetProperty()
	{
		Type type = this.Component.GetType();
		MemberInfo memberInfo = this.Component.GetType().GetMember(this.MemberName).FirstOrDefault<MemberInfo>();
		if (memberInfo == null)
		{
			throw new MissingMemberException("Member not found: " + type.Name + "." + this.MemberName);
		}
		if (!(memberInfo is FieldInfo) && !(memberInfo is PropertyInfo))
		{
			throw new InvalidCastException("Member " + this.MemberName + " is not an observable field or property");
		}
		return new dfObservableProperty(this.Component, memberInfo);
	}

	// Token: 0x0600095D RID: 2397 RVA: 0x00029A68 File Offset: 0x00027C68
	public override string ToString()
	{
		string arg = (this.Component != null) ? this.Component.GetType().Name : "[Missing ComponentType]";
		string arg2 = (!string.IsNullOrEmpty(this.MemberName)) ? this.MemberName : "[Missing MemberName]";
		return string.Format("{0}.{1}", arg, arg2);
	}

	// Token: 0x04000479 RID: 1145
	public Component Component;

	// Token: 0x0400047A RID: 1146
	public string MemberName;
}

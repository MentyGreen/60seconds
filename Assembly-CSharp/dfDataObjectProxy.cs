using System;
using System.Linq;
using System.Reflection;
using UnityEngine;

// Token: 0x020000A2 RID: 162
[AddComponentMenu("Daikon Forge/Data Binding/Proxy Data Object")]
[Serializable]
public class dfDataObjectProxy : MonoBehaviour, IDataBindingComponent
{
	// Token: 0x14000050 RID: 80
	// (add) Token: 0x06000973 RID: 2419 RVA: 0x00029C5C File Offset: 0x00027E5C
	// (remove) Token: 0x06000974 RID: 2420 RVA: 0x00029C94 File Offset: 0x00027E94
	public event dfDataObjectProxy.DataObjectChangedHandler DataChanged;

	// Token: 0x1700021A RID: 538
	// (get) Token: 0x06000975 RID: 2421 RVA: 0x00029CC9 File Offset: 0x00027EC9
	public bool IsBound
	{
		get
		{
			return this.data != null;
		}
	}

	// Token: 0x1700021B RID: 539
	// (get) Token: 0x06000976 RID: 2422 RVA: 0x00029CD4 File Offset: 0x00027ED4
	// (set) Token: 0x06000977 RID: 2423 RVA: 0x00029CDC File Offset: 0x00027EDC
	public string TypeName
	{
		get
		{
			return this.typeName;
		}
		set
		{
			if (this.typeName != value)
			{
				this.typeName = value;
				this.Data = null;
			}
		}
	}

	// Token: 0x1700021C RID: 540
	// (get) Token: 0x06000978 RID: 2424 RVA: 0x00029CFA File Offset: 0x00027EFA
	public Type DataType
	{
		get
		{
			return this.getTypeFromName(this.typeName);
		}
	}

	// Token: 0x1700021D RID: 541
	// (get) Token: 0x06000979 RID: 2425 RVA: 0x00029D08 File Offset: 0x00027F08
	// (set) Token: 0x0600097A RID: 2426 RVA: 0x00029D10 File Offset: 0x00027F10
	public object Data
	{
		get
		{
			return this.data;
		}
		set
		{
			if (value != this.data)
			{
				this.data = value;
				if (value != null)
				{
					this.typeName = value.GetType().Name;
				}
				if (this.DataChanged != null)
				{
					this.DataChanged(value);
				}
			}
		}
	}

	// Token: 0x0600097B RID: 2427 RVA: 0x00029D4A File Offset: 0x00027F4A
	public void Start()
	{
		if (this.DataType == null)
		{
			Debug.LogError("Unable to retrieve System.Type reference for type: " + this.TypeName);
		}
	}

	// Token: 0x0600097C RID: 2428 RVA: 0x00029D70 File Offset: 0x00027F70
	public Type GetPropertyType(string propertyName)
	{
		Type dataType = this.DataType;
		if (dataType == null)
		{
			return null;
		}
		MemberInfo memberInfo = dataType.GetMember(propertyName, BindingFlags.Instance | BindingFlags.Public).FirstOrDefault<MemberInfo>();
		if (memberInfo is FieldInfo)
		{
			return ((FieldInfo)memberInfo).FieldType;
		}
		if (memberInfo is PropertyInfo)
		{
			return ((PropertyInfo)memberInfo).PropertyType;
		}
		return null;
	}

	// Token: 0x0600097D RID: 2429 RVA: 0x00029DC7 File Offset: 0x00027FC7
	public dfObservableProperty GetProperty(string PropertyName)
	{
		if (this.data == null)
		{
			return null;
		}
		return new dfObservableProperty(this.data, PropertyName);
	}

	// Token: 0x0600097E RID: 2430 RVA: 0x00029DE0 File Offset: 0x00027FE0
	private Type getTypeFromName(string nameOfType)
	{
		if (nameOfType == null)
		{
			throw new ArgumentNullException("nameOfType");
		}
		return base.GetType().GetAssembly().GetTypes().FirstOrDefault((Type t) => t.Name == nameOfType);
	}

	// Token: 0x0600097F RID: 2431 RVA: 0x00029E30 File Offset: 0x00028030
	private static Type getTypeFromQualifiedName(string typeName)
	{
		Type type = Type.GetType(typeName);
		if (type != null)
		{
			return type;
		}
		if (typeName.IndexOf('.') == -1)
		{
			return null;
		}
		Assembly assembly = Assembly.Load(new AssemblyName(typeName.Substring(0, typeName.IndexOf('.'))));
		if (assembly == null)
		{
			return null;
		}
		return assembly.GetType(typeName);
	}

	// Token: 0x06000980 RID: 2432 RVA: 0x00029E88 File Offset: 0x00028088
	public void Bind()
	{
	}

	// Token: 0x06000981 RID: 2433 RVA: 0x00029E8A File Offset: 0x0002808A
	public void Unbind()
	{
	}

	// Token: 0x04000483 RID: 1155
	[SerializeField]
	protected string typeName;

	// Token: 0x04000484 RID: 1156
	private object data;

	// Token: 0x0200037C RID: 892
	// (Invoke) Token: 0x06001CE6 RID: 7398
	[dfEventCategory("Data Changed")]
	public delegate void DataObjectChangedHandler(object data);
}

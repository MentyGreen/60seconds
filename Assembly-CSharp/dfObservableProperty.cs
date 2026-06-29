using System;
using System.Linq;
using System.Reflection;

// Token: 0x020000A7 RID: 167
public class dfObservableProperty : IObservableValue
{
	// Token: 0x060009C0 RID: 2496 RVA: 0x0002B05C File Offset: 0x0002925C
	internal dfObservableProperty(object target, string memberName)
	{
		MemberInfo memberInfo = target.GetType().GetMember(memberName, BindingFlags.Instance | BindingFlags.Public).FirstOrDefault<MemberInfo>();
		if (memberInfo == null)
		{
			throw new ArgumentException("Invalid property or field name: " + memberName, "memberName");
		}
		this.initMember(target, memberInfo);
	}

	// Token: 0x060009C1 RID: 2497 RVA: 0x0002B0AA File Offset: 0x000292AA
	internal dfObservableProperty(object target, FieldInfo field)
	{
		this.initField(target, field);
	}

	// Token: 0x060009C2 RID: 2498 RVA: 0x0002B0BA File Offset: 0x000292BA
	internal dfObservableProperty(object target, PropertyInfo property)
	{
		this.initProperty(target, property);
	}

	// Token: 0x060009C3 RID: 2499 RVA: 0x0002B0CA File Offset: 0x000292CA
	internal dfObservableProperty(object target, MemberInfo member)
	{
		this.initMember(target, member);
	}

	// Token: 0x17000221 RID: 545
	// (get) Token: 0x060009C4 RID: 2500 RVA: 0x0002B0DA File Offset: 0x000292DA
	public Type PropertyType
	{
		get
		{
			if (this.fieldInfo != null)
			{
				return this.fieldInfo.FieldType;
			}
			return this.propertyInfo.PropertyType;
		}
	}

	// Token: 0x17000222 RID: 546
	// (get) Token: 0x060009C5 RID: 2501 RVA: 0x0002B101 File Offset: 0x00029301
	// (set) Token: 0x060009C6 RID: 2502 RVA: 0x0002B109 File Offset: 0x00029309
	public object Value
	{
		get
		{
			return this.getter();
		}
		set
		{
			this.lastValue = value;
			this.setter(value);
			this.hasChanged = false;
		}
	}

	// Token: 0x17000223 RID: 547
	// (get) Token: 0x060009C7 RID: 2503 RVA: 0x0002B120 File Offset: 0x00029320
	public bool HasChanged
	{
		get
		{
			if (this.hasChanged)
			{
				return true;
			}
			object obj = this.getter();
			if (obj == this.lastValue)
			{
				this.hasChanged = false;
			}
			else if (obj == null || this.lastValue == null)
			{
				this.hasChanged = true;
			}
			else
			{
				this.hasChanged = !obj.Equals(this.lastValue);
			}
			return this.hasChanged;
		}
	}

	// Token: 0x060009C8 RID: 2504 RVA: 0x0002B17F File Offset: 0x0002937F
	public void ClearChangedFlag()
	{
		this.hasChanged = false;
		this.lastValue = this.getter();
	}

	// Token: 0x060009C9 RID: 2505 RVA: 0x0002B194 File Offset: 0x00029394
	private void initMember(object target, MemberInfo member)
	{
		if (member is FieldInfo)
		{
			this.initField(target, (FieldInfo)member);
			return;
		}
		this.initProperty(target, (PropertyInfo)member);
	}

	// Token: 0x060009CA RID: 2506 RVA: 0x0002B1B9 File Offset: 0x000293B9
	private void initField(object target, FieldInfo field)
	{
		this.target = target;
		this.fieldInfo = field;
		this.Value = this.getter();
	}

	// Token: 0x060009CB RID: 2507 RVA: 0x0002B1D8 File Offset: 0x000293D8
	private void initProperty(object target, PropertyInfo property)
	{
		this.target = target;
		this.propertyInfo = property;
		this.propertyGetter = property.GetGetMethod();
		this.propertySetter = property.GetSetMethod();
		this.canWrite = (this.propertySetter != null);
		this.Value = this.getter();
	}

	// Token: 0x060009CC RID: 2508 RVA: 0x0002B229 File Offset: 0x00029429
	private object getter()
	{
		if (this.propertyInfo != null)
		{
			return this.getPropertyValue();
		}
		return this.getFieldValue();
	}

	// Token: 0x060009CD RID: 2509 RVA: 0x0002B246 File Offset: 0x00029446
	private void setter(object value)
	{
		if (this.propertyInfo != null)
		{
			this.setPropertyValue(value);
			return;
		}
		this.setFieldValue(value);
	}

	// Token: 0x060009CE RID: 2510 RVA: 0x0002B265 File Offset: 0x00029465
	private object getPropertyValue()
	{
		return this.propertyGetter.Invoke(this.target, null);
	}

	// Token: 0x060009CF RID: 2511 RVA: 0x0002B27C File Offset: 0x0002947C
	private void setPropertyValue(object value)
	{
		if (!this.canWrite)
		{
			return;
		}
		if (this.propertyType == null)
		{
			this.propertyType = this.propertyInfo.PropertyType;
		}
		if (value == null || this.propertyType.IsAssignableFrom(value.GetType()))
		{
			dfObservableProperty.tempArray[0] = value;
		}
		else
		{
			dfObservableProperty.tempArray[0] = Convert.ChangeType(value, this.propertyType);
		}
		this.propertySetter.Invoke(this.target, dfObservableProperty.tempArray);
	}

	// Token: 0x060009D0 RID: 2512 RVA: 0x0002B2FC File Offset: 0x000294FC
	private void setFieldValue(object value)
	{
		if (this.fieldInfo.IsLiteral)
		{
			return;
		}
		if (this.propertyType == null)
		{
			this.propertyType = this.fieldInfo.FieldType;
		}
		if (value == null || this.propertyType.IsAssignableFrom(value.GetType()))
		{
			this.fieldInfo.SetValue(this.target, value);
			return;
		}
		object value2 = Convert.ChangeType(value, this.propertyType);
		this.fieldInfo.SetValue(this.target, value2);
	}

	// Token: 0x060009D1 RID: 2513 RVA: 0x0002B37E File Offset: 0x0002957E
	private void setFieldValueNOP(object value)
	{
	}

	// Token: 0x060009D2 RID: 2514 RVA: 0x0002B380 File Offset: 0x00029580
	private object getFieldValue()
	{
		return this.fieldInfo.GetValue(this.target);
	}

	// Token: 0x0400049B RID: 1179
	private static object[] tempArray = new object[1];

	// Token: 0x0400049C RID: 1180
	private object lastValue;

	// Token: 0x0400049D RID: 1181
	private bool hasChanged;

	// Token: 0x0400049E RID: 1182
	private object target;

	// Token: 0x0400049F RID: 1183
	private FieldInfo fieldInfo;

	// Token: 0x040004A0 RID: 1184
	private PropertyInfo propertyInfo;

	// Token: 0x040004A1 RID: 1185
	private MethodInfo propertyGetter;

	// Token: 0x040004A2 RID: 1186
	private MethodInfo propertySetter;

	// Token: 0x040004A3 RID: 1187
	private Type propertyType;

	// Token: 0x040004A4 RID: 1188
	private bool canWrite;

	// Token: 0x02000380 RID: 896
	// (Invoke) Token: 0x06001CF0 RID: 7408
	private delegate object ValueGetter();

	// Token: 0x02000381 RID: 897
	// (Invoke) Token: 0x06001CF4 RID: 7412
	private delegate void ValueSetter(object value);
}

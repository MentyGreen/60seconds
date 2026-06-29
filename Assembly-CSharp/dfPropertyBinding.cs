using System;
using UnityEngine;

// Token: 0x020000A8 RID: 168
[AddComponentMenu("Daikon Forge/Data Binding/Property Binding")]
[Serializable]
public class dfPropertyBinding : MonoBehaviour, IDataBindingComponent
{
	// Token: 0x17000224 RID: 548
	// (get) Token: 0x060009D4 RID: 2516 RVA: 0x0002B3A0 File Offset: 0x000295A0
	public bool IsBound
	{
		get
		{
			return this.isBound;
		}
	}

	// Token: 0x060009D5 RID: 2517 RVA: 0x0002B3A8 File Offset: 0x000295A8
	public virtual void OnEnable()
	{
		if (!this.AutoBind || this.DataSource == null || this.DataTarget == null)
		{
			return;
		}
		if (!this.isBound && this.DataSource.IsValid && this.DataTarget.IsValid)
		{
			this.Bind();
		}
	}

	// Token: 0x060009D6 RID: 2518 RVA: 0x0002B3F8 File Offset: 0x000295F8
	public virtual void Start()
	{
		if (!this.AutoBind || this.DataSource == null || this.DataTarget == null)
		{
			return;
		}
		if (!this.isBound && this.DataSource.IsValid && this.DataTarget.IsValid)
		{
			this.Bind();
		}
	}

	// Token: 0x060009D7 RID: 2519 RVA: 0x0002B446 File Offset: 0x00029646
	public virtual void OnDisable()
	{
		if (this.AutoUnbind)
		{
			this.Unbind();
		}
	}

	// Token: 0x060009D8 RID: 2520 RVA: 0x0002B456 File Offset: 0x00029656
	public virtual void OnDestroy()
	{
		this.Unbind();
	}

	// Token: 0x060009D9 RID: 2521 RVA: 0x0002B460 File Offset: 0x00029660
	public virtual void Update()
	{
		if (this.sourceProperty == null || this.targetProperty == null)
		{
			return;
		}
		if (this.sourceProperty.HasChanged)
		{
			this.targetProperty.Value = this.formatValue(this.sourceProperty.Value);
			this.sourceProperty.ClearChangedFlag();
			return;
		}
		if (this.TwoWay && this.targetProperty.HasChanged)
		{
			this.sourceProperty.Value = this.targetProperty.Value;
			this.targetProperty.ClearChangedFlag();
		}
	}

	// Token: 0x060009DA RID: 2522 RVA: 0x0002B4E9 File Offset: 0x000296E9
	public static dfPropertyBinding Bind(Component sourceComponent, string sourceProperty, Component targetComponent, string targetProperty)
	{
		return dfPropertyBinding.Bind(sourceComponent.gameObject, sourceComponent, sourceProperty, targetComponent, targetProperty);
	}

	// Token: 0x060009DB RID: 2523 RVA: 0x0002B4FC File Offset: 0x000296FC
	public static dfPropertyBinding Bind(GameObject hostObject, Component sourceComponent, string sourceProperty, Component targetComponent, string targetProperty)
	{
		if (hostObject == null)
		{
			throw new ArgumentNullException("hostObject");
		}
		if (sourceComponent == null)
		{
			throw new ArgumentNullException("sourceComponent");
		}
		if (targetComponent == null)
		{
			throw new ArgumentNullException("targetComponent");
		}
		if (string.IsNullOrEmpty(sourceProperty))
		{
			throw new ArgumentNullException("sourceProperty");
		}
		if (string.IsNullOrEmpty(targetProperty))
		{
			throw new ArgumentNullException("targetProperty");
		}
		dfPropertyBinding dfPropertyBinding = hostObject.AddComponent<dfPropertyBinding>();
		dfPropertyBinding.DataSource = new dfComponentMemberInfo
		{
			Component = sourceComponent,
			MemberName = sourceProperty
		};
		dfPropertyBinding.DataTarget = new dfComponentMemberInfo
		{
			Component = targetComponent,
			MemberName = targetProperty
		};
		dfPropertyBinding.Bind();
		return dfPropertyBinding;
	}

	// Token: 0x060009DC RID: 2524 RVA: 0x0002B5AC File Offset: 0x000297AC
	public virtual bool CanSynchronize()
	{
		return this.DataSource != null && this.DataTarget != null && (this.DataSource.IsValid || this.DataTarget.IsValid) && !(this.DataTarget.GetMemberType() != this.DataSource.GetMemberType());
	}

	// Token: 0x060009DD RID: 2525 RVA: 0x0002B608 File Offset: 0x00029808
	public virtual void Bind()
	{
		if (this.isBound)
		{
			return;
		}
		if (!this.DataSource.IsValid || !this.DataTarget.IsValid)
		{
			Debug.LogError(string.Format("Invalid data binding configuration - Source:{0}, Target:{1}", this.DataSource, this.DataTarget));
			return;
		}
		this.sourceProperty = this.DataSource.GetProperty();
		this.targetProperty = this.DataTarget.GetProperty();
		this.isBound = (this.sourceProperty != null && this.targetProperty != null);
		if (this.isBound)
		{
			if (this.targetProperty.PropertyType == typeof(string) && this.sourceProperty.PropertyType != typeof(string))
			{
				this.useFormatString = !string.IsNullOrEmpty(this.FormatString);
			}
			this.targetProperty.Value = this.formatValue(this.sourceProperty.Value);
		}
	}

	// Token: 0x060009DE RID: 2526 RVA: 0x0002B700 File Offset: 0x00029900
	public virtual void Unbind()
	{
		if (!this.isBound)
		{
			return;
		}
		this.sourceProperty = null;
		this.targetProperty = null;
		this.isBound = false;
	}

	// Token: 0x060009DF RID: 2527 RVA: 0x0002B720 File Offset: 0x00029920
	private object formatValue(object value)
	{
		try
		{
			if (this.useFormatString && !string.IsNullOrEmpty(this.FormatString))
			{
				return string.Format(this.FormatString, value);
			}
		}
		catch (FormatException message)
		{
			Debug.LogError(message, this);
			if (Application.isPlaying)
			{
				base.enabled = false;
			}
		}
		return value;
	}

	// Token: 0x060009E0 RID: 2528 RVA: 0x0002B77C File Offset: 0x0002997C
	public override string ToString()
	{
		string text = (this.DataSource != null && this.DataSource.Component != null) ? this.DataSource.Component.GetType().Name : "[null]";
		string text2 = (this.DataSource != null && !string.IsNullOrEmpty(this.DataSource.MemberName)) ? this.DataSource.MemberName : "[null]";
		string text3 = (this.DataTarget != null && this.DataTarget.Component != null) ? this.DataTarget.Component.GetType().Name : "[null]";
		string text4 = (this.DataTarget != null && !string.IsNullOrEmpty(this.DataTarget.MemberName)) ? this.DataTarget.MemberName : "[null]";
		return string.Format("Bind {0}.{1} -> {2}.{3}", new object[]
		{
			text,
			text2,
			text3,
			text4
		});
	}

	// Token: 0x040004A5 RID: 1189
	public dfComponentMemberInfo DataSource;

	// Token: 0x040004A6 RID: 1190
	public dfComponentMemberInfo DataTarget;

	// Token: 0x040004A7 RID: 1191
	public string FormatString;

	// Token: 0x040004A8 RID: 1192
	public bool TwoWay;

	// Token: 0x040004A9 RID: 1193
	public bool AutoBind = true;

	// Token: 0x040004AA RID: 1194
	public bool AutoUnbind = true;

	// Token: 0x040004AB RID: 1195
	protected dfObservableProperty sourceProperty;

	// Token: 0x040004AC RID: 1196
	protected dfObservableProperty targetProperty;

	// Token: 0x040004AD RID: 1197
	protected bool isBound;

	// Token: 0x040004AE RID: 1198
	protected bool useFormatString;
}

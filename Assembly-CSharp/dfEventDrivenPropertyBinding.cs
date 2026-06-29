using System;
using UnityEngine;

// Token: 0x020000A4 RID: 164
[AddComponentMenu("Daikon Forge/Data Binding/Event-Driven Property Binding")]
[Serializable]
public class dfEventDrivenPropertyBinding : dfPropertyBinding
{
	// Token: 0x060009A9 RID: 2473 RVA: 0x0002A7B9 File Offset: 0x000289B9
	public override void Update()
	{
	}

	// Token: 0x060009AA RID: 2474 RVA: 0x0002A7BB File Offset: 0x000289BB
	public static dfEventDrivenPropertyBinding Bind(Component sourceComponent, string sourceProperty, string sourceEvent, Component targetComponent, string targetProperty, string targetEvent)
	{
		return dfEventDrivenPropertyBinding.Bind(sourceComponent.gameObject, sourceComponent, sourceProperty, sourceEvent, targetComponent, targetProperty, targetEvent);
	}

	// Token: 0x060009AB RID: 2475 RVA: 0x0002A7D0 File Offset: 0x000289D0
	public static dfEventDrivenPropertyBinding Bind(GameObject hostObject, Component sourceComponent, string sourceProperty, string sourceEvent, Component targetComponent, string targetProperty, string targetEvent)
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
		if (string.IsNullOrEmpty(sourceEvent))
		{
			throw new ArgumentNullException("sourceEvent");
		}
		dfEventDrivenPropertyBinding dfEventDrivenPropertyBinding = hostObject.AddComponent<dfEventDrivenPropertyBinding>();
		dfEventDrivenPropertyBinding.DataSource = new dfComponentMemberInfo
		{
			Component = sourceComponent,
			MemberName = sourceProperty
		};
		dfEventDrivenPropertyBinding.DataTarget = new dfComponentMemberInfo
		{
			Component = targetComponent,
			MemberName = targetProperty
		};
		dfEventDrivenPropertyBinding.SourceEventName = sourceEvent;
		dfEventDrivenPropertyBinding.TargetEventName = targetEvent;
		dfEventDrivenPropertyBinding.Bind();
		return dfEventDrivenPropertyBinding;
	}

	// Token: 0x060009AC RID: 2476 RVA: 0x0002A8A4 File Offset: 0x00028AA4
	public override void Bind()
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
		if (this.sourceProperty != null && this.targetProperty != null)
		{
			if (!string.IsNullOrEmpty(this.SourceEventName) && this.SourceEventName.Trim() != "")
			{
				this.bindSourceEvent();
			}
			if (!string.IsNullOrEmpty(this.TargetEventName) && this.TargetEventName.Trim() != "")
			{
				this.bindTargetEvent();
			}
			else if (this.targetProperty.PropertyType == typeof(string) && this.sourceProperty.PropertyType != typeof(string))
			{
				this.useFormatString = !string.IsNullOrEmpty(this.FormatString);
			}
			this.MirrorSourceProperty();
			this.isBound = (this.sourceEventBinding != null);
		}
	}

	// Token: 0x060009AD RID: 2477 RVA: 0x0002A9E4 File Offset: 0x00028BE4
	public override void Unbind()
	{
		if (!this.isBound)
		{
			return;
		}
		this.isBound = false;
		if (this.sourceEventBinding != null)
		{
			this.sourceEventBinding.Unbind();
			Object.Destroy(this.sourceEventBinding);
			this.sourceEventBinding = null;
		}
		if (this.targetEventBinding != null)
		{
			this.targetEventBinding.Unbind();
			Object.Destroy(this.targetEventBinding);
			this.targetEventBinding = null;
		}
	}

	// Token: 0x060009AE RID: 2478 RVA: 0x0002AA57 File Offset: 0x00028C57
	public void MirrorSourceProperty()
	{
		this.targetProperty.Value = this.formatValue(this.sourceProperty.Value);
	}

	// Token: 0x060009AF RID: 2479 RVA: 0x0002AA75 File Offset: 0x00028C75
	public void MirrorTargetProperty()
	{
		this.sourceProperty.Value = this.targetProperty.Value;
	}

	// Token: 0x060009B0 RID: 2480 RVA: 0x0002AA90 File Offset: 0x00028C90
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

	// Token: 0x060009B1 RID: 2481 RVA: 0x0002AAEC File Offset: 0x00028CEC
	private void bindSourceEvent()
	{
		this.sourceEventBinding = base.gameObject.AddComponent<dfEventBinding>();
		this.sourceEventBinding.hideFlags = (HideFlags.HideInHierarchy | HideFlags.HideInInspector | HideFlags.DontSaveInEditor | HideFlags.NotEditable | HideFlags.DontSaveInBuild | HideFlags.DontUnloadUnusedAsset);
		this.sourceEventBinding.DataSource = new dfComponentMemberInfo
		{
			Component = this.DataSource.Component,
			MemberName = this.SourceEventName
		};
		this.sourceEventBinding.DataTarget = new dfComponentMemberInfo
		{
			Component = this,
			MemberName = "MirrorSourceProperty"
		};
		this.sourceEventBinding.Bind();
	}

	// Token: 0x060009B2 RID: 2482 RVA: 0x0002AB74 File Offset: 0x00028D74
	private void bindTargetEvent()
	{
		this.targetEventBinding = base.gameObject.AddComponent<dfEventBinding>();
		this.targetEventBinding.hideFlags = (HideFlags.HideInHierarchy | HideFlags.HideInInspector | HideFlags.DontSaveInEditor | HideFlags.NotEditable | HideFlags.DontSaveInBuild | HideFlags.DontUnloadUnusedAsset);
		this.targetEventBinding.DataSource = new dfComponentMemberInfo
		{
			Component = this.DataTarget.Component,
			MemberName = this.TargetEventName
		};
		this.targetEventBinding.DataTarget = new dfComponentMemberInfo
		{
			Component = this,
			MemberName = "MirrorTargetProperty"
		};
		this.targetEventBinding.Bind();
	}

	// Token: 0x060009B3 RID: 2483 RVA: 0x0002ABFC File Offset: 0x00028DFC
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

	// Token: 0x04000491 RID: 1169
	public string SourceEventName;

	// Token: 0x04000492 RID: 1170
	public string TargetEventName;

	// Token: 0x04000493 RID: 1171
	protected dfEventBinding sourceEventBinding;

	// Token: 0x04000494 RID: 1172
	protected dfEventBinding targetEventBinding;
}

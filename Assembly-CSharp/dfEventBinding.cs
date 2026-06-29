using System;
using System.Collections;
using System.Linq;
using System.Reflection;
using UnityEngine;

// Token: 0x020000A3 RID: 163
[AddComponentMenu("Daikon Forge/Data Binding/Event Binding")]
[Serializable]
public class dfEventBinding : MonoBehaviour, IDataBindingComponent
{
	// Token: 0x1700021E RID: 542
	// (get) Token: 0x06000983 RID: 2435 RVA: 0x00029E94 File Offset: 0x00028094
	public bool IsBound
	{
		get
		{
			return this.isBound;
		}
	}

	// Token: 0x06000984 RID: 2436 RVA: 0x00029E9C File Offset: 0x0002809C
	public void OnEnable()
	{
		if (this.AutoBind && this.DataSource != null && !this.isBound && this.DataSource.IsValid && this.DataTarget.IsValid)
		{
			this.Bind();
		}
	}

	// Token: 0x06000985 RID: 2437 RVA: 0x00029ED6 File Offset: 0x000280D6
	public void Start()
	{
		if (this.AutoBind && this.DataSource != null && !this.isBound && this.DataSource.IsValid && this.DataTarget.IsValid)
		{
			this.Bind();
		}
	}

	// Token: 0x06000986 RID: 2438 RVA: 0x00029F10 File Offset: 0x00028110
	public void OnDisable()
	{
		if (this.AutoUnbind)
		{
			this.Unbind();
		}
	}

	// Token: 0x06000987 RID: 2439 RVA: 0x00029F20 File Offset: 0x00028120
	public void OnDestroy()
	{
		this.Unbind();
	}

	// Token: 0x06000988 RID: 2440 RVA: 0x00029F28 File Offset: 0x00028128
	public void Bind()
	{
		if (this.isBound || this.DataSource == null)
		{
			return;
		}
		if (!this.DataSource.IsValid || !this.DataTarget.IsValid)
		{
			Debug.LogError(string.Format("Invalid event binding configuration - Source:{0}, Target:{1}", this.DataSource, this.DataTarget));
			return;
		}
		this.sourceComponent = this.DataSource.Component;
		this.targetComponent = this.DataTarget.Component;
		MethodInfo method = this.DataTarget.GetMethod();
		if (method == null)
		{
			Debug.LogError("Event handler not found: " + this.targetComponent.GetType().Name + "." + this.DataTarget.MemberName);
			return;
		}
		if (this.bindToEventProperty(method))
		{
			this.isBound = true;
			return;
		}
		if (this.bindToEventField(method))
		{
			this.isBound = true;
			return;
		}
	}

	// Token: 0x06000989 RID: 2441 RVA: 0x0002A008 File Offset: 0x00028208
	public void Unbind()
	{
		if (!this.isBound)
		{
			return;
		}
		this.isBound = false;
		if (this.eventField != null)
		{
			Delegate value = Delegate.Remove((Delegate)this.eventField.GetValue(this.sourceComponent), this.eventDelegate);
			this.eventField.SetValue(this.sourceComponent, value);
		}
		else if (this.eventInfo != null)
		{
			this.eventInfo.GetRemoveMethod().Invoke(this.sourceComponent, new object[]
			{
				this.eventDelegate
			});
		}
		this.eventInfo = null;
		this.eventField = null;
		this.eventDelegate = null;
		this.handlerProxy = null;
		this.sourceComponent = null;
		this.targetComponent = null;
	}

	// Token: 0x0600098A RID: 2442 RVA: 0x0002A0C8 File Offset: 0x000282C8
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

	// Token: 0x0600098B RID: 2443 RVA: 0x0002A1BF File Offset: 0x000283BF
	[HideInInspector]
	[dfEventProxy]
	public void NotificationEventProxy()
	{
		this.callProxyEventHandler(Array.Empty<object>());
	}

	// Token: 0x0600098C RID: 2444 RVA: 0x0002A1CC File Offset: 0x000283CC
	[HideInInspector]
	[dfEventProxy]
	public void GenericCallbackProxy(object sender)
	{
		this.callProxyEventHandler(new object[]
		{
			sender
		});
	}

	// Token: 0x0600098D RID: 2445 RVA: 0x0002A1DE File Offset: 0x000283DE
	[HideInInspector]
	[dfEventProxy]
	public void AnimationEventProxy(dfTweenPlayableBase tween)
	{
		this.callProxyEventHandler(new object[]
		{
			tween
		});
	}

	// Token: 0x0600098E RID: 2446 RVA: 0x0002A1F0 File Offset: 0x000283F0
	[HideInInspector]
	[dfEventProxy]
	public void MouseEventProxy(dfControl control, dfMouseEventArgs mouseEvent)
	{
		this.callProxyEventHandler(new object[]
		{
			control,
			mouseEvent
		});
	}

	// Token: 0x0600098F RID: 2447 RVA: 0x0002A206 File Offset: 0x00028406
	[HideInInspector]
	[dfEventProxy]
	public void KeyEventProxy(dfControl control, dfKeyEventArgs keyEvent)
	{
		this.callProxyEventHandler(new object[]
		{
			control,
			keyEvent
		});
	}

	// Token: 0x06000990 RID: 2448 RVA: 0x0002A21C File Offset: 0x0002841C
	[HideInInspector]
	[dfEventProxy]
	public void DragEventProxy(dfControl control, dfDragEventArgs dragEvent)
	{
		this.callProxyEventHandler(new object[]
		{
			control,
			dragEvent
		});
	}

	// Token: 0x06000991 RID: 2449 RVA: 0x0002A232 File Offset: 0x00028432
	[HideInInspector]
	[dfEventProxy]
	public void ChildControlEventProxy(dfControl container, dfControl child)
	{
		this.callProxyEventHandler(new object[]
		{
			container,
			child
		});
	}

	// Token: 0x06000992 RID: 2450 RVA: 0x0002A248 File Offset: 0x00028448
	[HideInInspector]
	[dfEventProxy]
	public void FocusEventProxy(dfControl control, dfFocusEventArgs args)
	{
		this.callProxyEventHandler(new object[]
		{
			control,
			args
		});
	}

	// Token: 0x06000993 RID: 2451 RVA: 0x0002A25E File Offset: 0x0002845E
	[HideInInspector]
	[dfEventProxy]
	public void PropertyChangedProxy(dfControl control, int value)
	{
		this.callProxyEventHandler(new object[]
		{
			control,
			value
		});
	}

	// Token: 0x06000994 RID: 2452 RVA: 0x0002A279 File Offset: 0x00028479
	[HideInInspector]
	[dfEventProxy]
	public void PropertyChangedProxy(dfControl control, float value)
	{
		this.callProxyEventHandler(new object[]
		{
			control,
			value
		});
	}

	// Token: 0x06000995 RID: 2453 RVA: 0x0002A294 File Offset: 0x00028494
	[HideInInspector]
	[dfEventProxy]
	public void PropertyChangedProxy(dfControl control, bool value)
	{
		this.callProxyEventHandler(new object[]
		{
			control,
			value
		});
	}

	// Token: 0x06000996 RID: 2454 RVA: 0x0002A2AF File Offset: 0x000284AF
	[HideInInspector]
	[dfEventProxy]
	public void PropertyChangedProxy(dfControl control, string value)
	{
		this.callProxyEventHandler(new object[]
		{
			control,
			value
		});
	}

	// Token: 0x06000997 RID: 2455 RVA: 0x0002A2C5 File Offset: 0x000284C5
	[HideInInspector]
	[dfEventProxy]
	public void PropertyChangedProxy(dfControl control, Vector2 value)
	{
		this.callProxyEventHandler(new object[]
		{
			control,
			value
		});
	}

	// Token: 0x06000998 RID: 2456 RVA: 0x0002A2E0 File Offset: 0x000284E0
	[HideInInspector]
	[dfEventProxy]
	public void PropertyChangedProxy(dfControl control, Vector3 value)
	{
		this.callProxyEventHandler(new object[]
		{
			control,
			value
		});
	}

	// Token: 0x06000999 RID: 2457 RVA: 0x0002A2FB File Offset: 0x000284FB
	[HideInInspector]
	[dfEventProxy]
	public void PropertyChangedProxy(dfControl control, Vector4 value)
	{
		this.callProxyEventHandler(new object[]
		{
			control,
			value
		});
	}

	// Token: 0x0600099A RID: 2458 RVA: 0x0002A316 File Offset: 0x00028516
	[HideInInspector]
	[dfEventProxy]
	public void PropertyChangedProxy(dfControl control, Quaternion value)
	{
		this.callProxyEventHandler(new object[]
		{
			control,
			value
		});
	}

	// Token: 0x0600099B RID: 2459 RVA: 0x0002A331 File Offset: 0x00028531
	[HideInInspector]
	[dfEventProxy]
	public void PropertyChangedProxy(dfControl control, dfButton.ButtonState value)
	{
		this.callProxyEventHandler(new object[]
		{
			control,
			value
		});
	}

	// Token: 0x0600099C RID: 2460 RVA: 0x0002A34C File Offset: 0x0002854C
	[HideInInspector]
	[dfEventProxy]
	public void PropertyChangedProxy(dfControl control, dfPivotPoint value)
	{
		this.callProxyEventHandler(new object[]
		{
			control,
			value
		});
	}

	// Token: 0x0600099D RID: 2461 RVA: 0x0002A367 File Offset: 0x00028567
	[HideInInspector]
	[dfEventProxy]
	public void PropertyChangedProxy(dfControl control, Texture value)
	{
		this.callProxyEventHandler(new object[]
		{
			control,
			value
		});
	}

	// Token: 0x0600099E RID: 2462 RVA: 0x0002A37D File Offset: 0x0002857D
	[HideInInspector]
	[dfEventProxy]
	public void PropertyChangedProxy(dfControl control, Texture2D value)
	{
		this.callProxyEventHandler(new object[]
		{
			control,
			value
		});
	}

	// Token: 0x0600099F RID: 2463 RVA: 0x0002A393 File Offset: 0x00028593
	[HideInInspector]
	[dfEventProxy]
	public void PropertyChangedProxy(dfControl control, Material value)
	{
		this.callProxyEventHandler(new object[]
		{
			control,
			value
		});
	}

	// Token: 0x060009A0 RID: 2464 RVA: 0x0002A3A9 File Offset: 0x000285A9
	[HideInInspector]
	[dfEventProxy]
	public void SystemEventHandlerProxy(object sender, EventArgs args)
	{
		this.callProxyEventHandler(new object[]
		{
			sender,
			args
		});
	}

	// Token: 0x060009A1 RID: 2465 RVA: 0x0002A3C0 File Offset: 0x000285C0
	private bool bindToEventField(MethodInfo eventHandler)
	{
		this.eventField = dfEventBinding.getField(this.sourceComponent, this.DataSource.MemberName);
		if (this.eventField == null)
		{
			return false;
		}
		try
		{
			MethodInfo method = this.eventField.FieldType.GetMethod("Invoke");
			ParameterInfo[] parameters = method.GetParameters();
			ParameterInfo[] parameters2 = eventHandler.GetParameters();
			if (parameters.Length == parameters2.Length && !(method.ReturnType != eventHandler.ReturnType))
			{
				this.eventDelegate = Delegate.CreateDelegate(this.eventField.FieldType, this.targetComponent, eventHandler, true);
			}
			else
			{
				this.eventDelegate = this.createEventProxyDelegate(this.targetComponent, this.eventField.FieldType, parameters, eventHandler);
			}
			Delegate value = Delegate.Combine(this.eventDelegate, (Delegate)this.eventField.GetValue(this.sourceComponent));
			this.eventField.SetValue(this.sourceComponent, value);
		}
		catch (Exception ex)
		{
			base.enabled = false;
			Debug.LogError(string.Format("Event binding failed - Failed to create event handler for {0} ({1}) - {2}", this.DataSource, eventHandler, ex.ToString()), this);
			return false;
		}
		return true;
	}

	// Token: 0x060009A2 RID: 2466 RVA: 0x0002A4F4 File Offset: 0x000286F4
	private bool bindToEventProperty(MethodInfo eventHandler)
	{
		this.eventInfo = this.sourceComponent.GetType().GetEvent(this.DataSource.MemberName);
		if (this.eventInfo == null)
		{
			return false;
		}
		try
		{
			Type eventHandlerType = this.eventInfo.EventHandlerType;
			MethodBase addMethod = this.eventInfo.GetAddMethod();
			MethodInfo method = eventHandlerType.GetMethod("Invoke");
			ParameterInfo[] parameters = method.GetParameters();
			ParameterInfo[] parameters2 = eventHandler.GetParameters();
			if (parameters.Length == parameters2.Length && !(method.ReturnType != eventHandler.ReturnType))
			{
				this.eventDelegate = Delegate.CreateDelegate(eventHandlerType, this.targetComponent, eventHandler, true);
			}
			else
			{
				this.eventDelegate = this.createEventProxyDelegate(this.targetComponent, eventHandlerType, parameters, eventHandler);
			}
			addMethod.Invoke(this.DataSource.Component, new object[]
			{
				this.eventDelegate
			});
		}
		catch (Exception ex)
		{
			base.enabled = false;
			Debug.LogError(string.Format("Event binding failed - Failed to create event handler for {0} ({1}) - {2}", this.DataSource, eventHandler, ex.ToString()), this);
			return false;
		}
		return true;
	}

	// Token: 0x060009A3 RID: 2467 RVA: 0x0002A610 File Offset: 0x00028810
	private void callProxyEventHandler(params object[] arguments)
	{
		if (this.handlerProxy == null)
		{
			return;
		}
		if (this.handlerParameters.Length == 0)
		{
			arguments = null;
		}
		object obj = this.handlerProxy.Invoke(this.targetComponent, arguments);
		if (!(obj is IEnumerator))
		{
			return;
		}
		if (this.targetComponent is MonoBehaviour)
		{
			((MonoBehaviour)this.targetComponent).StartCoroutine((IEnumerator)obj);
		}
	}

	// Token: 0x060009A4 RID: 2468 RVA: 0x0002A678 File Offset: 0x00028878
	private static FieldInfo getField(Component component, string fieldName)
	{
		if (component == null)
		{
			throw new ArgumentNullException("component");
		}
		return component.GetType().GetAllFields().FirstOrDefault((FieldInfo f) => f.Name == fieldName);
	}

	// Token: 0x060009A5 RID: 2469 RVA: 0x0002A6C4 File Offset: 0x000288C4
	private Delegate createEventProxyDelegate(object target, Type delegateType, ParameterInfo[] eventParams, MethodInfo eventHandler)
	{
		MethodInfo methodInfo = (from m in typeof(dfEventBinding).GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
		where m.IsDefined(typeof(dfEventProxyAttribute), true) && this.signatureIsCompatible(eventParams, m.GetParameters())
		select m).FirstOrDefault<MethodInfo>();
		if (methodInfo == null)
		{
			return null;
		}
		this.handlerProxy = eventHandler;
		this.handlerParameters = eventHandler.GetParameters();
		return Delegate.CreateDelegate(delegateType, this, methodInfo, true);
	}

	// Token: 0x060009A6 RID: 2470 RVA: 0x0002A738 File Offset: 0x00028938
	private bool signatureIsCompatible(ParameterInfo[] lhs, ParameterInfo[] rhs)
	{
		if (lhs == null || rhs == null)
		{
			return false;
		}
		if (lhs.Length != rhs.Length)
		{
			return false;
		}
		for (int i = 0; i < lhs.Length; i++)
		{
			if (!this.areTypesCompatible(lhs[i], rhs[i]))
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x060009A7 RID: 2471 RVA: 0x0002A776 File Offset: 0x00028976
	private bool areTypesCompatible(ParameterInfo lhs, ParameterInfo rhs)
	{
		return lhs.ParameterType.Equals(rhs.ParameterType) || lhs.ParameterType.IsAssignableFrom(rhs.ParameterType);
	}

	// Token: 0x04000485 RID: 1157
	public dfComponentMemberInfo DataSource;

	// Token: 0x04000486 RID: 1158
	public dfComponentMemberInfo DataTarget;

	// Token: 0x04000487 RID: 1159
	public bool AutoBind = true;

	// Token: 0x04000488 RID: 1160
	public bool AutoUnbind = true;

	// Token: 0x04000489 RID: 1161
	private bool isBound;

	// Token: 0x0400048A RID: 1162
	private Component sourceComponent;

	// Token: 0x0400048B RID: 1163
	private Component targetComponent;

	// Token: 0x0400048C RID: 1164
	private EventInfo eventInfo;

	// Token: 0x0400048D RID: 1165
	private FieldInfo eventField;

	// Token: 0x0400048E RID: 1166
	private Delegate eventDelegate;

	// Token: 0x0400048F RID: 1167
	private MethodInfo handlerProxy;

	// Token: 0x04000490 RID: 1168
	private ParameterInfo[] handlerParameters;
}

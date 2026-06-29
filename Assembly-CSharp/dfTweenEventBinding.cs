using System;
using System.Linq;
using System.Reflection;
using UnityEngine;

// Token: 0x020000CE RID: 206
[AddComponentMenu("Daikon Forge/Tweens/Tween Event Binding")]
[Serializable]
public class dfTweenEventBinding : MonoBehaviour
{
	// Token: 0x06000B05 RID: 2821 RVA: 0x0002F1B2 File Offset: 0x0002D3B2
	private void OnEnable()
	{
		if (this.isValid())
		{
			this.Bind();
		}
	}

	// Token: 0x06000B06 RID: 2822 RVA: 0x0002F1C2 File Offset: 0x0002D3C2
	private void Start()
	{
		if (this.isValid())
		{
			this.Bind();
		}
	}

	// Token: 0x06000B07 RID: 2823 RVA: 0x0002F1D2 File Offset: 0x0002D3D2
	private void OnDisable()
	{
		this.Unbind();
	}

	// Token: 0x06000B08 RID: 2824 RVA: 0x0002F1DC File Offset: 0x0002D3DC
	public void Bind()
	{
		if (this.isBound && !this.isValid())
		{
			return;
		}
		this.isBound = true;
		if (!string.IsNullOrEmpty(this.StartEvent))
		{
			this.startEventBinding = this.bindEvent(this.StartEvent, "Play");
		}
		if (!string.IsNullOrEmpty(this.StopEvent))
		{
			this.stopEventBinding = this.bindEvent(this.StopEvent, "Stop");
		}
		if (!string.IsNullOrEmpty(this.ResetEvent))
		{
			this.resetEventBinding = this.bindEvent(this.ResetEvent, "Reset");
		}
	}

	// Token: 0x06000B09 RID: 2825 RVA: 0x0002F270 File Offset: 0x0002D470
	public void Unbind()
	{
		if (!this.isBound)
		{
			return;
		}
		this.isBound = false;
		if (this.startEventBinding != null)
		{
			this.startEventBinding.Unbind();
			this.startEventBinding = null;
		}
		if (this.stopEventBinding != null)
		{
			this.stopEventBinding.Unbind();
			this.stopEventBinding = null;
		}
		if (this.resetEventBinding != null)
		{
			this.resetEventBinding.Unbind();
			this.resetEventBinding = null;
		}
	}

	// Token: 0x06000B0A RID: 2826 RVA: 0x0002F2F0 File Offset: 0x0002D4F0
	private bool isValid()
	{
		if (this.Tween == null || !(this.Tween is dfTweenComponentBase))
		{
			return false;
		}
		if (this.EventSource == null)
		{
			return false;
		}
		if (string.IsNullOrEmpty(this.StartEvent) && string.IsNullOrEmpty(this.StopEvent) && string.IsNullOrEmpty(this.ResetEvent))
		{
			return false;
		}
		Type type = this.EventSource.GetType();
		return (string.IsNullOrEmpty(this.StartEvent) || !(this.getField(type, this.StartEvent) == null)) && (string.IsNullOrEmpty(this.StopEvent) || !(this.getField(type, this.StopEvent) == null)) && (string.IsNullOrEmpty(this.ResetEvent) || !(this.getField(type, this.ResetEvent) == null));
	}

	// Token: 0x06000B0B RID: 2827 RVA: 0x0002F3D0 File Offset: 0x0002D5D0
	private FieldInfo getField(Type type, string fieldName)
	{
		return (from f in type.GetAllFields()
		where f.Name == fieldName
		select f).FirstOrDefault<FieldInfo>();
	}

	// Token: 0x06000B0C RID: 2828 RVA: 0x0002F408 File Offset: 0x0002D608
	private void unbindEvent(FieldInfo eventField, Delegate eventDelegate)
	{
		Delegate value = Delegate.Remove((Delegate)eventField.GetValue(this.EventSource), eventDelegate);
		eventField.SetValue(this.EventSource, value);
	}

	// Token: 0x06000B0D RID: 2829 RVA: 0x0002F43C File Offset: 0x0002D63C
	private dfEventBinding bindEvent(string eventName, string handlerName)
	{
		if (this.Tween.GetType().GetMethod(handlerName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic) == null)
		{
			throw new MissingMemberException("Method not found: " + handlerName);
		}
		dfEventBinding dfEventBinding = base.gameObject.AddComponent<dfEventBinding>();
		dfEventBinding.hideFlags = (HideFlags.HideInHierarchy | HideFlags.HideInInspector | HideFlags.DontSaveInEditor | HideFlags.NotEditable | HideFlags.DontSaveInBuild | HideFlags.DontUnloadUnusedAsset);
		dfEventBinding.DataSource = new dfComponentMemberInfo
		{
			Component = this.EventSource,
			MemberName = eventName
		};
		dfEventBinding.DataTarget = new dfComponentMemberInfo
		{
			Component = this.Tween,
			MemberName = handlerName
		};
		dfEventBinding.Bind();
		return dfEventBinding;
	}

	// Token: 0x0400054F RID: 1359
	public Component Tween;

	// Token: 0x04000550 RID: 1360
	public Component EventSource;

	// Token: 0x04000551 RID: 1361
	public string StartEvent;

	// Token: 0x04000552 RID: 1362
	public string StopEvent;

	// Token: 0x04000553 RID: 1363
	public string ResetEvent;

	// Token: 0x04000554 RID: 1364
	private bool isBound;

	// Token: 0x04000555 RID: 1365
	private dfEventBinding startEventBinding;

	// Token: 0x04000556 RID: 1366
	private dfEventBinding stopEventBinding;

	// Token: 0x04000557 RID: 1367
	private dfEventBinding resetEventBinding;
}

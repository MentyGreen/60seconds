using System;
using UnityEngine;

// Token: 0x020000A9 RID: 169
[AddComponentMenu("Daikon Forge/Data Binding/Proxy Property Binding")]
[Serializable]
public class dfProxyPropertyBinding : MonoBehaviour, IDataBindingComponent
{
	// Token: 0x17000225 RID: 549
	// (get) Token: 0x060009E2 RID: 2530 RVA: 0x0002B889 File Offset: 0x00029A89
	public bool IsBound
	{
		get
		{
			return this.isBound;
		}
	}

	// Token: 0x060009E3 RID: 2531 RVA: 0x0002B891 File Offset: 0x00029A91
	public void Awake()
	{
	}

	// Token: 0x060009E4 RID: 2532 RVA: 0x0002B893 File Offset: 0x00029A93
	public void OnEnable()
	{
		if (!this.isBound && this.IsDataSourceValid() && this.DataTarget.IsValid)
		{
			this.Bind();
		}
	}

	// Token: 0x060009E5 RID: 2533 RVA: 0x0002B8B8 File Offset: 0x00029AB8
	public void Start()
	{
		if (!this.isBound && this.IsDataSourceValid() && this.DataTarget.IsValid)
		{
			this.Bind();
		}
	}

	// Token: 0x060009E6 RID: 2534 RVA: 0x0002B8DD File Offset: 0x00029ADD
	public void OnDisable()
	{
		this.Unbind();
	}

	// Token: 0x060009E7 RID: 2535 RVA: 0x0002B8E8 File Offset: 0x00029AE8
	public void Update()
	{
		if (this.sourceProperty == null || this.targetProperty == null)
		{
			return;
		}
		if (this.sourceProperty.HasChanged)
		{
			this.targetProperty.Value = this.sourceProperty.Value;
			this.sourceProperty.ClearChangedFlag();
			return;
		}
		if (this.TwoWay && this.targetProperty.HasChanged)
		{
			this.sourceProperty.Value = this.targetProperty.Value;
			this.targetProperty.ClearChangedFlag();
		}
	}

	// Token: 0x060009E8 RID: 2536 RVA: 0x0002B96C File Offset: 0x00029B6C
	public void Bind()
	{
		if (this.isBound)
		{
			return;
		}
		if (!this.IsDataSourceValid())
		{
			Debug.LogError(string.Format("Invalid data binding configuration - Source:{0}, Target:{1}", this.DataSource, this.DataTarget));
			return;
		}
		if (!this.DataTarget.IsValid)
		{
			Debug.LogError(string.Format("Invalid data binding configuration - Source:{0}, Target:{1}", this.DataSource, this.DataTarget));
			return;
		}
		dfDataObjectProxy dfDataObjectProxy = this.DataSource.Component as dfDataObjectProxy;
		this.sourceProperty = dfDataObjectProxy.GetProperty(this.DataSource.MemberName);
		this.targetProperty = this.DataTarget.GetProperty();
		this.isBound = (this.sourceProperty != null && this.targetProperty != null);
		if (this.isBound)
		{
			this.targetProperty.Value = this.sourceProperty.Value;
		}
		this.attachEvent();
	}

	// Token: 0x060009E9 RID: 2537 RVA: 0x0002BA46 File Offset: 0x00029C46
	public void Unbind()
	{
		if (!this.isBound)
		{
			return;
		}
		this.detachEvent();
		this.sourceProperty = null;
		this.targetProperty = null;
		this.isBound = false;
	}

	// Token: 0x060009EA RID: 2538 RVA: 0x0002BA6C File Offset: 0x00029C6C
	private bool IsDataSourceValid()
	{
		return this.DataSource != null || this.DataSource.Component != null || !string.IsNullOrEmpty(this.DataSource.MemberName) || (this.DataSource.Component as dfDataObjectProxy).Data != null;
	}

	// Token: 0x060009EB RID: 2539 RVA: 0x0002BAC0 File Offset: 0x00029CC0
	private void attachEvent()
	{
		if (this.eventsAttached)
		{
			return;
		}
		this.eventsAttached = true;
		dfDataObjectProxy dfDataObjectProxy = this.DataSource.Component as dfDataObjectProxy;
		if (dfDataObjectProxy != null)
		{
			dfDataObjectProxy.DataChanged += this.handle_DataChanged;
		}
	}

	// Token: 0x060009EC RID: 2540 RVA: 0x0002BB0C File Offset: 0x00029D0C
	private void detachEvent()
	{
		if (!this.eventsAttached)
		{
			return;
		}
		this.eventsAttached = false;
		dfDataObjectProxy dfDataObjectProxy = this.DataSource.Component as dfDataObjectProxy;
		if (dfDataObjectProxy != null)
		{
			dfDataObjectProxy.DataChanged -= this.handle_DataChanged;
		}
	}

	// Token: 0x060009ED RID: 2541 RVA: 0x0002BB55 File Offset: 0x00029D55
	private void handle_DataChanged(object data)
	{
		this.Unbind();
		if (this.IsDataSourceValid())
		{
			this.Bind();
		}
	}

	// Token: 0x060009EE RID: 2542 RVA: 0x0002BB6C File Offset: 0x00029D6C
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

	// Token: 0x040004AF RID: 1199
	public dfComponentMemberInfo DataSource;

	// Token: 0x040004B0 RID: 1200
	public dfComponentMemberInfo DataTarget;

	// Token: 0x040004B1 RID: 1201
	public bool TwoWay;

	// Token: 0x040004B2 RID: 1202
	private dfObservableProperty sourceProperty;

	// Token: 0x040004B3 RID: 1203
	private dfObservableProperty targetProperty;

	// Token: 0x040004B4 RID: 1204
	private bool isBound;

	// Token: 0x040004B5 RID: 1205
	private bool eventsAttached;
}

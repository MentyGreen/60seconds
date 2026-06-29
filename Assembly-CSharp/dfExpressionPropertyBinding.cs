using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000A6 RID: 166
[Obsolete("The expression binding functionality is no longer supported and may be removed in future versions of DFGUI")]
[Serializable]
public class dfExpressionPropertyBinding : MonoBehaviour, IDataBindingComponent
{
	// Token: 0x1700021F RID: 543
	// (get) Token: 0x060009B6 RID: 2486 RVA: 0x0002AD03 File Offset: 0x00028F03
	public bool IsBound
	{
		get
		{
			return this.isBound;
		}
	}

	// Token: 0x17000220 RID: 544
	// (get) Token: 0x060009B7 RID: 2487 RVA: 0x0002AD0B File Offset: 0x00028F0B
	// (set) Token: 0x060009B8 RID: 2488 RVA: 0x0002AD13 File Offset: 0x00028F13
	public string Expression
	{
		get
		{
			return this.expression;
		}
		set
		{
			if (!string.Equals(value, this.expression))
			{
				this.Unbind();
				this.expression = value;
			}
		}
	}

	// Token: 0x060009B9 RID: 2489 RVA: 0x0002AD30 File Offset: 0x00028F30
	public void OnDisable()
	{
		this.Unbind();
	}

	// Token: 0x060009BA RID: 2490 RVA: 0x0002AD38 File Offset: 0x00028F38
	public void Update()
	{
		if (this.isBound)
		{
			this.evaluate();
			return;
		}
		if (this.DataSource != null && !string.IsNullOrEmpty(this.expression) && this.DataTarget.IsValid)
		{
			this.Bind();
		}
	}

	// Token: 0x060009BB RID: 2491 RVA: 0x0002AD85 File Offset: 0x00028F85
	public void Unbind()
	{
		if (!this.isBound)
		{
			return;
		}
		this.compiledExpression = null;
		this.targetProperty = null;
		this.isBound = false;
	}

	// Token: 0x060009BC RID: 2492 RVA: 0x0002ADA8 File Offset: 0x00028FA8
	public void Bind()
	{
		if (this.isBound)
		{
			return;
		}
		if (this.DataSource is dfDataObjectProxy && ((dfDataObjectProxy)this.DataSource).Data == null)
		{
			return;
		}
		dfScriptEngineSettings dfScriptEngineSettings = new dfScriptEngineSettings
		{
			Constants = new Dictionary<string, object>
			{
				{
					"Application",
					typeof(Application)
				},
				{
					"Color",
					typeof(Color)
				},
				{
					"Color32",
					typeof(Color32)
				},
				{
					"Random",
					typeof(Random)
				},
				{
					"Time",
					typeof(Time)
				},
				{
					"ScriptableObject",
					typeof(ScriptableObject)
				},
				{
					"Vector2",
					typeof(Vector2)
				},
				{
					"Vector3",
					typeof(Vector3)
				},
				{
					"Vector4",
					typeof(Vector4)
				},
				{
					"Quaternion",
					typeof(Quaternion)
				},
				{
					"Matrix",
					typeof(Matrix4x4)
				},
				{
					"Mathf",
					typeof(Mathf)
				}
			}
		};
		if (this.DataSource is dfDataObjectProxy)
		{
			dfDataObjectProxy dfDataObjectProxy = this.DataSource as dfDataObjectProxy;
			dfScriptEngineSettings.AddVariable(new dfScriptVariable("source", null, dfDataObjectProxy.DataType));
		}
		else
		{
			dfScriptEngineSettings.AddVariable(new dfScriptVariable("source", this.DataSource));
		}
		this.compiledExpression = dfScriptEngine.CompileExpression(this.expression, dfScriptEngineSettings);
		this.targetProperty = this.DataTarget.GetProperty();
		this.isBound = (this.compiledExpression != null && this.targetProperty != null);
	}

	// Token: 0x060009BD RID: 2493 RVA: 0x0002AF70 File Offset: 0x00029170
	private void evaluate()
	{
		try
		{
			object obj = this.DataSource;
			if (obj is dfDataObjectProxy)
			{
				obj = ((dfDataObjectProxy)obj).Data;
			}
			object value = this.compiledExpression.DynamicInvoke(new object[]
			{
				obj
			});
			this.targetProperty.Value = value;
		}
		catch (Exception message)
		{
			Debug.LogError(message);
		}
	}

	// Token: 0x060009BE RID: 2494 RVA: 0x0002AFD4 File Offset: 0x000291D4
	public override string ToString()
	{
		string arg = (this.DataTarget != null && this.DataTarget.Component != null) ? this.DataTarget.Component.GetType().Name : "[null]";
		string arg2 = (this.DataTarget != null && !string.IsNullOrEmpty(this.DataTarget.MemberName)) ? this.DataTarget.MemberName : "[null]";
		return string.Format("Bind [expression] -> {0}.{1}", arg, arg2);
	}

	// Token: 0x04000495 RID: 1173
	public Component DataSource;

	// Token: 0x04000496 RID: 1174
	public dfComponentMemberInfo DataTarget;

	// Token: 0x04000497 RID: 1175
	[SerializeField]
	protected string expression;

	// Token: 0x04000498 RID: 1176
	private Delegate compiledExpression;

	// Token: 0x04000499 RID: 1177
	private dfObservableProperty targetProperty;

	// Token: 0x0400049A RID: 1178
	private bool isBound;
}

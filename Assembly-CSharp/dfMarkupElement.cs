using System;
using System.Collections.Generic;

// Token: 0x02000086 RID: 134
public abstract class dfMarkupElement
{
	// Token: 0x170001EC RID: 492
	// (get) Token: 0x06000884 RID: 2180 RVA: 0x00025ECA File Offset: 0x000240CA
	// (set) Token: 0x06000885 RID: 2181 RVA: 0x00025ED2 File Offset: 0x000240D2
	public dfMarkupElement Parent { get; protected set; }

	// Token: 0x170001ED RID: 493
	// (get) Token: 0x06000886 RID: 2182 RVA: 0x00025EDB File Offset: 0x000240DB
	// (set) Token: 0x06000887 RID: 2183 RVA: 0x00025EE3 File Offset: 0x000240E3
	private protected List<dfMarkupElement> ChildNodes { protected get; private set; }

	// Token: 0x06000888 RID: 2184 RVA: 0x00025EEC File Offset: 0x000240EC
	public dfMarkupElement()
	{
		this.ChildNodes = new List<dfMarkupElement>();
	}

	// Token: 0x06000889 RID: 2185 RVA: 0x00025EFF File Offset: 0x000240FF
	public void InsertChildNode(dfMarkupElement node, int index)
	{
		node.Parent = this;
		if (index >= 0)
		{
			if (this.ChildNodes.Count < index)
			{
				this.ChildNodes.Insert(index, node);
				return;
			}
			this.ChildNodes.Add(node);
		}
	}

	// Token: 0x0600088A RID: 2186 RVA: 0x00025F34 File Offset: 0x00024134
	public void AddChildNode(dfMarkupElement node)
	{
		node.Parent = this;
		this.ChildNodes.Add(node);
	}

	// Token: 0x0600088B RID: 2187 RVA: 0x00025F49 File Offset: 0x00024149
	public void PerformLayout(dfMarkupBox container, dfMarkupStyle style)
	{
		this._PerformLayoutImpl(container, style);
	}

	// Token: 0x0600088C RID: 2188 RVA: 0x00025F53 File Offset: 0x00024153
	internal virtual void Release()
	{
		this.Parent = null;
		this.ChildNodes.Clear();
	}

	// Token: 0x0600088D RID: 2189
	protected abstract void _PerformLayoutImpl(dfMarkupBox container, dfMarkupStyle style);
}

using System;

namespace DunGen.Graph
{
	// Token: 0x02000200 RID: 512
	[Serializable]
	public sealed class FlowNodeReference : FlowGraphObjectReference
	{
		// Token: 0x170003C4 RID: 964
		// (get) Token: 0x0600142F RID: 5167 RVA: 0x0005A575 File Offset: 0x00058775
		// (set) Token: 0x06001430 RID: 5168 RVA: 0x0005A58D File Offset: 0x0005878D
		public GraphNode Node
		{
			get
			{
				return this.flow.Nodes[this.index];
			}
			set
			{
				this.index = this.flow.Nodes.IndexOf(value);
			}
		}

		// Token: 0x06001431 RID: 5169 RVA: 0x0005A5A6 File Offset: 0x000587A6
		public FlowNodeReference(DungeonFlow flowGraph, GraphNode node)
		{
			this.flow = flowGraph;
			this.Node = node;
		}
	}
}

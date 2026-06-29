using System;

namespace DunGen.Graph
{
	// Token: 0x02000201 RID: 513
	[Serializable]
	public sealed class FlowLineReference : FlowGraphObjectReference
	{
		// Token: 0x170003C5 RID: 965
		// (get) Token: 0x06001432 RID: 5170 RVA: 0x0005A5BC File Offset: 0x000587BC
		// (set) Token: 0x06001433 RID: 5171 RVA: 0x0005A5D4 File Offset: 0x000587D4
		public GraphLine Line
		{
			get
			{
				return this.flow.Lines[this.index];
			}
			set
			{
				this.index = this.flow.Lines.IndexOf(value);
			}
		}

		// Token: 0x06001434 RID: 5172 RVA: 0x0005A5ED File Offset: 0x000587ED
		public FlowLineReference(DungeonFlow flowGraph, GraphLine line)
		{
			this.flow = flowGraph;
			this.Line = line;
		}
	}
}

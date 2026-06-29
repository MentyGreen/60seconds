using System;
using DunGen.Graph;
using UnityEngine;

namespace DunGen
{
	// Token: 0x020001F2 RID: 498
	[AddComponentMenu("DunGen/Tile")]
	public class Tile : MonoBehaviour
	{
		// Token: 0x170003B3 RID: 947
		// (get) Token: 0x060013EC RID: 5100 RVA: 0x000595D2 File Offset: 0x000577D2
		// (set) Token: 0x060013ED RID: 5101 RVA: 0x000595DA File Offset: 0x000577DA
		public TilePlacementData Placement
		{
			get
			{
				return this.placement;
			}
			internal set
			{
				this.placement = value;
			}
		}

		// Token: 0x170003B4 RID: 948
		// (get) Token: 0x060013EE RID: 5102 RVA: 0x000595E3 File Offset: 0x000577E3
		// (set) Token: 0x060013EF RID: 5103 RVA: 0x000595EB File Offset: 0x000577EB
		public DungeonArchetype Archetype
		{
			get
			{
				return this.archetype;
			}
			internal set
			{
				this.archetype = value;
			}
		}

		// Token: 0x170003B5 RID: 949
		// (get) Token: 0x060013F0 RID: 5104 RVA: 0x000595F4 File Offset: 0x000577F4
		// (set) Token: 0x060013F1 RID: 5105 RVA: 0x000595FC File Offset: 0x000577FC
		public TileSet TileSet
		{
			get
			{
				return this.tileSet;
			}
			internal set
			{
				this.tileSet = value;
			}
		}

		// Token: 0x170003B6 RID: 950
		// (get) Token: 0x060013F2 RID: 5106 RVA: 0x00059605 File Offset: 0x00057805
		// (set) Token: 0x060013F3 RID: 5107 RVA: 0x0005961C File Offset: 0x0005781C
		public GraphNode Node
		{
			get
			{
				if (this.node != null)
				{
					return this.node.Node;
				}
				return null;
			}
			internal set
			{
				if (value == null)
				{
					this.node = null;
					return;
				}
				this.node = new FlowNodeReference(value.Graph, value);
			}
		}

		// Token: 0x170003B7 RID: 951
		// (get) Token: 0x060013F4 RID: 5108 RVA: 0x0005963B File Offset: 0x0005783B
		// (set) Token: 0x060013F5 RID: 5109 RVA: 0x00059652 File Offset: 0x00057852
		public GraphLine Line
		{
			get
			{
				if (this.line != null)
				{
					return this.line.Line;
				}
				return null;
			}
			internal set
			{
				if (value == null)
				{
					this.line = null;
					return;
				}
				this.line = new FlowLineReference(value.Graph, value);
			}
		}

		// Token: 0x060013F6 RID: 5110 RVA: 0x00059674 File Offset: 0x00057874
		private void OnDrawGizmosSelected()
		{
			if (this.placement == null)
			{
				return;
			}
			Gizmos.color = Color.red;
			Gizmos.DrawWireCube(this.placement.Bounds.center, this.placement.Bounds.size);
		}

		// Token: 0x04000D23 RID: 3363
		public bool AllowRotation = true;

		// Token: 0x04000D24 RID: 3364
		[SerializeField]
		private TilePlacementData placement;

		// Token: 0x04000D25 RID: 3365
		[SerializeField]
		private DungeonArchetype archetype;

		// Token: 0x04000D26 RID: 3366
		[SerializeField]
		private TileSet tileSet;

		// Token: 0x04000D27 RID: 3367
		[SerializeField]
		private FlowNodeReference node;

		// Token: 0x04000D28 RID: 3368
		[SerializeField]
		private FlowLineReference line;
	}
}

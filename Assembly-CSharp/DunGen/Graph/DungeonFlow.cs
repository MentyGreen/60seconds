using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DunGen.Graph
{
	// Token: 0x020001FB RID: 507
	[Serializable]
	public class DungeonFlow : ScriptableObject
	{
		// Token: 0x06001426 RID: 5158 RVA: 0x0005A28C File Offset: 0x0005848C
		public void Reset()
		{
			this.Nodes.Clear();
			this.Lines.Clear();
			GraphNode graphNode = new GraphNode(this);
			GraphLine graphLine = new GraphLine(this);
			GraphNode graphNode2 = new GraphNode(this);
			graphNode.NodeType = NodeType.Start;
			graphNode.Label = "Start";
			graphLine.Length = 1f;
			graphNode2.NodeType = NodeType.Goal;
			graphNode2.Label = "Goal";
			graphNode2.Position = 1f;
			this.Nodes.Add(graphNode);
			this.Nodes.Add(graphNode2);
			this.Lines.Add(graphLine);
		}

		// Token: 0x06001427 RID: 5159 RVA: 0x0005A324 File Offset: 0x00058524
		public GraphLine GetLineAtDepth(float normalizedDepth)
		{
			normalizedDepth = Mathf.Clamp(normalizedDepth, 0f, 1f);
			if (normalizedDepth == 0f)
			{
				return this.Lines[0];
			}
			if (normalizedDepth == 1f)
			{
				return this.Lines[this.Lines.Count - 1];
			}
			foreach (GraphLine graphLine in this.Lines)
			{
				if (normalizedDepth >= graphLine.Position && normalizedDepth < graphLine.Position + graphLine.Length)
				{
					return graphLine;
				}
			}
			Debug.LogError("GetLineAtDepth was unable to find a line at depth " + normalizedDepth.ToString() + ". This shouldn't happen.");
			return null;
		}

		// Token: 0x06001428 RID: 5160 RVA: 0x0005A3F4 File Offset: 0x000585F4
		public DungeonArchetype[] GetUsedArchetypes()
		{
			return this.Lines.SelectMany((GraphLine x) => x.DungeonArchetypes).ToArray<DungeonArchetype>();
		}

		// Token: 0x06001429 RID: 5161 RVA: 0x0005A428 File Offset: 0x00058628
		public TileSet[] GetUsedTileSets()
		{
			return this.Nodes.SelectMany((GraphNode x) => x.TileSets).Concat(this.Lines.SelectMany((GraphLine x) => x.DungeonArchetypes).SelectMany((DungeonArchetype y) => y.TileSets)).ToArray<TileSet>();
		}

		// Token: 0x04000D3E RID: 3390
		public IntRange Length = new IntRange(5, 10);

		// Token: 0x04000D3F RID: 3391
		public List<int> GlobalPropGroupIDs = new List<int>();

		// Token: 0x04000D40 RID: 3392
		public List<IntRange> GlobalPropRanges = new List<IntRange>();

		// Token: 0x04000D41 RID: 3393
		public KeyManager KeyManager;

		// Token: 0x04000D42 RID: 3394
		public float DoorwayConnectionChance;

		// Token: 0x04000D43 RID: 3395
		public List<GraphNode> Nodes = new List<GraphNode>();

		// Token: 0x04000D44 RID: 3396
		public List<GraphLine> Lines = new List<GraphLine>();
	}
}

using System;
using System.Collections.Generic;
using DunGen.Graph;
using UnityEngine;

namespace DunGen
{
	// Token: 0x020001F7 RID: 503
	public sealed class DungeonArchetypeValidator
	{
		// Token: 0x170003C2 RID: 962
		// (get) Token: 0x06001416 RID: 5142 RVA: 0x00059CA3 File Offset: 0x00057EA3
		// (set) Token: 0x06001417 RID: 5143 RVA: 0x00059CAB File Offset: 0x00057EAB
		public DungeonFlow Flow { get; private set; }

		// Token: 0x06001418 RID: 5144 RVA: 0x00059CB4 File Offset: 0x00057EB4
		public DungeonArchetypeValidator(DungeonFlow flow)
		{
			this.Flow = flow;
		}

		// Token: 0x06001419 RID: 5145 RVA: 0x00059CC4 File Offset: 0x00057EC4
		public bool IsValid()
		{
			if (this.Flow == null)
			{
				this.LogError("No Dungeon Flow is assigned", Array.Empty<object>());
				return false;
			}
			DungeonArchetype[] usedArchetypes = this.Flow.GetUsedArchetypes();
			TileSet[] usedTileSets = this.Flow.GetUsedTileSets();
			foreach (GraphLine graphLine in this.Flow.Lines)
			{
				if (graphLine.DungeonArchetypes.Count == 0)
				{
					this.LogError("One or more line segments in your dungeon flow graph have no archetype applied. Each line segment must have at least one archetype assigned to it.", Array.Empty<object>());
					return false;
				}
				using (List<DungeonArchetype>.Enumerator enumerator2 = graphLine.DungeonArchetypes.GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						if (enumerator2.Current == null)
						{
							this.LogError("One or more of the archetypes in your dungeon flow graph have an unset archetype value.", Array.Empty<object>());
							return false;
						}
					}
				}
			}
			foreach (GraphNode graphNode in this.Flow.Nodes)
			{
				if (graphNode.TileSets.Count == 0)
				{
					this.LogError("The \"{0}\" node in your dungeon flow graph have no tile sets applied. Each node must have at least one tile set assigned to it.", new object[]
					{
						graphNode.Label
					});
					return false;
				}
			}
			DungeonArchetype[] array = usedArchetypes;
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i] == null)
				{
					this.LogError("An Archetype in the Dungeon Flow has not been assigned a value", Array.Empty<object>());
					return false;
				}
			}
			foreach (TileSet tileSet in usedTileSets)
			{
				if (tileSet == null)
				{
					this.LogError("A TileSet in the Dungeon Flow has not been assigned a value", Array.Empty<object>());
					return false;
				}
				if (tileSet.TileWeights.Weights.Count == 0)
				{
					this.LogError("TileSet \"{0}\" contains no Tiles", new object[]
					{
						tileSet.name
					});
					return false;
				}
				foreach (GameObjectChance gameObjectChance in tileSet.TileWeights.Weights)
				{
					if (gameObjectChance.Value == null)
					{
						this.LogWarning("TileSet \"{0}\" contains an entry with no Tile", new object[]
						{
							tileSet.name
						});
					}
					if (gameObjectChance.MainPathWeight <= 0f && gameObjectChance.BranchPathWeight <= 0f)
					{
						this.LogWarning("TileSet \"{0}\" contains an entry with an invalid weight. Both weights are below zero, resulting in no chance for this tile to spawn in the dungeon. Either MainPathWeight or BranchPathWeight can be zero, not both.", new object[]
						{
							tileSet.name
						});
					}
				}
			}
			return true;
		}

		// Token: 0x0600141A RID: 5146 RVA: 0x00059F90 File Offset: 0x00058190
		private void LogError(string format, params object[] args)
		{
			Debug.LogError(string.Format("[ArchetypeValidator] Error: " + format, args));
		}

		// Token: 0x0600141B RID: 5147 RVA: 0x00059FA8 File Offset: 0x000581A8
		private void LogWarning(string format, params object[] args)
		{
			Debug.LogWarning(string.Format("[ArchetypeValidator] Warning: " + format, args));
		}
	}
}

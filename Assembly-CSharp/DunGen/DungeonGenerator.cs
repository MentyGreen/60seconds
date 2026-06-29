using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using DunGen.Analysis;
using DunGen.Graph;
using UnityEngine;

namespace DunGen
{
	// Token: 0x020001DA RID: 474
	[Serializable]
	public class DungeonGenerator
	{
		// Token: 0x1700039A RID: 922
		// (get) Token: 0x0600136C RID: 4972 RVA: 0x00056746 File Offset: 0x00054946
		// (set) Token: 0x0600136D RID: 4973 RVA: 0x0005674E File Offset: 0x0005494E
		public Random RandomStream { get; protected set; }

		// Token: 0x14000067 RID: 103
		// (add) Token: 0x0600136E RID: 4974 RVA: 0x00056758 File Offset: 0x00054958
		// (remove) Token: 0x0600136F RID: 4975 RVA: 0x00056790 File Offset: 0x00054990
		public event GenerationStatusDelegate OnGenerationStatusChanged;

		// Token: 0x1700039B RID: 923
		// (get) Token: 0x06001370 RID: 4976 RVA: 0x000567C5 File Offset: 0x000549C5
		// (set) Token: 0x06001371 RID: 4977 RVA: 0x000567CD File Offset: 0x000549CD
		public GenerationStatus Status { get; private set; }

		// Token: 0x1700039C RID: 924
		// (get) Token: 0x06001372 RID: 4978 RVA: 0x000567D6 File Offset: 0x000549D6
		// (set) Token: 0x06001373 RID: 4979 RVA: 0x000567DE File Offset: 0x000549DE
		public GenerationStats GenerationStats { get; private set; }

		// Token: 0x1700039D RID: 925
		// (get) Token: 0x06001374 RID: 4980 RVA: 0x000567E7 File Offset: 0x000549E7
		// (set) Token: 0x06001375 RID: 4981 RVA: 0x000567EF File Offset: 0x000549EF
		public int ChosenSeed { get; protected set; }

		// Token: 0x1700039E RID: 926
		// (get) Token: 0x06001376 RID: 4982 RVA: 0x000567F8 File Offset: 0x000549F8
		public Dungeon CurrentDungeon
		{
			get
			{
				return this.currentDungeon;
			}
		}

		// Token: 0x06001377 RID: 4983 RVA: 0x00056800 File Offset: 0x00054A00
		public DungeonGenerator()
		{
			this.GenerationStats = new GenerationStats();
		}

		// Token: 0x06001378 RID: 4984 RVA: 0x00056855 File Offset: 0x00054A55
		public DungeonGenerator(GameObject root) : this()
		{
			this.Root = root;
		}

		// Token: 0x06001379 RID: 4985 RVA: 0x00056864 File Offset: 0x00054A64
		public bool OuterGenerate(int? seed)
		{
			this.ShouldRandomizeSeed = (seed == null);
			if (seed != null)
			{
				this.Seed = seed.Value;
			}
			return this.Generate();
		}

		// Token: 0x0600137A RID: 4986 RVA: 0x00056892 File Offset: 0x00054A92
		public bool Generate()
		{
			this.isAnalysis = false;
			return this.OuterGenerate();
		}

		// Token: 0x0600137B RID: 4987 RVA: 0x000568A4 File Offset: 0x00054AA4
		protected virtual bool OuterGenerate()
		{
			this.Status = GenerationStatus.NotStarted;
			if (!new DungeonArchetypeValidator(this.DungeonFlow).IsValid())
			{
				this.ChangeStatus(GenerationStatus.Failed);
				return false;
			}
			this.ChosenSeed = (this.ShouldRandomizeSeed ? new Random().Next() : this.Seed);
			this.RandomStream = new Random(this.ChosenSeed);
			if (this.Root == null)
			{
				this.Root = new GameObject("Dungeon");
			}
			bool flag = this.InnerGenerate(false);
			if (!flag)
			{
				this.Clear();
			}
			return flag;
		}

		// Token: 0x0600137C RID: 4988 RVA: 0x00056934 File Offset: 0x00054B34
		public GenerationAnalysis RunAnalysis(int iterations, float maximumAnalysisTime)
		{
			DungeonArchetypeValidator dungeonArchetypeValidator = new DungeonArchetypeValidator(this.DungeonFlow);
			if (Application.isEditor && !dungeonArchetypeValidator.IsValid())
			{
				this.ChangeStatus(GenerationStatus.Failed);
				return null;
			}
			bool shouldRandomizeSeed = this.ShouldRandomizeSeed;
			this.isAnalysis = true;
			this.ShouldRandomizeSeed = true;
			GenerationAnalysis generationAnalysis = new GenerationAnalysis(iterations);
			Stopwatch stopwatch = Stopwatch.StartNew();
			int num = 0;
			while (num < iterations && (maximumAnalysisTime <= 0f || stopwatch.Elapsed.TotalMilliseconds < (double)maximumAnalysisTime))
			{
				if (this.OuterGenerate())
				{
					generationAnalysis.IncrementSuccessCount();
					generationAnalysis.Add(this.GenerationStats);
				}
				num++;
			}
			this.Clear();
			generationAnalysis.Analyze();
			this.ShouldRandomizeSeed = shouldRandomizeSeed;
			return generationAnalysis;
		}

		// Token: 0x0600137D RID: 4989 RVA: 0x000569E0 File Offset: 0x00054BE0
		public void RandomizeSeed()
		{
			this.Seed = new Random().Next();
		}

		// Token: 0x0600137E RID: 4990 RVA: 0x000569F4 File Offset: 0x00054BF4
		protected virtual bool InnerGenerate(bool isRetry)
		{
			if (isRetry)
			{
				if (this.retryCount >= this.MaxAttemptCount)
				{
					Debug.LogError(string.Format("Failed to generate the dungeon {0} times. This could indicate a problem with the way the tiles are set up. Try to make sure most rooms have more than one doorway and that all doorways are easily accessible.", this.MaxAttemptCount));
					this.ChangeStatus(GenerationStatus.Failed);
					return false;
				}
				this.retryCount++;
				this.GenerationStats.IncrementRetryCount();
			}
			else
			{
				this.retryCount = 0;
				this.GenerationStats.Clear();
			}
			this.currentDungeon = this.Root.GetComponent<Dungeon>();
			if (this.currentDungeon == null)
			{
				this.currentDungeon = this.Root.AddComponent<Dungeon>();
			}
			this.currentDungeon.PreGenerateDungeon(this);
			this.Clear();
			this.GenerationStats.BeginTime(GenerationStatus.PreProcessing);
			this.PreProcess();
			this.GenerationStats.BeginTime(GenerationStatus.MainPath);
			if (!this.GenerateMainPath())
			{
				this.ChosenSeed = this.RandomStream.Next();
				return this.InnerGenerate(true);
			}
			this.GenerationStats.BeginTime(GenerationStatus.Branching);
			this.GenerateBranchPaths(true);
			this.GenerationStats.BeginTime(GenerationStatus.PostProcessing);
			this.PostProcess();
			this.GenerationStats.EndTime();
			this.ChangeStatus(GenerationStatus.Complete);
			return true;
		}

		// Token: 0x0600137F RID: 4991 RVA: 0x00056B1C File Offset: 0x00054D1C
		protected virtual void Clear()
		{
			this.currentDungeon.Clear();
			foreach (PreProcessTileData preProcessTileData in this.preProcessData)
			{
				Object.DestroyImmediate(preProcessTileData.Proxy);
			}
			this.useableTiles.Clear();
			this.preProcessData.Clear();
		}

		// Token: 0x06001380 RID: 4992 RVA: 0x00056B94 File Offset: 0x00054D94
		private void ChangeStatus(GenerationStatus status)
		{
			GenerationStatus status2 = this.Status;
			this.Status = status;
			if (status2 != status && this.OnGenerationStatusChanged != null)
			{
				this.OnGenerationStatusChanged(this, status);
			}
		}

		// Token: 0x06001381 RID: 4993 RVA: 0x00056BBC File Offset: 0x00054DBC
		protected virtual void PreProcess()
		{
			if (this.preProcessData.Count > 0)
			{
				return;
			}
			this.ChangeStatus(GenerationStatus.PreProcessing);
			TileSet[] usedTileSets = this.DungeonFlow.GetUsedTileSets();
			for (int i = 0; i < usedTileSets.Length; i++)
			{
				foreach (GameObjectChance gameObjectChance in usedTileSets[i].TileWeights.Weights)
				{
					if (gameObjectChance.Value != null)
					{
						this.useableTiles.Add(gameObjectChance.Value);
					}
				}
			}
			foreach (GameObject prefab in this.useableTiles)
			{
				this.preProcessData.Add(new PreProcessTileData(prefab, this.IgnoreSpriteBounds));
			}
		}

		// Token: 0x06001382 RID: 4994 RVA: 0x00056CB4 File Offset: 0x00054EB4
		protected virtual bool GenerateMainPath()
		{
			this.ChangeStatus(GenerationStatus.MainPath);
			this.targetLength = this.DungeonFlow.Length.GetRandom(this.RandomStream);
			this.nextNodeIndex = 0;
			List<GraphNode> list = new List<GraphNode>(this.DungeonFlow.Nodes.Count);
			bool flag = false;
			int num = 0;
			List<List<TileSet>> list2 = new List<List<TileSet>>(this.targetLength);
			List<DungeonArchetype> list3 = new List<DungeonArchetype>(this.targetLength);
			List<GraphNode> list4 = new List<GraphNode>(this.targetLength);
			List<GraphLine> list5 = new List<GraphLine>(this.targetLength);
			while (!flag)
			{
				float num2 = Mathf.Clamp((float)num / (float)(this.targetLength - 1), 0f, 1f);
				GraphLine lineAtDepth = this.DungeonFlow.GetLineAtDepth(num2);
				if (lineAtDepth == null)
				{
					return false;
				}
				if (lineAtDepth != this.previousLineSegment)
				{
					this.currentArchetype = lineAtDepth.DungeonArchetypes[this.RandomStream.Next(0, lineAtDepth.DungeonArchetypes.Count)];
					this.previousLineSegment = lineAtDepth;
				}
				GraphNode graphNode = null;
				GraphNode[] array = (from x in this.DungeonFlow.Nodes
				orderby x.Position
				select x).ToArray<GraphNode>();
				foreach (GraphNode graphNode2 in array)
				{
					if (num2 >= graphNode2.Position && !list.Contains(graphNode2))
					{
						graphNode = graphNode2;
						list.Add(graphNode2);
						break;
					}
				}
				List<TileSet> tileSets;
				if (graphNode != null)
				{
					tileSets = graphNode.TileSets;
					this.nextNodeIndex = ((this.nextNodeIndex >= array.Length - 1) ? -1 : (this.nextNodeIndex + 1));
					list3.Add(this.currentArchetype);
					list5.Add(null);
					list4.Add(graphNode);
					if (graphNode == array[array.Length - 1])
					{
						flag = true;
					}
				}
				else
				{
					tileSets = this.currentArchetype.TileSets;
					list3.Add(this.currentArchetype);
					list5.Add(lineAtDepth);
					list4.Add(null);
				}
				list2.Add(tileSets);
				num++;
			}
			for (int j = 0; j < list2.Count; j++)
			{
				Tile tile = this.AddTile((j == 0) ? null : this.currentDungeon.MainPathTiles[j - 1], list2[j], (float)j / (float)(list2.Count - 1), list3[j], true);
				if (tile == null)
				{
					return false;
				}
				tile.Node = list4[j];
				tile.Line = list5[j];
			}
			return true;
		}

		// Token: 0x06001383 RID: 4995 RVA: 0x00056F44 File Offset: 0x00055144
		protected virtual void GenerateBranchPaths(bool recoverOnTileGenerationFail = true)
		{
			this.ChangeStatus(GenerationStatus.Branching);
			Dictionary<DungeonArchetype, List<TileSet>> dictionary = new Dictionary<DungeonArchetype, List<TileSet>>();
			TileSet[] array = new TileSet[this.currentDungeon.DungeonFlow.GetUsedArchetypes()[0].TileSets.Count];
			this.currentDungeon.DungeonFlow.GetUsedArchetypes()[0].TileSets.CopyTo(array);
			for (int i = 0; i < 5; i++)
			{
				foreach (Tile tile in this.currentDungeon.MainPathTiles)
				{
					if (!(tile.Archetype == null))
					{
						if (!dictionary.ContainsKey(tile.Archetype))
						{
							dictionary.Add(tile.Archetype, new List<TileSet>());
						}
						int random = tile.Archetype.BranchCount.GetRandom(this.RandomStream);
						if (random != 0)
						{
							Tile tile2 = tile;
							int num = 0;
							while (num < random && tile.Archetype.TileSets.Count != 0)
							{
								Tile tile3 = this.AddTile(tile2, tile.Archetype.TileSets, (float)num / (float)(random - 1), tile.Archetype, true);
								if (!(tile3 == null) || !recoverOnTileGenerationFail)
								{
									goto IL_150;
								}
								tile3 = this.AddTile(tile2, tile.Archetype.TileSets, (float)num / (float)(random - 1), tile.Archetype, true);
								if (!(tile3 == null))
								{
									goto IL_150;
								}
								IL_1DF:
								num++;
								continue;
								IL_150:
								tile.Archetype.TileSets.Remove(tile3.TileSet);
								dictionary[tile.Archetype].Add(tile3.TileSet);
								tile3.Placement.BranchDepth = num;
								tile3.Placement.NormalizedBranchDepth = (float)num / (float)(random - 1);
								tile3.Node = tile2.Node;
								tile3.Line = tile2.Line;
								if (tile3.Placement.UnusedDoorways.Count <= 1)
								{
									tile2 = tile;
									goto IL_1DF;
								}
								tile2 = tile3;
								goto IL_1DF;
							}
						}
					}
				}
				foreach (DungeonArchetype dungeonArchetype in dictionary.Keys)
				{
					for (int j = dungeonArchetype.TileSets.Count - 1; j >= 0; j--)
					{
						for (int k = 0; k < this.currentDungeon.BranchPathTiles.Count; k++)
						{
							int random2 = dungeonArchetype.BranchCount.GetRandom(this.RandomStream);
							Tile tile4 = this.AddTile(this.currentDungeon.BranchPathTiles[k], dungeonArchetype.TileSets, 1f, dungeonArchetype, true);
							if (tile4 != null)
							{
								dungeonArchetype.TileSets.Remove(tile4.TileSet);
								dictionary[dungeonArchetype].Add(tile4.TileSet);
								tile4.Placement.BranchDepth = j;
								tile4.Placement.NormalizedBranchDepth = (float)j / (float)(random2 - 1);
								tile4.Node = this.currentDungeon.BranchPathTiles[k].Node;
								tile4.Line = this.currentDungeon.BranchPathTiles[k].Line;
								break;
							}
						}
					}
				}
				bool flag = false;
				foreach (DungeonArchetype dungeonArchetype2 in dictionary.Keys)
				{
					flag |= (dungeonArchetype2.TileSets.Count == 0);
				}
				if (flag)
				{
					break;
				}
			}
			DungeonArchetype dungeonArchetype3 = this.currentDungeon.DungeonFlow.GetUsedArchetypes()[0];
			dungeonArchetype3.TileSets.Clear();
			dungeonArchetype3.TileSets.AddRange(array);
			dictionary.Clear();
			array = null;
		}

		// Token: 0x06001384 RID: 4996 RVA: 0x0005737C File Offset: 0x0005557C
		protected virtual Tile AddTile(Tile attachTo, IList<TileSet> useableTileSets, float normalizedDepth, DungeonArchetype archetype, bool tryAllDoors = false)
		{
			Tile tile = null;
			TileSet tileSet = useableTileSets[this.RandomStream.Next(0, useableTileSets.Count)];
			if (tryAllDoors && attachTo != null)
			{
				int i = attachTo.Placement.UnusedDoorways.Count;
				int num = Random.Range(0, i);
				while (i > 0)
				{
					Doorway fromDoorway = attachTo.Placement.UnusedDoorways[num];
					tile = this.AddTile(attachTo, tileSet, fromDoorway, normalizedDepth, archetype);
					if (tile != null)
					{
						break;
					}
					i--;
					num++;
					if (num >= attachTo.Placement.UnusedDoorways.Count)
					{
						num = 0;
					}
				}
			}
			else
			{
				tile = this.AddTile(attachTo, tileSet, (attachTo == null) ? null : attachTo.Placement.PickRandomDoorway(this.RandomStream, true), normalizedDepth, archetype);
			}
			return tile;
		}

		// Token: 0x06001385 RID: 4997 RVA: 0x0005744C File Offset: 0x0005564C
		protected virtual Tile AddTile(Tile attachTo, TileSet tileSet, Doorway fromDoorway, float normalizedDepth, DungeonArchetype archetype)
		{
			if (attachTo != null && fromDoorway == null)
			{
				return null;
			}
			GameObjectChanceTable gameObjectChanceTable = tileSet.TileWeights.Clone();
			if (attachTo != null)
			{
				for (int i = gameObjectChanceTable.Weights.Count - 1; i >= 0; i--)
				{
					GameObjectChance c = gameObjectChanceTable.Weights[i];
					PreProcessTileData preProcessTileData = (from x in this.preProcessData
					where x.Prefab == c.Value
					select x).FirstOrDefault<PreProcessTileData>();
					if (preProcessTileData == null || !preProcessTileData.DoorwaySockets.Contains(fromDoorway.SocketGroup))
					{
						gameObjectChanceTable.Weights.RemoveAt(i);
					}
				}
			}
			if (gameObjectChanceTable.Weights.Count == 0)
			{
				return null;
			}
			GameObject tilePrefab = tileSet.TileWeights.GetRandom(this.RandomStream, this.Status == GenerationStatus.MainPath, normalizedDepth, false);
			PreProcessTileData preProcessTileData2 = (from x in this.preProcessData
			where x.Prefab == tilePrefab
			select x).FirstOrDefault<PreProcessTileData>();
			if (preProcessTileData2 == null)
			{
				return null;
			}
			int index = 0;
			Doorway doorway = null;
			if (fromDoorway != null)
			{
				Tile component = preProcessTileData2.Prefab.GetComponent<Tile>();
				Vector3? allowedDirection;
				if (component == null || component.AllowRotation)
				{
					allowedDirection = null;
				}
				else
				{
					allowedDirection = new Vector3?(-fromDoorway.transform.forward);
				}
				if (!preProcessTileData2.ChooseRandomDoorway(this.RandomStream, new DoorwaySocketType?(fromDoorway.SocketGroup), allowedDirection, out index, out doorway))
				{
					return null;
				}
				this.MoveIntoPosition(preProcessTileData2.Proxy, fromDoorway, doorway);
				if (this.IsCollidingWithAnyTile(preProcessTileData2.Proxy))
				{
					return null;
				}
			}
			TilePlacementData tilePlacementData = new TilePlacementData(preProcessTileData2, this.Status == GenerationStatus.MainPath, archetype, tileSet);
			if (tilePlacementData == null)
			{
				return null;
			}
			if (tilePlacementData.IsOnMainPath)
			{
				if (attachTo != null)
				{
					tilePlacementData.PathDepth = attachTo.Placement.PathDepth + 1;
				}
			}
			else
			{
				tilePlacementData.PathDepth = attachTo.Placement.PathDepth;
				tilePlacementData.BranchDepth = (attachTo.Placement.IsOnMainPath ? 0 : (attachTo.Placement.BranchDepth + 1));
			}
			if (fromDoorway != null)
			{
				if (!Application.isPlaying)
				{
					tilePlacementData.Root.SetActive(false);
				}
				tilePlacementData.Root.transform.parent = this.Root.transform;
				doorway = tilePlacementData.AllDoorways[index];
				this.MoveIntoPosition(tilePlacementData.Root, fromDoorway, doorway);
				if (!Application.isPlaying)
				{
					tilePlacementData.Root.SetActive(true);
				}
				this.currentDungeon.MakeConnection(fromDoorway, doorway, this.RandomStream);
			}
			else
			{
				tilePlacementData.Root.transform.parent = this.Root.transform;
			}
			if (tilePlacementData != null)
			{
				this.currentDungeon.AddTile(tilePlacementData.Tile);
			}
			tilePlacementData.RecalculateBounds(this.IgnoreSpriteBounds);
			return tilePlacementData.Tile;
		}

		// Token: 0x06001386 RID: 4998 RVA: 0x0005772C File Offset: 0x0005592C
		protected PreProcessTileData PickRandomTemplate(DoorwaySocketType? socketGroupFilter)
		{
			IEnumerable<PreProcessTileData> enumerable2;
			if (socketGroupFilter == null)
			{
				IEnumerable<PreProcessTileData> enumerable = this.preProcessData;
				enumerable2 = enumerable;
			}
			else
			{
				enumerable2 = from x in this.preProcessData
				where x.DoorwaySockets.Contains(socketGroupFilter.Value)
				select x;
			}
			IEnumerable<PreProcessTileData> source = enumerable2;
			return source.ElementAt(this.RandomStream.Next(0, source.Count<PreProcessTileData>()));
		}

		// Token: 0x06001387 RID: 4999 RVA: 0x0005778D File Offset: 0x0005598D
		protected int NormalizedDepthToIndex(float normalizedDepth)
		{
			return Mathf.RoundToInt(normalizedDepth * (float)(this.targetLength - 1));
		}

		// Token: 0x06001388 RID: 5000 RVA: 0x0005779F File Offset: 0x0005599F
		protected float IndexToNormalizedDepth(int index)
		{
			return (float)index / (float)this.targetLength;
		}

		// Token: 0x06001389 RID: 5001 RVA: 0x000577AC File Offset: 0x000559AC
		protected void MoveIntoPosition(GameObject obj, Doorway fromDoorway, Doorway toDoorway)
		{
			obj.transform.position = -toDoorway.transform.position + fromDoorway.transform.position;
			obj.transform.rotation = Quaternion.identity;
			Vector3 forward = fromDoorway.transform.forward;
			Vector3 rhs = -toDoorway.transform.forward;
			float num = Vector3.Dot(forward, rhs);
			float num2;
			if (num >= 0.99999f)
			{
				num2 = 0f;
			}
			else if (num <= -0.99999f)
			{
				num2 = 3.1415927f;
			}
			else
			{
				num2 = Mathf.Acos(num);
			}
			if (float.IsNaN(num2))
			{
				Debug.LogError(string.Concat(new string[]
				{
					"[FloorGenerator] Offset angle is NaN. This should never happen. | Dot: ",
					num.ToString(),
					", From: ",
					fromDoorway.transform.forward.ToString(),
					", To: ",
					toDoorway.transform.forward.ToString()
				}));
			}
			Vector3 rhs2 = Vector3.Cross(forward, rhs);
			if (Vector3.Dot(this.UpVector, rhs2) > 0f)
			{
				num2 *= -1f;
			}
			obj.transform.RotateAround(fromDoorway.transform.position, this.UpVector, num2 * 57.29578f);
		}

		// Token: 0x0600138A RID: 5002 RVA: 0x00057904 File Offset: 0x00055B04
		protected bool IsCollidingWithAnyTile(GameObject proxy)
		{
			using (IEnumerator<Tile> enumerator = this.currentDungeon.AllTiles.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.Placement.Bounds.Intersects(proxy.GetComponent<Collider>().bounds))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x0600138B RID: 5003 RVA: 0x00057974 File Offset: 0x00055B74
		protected void ClearPreProcessData()
		{
			foreach (PreProcessTileData preProcessTileData in this.preProcessData)
			{
				Object.DestroyImmediate(preProcessTileData.Proxy);
			}
			this.preProcessData.Clear();
		}

		// Token: 0x0600138C RID: 5004 RVA: 0x000579D4 File Offset: 0x00055BD4
		protected virtual void ConnectOverlappingDoorways(float percentageChance)
		{
			if (percentageChance <= 0f)
			{
				return;
			}
			Doorway[] componentsInChildren = this.Root.GetComponentsInChildren<Doorway>();
			List<Doorway> list = new List<Doorway>(componentsInChildren.Length);
			foreach (Doorway doorway in componentsInChildren)
			{
				foreach (Doorway doorway2 in componentsInChildren)
				{
					if (!(doorway == doorway2) && !(doorway.Tile == doorway2.Tile) && !list.Contains(doorway2) && (doorway.transform.position - doorway2.transform.position).sqrMagnitude < 1E-05f && this.RandomStream.NextDouble() < (double)percentageChance)
					{
						this.currentDungeon.MakeConnection(doorway, doorway2, this.RandomStream);
					}
				}
				list.Add(doorway);
			}
		}

		// Token: 0x0600138D RID: 5005 RVA: 0x00057AC0 File Offset: 0x00055CC0
		protected virtual void PostProcess()
		{
			this.ChangeStatus(GenerationStatus.PostProcessing);
			foreach (Tile tile in this.currentDungeon.AllTiles)
			{
				tile.gameObject.SetActive(true);
			}
			int count = this.currentDungeon.MainPathTiles.Count;
			int maxBranchDepth = (from x in this.currentDungeon.BranchPathTiles
			orderby x.Placement.BranchDepth descending
			select x.Placement.BranchDepth).FirstOrDefault<int>();
			if (!this.isAnalysis)
			{
				this.ConnectOverlappingDoorways(this.DungeonFlow.DoorwayConnectionChance);
				foreach (Tile tile2 in this.currentDungeon.AllTiles)
				{
					tile2.Placement.NormalizedPathDepth = (float)tile2.Placement.PathDepth / (float)(count - 1);
					tile2.Placement.ProcessDoorways();
				}
				this.currentDungeon.PostGenerateDungeon(this);
				this.PlaceLocksAndKeys();
				foreach (Tile tile3 in this.currentDungeon.AllTiles)
				{
					RandomProp[] componentsInChildren = tile3.GetComponentsInChildren<RandomProp>();
					for (int i = 0; i < componentsInChildren.Length; i++)
					{
						componentsInChildren[i].Process(this.RandomStream, tile3);
					}
				}
				this.ProcessGlobalProps();
			}
			this.GenerationStats.SetRoomStatistics(this.currentDungeon.MainPathTiles.Count, this.currentDungeon.BranchPathTiles.Count, maxBranchDepth);
			this.ClearPreProcessData();
		}

		// Token: 0x0600138E RID: 5006 RVA: 0x00057CB8 File Offset: 0x00055EB8
		protected virtual void ProcessGlobalProps()
		{
			Dictionary<int, GameObjectChanceTable> dictionary = new Dictionary<int, GameObjectChanceTable>();
			foreach (Tile tile in this.currentDungeon.AllTiles)
			{
				foreach (GlobalProp globalProp in tile.GetComponentsInChildren<GlobalProp>())
				{
					GameObjectChanceTable gameObjectChanceTable = null;
					if (!dictionary.TryGetValue(globalProp.PropGroupID, out gameObjectChanceTable))
					{
						gameObjectChanceTable = new GameObjectChanceTable();
						dictionary[globalProp.PropGroupID] = gameObjectChanceTable;
					}
					float num = tile.Placement.IsOnMainPath ? globalProp.MainPathWeight : globalProp.BranchPathWeight;
					num *= globalProp.DepthWeightScale.Evaluate(tile.Placement.NormalizedDepth);
					gameObjectChanceTable.Weights.Add(new GameObjectChance(globalProp.gameObject, num, 0f));
				}
			}
			foreach (GameObject gameObject in dictionary.SelectMany((KeyValuePair<int, GameObjectChanceTable> x) => from y in x.Value.Weights
			select y.Value))
			{
				gameObject.SetActive(false);
			}
			List<int> list = new List<int>(dictionary.Count);
			foreach (KeyValuePair<int, GameObjectChanceTable> keyValuePair in dictionary)
			{
				if (list.Contains(keyValuePair.Key))
				{
					Debug.LogWarning("Dungeon Flow contains multiple entries for the global prop group ID: " + keyValuePair.Key.ToString() + ". Only the first entry will be used.");
				}
				else
				{
					int num2 = this.DungeonFlow.GlobalPropGroupIDs.IndexOf(keyValuePair.Key);
					if (num2 != -1)
					{
						IntRange intRange = this.DungeonFlow.GlobalPropRanges[num2];
						GameObjectChanceTable gameObjectChanceTable2 = keyValuePair.Value.Clone();
						int num3 = intRange.GetRandom(this.RandomStream);
						num3 = Mathf.Clamp(num3, 0, gameObjectChanceTable2.Weights.Count);
						for (int j = 0; j < num3; j++)
						{
							GameObject random = gameObjectChanceTable2.GetRandom(this.RandomStream, true, 0f, true);
							if (random != null)
							{
								random.SetActive(true);
							}
						}
						list.Add(keyValuePair.Key);
					}
				}
			}
		}

		// Token: 0x0600138F RID: 5007 RVA: 0x00057F48 File Offset: 0x00056148
		protected virtual void PlaceLocksAndKeys()
		{
			GraphNode[] array = (from x in this.currentDungeon.ConnectionGraph.Nodes
			select x.Tile.Node into x
			where x != null
			select x).Distinct<GraphNode>().ToArray<GraphNode>();
			GraphLine[] array2 = (from x in this.currentDungeon.ConnectionGraph.Nodes
			select x.Tile.Line into x
			where x != null
			select x).Distinct<GraphLine>().ToArray<GraphLine>();
			Dictionary<Doorway, Key> dictionary = new Dictionary<Doorway, Key>();
			GraphNode[] array3 = array;
			for (int i = 0; i < array3.Length; i++)
			{
				GraphNode node = array3[i];
				Func<Tile, bool> <>9__4;
				foreach (KeyLockPlacement keyLockPlacement in node.Locks)
				{
					DungeonGenerator.<>c__DisplayClass62_1 CS$<>8__locals2 = new DungeonGenerator.<>c__DisplayClass62_1();
					DungeonGenerator.<>c__DisplayClass62_1 CS$<>8__locals3 = CS$<>8__locals2;
					IEnumerable<Tile> allTiles = this.currentDungeon.AllTiles;
					Func<Tile, bool> predicate;
					if ((predicate = <>9__4) == null)
					{
						predicate = (<>9__4 = ((Tile x) => x.Node == node));
					}
					CS$<>8__locals3.tile = allTiles.Where(predicate).FirstOrDefault<Tile>();
					List<DungeonGraphConnection> connections = (from x in this.currentDungeon.ConnectionGraph.Nodes
					where x.Tile == CS$<>8__locals2.tile
					select x).FirstOrDefault<DungeonGraphNode>().Connections;
					Doorway doorway = null;
					Doorway x2 = null;
					foreach (DungeonGraphConnection dungeonGraphConnection in connections)
					{
						if (dungeonGraphConnection.DoorwayA.Tile == CS$<>8__locals2.tile)
						{
							x2 = dungeonGraphConnection.DoorwayA;
						}
						else if (dungeonGraphConnection.DoorwayB.Tile == CS$<>8__locals2.tile)
						{
							doorway = dungeonGraphConnection.DoorwayB;
						}
					}
					if (doorway != null && (node.LockPlacement & NodeLockPlacement.Entrance) == NodeLockPlacement.Entrance)
					{
						Key keyByID = node.Graph.KeyManager.GetKeyByID(keyLockPlacement.ID);
						dictionary[doorway] = keyByID;
					}
					if (x2 != null && (node.LockPlacement & NodeLockPlacement.Exit) == NodeLockPlacement.Exit)
					{
						Key keyByID2 = node.Graph.KeyManager.GetKeyByID(keyLockPlacement.ID);
						dictionary[doorway] = keyByID2;
					}
				}
			}
			GraphLine[] array4 = array2;
			for (int i = 0; i < array4.Length; i++)
			{
				GraphLine line = array4[i];
				List<Doorway> list = (from x in this.currentDungeon.ConnectionGraph.Connections
				where x.DoorwayA.Tile.Line == line && x.DoorwayB.Tile.Line == line
				select x.DoorwayA).ToList<Doorway>();
				foreach (KeyLockPlacement keyLockPlacement2 in line.Locks)
				{
					Mathf.Clamp(keyLockPlacement2.Range.GetRandom(this.RandomStream), 0, list.Count);
					Doorway doorway2 = list[this.RandomStream.Next(0, list.Count)];
					list.Remove(doorway2);
					Key keyByID3 = line.Graph.KeyManager.GetKeyByID(keyLockPlacement2.ID);
					dictionary.Add(doorway2, keyByID3);
				}
			}
			List<Doorway> list2 = new List<Doorway>();
			foreach (KeyValuePair<Doorway, Key> keyValuePair in dictionary)
			{
				Doorway key3 = keyValuePair.Key;
				Key key = keyValuePair.Value;
				List<Tile> list3 = new List<Tile>();
				Func<KeyLockPlacement, bool> <>9__9;
				Func<KeyLockPlacement, bool> <>9__10;
				foreach (Tile tile in this.currentDungeon.AllTiles)
				{
					if (tile.Placement.NormalizedPathDepth < key3.Tile.Placement.NormalizedPathDepth)
					{
						bool flag = false;
						if (tile.Node == null)
						{
							goto IL_49D;
						}
						IEnumerable<KeyLockPlacement> keys = tile.Node.Keys;
						Func<KeyLockPlacement, bool> predicate2;
						if ((predicate2 = <>9__9) == null)
						{
							predicate2 = (<>9__9 = ((KeyLockPlacement x) => x.ID == key.ID));
						}
						if (keys.Where(predicate2).Count<KeyLockPlacement>() <= 0)
						{
							goto IL_49D;
						}
						flag = true;
						IL_4E6:
						if (flag && (key3.Tile.Placement.IsOnMainPath || tile.Placement.NormalizedBranchDepth < key3.Tile.Placement.NormalizedBranchDepth))
						{
							list3.Add(tile);
							continue;
						}
						continue;
						IL_49D:
						if (tile.Line == null)
						{
							goto IL_4E6;
						}
						IEnumerable<KeyLockPlacement> keys2 = tile.Line.Keys;
						Func<KeyLockPlacement, bool> predicate3;
						if ((predicate3 = <>9__10) == null)
						{
							predicate3 = (<>9__10 = ((KeyLockPlacement x) => x.ID == key.ID));
						}
						if (keys2.Where(predicate3).Count<KeyLockPlacement>() > 0)
						{
							flag = true;
							goto IL_4E6;
						}
						goto IL_4E6;
					}
				}
				IEnumerable<IKeySpawnable> source = list3.SelectMany((Tile x) => x.GetComponentsInChildren<Component>().OfType<IKeySpawnable>());
				if (source.Count<IKeySpawnable>() == 0)
				{
					list2.Add(key3);
				}
				else
				{
					IKeySpawnable keySpawnable = source.ElementAt(this.RandomStream.Next(0, source.Count<IKeySpawnable>()));
					keySpawnable.SpawnKey(key, this.DungeonFlow.KeyManager);
					foreach (IKeyLock keyLock in (keySpawnable as Component).GetComponentsInChildren<Component>().OfType<IKeyLock>())
					{
						keyLock.OnKeyAssigned(key, this.DungeonFlow.KeyManager);
					}
				}
			}
			foreach (Doorway key2 in list2)
			{
				dictionary.Remove(key2);
			}
			foreach (KeyValuePair<Doorway, Key> keyValuePair2 in dictionary)
			{
				keyValuePair2.Key.RemoveUsedPrefab();
				this.LockDoorway(keyValuePair2.Key, keyValuePair2.Value, this.DungeonFlow.KeyManager);
			}
		}

		// Token: 0x06001390 RID: 5008 RVA: 0x000586BC File Offset: 0x000568BC
		protected virtual void LockDoorway(Doorway doorway, Key key, KeyManager keyManager)
		{
			TilePlacementData placement = doorway.Tile.Placement;
			GameObjectChanceTable[] array = (from x in doorway.Tile.TileSet.LockPrefabs
			where x.SocketGroup == doorway.SocketGroup
			select x.LockPrefabs).ToArray<GameObjectChanceTable>();
			GameObject gameObject = Object.Instantiate<GameObject>(array[this.RandomStream.Next(0, array.Length)].GetRandom(this.RandomStream, placement.IsOnMainPath, placement.NormalizedDepth, false));
			gameObject.transform.parent = doorway.transform;
			gameObject.transform.localPosition = Vector3.zero;
			gameObject.transform.localRotation = Quaternion.identity;
			foreach (IKeyLock keyLock in doorway.GetComponentsInChildren<Component>().OfType<IKeyLock>())
			{
				keyLock.OnKeyAssigned(key, keyManager);
			}
		}

		// Token: 0x04000CD0 RID: 3280
		public int Seed;

		// Token: 0x04000CD1 RID: 3281
		public bool ShouldRandomizeSeed = true;

		// Token: 0x04000CD3 RID: 3283
		public int MaxAttemptCount = 20;

		// Token: 0x04000CD4 RID: 3284
		public bool IgnoreSpriteBounds = true;

		// Token: 0x04000CD5 RID: 3285
		public Vector3 UpVector = Vector3.up;

		// Token: 0x04000CD6 RID: 3286
		public GameObject Root;

		// Token: 0x04000CD7 RID: 3287
		public DungeonFlow DungeonFlow;

		// Token: 0x04000CDC RID: 3292
		protected int retryCount;

		// Token: 0x04000CDD RID: 3293
		protected Dungeon currentDungeon;

		// Token: 0x04000CDE RID: 3294
		protected readonly List<PreProcessTileData> preProcessData = new List<PreProcessTileData>();

		// Token: 0x04000CDF RID: 3295
		protected readonly List<GameObject> useableTiles = new List<GameObject>();

		// Token: 0x04000CE0 RID: 3296
		protected int targetLength;

		// Token: 0x04000CE1 RID: 3297
		private int nextNodeIndex;

		// Token: 0x04000CE2 RID: 3298
		private DungeonArchetype currentArchetype;

		// Token: 0x04000CE3 RID: 3299
		private GraphLine previousLineSegment;

		// Token: 0x04000CE4 RID: 3300
		private bool isAnalysis;

		// Token: 0x020003F9 RID: 1017
		private struct UsedTileSet
		{
			// Token: 0x06001EC4 RID: 7876 RVA: 0x000810F7 File Offset: 0x0007F2F7
			public UsedTileSet(DungeonArchetype archetype)
			{
				this.DungeonArchetypeRes = archetype;
				this.TileSets = new List<TileSet>();
			}

			// Token: 0x04001845 RID: 6213
			public DungeonArchetype DungeonArchetypeRes;

			// Token: 0x04001846 RID: 6214
			public List<TileSet> TileSets;
		}
	}
}

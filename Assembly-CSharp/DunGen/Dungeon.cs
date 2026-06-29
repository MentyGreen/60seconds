using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using DunGen.Graph;
using UnityEngine;

namespace DunGen
{
	// Token: 0x020001D8 RID: 472
	public class Dungeon : MonoBehaviour
	{
		// Token: 0x17000394 RID: 916
		// (get) Token: 0x06001358 RID: 4952 RVA: 0x000563D7 File Offset: 0x000545D7
		// (set) Token: 0x06001359 RID: 4953 RVA: 0x000563DF File Offset: 0x000545DF
		public DungeonFlow DungeonFlow { get; protected set; }

		// Token: 0x17000395 RID: 917
		// (get) Token: 0x0600135A RID: 4954 RVA: 0x000563E8 File Offset: 0x000545E8
		// (set) Token: 0x0600135B RID: 4955 RVA: 0x000563F0 File Offset: 0x000545F0
		public ReadOnlyCollection<Tile> AllTiles { get; private set; }

		// Token: 0x17000396 RID: 918
		// (get) Token: 0x0600135C RID: 4956 RVA: 0x000563F9 File Offset: 0x000545F9
		// (set) Token: 0x0600135D RID: 4957 RVA: 0x00056401 File Offset: 0x00054601
		public ReadOnlyCollection<Tile> MainPathTiles { get; private set; }

		// Token: 0x17000397 RID: 919
		// (get) Token: 0x0600135E RID: 4958 RVA: 0x0005640A File Offset: 0x0005460A
		// (set) Token: 0x0600135F RID: 4959 RVA: 0x00056412 File Offset: 0x00054612
		public ReadOnlyCollection<Tile> BranchPathTiles { get; private set; }

		// Token: 0x17000398 RID: 920
		// (get) Token: 0x06001360 RID: 4960 RVA: 0x0005641B File Offset: 0x0005461B
		// (set) Token: 0x06001361 RID: 4961 RVA: 0x00056423 File Offset: 0x00054623
		public ReadOnlyCollection<DoorwayConnection> Connections { get; private set; }

		// Token: 0x17000399 RID: 921
		// (get) Token: 0x06001362 RID: 4962 RVA: 0x0005642C File Offset: 0x0005462C
		// (set) Token: 0x06001363 RID: 4963 RVA: 0x00056434 File Offset: 0x00054634
		public DungeonGraph ConnectionGraph { get; private set; }

		// Token: 0x06001364 RID: 4964 RVA: 0x00056440 File Offset: 0x00054640
		internal void PreGenerateDungeon(DungeonGenerator dungeonGenerator)
		{
			this.DungeonFlow = dungeonGenerator.DungeonFlow;
			this.AllTiles = new ReadOnlyCollection<Tile>(new Tile[0]);
			this.MainPathTiles = new ReadOnlyCollection<Tile>(new Tile[0]);
			this.BranchPathTiles = new ReadOnlyCollection<Tile>(new Tile[0]);
			this.Connections = new ReadOnlyCollection<DoorwayConnection>(new DoorwayConnection[0]);
		}

		// Token: 0x06001365 RID: 4965 RVA: 0x0005649D File Offset: 0x0005469D
		internal void PostGenerateDungeon(DungeonGenerator dungeonGenerator)
		{
			this.ConnectionGraph = new DungeonGraph(this);
		}

		// Token: 0x06001366 RID: 4966 RVA: 0x000564AC File Offset: 0x000546AC
		public void Clear()
		{
			foreach (Tile tile in this.allTiles)
			{
				Object.DestroyImmediate(tile.gameObject);
			}
			this.allTiles.Clear();
			this.mainPathTiles.Clear();
			this.branchPathTiles.Clear();
			this.connections.Clear();
			this.ExposeRoomProperties();
		}

		// Token: 0x06001367 RID: 4967 RVA: 0x00056534 File Offset: 0x00054734
		internal void MakeConnection(Doorway a, Doorway b, Random randomStream)
		{
			DoorwayConnection item = new DoorwayConnection(a, b);
			a.Tile.Placement.UnusedDoorways.Remove(a);
			a.Tile.Placement.UsedDoorways.Add(a);
			b.Tile.Placement.UnusedDoorways.Remove(b);
			b.Tile.Placement.UsedDoorways.Add(b);
			this.connections.Add(item);
			List<GameObject> list = (a.DoorPrefabs.Count > 0) ? a.DoorPrefabs : b.DoorPrefabs;
			if (list.Count > 0)
			{
				GameObject gameObject = list[randomStream.Next(0, list.Count)];
				if (gameObject != null)
				{
					GameObject gameObject2 = Object.Instantiate<GameObject>(gameObject);
					gameObject2.transform.position = a.transform.position;
					gameObject2.transform.rotation = a.transform.rotation;
					gameObject2.transform.localScale = a.transform.localScale;
					gameObject2.transform.parent = a.transform;
					a.SetUsedPrefab(gameObject2);
					b.SetUsedPrefab(gameObject2);
				}
			}
		}

		// Token: 0x06001368 RID: 4968 RVA: 0x0005665D File Offset: 0x0005485D
		internal void AddTile(Tile tile)
		{
			this.allTiles.Add(tile);
			if (tile.Placement.IsOnMainPath)
			{
				this.mainPathTiles.Add(tile);
				return;
			}
			this.branchPathTiles.Add(tile);
		}

		// Token: 0x06001369 RID: 4969 RVA: 0x00056694 File Offset: 0x00054894
		internal void ExposeRoomProperties()
		{
			this.AllTiles = new ReadOnlyCollection<Tile>(this.allTiles);
			this.MainPathTiles = new ReadOnlyCollection<Tile>(this.mainPathTiles);
			this.BranchPathTiles = new ReadOnlyCollection<Tile>(this.branchPathTiles);
			this.Connections = new ReadOnlyCollection<DoorwayConnection>(this.connections);
		}

		// Token: 0x04000CC9 RID: 3273
		private readonly List<Tile> allTiles = new List<Tile>();

		// Token: 0x04000CCA RID: 3274
		private readonly List<Tile> mainPathTiles = new List<Tile>();

		// Token: 0x04000CCB RID: 3275
		private readonly List<Tile> branchPathTiles = new List<Tile>();

		// Token: 0x04000CCC RID: 3276
		private readonly List<DoorwayConnection> connections = new List<DoorwayConnection>();
	}
}

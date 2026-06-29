using System;
using System.Collections.Generic;
using UnityEngine;

namespace DunGen
{
	// Token: 0x020001F3 RID: 499
	[Serializable]
	public sealed class TilePlacementData
	{
		// Token: 0x170003B8 RID: 952
		// (get) Token: 0x060013F8 RID: 5112 RVA: 0x000596CE File Offset: 0x000578CE
		public GameObject Root
		{
			get
			{
				return this.root;
			}
		}

		// Token: 0x170003B9 RID: 953
		// (get) Token: 0x060013F9 RID: 5113 RVA: 0x000596D6 File Offset: 0x000578D6
		public Tile Tile
		{
			get
			{
				return this.tile;
			}
		}

		// Token: 0x170003BA RID: 954
		// (get) Token: 0x060013FA RID: 5114 RVA: 0x000596DE File Offset: 0x000578DE
		// (set) Token: 0x060013FB RID: 5115 RVA: 0x000596E6 File Offset: 0x000578E6
		public int PathDepth
		{
			get
			{
				return this.pathDepth;
			}
			internal set
			{
				this.pathDepth = value;
			}
		}

		// Token: 0x170003BB RID: 955
		// (get) Token: 0x060013FC RID: 5116 RVA: 0x000596EF File Offset: 0x000578EF
		// (set) Token: 0x060013FD RID: 5117 RVA: 0x000596F7 File Offset: 0x000578F7
		public float NormalizedPathDepth
		{
			get
			{
				return this.normalizedPathDepth;
			}
			internal set
			{
				this.normalizedPathDepth = value;
			}
		}

		// Token: 0x170003BC RID: 956
		// (get) Token: 0x060013FE RID: 5118 RVA: 0x00059700 File Offset: 0x00057900
		// (set) Token: 0x060013FF RID: 5119 RVA: 0x00059708 File Offset: 0x00057908
		public int BranchDepth
		{
			get
			{
				return this.branchDepth;
			}
			internal set
			{
				this.branchDepth = value;
			}
		}

		// Token: 0x170003BD RID: 957
		// (get) Token: 0x06001400 RID: 5120 RVA: 0x00059711 File Offset: 0x00057911
		// (set) Token: 0x06001401 RID: 5121 RVA: 0x00059719 File Offset: 0x00057919
		public float NormalizedBranchDepth
		{
			get
			{
				return this.normalizedBranchDepth;
			}
			internal set
			{
				this.normalizedBranchDepth = value;
			}
		}

		// Token: 0x170003BE RID: 958
		// (get) Token: 0x06001402 RID: 5122 RVA: 0x00059722 File Offset: 0x00057922
		// (set) Token: 0x06001403 RID: 5123 RVA: 0x0005972A File Offset: 0x0005792A
		public bool IsOnMainPath
		{
			get
			{
				return this.isOnMainPath;
			}
			internal set
			{
				this.isOnMainPath = value;
			}
		}

		// Token: 0x170003BF RID: 959
		// (get) Token: 0x06001404 RID: 5124 RVA: 0x00059733 File Offset: 0x00057933
		// (set) Token: 0x06001405 RID: 5125 RVA: 0x0005973B File Offset: 0x0005793B
		public Bounds Bounds
		{
			get
			{
				return this.bounds;
			}
			internal set
			{
				this.bounds = value;
			}
		}

		// Token: 0x170003C0 RID: 960
		// (get) Token: 0x06001406 RID: 5126 RVA: 0x00059744 File Offset: 0x00057944
		public int Depth
		{
			get
			{
				if (!this.isOnMainPath)
				{
					return this.branchDepth;
				}
				return this.pathDepth;
			}
		}

		// Token: 0x170003C1 RID: 961
		// (get) Token: 0x06001407 RID: 5127 RVA: 0x0005975B File Offset: 0x0005795B
		public float NormalizedDepth
		{
			get
			{
				if (!this.isOnMainPath)
				{
					return this.normalizedBranchDepth;
				}
				return this.normalizedPathDepth;
			}
		}

		// Token: 0x06001408 RID: 5128 RVA: 0x00059774 File Offset: 0x00057974
		internal TilePlacementData(PreProcessTileData preProcessData, bool isOnMainPath, DungeonArchetype archetype, TileSet tileSet)
		{
			this.root = Object.Instantiate<GameObject>(preProcessData.Prefab);
			this.Bounds = preProcessData.Proxy.GetComponent<Collider>().bounds;
			this.IsOnMainPath = isOnMainPath;
			this.tile = this.Root.GetComponent<Tile>();
			if (this.tile == null)
			{
				this.tile = this.Root.AddComponent<Tile>();
			}
			this.tile.Placement = this;
			this.tile.Archetype = archetype;
			this.tile.TileSet = tileSet;
			foreach (Doorway doorway in this.Root.GetComponentsInChildren<Doorway>())
			{
				doorway.Tile = this.tile;
				this.AllDoorways.Add(doorway);
			}
			this.UnusedDoorways.AddRange(this.AllDoorways);
			this.root.SetActive(false);
		}

		// Token: 0x06001409 RID: 5129 RVA: 0x00059880 File Offset: 0x00057A80
		public void ProcessDoorways()
		{
			foreach (Doorway doorway in this.UsedDoorways)
			{
				foreach (GameObject obj in doorway.AddWhenNotInUse)
				{
					Object.DestroyImmediate(obj);
				}
			}
			foreach (Doorway doorway2 in this.UnusedDoorways)
			{
				foreach (GameObject obj2 in doorway2.AddWhenInUse)
				{
					Object.DestroyImmediate(obj2);
				}
			}
			foreach (Doorway doorway3 in this.AllDoorways)
			{
				doorway3.placedByGenerator = true;
			}
		}

		// Token: 0x0600140A RID: 5130 RVA: 0x000599C4 File Offset: 0x00057BC4
		public void RecalculateBounds(bool ignoreSpriteRenderers)
		{
			this.Bounds = UnityUtil.CalculateObjectBounds(this.Root, true, ignoreSpriteRenderers);
		}

		// Token: 0x0600140B RID: 5131 RVA: 0x000599DC File Offset: 0x00057BDC
		public Doorway PickRandomDoorway(Random randomStream, bool mustBeAvailable)
		{
			int num = this.PickRandomDoorwayIndex(randomStream, mustBeAvailable);
			if (num != -1)
			{
				return this.AllDoorways[num];
			}
			return null;
		}

		// Token: 0x0600140C RID: 5132 RVA: 0x00059A04 File Offset: 0x00057C04
		public int PickRandomDoorwayIndex(Random randomStream, bool mustBeAvailable)
		{
			List<Doorway> list = mustBeAvailable ? this.UnusedDoorways : this.AllDoorways;
			if (list.Count == 0)
			{
				return -1;
			}
			return this.AllDoorways.IndexOf(list[randomStream.Next(0, list.Count)]);
		}

		// Token: 0x04000D29 RID: 3369
		public List<Doorway> UsedDoorways = new List<Doorway>();

		// Token: 0x04000D2A RID: 3370
		public List<Doorway> UnusedDoorways = new List<Doorway>();

		// Token: 0x04000D2B RID: 3371
		public List<Doorway> AllDoorways = new List<Doorway>();

		// Token: 0x04000D2C RID: 3372
		[SerializeField]
		private int pathDepth;

		// Token: 0x04000D2D RID: 3373
		[SerializeField]
		private float normalizedPathDepth;

		// Token: 0x04000D2E RID: 3374
		[SerializeField]
		private int branchDepth;

		// Token: 0x04000D2F RID: 3375
		[SerializeField]
		private float normalizedBranchDepth;

		// Token: 0x04000D30 RID: 3376
		[SerializeField]
		private bool isOnMainPath;

		// Token: 0x04000D31 RID: 3377
		[SerializeField]
		private Bounds bounds;

		// Token: 0x04000D32 RID: 3378
		[SerializeField]
		private GameObject root;

		// Token: 0x04000D33 RID: 3379
		[SerializeField]
		private Tile tile;
	}
}

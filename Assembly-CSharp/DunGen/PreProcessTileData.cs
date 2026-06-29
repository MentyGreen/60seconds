using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DunGen
{
	// Token: 0x020001EE RID: 494
	public sealed class PreProcessTileData
	{
		// Token: 0x170003B1 RID: 945
		// (get) Token: 0x060013DE RID: 5086 RVA: 0x00059317 File Offset: 0x00057517
		// (set) Token: 0x060013DF RID: 5087 RVA: 0x0005931F File Offset: 0x0005751F
		public GameObject Prefab { get; private set; }

		// Token: 0x170003B2 RID: 946
		// (get) Token: 0x060013E0 RID: 5088 RVA: 0x00059328 File Offset: 0x00057528
		// (set) Token: 0x060013E1 RID: 5089 RVA: 0x00059330 File Offset: 0x00057530
		public GameObject Proxy { get; private set; }

		// Token: 0x060013E2 RID: 5090 RVA: 0x0005933C File Offset: 0x0005753C
		public PreProcessTileData(GameObject prefab, bool ignoreSpriteRendererBounds)
		{
			this.Prefab = prefab;
			this.Proxy = new GameObject(prefab.name + "_PROXY");
			this.CalculateProxyBounds(ignoreSpriteRendererBounds);
			this.GetAllDoorways();
		}

		// Token: 0x060013E3 RID: 5091 RVA: 0x00059394 File Offset: 0x00057594
		public bool ChooseRandomDoorway(Random random, DoorwaySocketType? socketGroupFilter, Vector3? allowedDirection, out int doorwayIndex, out Doorway doorway)
		{
			doorwayIndex = -1;
			doorway = null;
			IEnumerable<Doorway> source = this.Doorways;
			if (socketGroupFilter != null)
			{
				source = from x in source
				where DoorwaySocket.IsMatchingSocket(x.SocketGroup, socketGroupFilter.Value)
				select x;
			}
			if (allowedDirection != null)
			{
				source = from x in source
				where x.transform.forward == allowedDirection
				select x;
			}
			if (source.Count<Doorway>() == 0)
			{
				return false;
			}
			doorway = source.ElementAt(random.Next(0, source.Count<Doorway>()));
			doorwayIndex = this.Doorways.IndexOf(doorway);
			return true;
		}

		// Token: 0x060013E4 RID: 5092 RVA: 0x00059438 File Offset: 0x00057638
		private void CalculateProxyBounds(bool ignoreSpriteRendererBounds)
		{
			Bounds bounds = UnityUtil.CalculateObjectBounds(this.Prefab, true, ignoreSpriteRendererBounds);
			bounds.size *= 0.99f;
			BoxCollider boxCollider = this.Proxy.AddComponent<BoxCollider>();
			boxCollider.center = bounds.center;
			boxCollider.size = bounds.size;
		}

		// Token: 0x060013E5 RID: 5093 RVA: 0x00059490 File Offset: 0x00057690
		private void GetAllDoorways()
		{
			this.DoorwaySockets.Clear();
			foreach (Doorway doorway in this.Prefab.GetComponentsInChildren<Doorway>(true))
			{
				this.Doorways.Add(doorway);
				if (!this.DoorwaySockets.Contains(doorway.SocketGroup))
				{
					this.DoorwaySockets.Add(doorway.SocketGroup);
				}
			}
		}

		// Token: 0x04000D1E RID: 3358
		public readonly List<DoorwaySocketType> DoorwaySockets = new List<DoorwaySocketType>();

		// Token: 0x04000D1F RID: 3359
		public readonly List<Doorway> Doorways = new List<Doorway>();
	}
}

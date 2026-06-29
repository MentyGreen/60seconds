using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000035 RID: 53
[ExecuteInEditMode]
[AddComponentMenu("Daikon Forge/User Interface/Texture Atlas")]
[Serializable]
public class dfAtlas : MonoBehaviour
{
	// Token: 0x1700015F RID: 351
	// (get) Token: 0x060005DB RID: 1499 RVA: 0x0001BCA1 File Offset: 0x00019EA1
	public Texture2D Texture
	{
		get
		{
			if (!(this.replacementAtlas != null))
			{
				return this.material.mainTexture as Texture2D;
			}
			return this.replacementAtlas.Texture;
		}
	}

	// Token: 0x17000160 RID: 352
	// (get) Token: 0x060005DC RID: 1500 RVA: 0x0001BCCD File Offset: 0x00019ECD
	public int Count
	{
		get
		{
			if (!(this.replacementAtlas != null))
			{
				return this.items.Count;
			}
			return this.replacementAtlas.Count;
		}
	}

	// Token: 0x17000161 RID: 353
	// (get) Token: 0x060005DD RID: 1501 RVA: 0x0001BCF4 File Offset: 0x00019EF4
	public List<dfAtlas.ItemInfo> Items
	{
		get
		{
			if (!(this.replacementAtlas != null))
			{
				return this.items;
			}
			return this.replacementAtlas.Items;
		}
	}

	// Token: 0x17000162 RID: 354
	// (get) Token: 0x060005DE RID: 1502 RVA: 0x0001BD16 File Offset: 0x00019F16
	// (set) Token: 0x060005DF RID: 1503 RVA: 0x0001BD38 File Offset: 0x00019F38
	public Material Material
	{
		get
		{
			if (!(this.replacementAtlas != null))
			{
				return this.material;
			}
			return this.replacementAtlas.Material;
		}
		set
		{
			if (this.replacementAtlas != null)
			{
				this.replacementAtlas.Material = value;
				return;
			}
			this.material = value;
		}
	}

	// Token: 0x17000163 RID: 355
	// (get) Token: 0x060005E0 RID: 1504 RVA: 0x0001BD5C File Offset: 0x00019F5C
	// (set) Token: 0x060005E1 RID: 1505 RVA: 0x0001BD64 File Offset: 0x00019F64
	public dfAtlas Replacement
	{
		get
		{
			return this.replacementAtlas;
		}
		set
		{
			this.replacementAtlas = value;
		}
	}

	// Token: 0x17000164 RID: 356
	public dfAtlas.ItemInfo this[string key]
	{
		get
		{
			if (this.replacementAtlas != null)
			{
				return this.replacementAtlas[key];
			}
			if (string.IsNullOrEmpty(key))
			{
				return null;
			}
			if (this.map.Count == 0)
			{
				this.RebuildIndexes();
			}
			dfAtlas.ItemInfo result = null;
			if (this.map.TryGetValue(key, out result))
			{
				return result;
			}
			return null;
		}
	}

	// Token: 0x060005E3 RID: 1507 RVA: 0x0001BDCA File Offset: 0x00019FCA
	internal static bool Equals(dfAtlas lhs, dfAtlas rhs)
	{
		return lhs == rhs || (!(lhs == null) && !(rhs == null) && lhs.material == rhs.material);
	}

	// Token: 0x060005E4 RID: 1508 RVA: 0x0001BDF7 File Offset: 0x00019FF7
	public void AddItem(dfAtlas.ItemInfo item)
	{
		this.items.Add(item);
		this.RebuildIndexes();
	}

	// Token: 0x060005E5 RID: 1509 RVA: 0x0001BE0B File Offset: 0x0001A00B
	public void AddItems(IEnumerable<dfAtlas.ItemInfo> list)
	{
		this.items.AddRange(list);
		this.RebuildIndexes();
	}

	// Token: 0x060005E6 RID: 1510 RVA: 0x0001BE20 File Offset: 0x0001A020
	public void Remove(string name)
	{
		for (int i = this.items.Count - 1; i >= 0; i--)
		{
			if (this.items[i].name == name)
			{
				this.items.RemoveAt(i);
			}
		}
		this.RebuildIndexes();
	}

	// Token: 0x060005E7 RID: 1511 RVA: 0x0001BE70 File Offset: 0x0001A070
	public void RebuildIndexes()
	{
		if (this.map == null)
		{
			this.map = new Dictionary<string, dfAtlas.ItemInfo>();
		}
		else
		{
			this.map.Clear();
		}
		for (int i = 0; i < this.items.Count; i++)
		{
			dfAtlas.ItemInfo itemInfo = this.items[i];
			this.map[itemInfo.name] = itemInfo;
		}
	}

	// Token: 0x04000210 RID: 528
	[SerializeField]
	protected Material material;

	// Token: 0x04000211 RID: 529
	[SerializeField]
	protected List<dfAtlas.ItemInfo> items = new List<dfAtlas.ItemInfo>();

	// Token: 0x04000212 RID: 530
	public dfAtlas.TextureAtlasGenerator generator;

	// Token: 0x04000213 RID: 531
	public string imageFileGUID;

	// Token: 0x04000214 RID: 532
	public string dataFileGUID;

	// Token: 0x04000215 RID: 533
	private Dictionary<string, dfAtlas.ItemInfo> map = new Dictionary<string, dfAtlas.ItemInfo>();

	// Token: 0x04000216 RID: 534
	private dfAtlas replacementAtlas;

	// Token: 0x02000366 RID: 870
	public enum TextureAtlasGenerator
	{
		// Token: 0x04001625 RID: 5669
		Internal,
		// Token: 0x04001626 RID: 5670
		TexturePacker
	}

	// Token: 0x02000367 RID: 871
	[Serializable]
	public class ItemInfo : IComparable<dfAtlas.ItemInfo>, IEquatable<dfAtlas.ItemInfo>
	{
		// Token: 0x06001C5F RID: 7263 RVA: 0x00079389 File Offset: 0x00077589
		public int CompareTo(dfAtlas.ItemInfo other)
		{
			return this.name.CompareTo(other.name);
		}

		// Token: 0x06001C60 RID: 7264 RVA: 0x0007939C File Offset: 0x0007759C
		public override int GetHashCode()
		{
			return this.name.GetHashCode();
		}

		// Token: 0x06001C61 RID: 7265 RVA: 0x000793A9 File Offset: 0x000775A9
		public override bool Equals(object obj)
		{
			return obj is dfAtlas.ItemInfo && this.name.Equals(((dfAtlas.ItemInfo)obj).name);
		}

		// Token: 0x06001C62 RID: 7266 RVA: 0x000793CB File Offset: 0x000775CB
		public bool Equals(dfAtlas.ItemInfo other)
		{
			return this.name.Equals(other.name);
		}

		// Token: 0x06001C63 RID: 7267 RVA: 0x000793DE File Offset: 0x000775DE
		public static bool operator ==(dfAtlas.ItemInfo lhs, dfAtlas.ItemInfo rhs)
		{
			return lhs == rhs || (lhs != null && rhs != null && lhs.name.Equals(rhs.name));
		}

		// Token: 0x06001C64 RID: 7268 RVA: 0x000793FF File Offset: 0x000775FF
		public static bool operator !=(dfAtlas.ItemInfo lhs, dfAtlas.ItemInfo rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x04001627 RID: 5671
		public string name;

		// Token: 0x04001628 RID: 5672
		public Rect region;

		// Token: 0x04001629 RID: 5673
		public RectOffset border = new RectOffset();

		// Token: 0x0400162A RID: 5674
		public bool rotated;

		// Token: 0x0400162B RID: 5675
		public Vector2 sizeInPixels = Vector2.zero;

		// Token: 0x0400162C RID: 5676
		[SerializeField]
		public string textureGUID = "";

		// Token: 0x0400162D RID: 5677
		public bool deleted;

		// Token: 0x0400162E RID: 5678
		[SerializeField]
		public Texture2D texture;
	}
}

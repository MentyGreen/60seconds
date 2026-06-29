using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using UnityEngine;

namespace DunGen
{
	// Token: 0x020001EA RID: 490
	[Serializable]
	public sealed class KeyManager : ScriptableObject
	{
		// Token: 0x170003B0 RID: 944
		// (get) Token: 0x060013D1 RID: 5073 RVA: 0x00058FE2 File Offset: 0x000571E2
		// (set) Token: 0x060013D2 RID: 5074 RVA: 0x00058FEA File Offset: 0x000571EA
		public ReadOnlyCollection<Key> Keys { get; private set; }

		// Token: 0x060013D3 RID: 5075 RVA: 0x00058FF4 File Offset: 0x000571F4
		public Key CreateKey()
		{
			Key key = new Key(this.GetNextAvailableID());
			key.Name = UnityUtil.GetUniqueName("New Key", from x in this.keys
			select x.Name);
			key.Colour = new Color(Random.value, Random.value, Random.value);
			this.keys.Add(key);
			this.ExposeKeyList();
			return key;
		}

		// Token: 0x060013D4 RID: 5076 RVA: 0x00059074 File Offset: 0x00057274
		public void DeleteKey(int index)
		{
			this.keys.RemoveAt(index);
			this.ExposeKeyList();
		}

		// Token: 0x060013D5 RID: 5077 RVA: 0x00059088 File Offset: 0x00057288
		public Key GetKeyByID(int id)
		{
			return (from x in this.keys
			where x.ID == id
			select x).FirstOrDefault<Key>();
		}

		// Token: 0x060013D6 RID: 5078 RVA: 0x000590C0 File Offset: 0x000572C0
		public Key GetKeyByName(string name)
		{
			return (from x in this.keys
			where x.Name == name
			select x).FirstOrDefault<Key>();
		}

		// Token: 0x060013D7 RID: 5079 RVA: 0x000590F8 File Offset: 0x000572F8
		public bool RenameKey(int index, string newName)
		{
			if (this.keys[index].Name == newName)
			{
				return false;
			}
			newName = UnityUtil.GetUniqueName(newName, from x in this.keys
			select x.Name);
			this.keys[index].Name = newName;
			return true;
		}

		// Token: 0x060013D8 RID: 5080 RVA: 0x00059165 File Offset: 0x00057365
		public void ExposeKeyList()
		{
			this.Keys = new ReadOnlyCollection<Key>(this.keys);
		}

		// Token: 0x060013D9 RID: 5081 RVA: 0x00059178 File Offset: 0x00057378
		private int GetNextAvailableID()
		{
			int num = 0;
			foreach (Key key in from x in this.keys
			orderby x.ID
			select x)
			{
				if (key.ID >= num)
				{
					num = key.ID + 1;
				}
			}
			return num;
		}

		// Token: 0x04000D14 RID: 3348
		[SerializeField]
		private List<Key> keys = new List<Key>();
	}
}

using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000103 RID: 259
[Serializable]
public class AudioCollection : ScriptableObject
{
	// Token: 0x06000C76 RID: 3190 RVA: 0x0003663C File Offset: 0x0003483C
	public void Initialize()
	{
		if (this._sfxEntries != null)
		{
			for (int i = 0; i < this._sfxEntries.Length; i++)
			{
				string group = this._sfxEntries[i].Group;
				if (!this._groupedEntries.ContainsKey(group))
				{
					this._groupedEntries.Add(group, new List<AudioEntry>());
				}
				this._groupedEntries[group].Add(this._sfxEntries[i]);
			}
		}
	}

	// Token: 0x06000C77 RID: 3191 RVA: 0x000366AC File Offset: 0x000348AC
	public AudioEntry GetEntry(string name)
	{
		for (int i = 0; i < this._sfxEntries.Length; i++)
		{
			if (this._sfxEntries[i].Name == name)
			{
				return this._sfxEntries[i];
			}
		}
		return null;
	}

	// Token: 0x06000C78 RID: 3192 RVA: 0x000366EB File Offset: 0x000348EB
	public AudioEntry[] GetMusicGroup()
	{
		return this._musicEntries;
	}

	// Token: 0x06000C79 RID: 3193 RVA: 0x000366F3 File Offset: 0x000348F3
	public AudioEntry[] GetGroup(string group)
	{
		if (this._groupedEntries.ContainsKey(group))
		{
			return this._groupedEntries[group].ToArray();
		}
		return null;
	}

	// Token: 0x06000C7A RID: 3194 RVA: 0x00036716 File Offset: 0x00034916
	public AudioEntry GetRandomEntry(string group)
	{
		if (this._groupedEntries.ContainsKey(group))
		{
			return this._groupedEntries[group][Random.Range(0, this._groupedEntries[group].Count)];
		}
		return null;
	}

	// Token: 0x040006AF RID: 1711
	private const string MUSIC_GROUP_TAG = "Music";

	// Token: 0x040006B0 RID: 1712
	[SerializeField]
	private AudioEntry[] _musicEntries;

	// Token: 0x040006B1 RID: 1713
	[SerializeField]
	private AudioEntry[] _sfxEntries;

	// Token: 0x040006B2 RID: 1714
	private Dictionary<string, List<AudioEntry>> _groupedEntries = new Dictionary<string, List<AudioEntry>>();
}

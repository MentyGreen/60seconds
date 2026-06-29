using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000089 RID: 137
public class dfMarkupImageCache
{
	// Token: 0x0600089F RID: 2207 RVA: 0x00026236 File Offset: 0x00024436
	public static void Clear()
	{
		dfMarkupImageCache.cache.Clear();
	}

	// Token: 0x060008A0 RID: 2208 RVA: 0x00026242 File Offset: 0x00024442
	public static void Load(string name, Texture image)
	{
		dfMarkupImageCache.cache[name.ToLowerInvariant()] = image;
	}

	// Token: 0x060008A1 RID: 2209 RVA: 0x00026255 File Offset: 0x00024455
	public static void Unload(string name)
	{
		dfMarkupImageCache.cache.Remove(name.ToLowerInvariant());
	}

	// Token: 0x060008A2 RID: 2210 RVA: 0x00026268 File Offset: 0x00024468
	public static Texture Load(string path)
	{
		path = path.ToLowerInvariant();
		if (dfMarkupImageCache.cache.ContainsKey(path))
		{
			return dfMarkupImageCache.cache[path];
		}
		Texture texture = Resources.Load(path) as Texture;
		if (texture != null)
		{
			dfMarkupImageCache.cache[path] = texture;
		}
		return texture;
	}

	// Token: 0x04000421 RID: 1057
	private static Dictionary<string, Texture> cache = new Dictionary<string, Texture>();
}

using System;
using System.Collections.Generic;

// Token: 0x02000073 RID: 115
internal class dfTempArray<T>
{
	// Token: 0x06000811 RID: 2065 RVA: 0x000237DA File Offset: 0x000219DA
	public static void Clear()
	{
		dfTempArray<T>.cache.Clear();
	}

	// Token: 0x06000812 RID: 2066 RVA: 0x000237E6 File Offset: 0x000219E6
	public static T[] Obtain(int length)
	{
		return dfTempArray<T>.Obtain(length, 128);
	}

	// Token: 0x06000813 RID: 2067 RVA: 0x000237F4 File Offset: 0x000219F4
	public static T[] Obtain(int length, int maxCacheSize)
	{
		List<T[]> obj = dfTempArray<T>.cache;
		T[] result;
		lock (obj)
		{
			for (int i = 0; i < dfTempArray<T>.cache.Count; i++)
			{
				T[] array = dfTempArray<T>.cache[i];
				if (array.Length == length)
				{
					if (i > 0)
					{
						dfTempArray<T>.cache.RemoveAt(i);
						dfTempArray<T>.cache.Insert(0, array);
					}
					return array;
				}
			}
			if (dfTempArray<T>.cache.Count >= maxCacheSize)
			{
				dfTempArray<T>.cache.RemoveAt(dfTempArray<T>.cache.Count - 1);
			}
			T[] array2 = new T[length];
			dfTempArray<T>.cache.Insert(0, array2);
			result = array2;
		}
		return result;
	}

	// Token: 0x040003D8 RID: 984
	private static List<T[]> cache = new List<T[]>(32);
}

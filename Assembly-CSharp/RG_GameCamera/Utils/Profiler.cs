using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace RG_GameCamera.Utils
{
	// Token: 0x02000188 RID: 392
	public static class Profiler
	{
		// Token: 0x06001164 RID: 4452 RVA: 0x00049170 File Offset: 0x00047370
		public static void Start(string key)
		{
			Stopwatch stopwatch = null;
			if (Profiler.timeSegments.TryGetValue(key, out stopwatch))
			{
				stopwatch.Reset();
				stopwatch.Start();
				return;
			}
			stopwatch = new Stopwatch();
			stopwatch.Start();
			Profiler.timeSegments.Add(key, stopwatch);
		}

		// Token: 0x06001165 RID: 4453 RVA: 0x000491B3 File Offset: 0x000473B3
		public static void Stop(string key)
		{
			Profiler.timeSegments[key].Stop();
		}

		// Token: 0x06001166 RID: 4454 RVA: 0x000491C8 File Offset: 0x000473C8
		public static string[] GetResults()
		{
			string[] array = new string[Profiler.timeSegments.Count];
			int num = 0;
			foreach (KeyValuePair<string, Stopwatch> keyValuePair in Profiler.timeSegments)
			{
				long elapsedMilliseconds = keyValuePair.Value.ElapsedMilliseconds;
				long num2 = keyValuePair.Value.ElapsedTicks / (Stopwatch.Frequency / 1000000L);
				array[num++] = string.Concat(new string[]
				{
					keyValuePair.Key,
					" ",
					elapsedMilliseconds.ToString(),
					" [ms] | ",
					num2.ToString(),
					" [us]"
				});
			}
			return array;
		}

		// Token: 0x04000B23 RID: 2851
		private static readonly Dictionary<string, Stopwatch> timeSegments = new Dictionary<string, Stopwatch>();
	}
}

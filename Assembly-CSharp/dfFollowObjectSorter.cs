using System;
using UnityEngine;

// Token: 0x020000B6 RID: 182
public class dfFollowObjectSorter : MonoBehaviour
{
	// Token: 0x1700022F RID: 559
	// (get) Token: 0x06000A4F RID: 2639 RVA: 0x0002D294 File Offset: 0x0002B494
	public static dfFollowObjectSorter Instance
	{
		get
		{
			Type typeFromHandle = typeof(dfFollowObjectSorter);
			dfFollowObjectSorter instance;
			lock (typeFromHandle)
			{
				if (dfFollowObjectSorter._instance == null && Application.isPlaying)
				{
					dfFollowObjectSorter._instance = new GameObject("Follow Object Sorter").AddComponent<dfFollowObjectSorter>();
					dfFollowObjectSorter.list.Clear();
				}
				instance = dfFollowObjectSorter._instance;
			}
			return instance;
		}
	}

	// Token: 0x06000A50 RID: 2640 RVA: 0x0002D30C File Offset: 0x0002B50C
	public static void Register(dfFollowObject follow)
	{
		if (Application.isPlaying)
		{
			dfFollowObjectSorter.Instance.register(follow);
		}
	}

	// Token: 0x06000A51 RID: 2641 RVA: 0x0002D320 File Offset: 0x0002B520
	public static void Unregister(dfFollowObject follow)
	{
		for (int i = 0; i < dfFollowObjectSorter.list.Count; i++)
		{
			if (dfFollowObjectSorter.list[i].follow == follow)
			{
				dfFollowObjectSorter.list.RemoveAt(i);
				return;
			}
		}
	}

	// Token: 0x06000A52 RID: 2642 RVA: 0x0002D368 File Offset: 0x0002B568
	public void Update()
	{
		int num = int.MaxValue;
		for (int i = 0; i < dfFollowObjectSorter.list.Count; i++)
		{
			dfFollowObjectSorter.FollowSortRecord followSortRecord = dfFollowObjectSorter.list[i];
			followSortRecord.distance = this.getDistance(followSortRecord.follow);
			if (followSortRecord.control.ZOrder < num)
			{
				num = followSortRecord.control.ZOrder;
			}
		}
		dfFollowObjectSorter.list.Sort();
		for (int j = 0; j < dfFollowObjectSorter.list.Count; j++)
		{
			dfFollowObjectSorter.list[j].control.ZOrder = num++;
		}
	}

	// Token: 0x06000A53 RID: 2643 RVA: 0x0002D404 File Offset: 0x0002B604
	private void register(dfFollowObject follow)
	{
		for (int i = 0; i < dfFollowObjectSorter.list.Count; i++)
		{
			if (dfFollowObjectSorter.list[i].follow == follow)
			{
				return;
			}
		}
		dfFollowObjectSorter.list.Add(new dfFollowObjectSorter.FollowSortRecord(follow));
	}

	// Token: 0x06000A54 RID: 2644 RVA: 0x0002D450 File Offset: 0x0002B650
	private float getDistance(dfFollowObject follow)
	{
		return (follow.mainCamera.transform.position - follow.attach.transform.position).sqrMagnitude;
	}

	// Token: 0x040004FA RID: 1274
	private static dfFollowObjectSorter _instance;

	// Token: 0x040004FB RID: 1275
	private static dfList<dfFollowObjectSorter.FollowSortRecord> list = new dfList<dfFollowObjectSorter.FollowSortRecord>();

	// Token: 0x02000386 RID: 902
	private class FollowSortRecord : IComparable<dfFollowObjectSorter.FollowSortRecord>
	{
		// Token: 0x06001D0F RID: 7439 RVA: 0x0007CD5D File Offset: 0x0007AF5D
		public FollowSortRecord(dfFollowObject follow)
		{
			this.follow = follow;
			this.control = follow.GetComponent<dfControl>();
		}

		// Token: 0x06001D10 RID: 7440 RVA: 0x0007CD78 File Offset: 0x0007AF78
		public int CompareTo(dfFollowObjectSorter.FollowSortRecord other)
		{
			return other.distance.CompareTo(this.distance);
		}

		// Token: 0x04001694 RID: 5780
		public float distance;

		// Token: 0x04001695 RID: 5781
		public dfFollowObject follow;

		// Token: 0x04001696 RID: 5782
		public dfControl control;
	}
}

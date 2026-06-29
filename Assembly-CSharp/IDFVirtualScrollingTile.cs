using System;

// Token: 0x02000006 RID: 6
public interface IDFVirtualScrollingTile
{
	// Token: 0x1700000D RID: 13
	// (get) Token: 0x06000034 RID: 52
	// (set) Token: 0x06000035 RID: 53
	int VirtualScrollItemIndex { get; set; }

	// Token: 0x06000036 RID: 54
	void OnScrollPanelItemVirtualize(object backingListItem);

	// Token: 0x06000037 RID: 55
	dfPanel GetDfPanel();
}

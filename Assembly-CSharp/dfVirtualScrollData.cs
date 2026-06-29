using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200007E RID: 126
public class dfVirtualScrollData<T>
{
	// Token: 0x06000842 RID: 2114 RVA: 0x00024228 File Offset: 0x00022428
	public void GetNewLimits(bool isVerticalFlow, bool getMaxes, out int index, out float newY)
	{
		IDFVirtualScrollingTile idfvirtualScrollingTile = this.Tiles[0];
		index = idfvirtualScrollingTile.VirtualScrollItemIndex;
		newY = (isVerticalFlow ? idfvirtualScrollingTile.GetDfPanel().RelativePosition.y : idfvirtualScrollingTile.GetDfPanel().RelativePosition.x);
		foreach (IDFVirtualScrollingTile idfvirtualScrollingTile2 in this.Tiles)
		{
			dfPanel dfPanel = idfvirtualScrollingTile2.GetDfPanel();
			float num = isVerticalFlow ? dfPanel.RelativePosition.y : dfPanel.RelativePosition.x;
			if (getMaxes)
			{
				if (num > newY)
				{
					newY = num;
				}
				if (idfvirtualScrollingTile2.VirtualScrollItemIndex > index)
				{
					index = idfvirtualScrollingTile2.VirtualScrollItemIndex;
				}
			}
			else
			{
				if (num < newY)
				{
					newY = num;
				}
				if (idfvirtualScrollingTile2.VirtualScrollItemIndex < index)
				{
					index = idfvirtualScrollingTile2.VirtualScrollItemIndex;
				}
			}
		}
		if (getMaxes)
		{
			index++;
			return;
		}
		index--;
	}

	// Token: 0x040003DE RID: 990
	public IList<T> BackingList;

	// Token: 0x040003DF RID: 991
	public List<IDFVirtualScrollingTile> Tiles = new List<IDFVirtualScrollingTile>();

	// Token: 0x040003E0 RID: 992
	public RectOffset ItemPadding;

	// Token: 0x040003E1 RID: 993
	public Vector2 LastScrollPosition = Vector2.zero;

	// Token: 0x040003E2 RID: 994
	public int MaxExtraOffscreenTiles = 10;

	// Token: 0x040003E3 RID: 995
	public IDFVirtualScrollingTile DummyTop;

	// Token: 0x040003E4 RID: 996
	public IDFVirtualScrollingTile DummyBottom;

	// Token: 0x040003E5 RID: 997
	public bool IsPaging;

	// Token: 0x040003E6 RID: 998
	public bool IsInitialized;
}

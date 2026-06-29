using System;

// Token: 0x02000064 RID: 100
public static class dfFontManager
{
	// Token: 0x060006E7 RID: 1767 RVA: 0x0001DA44 File Offset: 0x0001BC44
	public static void FlagPendingRequests(dfFontBase font)
	{
		dfDynamicFont dfDynamicFont = font as dfDynamicFont;
		if (dfDynamicFont != null && !dfFontManager.rebuildList.Contains(dfDynamicFont))
		{
			dfFontManager.rebuildList.Add(dfDynamicFont);
		}
	}

	// Token: 0x060006E8 RID: 1768 RVA: 0x0001DA79 File Offset: 0x0001BC79
	public static void Invalidate(dfFontBase font)
	{
		if (font == null || !(font is dfDynamicFont))
		{
			return;
		}
		if (!dfFontManager.dirty.Contains(font))
		{
			dfFontManager.dirty.Add(font);
		}
	}

	// Token: 0x060006E9 RID: 1769 RVA: 0x0001DAA5 File Offset: 0x0001BCA5
	public static bool IsDirty(dfFontBase font)
	{
		return dfFontManager.dirty.Contains(font);
	}

	// Token: 0x060006EA RID: 1770 RVA: 0x0001DAB4 File Offset: 0x0001BCB4
	public static bool RebuildDynamicFonts()
	{
		dfFontManager.rebuildList.Clear();
		dfList<dfControl> activeInstances = dfControl.ActiveInstances;
		for (int i = 0; i < activeInstances.Count; i++)
		{
			IRendersText rendersText = activeInstances[i] as IRendersText;
			if (rendersText != null)
			{
				rendersText.UpdateFontInfo();
			}
		}
		bool result = dfFontManager.rebuildList.Count > 0;
		for (int j = 0; j < dfFontManager.rebuildList.Count; j++)
		{
			dfDynamicFont dfDynamicFont = dfFontManager.rebuildList[j] as dfDynamicFont;
			if (dfDynamicFont != null)
			{
				dfDynamicFont.FlushCharacterRequests();
			}
		}
		dfFontManager.rebuildList.Clear();
		dfFontManager.dirty.Clear();
		return result;
	}

	// Token: 0x040002B2 RID: 690
	private static dfList<dfFontBase> dirty = new dfList<dfFontBase>();

	// Token: 0x040002B3 RID: 691
	private static dfList<dfFontBase> rebuildList = new dfList<dfFontBase>();
}

using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

// Token: 0x0200010D RID: 269
[Serializable]
public struct SDifficultyData
{
	// Token: 0x170002AE RID: 686
	// (get) Token: 0x06000D02 RID: 3330 RVA: 0x000370F4 File Offset: 0x000352F4
	// (set) Token: 0x06000D03 RID: 3331 RVA: 0x000370FC File Offset: 0x000352FC
	public int PrepareTime
	{
		get
		{
			return this._prepareTime;
		}
		set
		{
			this._prepareTime = value;
		}
	}

	// Token: 0x170002AF RID: 687
	// (get) Token: 0x06000D04 RID: 3332 RVA: 0x00037105 File Offset: 0x00035305
	// (set) Token: 0x06000D05 RID: 3333 RVA: 0x0003710D File Offset: 0x0003530D
	public int ScavengeTime
	{
		get
		{
			return this._scavengeTime;
		}
		set
		{
			this._scavengeTime = value;
		}
	}

	// Token: 0x170002B0 RID: 688
	// (get) Token: 0x06000D06 RID: 3334 RVA: 0x00037116 File Offset: 0x00035316
	// (set) Token: 0x06000D07 RID: 3335 RVA: 0x0003711E File Offset: 0x0003531E
	public SPairValue ItemsCollected
	{
		get
		{
			return this._itemsCollected;
		}
		set
		{
			this._itemsCollected = value;
		}
	}

	// Token: 0x170002B1 RID: 689
	// (get) Token: 0x06000D08 RID: 3336 RVA: 0x00037127 File Offset: 0x00035327
	// (set) Token: 0x06000D09 RID: 3337 RVA: 0x0003712F File Offset: 0x0003532F
	public SPairValue FamilyMembersCollected
	{
		get
		{
			return this._familyMembersCollected;
		}
		set
		{
			this._familyMembersCollected = value;
		}
	}

	// Token: 0x170002B2 RID: 690
	// (get) Token: 0x06000D0A RID: 3338 RVA: 0x00037138 File Offset: 0x00035338
	// (set) Token: 0x06000D0B RID: 3339 RVA: 0x00037140 File Offset: 0x00035340
	public SPairValue FoodCollected
	{
		get
		{
			return this._foodCollected;
		}
		set
		{
			this._foodCollected = value;
		}
	}

	// Token: 0x170002B3 RID: 691
	// (get) Token: 0x06000D0C RID: 3340 RVA: 0x00037149 File Offset: 0x00035349
	// (set) Token: 0x06000D0D RID: 3341 RVA: 0x00037151 File Offset: 0x00035351
	public SPairValue WaterCollected
	{
		get
		{
			return this._waterCollected;
		}
		set
		{
			this._waterCollected = value;
		}
	}

	// Token: 0x170002B4 RID: 692
	// (get) Token: 0x06000D0E RID: 3342 RVA: 0x0003715A File Offset: 0x0003535A
	// (set) Token: 0x06000D0F RID: 3343 RVA: 0x00037162 File Offset: 0x00035362
	public SPairValue SuitcaseStock
	{
		get
		{
			return this._suitcaseStock;
		}
		set
		{
			this._suitcaseStock = value;
		}
	}

	// Token: 0x170002B5 RID: 693
	// (get) Token: 0x06000D10 RID: 3344 RVA: 0x0003716B File Offset: 0x0003536B
	// (set) Token: 0x06000D11 RID: 3345 RVA: 0x00037173 File Offset: 0x00035373
	public SPairValue ExpeditionSicknessFalloutChance
	{
		get
		{
			return this._expeditionSicknessFalloutChance;
		}
		set
		{
			this._expeditionSicknessFalloutChance = value;
		}
	}

	// Token: 0x170002B6 RID: 694
	// (get) Token: 0x06000D12 RID: 3346 RVA: 0x0003717C File Offset: 0x0003537C
	// (set) Token: 0x06000D13 RID: 3347 RVA: 0x00037184 File Offset: 0x00035384
	public SPairValue ExpeditionSicknessPostFalloutChance
	{
		get
		{
			return this._expeditionSicknessPostFalloutChance;
		}
		set
		{
			this._expeditionSicknessPostFalloutChance = value;
		}
	}

	// Token: 0x170002B7 RID: 695
	// (get) Token: 0x06000D14 RID: 3348 RVA: 0x0003718D File Offset: 0x0003538D
	// (set) Token: 0x06000D15 RID: 3349 RVA: 0x00037195 File Offset: 0x00035395
	public SPairValue ExpeditionRaidersFollowChance
	{
		get
		{
			return this._expeditionRaidersFollowChance;
		}
		set
		{
			this._expeditionRaidersFollowChance = value;
		}
	}

	// Token: 0x170002B8 RID: 696
	// (get) Token: 0x06000D16 RID: 3350 RVA: 0x0003719E File Offset: 0x0003539E
	// (set) Token: 0x06000D17 RID: 3351 RVA: 0x000371A6 File Offset: 0x000353A6
	public SPairValue ExpeditionInventoryDmgChance
	{
		get
		{
			return this._expeditionInventoryDmgChance;
		}
		set
		{
			this._expeditionInventoryDmgChance = value;
		}
	}

	// Token: 0x170002B9 RID: 697
	// (get) Token: 0x06000D18 RID: 3352 RVA: 0x000371AF File Offset: 0x000353AF
	// (set) Token: 0x06000D19 RID: 3353 RVA: 0x000371B7 File Offset: 0x000353B7
	public int StartDay
	{
		get
		{
			return this._startDay;
		}
		set
		{
			this._startDay = value;
		}
	}

	// Token: 0x170002BA RID: 698
	// (get) Token: 0x06000D1A RID: 3354 RVA: 0x000371C0 File Offset: 0x000353C0
	public List<GameObject> ExactItemsCollected
	{
		get
		{
			return this._exactItemsCollected;
		}
	}

	// Token: 0x170002BB RID: 699
	// (get) Token: 0x06000D1B RID: 3355 RVA: 0x000371C8 File Offset: 0x000353C8
	public List<string> SurvivalParams
	{
		get
		{
			return this._survivalParams;
		}
	}

	// Token: 0x170002BC RID: 700
	// (get) Token: 0x06000D1C RID: 3356 RVA: 0x000371D0 File Offset: 0x000353D0
	public List<SurvivalCondition> SurvivalConditions
	{
		get
		{
			return this._survivalConditions;
		}
	}

	// Token: 0x170002BD RID: 701
	// (get) Token: 0x06000D1D RID: 3357 RVA: 0x000371D8 File Offset: 0x000353D8
	public bool DayOneDeathsIgnored
	{
		get
		{
			return this._dayOneDeathsIgnored;
		}
	}

	// Token: 0x170002BE RID: 702
	// (get) Token: 0x06000D1E RID: 3358 RVA: 0x000371E0 File Offset: 0x000353E0
	public bool LastSurvivorStaysSane
	{
		get
		{
			return this._lastSurvivorStaysSane;
		}
	}

	// Token: 0x06000D1F RID: 3359 RVA: 0x000371E8 File Offset: 0x000353E8
	public string GetText()
	{
		StringBuilder stringBuilder = new StringBuilder();
		if (this._prepareTime > 0)
		{
			stringBuilder.AppendFormat(Settings.Data.LocalizationManager.GetValue("menu_game_diff_exploretime"), this._prepareTime);
			stringBuilder.Append("\n");
		}
		if (this._scavengeTime > 0)
		{
			stringBuilder.AppendFormat(Settings.Data.LocalizationManager.GetValue("menu_game_diff_scavengetime"), this._scavengeTime);
			stringBuilder.Append("\n");
		}
		if (this._foodCollected.Available)
		{
			string empty = string.Empty;
			string empty2 = string.Empty;
			string empty3 = string.Empty;
			this._foodCollected.GetValues(out empty, out empty3, out empty2);
			stringBuilder.AppendFormat(Settings.Data.LocalizationManager.GetValue("menu_game_diff_food"), empty, empty3, empty2);
			stringBuilder.Append("\n");
		}
		if (this._waterCollected.Available)
		{
			string empty4 = string.Empty;
			string empty5 = string.Empty;
			string empty6 = string.Empty;
			this._waterCollected.GetValues(out empty4, out empty6, out empty5);
			stringBuilder.AppendFormat(Settings.Data.LocalizationManager.GetValue("menu_game_diff_water"), empty4, empty6, empty5);
			stringBuilder.Append("\n");
		}
		if (this._itemsCollected.Available)
		{
			string empty7 = string.Empty;
			string empty8 = string.Empty;
			string empty9 = string.Empty;
			this._itemsCollected.GetValues(out empty7, out empty9, out empty8);
			stringBuilder.AppendFormat(Settings.Data.LocalizationManager.GetValue("menu_game_diff_items"), empty7, empty9, empty8);
			stringBuilder.Append("\n");
		}
		if (this._familyMembersCollected.Available)
		{
			string empty10 = string.Empty;
			string empty11 = string.Empty;
			string empty12 = string.Empty;
			this._familyMembersCollected.GetValues(out empty10, out empty12, out empty11);
			stringBuilder.AppendFormat(Settings.Data.LocalizationManager.GetValue("menu_game_diff_family"), empty10, empty12, empty11);
			stringBuilder.Append("\n");
		}
		return stringBuilder.ToString();
	}

	// Token: 0x04000712 RID: 1810
	[SerializeField]
	private int _prepareTime;

	// Token: 0x04000713 RID: 1811
	[SerializeField]
	private int _scavengeTime;

	// Token: 0x04000714 RID: 1812
	[SerializeField]
	private SPairValue _itemsCollected;

	// Token: 0x04000715 RID: 1813
	[SerializeField]
	private SPairValue _familyMembersCollected;

	// Token: 0x04000716 RID: 1814
	[SerializeField]
	private SPairValue _foodCollected;

	// Token: 0x04000717 RID: 1815
	[SerializeField]
	private SPairValue _waterCollected;

	// Token: 0x04000718 RID: 1816
	[SerializeField]
	private SPairValue _suitcaseStock;

	// Token: 0x04000719 RID: 1817
	[SerializeField]
	private SPairValue _expeditionSicknessFalloutChance;

	// Token: 0x0400071A RID: 1818
	[SerializeField]
	private SPairValue _expeditionSicknessPostFalloutChance;

	// Token: 0x0400071B RID: 1819
	[SerializeField]
	private SPairValue _expeditionRaidersFollowChance;

	// Token: 0x0400071C RID: 1820
	[SerializeField]
	private SPairValue _expeditionInventoryDmgChance;

	// Token: 0x0400071D RID: 1821
	[SerializeField]
	private int _startDay;

	// Token: 0x0400071E RID: 1822
	[SerializeField]
	private List<GameObject> _exactItemsCollected;

	// Token: 0x0400071F RID: 1823
	[SerializeField]
	private List<string> _survivalParams;

	// Token: 0x04000720 RID: 1824
	[SerializeField]
	private List<SurvivalCondition> _survivalConditions;

	// Token: 0x04000721 RID: 1825
	[SerializeField]
	public bool _dayOneDeathsIgnored;

	// Token: 0x04000722 RID: 1826
	[SerializeField]
	public bool _lastSurvivorStaysSane;
}

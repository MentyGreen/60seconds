using System;
using RG.Parsecs.Common;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000144 RID: 324
public class RemasterAchievementTooltipContent : TooltipContent
{
	// Token: 0x1700033E RID: 830
	// (get) Token: 0x06000F9A RID: 3994 RVA: 0x00040EB1 File Offset: 0x0003F0B1
	public Achievement Achievement
	{
		get
		{
			return this._achievement;
		}
	}

	// Token: 0x06000F9B RID: 3995 RVA: 0x00040EB9 File Offset: 0x0003F0B9
	private void Start()
	{
		if (!this._achievement.IsAchieved)
		{
			this._icon.color = Color.clear;
		}
	}

	// Token: 0x06000F9C RID: 3996 RVA: 0x00040ED8 File Offset: 0x0003F0D8
	public override bool IsValid()
	{
		return this._achievement != null;
	}

	// Token: 0x0400099C RID: 2460
	[SerializeField]
	private Achievement _achievement;

	// Token: 0x0400099D RID: 2461
	[SerializeField]
	private Image _icon;
}

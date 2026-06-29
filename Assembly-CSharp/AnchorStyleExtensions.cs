using System;

// Token: 0x0200004B RID: 75
public static class AnchorStyleExtensions
{
	// Token: 0x0600061E RID: 1566 RVA: 0x0001CDDD File Offset: 0x0001AFDD
	public static bool IsFlagSet(this dfAnchorStyle value, dfAnchorStyle flag)
	{
		return flag == (value & flag);
	}

	// Token: 0x0600061F RID: 1567 RVA: 0x0001CDE5 File Offset: 0x0001AFE5
	public static bool IsAnyFlagSet(this dfAnchorStyle value, dfAnchorStyle flag)
	{
		return (value & flag) > dfAnchorStyle.None;
	}

	// Token: 0x06000620 RID: 1568 RVA: 0x0001CDED File Offset: 0x0001AFED
	public static dfAnchorStyle SetFlag(this dfAnchorStyle value, dfAnchorStyle flag)
	{
		return value | flag;
	}

	// Token: 0x06000621 RID: 1569 RVA: 0x0001CDF2 File Offset: 0x0001AFF2
	public static dfAnchorStyle SetFlag(this dfAnchorStyle value, dfAnchorStyle flag, bool on)
	{
		if (on)
		{
			return value | flag;
		}
		return value & ~flag;
	}
}

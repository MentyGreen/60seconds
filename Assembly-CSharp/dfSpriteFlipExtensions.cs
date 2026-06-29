using System;

// Token: 0x0200004D RID: 77
public static class dfSpriteFlipExtensions
{
	// Token: 0x06000622 RID: 1570 RVA: 0x0001CDFF File Offset: 0x0001AFFF
	public static bool IsSet(this dfSpriteFlip value, dfSpriteFlip flag)
	{
		return flag == (value & flag);
	}

	// Token: 0x06000623 RID: 1571 RVA: 0x0001CE07 File Offset: 0x0001B007
	public static dfSpriteFlip SetFlag(this dfSpriteFlip value, dfSpriteFlip flag, bool on)
	{
		if (on)
		{
			return value | flag;
		}
		return value & ~flag;
	}
}

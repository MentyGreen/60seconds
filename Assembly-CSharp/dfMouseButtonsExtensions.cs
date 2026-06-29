using System;

// Token: 0x02000044 RID: 68
public static class dfMouseButtonsExtensions
{
	// Token: 0x0600061B RID: 1563 RVA: 0x0001CD75 File Offset: 0x0001AF75
	public static bool IsSet(this dfMouseButtons value, dfMouseButtons flag)
	{
		return flag == (value & flag);
	}
}

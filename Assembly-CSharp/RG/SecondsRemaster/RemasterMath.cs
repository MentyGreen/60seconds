using System;

namespace RG.SecondsRemaster
{
	// Token: 0x0200024A RID: 586
	public static class RemasterMath
	{
		// Token: 0x06001615 RID: 5653 RVA: 0x00060DC8 File Offset: 0x0005EFC8
		public static int Modulo(int a, int b)
		{
			if (a < 0)
			{
				return b + a % b;
			}
			return a % b;
		}
	}
}

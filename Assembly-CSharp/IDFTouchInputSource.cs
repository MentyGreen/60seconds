using System;
using System.Collections.Generic;

// Token: 0x0200002D RID: 45
public interface IDFTouchInputSource
{
	// Token: 0x1700013C RID: 316
	// (get) Token: 0x06000581 RID: 1409
	int TouchCount { get; }

	// Token: 0x1700013D RID: 317
	// (get) Token: 0x06000582 RID: 1410
	IList<dfTouchInfo> Touches { get; }

	// Token: 0x06000583 RID: 1411
	void Update();

	// Token: 0x06000584 RID: 1412
	dfTouchInfo GetTouch(int index);
}

using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200002E RID: 46
public class dfMobileTouchInputSource : IDFTouchInputSource
{
	// Token: 0x1700013E RID: 318
	// (get) Token: 0x06000585 RID: 1413 RVA: 0x0001ACEC File Offset: 0x00018EEC
	public static dfMobileTouchInputSource Instance
	{
		get
		{
			if (dfMobileTouchInputSource.instance == null)
			{
				dfMobileTouchInputSource.instance = new dfMobileTouchInputSource();
			}
			return dfMobileTouchInputSource.instance;
		}
	}

	// Token: 0x1700013F RID: 319
	// (get) Token: 0x06000586 RID: 1414 RVA: 0x0001AD04 File Offset: 0x00018F04
	public int TouchCount
	{
		get
		{
			return Input.touchCount;
		}
	}

	// Token: 0x17000140 RID: 320
	// (get) Token: 0x06000587 RID: 1415 RVA: 0x0001AD0B File Offset: 0x00018F0B
	public IList<dfTouchInfo> Touches
	{
		get
		{
			return this.activeTouches;
		}
	}

	// Token: 0x06000588 RID: 1416 RVA: 0x0001AD13 File Offset: 0x00018F13
	public dfTouchInfo GetTouch(int index)
	{
		return Input.GetTouch(index);
	}

	// Token: 0x06000589 RID: 1417 RVA: 0x0001AD20 File Offset: 0x00018F20
	public void Update()
	{
		this.activeTouches.Clear();
		for (int i = 0; i < this.TouchCount; i++)
		{
			this.activeTouches.Add(this.GetTouch(i));
		}
	}

	// Token: 0x040001E3 RID: 483
	private static dfMobileTouchInputSource instance;

	// Token: 0x040001E4 RID: 484
	private List<dfTouchInfo> activeTouches = new List<dfTouchInfo>();
}

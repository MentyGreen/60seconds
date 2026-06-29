using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Token: 0x0200005F RID: 95
public class dfTouchEventArgs : dfMouseEventArgs
{
	// Token: 0x17000186 RID: 390
	// (get) Token: 0x0600067F RID: 1663 RVA: 0x0001D483 File Offset: 0x0001B683
	// (set) Token: 0x06000680 RID: 1664 RVA: 0x0001D48B File Offset: 0x0001B68B
	public dfTouchInfo Touch { get; private set; }

	// Token: 0x17000187 RID: 391
	// (get) Token: 0x06000681 RID: 1665 RVA: 0x0001D494 File Offset: 0x0001B694
	// (set) Token: 0x06000682 RID: 1666 RVA: 0x0001D49C File Offset: 0x0001B69C
	public List<dfTouchInfo> Touches { get; private set; }

	// Token: 0x17000188 RID: 392
	// (get) Token: 0x06000683 RID: 1667 RVA: 0x0001D4A5 File Offset: 0x0001B6A5
	public bool IsMultiTouch
	{
		get
		{
			return this.Touches.Count > 1;
		}
	}

	// Token: 0x06000684 RID: 1668 RVA: 0x0001D4B8 File Offset: 0x0001B6B8
	public dfTouchEventArgs(dfControl Source, dfTouchInfo touch, Ray ray) : base(Source, dfMouseButtons.Left, touch.tapCount, ray, touch.position, 0f)
	{
		this.Touch = touch;
		this.Touches = new List<dfTouchInfo>
		{
			touch
		};
		float deltaTime = Time.deltaTime;
		if (touch.deltaTime > 1E-45f && deltaTime > 1E-45f)
		{
			base.MoveDelta = touch.deltaPosition * (deltaTime / touch.deltaTime);
			return;
		}
		base.MoveDelta = touch.deltaPosition;
	}

	// Token: 0x06000685 RID: 1669 RVA: 0x0001D53F File Offset: 0x0001B73F
	public dfTouchEventArgs(dfControl source, List<dfTouchInfo> touches, Ray ray) : this(source, touches.First<dfTouchInfo>(), ray)
	{
		this.Touches = touches;
	}

	// Token: 0x06000686 RID: 1670 RVA: 0x0001D556 File Offset: 0x0001B756
	public dfTouchEventArgs(dfControl Source) : base(Source)
	{
		base.Position = Vector2.zero;
	}
}

using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200002F RID: 47
public class dfMouseTouchInputSource : IDFTouchInputSource
{
	// Token: 0x17000141 RID: 321
	// (get) Token: 0x0600058B RID: 1419 RVA: 0x0001AD6E File Offset: 0x00018F6E
	// (set) Token: 0x0600058C RID: 1420 RVA: 0x0001AD76 File Offset: 0x00018F76
	public bool MirrorAlt { get; set; }

	// Token: 0x17000142 RID: 322
	// (get) Token: 0x0600058D RID: 1421 RVA: 0x0001AD7F File Offset: 0x00018F7F
	// (set) Token: 0x0600058E RID: 1422 RVA: 0x0001AD87 File Offset: 0x00018F87
	public bool ParallelAlt { get; set; }

	// Token: 0x17000143 RID: 323
	// (get) Token: 0x0600058F RID: 1423 RVA: 0x0001AD90 File Offset: 0x00018F90
	public int TouchCount
	{
		get
		{
			int num = 0;
			if (this.touch != null)
			{
				num++;
			}
			if (this.altTouch != null)
			{
				num++;
			}
			return num;
		}
	}

	// Token: 0x17000144 RID: 324
	// (get) Token: 0x06000590 RID: 1424 RVA: 0x0001ADB8 File Offset: 0x00018FB8
	public IList<dfTouchInfo> Touches
	{
		get
		{
			this.activeTouches.Clear();
			if (this.touch != null)
			{
				this.activeTouches.Add(this.touch);
			}
			if (this.altTouch != null)
			{
				this.activeTouches.Add(this.altTouch);
			}
			return this.activeTouches;
		}
	}

	// Token: 0x06000591 RID: 1425 RVA: 0x0001AE14 File Offset: 0x00019014
	public void Update()
	{
		if (!Input.GetKey(KeyCode.LeftAlt) || !Input.GetMouseButtonDown(1))
		{
			if (Input.GetKeyUp(KeyCode.LeftAlt))
			{
				if (this.altTouch != null)
				{
					this.altTouch.Phase = TouchPhase.Ended;
					return;
				}
			}
			else if (this.altTouch != null)
			{
				if (this.altTouch.Phase == TouchPhase.Ended)
				{
					this.altTouch = null;
				}
				else if (this.altTouch.Phase == TouchPhase.Began || this.altTouch.Phase == TouchPhase.Moved)
				{
					this.altTouch.Phase = TouchPhase.Stationary;
				}
			}
			if (this.touch != null)
			{
				this.touch.IsActive = false;
			}
			if (this.touch != null && Input.GetKeyDown(KeyCode.Escape))
			{
				this.touch.Phase = TouchPhase.Canceled;
			}
			else if (this.touch == null || this.touch.Phase != TouchPhase.Canceled)
			{
				if (Input.GetMouseButtonUp(0))
				{
					if (this.touch != null)
					{
						this.touch.Phase = TouchPhase.Ended;
					}
				}
				else if (Input.GetMouseButtonDown(0))
				{
					this.touch = new dfTouchTrackingInfo
					{
						FingerID = 0,
						Phase = TouchPhase.Began,
						Position = Input.mousePosition
					};
				}
				else if (this.touch != null && this.touch.Phase != TouchPhase.Ended)
				{
					Vector2 b = Input.mousePosition - this.touch.Position;
					bool flag = Vector2.Distance(Input.mousePosition, this.touch.Position) > float.Epsilon;
					this.touch.Position = Input.mousePosition;
					this.touch.Phase = (flag ? TouchPhase.Moved : TouchPhase.Stationary);
					if (flag && this.altTouch != null && (this.MirrorAlt || this.ParallelAlt))
					{
						if (this.MirrorAlt)
						{
							this.altTouch.Position -= b;
						}
						else
						{
							this.altTouch.Position += b;
						}
						this.altTouch.Phase = TouchPhase.Moved;
					}
				}
			}
			if (this.touch != null && !this.touch.IsActive)
			{
				this.touch = null;
			}
			return;
		}
		if (this.altTouch != null)
		{
			this.altTouch.Phase = TouchPhase.Ended;
			return;
		}
		this.altTouch = new dfTouchTrackingInfo
		{
			Phase = TouchPhase.Began,
			FingerID = 1,
			Position = Input.mousePosition
		};
	}

	// Token: 0x06000592 RID: 1426 RVA: 0x0001B07F File Offset: 0x0001927F
	public dfTouchInfo GetTouch(int index)
	{
		return this.Touches[index];
	}

	// Token: 0x040001E7 RID: 487
	private List<dfTouchInfo> activeTouches = new List<dfTouchInfo>();

	// Token: 0x040001E8 RID: 488
	private dfTouchTrackingInfo touch;

	// Token: 0x040001E9 RID: 489
	private dfTouchTrackingInfo altTouch;
}

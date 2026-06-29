using System;
using System.Runtime.InteropServices;
using UnityEngine;

// Token: 0x02000032 RID: 50
[Serializable]
[StructLayout(LayoutKind.Sequential, Pack = 1)]
public struct dfTouchInfo
{
	// Token: 0x17000153 RID: 339
	// (get) Token: 0x060005C6 RID: 1478 RVA: 0x0001BA83 File Offset: 0x00019C83
	public int fingerId
	{
		get
		{
			return this.m_FingerId;
		}
	}

	// Token: 0x17000154 RID: 340
	// (get) Token: 0x060005C7 RID: 1479 RVA: 0x0001BA8B File Offset: 0x00019C8B
	public Vector2 position
	{
		get
		{
			return this.m_Position;
		}
	}

	// Token: 0x17000155 RID: 341
	// (get) Token: 0x060005C8 RID: 1480 RVA: 0x0001BA93 File Offset: 0x00019C93
	public Vector2 rawPosition
	{
		get
		{
			return this.m_RawPosition;
		}
	}

	// Token: 0x17000156 RID: 342
	// (get) Token: 0x060005C9 RID: 1481 RVA: 0x0001BA9B File Offset: 0x00019C9B
	public Vector2 deltaPosition
	{
		get
		{
			return this.m_PositionDelta;
		}
	}

	// Token: 0x17000157 RID: 343
	// (get) Token: 0x060005CA RID: 1482 RVA: 0x0001BAA3 File Offset: 0x00019CA3
	public float deltaTime
	{
		get
		{
			return this.m_TimeDelta;
		}
	}

	// Token: 0x17000158 RID: 344
	// (get) Token: 0x060005CB RID: 1483 RVA: 0x0001BAAB File Offset: 0x00019CAB
	public int tapCount
	{
		get
		{
			return this.m_TapCount;
		}
	}

	// Token: 0x17000159 RID: 345
	// (get) Token: 0x060005CC RID: 1484 RVA: 0x0001BAB3 File Offset: 0x00019CB3
	public TouchPhase phase
	{
		get
		{
			return this.m_Phase;
		}
	}

	// Token: 0x060005CD RID: 1485 RVA: 0x0001BABB File Offset: 0x00019CBB
	public dfTouchInfo(int fingerID, TouchPhase phase, int tapCount, Vector2 position, Vector2 positionDelta, float timeDelta)
	{
		this.m_FingerId = fingerID;
		this.m_Phase = phase;
		this.m_Position = position;
		this.m_PositionDelta = positionDelta;
		this.m_TapCount = tapCount;
		this.m_TimeDelta = timeDelta;
		this.m_RawPosition = position;
	}

	// Token: 0x060005CE RID: 1486 RVA: 0x0001BAF4 File Offset: 0x00019CF4
	public static implicit operator dfTouchInfo(Touch touch)
	{
		return new dfTouchInfo
		{
			m_PositionDelta = touch.deltaPosition,
			m_TimeDelta = touch.deltaTime,
			m_FingerId = touch.fingerId,
			m_Phase = touch.phase,
			m_Position = touch.position,
			m_TapCount = touch.tapCount
		};
	}

	// Token: 0x04000200 RID: 512
	private int m_FingerId;

	// Token: 0x04000201 RID: 513
	private Vector2 m_Position;

	// Token: 0x04000202 RID: 514
	private Vector2 m_RawPosition;

	// Token: 0x04000203 RID: 515
	private Vector2 m_PositionDelta;

	// Token: 0x04000204 RID: 516
	private float m_TimeDelta;

	// Token: 0x04000205 RID: 517
	private int m_TapCount;

	// Token: 0x04000206 RID: 518
	private TouchPhase m_Phase;
}

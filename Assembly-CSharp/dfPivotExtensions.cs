using System;
using UnityEngine;

// Token: 0x0200004F RID: 79
public static class dfPivotExtensions
{
	// Token: 0x06000624 RID: 1572 RVA: 0x0001CE14 File Offset: 0x0001B014
	public static Vector2 AsOffset(this dfPivotPoint pivot)
	{
		switch (pivot)
		{
		case dfPivotPoint.TopLeft:
			return Vector2.zero;
		case dfPivotPoint.TopCenter:
			return new Vector2(0.5f, 0f);
		case dfPivotPoint.TopRight:
			return new Vector2(1f, 0f);
		case dfPivotPoint.MiddleLeft:
			return new Vector2(0f, 0.5f);
		case dfPivotPoint.MiddleCenter:
			return new Vector2(0.5f, 0.5f);
		case dfPivotPoint.MiddleRight:
			return new Vector2(1f, 0.5f);
		case dfPivotPoint.BottomLeft:
			return new Vector2(0f, 1f);
		case dfPivotPoint.BottomCenter:
			return new Vector2(0.5f, 1f);
		case dfPivotPoint.BottomRight:
			return new Vector2(1f, 1f);
		default:
			return Vector2.zero;
		}
	}

	// Token: 0x06000625 RID: 1573 RVA: 0x0001CEDC File Offset: 0x0001B0DC
	public static Vector3 TransformToCenter(this dfPivotPoint pivot, Vector2 size)
	{
		switch (pivot)
		{
		case dfPivotPoint.TopLeft:
			return new Vector2(0.5f * size.x, 0.5f * -size.y);
		case dfPivotPoint.TopCenter:
			return new Vector2(0f, 0.5f * -size.y);
		case dfPivotPoint.TopRight:
			return new Vector2(0.5f * -size.x, 0.5f * -size.y);
		case dfPivotPoint.MiddleLeft:
			return new Vector2(0.5f * size.x, 0f);
		case dfPivotPoint.MiddleCenter:
			return new Vector2(0f, 0f);
		case dfPivotPoint.MiddleRight:
			return new Vector2(0.5f * -size.x, 0f);
		case dfPivotPoint.BottomLeft:
			return new Vector2(0.5f * size.x, 0.5f * size.y);
		case dfPivotPoint.BottomCenter:
			return new Vector2(0f, 0.5f * size.y);
		case dfPivotPoint.BottomRight:
			return new Vector2(0.5f * -size.x, 0.5f * size.y);
		default:
			throw new Exception("Unhandled " + pivot.GetType().Name + " value: " + pivot.ToString());
		}
	}

	// Token: 0x06000626 RID: 1574 RVA: 0x0001D060 File Offset: 0x0001B260
	public static Vector3 UpperLeftToTransform(this dfPivotPoint pivot, Vector2 size)
	{
		return pivot.TransformToUpperLeft(size).Scale(-1f, -1f, 1f);
	}

	// Token: 0x06000627 RID: 1575 RVA: 0x0001D080 File Offset: 0x0001B280
	public static Vector3 TransformToUpperLeft(this dfPivotPoint pivot, Vector2 size)
	{
		switch (pivot)
		{
		case dfPivotPoint.TopLeft:
			return new Vector2(0f, 0f);
		case dfPivotPoint.TopCenter:
			return new Vector2(0.5f * -size.x, 0f);
		case dfPivotPoint.TopRight:
			return new Vector2(-size.x, 0f);
		case dfPivotPoint.MiddleLeft:
			return new Vector2(0f, 0.5f * size.y);
		case dfPivotPoint.MiddleCenter:
			return new Vector2(0.5f * -size.x, 0.5f * size.y);
		case dfPivotPoint.MiddleRight:
			return new Vector2(-size.x, 0.5f * size.y);
		case dfPivotPoint.BottomLeft:
			return new Vector2(0f, size.y);
		case dfPivotPoint.BottomCenter:
			return new Vector2(0.5f * -size.x, size.y);
		case dfPivotPoint.BottomRight:
			return new Vector2(-size.x, size.y);
		default:
			throw new Exception("Unhandled " + pivot.GetType().Name + " value: " + pivot.ToString());
		}
	}
}

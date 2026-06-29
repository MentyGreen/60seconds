using System;
using UnityEngine;

// Token: 0x0200007A RID: 122
public static class dfVectorExtensions
{
	// Token: 0x06000824 RID: 2084 RVA: 0x00023A77 File Offset: 0x00021C77
	public static bool IsNaN(this Vector3 vector)
	{
		return float.IsNaN(vector.x) || float.IsNaN(vector.y) || float.IsNaN(vector.z);
	}

	// Token: 0x06000825 RID: 2085 RVA: 0x00023AA0 File Offset: 0x00021CA0
	public static Vector3 ClampRotation(this Vector3 euler)
	{
		if (euler.x < 0f)
		{
			euler.x += 360f;
		}
		if (euler.x >= 360f)
		{
			euler.x -= 360f;
		}
		if (euler.y < 0f)
		{
			euler.y += 360f;
		}
		if (euler.y >= 360f)
		{
			euler.y -= 360f;
		}
		if (euler.z < 0f)
		{
			euler.z += 360f;
		}
		if (euler.z >= 360f)
		{
			euler.z -= 360f;
		}
		return euler;
	}

	// Token: 0x06000826 RID: 2086 RVA: 0x00023B5C File Offset: 0x00021D5C
	public static Vector2 Scale(this Vector2 vector, float x, float y)
	{
		return new Vector2(vector.x * x, vector.y * y);
	}

	// Token: 0x06000827 RID: 2087 RVA: 0x00023B73 File Offset: 0x00021D73
	public static Vector3 Scale(this Vector3 vector, float x, float y, float z)
	{
		return new Vector3(vector.x * x, vector.y * y, vector.z * z);
	}

	// Token: 0x06000828 RID: 2088 RVA: 0x00023B92 File Offset: 0x00021D92
	public static Vector3 FloorToInt(this Vector3 vector)
	{
		return new Vector3((float)Mathf.FloorToInt(vector.x), (float)Mathf.FloorToInt(vector.y), (float)Mathf.FloorToInt(vector.z));
	}

	// Token: 0x06000829 RID: 2089 RVA: 0x00023BBD File Offset: 0x00021DBD
	public static Vector3 CeilToInt(this Vector3 vector)
	{
		return new Vector3((float)Mathf.CeilToInt(vector.x), (float)Mathf.CeilToInt(vector.y), (float)Mathf.CeilToInt(vector.z));
	}

	// Token: 0x0600082A RID: 2090 RVA: 0x00023BE8 File Offset: 0x00021DE8
	public static Vector2 FloorToInt(this Vector2 vector)
	{
		return new Vector2((float)Mathf.FloorToInt(vector.x), (float)Mathf.FloorToInt(vector.y));
	}

	// Token: 0x0600082B RID: 2091 RVA: 0x00023C07 File Offset: 0x00021E07
	public static Vector2 CeilToInt(this Vector2 vector)
	{
		return new Vector2((float)Mathf.CeilToInt(vector.x), (float)Mathf.CeilToInt(vector.y));
	}

	// Token: 0x0600082C RID: 2092 RVA: 0x00023C26 File Offset: 0x00021E26
	public static Vector3 RoundToInt(this Vector3 vector)
	{
		return new Vector3((float)Mathf.RoundToInt(vector.x), (float)Mathf.RoundToInt(vector.y), (float)Mathf.RoundToInt(vector.z));
	}

	// Token: 0x0600082D RID: 2093 RVA: 0x00023C51 File Offset: 0x00021E51
	public static Vector2 RoundToInt(this Vector2 vector)
	{
		return new Vector2((float)Mathf.RoundToInt(vector.x), (float)Mathf.RoundToInt(vector.y));
	}

	// Token: 0x0600082E RID: 2094 RVA: 0x00023C70 File Offset: 0x00021E70
	public static Vector2 Quantize(this Vector2 vector, float discreteValue)
	{
		vector.x = (float)Mathf.RoundToInt(vector.x / discreteValue) * discreteValue;
		vector.y = (float)Mathf.RoundToInt(vector.y / discreteValue) * discreteValue;
		return vector;
	}

	// Token: 0x0600082F RID: 2095 RVA: 0x00023CA4 File Offset: 0x00021EA4
	public static Vector3 Quantize(this Vector3 vector, float discreteValue)
	{
		vector.x = (float)Mathf.RoundToInt(vector.x / discreteValue) * discreteValue;
		vector.y = (float)Mathf.RoundToInt(vector.y / discreteValue) * discreteValue;
		vector.z = (float)Mathf.RoundToInt(vector.z / discreteValue) * discreteValue;
		return vector;
	}
}

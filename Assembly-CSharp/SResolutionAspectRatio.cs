using System;
using UnityEngine;

// Token: 0x02000135 RID: 309
[Serializable]
public struct SResolutionAspectRatio
{
	// Token: 0x06000F20 RID: 3872 RVA: 0x0003E94C File Offset: 0x0003CB4C
	public bool FindResolution(float width, float height)
	{
		if (this._supportedResolutions != null)
		{
			for (int i = 0; i < this._supportedResolutions.Length; i++)
			{
				if (Mathf.Approximately(width, this._supportedResolutions[i].x) && Mathf.Approximately(height, this._supportedResolutions[i].y))
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x17000330 RID: 816
	// (get) Token: 0x06000F21 RID: 3873 RVA: 0x0003E9A9 File Offset: 0x0003CBA9
	public bool Enabled
	{
		get
		{
			return this._enabled;
		}
	}

	// Token: 0x17000331 RID: 817
	// (get) Token: 0x06000F22 RID: 3874 RVA: 0x0003E9B1 File Offset: 0x0003CBB1
	public float CamFov
	{
		get
		{
			return this._camFov;
		}
	}

	// Token: 0x17000332 RID: 818
	// (get) Token: 0x06000F23 RID: 3875 RVA: 0x0003E9B9 File Offset: 0x0003CBB9
	public float AspectRatio
	{
		get
		{
			return this._aspectRatio;
		}
	}

	// Token: 0x17000333 RID: 819
	// (get) Token: 0x06000F24 RID: 3876 RVA: 0x0003E9C1 File Offset: 0x0003CBC1
	public Vector2 CursorOffset
	{
		get
		{
			return this._cursorOffset;
		}
	}

	// Token: 0x17000334 RID: 820
	// (get) Token: 0x06000F25 RID: 3877 RVA: 0x0003E9C9 File Offset: 0x0003CBC9
	public float LocalResolutionFactor
	{
		get
		{
			return this._localResolutionFactor;
		}
	}

	// Token: 0x17000335 RID: 821
	// (get) Token: 0x06000F26 RID: 3878 RVA: 0x0003E9D1 File Offset: 0x0003CBD1
	public Vector2[] SupportedResolutions
	{
		get
		{
			return this._supportedResolutions;
		}
	}

	// Token: 0x0400091D RID: 2333
	public static SResolutionAspectRatio EMPTY;

	// Token: 0x0400091E RID: 2334
	[SerializeField]
	private bool _enabled;

	// Token: 0x0400091F RID: 2335
	[SerializeField]
	private float _aspectRatio;

	// Token: 0x04000920 RID: 2336
	[SerializeField]
	private float _localResolutionFactor;

	// Token: 0x04000921 RID: 2337
	[SerializeField]
	private Vector2 _cursorOffset;

	// Token: 0x04000922 RID: 2338
	[SerializeField]
	private float _camFov;

	// Token: 0x04000923 RID: 2339
	[SerializeField]
	private Vector2[] _supportedResolutions;
}

using System;
using UnityEngine;

// Token: 0x02000102 RID: 258
[Serializable]
public class AudioEntry
{
	// Token: 0x06000C6C RID: 3180 RVA: 0x00036596 File Offset: 0x00034796
	public float GetRandomisedPitch()
	{
		return Random.Range(1f - this._pitchVariation, 1f + this._pitchVariation);
	}

	// Token: 0x06000C6D RID: 3181 RVA: 0x000365B5 File Offset: 0x000347B5
	public float GetRandomisedVolume()
	{
		return Random.Range(1f - this._volumeVariation, 1f + this._volumeVariation);
	}

	// Token: 0x17000269 RID: 617
	// (get) Token: 0x06000C6E RID: 3182 RVA: 0x000365D4 File Offset: 0x000347D4
	public string Name
	{
		get
		{
			if (!(this._clip == null))
			{
				return this._clip.name;
			}
			return string.Empty;
		}
	}

	// Token: 0x1700026A RID: 618
	// (get) Token: 0x06000C6F RID: 3183 RVA: 0x000365F5 File Offset: 0x000347F5
	public float BaseVolume
	{
		get
		{
			return this._baseVolume;
		}
	}

	// Token: 0x1700026B RID: 619
	// (get) Token: 0x06000C70 RID: 3184 RVA: 0x000365FD File Offset: 0x000347FD
	public float VolumeVariation
	{
		get
		{
			return this._volumeVariation;
		}
	}

	// Token: 0x1700026C RID: 620
	// (get) Token: 0x06000C71 RID: 3185 RVA: 0x00036605 File Offset: 0x00034805
	public float PitchVariation
	{
		get
		{
			return this._pitchVariation;
		}
	}

	// Token: 0x1700026D RID: 621
	// (get) Token: 0x06000C72 RID: 3186 RVA: 0x0003660D File Offset: 0x0003480D
	public AudioClip Clip
	{
		get
		{
			return this._clip;
		}
	}

	// Token: 0x1700026E RID: 622
	// (get) Token: 0x06000C73 RID: 3187 RVA: 0x00036615 File Offset: 0x00034815
	// (set) Token: 0x06000C74 RID: 3188 RVA: 0x0003661D File Offset: 0x0003481D
	public string Group
	{
		get
		{
			return this._group;
		}
		set
		{
			this._group = value;
		}
	}

	// Token: 0x040006AA RID: 1706
	[SerializeField]
	private AudioClip _clip;

	// Token: 0x040006AB RID: 1707
	[SerializeField]
	private string _group = string.Empty;

	// Token: 0x040006AC RID: 1708
	[SerializeField]
	private float _baseVolume = 1f;

	// Token: 0x040006AD RID: 1709
	[SerializeField]
	private float _volumeVariation;

	// Token: 0x040006AE RID: 1710
	[SerializeField]
	private float _pitchVariation;
}

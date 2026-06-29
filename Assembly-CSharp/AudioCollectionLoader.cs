using System;
using UnityEngine;

// Token: 0x020000F5 RID: 245
public class AudioCollectionLoader : MonoBehaviour
{
	// Token: 0x06000BD2 RID: 3026 RVA: 0x00034438 File Offset: 0x00032638
	private void Awake()
	{
		if (this._audioCollection != null)
		{
			SoundManager.Instance.LoadAudioCollection(this._audioCollection, this._playbackStyle, this._musicAutoPlay);
		}
	}

	// Token: 0x06000BD3 RID: 3027 RVA: 0x00034464 File Offset: 0x00032664
	private void Start()
	{
		Object.Destroy(this);
	}

	// Token: 0x0400061C RID: 1564
	[SerializeField]
	private AudioCollection _audioCollection;

	// Token: 0x0400061D RID: 1565
	[SerializeField]
	private EPlaybackStyle _playbackStyle = EPlaybackStyle.SEQUENCE;

	// Token: 0x0400061E RID: 1566
	[SerializeField]
	private bool _musicAutoPlay = true;
}

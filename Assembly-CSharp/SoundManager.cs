using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000F7 RID: 247
public class SoundManager : MonoBehaviour
{
	// Token: 0x06000BD5 RID: 3029 RVA: 0x00034484 File Offset: 0x00032684
	private void Awake()
	{
		if (SoundManager.Instance == null)
		{
			SoundManager.Instance = this;
		}
		else if (SoundManager.Instance != this)
		{
			Object.Destroy(base.gameObject);
		}
		this._sfxSources = new AudioSource[Mathf.Clamp(this._sfxSourceCount, 1, 32)];
		for (int i = 0; i < this._sfxSourceCount; i++)
		{
			this._sfxSources[i] = base.gameObject.AddComponent<AudioSource>();
		}
		Object.DontDestroyOnLoad(base.gameObject);
	}

	// Token: 0x06000BD6 RID: 3030 RVA: 0x00034506 File Offset: 0x00032706
	private void Start()
	{
		base.StartCoroutine(this.ControlMusic());
		base.StartCoroutine(this.ControlSfx());
	}

	// Token: 0x06000BD7 RID: 3031 RVA: 0x00034522 File Offset: 0x00032722
	private void Update()
	{
	}

	// Token: 0x06000BD8 RID: 3032 RVA: 0x00034524 File Offset: 0x00032724
	public void LoadAudioCollection(AudioCollection ac, EPlaybackStyle playback = EPlaybackStyle.SEQUENCE, bool musicAutoPlay = true)
	{
		this._audioCollection = ac;
		if (this._audioCollection != null)
		{
			this._audioCollection.Initialize();
			this._musicForcedChange = true;
			this._musicPlaybackStyle = playback;
			this._musicWaitTrigger = !musicAutoPlay;
			return;
		}
		Debug.LogError("Invalid audio collection.");
	}

	// Token: 0x06000BD9 RID: 3033 RVA: 0x00034574 File Offset: 0x00032774
	public void UnloadAudioCollection()
	{
		this._audioCollection = null;
	}

	// Token: 0x06000BDA RID: 3034 RVA: 0x0003457D File Offset: 0x0003277D
	public void MuteMusic(bool mute = true)
	{
		if (this._musicSource != null)
		{
			this._musicSource.mute = mute;
			this._musicMute = mute;
		}
	}

	// Token: 0x06000BDB RID: 3035 RVA: 0x000345A0 File Offset: 0x000327A0
	public void PauseMusic(bool pause)
	{
		if (pause)
		{
			this._musicSource.Pause();
			this._lastPauseTime = Time.time;
			return;
		}
		this._musicSource.UnPause();
		float num = Time.time - this._lastPauseTime;
		this._musicEndMark += num;
	}

	// Token: 0x06000BDC RID: 3036 RVA: 0x000345ED File Offset: 0x000327ED
	public void RestartMusic()
	{
		if (this._musicSource != null)
		{
			this._musicSource.Play();
		}
	}

	// Token: 0x06000BDD RID: 3037 RVA: 0x00034608 File Offset: 0x00032808
	public void PauseToggle()
	{
		this._paused = !this._paused;
		this.PauseMusic(this._paused);
		for (int i = 0; i < this._sfxPlayback.Count; i++)
		{
			if (this._paused)
			{
				this._sfxPlayback[i].Source.Pause();
			}
			else
			{
				this._sfxPlayback[i].Source.UnPause();
			}
		}
	}

	// Token: 0x06000BDE RID: 3038 RVA: 0x0003467C File Offset: 0x0003287C
	public void Stop()
	{
		this.StopSFX();
		this.StopMusic(false);
	}

	// Token: 0x06000BDF RID: 3039 RVA: 0x0003468B File Offset: 0x0003288B
	public void StopMusic(bool immediate = false)
	{
		this._currentMusicTrack = null;
		this._musicSource.Stop();
		this._musicStop = true;
	}

	// Token: 0x06000BE0 RID: 3040 RVA: 0x000346A6 File Offset: 0x000328A6
	public void StartMusic()
	{
		this._musicSource.Play();
		this._musicStop = false;
	}

	// Token: 0x06000BE1 RID: 3041 RVA: 0x000346BC File Offset: 0x000328BC
	public void StopSFX()
	{
		for (int i = this._sfxPlayback.Count - 1; i >= 0; i--)
		{
			this.StopSFXObject(this._sfxPlayback[i].Source);
		}
	}

	// Token: 0x06000BE2 RID: 3042 RVA: 0x000346F8 File Offset: 0x000328F8
	public void StopSFXObject(AudioSource src)
	{
		if (src != null)
		{
			src.Stop();
			for (int i = 0; i < this._sfxPlayback.Count; i++)
			{
				if (this._sfxPlayback[i].Source == src)
				{
					this._sfxPlayback.RemoveAt(i);
					return;
				}
			}
		}
	}

	// Token: 0x06000BE3 RID: 3043 RVA: 0x00034750 File Offset: 0x00032950
	private AudioSource GetFreeSfxSource()
	{
		int i;
		Predicate<SoundManager.PlayInstance> <>9__0;
		int j;
		for (i = 0; i < this._sfxSourceCount; i = j)
		{
			List<SoundManager.PlayInstance> sfxPlayback = this._sfxPlayback;
			Predicate<SoundManager.PlayInstance> match;
			if ((match = <>9__0) == null)
			{
				match = (<>9__0 = ((SoundManager.PlayInstance x) => x.Source == this._sfxSources[i]));
			}
			if (sfxPlayback.Find(match) == null)
			{
				return this._sfxSources[i];
			}
			j = i + 1;
		}
		Debug.LogError("Not enough sfx audio sources for playback.");
		return null;
	}

	// Token: 0x06000BE4 RID: 3044 RVA: 0x000347D6 File Offset: 0x000329D6
	public void StopSFXObject(GameObject obj)
	{
		if (obj != null)
		{
			this.StopSFXObject(obj.GetComponent<AudioSource>());
		}
	}

	// Token: 0x06000BE5 RID: 3045 RVA: 0x000347ED File Offset: 0x000329ED
	private void PlaySoundClip(AudioSource src, AudioClip audio, float volume)
	{
		src.volume = volume;
		src.pitch = 1f;
		src.PlayOneShot(audio);
	}

	// Token: 0x06000BE6 RID: 3046 RVA: 0x00034808 File Offset: 0x00032A08
	private void PlaySoundClip(AudioSource src, AudioEntry audio, float volume)
	{
		src.volume = audio.BaseVolume * audio.GetRandomisedVolume() * volume;
		src.pitch = 1f * audio.GetRandomisedPitch();
		src.PlayOneShot(audio.Clip);
	}

	// Token: 0x06000BE7 RID: 3047 RVA: 0x00034840 File Offset: 0x00032A40
	private void PlaySound(AudioSource src, AudioEntry audio, float volume, bool loop = false, float delay = 0f)
	{
		src.clip = audio.Clip;
		src.volume = audio.BaseVolume * audio.GetRandomisedVolume() * volume;
		src.pitch = 1f * audio.GetRandomisedPitch();
		src.loop = loop;
		if (Mathf.Approximately(delay, 0f))
		{
			src.Play();
			return;
		}
		src.PlayDelayed(delay);
	}

	// Token: 0x06000BE8 RID: 3048 RVA: 0x000348A5 File Offset: 0x00032AA5
	private IEnumerator ControlSfx()
	{
		while (!this._terminate)
		{
			for (int i = this._sfxPlayback.Count - 1; i >= 0; i--)
			{
				if ((!this._sfxPlayback[i].Source.isPlaying || this._sfxPlayback[i].ShouldStop()) && !this._sfxPlayback[i].Source.loop)
				{
					this._sfxPlayback[i].Source.clip = null;
					this._sfxPlayback.RemoveAt(i);
				}
			}
			yield return new WaitForSeconds(1f);
		}
		this.StopSFX();
		this._sfxPlayback.Clear();
		yield break;
	}

	// Token: 0x06000BE9 RID: 3049 RVA: 0x000348B4 File Offset: 0x00032AB4
	private IEnumerator ControlMusic()
	{
		AudioEntry[] musicTracks = null;
		bool ended = false;
		int nextTrackToPlayIndex = 0;
		List<int> tracksAlreadyPlayed = new List<int>();
		while (!this._terminate)
		{
			if (this._audioCollection != null)
			{
				if (this._musicForcedChange)
				{
					this.StopMusic(false);
					musicTracks = this._audioCollection.GetMusicGroup();
					this._musicForcedChange = false;
					this._musicStop = false;
					this._musicEndMark = 0f;
					ended = false;
					nextTrackToPlayIndex = 0;
					tracksAlreadyPlayed.Clear();
				}
				if (!ended && !this._paused && this._musicEndMark <= Time.time)
				{
					int num = -1;
					switch (this._musicPlaybackStyle)
					{
					case EPlaybackStyle.SEQUENCE:
						if (nextTrackToPlayIndex >= musicTracks.Length)
						{
							ended = true;
						}
						else
						{
							num = nextTrackToPlayIndex;
							int num2 = nextTrackToPlayIndex + 1;
							nextTrackToPlayIndex = num2;
						}
						break;
					case EPlaybackStyle.LOOPED_SEQUENCE:
					{
						if (nextTrackToPlayIndex >= musicTracks.Length)
						{
							nextTrackToPlayIndex = 0;
						}
						num = nextTrackToPlayIndex;
						int num2 = nextTrackToPlayIndex + 1;
						nextTrackToPlayIndex = num2;
						break;
					}
					case EPlaybackStyle.RANDOM:
						num = Random.Range(0, musicTracks.Length);
						ended = (tracksAlreadyPlayed.Count >= musicTracks.Length);
						if (!ended)
						{
							while (tracksAlreadyPlayed.Contains(num))
							{
								num++;
								if (num >= musicTracks.Length)
								{
									num = 0;
								}
							}
							tracksAlreadyPlayed.Add(num);
						}
						break;
					case EPlaybackStyle.LOOPED_RANDOM:
						num = Random.Range(0, musicTracks.Length);
						while (tracksAlreadyPlayed.Contains(num))
						{
							num++;
							if (num >= musicTracks.Length)
							{
								num = 0;
							}
						}
						if (tracksAlreadyPlayed.Count + 1 >= musicTracks.Length)
						{
							tracksAlreadyPlayed.Clear();
						}
						if (!tracksAlreadyPlayed.Contains(num))
						{
							tracksAlreadyPlayed.Add(num);
						}
						break;
					}
					if (!ended && num >= 0 && num < musicTracks.Length)
					{
						this._currentMusicTrack = musicTracks[num];
						this.PlaySound(this._musicSource, musicTracks[num], this._musicVolume, false, 0f);
						this._musicEndMark = Time.time + musicTracks[num].Clip.length;
						if (this._musicWaitTrigger)
						{
							this._musicWaitTrigger = false;
							this.StopMusic(true);
						}
					}
				}
			}
			yield return new WaitForSeconds(1f);
		}
		this.StopMusic(false);
		yield break;
	}

	// Token: 0x06000BEA RID: 3050 RVA: 0x000348C4 File Offset: 0x00032AC4
	public AudioSource PlaySfxAudioEntry(AudioEntry sound, bool loop, float delay, Transform source)
	{
		AudioSource origin = null;
		if (sound != null)
		{
			if (source == null)
			{
				origin = this.GetFreeSfxSource();
			}
			else
			{
				origin = source.GetComponent<AudioSource>();
			}
			if (origin != null)
			{
				if (this._sfxPlayback.Find((SoundManager.PlayInstance x) => x.Source == origin) != null)
				{
					this.PlaySoundClip(origin, sound, this._sfxVolume);
				}
				else
				{
					this.PlaySound(origin, sound, this._sfxVolume, loop, delay);
					this._sfxPlayback.Add(new SoundManager.PlayInstance(origin, Time.time, origin.clip.length));
				}
			}
		}
		return origin;
	}

	// Token: 0x06000BEB RID: 3051 RVA: 0x0003498C File Offset: 0x00032B8C
	public AudioSource PlaySFX(AudioClip sound, bool loop, float delay, float volume, float pitch, Transform source)
	{
		AudioSource origin = null;
		if (sound != null)
		{
			if (source == null)
			{
				origin = this.GetFreeSfxSource();
			}
			else
			{
				origin = source.GetComponent<AudioSource>();
			}
			if (origin != null)
			{
				if (this._sfxPlayback.Find((SoundManager.PlayInstance x) => x.Source == origin) != null)
				{
					this.PlaySoundClip(origin, sound, this._sfxVolume);
				}
				else
				{
					this.PlaySoundClip(origin, sound, this._sfxVolume);
					this._sfxPlayback.Add(new SoundManager.PlayInstance(origin, Time.time, origin.clip.length));
				}
			}
		}
		return origin;
	}

	// Token: 0x06000BEC RID: 3052 RVA: 0x00034A57 File Offset: 0x00032C57
	public AudioEntry LoadFromGroup(string name)
	{
		if (this._audioCollection != null)
		{
			return this._audioCollection.GetRandomEntry(name);
		}
		Debug.LogError(string.Format("Could not load audio from group '{0}'.", name));
		return null;
	}

	// Token: 0x06000BED RID: 3053 RVA: 0x00034A85 File Offset: 0x00032C85
	public AudioEntry Load(string name)
	{
		if (this._audioCollection != null)
		{
			return this._audioCollection.GetEntry(name);
		}
		Debug.LogError(string.Format("Could not load audio '{0}'.", name));
		return null;
	}

	// Token: 0x06000BEE RID: 3054 RVA: 0x00034AB4 File Offset: 0x00032CB4
	public bool IsAudioClipPlaying(string name)
	{
		for (int i = 0; i < this._sfxPlayback.Count; i++)
		{
			if (this._sfxPlayback[i].Source.clip.name == name)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06000BEF RID: 3055 RVA: 0x00034AFD File Offset: 0x00032CFD
	public void CrossIn(float time, AudioSource src)
	{
		base.StartCoroutine(this.DoCross(true, time, src));
	}

	// Token: 0x06000BF0 RID: 3056 RVA: 0x00034B0F File Offset: 0x00032D0F
	private IEnumerator DoCross(bool crossIn, float time, AudioSource src)
	{
		if (src != null && src.clip != null && src.isPlaying)
		{
			float target = crossIn ? src.volume : 0f;
			float origin = crossIn ? 0f : src.volume;
			float endTime = Time.time + time;
			src.volume = origin;
			float timeElapsed = 0f;
			bool terminate = false;
			while (endTime > Time.time)
			{
				if (!src.isPlaying || this._musicForcedChange)
				{
					terminate = true;
					break;
				}
				timeElapsed += Time.deltaTime;
				src.volume = Mathf.Lerp(origin, target, timeElapsed / time);
				yield return null;
			}
			if (!terminate)
			{
				src.volume = target;
				if (!crossIn)
				{
					src.Stop();
				}
			}
		}
		yield break;
	}

	// Token: 0x06000BF1 RID: 3057 RVA: 0x00034B33 File Offset: 0x00032D33
	public void CrossOut(float time, AudioSource src)
	{
		base.StartCoroutine(this.DoCross(false, time, src));
	}

	// Token: 0x17000251 RID: 593
	// (get) Token: 0x06000BF2 RID: 3058 RVA: 0x00034B45 File Offset: 0x00032D45
	public AudioSource MusicSource
	{
		get
		{
			return this._musicSource;
		}
	}

	// Token: 0x17000252 RID: 594
	// (get) Token: 0x06000BF3 RID: 3059 RVA: 0x00034B4D File Offset: 0x00032D4D
	// (set) Token: 0x06000BF4 RID: 3060 RVA: 0x00034B55 File Offset: 0x00032D55
	public float VolumeSfx
	{
		get
		{
			return this._sfxVolume;
		}
		set
		{
			this._sfxVolume = value;
		}
	}

	// Token: 0x17000253 RID: 595
	// (get) Token: 0x06000BF5 RID: 3061 RVA: 0x00034B5E File Offset: 0x00032D5E
	// (set) Token: 0x06000BF6 RID: 3062 RVA: 0x00034B66 File Offset: 0x00032D66
	public float VolumeMusic
	{
		get
		{
			return this._musicVolume;
		}
		set
		{
			this._musicVolume = value;
			if (this._musicSource != null && this._currentMusicTrack != null)
			{
				this._musicSource.volume = this._musicVolume * this._currentMusicTrack.GetRandomisedVolume();
			}
		}
	}

	// Token: 0x04000625 RID: 1573
	public static SoundManager Instance;

	// Token: 0x04000626 RID: 1574
	[SerializeField]
	private AudioSource _musicSource;

	// Token: 0x04000627 RID: 1575
	[Range(1f, 32f)]
	[SerializeField]
	private int _sfxSourceCount = 1;

	// Token: 0x04000628 RID: 1576
	private AudioSource[] _sfxSources;

	// Token: 0x04000629 RID: 1577
	private List<SoundManager.PlayInstance> _sfxPlayback = new List<SoundManager.PlayInstance>();

	// Token: 0x0400062A RID: 1578
	private AudioCollection _audioCollection;

	// Token: 0x0400062B RID: 1579
	private AudioEntry _currentMusicTrack;

	// Token: 0x0400062C RID: 1580
	private bool _terminate;

	// Token: 0x0400062D RID: 1581
	private bool _paused;

	// Token: 0x0400062E RID: 1582
	private bool _sfxMute;

	// Token: 0x0400062F RID: 1583
	private bool _musicMute;

	// Token: 0x04000630 RID: 1584
	private bool _musicStop;

	// Token: 0x04000631 RID: 1585
	private bool _musicForcedChange = true;

	// Token: 0x04000632 RID: 1586
	private bool _musicWaitTrigger;

	// Token: 0x04000633 RID: 1587
	private EPlaybackStyle _musicPlaybackStyle;

	// Token: 0x04000634 RID: 1588
	private float _sfxVolume = 1f;

	// Token: 0x04000635 RID: 1589
	private float _musicVolume = 1f;

	// Token: 0x04000636 RID: 1590
	private float _lastPauseTime;

	// Token: 0x04000637 RID: 1591
	private float _musicEndMark;

	// Token: 0x02000398 RID: 920
	private class PlayInstance
	{
		// Token: 0x06001D5A RID: 7514 RVA: 0x0007D68F File Offset: 0x0007B88F
		public PlayInstance(AudioSource source, float timeStarted, float length)
		{
			this._source = source;
			this._timeStarted = timeStarted;
			this._length = length;
			this._endTime = timeStarted + length;
		}

		// Token: 0x06001D5B RID: 7515 RVA: 0x0007D6B5 File Offset: 0x0007B8B5
		public bool ShouldStop()
		{
			return Time.time >= this._endTime;
		}

		// Token: 0x17000522 RID: 1314
		// (get) Token: 0x06001D5C RID: 7516 RVA: 0x0007D6C7 File Offset: 0x0007B8C7
		public AudioSource Source
		{
			get
			{
				return this._source;
			}
		}

		// Token: 0x17000523 RID: 1315
		// (get) Token: 0x06001D5D RID: 7517 RVA: 0x0007D6CF File Offset: 0x0007B8CF
		public float TimeStarted
		{
			get
			{
				return this._timeStarted;
			}
		}

		// Token: 0x17000524 RID: 1316
		// (get) Token: 0x06001D5E RID: 7518 RVA: 0x0007D6D7 File Offset: 0x0007B8D7
		public float Length
		{
			get
			{
				return this._length;
			}
		}

		// Token: 0x040016BF RID: 5823
		private AudioSource _source;

		// Token: 0x040016C0 RID: 5824
		private float _timeStarted;

		// Token: 0x040016C1 RID: 5825
		private float _length;

		// Token: 0x040016C2 RID: 5826
		private float _endTime;
	}
}

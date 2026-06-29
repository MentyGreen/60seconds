using System;
using FMOD.Studio;
using FMODUnity;
using RG.Parsecs.Common;
using RG.Parsecs.EventEditor;
using UnityEngine;

namespace RG.SecondsRemaster.Scavenge
{
	// Token: 0x020002D0 RID: 720
	public class TubaSoundController : MonoBehaviour
	{
		// Token: 0x06001944 RID: 6468 RVA: 0x0006DFD6 File Offset: 0x0006C1D6
		private void Start()
		{
			this._isPlayedOnce = false;
		}

		// Token: 0x06001945 RID: 6469 RVA: 0x0006DFE0 File Offset: 0x0006C1E0
		private void OnDisable()
		{
			PLAYBACK_STATE playback_STATE;
			this._eventInstance.getPlaybackState(out playback_STATE);
			if (playback_STATE == PLAYBACK_STATE.PLAYING)
			{
				this._eventInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
				AudioManager.PlaySoundAtPoint(this._tubaSqueek, base.transform, 1f, 1f, 0f);
			}
		}

		// Token: 0x06001946 RID: 6470 RVA: 0x0006E02C File Offset: 0x0006C22C
		public void PlayWithAudioManager()
		{
			if (this._isTubaDisabled != null && this._isTubaDisabled.Value)
			{
				return;
			}
			if (this._wasScavengeFailed.Value)
			{
				this._eventInstance = RuntimeManager.CreateInstance(this._defaultTuba);
				this._eventInstance.set3DAttributes(base.gameObject.transform.To3DAttributes());
				this._eventInstance.start();
				this._eventInstance.release();
				return;
			}
			if (!this._isPlayedOnce)
			{
				this._eventInstance = RuntimeManager.CreateInstance(this._sadTrombone);
				this._eventInstance.set3DAttributes(base.gameObject.transform.To3DAttributes());
				this._eventInstance.start();
				this._eventInstance.release();
				this._isPlayedOnce = true;
			}
		}

		// Token: 0x0400130C RID: 4876
		[SerializeField]
		private GlobalBoolVariable _wasScavengeFailed;

		// Token: 0x0400130D RID: 4877
		[SerializeField]
		private GlobalBoolVariable _isTubaDisabled;

		// Token: 0x0400130E RID: 4878
		[EventRef]
		[SerializeField]
		private string _defaultTuba;

		// Token: 0x0400130F RID: 4879
		[EventRef]
		[SerializeField]
		private string _sadTrombone;

		// Token: 0x04001310 RID: 4880
		[EventRef]
		[SerializeField]
		private string _tubaSqueek;

		// Token: 0x04001311 RID: 4881
		[SerializeField]
		private bool _isPlayedOnce;

		// Token: 0x04001312 RID: 4882
		private EventInstance _eventInstance;
	}
}

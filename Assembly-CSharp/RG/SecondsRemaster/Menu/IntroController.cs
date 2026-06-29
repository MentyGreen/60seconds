using System;
using Rewired;
using RG.Parsecs.Common;
using UnityEngine;
using UnityEngine.Playables;

namespace RG.SecondsRemaster.Menu
{
	// Token: 0x02000284 RID: 644
	public class IntroController : MonoBehaviour
	{
		// Token: 0x060017B8 RID: 6072 RVA: 0x00068020 File Offset: 0x00066220
		private void Awake()
		{
			if (!Singleton<GameManager>.Instance.FirstMenuEnter || !this._introEnabled)
			{
				this.DisableIntro();
				return;
			}
			this._player = ReInput.players.GetPlayer(0);
		}

		// Token: 0x060017B9 RID: 6073 RVA: 0x0006804E File Offset: 0x0006624E
		private void Start()
		{
			if (!Singleton<GameManager>.Instance.FirstMenuEnter || !this._introEnabled)
			{
				return;
			}
			Singleton<GameManager>.Instance.FirstMenuEnter = false;
			base.Invoke("Play", this._playableDelay);
		}

		// Token: 0x060017BA RID: 6074 RVA: 0x00068081 File Offset: 0x00066281
		private void Play()
		{
			this._director.Play();
			this._isIntroVisible = true;
		}

		// Token: 0x060017BB RID: 6075 RVA: 0x00068095 File Offset: 0x00066295
		private void OnDisable()
		{
			this._isIntroVisible = false;
		}

		// Token: 0x060017BC RID: 6076 RVA: 0x0006809E File Offset: 0x0006629E
		private void Update()
		{
			if ((this._isIntroVisible && this._player.GetButtonDown(29)) || this._player.GetButtonDown(30))
			{
				this.DisableIntro();
			}
		}

		// Token: 0x060017BD RID: 6077 RVA: 0x000680CC File Offset: 0x000662CC
		private void DisableIntro()
		{
			this._isIntroVisible = false;
			base.gameObject.SetActive(false);
			this._director.Stop();
		}

		// Token: 0x0400114D RID: 4429
		[SerializeField]
		private bool _disableIntroInEditor = true;

		// Token: 0x0400114E RID: 4430
		[SerializeField]
		private bool _introEnabled;

		// Token: 0x0400114F RID: 4431
		[SerializeField]
		private PlayableDirector _director;

		// Token: 0x04001150 RID: 4432
		[SerializeField]
		private float _playableDelay = 4f;

		// Token: 0x04001151 RID: 4433
		private Player _player;

		// Token: 0x04001152 RID: 4434
		private bool _isIntroVisible;
	}
}

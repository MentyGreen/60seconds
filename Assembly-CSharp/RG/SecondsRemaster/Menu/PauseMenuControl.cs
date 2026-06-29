using System;
using Rewired;
using RG.Core.SaveSystem;
using RG.Parsecs.Common;
using RG.Parsecs.EventEditor;
using RG.Parsecs.Menu;
using RG.VirtualInput;
using Steamworks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace RG.SecondsRemaster.Menu
{
	// Token: 0x020002BC RID: 700
	public class PauseMenuControl : BasePauseMenu
	{
		// Token: 0x17000484 RID: 1156
		// (get) Token: 0x060018CC RID: 6348 RVA: 0x0006CA6E File Offset: 0x0006AC6E
		// (set) Token: 0x060018CD RID: 6349 RVA: 0x0006CA76 File Offset: 0x0006AC76
		public bool Paused { get; private set; }

		// Token: 0x17000485 RID: 1157
		// (get) Token: 0x060018CE RID: 6350 RVA: 0x0006CA7F File Offset: 0x0006AC7F
		// (set) Token: 0x060018CF RID: 6351 RVA: 0x0006CA87 File Offset: 0x0006AC87
		public PauseMenuControl.EGameType GameType { get; set; }

		// Token: 0x060018D0 RID: 6352 RVA: 0x0006CA90 File Offset: 0x0006AC90
		private void Start()
		{
			this._player = ReInput.players.GetPlayer(0);
			if (SteamManager.Initialized)
			{
				this._gameOverlayActivated = Callback<GameOverlayActivated_t>.Create(new Callback<GameOverlayActivated_t>.DispatchDelegate(this.OnGameOverlayActivated));
			}
		}

		// Token: 0x060018D1 RID: 6353 RVA: 0x0006CAC1 File Offset: 0x0006ACC1
		private void OnGameOverlayActivated(GameOverlayActivated_t pCallback)
		{
			if (pCallback.m_bActive != 0)
			{
				this.HandlePause(true);
				Debug.Log("Steam Overlay has been activated");
				return;
			}
			Debug.Log("Steam Overlay has been closed");
		}

		// Token: 0x060018D2 RID: 6354 RVA: 0x0006CAE7 File Offset: 0x0006ACE7
		public void ExitToMenu()
		{
			if (this.Paused)
			{
				this.HandlePause(false);
			}
			AudioManager.Instance.StopPlayingMusicFadeOut();
			AudioManager.Instance.StopPlayingSfxFadeOut();
			Singleton<GameManager>.Instance.ResetGame();
		}

		// Token: 0x060018D3 RID: 6355 RVA: 0x0006CB16 File Offset: 0x0006AD16
		public void BackToGame()
		{
			this.HandlePause(false);
		}

		// Token: 0x060018D4 RID: 6356 RVA: 0x0006CB1F File Offset: 0x0006AD1F
		public void RestartScavenge()
		{
			if (this.Paused)
			{
				this.HandlePause(false);
			}
			AudioManager.Instance.StopPlayingMusicFadeOut();
			AudioManager.Instance.StopPlayingSfxFadeOut();
			ResetGame.RestartLevel();
		}

		// Token: 0x060018D5 RID: 6357 RVA: 0x0006CB49 File Offset: 0x0006AD49
		public void Show()
		{
			if (this._visible)
			{
				return;
			}
			this._visible = true;
			this._animator.SetTrigger(PauseMenuControl.ShowHash);
			if (this._onShowEvent != null)
			{
				this._onShowEvent.Invoke();
			}
		}

		// Token: 0x060018D6 RID: 6358 RVA: 0x0006CB7E File Offset: 0x0006AD7E
		public void Hide()
		{
			if (!this._visible)
			{
				return;
			}
			this._visible = false;
			this._animator.SetTrigger(PauseMenuControl.HideHash);
			if (this._onHideEvent != null)
			{
				this._onHideEvent.Invoke();
			}
		}

		// Token: 0x060018D7 RID: 6359 RVA: 0x0006CBB3 File Offset: 0x0006ADB3
		public void Update()
		{
			if (this._player.GetButtonDown(12) || (this.Paused && this._player.GetButtonDown(30)))
			{
				this.HandlePause(!this.Paused);
			}
		}

		// Token: 0x060018D8 RID: 6360 RVA: 0x0006CBEA File Offset: 0x0006ADEA
		private void OnApplicationFocus(bool hasFocus)
		{
			if (!hasFocus)
			{
				this.SetPause(true);
			}
		}

		// Token: 0x060018D9 RID: 6361 RVA: 0x0006CBF6 File Offset: 0x0006ADF6
		private void OnApplicationPause(bool pauseStatus)
		{
			if (pauseStatus)
			{
				this.SetPause(true);
			}
		}

		// Token: 0x060018DA RID: 6362 RVA: 0x0006CC04 File Offset: 0x0006AE04
		private void SetVisibilityOfScavengeMenuObjects(bool active)
		{
			for (int i = 0; i < this._scavengeOnlyObjects.Length; i++)
			{
				this._scavengeOnlyObjects[i].SetActive(active);
			}
			if (!active && this._controlModeVariable.Value == 3)
			{
				this._cursorSensitivitySlider.SetActive(true);
				return;
			}
			this._cursorSensitivitySlider.SetActive(false);
		}

		// Token: 0x060018DB RID: 6363 RVA: 0x0006CC5C File Offset: 0x0006AE5C
		private void HandlePause(bool pause)
		{
			if (pause && (this.currentGameState != BasePauseMenu.EGameState.GamePlay || Singleton<GameManager>.Instance.IsLoadingObscurerVisible))
			{
				return;
			}
			if (this.Paused && !pause)
			{
				if (this.GameType == PauseMenuControl.EGameType.SCAVENGE)
				{
					PauseMenuControl.IS_AFTER_PAUSE = true;
				}
				Time.timeScale = 1f;
				this.Paused = false;
				this.Hide();
				if (this.GameType == PauseMenuControl.EGameType.SCAVENGE)
				{
					Singleton<VirtualInputManager>.Instance.VisualManager.MouseCursorVisible = false;
				}
				if (this.GameType == PauseMenuControl.EGameType.SCAVENGE)
				{
					AudioManager.Instance.SetMusicPaused(this.Paused);
					AudioManager.Instance.SetSfxPaused(this.Paused);
				}
				AudioListener.pause = this.Paused;
				this.SaveSettings();
				EventSystem.current.SetSelectedGameObject(null);
				return;
			}
			if (!this.Paused && pause)
			{
				this._pauseMenuObject.SetActive(true);
				this.Paused = true;
				this.Show();
				if (this.GameType == PauseMenuControl.EGameType.SCAVENGE)
				{
					Singleton<VirtualInputManager>.Instance.VisualManager.MouseCursorVisible = true;
				}
				if (this.GameType != this._previousGameType)
				{
					this.SetVisibilityOfScavengeMenuObjects(this.GameType == PauseMenuControl.EGameType.SCAVENGE);
				}
				if (this.GameType == PauseMenuControl.EGameType.SCAVENGE)
				{
					AudioManager.Instance.SetMusicPaused(this.Paused);
					AudioManager.Instance.SetSfxPaused(this.Paused);
				}
				AudioListener.pause = this.Paused;
			}
		}

		// Token: 0x060018DC RID: 6364 RVA: 0x0006CDAB File Offset: 0x0006AFAB
		public override void TogglePauseGame()
		{
			this.HandlePause(!this.Paused);
		}

		// Token: 0x060018DD RID: 6365 RVA: 0x0006CDBC File Offset: 0x0006AFBC
		public override void SetPause(bool paused)
		{
			this.HandlePause(paused);
		}

		// Token: 0x060018DE RID: 6366 RVA: 0x0006CDC5 File Offset: 0x0006AFC5
		public void SaveSettings()
		{
			StorageDataManager.TheInstance.Save("settings", delegate()
			{
				Debug.Log("Saved Settings.");
			}, null, true, false);
		}

		// Token: 0x0400129A RID: 4762
		[SerializeField]
		private Animator _animator;

		// Token: 0x0400129B RID: 4763
		[SerializeField]
		private GameObject _pauseMenuObject;

		// Token: 0x0400129C RID: 4764
		[SerializeField]
		private GameObject[] _scavengeOnlyObjects;

		// Token: 0x0400129D RID: 4765
		[SerializeField]
		private GameObject _cursorSensitivitySlider;

		// Token: 0x0400129E RID: 4766
		[SerializeField]
		private GlobalIntVariable _controlModeVariable;

		// Token: 0x0400129F RID: 4767
		[SerializeField]
		private bool _pauseOnFocusLostInEditor;

		// Token: 0x040012A0 RID: 4768
		[SerializeField]
		private UnityEvent _onShowEvent;

		// Token: 0x040012A1 RID: 4769
		[SerializeField]
		private UnityEvent _onHideEvent;

		// Token: 0x040012A2 RID: 4770
		private PauseMenuControl.EGameType _previousGameType;

		// Token: 0x040012A3 RID: 4771
		private bool _visible;

		// Token: 0x040012A4 RID: 4772
		private static readonly int ShowHash = Animator.StringToHash("Show");

		// Token: 0x040012A5 RID: 4773
		private static readonly int HideHash = Animator.StringToHash("Hide");

		// Token: 0x040012A6 RID: 4774
		private const int PLAYER_INDEX = 0;

		// Token: 0x040012A8 RID: 4776
		public static bool IS_AFTER_PAUSE = false;

		// Token: 0x040012AA RID: 4778
		private Player _player;

		// Token: 0x040012AB RID: 4779
		protected Callback<GameOverlayActivated_t> _gameOverlayActivated;

		// Token: 0x02000428 RID: 1064
		public enum EGameType
		{
			// Token: 0x040018ED RID: 6381
			NONE,
			// Token: 0x040018EE RID: 6382
			SCAVENGE,
			// Token: 0x040018EF RID: 6383
			SURVIVAL
		}
	}
}

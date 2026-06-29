using System;
using System.Collections;
using Rewired;
using RG.Core.SaveSystem;
using RG.Parsecs.Common;
using RG.Parsecs.EventEditor;
using RG.Parsecs.Survival;
using RG.Remaster.Common;
using UnityEngine;

namespace RG.SecondsRemaster.Menu
{
	// Token: 0x02000287 RID: 647
	public class RemasterMenuManager : MonoBehaviour
	{
		// Token: 0x17000477 RID: 1143
		// (get) Token: 0x060017C6 RID: 6086 RVA: 0x0006830C File Offset: 0x0006650C
		// (set) Token: 0x060017C7 RID: 6087 RVA: 0x00068314 File Offset: 0x00066514
		public DifficultyLevel CurrentDifficultyLevel { get; set; }

		// Token: 0x17000478 RID: 1144
		// (get) Token: 0x060017C8 RID: 6088 RVA: 0x0006831D File Offset: 0x0006651D
		// (set) Token: 0x060017C9 RID: 6089 RVA: 0x00068325 File Offset: 0x00066525
		public Character CurrentCharacter { get; set; }

		// Token: 0x17000479 RID: 1145
		// (get) Token: 0x060017CA RID: 6090 RVA: 0x0006832E File Offset: 0x0006652E
		// (set) Token: 0x060017CB RID: 6091 RVA: 0x00068340 File Offset: 0x00066540
		public Challenge CurrentChallenge
		{
			get
			{
				return this._currentChallengeData.RuntimeData.Challenge;
			}
			set
			{
				this._currentChallengeData.RuntimeData.Challenge = value;
			}
		}

		// Token: 0x060017CC RID: 6092 RVA: 0x00068353 File Offset: 0x00066553
		public void Start()
		{
			this.ResetData();
			this.SaveGlobalData();
			Singleton<PlatformManager>.Instance.RichPresenceManager.SetRichPresenceStatus(ERichPresenceStatus.MENU);
		}

		// Token: 0x060017CD RID: 6093 RVA: 0x00068374 File Offset: 0x00066574
		private void SaveGlobalData()
		{
			StorageDataManager.TheInstance.Save(this._globalGameDataSaveEvent.DataTag, delegate()
			{
				Debug.Log("Saved GlobalGameData");
			}, delegate()
			{
				Debug.Log("Failed saving GlobalGameData");
			}, true, false);
		}

		// Token: 0x060017CE RID: 6094 RVA: 0x000683D8 File Offset: 0x000665D8
		private void ResetData()
		{
			this._survivalSaveEvent.ResetGame();
			this._isTutorial.ResetData();
			this._isSurvivalOnly.ResetData();
			this._isScavengeOnly.ResetData();
			this._showJournal.ResetData();
			this._survivalDifficulty.ResetData();
		}

		// Token: 0x060017CF RID: 6095 RVA: 0x00068428 File Offset: 0x00066628
		public void StartFullGame()
		{
			this.SetGameplayVariables(false, false, false, true, true);
			this.SetScavengeData(this.CurrentDifficultyLevel.Setup, DemoManager.IS_DEMO_VERSION ? "level_scavenge_11" : "", false);
			this.SetSurvivalData(this._survivalMission, false);
			Singleton<PlatformManager>.Instance.RichPresenceManager.SetRichPresenceStatus(ERichPresenceStatus.SCAVENGE);
			Singleton<GameManager>.Instance.StartScavenge();
		}

		// Token: 0x060017D0 RID: 6096 RVA: 0x0006848C File Offset: 0x0006668C
		public void StartScavenge()
		{
			this.SetGameplayVariables(false, false, true, false, false);
			this.SetScavengeData(this._scavengeSetup, null, false);
			this.SetSurvivalData(this._scavengeMission, false);
			Singleton<PlatformManager>.Instance.RichPresenceManager.SetRichPresenceStatus(ERichPresenceStatus.SCAVENGE);
			Singleton<GameManager>.Instance.StartScavenge();
		}

		// Token: 0x060017D1 RID: 6097 RVA: 0x000684DC File Offset: 0x000666DC
		public void StartSurvival()
		{
			this.CurrentCharacter = ((Random.Range(0, 2) > 0) ? this._dad : this._mom);
			this.SetGameplayVariables(false, true, false, true, true);
			this.SetSurvivalData(this._survivalMission, false);
			Singleton<GameManager>.Instance.StartSurvival();
		}

		// Token: 0x060017D2 RID: 6098 RVA: 0x0006852C File Offset: 0x0006672C
		public void StartTutorial()
		{
			this.CurrentCharacter = this._dad;
			this.SetGameplayVariables(true, false, false, true, false);
			this.SetScavengeData(this._tutorialSetup, this._tutorialLevel, false);
			this.SetSurvivalData(this._tutorialMission, false);
			Singleton<PlatformManager>.Instance.RichPresenceManager.SetRichPresenceStatus(ERichPresenceStatus.SCAVENGE);
			Singleton<GameManager>.Instance.StartScavenge();
		}

		// Token: 0x060017D3 RID: 6099 RVA: 0x0006858A File Offset: 0x0006678A
		public void Continue()
		{
			this._shouldGameBeSaved.Value = true;
			Singleton<GameManager>.Instance.StartSurvival(this._survivalSaveEvent.DataTag);
		}

		// Token: 0x060017D4 RID: 6100 RVA: 0x000685AD File Offset: 0x000667AD
		public void ExitGame()
		{
			base.StartCoroutine(this.ActuallyExitGame());
		}

		// Token: 0x060017D5 RID: 6101 RVA: 0x000685BC File Offset: 0x000667BC
		private IEnumerator ActuallyExitGame()
		{
			yield return Singleton<GameManager>.Instance.LoadingManager.ObscurerController.ShowObscurer();
			yield return new WaitForSeconds(this._exitBlackScreenTime);
			Application.Quit();
			yield break;
		}

		// Token: 0x060017D6 RID: 6102 RVA: 0x000685CC File Offset: 0x000667CC
		public void StartChallenge()
		{
			this.CurrentCharacter = this._dad;
			if (this.CurrentChallenge == null)
			{
				return;
			}
			Challenge.EChallengeType challengeType = this.CurrentChallenge.ChallengeType;
			if (challengeType == Challenge.EChallengeType.SURVIVAL)
			{
				this.SetGameplayVariables(false, true, false, true, true);
				this.SetSurvivalData(this.CurrentChallenge.Mission, true);
				Singleton<PlatformManager>.Instance.RichPresenceManager.SetRichPresenceStatus(ERichPresenceStatus.CHALLENGE_SURVIVAL_INTRO);
				Singleton<GameManager>.Instance.StartSurvival();
				return;
			}
			if (challengeType != Challenge.EChallengeType.SCAVENGE)
			{
				return;
			}
			this.SetGameplayVariables(false, false, false, false, false);
			this.SetScavengeData(this.CurrentChallenge.GameSetup, this.CurrentChallenge.ScavengeLevel, true);
			this.SetSurvivalData(this.CurrentChallenge.Mission, true);
			Singleton<PlatformManager>.Instance.RichPresenceManager.SetRichPresenceStatus(ERichPresenceStatus.CHALLENGE_SCAVENGE);
			Singleton<GameManager>.Instance.StartScavenge();
		}

		// Token: 0x060017D7 RID: 6103 RVA: 0x00068698 File Offset: 0x00066898
		private void SetGameplayVariables(bool isTutorial, bool isSurvivalOnly, bool isScavengeOnly, bool showJournal, bool shouldGameBeSaved)
		{
			this._shouldGameBeSaved.Value = shouldGameBeSaved;
			this._isTutorial.Value = isTutorial;
			this._isSurvivalOnly.Value = isSurvivalOnly;
			this._isScavengeOnly.Value = isScavengeOnly;
			this._showJournal.Value = showJournal;
			if (this.CurrentCharacter == null)
			{
				this.CurrentCharacter = this._dad;
			}
		}

		// Token: 0x060017D8 RID: 6104 RVA: 0x00068700 File Offset: 0x00066900
		private void SetScavengeData(GameSetup setup, string scavengeLevel = null, bool isChallenge = false)
		{
			GameSessionData instance = GameSessionData.Instance;
			instance.CurrentChallenge = (isChallenge ? this.CurrentChallenge : null);
			instance.Setup = setup;
			instance.Difficulty = (this.CurrentDifficultyLevel ? this.CurrentDifficultyLevel.ScavengeDifficulty : EGameDifficulty.NORMAL);
			if (!isChallenge)
			{
				switch (instance.Difficulty)
				{
				case EGameDifficulty.EASY:
					instance.SetScavengeData(instance.Setup.Difficulties.Easy.PrepareTime, instance.Setup.Difficulties.Easy.ScavengeTime);
					break;
				case EGameDifficulty.NORMAL:
					instance.SetScavengeData(instance.Setup.Difficulties.Normal.PrepareTime, instance.Setup.Difficulties.Normal.ScavengeTime);
					break;
				case EGameDifficulty.HARD:
					instance.SetScavengeData(instance.Setup.Difficulties.Hard.PrepareTime, instance.Setup.Difficulties.Hard.ScavengeTime);
					break;
				}
				this._currentChallengeData.RuntimeData.Challenge = null;
			}
			else
			{
				instance.SetScavengeData(setup.PrepareTime, setup.GameTime);
			}
			if (this.CurrentCharacter == this._dad)
			{
				instance.Character = ECharacter.DAD;
			}
			else if (this.CurrentCharacter == this._mom)
			{
				instance.Character = ECharacter.MOM;
			}
			else
			{
				Debug.LogWarningFormat("Incorrect character was chosen for Scavenge ('{0}'). Scavenge character will fallback to DAD.", new object[]
				{
					this.CurrentCharacter
				});
				instance.Character = ECharacter.DAD;
			}
			if (string.IsNullOrEmpty(scavengeLevel))
			{
				Singleton<GameManager>.Instance.ScavangeSceneName = instance.Setup.GetRandomScavengeLevelName();
			}
			else
			{
				Singleton<GameManager>.Instance.ScavangeSceneName = scavengeLevel;
			}
			this.ReInitializeInput();
		}

		// Token: 0x060017D9 RID: 6105 RVA: 0x000688D8 File Offset: 0x00066AD8
		private void ReInitializeInput()
		{
			Player player = ReInput.players.GetPlayer(0);
			EPlayerInput eplayerInput = (EPlayerInput)this._currentControlModeVariable.Value;
			if (player.controllers.joystickCount == 0 && eplayerInput == EPlayerInput.GAMEPAD)
			{
				eplayerInput = EPlayerInput.KEYBOARD_MOUSE;
				this._currentControlModeVariable.Value = (int)eplayerInput;
			}
			int layoutId = (Application.systemLanguage == SystemLanguage.French) ? 1 : 0;
			int layoutId2 = (Application.systemLanguage != SystemLanguage.French) ? 1 : 0;
			string name = ReInput.mapping.GetLayout(ControllerType.Keyboard, layoutId).name;
			string name2 = ReInput.mapping.GetLayout(ControllerType.Keyboard, layoutId2).name;
			player.controllers.maps.SetMapsEnabled(false, 8);
			player.controllers.maps.SetMapsEnabled(false, ReInput.mapping.GetMapCategory(7).name);
			player.controllers.maps.SetMapsEnabled(false, ReInput.mapping.GetMapCategory(7).name);
			player.controllers.maps.SetMapsEnabled(false, 9);
			player.controllers.maps.SetMapsEnabled(false, 6);
			switch (eplayerInput)
			{
			case EPlayerInput.KEYBOARD:
				player.controllers.maps.SetMapsEnabled(true, ReInput.mapping.GetMapCategory(7).name, name);
				player.controllers.maps.SetMapsEnabled(true, ReInput.mapping.GetMapCategory(7).name, name2);
				return;
			case EPlayerInput.KEYBOARD_MOUSE:
				player.controllers.maps.SetMapsEnabled(true, ReInput.mapping.GetMapCategory(9).name, name);
				player.controllers.maps.SetMapsEnabled(true, ReInput.mapping.GetMapCategory(9).name, name2);
				return;
			case EPlayerInput.GAMEPAD:
				player.controllers.maps.SetMapsEnabled(true, ReInput.mapping.GetMapCategory(7).name, name);
				player.controllers.maps.SetMapsEnabled(true, ReInput.mapping.GetMapCategory(9).name, name);
				player.controllers.maps.SetMapsEnabled(true, 8);
				return;
			case EPlayerInput.TOUCH_ANALOGUE:
			case EPlayerInput.TOUCH_DIGITAL:
				break;
			case EPlayerInput.MOUSE_ONLY:
				player.controllers.maps.SetMapsEnabled(true, 6);
				break;
			default:
				return;
			}
		}

		// Token: 0x060017DA RID: 6106 RVA: 0x00068B04 File Offset: 0x00066D04
		private void SetSurvivalData(Mission mission, bool isChallenge = false)
		{
			this._survivalDifficulty.Value = (this.CurrentDifficultyLevel ? this.CurrentDifficultyLevel.SurvivalDifficulty : 1);
			if (!mission.SetCaptainExternal && mission.Captain != null)
			{
				this.CurrentCharacter = mission.Captain;
			}
			if (!isChallenge)
			{
				this._currentChallengeData.RuntimeData.Challenge = null;
			}
			this._characterList.AddCharToList(this.CurrentCharacter);
			this.CurrentCharacter.RuntimeData.IsCaptain = true;
			MissionManager.Instance.SetActualMission(mission);
		}

		// Token: 0x04001162 RID: 4450
		[SerializeField]
		[Header("Main Data")]
		private CharacterList _characterList;

		// Token: 0x04001163 RID: 4451
		[SerializeField]
		private Character _dad;

		// Token: 0x04001164 RID: 4452
		[SerializeField]
		private Character _mom;

		// Token: 0x04001165 RID: 4453
		[SerializeField]
		private SaveEvent _survivalSaveEvent;

		// Token: 0x04001166 RID: 4454
		[SerializeField]
		private SaveEvent _globalGameDataSaveEvent;

		// Token: 0x04001167 RID: 4455
		[SerializeField]
		private float _exitBlackScreenTime;

		// Token: 0x04001168 RID: 4456
		[SerializeField]
		[Header("Main Gameplay Variables")]
		private GlobalBoolVariable _isTutorial;

		// Token: 0x04001169 RID: 4457
		[SerializeField]
		private GlobalBoolVariable _isSurvivalOnly;

		// Token: 0x0400116A RID: 4458
		[SerializeField]
		private GlobalBoolVariable _isScavengeOnly;

		// Token: 0x0400116B RID: 4459
		[SerializeField]
		private GlobalBoolVariable _showJournal;

		// Token: 0x0400116C RID: 4460
		[SerializeField]
		private GlobalBoolVariable _shouldGameBeSaved;

		// Token: 0x0400116D RID: 4461
		[SerializeField]
		[Header("Survival Data")]
		private GlobalIntVariable _survivalDifficulty;

		// Token: 0x0400116E RID: 4462
		[SerializeField]
		private Mission _survivalMission;

		// Token: 0x0400116F RID: 4463
		[SerializeField]
		private Mission _scavengeMission;

		// Token: 0x04001170 RID: 4464
		[SerializeField]
		[Header("Scavenge Data")]
		private GameSetup _scavengeSetup;

		// Token: 0x04001171 RID: 4465
		[SerializeField]
		[Header("Tutorial Data")]
		private GameSetup _tutorialSetup;

		// Token: 0x04001172 RID: 4466
		[SerializeField]
		private Mission _tutorialMission;

		// Token: 0x04001173 RID: 4467
		[SerializeField]
		private string _tutorialLevel;

		// Token: 0x04001174 RID: 4468
		[SerializeField]
		[Header("Challenges Data")]
		private CurrentChallengeData _currentChallengeData;

		// Token: 0x04001175 RID: 4469
		[SerializeField]
		[Header("Other Data")]
		private GlobalIntVariable _currentControlModeVariable;

		// Token: 0x04001178 RID: 4472
		private const int MEDIUM_SURVIVAL_DIFFICULTY = 1;
	}
}

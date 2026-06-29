using System;
using System.Collections;
using System.Collections.Generic;
using DunGen;
using FMODUnity;
using RG.Parsecs.Common;
using RG.Parsecs.EventEditor;
using RG.Parsecs.GoalEditor;
using RG.Parsecs.Menu;
using RG.Parsecs.Survival;
using RG.SecondsRemaster;
using RG.SecondsRemaster.Menu;
using RG.SecondsRemaster.Scavenge;
using RG.VirtualInput;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x020000FB RID: 251
public class GameFlow : MonoBehaviour
{
	// Token: 0x14000061 RID: 97
	// (add) Token: 0x06000C0E RID: 3086 RVA: 0x00034DD0 File Offset: 0x00032FD0
	// (remove) Token: 0x06000C0F RID: 3087 RVA: 0x00034E08 File Offset: 0x00033008
	public event GameFlow.InteractionReport NearShelter;

	// Token: 0x06000C10 RID: 3088 RVA: 0x00034E40 File Offset: 0x00033040
	private void Awake()
	{
		Singleton<GameManager>.Instance.LoadingManager.BeforeUnloadLoadingScene += this.ScavengeLoaded;
		GameSessionData.Instance.CurrentGameStage = ECurrentGameStage.SCAVENGE;
		this._wasScavengeFailed.Value = true;
		if (this._isTubaDisabled != null)
		{
			this._isTubaDisabled.Value = false;
		}
		ScavengeGUISetup scavengeGUISetup = Object.FindObjectOfType<ScavengeGUISetup>();
		if (scavengeGUISetup != null)
		{
			scavengeGUISetup.Process();
		}
		if (GameSessionData.Instance.Setup.GameType == EGameType.TUTORIAL)
		{
			base.gameObject.GetComponent<ScavengeTutorialDriver>().enabled = true;
		}
	}

	// Token: 0x06000C11 RID: 3089 RVA: 0x00034ED4 File Offset: 0x000330D4
	private void ScavengeLoaded(object sender, EventArgs e)
	{
		LoadingManager.LoadingEventArgs loadingEventArgs = (LoadingManager.LoadingEventArgs)e;
		if (loadingEventArgs.LoadedSceneName.Contains("scavenge") || loadingEventArgs.LoadedSceneName.Contains("challenge") || loadingEventArgs.LoadedSceneName.Contains("tutorial"))
		{
			Singleton<VirtualInputManager>.Instance.VisualManager.MouseCursorVisible = false;
		}
	}

	// Token: 0x06000C12 RID: 3090 RVA: 0x00034F30 File Offset: 0x00033130
	private void Start()
	{
		this.GenerateLevel();
		this.NearShelter += this.ProcessChallengeConditions;
		this._pauseMenuControl = (BasePauseMenu.Instance as PauseMenuControl);
		if (GameSessionData.Instance.Setup.AreSpecificItemsToBeCollected())
		{
			this._itemsToCollect = new List<ScavengeItemController>(GameSessionData.Instance.Setup.CollectItems);
		}
	}

	// Token: 0x06000C13 RID: 3091 RVA: 0x00034F90 File Offset: 0x00033190
	private void UpdateCollectGUI(int swapIndex)
	{
		this._updateCollectGUI = true;
	}

	// Token: 0x06000C14 RID: 3092 RVA: 0x00034F9C File Offset: 0x0003319C
	private void ProcessChallengeConditions()
	{
		if (GameSessionData.Instance.Setup.GameType == EGameType.CHALLENGE_SCAVENGE)
		{
			bool flag = false;
			if (GameSessionData.Instance.Setup.AreSpecificItemsToBeCollected())
			{
				flag = (this._itemsToCollect.Count == 0);
			}
			if (flag && this._playerInteraction.IsPlayerNearShelter())
			{
				this.Terminate();
			}
		}
	}

	// Token: 0x06000C15 RID: 3093 RVA: 0x00034FF4 File Offset: 0x000331F4
	public void ReportCollectedItem(ScavengeItemController scavengeItemController)
	{
		if (this._itemsToCollect != null && this._itemsToCollect.Count > 0)
		{
			for (int i = 0; i < this._itemsToCollect.Count; i++)
			{
				if (this._itemsToCollect[i].SurvivalName == scavengeItemController.SurvivalName)
				{
					this._itemsToCollect.RemoveAt(i);
					this.UpdateCollectGUI(i);
					return;
				}
			}
		}
	}

	// Token: 0x06000C16 RID: 3094 RVA: 0x00035060 File Offset: 0x00033260
	private void GenerateLevel()
	{
		GameSetup setup = GameSessionData.Instance.Setup;
		int num = 0;
		for (int i = 0; i < setup.LevelItems.Count; i++)
		{
			ScavengeItemController component = setup.LevelItems[i].GetComponent<ScavengeItemController>();
			if (component != null && component.SpecialItem)
			{
				num++;
			}
		}
		this._specialLevelItems = new GameObject[num];
		int num2 = 0;
		for (int j = 0; j < setup.LevelItems.Count; j++)
		{
			ScavengeItemController component2 = setup.LevelItems[j].GetComponent<ScavengeItemController>();
			if (component2 != null && component2.SpecialItem)
			{
				this._specialLevelItems[num2] = setup.LevelItems[j].gameObject;
				num2++;
			}
		}
		RuntimeDungeon component3 = base.GetComponent<RuntimeDungeon>();
		if (component3 != null && component3.enabled)
		{
			if (setup.LevelData.FlowData != null)
			{
				if (!this._forcedHouse)
				{
					component3.Generator.DungeonFlow = setup.LevelData.FlowData;
					component3.Generator.OnGenerationStatusChanged += this.OnGenerationStatusChanged;
					component3.Generator.Generate();
				}
			}
			else
			{
				Object.Instantiate<GameObject>(setup.LevelData.HousePrefab);
				Object.Destroy(component3);
				this.Initialize();
			}
		}
		else
		{
			this.Initialize();
		}
		if (this._runtimeMeshBatcher != null)
		{
			this._runtimeMeshBatcher.CombineMeshes();
		}
	}

	// Token: 0x06000C17 RID: 3095 RVA: 0x000351EB File Offset: 0x000333EB
	public void FullExitGame()
	{
		this.ExitGame(true);
	}

	// Token: 0x06000C18 RID: 3096 RVA: 0x000351F4 File Offset: 0x000333F4
	public void Terminate()
	{
		this._terminated = true;
	}

	// Token: 0x06000C19 RID: 3097 RVA: 0x000351FD File Offset: 0x000333FD
	public void Restart(bool value)
	{
		ResetGame.RestartLevel();
	}

	// Token: 0x06000C1A RID: 3098 RVA: 0x00035204 File Offset: 0x00033404
	public void ExitGame(bool fullExit)
	{
		AudioManager.Instance.StopPlayingSfxFadeOut();
		if (fullExit)
		{
			Application.Quit();
			return;
		}
		Loading.Loader.NextLevelName = "main_menu";
		Loading.Loader.GoToLoading(false);
	}

	// Token: 0x06000C1B RID: 3099 RVA: 0x00035234 File Offset: 0x00033434
	private void Initialize()
	{
		bool spawnItems = GameSessionData.Instance.Setup.GameType != EGameType.TUTORIAL;
		this.InitializeNewLevel(GameSessionData.Instance.GetPlayerTemplate(), spawnItems);
		base.StartCoroutine(this.InitializePreGame());
	}

	// Token: 0x06000C1C RID: 3100 RVA: 0x00035275 File Offset: 0x00033475
	private IEnumerator InitializePreGame()
	{
		GameObject gameObject = null;
		while (gameObject == null)
		{
			yield return this._initPreGameTimeout;
			gameObject = GlobalTools.GetPlayer();
		}
		this._playerInteraction = gameObject.GetComponent<PlayerInteraction>();
		this._thirdPersonController = gameObject.GetComponent<ThirdPersonController>();
		this._shelterInventory = GlobalTools.GetShelterInventory();
		this._watch = base.GetComponent<Watch>();
		if (this._playerInteraction == null)
		{
			GlobalTools.DebugLogError("Player Interaction not set! (GameFlow)");
		}
		if (this._shelterInventory == null)
		{
			GlobalTools.DebugLogError("Shelter Inventory not set! (GameFlow)");
		}
		Shelter shelter = GlobalTools.GetShelter();
		if (shelter != null)
		{
			shelter.SetGuider();
		}
		this.StartPreGame();
		yield break;
	}

	// Token: 0x06000C1D RID: 3101 RVA: 0x00035284 File Offset: 0x00033484
	private void OnGenerationStatusChanged(DungeonGenerator generator, GenerationStatus status)
	{
		if (status == GenerationStatus.Complete)
		{
			Object.Instantiate<GameObject>(GameSessionData.Instance.Setup.LevelData.HousePrefab);
			Tile[] array = Object.FindObjectsOfType<Tile>();
			int num = GameSessionData.Instance.Setup.LevelData.FlowData.Length.Max + GameSessionData.Instance.Setup.LevelData.FlowData.GetUsedArchetypes()[0].BranchCount.Max;
			if (array.Length != num)
			{
				Object.Destroy(generator.CurrentDungeon.gameObject);
				Object.Instantiate<GameObject>(GameSessionData.Instance.Setup.LevelData.GetRandomLevelPrefab());
				this._forcedHouse = true;
			}
			this.Initialize();
		}
	}

	// Token: 0x06000C1E RID: 3102 RVA: 0x00035344 File Offset: 0x00033544
	private void HideScavengeUI(bool hideHands = true, bool hideClock = true, bool hideChallengeItems = true)
	{
		if (hideHands)
		{
			this._handsController.HideHands();
		}
		if (hideClock)
		{
			this._clockController.HideClock();
		}
		if (hideChallengeItems && GameSessionData.Instance.Setup.GameType == EGameType.CHALLENGE_SCAVENGE)
		{
			this._challengeItemsController.HideChallengeUI();
		}
	}

	// Token: 0x06000C1F RID: 3103 RVA: 0x00035384 File Offset: 0x00033584
	public ScavengeItemController ActivateAnnouncer(int index, bool activate, float customTime = -1f, string customItem = null)
	{
		if (this._announcers.Length != 0 && index >= 0 && index < this._announcers.Length)
		{
			if (customTime >= 0f)
			{
				this._announcers[index].Timeout = customTime;
			}
			this._announcers[index].Activate(activate, customItem);
			return this._announcers[index].AnnouncedScavengeItemController;
		}
		return null;
	}

	// Token: 0x06000C20 RID: 3104 RVA: 0x000353E0 File Offset: 0x000335E0
	public void StartPreGame()
	{
		GlobalTools.DebugLog("StartPreGame");
		Singleton<VirtualInputManager>.Instance.VisualManager.MouseCursorVisible = false;
		GlobalTools.GetShelter().ShowRange(false);
		this._thirdPersonController.SetMovementLimited(true);
		this._playerInteraction.EnableInteraction(false, false, false);
		this._watch.Initialize();
		this._preGame = true;
		base.StartCoroutine(GUIHelper.DoCircleFade(true, 1f, 0f, false, false, null, false));
		if (GameSessionData.Instance.Setup.GameType != EGameType.TUTORIAL)
		{
			this.StartTimer(2);
		}
	}

	// Token: 0x06000C21 RID: 3105 RVA: 0x00035472 File Offset: 0x00033672
	public void StartTimer(int prestartTimeMargin)
	{
		this._watch.StartTicking(prestartTimeMargin);
	}

	// Token: 0x06000C22 RID: 3106 RVA: 0x00035480 File Offset: 0x00033680
	public void StartGame()
	{
		GlobalTools.DebugLog("StartGame");
		AudioManager.Instance.PlayMusicFadeOut(this._scavengeMusic, 1f, 1f, 0.5f);
		this._gameRunning = true;
		this._preGame = false;
		this._thirdPersonController.SetMovementLimited(false);
		this._playerInteraction.EnableInteraction(true, true, true);
		this.EnableInteraction();
		AudioManager.PlaySound(this._sirenSound, 1f, 1f, 0f);
		if (GameSessionData.Instance.Setup.GameType == EGameType.TUTORIAL)
		{
			this.StartTimer(0);
			return;
		}
		this.OpenShelter();
	}

	// Token: 0x06000C23 RID: 3107 RVA: 0x0003551E File Offset: 0x0003371E
	public void OpenShelter()
	{
		base.StartCoroutine(this.OpenShelter(1f));
	}

	// Token: 0x06000C24 RID: 3108 RVA: 0x00035532 File Offset: 0x00033732
	private IEnumerator OpenShelter(float openTime)
	{
		Shelter shelter = GlobalTools.GetShelter();
		if (shelter != null)
		{
			shelter.OpenHatch(true, openTime);
			yield return new WaitForSeconds(openTime - 0.5f);
			shelter.Flash();
		}
		yield return null;
		yield break;
	}

	// Token: 0x06000C25 RID: 3109 RVA: 0x00035541 File Offset: 0x00033741
	public IEnumerator DoTutorialResetStart()
	{
		yield return Singleton<GameManager>.Instance.LoadingManager.ObscurerController.ShowObscurer();
		yield break;
	}

	// Token: 0x06000C26 RID: 3110 RVA: 0x00035549 File Offset: 0x00033749
	public IEnumerator DoTutorialResetEnd()
	{
		yield return Singleton<GameManager>.Instance.LoadingManager.ObscurerController.HideObscurer();
		yield break;
	}

	// Token: 0x06000C27 RID: 3111 RVA: 0x00035551 File Offset: 0x00033751
	public void EndLevel()
	{
		GlobalTools.DebugLog("Game ended!");
		this._gameRunning = false;
		base.StartCoroutine(this.FinalizeLevel());
	}

	// Token: 0x06000C28 RID: 3112 RVA: 0x00035571 File Offset: 0x00033771
	public IEnumerator FinalizeLevel()
	{
		bool triggerEnd = false;
		this._thirdPersonController.SetMovementBlocked(true);
		this._playerInteraction.EnableInteraction(false, false, false);
		Shelter shelter = GlobalTools.GetShelter();
		shelter.ShowRange(false);
		GameObject gameObject;
		SCamParams scamParams;
		if (this._playerInteraction.IsPlayerNearShelter())
		{
			this._addFailedStatus = false;
			this.HideScavengeUI(true, true, true);
			this._playerInteraction.JumpToShelter();
			if (GameSessionData.Instance.Setup.GameType != EGameType.TUTORIAL && GameSessionData.Instance.Setup.GameType != EGameType.CHALLENGE_SCAVENGE)
			{
				if (!AchievementsSystem.IsAchievementUnlocked(this._madeIt))
				{
					AchievementsSystem.UnlockAchievement(this._madeIt);
				}
				if (GameSessionData.Instance.Character == ECharacter.MOM && !AchievementsSystem.IsAchievementUnlocked(this._dolores))
				{
					AchievementsSystem.UnlockAchievement(this._dolores);
				}
			}
			gameObject = GameObject.FindGameObjectWithTag("SecondaryCamera1");
			scamParams = this._shelterJumpCamParams;
			this._wasScavengeFailed.Value = false;
		}
		else
		{
			this._playerInteraction.DuckAndCover();
			this.HideScavengeUI(true, true, true);
			if (GameSessionData.Instance.Setup.GameType != EGameType.TUTORIAL)
			{
				AchievementsSystem.UnlockAchievement(this._notMadeIt);
			}
			gameObject = GameObject.FindGameObjectWithTag("SecondaryCamera2");
			scamParams = this._duckCamParams;
			this._addFailedStatus = true;
		}
		Transform child = gameObject.transform.GetChild(0);
		child.gameObject.SetActive(true);
		iTween.MoveBy(child.gameObject, iTween.Hash(new object[]
		{
			"x",
			scamParams.Relocation.x,
			"y",
			scamParams.Relocation.y,
			"z",
			scamParams.Relocation.z,
			"time",
			scamParams.Time,
			"easeType",
			scamParams.EaseType
		}));
		Camera.main.gameObject.SetActive(false);
		while (!triggerEnd && !this._playerInteraction.EndInteractionDone())
		{
			yield return new WaitForSeconds(0.05f);
		}
		shelter.OpenHatch(false, 2f);
		GUIHelper.MakeObscurerVisible(true);
		if (GameSessionData.Instance.Setup.GameType != EGameType.TUTORIAL)
		{
			this._explosionAnimator.gameObject.SetActive(true);
			AudioManager.PlaySound(this._bombSound, 1f, 1f, 0f);
		}
		yield return new WaitForSeconds(5f);
		if (GameSessionData.Instance.Setup.GameType == EGameType.CHALLENGE_SCAVENGE)
		{
			this.SubmitResults();
			if (this._wasScavengeFailed.Value)
			{
				this._isTubaDisabled.Value = true;
				this._restartPanel.SetActive(true);
			}
			else
			{
				GameSessionData.Instance.CurrentChallenge.Unlock(GameSessionData.Instance.ScavengeFinishedTime);
				base.StartCoroutine(this.LoadConclusionSceneCoroutine());
			}
		}
		else
		{
			BasePauseMenu.Instance.currentGameState = BasePauseMenu.EGameState.Menu;
			this.SubmitResults();
			Singleton<GameManager>.Instance.StartSurvival();
		}
		yield break;
	}

	// Token: 0x06000C29 RID: 3113 RVA: 0x00035580 File Offset: 0x00033780
	private IEnumerator LoadConclusionSceneCoroutine()
	{
		BasePauseMenu.Instance.currentGameState = BasePauseMenu.EGameState.Menu;
		yield return Singleton<GameManager>.Instance.LoadingManager.ObscurerController.ShowObscurer();
		yield return new WaitForSeconds(this._waitTimeBeforeShowingConclusion);
		yield return SceneManager.LoadSceneAsync(this._challengeConclusionSceneName, LoadSceneMode.Additive);
		yield return Singleton<GameManager>.Instance.LoadingManager.ObscurerController.HideObscurer();
		Singleton<GameManager>.Instance.RaycastCatcher.SetCatchingRaycasts(false, null);
		yield break;
	}

	// Token: 0x06000C2A RID: 3114 RVA: 0x00035590 File Offset: 0x00033790
	public void SpawnPlayer(GameObject playerTemplate)
	{
		GameObject gameObject = GameObject.FindGameObjectWithTag("PlayerSpawnPoint");
		if (gameObject == null)
		{
			GlobalTools.DebugLogError("Player spawn point missing");
			return;
		}
		Spawner component = gameObject.GetComponent<Spawner>();
		component.Template = playerTemplate;
		if (component.Spawn(null))
		{
			Object.Destroy(gameObject);
			return;
		}
		GlobalTools.DebugLogError("Player spawning failed");
	}

	// Token: 0x06000C2B RID: 3115 RVA: 0x000355E4 File Offset: 0x000337E4
	public void InitializeNewLevel(GameObject playerTemplate, bool spawnItems = true)
	{
		GlobalTools.DebugLog("InitializeNewLevel");
		GameSetup setup = GameSessionData.Instance.Setup;
		setup.SetItemsForCharacter(GameSessionData.Instance.Character);
		if (playerTemplate != null)
		{
			this.SpawnPlayer(playerTemplate);
		}
		if (spawnItems)
		{
			GameObject[] array = GameObject.FindGameObjectsWithTag("SpawnPoint");
			Dictionary<GameObject, List<Spawner>> dictionary = new Dictionary<GameObject, List<Spawner>>();
			for (int i = 0; i < setup.LevelItems.Count; i++)
			{
				if (!dictionary.ContainsKey(setup.LevelItems[i].gameObject))
				{
					dictionary.Add(setup.LevelItems[i].gameObject, new List<Spawner>());
				}
			}
			for (int j = array.Length - 1; j >= 0; j--)
			{
				Spawner component = array[j].GetComponent<Spawner>();
				if (dictionary.ContainsKey(component.Template))
				{
					dictionary[component.Template].Add(component);
				}
				else
				{
					Object.Destroy(component.gameObject);
				}
			}
			for (int k = 0; k < setup.LevelItems.Count; k++)
			{
				List<Spawner> list = dictionary[setup.LevelItems[k].gameObject];
				if (list.Count > 0)
				{
					Spawner spawner = list[Random.Range(0, list.Count)];
					if (spawner.Spawn(setup.LevelItems[k].gameObject))
					{
						list.Remove(spawner);
						Object.Destroy(spawner.gameObject);
					}
					else
					{
						GlobalTools.DebugLogError("Error spawning item " + setup.LevelItems[k].name + ", spawning aborted");
					}
				}
			}
		}
	}

	// Token: 0x06000C2C RID: 3116 RVA: 0x0003578B File Offset: 0x0003398B
	public void EnableInteraction()
	{
		if (!this.HandsController.gameObject.activeSelf)
		{
			this.HandsController.gameObject.SetActive(true);
		}
	}

	// Token: 0x06000C2D RID: 3117 RVA: 0x000357B0 File Offset: 0x000339B0
	public void ReportNearShelter(bool near)
	{
		if (near && this.NearShelter != null)
		{
			this.NearShelter();
		}
	}

	// Token: 0x06000C2E RID: 3118 RVA: 0x000357C8 File Offset: 0x000339C8
	private void SubmitResults()
	{
		bool flag = false;
		if (this._playerInteraction.IsPlayerNearShelter())
		{
			flag = true;
			this._survivalTransferManager.TransferHeldItems();
			if (GameSessionData.Instance.Setup.GameType != EGameType.CHALLENGE_SCAVENGE)
			{
				StatsManager.Instance.AddGlobalData("NukeDropSurvived", 1);
			}
		}
		else
		{
			if (GameSessionData.Instance.Setup.GameType != EGameType.CHALLENGE_SCAVENGE)
			{
				StatsManager.Instance.AddGlobalData("AtomPerished", 1);
			}
			this._showJournal.Value = false;
		}
		this._survivalTransferManager.TransferScavengedItems();
		if (GameSessionData.Instance.Setup.GameType == EGameType.CHALLENGE_SCAVENGE)
		{
			this._wasScavengeFailed.Value = !this.IsChallengeConditionAchieved();
			if (!this._wasScavengeFailed.Value && this._currentChallenge != null && this._currentChallenge.RuntimeData.Challenge.Rewards != null)
			{
				for (int i = 0; i < this._currentChallenge.RuntimeData.Challenge.Rewards.Count; i++)
				{
					if (this._currentChallenge.RuntimeData.Challenge.Rewards[i].ScavengeRewardIsUnlockedVariable != null)
					{
						this._currentChallenge.RuntimeData.Challenge.Rewards[i].ScavengeRewardIsUnlockedVariable.SetValue(true);
					}
				}
			}
		}
		else if (this._addFailedStatus)
		{
			this._characterList.GetCaptain().RuntimeData.AddStatus(this._failedScavenge);
		}
		if (GameSessionData.Instance.Setup.GameType != EGameType.TUTORIAL && GameSessionData.Instance.Setup.GameType != EGameType.CHALLENGE_SCAVENGE)
		{
			this.CheckScavengeAchievements(flag);
			this.CheckTotalItemCollectedRecord();
			this.CheckPeoplePershied(flag);
		}
	}

	// Token: 0x06000C2F RID: 3119 RVA: 0x00035984 File Offset: 0x00033B84
	private void CheckPeoplePershied(bool win)
	{
		int num = 0;
		for (int i = 0; i < this._survivalTransferManager.ItemList.Items.Count; i++)
		{
			if (this._survivalTransferManager.ItemList.Items[i].Character != null && !this._survivalTransferManager.ItemList.Items[i].WasTaken)
			{
				num++;
			}
		}
		if (!win)
		{
			num++;
		}
		StatsManager.Instance.AddGlobalData("TotalCharactersDeadOnScavenge", num);
	}

	// Token: 0x06000C30 RID: 3120 RVA: 0x00035A10 File Offset: 0x00033C10
	private void CheckTotalItemCollectedRecord()
	{
		int num = 0;
		for (int i = 0; i < this._survivalTransferManager.ItemList.Items.Count; i++)
		{
			num += this._survivalTransferManager.ItemList.Items[i].Amount;
		}
		int globalData = StatsManager.Instance.GetGlobalData("MostItemsCollected");
		if (globalData < num)
		{
			StatsManager.Instance.AddGlobalData("MostItemsCollected", num - globalData);
		}
	}

	// Token: 0x06000C31 RID: 3121 RVA: 0x00035A84 File Offset: 0x00033C84
	private void CheckScavengeAchievements(bool scavengeStatus)
	{
		if (!AchievementsSystem.IsAchievementUnlocked(this._toraToraTora))
		{
			AchievementsSystem.ProgressAchievement(this._toraToraTora, StatsManager.Instance.GetGlobalData("ScavengeCollision"), 1337);
		}
		if (!AchievementsSystem.IsAchievementUnlocked(this._souper))
		{
			AchievementsSystem.ProgressAchievement(this._souper, StatsManager.Instance.GetGlobalData("ScavengedSoups"), 10);
		}
		if (!AchievementsSystem.IsAchievementUnlocked(this._allItems))
		{
			AchievementsSystem.ProgressAchievement(this._allItems, StatsManager.Instance.GetGlobalData("ScavengedItemsIDs"), 15);
		}
		if (!AchievementsSystem.IsAchievementUnlocked(this._waterAchievement))
		{
			AchievementsSystem.ProgressAchievement(this._waterAchievement, StatsManager.Instance.GetGlobalData("ScavengedWater"), 10);
		}
		if (GameSessionData.Instance.Setup.GameType == EGameType.SCAVENGE && scavengeStatus)
		{
			switch (GameSessionData.Instance.Difficulty)
			{
			case EGameDifficulty.EASY:
				if (this._enolaGay != null && this._enolaGayWasScavengeWon != null && !this._enolaGay.IsAchieved && !this._enolaGayWasScavengeWon.Value)
				{
					this._enolaGayWasScavengeWon.Value = true;
					if (this.CheckDifficultyAchievementVariables(this._easyDifficultyCompletionVariables))
					{
						AchievementsSystem.UnlockAchievement(this._enolaGay);
						return;
					}
				}
				break;
			case EGameDifficulty.NORMAL:
				if (this._manhattanProject != null && this._manhattanProjectWasScavengeWon != null && !this._manhattanProject.IsAchieved && !this._manhattanProjectWasScavengeWon.Value)
				{
					this._manhattanProjectWasScavengeWon.Value = true;
					if (this.CheckDifficultyAchievementVariables(this._mediumDifficultyCompletionVariables))
					{
						AchievementsSystem.UnlockAchievement(this._manhattanProject);
						return;
					}
				}
				break;
			case EGameDifficulty.HARD:
				if (this._deadHand != null && this._deadHandWasScavengeWon != null && !this._deadHand.IsAchieved && !this._deadHandWasScavengeWon.Value)
				{
					this._deadHandWasScavengeWon.Value = true;
					if (this.CheckDifficultyAchievementVariables(this._hardDifficultyCompletionVariables))
					{
						AchievementsSystem.UnlockAchievement(this._deadHand);
					}
				}
				break;
			default:
				return;
			}
		}
	}

	// Token: 0x06000C32 RID: 3122 RVA: 0x00035CA4 File Offset: 0x00033EA4
	private bool CheckDifficultyAchievementVariables(List<GlobalBoolVariable> difficultyVariables)
	{
		for (int i = 0; i < difficultyVariables.Count; i++)
		{
			if (!difficultyVariables[i].Value)
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x06000C33 RID: 3123 RVA: 0x00035CD4 File Offset: 0x00033ED4
	public bool IsChallengeConditionAchieved()
	{
		ScavengeItemList itemList = this._survivalTransferManager.ItemList;
		Dictionary<string, int> dictionary = this.CreateItemsToCollect();
		for (int i = 0; i < itemList.Items.Count; i++)
		{
			if (dictionary.ContainsKey(itemList.Items[i].Guid) && itemList.Items[i].Amount < dictionary[itemList.Items[i].Guid])
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x06000C34 RID: 3124 RVA: 0x00035D50 File Offset: 0x00033F50
	private Dictionary<string, int> CreateItemsToCollect()
	{
		Challenge currentChallenge = GameSessionData.Instance.CurrentChallenge;
		Dictionary<string, int> dictionary = new Dictionary<string, int>();
		for (int i = 0; i < currentChallenge.Collectables.Count; i++)
		{
			if (dictionary.ContainsKey(currentChallenge.Collectables[i].Guid))
			{
				Dictionary<string, int> dictionary2 = dictionary;
				string guid = currentChallenge.Collectables[i].Guid;
				int num = dictionary2[guid];
				dictionary2[guid] = num + 1;
			}
			else
			{
				dictionary.Add(currentChallenge.Collectables[i].Guid, 1);
			}
		}
		return dictionary;
	}

	// Token: 0x06000C35 RID: 3125 RVA: 0x00035DDE File Offset: 0x00033FDE
	public bool ReportSpecialItem(ScavengeItemController scavengeItemController)
	{
		if (this._reportedSpecialItems.Contains(scavengeItemController))
		{
			return false;
		}
		this._reportedSpecialItems.Add(scavengeItemController);
		return true;
	}

	// Token: 0x06000C36 RID: 3126 RVA: 0x00035DFD File Offset: 0x00033FFD
	public void ShowGoal(bool show, string text)
	{
		this._objectiveText.text = text;
		if (this._objectiveText.gameObject.activeSelf != show)
		{
			this._objectiveText.gameObject.SetActive(true);
		}
	}

	// Token: 0x06000C37 RID: 3127 RVA: 0x00035E30 File Offset: 0x00034030
	public void EnableGmanText(bool enable, bool talk, string text)
	{
		if (enable)
		{
			if (!this._sergeantAnimator.gameObject.activeSelf)
			{
				this._sergeantAnimator.gameObject.SetActive(true);
			}
			if (talk)
			{
				this._sergeantAnimator.SetBool("Talk", true);
			}
			this._sergeantText.ShowText(text);
			return;
		}
		if (talk)
		{
			this._sergeantAnimator.SetBool("Talk", false);
		}
		this._sergeantText.HideText();
	}

	// Token: 0x06000C38 RID: 3128 RVA: 0x00035EA3 File Offset: 0x000340A3
	public void EnableGmanChar(bool enable)
	{
		this._sergeantAnimator.gameObject.SetActive(enable);
	}

	// Token: 0x1700025C RID: 604
	// (get) Token: 0x06000C39 RID: 3129 RVA: 0x00035EB6 File Offset: 0x000340B6
	public Watch TheWatch
	{
		get
		{
			return this._watch;
		}
	}

	// Token: 0x1700025D RID: 605
	// (get) Token: 0x06000C3A RID: 3130 RVA: 0x00035EBE File Offset: 0x000340BE
	public bool ForcedHouse
	{
		get
		{
			return this._forcedHouse;
		}
	}

	// Token: 0x1700025E RID: 606
	// (get) Token: 0x06000C3B RID: 3131 RVA: 0x00035EC6 File Offset: 0x000340C6
	// (set) Token: 0x06000C3C RID: 3132 RVA: 0x00035ECE File Offset: 0x000340CE
	public bool Terminated
	{
		get
		{
			return this._terminated;
		}
		set
		{
			this._terminated = value;
		}
	}

	// Token: 0x1700025F RID: 607
	// (get) Token: 0x06000C3D RID: 3133 RVA: 0x00035ED7 File Offset: 0x000340D7
	public GameObject[] SpecialLevelItems
	{
		get
		{
			return this._specialLevelItems;
		}
	}

	// Token: 0x17000260 RID: 608
	// (get) Token: 0x06000C3E RID: 3134 RVA: 0x00035EDF File Offset: 0x000340DF
	public bool GameRunning
	{
		get
		{
			return this._gameRunning;
		}
	}

	// Token: 0x17000261 RID: 609
	// (get) Token: 0x06000C3F RID: 3135 RVA: 0x00035EE7 File Offset: 0x000340E7
	public bool Paused
	{
		get
		{
			return this._pauseMenuControl != null && this._pauseMenuControl.Paused;
		}
	}

	// Token: 0x17000262 RID: 610
	// (get) Token: 0x06000C40 RID: 3136 RVA: 0x00035F04 File Offset: 0x00034104
	public HandsController HandsController
	{
		get
		{
			return this._handsController;
		}
	}

	// Token: 0x17000263 RID: 611
	// (get) Token: 0x06000C41 RID: 3137 RVA: 0x00035F0C File Offset: 0x0003410C
	public SurvivalTransferManager SurvivalTransferManager
	{
		get
		{
			return this._survivalTransferManager;
		}
	}

	// Token: 0x17000264 RID: 612
	// (get) Token: 0x06000C42 RID: 3138 RVA: 0x00035F14 File Offset: 0x00034114
	public ScavengeTextController NoRoomText
	{
		get
		{
			return this._noRoomText;
		}
	}

	// Token: 0x17000265 RID: 613
	// (get) Token: 0x06000C43 RID: 3139 RVA: 0x00035F1C File Offset: 0x0003411C
	public ChallengeItemsController ChallengeItemsController
	{
		get
		{
			return this._challengeItemsController;
		}
	}

	// Token: 0x0400064A RID: 1610
	[SerializeField]
	private CurrentChallengeData _currentChallenge;

	// Token: 0x0400064B RID: 1611
	[SerializeField]
	private GlobalBoolVariable _wasScavengeFailed;

	// Token: 0x0400064C RID: 1612
	[SerializeField]
	private GlobalBoolVariable _showJournal;

	// Token: 0x0400064D RID: 1613
	[SerializeField]
	private GlobalBoolVariable _isTubaDisabled;

	// Token: 0x0400064E RID: 1614
	[SerializeField]
	private GameObject _restartPanel;

	// Token: 0x0400064F RID: 1615
	[SerializeField]
	private SCamParams _shelterJumpCamParams;

	// Token: 0x04000650 RID: 1616
	[SerializeField]
	private SCamParams _duckCamParams;

	// Token: 0x04000651 RID: 1617
	[SerializeField]
	private RuntimeMeshBatcherController _runtimeMeshBatcher;

	// Token: 0x04000652 RID: 1618
	[SerializeField]
	private HandsController _handsController;

	// Token: 0x04000653 RID: 1619
	[SerializeField]
	private ClockController _clockController;

	// Token: 0x04000654 RID: 1620
	[SerializeField]
	private SurvivalTransferManager _survivalTransferManager;

	// Token: 0x04000655 RID: 1621
	[SerializeField]
	private ScavengeTextController _noRoomText;

	// Token: 0x04000656 RID: 1622
	[SerializeField]
	private Animator _explosionAnimator;

	// Token: 0x04000657 RID: 1623
	[SerializeField]
	private GameObject _tutorial;

	// Token: 0x04000658 RID: 1624
	[SerializeField]
	private Animator _sergeantAnimator;

	// Token: 0x04000659 RID: 1625
	[SerializeField]
	private TextMeshProUGUI _objectiveText;

	// Token: 0x0400065A RID: 1626
	[SerializeField]
	private SergeantSpeechController _sergeantText;

	// Token: 0x0400065B RID: 1627
	[SerializeField]
	private ChallengeItemsController _challengeItemsController;

	// Token: 0x0400065C RID: 1628
	[EventRef]
	[SerializeField]
	private string _scavengeMusic;

	// Token: 0x0400065D RID: 1629
	[EventRef]
	[SerializeField]
	private string _sirenSound;

	// Token: 0x0400065E RID: 1630
	[EventRef]
	[SerializeField]
	private string _bombSound;

	// Token: 0x0400065F RID: 1631
	private GameObject[] _specialLevelItems;

	// Token: 0x04000660 RID: 1632
	private List<ScavengeItemController> _reportedSpecialItems = new List<ScavengeItemController>();

	// Token: 0x04000661 RID: 1633
	private Announcer[] _announcers;

	// Token: 0x04000662 RID: 1634
	private List<ScavengeItemController> _itemsToCollect;

	// Token: 0x04000663 RID: 1635
	private bool _gameRunning;

	// Token: 0x04000664 RID: 1636
	private bool _preGame;

	// Token: 0x04000665 RID: 1637
	private bool _forcedHouse = true;

	// Token: 0x04000666 RID: 1638
	private bool _terminated;

	// Token: 0x04000667 RID: 1639
	private bool _updateCollectGUI;

	// Token: 0x04000668 RID: 1640
	private PauseMenuControl _pauseMenuControl;

	// Token: 0x04000669 RID: 1641
	private Watch _watch;

	// Token: 0x0400066A RID: 1642
	private PlayerInteraction _playerInteraction;

	// Token: 0x0400066B RID: 1643
	private ThirdPersonController _thirdPersonController;

	// Token: 0x0400066C RID: 1644
	private ShelterInventory _shelterInventory;

	// Token: 0x0400066D RID: 1645
	[SerializeField]
	private CharacterStatus _failedScavenge;

	// Token: 0x0400066E RID: 1646
	[SerializeField]
	private CharacterList _characterList;

	// Token: 0x0400066F RID: 1647
	private bool _addFailedStatus;

	// Token: 0x04000670 RID: 1648
	private WaitForSeconds _initPreGameTimeout = new WaitForSeconds(0.1f);

	// Token: 0x04000671 RID: 1649
	[SerializeField]
	private Achievement _notMadeIt;

	// Token: 0x04000672 RID: 1650
	[SerializeField]
	private Achievement _madeIt;

	// Token: 0x04000673 RID: 1651
	[SerializeField]
	private Achievement _dolores;

	// Token: 0x04000674 RID: 1652
	[SerializeField]
	private Achievement _allItems;

	// Token: 0x04000675 RID: 1653
	[SerializeField]
	private Achievement _souper;

	// Token: 0x04000676 RID: 1654
	[SerializeField]
	private Goal _bePreparedGoal;

	// Token: 0x04000677 RID: 1655
	[SerializeField]
	private Goal _proGamerGoal;

	// Token: 0x04000678 RID: 1656
	[SerializeField]
	private Achievement _familyGuy;

	// Token: 0x04000679 RID: 1657
	[SerializeField]
	private Achievement _toraToraTora;

	// Token: 0x0400067A RID: 1658
	[SerializeField]
	private Achievement _waterAchievement;

	// Token: 0x0400067B RID: 1659
	[Header("Difficulty related achievements")]
	[SerializeField]
	private Achievement _enolaGay;

	// Token: 0x0400067C RID: 1660
	[SerializeField]
	private Achievement _manhattanProject;

	// Token: 0x0400067D RID: 1661
	[SerializeField]
	private Achievement _deadHand;

	// Token: 0x0400067E RID: 1662
	[SerializeField]
	private GlobalBoolVariable _enolaGayWasScavengeWon;

	// Token: 0x0400067F RID: 1663
	[SerializeField]
	private List<GlobalBoolVariable> _easyDifficultyCompletionVariables;

	// Token: 0x04000680 RID: 1664
	[SerializeField]
	private GlobalBoolVariable _manhattanProjectWasScavengeWon;

	// Token: 0x04000681 RID: 1665
	[SerializeField]
	private List<GlobalBoolVariable> _mediumDifficultyCompletionVariables;

	// Token: 0x04000682 RID: 1666
	[SerializeField]
	private GlobalBoolVariable _deadHandWasScavengeWon;

	// Token: 0x04000683 RID: 1667
	[SerializeField]
	private List<GlobalBoolVariable> _hardDifficultyCompletionVariables;

	// Token: 0x04000684 RID: 1668
	[SerializeField]
	private string _challengeConclusionSceneName = "ChallengeConclusion";

	// Token: 0x04000685 RID: 1669
	[SerializeField]
	private float _waitTimeBeforeShowingConclusion = 1f;

	// Token: 0x04000686 RID: 1670
	private const int ITEMS_TO_SCAVENGE = 15;

	// Token: 0x04000687 RID: 1671
	private const int SOUPS_TO_COLLECT = 10;

	// Token: 0x04000688 RID: 1672
	private const int FAMILY_MEMBERS_TO_GET = 3;

	// Token: 0x04000689 RID: 1673
	private const int TORA_COLLISIONS = 1337;

	// Token: 0x0400068A RID: 1674
	private const int SCAVENGED_WATER_FOR_ACHIEVEMENT = 10;

	// Token: 0x0400068B RID: 1675
	private const string TALK_PARAM_NAME = "Talk";

	// Token: 0x020003A0 RID: 928
	// (Invoke) Token: 0x06001D7B RID: 7547
	public delegate void InteractionReport();
}

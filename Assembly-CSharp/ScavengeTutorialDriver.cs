using System;
using System.Collections;
using System.Collections.Generic;
using FMOD.Studio;
using FMODUnity;
using I2.Loc;
using Rewired;
using RG.Parsecs.Common;
using RG.SecondsRemaster.Scavenge;
using UnityEngine;

// Token: 0x02000129 RID: 297
public class ScavengeTutorialDriver : MonoBehaviour
{
	// Token: 0x06000E90 RID: 3728 RVA: 0x0003C713 File Offset: 0x0003A913
	private void Awake()
	{
		this._gameFlow = GlobalTools.GetController<GameFlow>();
		this._player = ReInput.players.GetPlayer(0);
	}

	// Token: 0x06000E91 RID: 3729 RVA: 0x0003C731 File Offset: 0x0003A931
	private void Start()
	{
		this._scavengeItemsController = Object.FindObjectsOfType<ScavengeItemController>();
		base.StartCoroutine(this.RunTutorial());
	}

	// Token: 0x06000E92 RID: 3730 RVA: 0x0003C74B File Offset: 0x0003A94B
	public void SetGameFlow(GameFlow flow)
	{
		this._gameFlow = flow;
	}

	// Token: 0x06000E93 RID: 3731 RVA: 0x0003C754 File Offset: 0x0003A954
	private IEnumerator RunTutorial()
	{
		yield return new WaitForSeconds(1f);
		this.StartStage(ScavengeTutorialDriver.ETutorialStage.INTRO);
		while (this._stageRunning)
		{
			yield return new WaitForSeconds(1f);
		}
		this.StartStage(ScavengeTutorialDriver.ETutorialStage.MOVE1);
		while (this._stageRunning)
		{
			yield return new WaitForSeconds(1f);
		}
		this.StartStage(ScavengeTutorialDriver.ETutorialStage.MOVE2);
		while (this._stageRunning)
		{
			yield return new WaitForSeconds(1f);
		}
		this.StartStage(ScavengeTutorialDriver.ETutorialStage.EXPLORE);
		while (this._stageRunning)
		{
			yield return new WaitForSeconds(1f);
		}
		this.StartStage(ScavengeTutorialDriver.ETutorialStage.GRAB_SON);
		while (this._stageRunning)
		{
			yield return new WaitForSeconds(1f);
		}
		this.StartStage(ScavengeTutorialDriver.ETutorialStage.GRAB_FOOD);
		while (this._stageRunning)
		{
			yield return new WaitForSeconds(1f);
		}
		this.StartStage(ScavengeTutorialDriver.ETutorialStage.GRAB_WATER);
		while (this._stageRunning)
		{
			yield return new WaitForSeconds(1f);
		}
		this.StartStage(ScavengeTutorialDriver.ETutorialStage.CAPACITY);
		while (this._stageRunning)
		{
			yield return new WaitForSeconds(1f);
		}
		this.StartStage(ScavengeTutorialDriver.ETutorialStage.DROP);
		while (this._stageRunning)
		{
			yield return new WaitForSeconds(1f);
		}
		this.StartStage(ScavengeTutorialDriver.ETutorialStage.SPECIALS);
		while (this._stageRunning)
		{
			yield return new WaitForSeconds(1f);
		}
		this.StartStage(ScavengeTutorialDriver.ETutorialStage.TIMED_SCAVENGE);
		while (this._stageRunning)
		{
			yield return new WaitForSeconds(1f);
		}
		this.StartStage(ScavengeTutorialDriver.ETutorialStage.OUTRO);
		while (this._stageRunning)
		{
			yield return new WaitForSeconds(1f);
		}
		yield break;
	}

	// Token: 0x06000E94 RID: 3732 RVA: 0x0003C763 File Offset: 0x0003A963
	private void BlockPlayerMovement()
	{
		GlobalTools.GetThirdPersonController().SetMovementBlocked(true);
	}

	// Token: 0x06000E95 RID: 3733 RVA: 0x0003C770 File Offset: 0x0003A970
	private void UnblockPlayerMovement()
	{
		GlobalTools.GetThirdPersonController().SetMovementBlocked(false);
	}

	// Token: 0x06000E96 RID: 3734 RVA: 0x0003C780 File Offset: 0x0003A980
	private void StartStage(ScavengeTutorialDriver.ETutorialStage stage)
	{
		this._stage = stage;
		this._stageRunning = true;
		ScavengeTutorialState texts = this._tutorialTexts.GetTexts(this._stage);
		switch (this._stage)
		{
		case ScavengeTutorialDriver.ETutorialStage.INTRO:
			base.StartCoroutine(this.ShowTexts(texts.Texts, true, false, null, new ScavengeTutorialDriver.Progress(this.EndStage), false));
			return;
		case ScavengeTutorialDriver.ETutorialStage.MOVE1:
			base.StartCoroutine(this.ShowTexts(texts.Texts, false, false, null, new ScavengeTutorialDriver.Progress(this.UnblockPlayerMovement), false));
			base.StartCoroutine(this.MonitorGoToMarker(texts.Goal, "MoveMarker1", 15f, texts.Success, texts.Fail));
			return;
		case ScavengeTutorialDriver.ETutorialStage.MOVE2:
			base.StartCoroutine(this.ShowTexts(texts.Texts, false, false, null, new ScavengeTutorialDriver.Progress(this.UnblockPlayerMovement), false));
			base.StartCoroutine(this.MonitorGoToMarker(texts.Goal, "MoveMarker2", 15f, texts.Success, texts.Fail));
			return;
		case ScavengeTutorialDriver.ETutorialStage.EXPLORE:
			base.StartCoroutine(this.ShowTexts(texts.Texts, false, false, null, new ScavengeTutorialDriver.Progress(this.UnblockPlayerMovement), false));
			base.StartCoroutine(this.MonitorGoToMarker(texts.Goal, "ExploreMarker", 15f, texts.Success, texts.Fail));
			return;
		case ScavengeTutorialDriver.ETutorialStage.GRAB_SON:
			GlobalTools.GetPlayerInteraction().EnableInteraction(true, false, false);
			this._gameFlow.EnableInteraction();
			base.StartCoroutine(this.ShowTexts(texts.Texts, false, false, null, new ScavengeTutorialDriver.Progress(this.UnblockPlayerMovement), false));
			base.StartCoroutine(this.MonitorGrabItem(texts.Goal, this._son, 15f, texts.Success, texts.Fail, null));
			return;
		case ScavengeTutorialDriver.ETutorialStage.GRAB_FOOD:
			base.StartCoroutine(this.ShowTexts(texts.Texts, false, false, null, new ScavengeTutorialDriver.Progress(this.UnblockPlayerMovement), false));
			base.StartCoroutine(this.MonitorGrabItem(texts.Goal, null, 15f, texts.Success, texts.Fail, "tutorial_soup"));
			return;
		case ScavengeTutorialDriver.ETutorialStage.GRAB_WATER:
			base.StartCoroutine(this.ShowTexts(texts.Texts, false, false, null, new ScavengeTutorialDriver.Progress(this.UnblockPlayerMovement), false));
			base.StartCoroutine(this.MonitorGrabItem(texts.Goal, null, 15f, texts.Success, texts.Fail, "tutorial_water"));
			return;
		case ScavengeTutorialDriver.ETutorialStage.CAPACITY:
			base.StartCoroutine(this.ShowTexts(texts.Texts, false, false, null, new ScavengeTutorialDriver.Progress(this.EndStage), false));
			return;
		case ScavengeTutorialDriver.ETutorialStage.SHELTER:
			GlobalTools.GetShelter().Flash();
			base.StartCoroutine(this.ShowTexts(texts.Texts, false, false, null, new ScavengeTutorialDriver.Progress(this.UnblockPlayerMovement), false));
			base.StartCoroutine(this.MonitorGoToMarker(texts.Goal, "ShelterMarker", 15f, texts.Success, texts.Fail));
			return;
		case ScavengeTutorialDriver.ETutorialStage.DROP:
			base.StartCoroutine(this.ShowTexts(texts.Texts, false, false, null, new ScavengeTutorialDriver.Progress(this.UnblockPlayerMovement), false));
			GlobalTools.GetPlayerInteraction().EnableInteraction(true, true, false);
			this._gameFlow.OpenShelter();
			base.StartCoroutine(this.MonitorDrop(texts.Goal, -1, new ScavengeItem[]
			{
				this._food,
				this._water,
				this._son
			}, 15f, texts.Success, texts.Fail));
			return;
		case ScavengeTutorialDriver.ETutorialStage.SPECIALS:
			base.StartCoroutine(this.ShowTexts(texts.Texts, false, false, null, new ScavengeTutorialDriver.Progress(this.UnblockPlayerMovement), false));
			base.StartCoroutine(this.MonitorGrabItem(texts.Goal, this._radio, 15f, texts.Success, texts.Fail, null));
			return;
		case ScavengeTutorialDriver.ETutorialStage.ANNOUNCEMENTS:
		{
			base.StartCoroutine(this.ShowTexts(texts.Texts, false, false, null, new ScavengeTutorialDriver.Progress(this.UnblockPlayerMovement), false));
			ScavengeItemController scavengeItemController = this._gameFlow.ActivateAnnouncer(1, true, 9999f, "suitcase");
			base.StartCoroutine(this.MonitorGrabItem(texts.Goal, (scavengeItemController == null) ? null : scavengeItemController.ScavengeItem, 15f, texts.Success, texts.Fail, null));
			return;
		}
		case ScavengeTutorialDriver.ETutorialStage.DROP_ANNOUNCED:
			this._gameFlow.ActivateAnnouncer(1, false, -1f, null);
			base.StartCoroutine(this.ShowTexts(texts.Texts, false, false, null, new ScavengeTutorialDriver.Progress(this.UnblockPlayerMovement), false));
			base.StartCoroutine(this.MonitorDrop(texts.Goal, 5, null, 15f, texts.Success, texts.Fail));
			return;
		case ScavengeTutorialDriver.ETutorialStage.TIMED_SCAVENGE:
			GameSessionData.Instance.Setup.PrepareTime = 0;
			GameSessionData.Instance.Setup.GameTime = 60;
			this.ShowGoalText(texts.Goal, true);
			base.StartCoroutine(this.ShowTexts(texts.Texts, false, false, null, new ScavengeTutorialDriver.Progress(this.StartMockScavenge), false));
			return;
		case ScavengeTutorialDriver.ETutorialStage.OUTRO:
			base.StartCoroutine(this.ShowTexts(texts.Texts, false, false, null, new ScavengeTutorialDriver.Progress(this.EndTutorial), false));
			return;
		default:
			return;
		}
	}

	// Token: 0x06000E97 RID: 3735 RVA: 0x0003CC99 File Offset: 0x0003AE99
	private IEnumerator MonitorMockScavenge()
	{
		Vector3 playerPos = GlobalTools.GetPlayer().transform.position;
		this._gameFlow.SurvivalTransferManager.GetCurrentInventory();
		bool collectionOk = false;
		bool checkCollection = true;
		bool collectionReminder = false;
		while (this._gameFlow.TheWatch.TimeLeft > 0)
		{
			if (checkCollection)
			{
				collectionOk = (this._gameFlow.SurvivalTransferManager.GetCurrentItemsCount() >= 9);
				checkCollection = !collectionOk;
				if (collectionOk)
				{
					base.StartCoroutine(this.ShowTexts(this.TIMED_SCAVENGE_COLLECT_SUCCESS_TXT, false, false, null, null, false));
				}
				else if (!collectionReminder && this._gameFlow.TheWatch.TimeLeft < 30)
				{
					collectionReminder = true;
					base.StartCoroutine(this.ShowTexts(this.TIMED_SCAVENGE_COLLECT_FAIL_TXT, false, false, null, null, false));
				}
			}
			yield return new WaitForSeconds(1f);
		}
		this.BlockPlayerMovement();
		yield return new WaitForSeconds(2f);
		if (collectionOk && GlobalTools.GetPlayerInteraction().IsPlayerNearShelter())
		{
			this.EndStage();
		}
		else
		{
			ScavengeTutorialDriver.<>c__DisplayClass29_0 CS$<>8__locals1 = new ScavengeTutorialDriver.<>c__DisplayClass29_0();
			CS$<>8__locals1.ready = false;
			base.StartCoroutine(this.ShowTexts(this._tutorialTexts.GetTexts(ScavengeTutorialDriver.ETutorialStage.TIMED_SCAVENGE).Fail, false, false, null, delegate
			{
				CS$<>8__locals1.ready = true;
			}, false));
			while (!CS$<>8__locals1.ready)
			{
				yield return new WaitForSeconds(0.5f);
			}
			yield return this._gameFlow.DoTutorialResetStart();
			GlobalTools.GetPlayer().transform.position = playerPos;
			for (int i = 0; i < this._scavengeItemsController.Length; i++)
			{
				bool flag = true;
				for (int j = 0; j < this._excludedItems.Length; j++)
				{
					if (this._excludedItems[j] == this._scavengeItemsController[i])
					{
						flag = false;
						break;
					}
				}
				if (flag)
				{
					this._scavengeItemsController[i].gameObject.SetActive(true);
				}
			}
			ScavengeItemController[] array = Object.FindObjectsOfType<ScavengeItemController>();
			for (int k = 0; k < array.Length; k++)
			{
				array[k].CanBePickedUp = false;
				array[k].Highlight(false);
			}
			this._gameFlow.SurvivalTransferManager.ResetItems(new List<ScavengeItem>
			{
				this._son,
				this._radio,
				this._food,
				this._water
			}, new int[]
			{
				1,
				1,
				1,
				1
			});
			this._gameFlow.HandsController.Clear();
			this._gameFlow.TheWatch.ClockController.ResetRedFill();
			yield return this._gameFlow.DoTutorialResetEnd();
			yield return new WaitForSeconds(2.5f);
			this.StartStage(ScavengeTutorialDriver.ETutorialStage.TIMED_SCAVENGE);
			CS$<>8__locals1 = null;
		}
		yield break;
	}

	// Token: 0x06000E98 RID: 3736 RVA: 0x0003CCA8 File Offset: 0x0003AEA8
	private void StartMockScavenge()
	{
		GlobalTools.GetPlayerInteraction().EnableInteraction(true, true, true);
		this.UnblockPlayerMovement();
		this._gameFlow.StartGame();
		ScavengeItemController[] array = Object.FindObjectsOfType<ScavengeItemController>();
		for (int i = 0; i < array.Length; i++)
		{
			array[i].CanBePickedUp = true;
			array[i].Highlight(true);
		}
		base.StartCoroutine(this.MonitorMockScavenge());
	}

	// Token: 0x06000E99 RID: 3737 RVA: 0x0003CD06 File Offset: 0x0003AF06
	private void ShowGoalText(LocalizedString text, bool show)
	{
		GlobalTools.GetController<GameFlow>().ShowGoal(show, show ? text.ToString() : null);
	}

	// Token: 0x06000E9A RID: 3738 RVA: 0x0003CD26 File Offset: 0x0003AF26
	private IEnumerator MonitorGrabItem(LocalizedString goalText, ScavengeItem item, float timeout, List<LocalizedString> successTexts, List<LocalizedString> failTexts, string specificItemOverride = null)
	{
		this.ShowGoalText(goalText, true);
		bool flag = !string.IsNullOrEmpty(specificItemOverride);
		bool flag2 = item != null;
		if (flag || flag2)
		{
			if (flag2)
			{
				ScavengeItemController[] array = Object.FindObjectsOfType<ScavengeItemController>();
				for (int i = 0; i < array.Length; i++)
				{
					if (array[i].ScavengeItem == item)
					{
						this.SetTarget(array[i].gameObject);
						break;
					}
				}
			}
			else if (flag)
			{
				this.SetTarget(GameObject.Find(specificItemOverride));
			}
			ScavengeItemController scavengeItemController = this._target.GetComponent<ScavengeItemController>();
			scavengeItemController.CanBePickedUp = true;
			scavengeItemController.Highlight(true);
			float time = 0f;
			while (this._gameFlow.HandsController.LastScavengeItemAdded != scavengeItemController.ScavengeItem)
			{
				yield return new WaitForSeconds(1f);
				if (!this._showingText)
				{
					time += 1f;
					if (time > timeout)
					{
						time = 0f;
						base.StartCoroutine(this.ShowTexts(failTexts, false, false, null, null, false));
					}
				}
			}
			base.StartCoroutine(this.ShowTexts(successTexts, false, false, null, new ScavengeTutorialDriver.Progress(this.EndStage), false));
			scavengeItemController = null;
		}
		this.ShowGoalText(goalText, false);
		yield break;
	}

	// Token: 0x06000E9B RID: 3739 RVA: 0x0003CD62 File Offset: 0x0003AF62
	private IEnumerator MonitorDrop(LocalizedString goalText, int itemCount, ScavengeItem[] items, float timeout, List<LocalizedString> successTexts, List<LocalizedString> failTexts)
	{
		this.ShowGoalText(goalText, true);
		if ((items != null && items.Length != 0) || itemCount > 0)
		{
			float time = 0f;
			bool flag = false;
			while (!flag)
			{
				yield return new WaitForSeconds(1f);
				flag = true;
				if (items != null)
				{
					for (int i = 0; i < items.Length; i++)
					{
						if (!this._gameFlow.SurvivalTransferManager.GetCurrentInventory().Contains(items[i]))
						{
							flag = false;
							break;
						}
					}
				}
				else
				{
					flag = (this._gameFlow.SurvivalTransferManager.GetCurrentInventory().Count >= itemCount);
				}
				if (!this._showingText)
				{
					time += 1f;
					if (time > timeout)
					{
						time = 0f;
						base.StartCoroutine(this.ShowTexts(failTexts, false, false, null, null, false));
					}
				}
			}
			base.StartCoroutine(this.ShowTexts(successTexts, false, false, null, new ScavengeTutorialDriver.Progress(this.EndStage), false));
		}
		this.ShowGoalText(goalText, false);
		yield break;
	}

	// Token: 0x06000E9C RID: 3740 RVA: 0x0003CD9E File Offset: 0x0003AF9E
	private IEnumerator MonitorGoToMarker(LocalizedString goalText, string markerName, float timeout, List<LocalizedString> successTexts, List<LocalizedString> failTexts)
	{
		this.ShowGoalText(goalText, true);
		if (!string.IsNullOrEmpty(markerName))
		{
			this.SetTarget(markerName);
			Marker marker = this._target.GetComponent<Marker>();
			marker.Show(true);
			yield return new WaitForSeconds(0.5f);
			marker.Animate();
			float time = 0f;
			GameObject player = GlobalTools.GetPlayer();
			while (marker.CurrentUser != player)
			{
				yield return new WaitForSeconds(1f);
				if (!this._showingText)
				{
					time += 1f;
					if (time > timeout)
					{
						time = 0f;
						base.StartCoroutine(this.ShowTexts(failTexts, false, false, null, null, false));
					}
				}
			}
			marker.Show(false);
			base.StartCoroutine(this.ShowTexts(successTexts, false, false, null, new ScavengeTutorialDriver.Progress(this.EndStage), false));
			marker = null;
			player = null;
		}
		this.ShowGoalText(goalText, false);
		yield break;
	}

	// Token: 0x06000E9D RID: 3741 RVA: 0x0003CDD2 File Offset: 0x0003AFD2
	private void EndStage()
	{
		this._stageRunning = false;
	}

	// Token: 0x06000E9E RID: 3742 RVA: 0x0003CDDB File Offset: 0x0003AFDB
	private void EndTutorial()
	{
		this._gameFlow.EndLevel();
	}

	// Token: 0x06000E9F RID: 3743 RVA: 0x0003CDE8 File Offset: 0x0003AFE8
	private void SetTarget(string name)
	{
		GameObject gameObject = GameObject.Find(name);
		if (gameObject != null)
		{
			this._target = gameObject;
		}
	}

	// Token: 0x06000EA0 RID: 3744 RVA: 0x0003CE0C File Offset: 0x0003B00C
	private void SetTarget(GameObject obj)
	{
		this._target = obj;
	}

	// Token: 0x06000EA1 RID: 3745 RVA: 0x0003CE15 File Offset: 0x0003B015
	private void ShowTarget()
	{
		this.Show(true, this._target);
	}

	// Token: 0x06000EA2 RID: 3746 RVA: 0x0003CE24 File Offset: 0x0003B024
	private void HideTarget()
	{
		this.Show(false, this._target);
	}

	// Token: 0x06000EA3 RID: 3747 RVA: 0x0003CE33 File Offset: 0x0003B033
	private void Show(bool show, GameObject obj)
	{
		if (obj != null && obj.GetComponent<Renderer>() != null)
		{
			obj.GetComponent<Renderer>().enabled = show;
		}
	}

	// Token: 0x06000EA4 RID: 3748 RVA: 0x0003CE58 File Offset: 0x0003B058
	private IEnumerator TestLocation(GameObject target, Vector3 destination, float distance, ScavengeTutorialDriver.Progress f)
	{
		bool reachedTarget = false;
		while (!reachedTarget)
		{
			yield return new WaitForSeconds(1f);
			if (Vector3.Distance(target.transform.position, destination) <= distance)
			{
				reachedTarget = true;
			}
		}
		if (f != null)
		{
			f();
		}
		yield break;
	}

	// Token: 0x06000EA5 RID: 3749 RVA: 0x0003CE7D File Offset: 0x0003B07D
	private IEnumerator ShowTexts(List<LocalizedString> texts, bool showChar = true, bool hideCharAfter = true, ScavengeTutorialDriver.Progress startMethod = null, ScavengeTutorialDriver.Progress endMethod = null, bool playSounds = false)
	{
		if (texts != null)
		{
			while (this._showingText)
			{
				yield return new WaitForSeconds(1f);
			}
			this._showingText = true;
			if (startMethod != null)
			{
				startMethod();
			}
			if (showChar)
			{
				this._gameFlow.EnableGmanChar(true);
				yield return new WaitForSeconds(1f);
			}
			EventInstance eventInstance = default(EventInstance);
			if (playSounds)
			{
				eventInstance = AudioManager.PlaySoundAndReturnInstance(this._broadcastSoundName, 1f, 1f, 0f);
			}
			float readTimePerSymbol = this._readTimePerSymbol * ((LocalizationManager.CurrentLanguage == "Chinease" || LocalizationManager.CurrentLanguage == "Japanese") ? 2.5f : 1f);
			int num;
			for (int i = 0; i < texts.Count; i = num)
			{
				int fastForwardPrompt = 2;
				string text = texts[i];
				float textTimeout = Time.time + (float)text.Length * readTimePerSymbol;
				this._gameFlow.EnableGmanText(true, i == 0, text);
				while (textTimeout > Time.time)
				{
					yield return new WaitForSeconds(0.25f);
					if (this._player.GetButton(4))
					{
						num = fastForwardPrompt - 1;
						fastForwardPrompt = num;
						if (fastForwardPrompt <= 0)
						{
							break;
						}
					}
					else
					{
						fastForwardPrompt = 2;
					}
				}
				num = i + 1;
			}
			this._gameFlow.EnableGmanText(false, true, string.Empty);
			if (playSounds && eventInstance.isValid())
			{
				AudioManager.StopSound(eventInstance, FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
			}
			if (hideCharAfter)
			{
				yield return new WaitForSeconds(1f);
				this._gameFlow.EnableGmanChar(false);
			}
			yield return new WaitForSeconds(1f);
			if (endMethod != null)
			{
				endMethod();
			}
			this._showingText = false;
			eventInstance = default(EventInstance);
		}
		yield break;
	}

	// Token: 0x040008C0 RID: 2240
	[SerializeField]
	private List<LocalizedString> TIMED_SCAVENGE_COLLECT_SUCCESS_TXT = new List<LocalizedString>
	{
		"Tutorial/Scavenge/tut_timed_drop_ok_01"
	};

	// Token: 0x040008C1 RID: 2241
	[SerializeField]
	private List<LocalizedString> TIMED_SCAVENGE_COLLECT_FAIL_TXT = new List<LocalizedString>
	{
		"Tutorial/Scavenge/tut_timed_drop_bad_01"
	};

	// Token: 0x040008C2 RID: 2242
	private GameObject _target;

	// Token: 0x040008C3 RID: 2243
	private GameFlow _gameFlow;

	// Token: 0x040008C4 RID: 2244
	private ScavengeTutorialDriver.ETutorialStage _stage;

	// Token: 0x040008C5 RID: 2245
	private string[] _currentTexts;

	// Token: 0x040008C6 RID: 2246
	private ScavengeItemController[] _scavengeItemsController;

	// Token: 0x040008C7 RID: 2247
	private bool _stageRunning;

	// Token: 0x040008C8 RID: 2248
	private bool _showingText;

	// Token: 0x040008C9 RID: 2249
	[SerializeField]
	private float _readTimePerSymbol = 0.075f;

	// Token: 0x040008CA RID: 2250
	[SerializeField]
	private ScavengeTutorialTexts _tutorialTexts;

	// Token: 0x040008CB RID: 2251
	[SerializeField]
	private ScavengeItem _food;

	// Token: 0x040008CC RID: 2252
	[SerializeField]
	private ScavengeItem _water;

	// Token: 0x040008CD RID: 2253
	[SerializeField]
	private ScavengeItem _son;

	// Token: 0x040008CE RID: 2254
	[SerializeField]
	private ScavengeItem _radio;

	// Token: 0x040008CF RID: 2255
	[EventRef]
	[SerializeField]
	private string _broadcastSoundName;

	// Token: 0x040008D0 RID: 2256
	[SerializeField]
	private ScavengeItemController[] _excludedItems;

	// Token: 0x040008D1 RID: 2257
	private Player _player;

	// Token: 0x040008D2 RID: 2258
	private const int PLAYER_INDEX = 0;

	// Token: 0x020003B8 RID: 952
	public enum ETutorialStage
	{
		// Token: 0x04001726 RID: 5926
		NONE,
		// Token: 0x04001727 RID: 5927
		INTRO,
		// Token: 0x04001728 RID: 5928
		MOVE1,
		// Token: 0x04001729 RID: 5929
		MOVE2,
		// Token: 0x0400172A RID: 5930
		EXPLORE,
		// Token: 0x0400172B RID: 5931
		GRAB_SON,
		// Token: 0x0400172C RID: 5932
		GRAB_FOOD,
		// Token: 0x0400172D RID: 5933
		GRAB_WATER,
		// Token: 0x0400172E RID: 5934
		CAPACITY,
		// Token: 0x0400172F RID: 5935
		SHELTER,
		// Token: 0x04001730 RID: 5936
		DROP,
		// Token: 0x04001731 RID: 5937
		SPECIALS,
		// Token: 0x04001732 RID: 5938
		ANNOUNCEMENTS,
		// Token: 0x04001733 RID: 5939
		DROP_ANNOUNCED,
		// Token: 0x04001734 RID: 5940
		TIMED_SCAVENGE,
		// Token: 0x04001735 RID: 5941
		OUTRO
	}

	// Token: 0x020003B9 RID: 953
	// (Invoke) Token: 0x06001DF4 RID: 7668
	private delegate void Progress();

	// Token: 0x020003BA RID: 954
	// (Invoke) Token: 0x06001DF8 RID: 7672
	private delegate bool ConditionalProgress();
}

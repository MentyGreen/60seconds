using System;
using System.Collections;
using System.Collections.Generic;
using FMODUnity;
using Rewired;
using RG.Parsecs.Common;
using RG.Parsecs.EventEditor;
using RG.Remaster.Scavenge;
using RG.SecondsRemaster.Scavenge;
using UnityEngine;

// Token: 0x02000126 RID: 294
public class PlayerInteraction : MonoBehaviour
{
	// Token: 0x06000E6C RID: 3692 RVA: 0x0003BABD File Offset: 0x00039CBD
	private void Awake()
	{
		this._player = ReInput.players.GetPlayer(0);
	}

	// Token: 0x06000E6D RID: 3693 RVA: 0x0003BAD0 File Offset: 0x00039CD0
	private void Start()
	{
		this._gameFlow = GlobalTools.GetController<GameFlow>();
		this._inventory = GlobalTools.GetPlayerInventory();
		this._anim = base.GetComponent<Animator>();
		this._exit = GlobalTools.GetShelter().gameObject;
		this._shelter = GlobalTools.GetShelter();
		base.StartCoroutine(this.ShowTarget());
	}

	// Token: 0x06000E6E RID: 3694 RVA: 0x0003BB27 File Offset: 0x00039D27
	private void Update()
	{
		if (!this._gameFlow.Paused)
		{
			this.DoInteraction();
		}
	}

	// Token: 0x06000E6F RID: 3695 RVA: 0x0003BB3C File Offset: 0x00039D3C
	private IEnumerator ShowTarget()
	{
		for (;;)
		{
			ScavengeItemController scavengeItemController = this.FindClosestItemInSight();
			if (scavengeItemController != this._currentTarget)
			{
				if (this._currentTarget != null)
				{
					this._currentTarget.TargetHighlight(false);
				}
				if (scavengeItemController != null)
				{
					scavengeItemController.TargetHighlight(true);
				}
				this._currentTarget = scavengeItemController;
			}
			yield return new WaitForSeconds(this._targetUpdateTime);
		}
		yield break;
	}

	// Token: 0x06000E70 RID: 3696 RVA: 0x0003BB4C File Offset: 0x00039D4C
	private void OnGrabTouchdown()
	{
		AudioManager.PlaySound(this._scavengeGrabSoundName, 1f, 1f, 0f);
		if (this._grabEffectTemplate != null)
		{
			Vector3 position = this._itemToGrab.GetComponent<ScavengeItemController>().IsCharacter ? new Vector3(this._itemToGrab.transform.position.x, this._grabEffectSource.transform.position.y, this._itemToGrab.transform.position.z) : this._itemToGrab.transform.position;
			Object.Instantiate<GameObject>(this._grabEffectTemplate, position, default(Quaternion)).transform.parent = Camera.main.transform;
		}
		if (this._itemToGrab != null)
		{
			this._itemToGrab.gameObject.SetActive(false);
		}
	}

	// Token: 0x06000E71 RID: 3697 RVA: 0x0003BC35 File Offset: 0x00039E35
	public void EnableInteraction(bool enableGrab, bool enableDrop, bool showGrabLimit)
	{
		this._canGrab = enableGrab;
		this._canDrop = enableDrop;
		this._showGrabLimit = showGrabLimit;
	}

	// Token: 0x06000E72 RID: 3698 RVA: 0x0003BC4C File Offset: 0x00039E4C
	private IEnumerator DeactivateGrabItem()
	{
		if (this._itemToGrab != null)
		{
			this._itemToGrab.gameObject.SetActive(false);
		}
		yield break;
	}

	// Token: 0x06000E73 RID: 3699 RVA: 0x0003BC5B File Offset: 0x00039E5B
	private void OnGrabFinished()
	{
		this._anim.SetBool("grabbing", false);
		this._itemToGrab = null;
		this._canGrab = true;
	}

	// Token: 0x06000E74 RID: 3700 RVA: 0x0003BC7C File Offset: 0x00039E7C
	private void OnDropFinished()
	{
		this._anim.SetBool("dropping", false);
		this._shelter.DropIntoShelter(this._scavengeDropSoundName);
	}

	// Token: 0x06000E75 RID: 3701 RVA: 0x0003BCA0 File Offset: 0x00039EA0
	private void OnDuckAndCoverComplete()
	{
		this._endInteractionDone = true;
	}

	// Token: 0x06000E76 RID: 3702 RVA: 0x0003BCA9 File Offset: 0x00039EA9
	private void OnShelterJumpComplete()
	{
		this._endInteractionDone = true;
	}

	// Token: 0x06000E77 RID: 3703 RVA: 0x0003BCB4 File Offset: 0x00039EB4
	public void JumpToShelter()
	{
		Transform transform = GameObject.FindGameObjectWithTag("JumpPoint").transform;
		this.UpdateGlobalStats(true);
		base.transform.position = transform.position;
		base.transform.rotation = transform.rotation;
		if (GameSessionData.Instance.Character == ECharacter.MOM)
		{
			base.transform.position += base.transform.forward * 0.2f;
		}
		Object.Destroy(base.GetComponent<Rigidbody>());
		this._anim.SetBool("jumping", true);
	}

	// Token: 0x06000E78 RID: 3704 RVA: 0x0003BD50 File Offset: 0x00039F50
	public void DuckAndCover()
	{
		Transform transform = GameObject.FindGameObjectWithTag("DuckPoint").transform;
		this.UpdateGlobalStats(false);
		Vector3 origin = base.transform.position + new Vector3(0f, 0.5f, 0f);
		bool flag = true;
		int num = LayerMask.NameToLayer("Room");
		RaycastHit raycastHit;
		if (Physics.Raycast(origin, -Vector3.up, out raycastHit) && raycastHit.collider.gameObject.layer == num)
		{
			transform.parent.GetComponent<Renderer>().material = raycastHit.collider.GetComponent<Renderer>().material;
			flag = false;
		}
		if (flag)
		{
			GameObject gameObject = null;
			float num2 = 999f;
			Vector3 position = base.transform.position;
			GameObject[] array = Object.FindObjectsOfType(typeof(GameObject)) as GameObject[];
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i].layer == num)
				{
					float num3 = Vector3.Distance(array[i].transform.position, position);
					if (num3 < num2)
					{
						gameObject = array[i];
						num2 = num3;
					}
				}
			}
			if (gameObject != null)
			{
				Material[] materials = gameObject.GetComponent<Renderer>().materials;
				for (int j = 0; j < materials.Length; j++)
				{
					if (materials[j].name.Contains("floor"))
					{
						transform.parent.GetComponent<Renderer>().material = materials[j];
						break;
					}
				}
			}
		}
		transform.parent.GetComponent<Renderer>().material.mainTextureScale = transform.parent.transform.localScale;
		base.transform.position = transform.position;
		base.transform.rotation = transform.rotation;
		Object.Destroy(base.GetComponent<Rigidbody>());
		this._anim.SetBool("ducking", true);
	}

	// Token: 0x06000E79 RID: 3705 RVA: 0x0003BF2C File Offset: 0x0003A12C
	private void UpdateGlobalStats(bool scavengeSurvived)
	{
		if (GameSessionData.Instance.Setup.GameType != EGameType.TUTORIAL && GameSessionData.Instance.Setup.GameType != EGameType.CHALLENGE_SCAVENGE)
		{
			if (scavengeSurvived)
			{
				if (this._nukeDropsSurvivedVariable != null)
				{
					this._nukeDropsSurvivedVariable.SetValue(this._nukeDropsSurvivedVariable.Value + 1);
				}
			}
			else if (this._perishedInExplosionVariable != null)
			{
				this._perishedInExplosionVariable.SetValue(this._perishedInExplosionVariable.Value + 1);
			}
			this.UpdateTotalItemsCollected();
			this.UpdateMostSuppliesCollected();
			this.UpdatePeoplePerished();
		}
	}

	// Token: 0x06000E7A RID: 3706 RVA: 0x0003BFC0 File Offset: 0x0003A1C0
	private void UpdatePeoplePerished()
	{
		if (this._peoplePerishedFromNukeDropVariable != null)
		{
			List<ScavengeItem> currentInventory = this._gameFlow.SurvivalTransferManager.GetCurrentInventory();
			int num = 3;
			for (int i = 0; i < currentInventory.Count; i++)
			{
				if (currentInventory[i].Character != null)
				{
					num--;
				}
			}
			if (num > 0)
			{
				this._peoplePerishedFromNukeDropVariable.SetValue(this._peoplePerishedFromNukeDropVariable.Value + num);
			}
		}
	}

	// Token: 0x06000E7B RID: 3707 RVA: 0x0003C034 File Offset: 0x0003A234
	private void UpdateMostSuppliesCollected()
	{
		if (this._mostSuppliesCollectedVariable != null && this._gameFlow.SurvivalTransferManager.GetCurrentItemsCount() > this._mostSuppliesCollectedVariable.Value)
		{
			this._mostSuppliesCollectedVariable.SetValue(this._mostSuppliesCollectedVariable.Value + this._gameFlow.SurvivalTransferManager.GetCurrentItemsCount());
		}
	}

	// Token: 0x06000E7C RID: 3708 RVA: 0x0003C093 File Offset: 0x0003A293
	private void UpdateTotalItemsCollected()
	{
		if (this._totalItemsCollectedVariable != null)
		{
			this._totalItemsCollectedVariable.SetValue(this._totalItemsCollectedVariable.Value + this._gameFlow.SurvivalTransferManager.GetCurrentItemsCount());
		}
	}

	// Token: 0x06000E7D RID: 3709 RVA: 0x0003C0CA File Offset: 0x0003A2CA
	public bool EndInteractionDone()
	{
		return this._endInteractionDone;
	}

	// Token: 0x06000E7E RID: 3710 RVA: 0x0003C0D4 File Offset: 0x0003A2D4
	private void DoInteraction()
	{
		if (this._canInteract || this._nextInteractionTime <= Time.time)
		{
			this._canInteract = true;
			if (!this._anim.GetBool("grabbing") && !this._anim.GetBool("dropping") && this._itemToGrab == null && this._player.GetButtonRepeating(4))
			{
				if (this._canDrop && this._isNearShelter && !this._gameFlow.HandsController.AreHandsEmpty() && this.GetAngleTo(this._exit) < this._maxAngle)
				{
					this._nextInteractionTime = Time.time + this._interactionDelay;
					this._canInteract = false;
					this._thirdPersonController.StopMovement();
					this._anim.SetBool("dropping", true);
					this._gameFlow.SurvivalTransferManager.TransferHeldItems();
					this._gameFlow.HandsController.Clear();
					if (GameSessionData.Instance.Setup.GameType == EGameType.CHALLENGE_SCAVENGE && this._gameFlow.IsChallengeConditionAchieved())
					{
						this._gameFlow.Terminated = true;
						return;
					}
				}
				else if (this._canGrab)
				{
					this.GrabItem();
				}
			}
		}
	}

	// Token: 0x06000E7F RID: 3711 RVA: 0x0003C220 File Offset: 0x0003A420
	private void OnTriggerEnter(Collider other)
	{
		if (other.transform.parent != null && other.transform.parent.gameObject == this._exit)
		{
			this._isNearShelter = true;
			GlobalTools.GetController<GameFlow>().ReportNearShelter(true);
		}
	}

	// Token: 0x06000E80 RID: 3712 RVA: 0x0003C270 File Offset: 0x0003A470
	private void OnTriggerExit(Collider other)
	{
		if (other.transform.parent != null && other.transform.parent.gameObject == this._exit)
		{
			this._isNearShelter = false;
			GlobalTools.GetController<GameFlow>().ReportNearShelter(false);
		}
	}

	// Token: 0x06000E81 RID: 3713 RVA: 0x0003C2BF File Offset: 0x0003A4BF
	public bool IsPlayerNearShelter()
	{
		return this._isNearShelter;
	}

	// Token: 0x06000E82 RID: 3714 RVA: 0x0003C2C8 File Offset: 0x0003A4C8
	private void GrabItem()
	{
		if (this._itemToGrab == null && this._currentTarget != null)
		{
			ScavengeItemController x = this.FindClosestItemInSight();
			if (x != null && x == this._currentTarget)
			{
				this._thirdPersonController.StopMovement();
				if (this._gameFlow.HandsController.AddItem(this._currentTarget.ScavengeItem))
				{
					this._anim.SetBool("grabbing", true);
					this._currentTarget.TargetHighlight(false);
					this._itemToGrab = this._currentTarget;
					this._canGrab = false;
					this._canInteract = false;
					this._currentTarget.ScavengeItem.AddHeldItem(1);
					this._gameFlow.ReportCollectedItem(this._currentTarget);
					this._gameFlow.ChallengeItemsController.DisableScavengeItem(this._currentTarget.ScavengeItem);
					ScavengeItemManager.Instance.UnregisterController(this._currentTarget);
					this._currentTarget = null;
					this._nextInteractionTime = Time.time + this._interactionDelay;
					return;
				}
				if (this._showGrabLimit && this._nextShowGrabLimitTime <= Time.time)
				{
					this._gameFlow.NoRoomText.ShowText();
					if (GameSessionData.Instance.Character == ECharacter.DAD)
					{
						AudioManager.PlaySound(this._nopeMaleSoundName, 1f, 1f, 0f);
					}
					else
					{
						AudioManager.PlaySound(this._nopeFemaleSoundName, 1f, 1f, 0f);
					}
					this.DelayShowGrabLimit();
				}
			}
		}
	}

	// Token: 0x06000E83 RID: 3715 RVA: 0x0003C452 File Offset: 0x0003A652
	public void DelayShowGrabLimit()
	{
		this._nextShowGrabLimitTime = Time.time + this._showGrabLimitDelay;
	}

	// Token: 0x06000E84 RID: 3716 RVA: 0x0003C468 File Offset: 0x0003A668
	private float GetAngleTo(GameObject target)
	{
		Vector3 vector = target.transform.position - base.gameObject.transform.position;
		return Vector3.Angle(new Vector3(base.gameObject.transform.forward.x, 0f, base.gameObject.transform.forward.z), new Vector3(vector.x, 0f, vector.z));
	}

	// Token: 0x06000E85 RID: 3717 RVA: 0x0003C4E8 File Offset: 0x0003A6E8
	private ScavengeItemController FindClosestItemInSight()
	{
		ScavengeItemController result = null;
		float num = 9999f;
		List<ScavengeItemController> scavengeItems = ScavengeItemManager.Instance.ScavengeItems;
		for (int i = 0; i < scavengeItems.Count; i++)
		{
			if (scavengeItems[i] != null && scavengeItems[i].CanBePickedUp && this.GetAngleTo(scavengeItems[i].gameObject) < this._maxAngle)
			{
				Vector3 vector = scavengeItems[i].transform.position - base.transform.position;
				vector.y = 0f;
				if (vector.magnitude <= scavengeItems[i].MaxReachDistance && vector.magnitude < num && scavengeItems[i].RaycastTarget != null)
				{
					Vector3 end = base.gameObject.transform.position + Vector3.up;
					RaycastHit raycastHit;
					if (Physics.Linecast(scavengeItems[i].RaycastTarget.position, end, out raycastHit, this._layerMask.value) && raycastHit.collider.gameObject == base.gameObject)
					{
						result = scavengeItems[i];
						num = vector.magnitude;
					}
				}
			}
		}
		return result;
	}

	// Token: 0x17000319 RID: 793
	// (get) Token: 0x06000E86 RID: 3718 RVA: 0x0003C634 File Offset: 0x0003A834
	public Color HighlightColor
	{
		get
		{
			return this._highlightColor;
		}
	}

	// Token: 0x1700031A RID: 794
	// (get) Token: 0x06000E87 RID: 3719 RVA: 0x0003C63C File Offset: 0x0003A83C
	public Color TargetHighlightColor
	{
		get
		{
			return this._targetHighlightColor;
		}
	}

	// Token: 0x1700031B RID: 795
	// (get) Token: 0x06000E88 RID: 3720 RVA: 0x0003C644 File Offset: 0x0003A844
	public Color TargetHighlightColorInventoryFull
	{
		get
		{
			return this._targetHighlightColorInventoryFull;
		}
	}

	// Token: 0x04000898 RID: 2200
	[SerializeField]
	private float _maxAngle = 45f;

	// Token: 0x04000899 RID: 2201
	[SerializeField]
	private GameObject _grabEffectTemplate;

	// Token: 0x0400089A RID: 2202
	[SerializeField]
	private GameObject _grabEffectSource;

	// Token: 0x0400089B RID: 2203
	private bool _canInteract = true;

	// Token: 0x0400089C RID: 2204
	private bool _isNearShelter;

	// Token: 0x0400089D RID: 2205
	private bool _canGrab;

	// Token: 0x0400089E RID: 2206
	private bool _canDrop;

	// Token: 0x0400089F RID: 2207
	private bool _showGrabLimit;

	// Token: 0x040008A0 RID: 2208
	private bool _endInteractionDone;

	// Token: 0x040008A1 RID: 2209
	private ScavengeItemController _itemToGrab;

	// Token: 0x040008A2 RID: 2210
	private ScavengeItemController _currentTarget;

	// Token: 0x040008A3 RID: 2211
	private GameObject _exit;

	// Token: 0x040008A4 RID: 2212
	private float _nextInteractionTime;

	// Token: 0x040008A5 RID: 2213
	private float _nextShowGrabLimitTime;

	// Token: 0x040008A6 RID: 2214
	[SerializeField]
	private float _interactionDelay = 1f;

	// Token: 0x040008A7 RID: 2215
	[SerializeField]
	private float _showGrabLimitDelay = 1f;

	// Token: 0x040008A8 RID: 2216
	[SerializeField]
	private LayerMask _layerMask;

	// Token: 0x040008A9 RID: 2217
	[SerializeField]
	private float _targetUpdateTime = 0.05f;

	// Token: 0x040008AA RID: 2218
	[SerializeField]
	private Color _highlightColor = Color.white;

	// Token: 0x040008AB RID: 2219
	[SerializeField]
	private Color _targetHighlightColor = Color.red;

	// Token: 0x040008AC RID: 2220
	[SerializeField]
	private Color _targetHighlightColorInventoryFull = Color.red;

	// Token: 0x040008AD RID: 2221
	[SerializeField]
	private float _maxInteractionDistance = 5f;

	// Token: 0x040008AE RID: 2222
	[EventRef]
	[SerializeField]
	private string _nopeMaleSoundName;

	// Token: 0x040008AF RID: 2223
	[EventRef]
	[SerializeField]
	private string _nopeFemaleSoundName;

	// Token: 0x040008B0 RID: 2224
	[EventRef]
	[SerializeField]
	private string _scavengeGrabSoundName;

	// Token: 0x040008B1 RID: 2225
	[EventRef]
	[SerializeField]
	private string _scavengeDropSoundName;

	// Token: 0x040008B2 RID: 2226
	[SerializeField]
	private ThirdPersonController _thirdPersonController;

	// Token: 0x040008B3 RID: 2227
	[SerializeField]
	private GlobalIntVariable _nukeDropsSurvivedVariable;

	// Token: 0x040008B4 RID: 2228
	[SerializeField]
	private GlobalIntVariable _perishedInExplosionVariable;

	// Token: 0x040008B5 RID: 2229
	[SerializeField]
	private GlobalIntVariable _totalItemsCollectedVariable;

	// Token: 0x040008B6 RID: 2230
	[SerializeField]
	private GlobalIntVariable _mostSuppliesCollectedVariable;

	// Token: 0x040008B7 RID: 2231
	[SerializeField]
	private GlobalIntVariable _peoplePerishedFromNukeDropVariable;

	// Token: 0x040008B8 RID: 2232
	private PlayerInventory _inventory;

	// Token: 0x040008B9 RID: 2233
	private Animator _anim;

	// Token: 0x040008BA RID: 2234
	private GameFlow _gameFlow;

	// Token: 0x040008BB RID: 2235
	private Shelter _shelter;

	// Token: 0x040008BC RID: 2236
	private Player _player;

	// Token: 0x040008BD RID: 2237
	private const int PLAYER_INDEX = 0;
}

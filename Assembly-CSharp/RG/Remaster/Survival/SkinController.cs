using System;
using System.Collections;
using System.Collections.Generic;
using Rewired;
using RG.Parsecs.Common;
using RG.Parsecs.EventEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

namespace RG.Remaster.Survival
{
	// Token: 0x02000236 RID: 566
	public class SkinController : MonoBehaviour
	{
		// Token: 0x1700041D RID: 1053
		// (get) Token: 0x060015AC RID: 5548 RVA: 0x0006022E File Offset: 0x0005E42E
		public GlobalIntVariable CurrentSkinIndex
		{
			get
			{
				return this._currentSkinIndex;
			}
		}

		// Token: 0x1700041E RID: 1054
		// (get) Token: 0x060015AD RID: 5549 RVA: 0x00060236 File Offset: 0x0005E436
		public SkinDataList DataList
		{
			get
			{
				return this._dataList;
			}
		}

		// Token: 0x060015AE RID: 5550 RVA: 0x00060240 File Offset: 0x0005E440
		private void Awake()
		{
			this._player = ReInput.players.GetPlayer(0);
			Skin[] componentsInChildren = base.GetComponentsInChildren<Skin>(true);
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				this._skins.Add(componentsInChildren[i]);
			}
			SkinManager.Instance.RegisterSkinController(this);
		}

		// Token: 0x060015AF RID: 5551 RVA: 0x0006028D File Offset: 0x0005E48D
		private void Start()
		{
			this.RefreshSkins();
		}

		// Token: 0x060015B0 RID: 5552 RVA: 0x00060295 File Offset: 0x0005E495
		private void OnMouseUpAsButton()
		{
			if (this._useLeftClick)
			{
				this.CheckClick(true);
			}
		}

		// Token: 0x060015B1 RID: 5553 RVA: 0x000602A8 File Offset: 0x0005E4A8
		private void Update()
		{
			if (this.isMouseOver)
			{
				if (this._useRightClick && this._player.GetButtonUp(33))
				{
					this.CheckClick(true);
					return;
				}
				if (this._useRightClick && this._player.GetButtonUp(37))
				{
					this.CheckClick(false);
				}
			}
		}

		// Token: 0x060015B2 RID: 5554 RVA: 0x000602FA File Offset: 0x0005E4FA
		private void OnMouseEnter()
		{
			this.isMouseOver = true;
		}

		// Token: 0x060015B3 RID: 5555 RVA: 0x00060303 File Offset: 0x0005E503
		private void OnMouseExit()
		{
			this.isMouseOver = false;
		}

		// Token: 0x060015B4 RID: 5556 RVA: 0x0006030C File Offset: 0x0005E50C
		private void CheckClick(bool moveForward)
		{
			if (!EventSystem.current.IsPointerOverGameObject())
			{
				this.ChangeSkin(moveForward);
			}
		}

		// Token: 0x060015B5 RID: 5557 RVA: 0x00060324 File Offset: 0x0005E524
		private void ChangeSkin(bool moveForward)
		{
			if (this._allowSkinChange && this._skins.Count > 0 && this._dataList != null && this._dataList.IsValid())
			{
				if (moveForward)
				{
					this.UpdateSkinIndexToNext();
				}
				else
				{
					this.UpdateSkinIndexToPrevious();
				}
				this.RefreshSkins();
				return;
			}
			this.PlayDeclineSound();
		}

		// Token: 0x060015B6 RID: 5558 RVA: 0x00060380 File Offset: 0x0005E580
		private void RefreshSkins()
		{
			for (int i = 0; i < this._skins.Count; i++)
			{
				if (this._skins[i].Id.Equals(this._dataList.SkinData[this._currentSkinIndex.Value].SkinId))
				{
					this._skins[i].gameObject.SetActive(true);
				}
				else
				{
					this._skins[i].gameObject.SetActive(false);
				}
			}
		}

		// Token: 0x060015B7 RID: 5559 RVA: 0x0006040B File Offset: 0x0005E60B
		public void AllowSkinChange()
		{
			this._allowSkinChange = true;
		}

		// Token: 0x060015B8 RID: 5560 RVA: 0x00060414 File Offset: 0x0005E614
		public void ForceSkinUse(SkinId forcedSkinId)
		{
			this._allowSkinChange = false;
			for (int i = 0; i < this._dataList.SkinData.Count; i++)
			{
				if (this._dataList.SkinData[i].SkinId.Equals(forcedSkinId))
				{
					this._currentSkinIndex.Value = i;
				}
			}
			this.RefreshSkins();
		}

		// Token: 0x060015B9 RID: 5561 RVA: 0x00060473 File Offset: 0x0005E673
		public IEnumerator PlaySound(string eventName)
		{
			AudioManager.PlaySoundAndReturnInstance(eventName, 1f, 1f, 0f);
			yield return null;
			yield break;
		}

		// Token: 0x060015BA RID: 5562 RVA: 0x00060482 File Offset: 0x0005E682
		private void PlaySuccessSound()
		{
			if (this._successSoundSlot != null && !string.IsNullOrEmpty(this._successSoundSlot.SoundEventName))
			{
				Singleton<GameManager>.Instance.PlaySoundInvoke(this.PlaySound(this._successSoundSlot.SoundEventName));
			}
		}

		// Token: 0x060015BB RID: 5563 RVA: 0x000604BF File Offset: 0x0005E6BF
		private void PlayDeclineSound()
		{
			if (this._declineSoundSlot != null && !string.IsNullOrEmpty(this._declineSoundSlot.SoundEventName))
			{
				Singleton<GameManager>.Instance.PlaySoundInvoke(this.PlaySound(this._declineSoundSlot.SoundEventName));
			}
		}

		// Token: 0x060015BC RID: 5564 RVA: 0x000604FC File Offset: 0x0005E6FC
		private void UpdateSkinIndexToNext()
		{
			int num = 0;
			for (int i = this._currentSkinIndex.Value + 1; i < this._dataList.SkinData.Count; i++)
			{
				if (this._dataList.SkinData[i].IsUnlockedVariable.Value)
				{
					if (this._dataList.SkinData[i].AdditionalRequirements == null || this._dataList.SkinData[i].AdditionalRequirements.Length == 0)
					{
						num = i;
						break;
					}
					bool flag = true;
					for (int j = 0; j < this._dataList.SkinData[i].AdditionalRequirements.Length; j++)
					{
						if (!this._dataList.SkinData[i].AdditionalRequirements[j].Value)
						{
							flag = false;
							break;
						}
					}
					if (flag)
					{
						num = i;
						break;
					}
				}
			}
			if (this._currentSkinIndex.Value != num)
			{
				this.PlaySuccessSound();
			}
			else
			{
				this.PlayDeclineSound();
			}
			this._currentSkinIndex.Value = num;
		}

		// Token: 0x060015BD RID: 5565 RVA: 0x00060604 File Offset: 0x0005E804
		private void UpdateSkinIndexToPrevious()
		{
			int num = 0;
			for (int i = (this._currentSkinIndex.Value == 0) ? (this._dataList.SkinData.Count - 1) : (this._currentSkinIndex.Value - 1); i > 0; i--)
			{
				if (this._dataList.SkinData[i].IsUnlockedVariable.Value)
				{
					if (this._dataList.SkinData[i].AdditionalRequirements == null || this._dataList.SkinData[i].AdditionalRequirements.Length == 0)
					{
						num = i;
						break;
					}
					bool flag = true;
					for (int j = 0; j < this._dataList.SkinData[i].AdditionalRequirements.Length; j++)
					{
						if (!this._dataList.SkinData[i].AdditionalRequirements[j].Value)
						{
							flag = false;
							break;
						}
					}
					if (flag)
					{
						num = i;
						break;
					}
				}
			}
			if (this._currentSkinIndex.Value != num)
			{
				this.PlaySuccessSound();
			}
			else
			{
				this.PlayDeclineSound();
			}
			this._currentSkinIndex.Value = num;
		}

		// Token: 0x04000E9A RID: 3738
		private const int RIGHT_MOUSE_BUTTON = 1;

		// Token: 0x04000E9B RID: 3739
		[SerializeField]
		private bool _allowSkinChange = true;

		// Token: 0x04000E9C RID: 3740
		[SerializeField]
		private bool _useRightClick = true;

		// Token: 0x04000E9D RID: 3741
		[SerializeField]
		private bool _useLeftClick;

		// Token: 0x04000E9E RID: 3742
		[SerializeField]
		private GlobalIntVariable _currentSkinIndex;

		// Token: 0x04000E9F RID: 3743
		[SerializeField]
		private List<Skin> _skins = new List<Skin>();

		// Token: 0x04000EA0 RID: 3744
		[SerializeField]
		private SkinDataList _dataList;

		// Token: 0x04000EA1 RID: 3745
		[FormerlySerializedAs("_soundSlot")]
		[SerializeField]
		private SoundSlot _successSoundSlot;

		// Token: 0x04000EA2 RID: 3746
		[SerializeField]
		private SoundSlot _declineSoundSlot;

		// Token: 0x04000EA3 RID: 3747
		private Player _player;

		// Token: 0x04000EA4 RID: 3748
		private bool isMouseOver;
	}
}

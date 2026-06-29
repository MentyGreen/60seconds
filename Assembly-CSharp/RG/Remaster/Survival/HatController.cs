using System;
using System.Collections;
using RG.Parsecs.Common;
using RG.Parsecs.EventEditor;
using RG.Parsecs.Survival;
using UnityEngine;
using UnityEngine.Serialization;

namespace RG.Remaster.Survival
{
	// Token: 0x0200022C RID: 556
	[RequireComponent(typeof(CharacterSlot))]
	public class HatController : MonoBehaviour
	{
		// Token: 0x0600157B RID: 5499 RVA: 0x0005ECBC File Offset: 0x0005CEBC
		private void Start()
		{
			this._characterSlot = base.GetComponent<CharacterSlot>();
			this._character = this._characterSlot.GetCharacter();
			if (this._currentHatIndex != null && this._currentHatIndex.Value != 0)
			{
				this._character.RuntimeData.AddStatus(this._hatList.HatData[this._currentHatIndex.Value].IsWornStatus);
			}
		}

		// Token: 0x0600157C RID: 5500 RVA: 0x0005ED34 File Offset: 0x0005CF34
		public void HatClicked(bool findNext = false)
		{
			if (this._hatChangesAllowed == null || this._hatChangesAllowed.Value)
			{
				if (this._hatList != null && this._hatList.HatData != null && this._hatList.HatData.Count > 1)
				{
					this.RemoveHatRelatedStatusesFromCharacter();
					this._previousHatIndex = this._currentHatIndex.Value;
					if (findNext)
					{
						this.IncrementHatIndex();
					}
					else
					{
						this.DecrementHatIndex();
					}
					this._characterSlot.OnEndOfDay();
					return;
				}
			}
			else
			{
				this.PlayDeclineSound();
			}
		}

		// Token: 0x0600157D RID: 5501 RVA: 0x0005EDC4 File Offset: 0x0005CFC4
		private void IncrementHatIndex()
		{
			this._currentHatIndex.Value++;
			if (this._currentHatIndex.Value < this._hatList.HatData.Count)
			{
				if (!this._hatList.HatData[this._currentHatIndex.Value].IsUnlockedVariable.Value || !this._hatList.HatData[this._currentHatIndex.Value].AllowedCharacters.Contains(this._character) || this._hatList.HatData[this._currentHatIndex.Value].DisallowedMissions.Contains(MissionManager.Instance.GetCurrentMission()))
				{
					this.IncrementHatIndex();
					return;
				}
				if (this._hatList.HatData[this._currentHatIndex.Value].IsWornStatus != null && this._character != null)
				{
					this._character.RuntimeData.AddStatus(this._hatList.HatData[this._currentHatIndex.Value].IsWornStatus);
					if (this._wasHatChangedSuccessfully != null)
					{
						this._wasHatChangedSuccessfully.SetValue(true);
						if (!AchievementsSystem.IsAchievementUnlocked(this._madHatterAchievement))
						{
							AchievementsSystem.UnlockAchievement(this._madHatterAchievement);
						}
						if (this._successSoundSlot != null && !string.IsNullOrEmpty(this._successSoundSlot.SoundEventName))
						{
							Singleton<GameManager>.Instance.PlaySoundInvoke(this.PlaySound(this._successSoundSlot.SoundEventName));
							return;
						}
					}
				}
			}
			else
			{
				this._currentHatIndex.Value = 0;
				if (this._currentHatIndex.Value != this._previousHatIndex)
				{
					this.PlaySuccessSound();
					return;
				}
				this.PlayDeclineSound();
			}
		}

		// Token: 0x0600157E RID: 5502 RVA: 0x0005EFA4 File Offset: 0x0005D1A4
		private void DecrementHatIndex()
		{
			this._currentHatIndex.Value--;
			if (this._currentHatIndex.Value != 0)
			{
				if (this._currentHatIndex.Value > 0)
				{
					if (!this._hatList.HatData[this._currentHatIndex.Value].IsUnlockedVariable.Value || !this._hatList.HatData[this._currentHatIndex.Value].AllowedCharacters.Contains(this._character) || this._hatList.HatData[this._currentHatIndex.Value].DisallowedMissions.Contains(MissionManager.Instance.GetCurrentMission()))
					{
						this.DecrementHatIndex();
						return;
					}
					if (this._hatList.HatData[this._currentHatIndex.Value].IsWornStatus != null && this._character != null)
					{
						this._character.RuntimeData.AddStatus(this._hatList.HatData[this._currentHatIndex.Value].IsWornStatus);
						if (this._wasHatChangedSuccessfully != null)
						{
							this._wasHatChangedSuccessfully.SetValue(true);
							if (!AchievementsSystem.IsAchievementUnlocked(this._madHatterAchievement))
							{
								AchievementsSystem.UnlockAchievement(this._madHatterAchievement);
							}
							if (this._successSoundSlot != null && !string.IsNullOrEmpty(this._successSoundSlot.SoundEventName))
							{
								Singleton<GameManager>.Instance.PlaySoundInvoke(this.PlaySound(this._successSoundSlot.SoundEventName));
								return;
							}
						}
					}
				}
				else
				{
					this._currentHatIndex.Value = this._hatList.HatData.Count;
					this.DecrementHatIndex();
				}
				return;
			}
			if (this._currentHatIndex.Value != this._previousHatIndex)
			{
				this.PlaySuccessSound();
				return;
			}
			this.PlayDeclineSound();
		}

		// Token: 0x0600157F RID: 5503 RVA: 0x0005F196 File Offset: 0x0005D396
		public IEnumerator PlaySound(string eventName)
		{
			AudioManager.PlaySoundAndReturnInstance(eventName, 1f, 1f, 0f);
			yield return null;
			yield break;
		}

		// Token: 0x06001580 RID: 5504 RVA: 0x0005F1A5 File Offset: 0x0005D3A5
		private void PlaySuccessSound()
		{
			if (this._successSoundSlot != null && !string.IsNullOrEmpty(this._successSoundSlot.SoundEventName))
			{
				Singleton<GameManager>.Instance.PlaySoundInvoke(this.PlaySound(this._successSoundSlot.SoundEventName));
			}
		}

		// Token: 0x06001581 RID: 5505 RVA: 0x0005F1E2 File Offset: 0x0005D3E2
		private void PlayDeclineSound()
		{
			if (this._declineSoundSlot != null && !string.IsNullOrEmpty(this._declineSoundSlot.SoundEventName))
			{
				Singleton<GameManager>.Instance.PlaySoundInvoke(this.PlaySound(this._declineSoundSlot.SoundEventName));
			}
		}

		// Token: 0x06001582 RID: 5506 RVA: 0x0005F220 File Offset: 0x0005D420
		private void RemoveHatRelatedStatusesFromCharacter()
		{
			if (this._character != null)
			{
				for (int i = 0; i < this._hatList.HatData.Count; i++)
				{
					if (this._hatList.HatData[i].IsWornStatus != null && this._character.RuntimeData.HasStatus(this._hatList.HatData[i].IsWornStatus.Id))
					{
						this._character.RuntimeData.RemoveStatus(this._hatList.HatData[i].IsWornStatus);
					}
				}
			}
		}

		// Token: 0x04000E5F RID: 3679
		[SerializeField]
		private GlobalIntVariable _currentHatIndex;

		// Token: 0x04000E60 RID: 3680
		private int _previousHatIndex;

		// Token: 0x04000E61 RID: 3681
		[SerializeField]
		private HatDataList _hatList;

		// Token: 0x04000E62 RID: 3682
		[SerializeField]
		private GlobalBoolVariable _hatChangesAllowed;

		// Token: 0x04000E63 RID: 3683
		[SerializeField]
		private GlobalBoolVariable _wasHatChangedSuccessfully;

		// Token: 0x04000E64 RID: 3684
		private Character _character;

		// Token: 0x04000E65 RID: 3685
		private CharacterSlot _characterSlot;

		// Token: 0x04000E66 RID: 3686
		[FormerlySerializedAs("_soundSlot")]
		[SerializeField]
		private SoundSlot _successSoundSlot;

		// Token: 0x04000E67 RID: 3687
		[SerializeField]
		private SoundSlot _declineSoundSlot;

		// Token: 0x04000E68 RID: 3688
		[SerializeField]
		private Achievement _madHatterAchievement;

		// Token: 0x04000E69 RID: 3689
		public bool TestOnlyReverseHatClicked;
	}
}
